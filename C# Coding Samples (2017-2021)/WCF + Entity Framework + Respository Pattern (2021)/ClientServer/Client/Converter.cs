using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Client.Models;
using MMOCore.Common.DTO;

namespace MMOCore.Client
{
    public static class Converter
    {
        public static Client_Event EventFromDTO (DTO_Event ev)
        {
            return new Client_Event(ev.Name, ev.SessionId);
        }

        public static Client_Session SessionFromDTO(DTO_Session session)
        {
            return new Client_Session(session.Id);
        }

        public static IEnumerable<Client_Event> EventsFromDTO (IEnumerable<DTO_Event> evs)
        {
            List<Client_Event> events = new List<Client_Event>();
            foreach (DTO_Event ev in evs)
            {
                events.Add(EventFromDTO(ev));
            }
            return events.AsEnumerable();
        }

        public static IEnumerable<Client_EventCount> EventCountFromDTO(IEnumerable<DTO_EventCount> evs)
        {
            List<Client_EventCount> eventcounts = new List<Client_EventCount>();
            foreach (DTO_EventCount ev in evs)
            {
                eventcounts.Add(new Client_EventCount(ev.Name, ev.Count));
            }
            return eventcounts.AsEnumerable();
        }
    }
}
