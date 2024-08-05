using ASNAOrders.Web.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ItemMeasures")]
    public class Measure
    {
        /// <summary>
        /// Primary key identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Значение измерения. Для весовых товаров передавать 1000. Для штучных товаров актуальный вес товара.
        /// </summary>
        /// <value>Значение измерения. Для весовых товаров передавать 1000. Для штучных товаров актуальный вес товара.</value>
        /// <example>1000</example>
        [Column]
        [Required]
        public int Value { get; set; }

        /// <summary>
        /// Наименьшее количество товара (квант) доступное для заказа. Значение расчитывается от value. Для весовых товаров (isCatchWeight&#x3D;true) должно быть заполненно. В примере указано как передавать квант равный 300 граммам.
        /// </summary>
        /// <value>Наименьшее количество товара (квант) доступное для заказа. Значение расчитывается от value. Для весовых товаров (isCatchWeight&#x3D;true) должно быть заполненно. В примере указано как передавать квант равный 300 граммам.</value>
        /// <example>0.3</example>
        [Column]
        public float Quantum { get; set; }

        /// <summary>
        /// Единица измерения. Возможные значения: MLT, GRM
        /// </summary>
        /// <value>Единица измерения</value>
        /// <example>GRM</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Unit { get; set; }
    }
}