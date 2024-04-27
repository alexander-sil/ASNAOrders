using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing promotional records. This schema is an inner placeholder.
    /// </summary>
    [Table("Items")]
    public class Promo
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Owner record for InversePropertyAttribute at Data\Order.cs
        /// </summary>
        public virtual Order Owner { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым - GIFT - PERCENTAGE - FIXED
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым - GIFT - PERCENTAGE - FIXED</value>
        /// <example>FIXED</example>
        [Column]
        public string Type { get; set; }

        /// <summary>
        /// Сума скидки в валюте
        /// </summary>
        /// <value>Сума скидки в валюте</value>
        /// <example>0</example>
        [Column]
        public decimal Discount { get; set; }
    }
}