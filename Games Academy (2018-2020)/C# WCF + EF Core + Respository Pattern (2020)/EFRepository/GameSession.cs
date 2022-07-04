using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Repository;
using MMOCore.Repository.Models;

namespace MMOCore.EFRepository
{
    public class GameSession : EFRepository<POCO_Session>, IGameSession
    {
        public GameSession(GameContext context) : base (context) { }
    }
}
