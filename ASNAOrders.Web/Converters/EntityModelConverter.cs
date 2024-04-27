using ASNAOrders.Web.Data;
using ASNAOrders.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASNAOrders.Web.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityModelConverter
    {
        /// <summary>
        /// 
        /// </summary>
        public static ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// Возвращает информацию об остатках товаров в указанной точке.
        /// </summary>
        /// <param name="placeId">ИД точки</param>
        /// <returns>Объект Availability</returns>
        public static Availability GetAvailability(string placeId)
        {
            List<AvailabilityItemsInner> items = new List<AvailabilityItemsInner>();

            foreach (var item in Context.NativeStocks.Where(f => f.PlaceId == placeId))
            {
                items.Add(new AvailabilityItemsInner
                {
                    Id = Context.YENomenclatureItems.Where(f => f.Name == item.ItemName).First().Id,
                    Stock = item.Qtty
                });
            }

            return new Availability() { Items = items };
        }
    }
}
