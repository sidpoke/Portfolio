using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Common;
using MMOCore.Common.DTO;

namespace MMOCore.Client
{
    public class ClientService : IGameService
    {
        private static Uri baseAddress = new Uri("net.tcp://localhost:8009/MMOGameDB");
        private EndpointAddress address = new EndpointAddress(baseAddress);
        private NetTcpBinding binding = new NetTcpBinding();
        private ChannelFactory<IGameService> factory;

        public ClientService()
        {
            factory = new ChannelFactory<IGameService>(binding, address);
        }

        public DTO_Event CreateEvent(string name, int gameSession)
        {
            IGameService svc = factory.CreateChannel();
            DTO_Event ev = svc.CreateEvent(name, gameSession);
            (svc as IChannel).Close();
            return ev;
        }

        public DTO_Session CreateGameSession()
        {
            IGameService svc = factory.CreateChannel();
            DTO_Session session = svc.CreateGameSession();
            (svc as IChannel).Close();
            return session;
        }

        public IEnumerable<DTO_EventCount> GetAllEventsOrderedByCountDesc()
        {
            IGameService svc = factory.CreateChannel();
            IEnumerable<DTO_EventCount> evs = svc.GetAllEventsOrderedByCountDesc();
            (svc as IChannel).Close();
            return evs;
        }
        public IEnumerable<DTO_EventCount> GetNextEventsOrderedByCountDesc(string v)
        {
            IGameService svc = factory.CreateChannel();
            IEnumerable<DTO_EventCount> evs = svc.GetNextEventsOrderedByCountDesc(v);
            (svc as IChannel).Close();
            return evs;
        }

        public IEnumerable<DTO_EventCount> GetLastEventsOrderedByCountDesc()
        {
            IGameService svc = factory.CreateChannel();
            IEnumerable<DTO_EventCount> evs = svc.GetLastEventsOrderedByCountDesc();
            (svc as IChannel).Close();
            return evs;
        }
    }
}