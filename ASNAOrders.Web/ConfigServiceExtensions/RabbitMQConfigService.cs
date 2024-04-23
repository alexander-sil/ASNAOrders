using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using Serilog;

namespace ASNAOrders.Web.ConfigServiceExtensions
{
    /// <summary>
    /// Service to listen for RabbitMQ Virtual Host messages from the administration interface.
    /// </summary>
    public class RabbitMQConfigService
    {
        /// <summary>
        /// Initializes the RabbitMQ listener service and sets the event handler to update the config file.
        /// </summary>
        public void Initialize()
        {
            var factory = new ConnectionFactory 
            { 
                HostName = StaticConfig.MQHostname,
                Port = StaticConfig.MQPort,
                VirtualHost = StaticConfig.MQVHost 
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "default",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Log.Information($"Waiting for RabbitMQ messages on factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += OnReceived;

            channel.BasicConsume(queue: "default",
                                 autoAck: true,
                                 consumer: consumer);
        }

        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            JToken root = JToken.Parse(Encoding.UTF8.GetString(e.Body.ToArray()));


        }
    }
}
