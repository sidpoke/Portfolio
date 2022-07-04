using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOCore.Client.Models
{
    public class Client_Session
    {
        public Client_Session(int id) { Id = id; }
        public int Id { get; set; }
    }
}
