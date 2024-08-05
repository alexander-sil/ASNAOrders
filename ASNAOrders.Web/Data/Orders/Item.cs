using ASNAOrders.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing received item information. This schema is inner.
    /// </summary>
    [Table("Items")]
    public class Item
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        /// <value>Наименование товара</value>
        /// <example>Молоко Домик в деревне</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Name { get; set; }

        /// <summary>
        /// Количество позиции в заказе
        /// </summary>
        /// <value>Количество позиции в заказе</value>
        /// <example>3</example>
        [Column]
        [Required]
        public float Quantity { get; set; }

        /// <summary>
        /// Цена одной штуки товара. Для весовых товаров цена передается за 1 кг
        /// </summary>
        /// <value>Цена одной штуки товара. Для весовых товаров цена передается за 1 кг</value>
        /// <example>84</example>
        [Column]
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым</value>
        [InverseProperty(nameof(Modification.Owner))]
        public virtual List<Modification> Modifications { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым</value>
        [InverseProperty(nameof(OrderPromo.Owner))]
        public virtual List<ItemPromo> Promos { get; set; }
    }
}
