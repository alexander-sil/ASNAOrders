using System.Collections.Generic;
using System.Text.Json;
using ASNAOrders.Web.Data.YENomenclature;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        public DbSet<Orders.OrderPromo> Promos { get; set; }

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
        public DbSet<YENomenclature.Barcode> Barcodes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.Category> Categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.CategoryImage> CategoryImages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.Description> ItemDescs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.ItemImage> ItemImages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.Measure> ItemMeasures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.Volume> YandexEatsVolumes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<YENomenclature.NomenclatureItem> YENomenclatureItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Stocks.NativeStock> NativeStocks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ASNAOrdersDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barcode>()
                .Property(e => e.Values)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                );
        }
    }
}
