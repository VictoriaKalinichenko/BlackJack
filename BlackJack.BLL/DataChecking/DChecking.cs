 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;

namespace BlackJack.BLL.DataChecking
{
    public class DChecking : IDataChecking
    {
        IUnitOfWork db;

        public DChecking(IUnitOfWork repository)
        {
            db = repository;
        }

        public bool PlayerNameCheck(string name)
        {
            bool correct = true;

            if (db.Players.SelectByName(name) != null)
            {
                correct = false;
            }

            return correct;
        }

        public bool BetCheck(Player player, int bet)
        {
            bool correct = true;

            if (player.Score < bet)
            {
                correct = false;
            }

            return correct;
        }
    }
}
