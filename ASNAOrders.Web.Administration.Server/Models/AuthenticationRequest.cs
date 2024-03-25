namespace ASNAOrders.Web.Administration.Server.Models
{
    public class AuthenticationRequest
    {
        /// <summary>
        /// User name set during registration.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password set during user registration.
        /// </summary>
        public string Password { get; set; }
    }
}
