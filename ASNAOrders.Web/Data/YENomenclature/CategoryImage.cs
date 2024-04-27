using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// .NET 8 Entity Framework Core entity data model for storing category images. This schema is inner.
    /// </summary>
    [Table("CategoryImages")]
    public class CategoryImage
    {

        /// <summary>
        /// Primary key column for ASNAOrders database entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Owner parameter for InversePropertyAttribute at Data/Category.cs
        /// </summary>
        public virtual Category Owner { get; set; }

        /// <summary>
        /// SHA1-хэш от содержимого файла изображения. Рассчитывается партнером, служит признаком уникальности. В случае если он меняется, Яндекс Еда перезагружает картинку
        /// </summary>
        /// <value>SHA1-хэш от содержимого файла изображения. Рассчитывается партнером, служит признаком уникальности. В случае если он меняется, Яндекс Еда перезагружает картинку</value>
        /// <example>2fd4e1c6 7a2d28fc ed849ee1 bb76e739 1b93eb12</example>
        [Column]
        [Required]
        [StringLength(80)]
        public string Hash { get; set; }

        /// <summary>
        /// Ссылка на изображение для скачивания
        /// </summary>
        /// <value>Ссылка на изображение для скачивания</value>
        /// <example>https://ya.ru/images/image_1.jpg</example>
        [Column]
        [Required]
        [StringLength(1024)]
        public string Url { get; set; }
        
    }
}