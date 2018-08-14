using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;

namespace BlackJack.Configuration
{
    public static class Config
    {
        public static readonly IUnitOfWork db;


        static Config()
        {
            db = new EFUnitOfWork();
        }
    }
}
