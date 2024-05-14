using System.Runtime.Serialization.Formatters.Binary;
using ASNAOrders.Web.ConfigServiceExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Xml.Serialization;
using System;
using System.IO;
using Serilog;

namespace ASNAOrders.Web
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Resources.ConfigXmlPath);

            if (!File.Exists(filename))
            {
                File.Create(filename).Dispose();
            }

            if (new FileInfo(filename).Length == 0)
            {
                using FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                XmlSerializerFactory serializer = new XmlSerializerFactory();
                serializer.CreateSerializer(typeof(Config)).Serialize(file, 
                new Config() 
                {
                    DatabaseType = Properties.Resources.ConfigDatabaseType,
                    InitialCatalog = Properties.Resources.ConfigInitialCatalog,
                    MssqlServerHost = Properties.Resources.ConfigMssqlServerHost,
                    MssqlServerPort = ushort.Parse(Properties.Resources.ConfigMssqlServerPort),
                    MssqlServerUsername = Properties.Resources.ConfigMssqlUsername,
                    MssqlServerPassword = Properties.Resources.ConfigMssqlDefaultPass,
                    MailHost = Properties.Resources.ConfigMailHost,
                    MailPort = ushort.Parse(Properties.Resources.ConfigMailPort),
                    MailTo = Properties.Resources.ConfigMailTo,
                    MailPassword = Properties.Resources.ConfigMailPassword,
                    MailSSLOptions = Properties.Resources.ConfigMailSSLOptions,
                    Sink = Properties.Resources.ConfigSerilogSink,
                    MQHostname = Properties.Resources.ConfigMQHostname,
                    MQPort = ushort.Parse(Properties.Resources.ConfigMQPort),
                    MQVHost = Properties.Resources.ConfigMQVHost
                });
            }

            using FileStream dfile = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StaticConfig.Load((Config)new XmlSerializerFactory().CreateSerializer(typeof(Config)).Deserialize(dfile));

            RabbitMQConfigService.Initialize();

            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlushAsync();
        }

        /// <summary>
        /// Create the host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseUrls("http://0.0.0.0:8080/");
                });
    }
}
