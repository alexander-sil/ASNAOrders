namespace ASNAOrders.Web.Administration.Server.Models
{
    public class AuthenticationResponseErrorsInner
    {
        /// <summary>
        /// Determines the unique identifier of the informational message.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Informational message issued by upstream services.
        /// </summary>
        public string? Message { get; set; }
    }
}
