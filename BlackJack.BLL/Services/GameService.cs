using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.ViewModels.ViewModels;
using BlackJack.Entity.Models;
using BlackJack.Entity.Enums;
using BlackJack.BLL.Services.Interfaces;


namespace BlackJack.BLL.Services
{
    public class GameService : IGameService
    {
        private IGameRepository GameRepository;
        private IPlayerRepository PlayerRepository;
        private IGamePlayerRepository GamePlayerRepository;
        private IPlayerCardRepository PlayerCardRepository;

        private IBetProvider BetProvider;
        private ICardDistributionProvider CardDistributionProvider;
        private ICardCheckProvider CardCheckProvider;

        private int DefaultPlayerScore = 8000;


        public GameService()
        {
            PlayerRepository = new PlayerRepository();
            GameRepository = new GameRepository();
            PlayerCardRepository = new PlayerCardRepository();
            GamePlayerRepository = new GamePlayerRepository();

            BetProvider = new BetProvider();
            CardDistributionProvider = new CardDistributionProvider();
            CardCheckProvider = new CardCheckProvider();
        }
        

        public string PlayerNameValidation(string name)
        {
            string result = "";

            if (PlayerRepository.SelectByName(name) != null)
            {
                result = Message.NameAlreadyExist;
            }

            if (String.IsNullOrEmpty(name))
            {
                result = Message.IsNullOrEmpty;
            }

            return result;
        }
        
        public void DeletePlayer(int playerId)
        {
            List<GamePlayer> gamePlayers = GamePlayerRepository.GetAll()
                .Where(m => m.PlayerId == playerId)
                .ToList();

            foreach(GamePlayer gamePlayer in gamePlayers)
            {
                DeleteGame(gamePlayer.GameId);
            }
        }


        public GameViewModel CreateGame(string name, int amountOfBots)
        {
            GameViewModel gameViewModel = new GameViewModel();

            Game game = new Game();
            Game lastGame = GameRepository.GetLastObj();
            game.Id = 1;
            if (lastGame != null)
            {
                game.Id = lastGame.Id + 1;
            }
            GameRepository.Create(game);
            gameViewModel.Game = game;

            gameViewModel.Players = new List<PlayerViewModel>();
            List<Player> players = CreatePlayerList(name, amountOfBots);

            foreach(Player player in players)
            {
                PlayerViewModel playerViewModel = CreatePlayerViewModel(player, game.Id);
                gameViewModel.Players.Add(playerViewModel);
            }

            return gameViewModel;
        }

        public void UpdateGameStage(Game game)
        {
            GameRepository.Update(game);
        }

        public void UpdatePlayerCards(GamePlayer gamePlayer, List<int> cardIds)
        {
            GamePlayerRepository.Update(gamePlayer);

            PlayerCardRepository.DeleteByGamePlayerId(gamePlayer.Id);
            CreatePlayerCards(gamePlayer.Id, cardIds);
        }
    
        public void DeleteGame(int gameId)
        {
            List<GamePlayer> gamePlayers = GamePlayerRepository.GetAll()
                .Where(m => m.GameId == gameId)
                .ToList();

            foreach(GamePlayer gamePlayer in gamePlayers)
            {
                PlayerCardRepository.DeleteByGamePlayerId(gamePlayer.Id);
                GamePlayerRepository.Delete(gamePlayer.Id);
            }

            GameRepository.Delete(gameId);
        }

        

        private Player CreatePlayer(string name)
        {
            Player player = new Player();

            Player lastPlayer = PlayerRepository.GetLastObj();
            player.Id = 1;
            if (lastPlayer != null)
            {
                player.Id = lastPlayer.Id + 1;
            }
            player.Name = name;
            player.IsHuman = true;

            PlayerRepository.Create(player);

            return player;
        }

        private List<Player> CreatePlayerList(string name, int amountOfBots)
        {
            List<Player> players = new List<Player>();

            Player dealer = PlayerRepository.GetDealer();
            players.Add(dealer);
            List<Player> bots = PlayerRepository.GetBots(amountOfBots);
            players.AddRange(bots);

            Player human = PlayerRepository.SelectByName(name);
            if (human == null)
            {
                human = CreatePlayer(name);
            }
            players.Add(human);

            return players;
        }

        private PlayerViewModel CreatePlayerViewModel(Player player, int gameId)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();

            playerViewModel.Player = player;
            playerViewModel.GameScore = CreateGamePlayer(gameId, player.Id);
            playerViewModel.Cards = new List<Card>();

            return playerViewModel;
        }

        private GamePlayer CreateGamePlayer(int gameId, int playerId)
        {
            GamePlayer gamePlayer = new GamePlayer();

            GamePlayer lastGamePlayer = GamePlayerRepository.GetLastObj();
            gamePlayer.Id = 1;
            if (lastGamePlayer != null)
            {
                gamePlayer.Id = lastGamePlayer.Id + 1;
            }
            gamePlayer.PlayerId = playerId;
            gamePlayer.GameId = gameId;
            gamePlayer.Score = DefaultPlayerScore;
            GamePlayerRepository.Create(gamePlayer);

            return gamePlayer;
        }

        private void CreatePlayerCards(int gamePlayerId, List<int> cardIds)
        {
            foreach(int cardId in cardIds)
            {
                PlayerCard playerCard = new PlayerCard();
                playerCard.CardId = cardId;
                playerCard.GamePlayerId = gamePlayerId;

                PlayerCardRepository.Create(playerCard);
            }
        }

        


        private void BetPayment(PlayerViewModel player, PlayerViewModel dealer, float betCoef)
        {
            int pay = (int)(player.GameScore.Bet * betCoef);

            player.GameScore.Score += player.GameScore.Bet + pay;
            player.GameScore.Bet = Value.BetNull;
            GamePlayerRepository.Update(player.GameScore);

            dealer.GameScore.Score -= pay;
            GamePlayerRepository.Update(dealer.GameScore);
        }
        
        public void BetCreations(List<PlayerViewModel> players, int bet)
        {
            foreach (PlayerViewModel player in players)
            {
                if (player.Player.IsHuman)
                {
                    player.GameScore.Bet = bet;
                }

                if (!player.Player.IsDealer && !player.Player.IsHuman)
                {
                    player.GameScore.Bet = BetProvider.BetGenerate(player.GameScore.Score);
                }

                if (!player.Player.IsDealer)
                {
                    player.GameScore.Score = player.GameScore.Score - bet;
                    GamePlayerRepository.Update(player.GameScore);
                }
            }
        }

        public void RoundBetPayments(List<PlayerViewModel> players, int oneToOnePayKey = 0)
        {
            PlayerViewModel human = players.Where(m => m.Player.IsHuman).FirstOrDefault();
            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).FirstOrDefault();


            if (oneToOnePayKey == 1)
            {
                BetPayment(human, dealer, Value.BetWinCoefficient);
                human.RoundResult = RoundResult.Continue;
            }

            foreach (PlayerViewModel player in players)
            {
                if (player.BetCoefficient != Value.BetDefaultCoefficient)
                {
                    BetPayment(player, dealer, player.BetCoefficient);
                }
            }
        }
        


        public void FirstCardsDistribution(List<PlayerViewModel> players, List<Card> deck)
        {
            CardDistributionProvider.ShuffleDeck(deck);

            foreach (PlayerViewModel player in players)
            {
                AddingCardToPlayer(player, deck);
                AddingCardToPlayer(player, deck);
            }
        }

        public bool OneMoreCardToHuman(PlayerViewModel player, List<Card> deck = null, int takeCardKey = 0)
        {
            bool canTakeOneMoreCard = true;

            if (takeCardKey == 1)
            {
                AddingCardToPlayer(player, deck);
            }

            if (player.GameScore.RoundScore >= Value.CardBjScore)
            {
                canTakeOneMoreCard = false;
            }

            return canTakeOneMoreCard;
        }

        private void AddingCardToPlayer(PlayerViewModel player, List<Card> deck)
        {
            Card card;
            card = deck.First();
            deck.Remove(card);

            player.Cards.Add(card);
            CardScoreCount(player);

            List<int> cardIds = player.Cards.ConvertAll(CardDistributionProvider.CardToIntConverter);
            UpdatePlayerCards(player.GameScore, cardIds);
        }

        private void CardScoreCount(PlayerViewModel player)
        {
            int count = player.Cards.Sum(m => (int)m.CardName);
            int AceCount = player.Cards
                .Where(m => m.CardName == CardName.Ace)
                .Count();

            for (; AceCount > 0 && count > 21;)
            {
                AceCount--;
                count -= (int)CardName.Ten;
            }

            player.GameScore.RoundScore = count;
        }




        public bool FirstCardCheck(List<PlayerViewModel> players)
        {
            bool humanBjAndDealerBjDanger = false;

            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).First();
            bool dealerBjDanger = CardCheckProvider.DealerBjDanger((int)dealer.Cards[0].CardName);

            foreach (PlayerViewModel player in players)
            {
                if (!player.Player.IsDealer)
                {
                    RoundResult roundResult = CardCheckProvider.RoundFirstPhaseResult(player.GameScore.RoundScore, player.Cards.Count, dealerBjDanger);
                }
            }


            PlayerViewModel human = players.Where(m => m.Player.IsHuman).First();
            if (human.RoundResult == RoundResult.IsOneToOne)
            {
                human.RoundResult = RoundResult.Continue;
                humanBjAndDealerBjDanger = true;
            }

            return humanBjAndDealerBjDanger;
        }
    }
}
