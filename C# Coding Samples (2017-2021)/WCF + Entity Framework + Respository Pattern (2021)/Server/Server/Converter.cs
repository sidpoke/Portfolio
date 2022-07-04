using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Common.DTO;
using MMOCore.Repository.Models;

namespace MMOCore.Server
{
    public static class Converter
    {
        public static DTO_Event EventToDTO (POCO_Event ev)
        {
            return new DTO_Event(ev.Name, ev.SessionId);
        }

        public static DTO_Session SessionToDTO(POCO_Session session)
        {
            return new DTO_Session(session.Id);
        }

        public static IEnumerable<DTO_Event> EventsToDTO (IEnumerable<POCO_Event> evs)
        {
            List<DTO_Event> events = new List<DTO_Event>();
            foreach(POCO_Event ev in evs)
            {
                events.Add(new DTO_Event(ev.Name, ev.SessionId));
            }
            return events.AsEnumerable();
        }

        public static IEnumerable<DTO_EventCount> EventsToDTOCount(IEnumerable<POCO_Event> evs)
        {
            List<DTO_EventCount> events = new List<DTO_EventCount>();

            //linq ref from https://stackoverflow.com/questions/454601/how-to-count-duplicates-in-list-with-linq

            if (!evs.Any())
                return events.AsEnumerable();

            var q = evs.GroupBy(x => x != null ? x.Name : null)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

            foreach (var x in q)
            {
                if (x.Value == null) continue;
                events.Add(new DTO_EventCount(x.Value,  (x != null) ? x.Count : 0));
            }

            return events.AsEnumerable();
        }
    }
}
