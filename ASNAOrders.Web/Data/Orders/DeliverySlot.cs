using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing information about delivery slots. This schema is inner.
    /// </summary>
    [Table("DeliverySlots")]
    public class DeliverySlot
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlotId { get; set; }

        /// <summary>
        /// Начало слота доставки
        /// </summary>
        /// <value>Начало слота доставки</value>
        /// <example>1970-01-01T00:00:27.870+00:20</example>
        [Column]
        [Required]
        public DateTime From { get; set; }

        /// <summary>
        /// Конец слота доставки
        /// </summary>
        /// <value>Конец слота доставки</value>
        /// <example>1970-01-01T00:00:27.870+00:20</example>
        [Column]
        [Required]
        public DateTime To { get; set; }
    }
}
