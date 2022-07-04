using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Repository.Models;

namespace MMOCore.Repository
{
    //Interface for a game session method structure
    public interface IGameSession : IRepository<POCO_Session>
    {
        //Everything needed is already in IRepository
    }
}