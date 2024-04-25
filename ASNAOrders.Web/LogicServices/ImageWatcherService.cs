using ASNAOrders.Web.ConfigServiceExtensions;
using ASNAOrders.Web.Data;
using System;
using System.IO;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to watch for new and existing images uploaded to {StaticConfig.XMLStockPath}\images directory.
    /// </summary>
    public class ImageWatcherService : IWatcherService
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
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="context">Database context to write to. Stocks table is used.</param>
        public ImageWatcherService(ASNAOrdersDbContext context)
        {
            Context = context;

            if (!Directory.Exists(Path.Combine(StaticConfig.XMLStockPath, "images")))
            {
                Directory.CreateDirectory(Path.Combine(StaticConfig.XMLStockPath, "images"));
            }

            foreach (FileInfo file in new DirectoryInfo(Path.Combine(StaticConfig.XMLStockPath, "images")).EnumerateFiles())
            {

            }

            Watcher = new FileSystemWatcher(Path.Combine(StaticConfig.XMLStockPath, "images"));

            Watcher.Created += OnUpload;
        }

        private void OnUpload(object sender, FileSystemEventArgs e)
        {
            
        }
    }
}
