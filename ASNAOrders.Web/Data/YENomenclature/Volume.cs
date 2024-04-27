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
    [Table("YandexEatsVolumes")]
    public class Volume
    {
        /// <summary>
        /// Public primary key identifier for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Значение объема
        /// </summary>
        /// <value>Значение объема</value>
        /// <example>100</example>
        [Column]
        [Required]
        public int Value { get; set; }

        /// <summary>
        /// Единица измерения обьема
        /// Возможные значения: CMQ, DMQ
        /// </summary>
        /// <value>Единица измерения обьема</value>
        /// <example>DMQ</example>
        [Column]
        [Required]
        [StringLength(8)]
        public string Unit { get; set; }
    }
}