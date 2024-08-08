using ASNAOrders.Web.ConfigServiceExtensions;
using ASNAOrders.Web.Data;
using ASNAOrders.Web.Data.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Service to watch for stock uploads from agent. XML format is used.
    /// </summary>
    public class XMLStockWatcherService : IDisposable, IHostedService
    {
        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private FileSystemWatcher Watcher { get; set; }

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
            Context = contextFactory.CreateDbContext();

            
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

            int i = 0;

            foreach (XElement element in document.Element("Root").Elements())
            {
                if (Context.NativeStocks.Where(f => f.ItemName.Equals(element.Element("item_name").Value)).Count() < 1)
                {
                    var newStock = new NativeStock()
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
                        UploadRecordedDate = date.ToString(),

                    };

                    Context.NativeStocks.Add(newStock);

                    #if DEBUG
                    Log.Information($"Processed new stock info. NNT {newStock.ItemId} name {newStock.ItemName} quantity {newStock.Qtty} category {newStock.Category}");
                    #endif

                    i++;
                }
            }

            Context.SaveChanges();
            Log.Information($"Processed a total of {i} native stock objects. The changes will come into effect (AKA formatted) upon next stop or restart operation.");
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void DoWork()
        {
            string path = Program.XMLPath;

            foreach (FileInfo file in new DirectoryInfo(path).EnumerateFiles())
            {
                DateTime date = DateTime.ParseExact(Regex.Replace(file.Name, @"_.*\.xml", string.Empty), Properties.Resources.DefaultDateFormatString, new CultureInfo("nl-NL"));
                XDocument document = XDocument.Parse(File.ReadAllText(file.FullName));

                foreach (XElement element in document.Element("Root").Elements())
                {
                    if (Context.NativeStocks.Where(f => f.ItemName.Equals(element.Element("item_name").Value)).Count() < 1)
                    {
                        var extStock = new NativeStock()
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
                            UploadRecordedDate = date.ToString(),

                        };

                        Context.NativeStocks.Add(extStock);
                        #if DEBUG
                        Log.Information($"Processed existing stock info. NNT {extStock.ItemId} name {extStock.ItemName} quantity {extStock.Qtty} category {extStock.Category}");
                        #endif
                    }
                }
            }

            Context.SaveChanges();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Watcher = new FileSystemWatcher(Program.XMLPath);

            Watcher.Created += OnUpload;

            Log.Information($"Started XML stock watcher service {DateTime.Now}");
            return Task.Run(DoWork);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
