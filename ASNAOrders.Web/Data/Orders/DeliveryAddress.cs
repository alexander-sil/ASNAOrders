using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing delivery address information. This schema is inner.
    /// </summary>
    [Table("DeliveryAddrs")]
    public class DeliveryAddress
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Полный адрес
        /// </summary>
        /// <value>Полный адрес</value>
        /// <example>Москва, улица Тверская, дом 1 строение 4, подъезд 2. 4-й этаж, код домофона: 123 К 4567</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Full { get; set; }

        /// <summary>
        /// Широта точки доставки
        /// </summary>
        /// <value>Широта точки доставки</value>
        /// <example>55.756994</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Latitude { get; set; }

        /// <summary>
        /// Долгота точки доставки
        /// </summary>
        /// <value>Долгота точки доставки</value>
        /// <example>37.614006</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Longitude { get; set; }
    }
}