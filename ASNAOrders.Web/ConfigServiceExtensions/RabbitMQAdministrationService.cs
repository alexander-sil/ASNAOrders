using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Serilog;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System;
using ASNAOrders.Web.Data.Orders;
using Newtonsoft.Json;

namespace ASNAOrders.Web.ConfigServiceExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMQAdministrationService
    {
        /// <summary>
        /// 
        /// </summary>
        public class RabbitMQConfigService
        {
            /// <summary>
            /// Initializes the RabbitMQ listener service and sets the event handler to update the config file.
            /// </summary>
            public static void Initialize()
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

                channel.QueueDeclare(queue: Properties.Resources.AdminInQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Log.Information($"Waiting for RabbitMQ ADMIN-IN messages @ factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += OnReceived;

                channel.BasicConsume(queue: Properties.Resources.AdminInQueue,
                                     autoAck: true,
                                     consumer: consumer);
            }

            private static void OnReceived(object sender, BasicDeliverEventArgs e)
            {
                if (Encoding.UTF8.GetString(e.Body.ToArray()) == "NEED CONFIG")
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = StaticConfig.MQHostname,
                        Port = StaticConfig.MQPort,
                        VirtualHost = StaticConfig.MQVHost
                    };

                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: Properties.Resources.AdminOutQueue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                    arguments: null);

                    string message = JsonConvert.SerializeObject(new Config()
                    {
                        ClientId = StaticConfig.ClientId,
                        ClientIdSetToAuto = StaticConfig.ClientIdSetToAuto,
                        ClientSecret = StaticConfig.ClientSecret,
                        ClientSecretSetToAuto = StaticConfig.ClientSecretSetToAuto,
                        ClientSecretFilename = StaticConfig.ClientSecretFilename,
                        ClientSecretFilenameSetToAuto = StaticConfig.ClientSecretFilenameSetToAuto,

                        IssuerSigningKey = StaticConfig.IssuerSigningKey,
                        IssuerSigningKeySetToAuto = StaticConfig.IssuerSigningKeySetToAuto,
                        SigningKeyFile = StaticConfig.SigningKeyFile,
                        SigningKeyFileSetToAuto = StaticConfig.SigningKeyFileSetToAuto,

                        MssqlServerUsername = StaticConfig.MssqlServerUsername,
                        MssqlServerPassword = StaticConfig.MssqlServerPassword,
                        MssqlServerHost = StaticConfig.MssqlServerHost,
                        MssqlServerPort = StaticConfig.MssqlServerPort,

                        ClientSecretTransmissionMethod = StaticConfig.ClientSecretTransmissionMethod,

                        DatabaseType = StaticConfig.DatabaseType,
                        ConnectionString = StaticConfig.ConnectionString,
                        InitialCatalog = StaticConfig.InitialCatalog,
                        SqliteDbCacheFilename = StaticConfig.SqliteDbCacheFilename,

                        ErrorLogPrefix = StaticConfig.ErrorLogPrefix,

                        Sink = StaticConfig.Sink,

                        MailHost = StaticConfig.MailHost,
                        MailPort = StaticConfig.MailPort,

                        MailSSLOptions = StaticConfig.MailSSLOptions,
                        MailPassword = StaticConfig.MailPassword,

                        MailTo = StaticConfig.MailTo,

                        MQVHost = StaticConfig.MQVHost,
                        MQHostname = StaticConfig.MQHostname,
                        MQPort = StaticConfig.MQPort,

                        XMLStockPath = StaticConfig.XMLStockPath
                    });

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: string.Empty,
                                     routingKey: Properties.Resources.AdminOutQueue,
                                     basicProperties: null,
                                     body: body);

                    Log.Information($"Administration configuration notification issued to agent @ factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");
                }
            }       
        }
    }
}
