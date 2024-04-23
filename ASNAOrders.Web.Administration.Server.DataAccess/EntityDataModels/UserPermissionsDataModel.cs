using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Web.Administration.Server.DataAccess.EntityDataModels
{
    /// <summary>
    /// Permissions issued upon registration. For native client usage only.
    /// </summary>
    [Table("Permissions")]
    public class UserPermissionsDataModel
    {
        #region Special

        /// <summary>
        /// Internal identifier of the permission model.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Determines the user the specified permissions are bound to. For service use only.
        /// Does not unset permissions.
        /// </summary>
        [Column]
        public UserEntityDataModel? UserBinding { get; set; }

        /// <summary>
        /// Makes the user able to perform any operation whilst not being subject to ban/kick.
        /// Unsets all lower tiers.
        /// </summary>
        [Column]
        public bool Operator { get; set; }

        #endregion

        #region UserPermissions

        /// <summary>
        /// Makes the user able to ban and kick others whilst not being subject to ban/kick.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool UsersBanAndKickInvincible { get; set; }

        /// <summary>
        /// Makes the user able to ban and kick others.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool UsersBanAndKick { get; set; }

        /// <summary>
        /// Makes the user able to kick others.
        /// </summary>
        [Column]
        public bool UsersKick { get; set; }

        #endregion

        #region DatabasePermissions 

        /// <summary>
        /// Makes the user able to ban and kick others whilst not being subject to self-ban/kick.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool DatabaseReadUpdateDelete { get; set; }

        /// <summary>
        /// Makes the user able to ban and kick others.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool DatabaseReadUpdate { get; set; }

        /// <summary>
        /// Makes the user able to kick others.
        /// </summary>
        [Column]
        public bool DatabaseRead { get; set; }

        #endregion

        #region OrderPermissions 

        /// <summary>
        /// Makes the user able to view and/or clear order listings.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool OrdersViewClear { get; set; }

        /// <summary>
        /// Makes the user able to view order listings.
        /// </summary>
        [Column]
        public bool OrdersView { get; set; }

        #endregion

        #region StockPermissions

        /// <summary>
        /// Makes the user able to view, edit and clear stock listings.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool StocksViewEditClear { get; set; }

        /// <summary>
        /// Makes the user able to view/edit stock listings.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool StocksViewEdit { get; set; }

        /// <summary>
        /// Makes the user able to view stock listings.
        /// </summary>
        [Column]
        public bool StocksView { get; set; }

        #endregion

        #region OptionPermissions

        /// <summary>
        /// Makes the user able to view/edit options.
        /// Unsets lower tier.
        /// </summary>
        [Column]
        public bool OptionsViewEdit { get; set; }

        /// <summary>
        /// Makes the user able to view options.
        /// </summary>
        [Column]
        public bool OptionsView { get; set; }

        #endregion
    }
}
