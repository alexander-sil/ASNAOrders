namespace ASNAOrders.Web.Administration.Server
{
    public static class Config
    {
        /// <summary>
        /// Determines whether the token issuer signing key is generated manually.
        /// </summary>
        public static bool IssuerSigningKeySetToManual { get; set; }

        public static string? IssuerSigningKey { get; set; }
    }
}
