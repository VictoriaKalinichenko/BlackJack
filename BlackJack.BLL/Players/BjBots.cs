using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Cards;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.Deck;
using BlackJack.BLL.DataChecking;
using BlackJack.BLL.Randomize;

namespace BlackJack.BLL.Players
{
    public class BjBots : IBots
    {
        List<BjPlayer> Bots = new List<BjPlayer>();
        
        IUnitOfWork db;
        IDeck deck;


        public BjBots(IUnitOfWork context, List<Player> _bots, Player dealer, IDeck _deck)
        {
            db = context;
            deck = _deck;

            foreach(Player bot in _bots)
            {
                Bots.Add(new BjPlayer(context, bot, dealer, _deck));
            }
        }



        public void CreateBets()
        {
            IRandomize random = new GameRandomize();
            IDataChecking dataChecking = new DChecking(db);

            for (int i = 0; i < Bots.Count; i++)
            {
                if (Bots[i].IsScoreNull())
                {
                    continue;
                }

                int bet;
                {
                    bet = random.BetGenerate();
                }
                while (dataChecking.BetCheck(Bots[i].GetPlayer(), bet));

                Bots[i].Bet.Create(bet);
            }
        }

        public void FirstCardsAdding()
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                if (Bots[i].IsScoreNull())
                {
                    continue;
                }

                Bots[i].Cards.FirstCardsTaking();
            }
        }

        public void BJChecking(bool DealerBJDanger)
        {
            foreach (BjPlayer bot in Bots)
            {
                if (bot.IsBetNull())
                {
                    continue;
                }

                bool bj = false;

                if (bot.Cards.Equals21() && !DealerBJDanger)
                {
                    bj = true;
                }

                if (bj)
                {
                    bot.Bet.PayBlackJack();
                }
            }
        }

        public void SecondCardsEdding()
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                if (Bots[i].IsBetNull())
                {
                    continue;
                }

                while (Bots[i].GetPlayer().RoundScore < 18)
                {
                    Bots[i].Cards.TakeCard();
                }
            }
        }

        public void SecondCardsChecking()
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                if (Bots[i].IsBetNull())
                {
                    continue;
                }

                if (Bots[i].Cards.MoreThan21() || !Bots[i].Cards.BetterThanDealerScore())
                {
                    Bots[i].Bet.LossingBet();
                }

                if(Bots[i].Cards.EqualsDealerScore())
                {
                    Bots[i].Bet.ReturnBet();
                }

                if (Bots[i].Cards.BetterThanDealerScore())
                {
                    Bots[i].Bet.PayOneToOne();
                }
            }
        }

        public void RemoveCards()
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                if (Bots[i].IsBetNull())
                {
                    continue;
                }

                Bots[i].Cards.RemoveCards();
            }
        }

        public bool IsRoundOver()
        {
            bool result = false;

            for(int i = 0; i < Bots.Count && !result; i++)
            {
                if (Bots[i].IsBetNull())
                {
                    result = true;
                }
            }

            return result;
        }

        public List<Player> GetBots()
        {
            List<Player> bots = new List<Player>();

            foreach (BjPlayer bot in Bots)
            {
                bots.Add(bot.GetPlayer());
            }

            return bots;
        }

    }
}
