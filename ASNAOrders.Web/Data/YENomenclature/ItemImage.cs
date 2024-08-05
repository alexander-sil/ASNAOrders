using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ItemImages")]
    public class ItemImage
    {

        /// <summary>
        /// 
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Owner parameter for InversePropertyAttribute at Data/Category.cs
        /// </summary>
        public virtual NomenclatureItem Owner { get; set; }

        /// <summary>
        /// SHA1-хэш от содержимого файла изображения. Рассчитывается партнером, служит признаком уникальности. В случае если он меняется, Яндекс Еда перезагружает картинку
        /// </summary>
        /// <value>SHA1-хэш от содержимого файла изображения. Рассчитывается партнером, служит признаком уникальности. В случае если он меняется, Яндекс Еда перезагружает картинку</value>
        /// <example>2fd4e1c6 7a2d28fc ed849ee1 bb76e739 1b93eb12</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Hash { get; set; }

        /// <summary>
        /// Ссылка на изображение товара для скачивания
        /// </summary>
        /// <value>Ссылка на изображение товара для скачивания</value>
        /// <example>https://ya.ru/images/image_1.jpg</example>
        [Column]
        [Required]
        [StringLength(2048)]
        public string Url { get; set; }

        /// <summary>
        /// Порядок сортировки от меньшего к большему
        /// </summary>
        /// <value>Порядок сортировки от меньшего к большему</value>
        /// <example>0</example>
        [Column]
        public int Order { get; set; }
    }
}