using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Repository;
using MMOCore.Repository.Models;
using MMOCore.EFRepository;
using MMOCore.Common.DTO;

namespace MMOCore.Server
{
    //Controller Class that is used to create sessions and events
    public class GameController
    {
        private SessionScope _scope;
        private int rainbowCounter;

        public ConsoleColor[] colors = {ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta};


        public GameController(SessionScope scope)
        {
            this._scope = scope;
        }

        public POCO_Session CreateGameSession()
        {
            POCO_Session session = new POCO_Session() {};
            _scope.Context.Sessions.Add(session);
            Console.WriteLine("Created Session", session.Id);
            Save();
            return session;
        }

        public POCO_Event CreateGameEvent(string name, int session)
        {
            POCO_Event ev = new POCO_Event() { Name = name, SessionId = session };
            _scope.Context.Events.Add(ev);

            if(rainbowCounter > 1000)
            {
                Console.ForegroundColor = colors[rainbowCounter % colors.Length];
            }

            Console.WriteLine("Created Event '{0}' with SessionID {1}", ev.Name, ev.Session.Id);
            Save();
            rainbowCounter++;
            return ev;
        }

        public IEnumerable<POCO_Session> GetGameSessions()
        {
            Save();
            return _scope.Sessions.GetAll();
        }

        public IEnumerable<POCO_Event> GetGameEvents()
        {
            Save();
            return _scope.Events.GetAll();
        }

        public IEnumerable<POCO_Event> GetNextGameEvents(string v)
        {
            Save();
            return _scope.Events.GetNextGameEvents(v);
        }

        public IEnumerable<POCO_Event> GetLastGameEvents()
        {
            Save();
            return _scope.Events.GetLastGameEvents();
        }

        public void Save()
        {
            _scope.Save();
        }
    }
}
