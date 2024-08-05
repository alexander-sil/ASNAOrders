using ASNAOrders.Web.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Barcodes")]
    public class Barcode
    {
        /// <summary>
        /// 
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Штрихкод товара
        /// </summary>
        /// <value>Штрихкод товара</value>
        /// <example>987654321098</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Value { get; set; }

        /// <summary>
        /// Список штрихкодов товара
        /// </summary>
        /// <value>Список штрихкодов товара</value>
        public List<string> Values { get; set; }

        /// <summary>
        /// Тип штрихкода. Возможные значения см. TypeEnum
        /// </summary>
        /// <value>Тип штрихкода</value>
        /// <example>ean13</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Type { get; set; }


        /// <summary>
        /// Алгоритм кодирования веса в штрихкоде. Возможные значения см. WeightEncodingEnum
        /// </summary>
        /// <value>Алгоритм кодирования веса в штрихкоде</value>
        /// <example>ean13-tail-gram-4</example>
        [Column]
        [StringLength(2048)]
        public string WeightEncoding { get; set; }
    }
}