using System.Collections.Generic;

namespace ASNAOrders.Web.NotificationServiceExtensions
{
    /// <summary>
    /// Notification model to be issued to agent via RabbitMQNotificationService.cs
    /// </summary>
    public class OrderNotification
    {
        /// <summary>
        /// The specified order's primary key in the ASNAOrders database.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Total price of the order, calculated from PaymentInfo.Total property.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// The specified order's current items, ranged randomly or grouped by ItemName. 
        /// </summary>
        public List<string> Items { get; set; }
    }
}
