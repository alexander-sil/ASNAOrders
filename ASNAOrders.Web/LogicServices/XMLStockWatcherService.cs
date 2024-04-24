using ASNAOrders.Web.ConfigServiceExtensions;
using ASNAOrders.Web.Data;
using System.IO;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Service to watch for stock uploads from agent. XML format is used.
    /// </summary>
    public class XMLStockWatcherService : IWatcherService
    {
        public ASNAOrdersDbContext Context { get; set; }

        public FileSystemWatcher Watcher { get; set; }

        /// <summary>
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="context">Database context to write to. Stocks table is used.</param>
        public XMLStockWatcherService(ASNAOrdersDbContext context) 
        { 
            Context = context;

            Watcher = new FileSystemWatcher(StaticConfig.XMLStockPath);

            Watcher.Created += OnXmlStockReceived;
        }
        
        /// <summary>
        /// Event handler to process XML stock uploads received from agent service/application in file form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnXmlStockReceived(object sender, FileSystemEventArgs e)
        {
            
        }
    }
}
