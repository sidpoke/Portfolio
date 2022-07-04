using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using MMOCore.Common;
using MMOCore.Common.DTO;

namespace MMOCore.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SessionService : IGameService
    {
        public GameController _ctr { get; set; }
        public SessionService() { }

        public DTO_Event CreateEvent(string name, int gameSession)
        {
            return Converter.EventToDTO(_ctr.CreateGameEvent(name, gameSession));
        }

        public DTO_Session CreateGameSession()
        {
            return Converter.SessionToDTO(_ctr.CreateGameSession());
        }

        public IEnumerable<DTO_EventCount> GetAllEventsOrderedByCountDesc()
        {
            Console.WriteLine("Get All Events Ordered By Count Descending request");
            return Converter.EventsToDTOCount(_ctr.GetGameEvents());
        }
        public IEnumerable<DTO_EventCount> GetNextEventsOrderedByCountDesc(string v)
        {
            return Converter.EventsToDTOCount(_ctr.GetNextGameEvents(v));
        }

        public IEnumerable<DTO_EventCount> GetLastEventsOrderedByCountDesc()
        {
            return Converter.EventsToDTOCount(_ctr.GetLastGameEvents());
        }
    }
}
