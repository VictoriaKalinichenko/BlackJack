using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Context;
using BlackJack.DAL.Repositories;
using BlackJack.Entity;
using System.Data.Entity;

namespace BlackJack.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataBaseContext db;
        
        private PlayerRepository playerRepository;
        private GameRepository gameRepository;
        private GamePlayerRepository gamePlayerRepository;

        public EFUnitOfWork()
        {
            db = new DataBaseContext();
        }


        public IPlayerRepository Players
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new PlayerRepository(db);
                return playerRepository;
            }
        }

        public IGameRepository Games
        {
            get
            {
                if (gameRepository == null)
                {
                    gameRepository = new GameRepository(db);
                }
                return gameRepository;
            }
        }
        
        public IGamePlayerRepository GamePlayers
        {
            get
            {
                if (gamePlayerRepository == null)
                {
                    gamePlayerRepository = new GamePlayerRepository(db);
                }
                return gamePlayerRepository;
            }
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            playerRepository.Dispose();
            gameRepository.Dispose();
            gamePlayerRepository.Dispose();
            
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
