using ASNAOrders.Web.Data;
using System.IO;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Interface for FileSystemWatcher based watcher service implementations.
    /// </summary>
    public interface IWatcherService
    {
        public ASNAOrdersDbContext Context { get; set; }

        public FileSystemWatcher Watcher { get; set; }

        private void OnUpload(object sender, FileSystemEventArgs e) { }
        
    }
}