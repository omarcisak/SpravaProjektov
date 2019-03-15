using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF_Wpf
{
    public class ServiceAgent
    {
        public ProjectService.SPF_WSSoapClient soapClient { get; set; }

        public ServiceAgent()
        {
            this.soapClient = new ProjectService.SPF_WSSoapClient();
            soapClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(Properties.Settings.Default.CS_WS);

        }
    }
}
