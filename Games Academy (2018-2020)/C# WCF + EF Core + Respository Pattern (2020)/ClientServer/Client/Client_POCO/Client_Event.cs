using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOCore.Client.Models
{
    public class Client_Event
    {
        public Client_Event(string name, int session) { Name = name; SessionId = session; }
        
        public string Name { get; set; }
        public int SessionId { get; set; }
    }
}
