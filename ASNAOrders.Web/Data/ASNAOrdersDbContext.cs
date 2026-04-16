using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text.Json;
using ASNAOrders.Web.Data.YENomenclature;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Игнорируем List<string> как сущность
            modelBuilder.Ignore<List<string>>();

            var listComparer = new ValueComparer<List<string>>(
                (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                c => c == null ? 0 : c.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                c => c == null ? new List<string>() : c.ToList()
            );

            var converter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrEmpty(v)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>() ?? new List<string>()
            );

            modelBuilder.Entity<Barcode>(entity =>
            {
                // Конфигурация для Values
                entity.Property(e => e.Values)
                    .HasConversion(converter)
                    .HasColumnType("nvarchar(max)")
                    .Metadata.SetValueComparer(listComparer);

                // Остальная конфигурация...
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
