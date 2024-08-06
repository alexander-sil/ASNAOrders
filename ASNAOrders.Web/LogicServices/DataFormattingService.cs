using ASNAOrders.Web.Data;
using ASNAOrders.Web.Data.Orders;
using ASNAOrders.Web.Data.YENomenclature;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using SQLitePCL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to process (aka format) raw native stock uploads to YE-ready nomenclature format.
    /// </summary>
    public class DataFormattingService : IDisposable, IHostedService
    {
        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void FormatData()
        {
            for (int i = int.Parse(Properties.Resources.DFSStartCountdown); i >= 0; i--)
            {
                Log.Information($"DataFormattingService activates in {i} seconds");
                Thread.Sleep(1000);
            }

            if (Context.YENomenclatureItems.Count() > 0)
            {
                foreach (var item in Context.YENomenclatureItems)
                {
                    if (Context.NativeStocks.Where(f => f.ItemName == item.Name).First().Qtty < 1)
                    {
                        Context.YENomenclatureItems.Remove(item);
                    }
                }
            }

            foreach (var item in Context.NativeStocks)
            {
                if (item is not null)
                {
                    if (Context.YENomenclatureItems.Where(f => f.Name == item.ItemName).Count() < 1)
                    {
                        Regex unitSegregator = new Regex("([0-9]{1,4}|([0-9]{1,4},[0-9]{1,2}))[а-я]{1,3}");
                        MatchCollection unparsedUnits = unitSegregator.Matches(item.ItemName);

                        List<string> units = new List<string>();

                        foreach (Match match in unparsedUnits)
                        {
                            if (match.Success)
                            {
                                if (match.Value.Contains(Properties.Resources.Mgr)
                                    || match.Value.Contains(Properties.Resources.Mkg)
                                    || match.Value.Contains(Properties.Resources.Mlt)
                                    || match.Value.Contains(Properties.Resources.Grm))
                                {
                                    units.Add(match.Value);
                                }
                            }
                        }

                        if (units.Count < 1)
                        {
                            units.Add("100мл");
                        }

                        int measure = int.Parse(Regex.Replace(units[0], $"{Properties.Resources.Grm}|{Properties.Resources.Mgr}|{Properties.Resources.Mkg}|{Properties.Resources.Mlt}", string.Empty));

                        var newItem = new Data.YENomenclature.NomenclatureItem()
                        {
                            CategoryId = Context.Categories.First().Id.ToString(),
                            Name = item.ItemName,
                            Description = new Data.YENomenclature.Description()
                            {
                                VendorName = item.Category != null ? item.Category : Properties.Resources.ASNAPurpose,
                                VendorCountry = item.Country != null ? item.Country : Properties.Resources.ASNACountry,
                                Composition = item.Composition != null ? item.Composition : Properties.Resources.ASNAComposition,
                                General = item.ItemDesc,
                                Purpose = Properties.Resources.ASNAPurpose,
                                NutritionalValue = Properties.Resources.ASNANutritionalValue,
                                ExpiresIn = Properties.Resources.ASNAExpiresIn

                            },
                            Images = new System.Collections.Generic.List<Data.YENomenclature.ItemImage>()
                            {
                                new Data.YENomenclature.ItemImage()
                                {
                                    Url = ImageWatcherService.GetImageTinystash(item.ItemId),
                                    Hash = ImageWatcherService.GetImageSha1(item.ItemId)
                                }
                            },
                            IsCatchWeight = false,
                            Barcode = new Barcode()
                            {
                                Type = "ean13",
                                Value = item.Barcode,
                                WeightEncoding = "none"
                            },
                            ExciseValue = "",
                            Labels = new System.Collections.Generic.List<string>() { Properties.Resources.MedicalGoodsString },
                            VendorCode = item.ItemId,
                            Price = (float)item.Price,
                            Vat = 20,
                            Measure = new Measure()
                            {
                                Value = measure,
                                Unit = Regex.IsMatch(units[0], Properties.Resources.Mlt) ? "MLT" : "GRM"
                            }
                        };

                        Context.YENomenclatureItems.Add(newItem);

                        Log.Information($"Formatted native stock {item.ItemName} with NNT {item.ItemId} as YE nomenclature item {newItem.Name}");
                    }
                }
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="contextFactory">Database context to write to. NativeStocks table and Nomenclature cluster are used.</param>
        public DataFormattingService(IDbContextFactory<ASNAOrdersDbContext> contextFactory)
        {
            Context = contextFactory.CreateDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DoWork()
        {
            if (Context.Categories.Count() == 0)
            {
                Context.Categories.Add(new Data.YENomenclature.Category()
                {
                    Name = Properties.Resources.ASNAYECategoryName,
                    Images = new System.Collections.Generic.List<Data.YENomenclature.CategoryImage>
                    {
                        new Data.YENomenclature.CategoryImage()
                        {
                            Url = Properties.Resources.ASNAYECategoryDefaultImageUri,
                            Hash = Properties.Resources.ASNAYECategoryDefaultImageHash
                        }
                    }
                });

                Context.SaveChanges();
            };

            FormatData();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information($"DataFormattingService running at {DateTime.Now}");
            return Task.Run(DoWork);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            DoWork();
            return Task.CompletedTask;
        }
    }
}
