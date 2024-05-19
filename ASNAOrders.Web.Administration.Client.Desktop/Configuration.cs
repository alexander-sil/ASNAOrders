using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Web.Administration.Client.Desktop
{
    internal class Configuration
    {
        public static string RabbitMQHostname { get; set; } = "localhost";

        public static ushort RabbitMQPort { get; set; } = 5672;

        public static string RabbitMQVhost { get; set; } = "asna-orders";

        public static string AccessToken { get; set; } = "";

        public static OpenApi.Config? CurrentConfiguration { get; set; }

        public static OpenApi.IssuibleConfig ConfigToBeIssued { get; set; } = new OpenApi.IssuibleConfig();
        
        public static OpenApi.InterfaceClientFE? ClientFE { get; set; }
    }
}
