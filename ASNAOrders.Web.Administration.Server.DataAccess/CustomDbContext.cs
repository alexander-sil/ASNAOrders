using ASNAOrders.Web.Administration.Server.DataAccess.EntityDataModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASNAOrders.Web.Administration.Server.DataAccess
{
    /// <summary>
    /// DbContext for UserEntityDataModel and UserPermissionsDataModel storage.
    /// </summary>
    public class CustomDbContext : DbContext
    {
        public DbSet<EntityDataModels.UserEntityDataModel> Users { get; set; }

        public DbSet<UserPermissionsDataModel> Permissions { get; set; }

        public CustomDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
