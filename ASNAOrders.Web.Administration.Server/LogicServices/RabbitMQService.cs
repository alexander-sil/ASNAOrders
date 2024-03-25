using RabbitMQ.Client;

namespace ASNAOrders.Web.Administration.Server.LogicServices
{
    public static class RabbitMQService
    {
        public static bool IssueConfigurationOptions(IssuibleConfig config)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672/asna-orders");

            IConnection conn = factory.CreateConnection();
        }
    }
}
