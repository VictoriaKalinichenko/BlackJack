﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IGameRepository : IDisposable
    {
        IEnumerable<Game> GetAll();

        Game Get(int Id);

        void Create(Game obj);

        void Update(Game obj);

        void Delete(int Id);
    }
}
