using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using MMOCore.Common;

namespace MMOCore.Server
{
    public class Host
    {
        SessionService _service;
        private Uri _baseAddress;

        public Host(SessionService service,  string uri) 
        { 
            _baseAddress = new Uri(uri);
            _service = service;
        }

        public void StartServiceHost()
        {
            using (ServiceHost host = new ServiceHost(_service))
            {
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IGameService), binding, _baseAddress);
                host.Open();

                Console.WriteLine("Server Started");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
