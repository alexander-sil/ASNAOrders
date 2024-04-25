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
        /// 
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime RecordDate { get; set; }


    }
}
