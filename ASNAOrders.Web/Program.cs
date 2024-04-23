using System.Runtime.Serialization.Formatters.Binary;
using ASNAOrders.Web.ConfigServiceExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Xml.Serialization;
using System;
using System.IO;

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
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            if (new FileInfo(filename).Length == 0)
            {
                using FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                XmlSerializerFactory serializer = new XmlSerializerFactory();
                serializer.CreateSerializer(typeof(Config)).Serialize(file, new Config());
            }

            using FileStream dfile = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StaticConfig.Load((Config)(new XmlSerializerFactory().CreateSerializer(typeof(Config)).Deserialize(dfile)));

            CreateHostBuilder(args).Build().Run();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tokenHashes.txt");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
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
