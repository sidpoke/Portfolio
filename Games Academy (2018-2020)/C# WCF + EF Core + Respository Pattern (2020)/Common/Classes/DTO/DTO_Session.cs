using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMOCore.Common.DTO
{
    [DataContract(Name = "Session")]
    public class DTO_Session
    {
        public DTO_Session(int id) { Id = id; }
        [DataMember(Name = "ID")]
        public int Id { get; set; }
    }
}
