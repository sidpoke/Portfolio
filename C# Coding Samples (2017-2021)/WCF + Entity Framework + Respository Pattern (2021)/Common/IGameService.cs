using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using MMOCore.Common.DTO;

namespace MMOCore.Common
{
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        DTO_Session CreateGameSession();

        [OperationContract]
        DTO_Event CreateEvent(string name, int gameSession);

        [OperationContract]
        IEnumerable<DTO_EventCount> GetAllEventsOrderedByCountDesc();
        
        [OperationContract]
        IEnumerable<DTO_EventCount> GetNextEventsOrderedByCountDesc(string v);
        
        [OperationContract]
        IEnumerable<DTO_EventCount> GetLastEventsOrderedByCountDesc();
    }
}
