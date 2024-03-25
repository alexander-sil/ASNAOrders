using ASNAOrders.Web.Administration.Server.AbstrModels;
using RabbitMQ.Client;

namespace ASNAOrders.Web.Administration.Server.LogicServices
{
    /// <summary>
    /// A service to control RabbitMQ node subsystems from within CodeBehind classes.
    /// </summary>
    public static class RabbitMQService
    {
        /// <summary>
        /// Dispatches given IssuibleConfig to RabbitMQ, to be consumed by the order server.
        /// </summary>
        /// <param name="config">Configuration to be passed.</param>
        /// <returns>The action result of the operation (success/failure).</returns>
        public static bool IssueConfigurationOptions(IssuibleConfig config)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri($"amqp://{"guest"}:guest@localhost:5672/asna-orders");

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
