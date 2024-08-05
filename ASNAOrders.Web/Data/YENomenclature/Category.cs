using ASNAOrders.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// .NET 8 Entity Framework Core entity data model for storing nomenclature category information. This schema is inner.
    /// </summary>
    [Table("Categories")]
    public class Category
    {
        /// <summary>
        /// Внутренний уникальный идентификатор категории в системе партнера
        /// </summary>
        /// <value>Внутренний уникальный идентификатор категории в системе партнера</value>
        /// <example>some-uniq-identifier</example>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        /// <value>Наименование категории</value>
        /// <example>Молоко</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Name { get; set; }

        /// <summary>
        /// Порядок сортировки от меньшего к большему
        /// </summary>
        /// <value>Порядок сортировки от меньшего к большему</value>
        /// <example>0</example>
        [Column]
        public int SortOrder { get; set; }

        /// <summary>
        /// Изображение категории
        /// </summary>
        /// <value>Изображение категории</value>
        [InverseProperty(nameof(CategoryImage.Owner))]
        public virtual List<CategoryImage> Images { get; set; }
    }
}