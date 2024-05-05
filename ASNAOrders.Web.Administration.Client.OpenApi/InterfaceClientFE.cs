using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Web.Administration.Client.OpenApi
{
    public class InterfaceClientFE
    {
        public InterfaceClient Client {  get; set; }

        public static InterfaceClientFE? Instance { get; set; }

        public static InterfaceClientFE GetInstance()
        {
            return Instance;
        }

        public InterfaceClientFE(string baseUrl, HttpClient client)
        {
            Instance = this;
            Client = new InterfaceClient(baseUrl, client);
        }
    }
}
