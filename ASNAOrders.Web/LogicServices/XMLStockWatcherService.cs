using ASNAOrders.Web.ConfigServiceExtensions;
using ASNAOrders.Web.Data;
using System.IO;
using System.Xml.Linq;

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

            if (!Directory.Exists(StaticConfig.XMLStockPath))
            {
                Directory.CreateDirectory(StaticConfig.XMLStockPath);
            }

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
            foreach (FileInfo file in new DirectoryInfo(StaticConfig.XMLStockPath).EnumerateFiles())
            {
                XDocument document = XDocument.Parse(File.ReadAllText(file.FullName));
                
                foreach(XElement element in document.Elements())
                {

                    element.Element("org_id");
                }
            }
        }
    }
}
