using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOCore.Client.Models
{
    public class Client_EventCount
    {
        public Client_EventCount(string name, int count) { Name = name; Count = count; }

        public string Name { get; set; }
        public int Count { get; set; }
    }

}
