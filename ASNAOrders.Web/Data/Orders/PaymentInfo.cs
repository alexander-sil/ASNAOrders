using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing received order payment information. This schema is inner.
    /// </summary>
    [Table("PaymentInfos")]
    public class PaymentInfo
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Determines the type of the record schema.
        /// Possible values are YANDEX, MARKETPLACE and PICKUP.
        /// </summary>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Discriminator { get; set; }

        /// <summary>
        /// Информация о типе оплаты
        /// </summary>
        /// <value>Информация о типе оплаты</value>
        [Column]
        [Required]
        [StringLength(2480)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Полная сумма стоимости товаров в заказе
        /// </summary>
        /// <value>Полная сумма стоимости товаров в заказе</value>
        /// <example>1570</example>
        [Column]
        [Required]
        public double ItemsCost { get; set; }


        /// <summary>
        /// Общая стоимость заказа
        /// </summary>
        /// <value>Общая стоимость заказа</value>
        /// <example>1570</example>
        [Column]
        public double Total { get; set; }

        /// <summary>
        /// Сумма, с которой потребуется сдача. Другими словами, это сумма которой клиент планирует расплатиться
        /// </summary>
        /// <value>Сумма, с которой потребуется сдача. Другими словами, это сумма которой клиент планирует расплатиться</value>
        /// <example>2000</example>
        [Column]
        public double Change { get; set; }
    }
}
