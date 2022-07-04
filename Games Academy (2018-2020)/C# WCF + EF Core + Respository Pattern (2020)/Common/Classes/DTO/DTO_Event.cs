using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMOCore.Common.DTO
{
    [DataContract(Name = "Event")]
    public class DTO_Event
    {
        public DTO_Event(string name, int session) { Name = name; SessionId = session; }

        [DataMember(Name = "EventName")]
        public string Name;
        [DataMember(Name = "SessionID")]
        public int SessionId;
    }
}
