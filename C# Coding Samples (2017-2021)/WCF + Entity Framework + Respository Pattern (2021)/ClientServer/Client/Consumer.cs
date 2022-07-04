using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Client.Models;

namespace MMOCore.Client
{
    public class Consumer : IDisposable
    {
        private ClientService _service;

        public Consumer()
        {
            _service = new ClientService();
        }

        public Client_Session CreateGameSession()
        {
            return Converter.SessionFromDTO(_service.CreateGameSession());
        }

        public Client_Event CreateEvent(string name, Client_Session gameSession)
        {
            return Converter.EventFromDTO(_service.CreateEvent(name, gameSession.Id));
        }

        public IEnumerable<Client_EventCount> GetAllEventsOrderedByCountDesc()
        {
            return Converter.EventCountFromDTO(_service.GetAllEventsOrderedByCountDesc());
        }

        public IEnumerable<Client_EventCount> GetNextEventsOrderedByCountDesc(string v)
        {
            return Converter.EventCountFromDTO(_service.GetNextEventsOrderedByCountDesc(v));
        }

        public IEnumerable<Client_EventCount> GetLastEventsOrderedByCountDesc()
        {
            return Converter.EventCountFromDTO(_service.GetLastEventsOrderedByCountDesc());
        }

        public void Dispose() {}
    }
}
