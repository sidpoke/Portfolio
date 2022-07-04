using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Repository.Models;

namespace MMOCore.Repository
{
    //Interface for a game event method structure
    public interface IGameEvent : IRepository<POCO_Event>
    {
         IEnumerable<POCO_Event> GetNextGameEvents(string v);
         IEnumerable<POCO_Event> GetLastGameEvents();
    }
}