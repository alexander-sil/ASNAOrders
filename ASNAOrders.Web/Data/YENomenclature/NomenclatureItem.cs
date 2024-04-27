using ASNAOrders.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ASNAOrders.Web.Data.YENomenclature
{
    /// <summary>
    /// 
    /// </summary>
    [Table("YENomenclatureItems")]
    public class NomenclatureItem
    {
        /// <summary>
        /// Внутренний уникальный идентификатор товара в системе партнера
        /// </summary>
        /// <value>Внутренний уникальный идентификатор товара в системе партнера</value>
        /// <example>some-uniq-identifier</example>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /// <summary>
        /// Артикул товара в системе партнера
        /// </summary>
        /// <value>Артикул товара в системе партнера</value>
        [Column]
        [Required]
        [StringLength(16)]
        public string VendorCode { get; set; }

        /// <summary>
        /// Внутренний уникальный идентификатор категории в системе партнера из блока #/categories
        /// </summary>
        /// <value>Внутренний уникальный идентификатор категории в системе партнера из блока #/categories</value>
        /// <example>some-uniq-identifier</example>
        [Column]
        [Required]
        [StringLength(16)]
        public string CategoryId { get; set; }

        /// <summary>
        /// Расположение товара в маганизе. Можно не передавать.
        /// </summary>
        /// <value>Расположение товара в маганизе. Можно не передавать.</value>
        /// <example>Бакалея. Линия 8</example>
        [Column]
        [StringLength(50)]
        public string Location { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        /// <value>Наименование товара</value>
        /// <example>Молоко Домик в деревне</example>
        [Column]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [Required]
        public Description Description { get; set; }

        /// <summary>
        /// Цена товара. Товары с нулевой ценой не обрабатываются
        /// </summary>
        /// <value>Цена товара. Товары с нулевой ценой не обрабатываются</value>
        /// <example>189</example>
        [Column]
        [Required]
        public float Price { get; set; }

        /// <summary>
        /// Старая цена товара. Используется для отображения скидок на товары. Если не передано - будет использован null в качестве значения
        /// </summary>
        /// <value>Старая цена товара. Используется для отображения скидок на товары. Если не передано - будет использован null в качестве значения</value>
        /// <example>239</example>
        [Column]
        public float? OldPrice { get; set; }

        /// <summary>
        /// НДС, включенный в стоимость, в процентах. По-умолчанию, значение соответствует настройке конкретной торговой точки в системе Яндекс Еды
        /// </summary>
        /// <value>НДС, включенный в стоимость, в процентах. По-умолчанию, значение соответствует настройке конкретной торговой точки в системе Яндекс Еды</value>
        /// <example>20</example>
        [Column]
        public int Vat { get; set; }

        /// <summary>
        /// Gets or Sets Barcode
        /// </summary>
        [Required]
        public Barcode Barcode { get; set; }

        /// <summary>
        /// Gets or Sets Measure
        /// </summary>
        [Required]
        public Measure Measure { get; set; }

        /// <summary>
        /// Gets or Sets Volume
        /// </summary>
        public Volume Volume { get; set; }

        /// <summary>
        /// Параметр указывающий весовой товар или нет. Для весовых true
        /// </summary>
        /// <value>Параметр указывающий весовой товар или нет. Для весовых true</value>
        /// <example>false</example>
        [Column]
        [Required]
        public bool IsCatchWeight { get; set; }

        /// <summary>
        /// Тип акциза. Пример, ССН (кириллица - сахаросодержащие напитки)
        /// </summary>
        /// <value>Тип акциза. Пример, ССН (кириллица - сахаросодержащие напитки)</value>
        /// <example>ССН</example>
        [Column]
        [StringLength(16)]
        public string ExciseValue { get; set; }

        /// <summary>
        /// Порядок сортировки от меньшего к большему
        /// </summary>
        /// <value>Порядок сортировки от меньшего к большему</value>
        /// <example>0</example>
        [Column]
        public int SortOrder { get; set; }

        /// <summary>
        /// Изображение товара
        /// </summary>
        /// <value>Изображение товара</value>
        [Required]
        public List<ItemImage> Images { get; set; }

        /// <summary>
        /// Список особых признаков товара. Например: Маркированный, Для взрослых, Алкоголь, СТМ, Рецептурный и другие.
        /// </summary>
        /// <value>Список особых признаков товара. Например: Маркированный, Для взрослых, Алкоголь, СТМ, Рецептурный и другие.</value>
        public List<string> Labels { get; set; }
    }
}