using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Data.Stocks
{
    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing direct uploads from .
    /// </summary>
    [Table("NativeStocks")]
    public class NativeStock
    {
        /// <summary>
        /// Primary key for entries.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Timestamp of UploadRecordedToDatabase event. For utility use only.
        /// </summary>
        [Column]
        public DateTime UploadRecordedDate { get; set; }

        /// <summary>
        /// Unique place identifier sent by agent.
        /// </summary>
        [Column]
        [StringLength(50)]
        public string PlaceId { get; set; }

        /// <summary>
        /// cntr_name column in wSklad.ws_Prt
        /// </summary>
        [Column]
        [StringLength(64)]
        public string Country { get; set; }

        /// <summary>
        /// rnp_name column in wSklad.ws_Prt
        /// </summary>
        [Column]
        [StringLength(64)]
        public string Composition { get; set; }

        /// <summary>
        /// cmp_name column in wSklad.ws_Prt
        /// </summary>
        [Column]
        [StringLength(500)]
        public string ItemName { get; set; }

        /// <summary>
        /// cmp_unicode column in wSklad.ws_Prt - ННТ ака артикул Трейд Фарм
        /// </summary>
        [Column]
        [StringLength(32)]
        public string ItemId { get; set; }

        /// <summary>
        /// Stock quantity at the location by the specified filename prefix ((Prt_CurrQnt - Prt_Reserve) column in wSklad.ws_Prt).
        /// </summary>
        [Column]
        public int Qtty { get; set; }


        /// <summary>
        /// Str_Value4_04 column in wSklad.ws_Prt - розничная цена без НДС
        /// </summary>
        [Column]
        public double Price { get; set; }

        /// <summary>
        /// pro_name column in wSklad.ws_Prt - производитель/категория
        /// </summary>
        [Column]
        [StringLength(64)]
        public string Category { get; set; }

        /// <summary>
        /// info_str_11 column in wSklad.ws_Prt
        /// </summary>
        [Column]
        [StringLength(500)]
        public string ItemDesc { get; set; }

        /// <summary>
        /// str_barcode column in wSklad.ws_Prt
        /// </summary>
        [Column]
        [StringLength(16)]
        public string Barcode { get; set; }

    }
}
