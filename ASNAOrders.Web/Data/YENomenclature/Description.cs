using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ItemDescs")]
    public class Description
    {
        /// <summary>
        /// 
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Общее описание товара
        /// </summary>
        /// <value>Общее описание товара</value>
        [Column]
        [StringLength(4096)]
        public string General { get; set; }

        /// <summary>
        /// Сведения о составе
        /// </summary>
        /// <value>Сведения о составе</value>
        /// <example>молоко нормализованное (молоко цельное, молоко обезжиренное)</example>
        [Column]
        [StringLength(256)]
        public string Composition { get; set; }

        /// <summary>
        /// Пищевая ценность
        /// </summary>
        /// <value>Пищевая ценность</value>
        /// <example>600 ккал, 8 белки, 3,2 жиры, 40 углеводы</example>
        [Column]
        [StringLength(256)]
        public string NutritionalValue { get; set; }

        /// <summary>
        /// Назначение
        /// </summary>
        /// <value>Назначение</value>
        [Column]
        [StringLength(64)]
        public string Purpose { get; set; }

        /// <summary>
        /// Условия хранения
        /// </summary>
        /// <value>Условия хранения</value>
        /// <example>от -5 до 5 градусов</example>
        [Column]
        [StringLength(256)]
        public string StorageRequirements { get; set; }

        /// <summary>
        /// Срок годности в днях
        /// </summary>
        /// <value>Срок годности в днях</value>
        /// <example>60</example>
        [Column]
        [StringLength(8)]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Страна изготовления
        /// </summary>
        /// <value>Страна изготовления</value>
        /// <example>Россия</example>
        [Column]
        [StringLength(80)]
        public string VendorCountry { get; set; }

        /// <summary>
        /// Сведения об упаковке
        /// </summary>
        /// <value>Сведения об упаковке</value>
        /// <example>Тетрапак</example>
        [Column]
        [StringLength(50)]
        public string PackageInfo { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        /// <value>Производитель</value>
        /// <example>ООО Молочный завод</example>
        [Column]
        [StringLength(128)]
        public string VendorName { get; set; }
    }
}