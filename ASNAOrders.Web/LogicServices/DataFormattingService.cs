using ASNAOrders.Web.Data;
using Castle.Components.DictionaryAdapter.Xml;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to process (aka format) raw native stock uploads to YE-ready nomenclature format.
    /// </summary>
    public class DataFormattingService
    {
        public ASNAOrdersDbContext Context { get; set; }


        /// <summary>
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="context">Database context to write to. NativeStocks table and Nomenclature cluster are used.</param>
        public DataFormattingService(ASNAOrdersDbContext context)
        {
            Context = context;

            context.SavedChanges += OnSaveChanges;
        }

        private void OnSaveChanges(object sender, Microsoft.EntityFrameworkCore.SavedChangesEventArgs e)
        {
            Context.
        }
    }
}
