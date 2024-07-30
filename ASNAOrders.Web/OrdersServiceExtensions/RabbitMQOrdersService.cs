using ASNAOrders.Web.ConfigServiceExtensions;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Serilog;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System;
using ASNAOrders.Web.Data;
using System.Linq;
using System.Collections.Generic;
using ASNAOrders.Web.Models;
using ASNAOrders.Web.Data.YENomenclature;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using ASNAOrders.Web.Converters;
using Microsoft.EntityFrameworkCore;

namespace ASNAOrders.Web.OrdersServiceExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMQOrdersService : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConnection Connection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static IModel Channel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public RabbitMQOrdersService(IDbContextFactory<ASNAOrdersDbContext> contextFactory)
        {
            using var context = contextFactory.CreateDbContext();
            Context = context;

            var factory = new ConnectionFactory
            {
                HostName = StaticConfig.MQHostname,
                Port = StaticConfig.MQPort,
                VirtualHost = StaticConfig.MQVHost,
                UserName = Properties.Resources.ConfigMQUsername,
                Password = Properties.Resources.ConfigMQPassword
            };

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();

            Channel.QueueDeclare(queue: Properties.Resources.OrdersQueueString,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Log.Information($"Waiting for RabbitMQ ORDERS messages @ factory {nameof(factory)} hostname {factory.HostName} port {factory.Port} vhost {factory.VirtualHost}");

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += OnReceived;

            Channel.BasicConsume(queue: Properties.Resources.OrdersQueueString,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            JToken root = JToken.Parse(Encoding.UTF8.GetString(e.Body.ToArray()));

            string placeId = (string)root["placeId"];
            EntityModelConverter.PlaceResponse = placeId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            Channel.Dispose();
            Connection.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        ~RabbitMQOrdersService()
        {
            this.Dispose();
        }
    }
}
