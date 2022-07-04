using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity;

using MMOCore.Repository;
using MMOCore.Repository.Models;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace MMOCore.EFRepository
{
    public class GameEvent : EFRepository<POCO_Event>, IGameEvent
    {
        private GameContext _context;
        public GameEvent(GameContext context) : base(context) { _context = context; }

        public class NextEventResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Nullable<int> NextEvent { get; set; }
        }

        public IEnumerable<POCO_Event> GetNextGameEvents(string v)
        {
            List<POCO_Event> ev = new List<POCO_Event>();
            List<NextEventResult> results = _context.Database.SqlQuery<NextEventResult>("GetNextEvents @ev", new SqlParameter("@ev", v)).ToList();
            
            foreach (NextEventResult r in results)
            {
                if (r.NextEvent != null)
                {
                    ev.Add(Get(r.NextEvent.Value));
                }
            }

            Console.WriteLine("Result Count: {0}", ev.Count);

            return ev;
        }
        public class LastEventResult
        {
            public int SessionId { get; set; }
            public int EventId { get; set; }
        }

        public IEnumerable<POCO_Event> GetLastGameEvents()
        {
            List<POCO_Event> ev = new List<POCO_Event>();
            List<LastEventResult> results = _context.Database.SqlQuery<LastEventResult>("GetLastEvents").ToList();

            foreach (LastEventResult r in results)
            {
                ev.Add(Get(r.EventId));
            }

            return ev;
        }
    }
}
