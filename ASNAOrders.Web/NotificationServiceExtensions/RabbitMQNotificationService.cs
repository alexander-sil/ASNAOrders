using ASNAOrders.Web.ConfigServiceExtensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using Serilog.Core;
using System;
using System.Text;

namespace ASNAOrders.Web.NotificationServiceExtensions
{
    /// <summary>
    /// Service to send RabbitMQ Virtual Host notification messages to the agent.
    /// </summary>
    public class RabbitMQNotificationService
    {
        /// <summary>
        /// Constructor method for ASNAOrders.Web.NotificationServiceExtensions.RabbitMQNotificationService class data type.
        /// </summary>
        public RabbitMQNotificationService() 
        {

        }

        /// <summary>
        /// Initializes the RabbitMQ notification sender service and issues a message to the notification queue.
        /// </summary>
        public void Send(OrderNotification notification)
        {
            var factory = new ConnectionFactory
            {
                HostName = StaticConfig.MQHostname,
                Port = StaticConfig.MQPort,
                VirtualHost = StaticConfig.MQVHost,
                UserName = Properties.Resources.ConfigMQUsername,
                Password = Properties.Resources.ConfigMQPassword
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Properties.Resources.NotifyQueueProperty,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonConvert.SerializeObject(notification);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                             routingKey: Properties.Resources.NotifyQueueProperty,
                             basicProperties: null,
                             body: body);

            Log.Information($"Order notification issued to agent @ factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");
        }
    }
}
