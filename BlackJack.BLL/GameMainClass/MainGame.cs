using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.Randomize;
using BlackJack.BLL.Infrastructure;
using BlackJack.BLL.Helpers;
using BlackJack.DataOnView.PageView;


namespace BlackJack.BLL.GameMainClass
{
    public class MainGame : IMainGame
    {
        private Game Game;
        private List<Player> Players;
        private Deck Deck;
        
        private int DefaultPlayerScore = 8000;
        private List<string> RoundMessages;
        private Dictionary<int, RoundResult> RoundResults;

        private enum RoundResult
        {
            Continue,
            IsBlackJack,
            IsOneToOne,
            IsBetReturning,
            IsBetLossing
        }


        private IUnitOfWork db;

        public MainGame(IUnitOfWork context)
        {
            db = context;
        }



        #region GameCreation

        public void PlayerNameValidation(string name)
        {
            if (name == "")
            {
                throw new ValidationException(Message.NameFieldIsEmpty);
            }
        }

        public bool PlayerGameExist(string name)
        {
            bool result = false;

            if (db.Players.SelectByName(name) != null)
            {
                result = true;
            }

            return result;
        }

        public void Create(string humanPlayerName, int AmountOfBots)
        {
            Initialization();

            if (db.Players.SelectByName(humanPlayerName) != null)
            {
                InitializationFromDB(humanPlayerName);
                GameDelete();
            }

            AddDealer();
            AddPlayerToGame(false, true, humanPlayerName);
            AddBots(AmountOfBots);
            BotsAndDealerNamesGenerating();

            foreach(Player player in Players)
            {
                player.Cards = new List<Card>();
            }

            Game.Players = Players;

            DeckGeneration();
            Game.Deck = Deck;

            db.Games.Create(Game);

            InitializationFromDB(humanPlayerName);
            GameStageUpdate();
        }

        public void Resume(string humanPlayerName)
        {
            Initialization();

            Game = db.Players.SelectByName(humanPlayerName).Game;
            Players = Game.Players;
            Deck = Game.Deck;

            Player dealer = GetDealer();
            Player human = GetHumanPlayer();
            Players.Remove(dealer);
            Players.Remove(human);
            Players.Insert(0, dealer);
            Players.Insert(1, human);
        }
        
        
        private void Initialization()
        {
            Game = new Game();
            Players = new List<Player>();
            Deck = new Deck();

            RoundMessages = new List<string>();
            RoundResults = new Dictionary<int, RoundResult>();
        }

        private void InitializationFromDB(string humanPlayerName)
        {
            Game = db.Players.SelectByName(humanPlayerName).Game;
            Players = Game.Players;
            Deck = Game.Deck;
        }

        private void AddPlayerToGame(bool IsDealer = false, bool IsHuman = false, string Name = "")
        {
            Player player = new Player();
            player.Name = Name;
            player.Game = Game;
            player.Score = DefaultPlayerScore;
            player.IsDealer = IsDealer;
            player.IsHuman = IsHuman;

            Players.Add(player);
        }

        private void AddDealer()
        {
            AddPlayerToGame(true);
        }

        private void AddBots(int AmountOfBots)
        {
            for (int i = 0; i < AmountOfBots; i++)
            {
                AddPlayerToGame();
            }
        }
        
        private void BotsAndDealerNamesGenerating()
        {
            Random random = new Random();
            
            foreach (Player player in Players)
            {
                if (!player.IsHuman)
                {
                    player.Name = BotNames.Names[random.Next(BotNames.Names.Length)] + " " + BotNames.Names[random.Next(BotNames.Names.Length)];
                }
            }
        }

       
        private void DeckGeneration()
        {
            List<Card> cards = db.Cards.GetAll().ToList();
            Deck.Cards = cards;
        }

        #endregion
        

        #region RoundStarting

        public RoundStartPage GenerateRoundStartPage()
        {
            RoundStartPage roundStartPage = new RoundStartPage();

            roundStartPage.Players = new List<PlayerInfo>();
            foreach (Player player in Players)
            {
                PlayerInfo playerInfo = GetPlayerInfoForView(player);
                playerInfo.Name = player.Name;
                playerInfo.Score = player.Score;

                playerInfo.PlayerType = "Bot";
                if (player.IsHuman)
                {
                    playerInfo.PlayerType = "Human";
                }
                if (player.IsDealer)
                {
                    playerInfo.PlayerType = "Dealer";
                }

                roundStartPage.Players.Add(playerInfo);
            }

            return roundStartPage;
        }

        public void BetsCreation(int HumanBet)
        {
            IRandomize randomize = new GameRandomize();
            Player humanPlayer = (Player)Game.Players.Where(m => m.IsHuman).First();
            BetValidation(humanPlayer, HumanBet);

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsHuman)
                {
                    CreateBet(Players[i], HumanBet);
                }

                if (!Players[i].IsHuman && !Players[i].IsDealer)
                {
                    CreateBet(Players[i], randomize.BetGenerate(Players[i].Score));
                }
            }
        }


        private void BetValidation(Player player, int bet)
        {
            if (player.Score < bet)
            {
                throw new ValidationException(Message.BetMoreThanScore);
            }
        }

        private void CreateBet(Player player, int bet)
        {
            player.Bet = bet;
            player.Score = player.Score - bet;
            db.Players.Update(player);
        }

        #endregion


        #region FirstRoundPhase

        public void FirstCardsDistribution()
        {
            DeckShuffling();

            foreach (Player player in Players)
            {
                if (player.Cards.Count != 2)
                {
                    CardAddingToPlayer(player, CardTakingFromDeck());
                    CardAddingToPlayer(player, CardTakingFromDeck());
                    RoundScoreCount(player);
                }
            }

            Players = db.Games.Get(Game.Id).Players;
            FirstCardsChecking();
        }
        

        private void FirstCardsChecking()
        {
            RoundResults.Clear();

            bool dealerBjDanger = DealerBjDanger();
            if (dealerBjDanger)
            {
                RoundMessages.Add(Message.DealerBjDanger);
            }

            for (int i = 1; i < Players.Count; i++)
            {
                RoundResults.Add(Players[i].Id, RoundResult.Continue);
                bool playerBj = PlayerBj(Players[i]);

                if (playerBj)
                {
                    RoundMessages.Add(Players[i].Name + Message.PlayerHasBj);
                    RoundResults[Players[i].Id] = RoundResult.IsBlackJack;
                }

                if (playerBj && dealerBjDanger)
                {
                    RoundResults[Players[i].Id] = RoundResult.IsOneToOne;
                }

                if (playerBj && dealerBjDanger && Players[i].IsHuman)
                {
                    RoundResults[Players[i].Id] = RoundResult.Continue;
                    RoundMessages.Add(Message.Press1ToTakeReward);
                }
            }

            RoundMessages.Add(Message.Press0ToContinue);
        }
        
        private bool DealerBjDanger()
        {
            bool danger = false;

            Player dealer = GetDealer();
            Card first = dealer.Cards[0];

            if (first.Value >= 10)
            {
                danger = true;
            }

            return danger;
        }

        #endregion


        #region SecondRoundPhase

        public bool CanHumanTakeOneMoreCard ()
        {
            bool result = true;

            Player human = GetHumanPlayer();
            if (human.RoundScore >= 21)
            {
                result = false;
            }

            return result;
        }

        public void OneMoreCardAddingToHumanPlayer()
        {
            Player human = GetHumanPlayer();
            CardAddingToPlayer(human, CardTakingFromDeck());
            
            RoundScoreCount(human);
        }


        public void SecondCardsDistributionToBots ()
        {
            for (int i = 2; i < Players.Count; i++)
            {
                if (RoundEndedForPlayer(Players[i]))
                {
                    continue;
                }

                SecondCardsAddingToBot(Players[i]);
            }

            SecondCardsAddingToBot(GetDealer());

            SecondCardsChecking();
        }
        

        private void SecondCardsChecking()
        {
            RoundResults.Clear();

            Player dealer = GetDealer();
            bool dealerBj = PlayerBj(dealer);


            for (int i = 1; i < Players.Count; i++)
            {
                if (RoundEndedForPlayer(Players[i]))
                {
                    continue;
                }


                RoundResults.Add(Players[i].Id, RoundResult.IsBetLossing);
                RoundMessages.Add(Players[i].Name + Message.PlayerLost);

                if (PlayerBj(Players[i]) && !dealerBj)
                {
                    RoundResults[Players[i].Id] = RoundResult.IsBlackJack;

                    RoundMessages.Remove(RoundMessages.Last());
                    RoundMessages.Add(Players[i].Name + Message.PlayerHasBj);
                }

                if (PlayerScoreEqualsDealerScore(Players[i]))
                {
                    RoundResults[Players[i].Id] = RoundResult.IsBetReturning;

                    RoundMessages.Remove(RoundMessages.Last());
                    RoundMessages.Add(Players[i].Name + Message.PlayerScoreEqualsDealerScore);
                }

                if (PlayerScoreBetterThanDealerScore(Players[i]))
                {
                    RoundResults[Players[i].Id] = RoundResult.IsOneToOne;

                    RoundMessages.Remove(RoundMessages.Last());
                    RoundMessages.Add(Players[i].Name + Message.PlayerScoreBetterThanDealerScore);
                }
            }
        }

        private void SecondCardsAddingToBot(Player player)
        {
            while(player.RoundScore < 18)
            {
                CardAddingToPlayer(player, CardTakingFromDeck());
                RoundScoreCount(player);
            }
        }

        #endregion


        #region RoundEnding

        public void RoundEnding()
        {
            foreach(Player player in Players)
            {
                RemovingCardsFromPlayer(player);
                player.RoundScore = 0;

                db.Players.Update(player);
            }
        }
        
        public bool RemovingBotsWithNullScore()
        {
            bool result = false;

            for (int i = 2; i < Players.Count; i++)
            {
                if (GameEndedForPlayer(Players[i]))
                {
                    RoundMessages.Add(Players[i].Name + Message.PlayerEndedTheGame);
                    db.Players.Delete(Players[i].Id);
                    result = true;
                }
            }

            Players = db.Games.Get(Game.Id).Players;
            return result;
        }


        private void RemovingCardsFromPlayer(Player player)
        {
            foreach(Card card in player.Cards)
            {
                ReturnCardToDeck(card);
            }

            player.Cards = new List<Card>();
        }

        #endregion


        #region GameOver

        public void GameDelete()
        {
            for(int i = 0; Players.Count != 0; )
            {
                db.Players.Delete(Players[i].Id);
            }

            db.Decks.Delete(Deck.Id);

            db.Games.Delete(Game.Id);
        }

        public bool IsGameOver()
        {
            bool result = false;

            if (GameEndedForPlayer(GetDealer()) || (GameEndedForPlayer(GetHumanPlayer()) && RoundEndedForPlayer(GetHumanPlayer())))
            {
                result = true;
            }

            return result;
        }

        public string GetWinner()
        {
            string winner;

            winner = Message.DealerIsWinner;
            Player human = GetHumanPlayer();
            if(human.Score > 0)
            {
                winner = Message.DealerIsLoser;
            }

            return winner;
        }

        #endregion


        #region Stage
        
        public void GameStageIncrement()
        {
            Game.Stage++;
            db.Games.Update(Game);
        }

        public void GameStageUpdate()
        {
            Game.Stage = 0;
            db.Games.Update(Game);
        }

        public int GetStage()
        {
            int stage;

            stage = Game.Stage;

            return stage;
        }

        #endregion



        public void BetPayment(int key = 0)
        {
            if (key == 1)
            {
                Player human = GetHumanPlayer();
                PayOneToOne(human);
                RoundMessages.Add(human.Name + Message.PlayerGetsOneToOnePayment);
            }

            foreach (var roundResult in RoundResults)
            {
                Player player = Players.Where(m => m.Id == roundResult.Key).First();

                if (roundResult.Value == RoundResult.IsBlackJack)
                {
                    PayBj(player);
                    RoundMessages.Add(player.Name + Message.PlayerGetsBjPayment);
                }

                if (roundResult.Value == RoundResult.IsOneToOne)
                {
                    PayOneToOne(player);
                    RoundMessages.Add(player.Name + Message.PlayerGetsOneToOnePayment);
                }

                if (roundResult.Value == RoundResult.IsBetLossing)
                {
                    BetLossing(player);
                    RoundMessages.Add(player.Name + Message.PlayerLostBet);
                }

                if (roundResult.Value == RoundResult.IsBetReturning)
                {
                    BetReturning(player);
                    RoundMessages.Add(Message.BetGoesBackToPlayer + player.Name);
                }
            }
        }

        public RoundProcessPage GenerateRoundProcessPage()
        {
            RoundProcessPage roundProcessPage = new RoundProcessPage();

            roundProcessPage.Players = new List<PlayerWithCardsInfo>();
            foreach (Player player in Players)
            {
                PlayerWithCardsInfo playerWithCardsInfo = new PlayerWithCardsInfo();
                playerWithCardsInfo.Cards = GetCardsForView(player);
                playerWithCardsInfo.PlayerInfo = GetPlayerInfoForView(player);
                playerWithCardsInfo.RoundScore = player.RoundScore;
                playerWithCardsInfo.Bet = player.Bet;

                roundProcessPage.Players.Add(playerWithCardsInfo);
            }

            roundProcessPage.Messages = new List<string>();
            foreach (string message in RoundMessages)
            {
                roundProcessPage.Messages.Add(message);
            }
            RoundMessages.Clear();

            return roundProcessPage;
        }



        private void CardAddingToPlayer(Player player, Card card)
        {
            player.Cards.Add(card);
            db.Players.Update(player);
        }

        private void RoundScoreCount(Player player)
        {
            int cardSum = player.Cards.Sum(m => m.Value);
            int AceCount = player.Cards
                .Where(m => m.Name == "Ace")
                .Count();

            while (AceCount > 0 && cardSum > 21)
            {
                AceCount--;
                cardSum -= 10;
            }

            player.RoundScore = cardSum;
            db.Players.Update(player);
        }


        private bool RoundEndedForPlayer(Player player)
        {
            bool result = false;

            if (player.Bet == 0)
            {
                result = true;
            }

            return result;
        }

        private bool GameEndedForPlayer(Player player)
        {
            bool result = false;

            if (player.Score <= 0)
            {
                result = true;
            }

            return result;
        }


        private bool PlayerBj(Player player)
        {
            bool result = false;

            if (player.RoundScore == 21 && player.Cards.Count == 2)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerMoreThan21(Player player)
        {
            bool result = false;

            if (player.RoundScore > 21)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerScoreEqualsDealerScore(Player player)
        {
            bool result = false;

            if (player.RoundScore == GetDealer().RoundScore && !PlayerMoreThan21(player))
            {
                result = true;
            }

            return result;
        }

        private bool PlayerScoreBetterThanDealerScore(Player player)
        {
            bool result = false;

            if (!PlayerMoreThan21(player) && (player.RoundScore > GetDealer().RoundScore || PlayerMoreThan21(GetDealer())))
            {
                result = true;
            }

            return result;
        }

        
        private Player GetDealer()
        {
            Player dealer;

            dealer = db.Players.GetAll().Where(m => m.IsDealer).First();

            return dealer;
        }

        private Player GetHumanPlayer()
        {
            Player humanPlayer;

            humanPlayer = Players.Where(m => m.IsHuman).First();

            return humanPlayer;
        }


        private void PayBj(Player player)
        {
            int pay = (int)(player.Bet * 1.5);

            player.Score += player.Bet + pay;
            player.Bet = 0;
            db.Players.Update(player);

            Player dealer = GetDealer();
            dealer.Score -= pay;
            db.Players.Update(dealer);
        }

        private void PayOneToOne(Player player)
        {
            int pay = player.Bet;

            player.Score += player.Bet + pay;
            player.Bet = 0;
            db.Players.Update(player);

            Player dealer = GetDealer();
            dealer.Score -= pay;
            db.Players.Update(dealer);
        }

        private void BetReturning(Player player)
        {
            player.Score += player.Bet;
            player.Bet = 0;
            db.Players.Update(player);
        }

        private void BetLossing(Player player)
        {
            Player dealer = GetDealer();
            dealer.Score += player.Bet;
            db.Players.Update(dealer);

            player.Bet = 0;
            db.Players.Update(player);
        }


        private List<string> GetCardsForView (Player player)
        {
            List<string> cardNames = new List<string>(); 

            List<Card> cards = player.Cards;
            foreach(Card card in cards)
            {
                cardNames.Add(card.Name);
            }

            return cardNames;
        }

        private PlayerInfo GetPlayerInfoForView(Player player)
        {
            PlayerInfo playerInfo = new PlayerInfo();

            playerInfo.Name = player.Name;
            playerInfo.Score = player.Score;

            playerInfo.PlayerType = "Bot";
            if (player.IsHuman)
            {
                playerInfo.PlayerType = "Human";
            }
            if (player.IsDealer)
            {
                playerInfo.PlayerType = "Dealer";
            }

            return playerInfo;
        }


        private void DeckShuffling()
        {
            Random random = new Random();
            int card1;
            int card2;
            Card glass;

            for (int i = 0; i < Deck.Cards.Count; i++)
            {
                card1 = random.Next(Deck.Cards.Count);
                card2 = random.Next(Deck.Cards.Count);

                glass = Deck.Cards[card1];
                Deck.Cards[card1] = Deck.Cards[card2];
                Deck.Cards[card2] = glass;
            }

            db.Decks.Update(Deck);
        }

        private Card CardTakingFromDeck()
        {
            Card card;

            IRandomize randomize = new GameRandomize();
            card = Deck.Cards[randomize.CardIdSelection(Deck.Cards.Count)];
            Deck.Cards.Remove(card);
            db.Decks.Update(Deck);

            return card;
        }

        private void ReturnCardToDeck(Card card)
        {
            Deck.Cards.Add(card);
            db.Decks.Update(Deck);
        }
    }
}
