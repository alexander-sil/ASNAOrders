namespace ASNAOrders.Web.Administration.Server.AbstrModels
{
    /// <summary>
    /// Determines the configuraion of the ASNAOrders.Web API project. Issued by RabbitMQ.
    /// </summary>
    public class IssuibleConfig
    {
        #region SecretGeneratorServiceOptions

        /// <summary>
        /// Determines whether the token issuer signing key is generated automatically.
        /// </summary>
        public bool IssuerSigningKeySetToAuto { get; set; }

        /// <summary>
        /// Determines whether the token issuer signing key in case of manual generation.
        /// </summary>
        public string? IssuerSigningKey { get; set; } = "";

        /// <summary>
        /// Determines whether the signing key storage file is named by the SecretGeneratorService.
        /// </summary>
        public bool SigningKeyFileSetToAuto { get; set; }

        /// <summary>
        /// Determines the OAuth client ID in case of manual generation.
        /// </summary>
        public string? ClientId { get; set; } = "";

        /// <summary>
        /// Determines whether the OAuth client ID is named by the SecretGeneratorService.
        /// </summary>
        public bool ClientIdSetToAuto { get; set; }

        /// <summary>
        /// Determines the key storage file name in case of manual generation.
        /// </summary>
        public string? SigningKeyFile { get; set; } = "";

        #endregion

        #region DatabaseOptions

        /// <summary>
        /// Determines the database connection string to be used.
        /// </summary>
        public string? ConnectionString { get; set; } = "";

        #endregion

        #region LoggingOptions

        /// <summary>
        /// Determines the Serilog logger sink to be used.
        /// Possible values are "console" and "file*[filename]".
        /// </summary>
        public string? SinkSelector { get; set; } = "";

        #endregion
    }
}
