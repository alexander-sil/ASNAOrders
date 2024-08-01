using ASNAOrders.Web.Data;
using ASNAOrders.Web.Data.Orders;
using ASNAOrders.Web.Data.YENomenclature;
using ASNAOrders.Web.Models;
using ASNAOrders.Web.NotificationServiceExtensions;
using Castle.DynamicProxy.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static ASNAOrders.Web.Models.MarketplaceOrderCreatePromosInner;
using static ASNAOrders.Web.Models.NomenclatureItemsInnerBarcode;

namespace ASNAOrders.Web.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityModelConverter : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public static string PlaceResponse { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        private RabbitMQNotificationService NotifyService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public EntityModelConverter(IDbContextFactory<ASNAOrdersDbContext> factory, RabbitMQNotificationService notifyService)
        {
            Context = factory.CreateDbContext();
            NotifyService = notifyService;
        }

        /// <summary>
        /// Возвращает информацию об остатках товаров в указанной точке.
        /// </summary>
        /// <param name="placeId">ИД точки</param>
        /// <returns>Объект Availability</returns>
        public Availability GetAvailability(string placeId)
        {
            List<AvailabilityItemsInner> items = new List<AvailabilityItemsInner>();

            foreach (var item in Context.NativeStocks.Where(f => f.PlaceId == placeId))
            {
                items.Add(new AvailabilityItemsInner
                {
                    Id = Context.YENomenclatureItems.Where(f => f.Name == item.ItemName).First().Id.ToString(),
                    Stock = item.Qtty
                });
            }

            return new Availability() { Items = items };
        }

        /// <summary>
        /// Возвращает информацию о товарах в указанной точке.
        /// </summary>

        public Nomenclature GetComposition(string placeId)
        {
            List<NomenclatureItemsInner> items = new List<NomenclatureItemsInner>();

            foreach (var data in Context.YENomenclatureItems)
            {
                List<NomenclatureItemsInnerImagesInner> images = new List<NomenclatureItemsInnerImagesInner>();

                foreach (var image in data.Images)
                {
                    images.Add(new NomenclatureItemsInnerImagesInner()
                    {
                        Hash = image.Hash,
                        Url = image.Url
                    });
                }

                items.Add(new NomenclatureItemsInner()
                {
                    Id = data.Id.ToString(),
                    Vat = data.Vat,
                    Name = data.Name,
                    Price = data.Price,
                    Barcode = new NomenclatureItemsInnerBarcode()
                    {
                        Type = NomenclatureItemsInnerBarcode.TypeEnum.UpceEnum,
                        Value = data.Barcode.Value,
                        WeightEncoding = WeightEncodingEnum.NoneEnum,
                        Values = data.Barcode.Values
                    },
                    VendorCode = data.VendorCode,
                    CategoryId = data.CategoryId,
                    IsCatchWeight = data.IsCatchWeight,
                    Description = new NomenclatureItemsInnerDescription()
                    {
                        General = data.Description.General,
                        Composition = data.Description.Composition,
                        ExpiresIn = data.Description.ExpiresIn,
                        NutritionalValue = data.Description.NutritionalValue,
                        PackageInfo = data.Description.PackageInfo,
                        Purpose = data.Description.Purpose,
                        StorageRequirements = data.Description.StorageRequirements,
                        VendorCountry = data.Description.VendorCountry,
                        VendorName = data.Description.VendorName
                    },
                    ExciseValue = data.ExciseValue,
                    Images = images,
                    Labels = data.Labels,
                    Location = data.Location,
                    Measure = new NomenclatureItemsInnerMeasure()
                    {
                        Quantum = data.Measure.Quantum,
                        Value = data.Measure.Value,
                        Unit = data.Measure.Unit == "MLT" ? NomenclatureItemsInnerMeasure.UnitEnum.MLTEnum : NomenclatureItemsInnerMeasure.UnitEnum.GRMEnum
                    }
                });
            }

            List<NomenclatureCategoriesInner> categories = new List<NomenclatureCategoriesInner>();

            foreach (Category data in Context.Categories)
            {
                List<NomenclatureCategoriesInnerImagesInner> images = new List<NomenclatureCategoriesInnerImagesInner>();

                foreach (var image in data.Images)
                {
                    images.Add(new NomenclatureCategoriesInnerImagesInner()
                    {
                        Hash = image.Hash,
                        Url = image.Url
                    });
                }

                categories.Add(new NomenclatureCategoriesInner()
                {
                    Id = data.Id.ToString(), 
                    Name = data.Name,
                    Images = images
                });
            }

            return new Nomenclature()
            {
                Items = items,
                Categories = categories
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public PartnerOrderCreate200Response CreateOrder(PartnerOrderCreateRequest request)
        {
            List<Item> items = new List<Item>();

            foreach (var item in request.Items)
            {
                items.Add(new Item()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Modifications = new List<Modification>(),
                    Promos = new List<ItemPromo>(),
                    Quantity = item.Quantity
                });
            }

            Order order = new Order()
            {
                Items = items,
                Discriminator = request.Discriminator,
                DeliveryInfo = new DeliveryInfo()
                {
                    Discriminator = request.Discriminator.ToUpper(),
                    ClientArrivementDate = request.DeliveryInfo.ClientArrivementDate,
                    ClientName = request.DeliveryInfo.ClientName,
                    PhoneNumber = request.DeliveryInfo.PhoneNumber
                },
                Comment = request.Comment,
                EatsId = request.EatsId,
                PaymentInfo = new PaymentInfo()
                {
                    Discriminator = request.Discriminator.ToUpper(),
                    ItemsCost = request.PaymentInfo.ItemsCost,
                    PaymentType = request.PaymentInfo.PaymentType.ToString(),
                    Total = request.PaymentInfo.Total,
                    Change = request.PaymentInfo.Change
                },
                RestaurantId = request.RestaurantId,
                Platform = request.Platform.ToString()
            };

            Context.Orders.Add(order);

            List<string> item_names = new List<string>();

            foreach (var item in order.Items)
            {
                string nnt = Context.YENomenclatureItems.First(f => f.Name == item.Name).VendorCode;
                item_names.Add($"ННТ: {nnt}{Environment.NewLine}Имя: {item.Name}");
            }

            NotifyService.Send(new OrderNotification()
            {
                OrderId = order.Id,
                Price = order.PaymentInfo.Total.ToString(),
                Items = item_names
            });

            for (int i = 10; i > 0; i--)
            {
                Thread.Sleep(1000);
                Log.Information($"Awaiting response from place for order {order.Id}. {i} seconds left");
            }

            if (!string.IsNullOrWhiteSpace(PlaceResponse))
            {
                var itemNames = order.Items.Select(item => item.Name).Distinct().ToList();

                foreach (var itemName in itemNames)
                {
                    var itemCount = order.Items.Count(item => item.Name == itemName);
                    var stocksToUpdate = Context.NativeStocks.Where(stock => stock.ItemName == itemName && stock.PlaceId == PlaceResponse).ToList();

                    foreach (var stock in stocksToUpdate)
                    {
                        stock.Qtty -= itemCount;
                    }
                }

                Context.SaveChanges();

                return new PartnerOrderCreate200Response()
                {
                    OrderId = Context.Orders.Single(f => f == order).Id.ToString(),
                    Result = Properties.Resources.OrderDataString
                };
            }

            Context.Remove(order);

            throw new KeyNotFoundException(Properties.Resources.KeyNotFoundString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public PartnerOrderGet200Response GetOrderById(string orderId)
        {
            Order order = Context.Orders.FirstOrDefault(f => f.Id == int.Parse(orderId));

            if (order == null)
            {
                throw new KeyNotFoundException();
            }

            List<PickupOrderV1ItemsInner> items = new List<PickupOrderV1ItemsInner>();

            foreach (var item in order.Items)
            {
                items.Add(new PickupOrderV1ItemsInner()
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Price = item.Price,
                    Modifications = new List<YandexOrderCreateItemsInnerModificationsInner>(),
                    Promos = new List<YandexOrderCreatePromosInner>(),
                    Quantity = item.Quantity
                });
            }

            return new PartnerOrderGet200Response()
            {
                Items = items,
                Comment = order.Comment,
                DeliveryInfo = new PickupOrderV1DeliveryInfo()
                {
                    ClientArrivementDate = order.DeliveryInfo.ClientArrivementDate,
                    ClientName = order.DeliveryInfo.ClientName,
                    PhoneNumber = order.DeliveryInfo.PhoneNumber
                },
                Discriminator = order.Discriminator.ToLower(),
                EatsId = order.EatsId,
                RestaurantId = order.RestaurantId,
                PaymentInfo = new PickupOrderV1PaymentInfo()
                {
                    Total = order.PaymentInfo.Total,
                    Change = order.PaymentInfo.Change,
                    ItemsCost = order.PaymentInfo.ItemsCost,
                    PaymentType = order.PaymentInfo.PaymentType == "CARD" ? PickupOrderV1PaymentInfo.PaymentTypeEnum.CARDEnum : PickupOrderV1PaymentInfo.PaymentTypeEnum.CASHEnum
                },
                Platform = order.Platform == "YE" ? PartnerOrderGet200Response.PlatformEnum.YEEnum : PartnerOrderGet200Response.PlatformEnum.DCEnum
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderStatus GetOrderStatusById(string orderId)
        {
            return new OrderStatus()
            {
                Status = OrderStatus.StatusEnum.ACCEPTEDBYRESTAURANTEnum,
                Comment = Properties.Resources.OrderDataString,
                UpdatedAt = DateTime.Now
            };
        }

        /// <summary>
        /// Удаление контекста
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
