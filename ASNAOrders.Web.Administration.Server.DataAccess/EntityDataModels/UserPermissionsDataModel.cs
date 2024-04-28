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

        /// <summary>
        /// Internal identifier of the permission model.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Makes the user able to perform all option operations, as well as enabled to ban other users.
        /// Unsets all lower tiers.
        /// </summary>
        [Column]
        public bool Operator { get; set; }

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

    }
}
