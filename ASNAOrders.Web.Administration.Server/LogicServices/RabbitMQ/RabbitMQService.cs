using ASNAOrders.Web.Administration.Server.AbstrModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ASNAOrders.Web.Administration.Server.LogicServices.RabbitMQ
{
    /// <summary>
    /// A service to control RabbitMQ node subsystems from within CodeBehind classes.
    /// </summary>
    public static class RabbitMQService
    {
        public static IConnection? Connection { get; private set; }

        public static IModel? Channel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static Config? ReceivedConfig { get; set; }

        /// <summary>
        /// Dispatches given IssuibleConfig to RabbitMQ, to be consumed by the order server.
        /// </summary>
        /// <param name="config">Configuration to be passed.</param>
        /// <returns>The action result of the operation (success/failure).</returns>
        public static void IssueConfigurationOptions(IssuibleConfig config, string server, ushort port, string vhost)
        {
            var factory = new ConnectionFactory
            {
                HostName = server,
                Port = port,
                VirtualHost = vhost,
                UserName = Properties.Resources.ConfigMQUsername,
                Password = Properties.Resources.ConfigMQPassword
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Properties.Resources.AdministrationQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = System.Text.Json.JsonSerializer.Serialize(config, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: Properties.Resources.AdministrationQueue,
                                 basicProperties: null,
                                 body: body);
        }

        private static void RequestConfig(string server, string port, string vhost)
        {

            var factory = new ConnectionFactory
            {
                HostName = server,
                Port = int.Parse(port),
                VirtualHost = vhost,
                UserName = Properties.Resources.ConfigMQUsername,
                Password = Properties.Resources.ConfigMQPassword
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Properties.Resources.AdminInQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            const string message = "NEED CONFIG";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: Properties.Resources.AdminInQueue,
                                 basicProperties: null,
                                 body: body);
        }


        public static Config GetConfig(string server, string port, string vhost)
        {

            var factory = new ConnectionFactory
            {
                HostName = server,
                Port = int.Parse(port),
                VirtualHost = vhost,
                UserName = Properties.Resources.ConfigMQUsername,
                Password = Properties.Resources.ConfigMQPassword
            };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();

            Channel.QueueDeclare(queue: Properties.Resources.AdminOutQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(Channel);

            consumer.Received += OnReceived;

            Channel.BasicConsume(queue: Properties.Resources.AdminOutQueue,
                                 autoAck: true,
                                 consumer: consumer);

            RequestConfig(server, port, vhost);

            Thread.Sleep(5000);

            Channel.Dispose();
            Connection.Dispose();

            return ReceivedConfig;
        }

        private static void OnReceived(object? sender, BasicDeliverEventArgs e)
        {
            Config? config = JsonConvert.DeserializeObject<Config>(Encoding.UTF8.GetString(e.Body.ToArray()));
            ReceivedConfig = config;
        }
    }
}
