using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using MMOCore.Repository;

namespace MMOCore.EFRepository
{
    public class SessionScope : ISessionScope
    {
        //lazy objects
        private Lazy<GameContext> _ctx;
        private Lazy<GameEvent> _eventRepo;
        private Lazy<GameSession> _sessionRepo;

        public GameContext Context => _ctx.Value;
        public GameEvent Events => _eventRepo.Value;
        public GameSession Sessions => _sessionRepo.Value;

        private bool disposed;

        //dependency injection
        public SessionScope(GameContext ctx, GameEvent gameEvent, GameSession gameSesssion) 
        {
            _ctx = new Lazy<GameContext>(() => ctx, false);
            _eventRepo = new Lazy<GameEvent>(() => gameEvent, false);
            _sessionRepo = new Lazy<GameSession>(() => gameSesssion, false);
        }
        public void Save()
        {
            Context.SaveChanges();
        }

        //Disposing of the Context Class while disposing this
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if(disposing)
            {
                if (Context != null) { Context.Dispose(); }
                disposed = true;
            }
        }
    }
}
