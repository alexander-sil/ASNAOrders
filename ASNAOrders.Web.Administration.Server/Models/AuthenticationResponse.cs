
using Microsoft.AspNetCore.Mvc;

namespace ASNAOrders.Web.Administration.Server.Models
{
    public class AuthenticationResponse
    {
        /// <summary>
        /// Determines result of authentication operation attempt.
        /// If false, the operation has exited with a failure.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Determines result of authentication operation attempt.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Informational messages issued by controller.
        /// </summary>
        public AuthenticationResponseErrorsInner? Information { get; set; }
    }
}

