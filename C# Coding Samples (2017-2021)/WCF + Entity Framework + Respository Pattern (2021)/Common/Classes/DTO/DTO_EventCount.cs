using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMOCore.Common.DTO
{
    [DataContract(Name = "EventCount")]
    public class DTO_EventCount
    {
        public DTO_EventCount(string name, int count) { Name = name; Count = count; }

        [DataMember(Name = "EventName")]
        public string Name;
        [DataMember(Name = "Count")]
        public int Count;
    }
}
