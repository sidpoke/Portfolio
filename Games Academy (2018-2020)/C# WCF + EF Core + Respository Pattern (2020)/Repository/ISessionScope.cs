using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

using MMOCore.Repository.Models;

namespace MMOCore.Repository
{
    public interface ISessionScope : IDisposable
    {
        void Save();
    }
}
