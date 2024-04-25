using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing modification information. This schema is inner.
    /// </summary>
    [Table("Modifications")]
    public class Modification
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование модификатора
        /// </summary>
        /// <value>Наименование модификатора</value>
        /// <example>Нарезать</example>
        [Column]
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// Количество в заказе
        /// </summary>
        /// <value>Количество в заказе</value>
        /// <example>1</example>
        [Column]
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Цена модификатора
        /// </summary>
        /// <value>Цена модификатора</value>
        /// <example>39</example>
        [Column]
        [Required]
        public double Price { get; set; }
    }
}
