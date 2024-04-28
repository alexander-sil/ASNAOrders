using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using Serilog;
using System.IO;
using System.Xml.Serialization;

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
        public static void Initialize()
        {
            var factory = new ConnectionFactory 
            { 
                HostName = StaticConfig.MQHostname,
                Port = StaticConfig.MQPort,
                VirtualHost = StaticConfig.MQVHost 
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Properties.Resources.AdministrationQueue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Log.Information($"Waiting for RabbitMQ CONFIG messages @ factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += OnReceived;

            channel.BasicConsume(queue: Properties.Resources.AdministrationQueue,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private static void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            JToken root = JToken.Parse(Encoding.UTF8.GetString(e.Body.ToArray()));

            Config config = new Config()
            {
                MQVHost = (string)root["mqVhost"],
                MQHostname = (string)root["mqHostname"],
                MQPort = (ushort)root["mqPort"],


                IssuerSigningKeySetToAuto = (bool)root["issuerSigningKeySetToAuto"],
                IssuerSigningKey = (string)root["issuerSigningKey"],

                SigningKeyFileSetToAuto = (bool)root["signingKeyFileSetToAuto"],
                SigningKeyFile = (string)root["signingKeyFile"],

                ClientIdSetToAuto = (bool)root["clientIdSetToAuto"],
                ClientId = (string)root["clientId"],


                ClientSecretFilenameSetToAuto = (bool)root["clientSecretFilenameSetToAuto"],
                ClientSecretFilename = (string)root["clientSecretFilename"],

                ClientSecretSetToAuto = (bool)root["clientSecretSetToAuto"],
                ClientSecret = (string)root["clientSecret"],


                DatabaseType = (string)root["dbType"],
                InitialCatalog = (string)root["initialCatalog"],

                XMLStockPath = (string)root["xmlStockPath"],
                SqliteDbCacheFilename = (string)root["sqliteDbCacheFilename"],

                MssqlServerHost = (string)root["mssqlServerHost"],
                MssqlServerPort = (ushort)root["mssqlServerPort"],

                MssqlServerUsername = (string)root["mssqlServerUsername"],
                MssqlServerPassword = (string)root["mssqlServerPassword"],

                ConnectionString = (string)root["connectionString"],


                Sink = (string)root["sink"],
                ErrorLogPrefix = (string)root["errorLogPrefix"],

                MailPassword = (string)root["mailPassword"],
                MailTo = (string)root["mailTo"],
                MailHost = (string)root["mailHost"],
                MailPort = (ushort)root["mailPort"],
                MailSSLOptions = (string)root["mailSslOptions"],


                ClientSecretTransmissionMethod = (string)root["clientSecretTransmissionMethod"]
            };

            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Resources.ConfigXmlPath);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializerFactory serializer = new XmlSerializerFactory();

            serializer.CreateSerializer(typeof(Config)).Serialize(file, config);
            StaticConfig.Load((Config)serializer.CreateSerializer(typeof(Config)).Deserialize(file));
        }
    }
}
