using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using Serilog;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace ASNAOrders.Web.ConfigServiceExtensions
{
    /// <summary>
    /// Service to listen for RabbitMQ Virtual Host messages from the administration interface.
    /// </summary>
    public class RabbitMQConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConnection Connection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static IModel Channel { get; private set; }

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

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();

            Channel.QueueDeclare(queue: Properties.Resources.AdministrationQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Log.Information($"Waiting for RabbitMQ CONFIG messages @ factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += OnReceived;

            Channel.BasicConsume(queue: Properties.Resources.AdministrationQueue,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private static void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            Log.Information($"Config accepted at {DateTime.Now}");
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


                DatabaseType = (string)root["databaseType"],
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

            config.ConnectionString = config.DatabaseType == "sqlite" ? $"Data Source={config.SqliteDbCacheFilename};Cache=Shared" : $"Data Source={config.MssqlServerHost},{config.MssqlServerPort};Initial Catalog={config.InitialCatalog};User ID={config.MssqlServerUsername};Password={config.MssqlServerPassword};TrustServerCertificate=True;App=ASNAOrders;MultipleActiveResultSets=True";
            config.ErrorLogPrefix = $"hs_log_id{Task.Run(() => { Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL"); return DateTime.Now.ToShortDateString(); }).Result}_";

            string filename = Program.ConfigFilename;

            Log.Information($"Start configuration reset procedure at {DateTime.Now}");

            try
            {
                File.Delete(filename);
                Log.Information($"Config file reset at {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Log.Information($"{DateTime.Now} Config file at {filename} not found. {ex.Message} at {ex.StackTrace}");
            }

            FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializerFactory serializer = new XmlSerializerFactory();

            serializer.CreateSerializer(typeof(Config)).Serialize(file, config);
            StaticConfig.Load((Config)serializer.CreateSerializer(typeof(Config)).Deserialize(file));

            file.Dispose();
        }
    }
}
