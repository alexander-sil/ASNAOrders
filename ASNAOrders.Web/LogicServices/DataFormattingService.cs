using ASNAOrders.Web.Data;
using ASNAOrders.Web.Data.Orders;
using Castle.Components.DictionaryAdapter.Xml;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using SQLitePCL;
using System.Linq;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to process (aka format) raw native stock uploads to YE-ready nomenclature format.
    /// </summary>
    public class DataFormattingService
    {
        /// <summary>
        /// 
        /// </summary>
        public ASNAOrdersDbContext Context { get; set; }


        /// <summary>
        /// Constructor for native instantiation. DI use only.
        /// </summary>
        /// <param name="context">Database context to write to. NativeStocks table and Nomenclature cluster are used.</param>
        public DataFormattingService(ASNAOrdersDbContext context)
        {
            Context = context;

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
            }

            Context.SavedChanges += OnSaveChanges;
        }

        private void ReconstituteSavedChangesEvent()
        {
            Context.SavedChanges += OnSaveChanges;
        }

        private void OnSaveChanges(object sender, Microsoft.EntityFrameworkCore.SavedChangesEventArgs e)
        {
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
            }

            foreach (var item in Context.NativeStocks)
            {
                if (item != null)
                {
                    if (Context.YENomenclatureItems.Where(f => f.Name == item.ItemName).Count() == 0)
                    {

                        Context.YENomenclatureItems.Add(new Data.YENomenclature.NomenclatureItem()
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
                            }
                        });
                    }
                }
            }

            Context.SavedChanges -= OnSaveChanges;
            Context.SaveChanges();

            ReconstituteSavedChangesEvent();
        }
    }
}
