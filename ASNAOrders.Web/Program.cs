using System.Runtime.Serialization.Formatters.Binary;
using ASNAOrders.Web.ConfigServiceExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Xml.Serialization;
using System;
using System.IO;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Extensions.Hosting;
using System.Diagnostics;

namespace ASNAOrders.Web
{
#nullable enable
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public static string? ConfigFilename { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public static string? XMLPath { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public static string? ImagePath { get; set; } = "";

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {


            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Resources.ConfigXmlPath);
            ConfigFilename = filename;

            if (!File.Exists(filename))
            {
                File.Create(filename).Dispose();
            }

            if (new FileInfo(filename).Length == 0)
            {
                FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
                    MQVHost = Properties.Resources.ConfigMQVHost,
                    XMLStockPath = Properties.Resources.ConfigXMLStockPath
                });
                file.Dispose();
            }

            FileStream dfile = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StaticConfig.Load((Config)new XmlSerializerFactory().CreateSerializer(typeof(Config)).Deserialize(dfile));

            dfile.Dispose();


            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath);
            string path1 = Path.Combine(path, Properties.Resources.ASNAImagesPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                string message = $"XML directory created at {path}. Please point the FTP server to this exact folder.";

                Log.Information(message);

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(path1);
                    Log.Information($"Images directory created at {path1}");
                }
            }

            XMLPath = path;
            ImagePath = path1;

            RabbitMQConfigService.Initialize();
            RabbitMQAdministrationService.Initialize();

            CreateHostBuilder(args).Build().Run();

            RabbitMQAdministrationService.Channel.Dispose();
            RabbitMQAdministrationService.Connection.Dispose();


            RabbitMQConfigService.Channel.Dispose();
            RabbitMQConfigService.Connection.Dispose();

            Log.CloseAndFlush();
        }

        /// <summary>
        /// Create the host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(dispose: true)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                       .UseUrls("http://0.0.0.0:8080/");
                });
    }
}
