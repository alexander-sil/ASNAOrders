using ASNAOrders.Web.Converters;
using ASNAOrders.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Data.Orders
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing received orders.
    /// </summary>
    [Table("Orders")]
    public class Order
    {
        /// <summary>
        /// System identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор платформы, DC - Delivery Club, YE - Yandex Eda
        /// </summary>
        /// <value>Идентификатор платформы, DC - Delivery Club, YE - Yandex Eda</value>
        /// <example>YE</example>
        [Column]
        [Required]
        [StringLength(4)]
        public string Platform { get; set; }

        /// <summary>
        /// Дискриминатор схемы. Для заказов с самовывозом равен \&quot;pickup\&quot;
        /// </summary>
        /// <value>Дискриминатор схемы. Для заказов с самовывозом равен \&quot;pickup\&quot;</value>
        /// <example>pickup</example>
        [Column]
        [Required]
        [StringLength(16)]
        public string Discriminator { get; set; }

        /// <summary>
        /// Сквозной идентификатор заказа на стороне Яндекс Еды в формате DDDDDD-DDDDDDD
        /// </summary>
        /// <value>Сквозной идентификатор заказа на стороне Яндекс Еды в формате DDDDDD-DDDDDDD</value>
        /// <example>230130-1234567</example>
        [Column]
        [Required]
        [StringLength(16)]
        public string EatsId { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор магазина в системе партнёра. Формат свободный, рекомендуется UUID4
        /// </summary>
        /// <value>Внутренний уникальный идентификатор магазина в системе партнёра. Формат свободный, рекомендуется UUID4</value>
        /// <example>937c57f6-4508-4858-be7f-20691a16fbb0</example>
        [Column]
        [StringLength(48)]
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or Sets DeliveryInfo
        /// </summary>
        [Required]
        public DeliveryInfo DeliveryInfo { get; set; }

        /// <summary>
        /// Gets or Sets PaymentInfo
        /// </summary>
        [Required]
        public PaymentInfo PaymentInfo { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>
        [Required]
        public List<Item> Items { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым</value>
        /// <example>0</example>
        [Column]
        public int Persons { get; set; }

        /// <summary>
        /// Дополнительная информация о заказе
        /// </summary>
        /// <value>Дополнительная информация о заказе</value>
        /// <example>Дополнительная информация о заказе: ...</example>
        [Column]
        [StringLength(512)]
        public string Comment { get; set; }

        /// <summary>
        /// Параметр не поддерживается в интеграции магазинов и передается пустым
        /// </summary>
        /// <value>Параметр не поддерживается в интеграции магазинов и передается пустым</value>
        [InverseProperty(nameof(Promo.Owner))]
        public List<Promo> Promos { get; set; }
    }
}
