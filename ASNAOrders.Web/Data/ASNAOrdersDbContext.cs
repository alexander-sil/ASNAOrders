using Microsoft.EntityFrameworkCore;

namespace ASNAOrders.Web.Data
{
    /// <summary>
    /// Primary database context to serve for all ASNAOrders-related services, watchers and configurations.
    /// </summary>
    public class ASNAOrdersDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.Order> Orders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.Item> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.Promo> Promos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.DeliveryInfo> DeliveryInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.PaymentInfo> PaymentInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.DeliveryAddress> DeliveryAddrs { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Orders.DeliverySlot> DeliverySlots { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ASNAOrdersDbContext(DbContextOptions options) : base(options) { }
    }
}
