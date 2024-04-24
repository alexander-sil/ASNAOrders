using Microsoft.EntityFrameworkCore;

namespace ASNAOrders.Web.Data
{
    public class ASNAOrdersDbContext : DbContext
    {
        public 
        public ASNAOrdersDbContext(DbContextOptions options) : base(options) { }
    }
}
