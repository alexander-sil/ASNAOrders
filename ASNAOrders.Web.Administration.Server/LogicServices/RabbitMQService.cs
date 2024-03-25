using ASNAOrders.Web.Administration.Server.AbstrModels;
using RabbitMQ.Client;

namespace ASNAOrders.Web.Administration.Server.LogicServices
{
    public static class RabbitMQService
    {
        public static bool IssueConfigurationOptions(IssuibleConfig config)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri("amqp://guest:guest@localhost:5672/asna-orders");

                IConnection conn = factory.CreateConnection();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
