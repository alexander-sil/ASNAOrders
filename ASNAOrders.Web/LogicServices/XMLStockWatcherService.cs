using ASNAOrders.Web.ConfigServiceExtensions;
using ASNAOrders.Web.Data;
using ASNAOrders.Web.Data.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Service to watch for stock uploads from agent. XML format is used.
    /// </summary>
    public class XMLStockWatcherService
    {
        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FileSystemWatcher Watcher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ILogger<XMLStockWatcherService> Logger { get; set; }

        /// <summary>
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="contextFactory">Database context to write to. NativeStocks table is used.</param>
        public XMLStockWatcherService(IDbContextFactory<ASNAOrdersDbContext> contextFactory, ILogger<XMLStockWatcherService> logger) 
        {
            Logger = logger;
            using var context = contextFactory.CreateDbContext();
            Context = context;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticConfig.XMLStockPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Logger.LogInformation($"XML directory created at {path}. Please point the FTP server to this exact folder.");
            }

            foreach (FileInfo file in new DirectoryInfo(path).EnumerateFiles())
            {
                DateTime date = DateTime.ParseExact(Regex.Replace(file.Name, @"_.*\.xml", string.Empty), Properties.Resources.DefaultDateFormatString, new CultureInfo("nl-NL"));
                XDocument document = XDocument.Parse(File.ReadAllText(file.FullName));

                foreach (XElement element in document.Elements())
                {
                    if (Context.NativeStocks.Where(f => f.ItemName.Equals(element.Element("item_name").Value)).Count() < 1)
                    {
                        Context.NativeStocks.Add(new NativeStock()
                        {
                            Category = element.Element("category").Value,
                            Composition = element.Element("composition").Value,
                            Qtty = int.Parse(element.Element("qtty").Value),
                            Country = element.Element("country").Value,
                            ItemId = element.Element("item_id").Value,
                            Price = double.Parse(element.Element("price").Value, new CultureInfo("ru-RU")),
                            ItemDesc = element.Element("item_desc").Value,
                            ItemName = element.Element("item_name").Value,
                            PlaceId = element.Element("place_id").Value,
                            Barcode = element.Element("barcode").Value,
                            UploadRecordedDate = date,

                        });
                    }
                }
            }

            Context.SaveChanges();

            Watcher = new FileSystemWatcher(StaticConfig.XMLStockPath);

            Watcher.Created += OnUpload;
        }
        
        /// <summary>
        /// Event handler to process XML stock uploads received from agent service/application in file form.
        /// </summary>
        /// <param name="sender">Generic sender parameter for event handler delegaate.</param>
        /// <param name="e">Event arguments object for </param>
        private void OnUpload(object sender, FileSystemEventArgs e)
        {
            DateTime date = DateTime.ParseExact(Regex.Replace(e.Name, @"_.*\.xml", string.Empty), Properties.Resources.DefaultDateFormatString, new CultureInfo("nl-NL"));
            XDocument document = XDocument.Parse(File.ReadAllText(e.FullPath));

            foreach (XElement element in document.Elements())
            {
                if (Context.NativeStocks.Where(f => f.ItemName.Equals(element.Element("item_name").Value)).Count() < 1)
                {
                    Context.NativeStocks.Add(new NativeStock()
                    {
                        Category = element.Element("category").Value,
                        Composition = element.Element("composition").Value,
                        Qtty = int.Parse(element.Element("qtty").Value),
                        Country = element.Element("country").Value,
                        ItemId = element.Element("item_id").Value,
                        Price = double.Parse(element.Element("price").Value, new CultureInfo("ru-RU")),
                        ItemDesc = element.Element("item_desc").Value,
                        ItemName = element.Element("item_name").Value,
                        PlaceId = element.Element("place_id").Value,
                        UploadRecordedDate = date,

                    });
                }
            }

            Context.SaveChanges();
        }
    }
}
