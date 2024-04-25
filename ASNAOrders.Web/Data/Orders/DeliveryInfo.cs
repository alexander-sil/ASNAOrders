﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Data.Orders
{

    /// <summary>
    /// .NET Entity Framework Core 8 data model for storing order delivery information. This schema is inner.
    /// </summary>
    public class DeliveryInfo
    {
        /// <summary>
        /// Имя клиента в сервисе Яндекс Еда
        /// </summary>
        /// <value>Имя клиента в сервисе Яндекс Еда</value>
        /// <example>Иванов Иван Иванович</example>
        [Column]
        [Required]
        public string ClientName { get; set; }

        /// <summary>
        /// Номер телефона для связи с клиентом в международном формате. Состоит из частей \&quot;+&lt;код страны&gt;&lt;номер&gt;\&quot;. Может содержать добавочный номер: \&quot;+&lt;код страны&gt;&lt;номер&gt; доб. &lt;добавочный номер&gt;\&quot;
        /// </summary>
        /// <value>Номер телефона для связи с клиентом в международном формате. Состоит из частей \&quot;+&lt;код страны&gt;&lt;номер&gt;\&quot;. Может содержать добавочный номер: \&quot;+&lt;код страны&gt;&lt;номер&gt; доб. &lt;добавочный номер&gt;\&quot;</value>
        /// <example>+74732006745 доб. 12099</example>
        [Column]
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата, когда придет клиент в магазин, в формате RFC3339 с дробной частью секунд (Y-m-d\\TH:i:s.uP)
        /// </summary>
        /// <value>Дата, когда придет клиент в магазин, в формате RFC3339 с дробной частью секунд (Y-m-d\\TH:i:s.uP)</value>
        /// <example>1970-01-01T00:00:27.870+00:20</example>
        [Column]
        [Required]
        public DateTime ClientArrivementDate { get; set; }
    }
}