namespace ASNAOrders.Web.Administration.Server.Models
{
    public class AuthenticationResponseErrorsInner
    {
        /// <summary>
        /// Informational messages issued by controller.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Informational message issued by upstream services.
        /// </summary>
        public string Message { get; set; }
    }
}
