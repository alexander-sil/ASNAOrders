using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Web.Administration.Client.Mobile
{
    internal static class Configuration
    {
        public static string RabbitMQHostname { get; set; } = "rabbitmq.local";

        public static ushort RabbitMQPort { get; set; } = 5672;

        public static string RabbitMQVhost { get; set; } = "asna-orders";

        public static string ServerUsername { get; set; } = "admin";

        public static string ServerPassword { get; set; } = "admin";

        public static string ServerHostname { get; set; } = "administration.asna-orders";

        public static Config? CurrentConfiguration { get; set; }

    }
}
