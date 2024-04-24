using System;
using System.Globalization;

namespace ASNAOrders.Web.ConfigServiceExtensions
{
    /// <summary>
    /// Configuration class into which Config.cs contents are loaded upon application start.
    /// </summary>
    public static class StaticConfig
    {
        /// <summary>
        /// (Utility method) Load a dynamic configuration (Config.cs) from a designated location.
        /// </summary>
        /// <param name="config">The dynamic configuration to be loaded.</param>
        public static void Load(Config config)
        {
            MQVHost = config.MQVHost;
            MQHostname = config.MQHostname;
            MQPort = config.MQPort;


            IssuerSigningKeySetToAuto = config.IssuerSigningKeySetToAuto;
            IssuerSigningKey = config.IssuerSigningKey;
            SigningKeyFile = config.SigningKeyFile;
            SigningKeyFileSetToAuto = config.SigningKeyFileSetToAuto;
            ClientId = config.ClientId;
            ClientIdSetToAuto = config.ClientIdSetToAuto;

            ClientSecretFilenameSetToAuto = config.ClientSecretFilenameSetToAuto;
            ClientSecretFilename = config.ClientSecretFilename;
            ClientSecretSetToAuto = config.ClientSecretSetToAuto;
            ClientSecret = config.ClientSecret;


            DatabaseType = config.DatabaseType;
            InitialCatalog = config.InitialCatalog;

            SqliteDbCacheFilename = config.SqliteDbCacheFilename;
            MssqlServerHost = config.MssqlServerHost;
            MssqlServerPort = config.MssqlServerPort;
            MssqlServerUsername = config.MssqlServerUsername;
            MssqlServerPassword = config.MssqlServerPassword;

            ConnectionString = config.ConnectionString;


            Sink = config.Sink;
            ErrorLogPrefix = config.ErrorLogPrefix;

            MailPassword = config.MailPassword;
            MailTo = config.MailTo;
            MailHost = config.MailHost;
            MailPort = config.MailPort;
            MailSSLOptions = config.MailSSLOptions;


            ClientSecretTransmissionMethod = config.ClientSecretTransmissionMethod;
        }

        #nullable enable
        #region RabbitMQOptions 

        /// <summary>
        /// Determines the RabbitMQ Virtual Host to be used.
        /// </summary>
        public static string MQVHost { get; set; } = "asna-orders";

        /// <summary>
        /// Determines the hostname of the RabbitMQ instance to be used.
        /// </summary>
        public static string MQHostname { get; set; } = "localhost";

        /// <summary>
        /// Determines the listening port of the RabbitMQ instance to be used.
        /// </summary>
        public static ushort MQPort { get; set; } = 5672;

        #endregion

        #region SecretGeneratorServiceOptions

        /// <summary>
        /// Determines whether the token issuer signing key is generated automatically.
        /// </summary>
        public static bool IssuerSigningKeySetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the token issuer signing key in case of manual generation.
        /// </summary>
        public static string? IssuerSigningKey { get; set; } = "";

        /// <summary>
        /// Determines the key storage file name in case of manual generation.
        /// </summary>
        public static string? SigningKeyFile { get; set; } = "skey";

        /// <summary>
        /// Determines whether the signing key storage file is named by the SecretGeneratorService.
        /// </summary>
        public static bool SigningKeyFileSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the OAuth client ID in case of manual generation.
        /// </summary>
        public static string? ClientId { get; set; } = "";

        /// <summary>
        /// Determines whether the OAuth client ID is named by the SecretGeneratorService.
        /// </summary>
        public static bool ClientIdSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines whether the client secret storage file name is generated programmatically.
        /// </summary>
        public static bool ClientSecretSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the client secret data in case of manual generation.
        /// Must be a valid Base64 string.
        /// </summary>
        public static string? ClientSecret { get; set; } = "";

        /// <summary>
        /// Determines whether the client secret storage file name is generated programmatically.
        /// </summary>
        public static bool ClientSecretFilenameSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the client secret storage file name in case of manual generation.
        /// </summary>
        public static string? ClientSecretFilename { get; set; } = "client-secret-hash";


        #endregion

        #region DatabaseOptions

        /// <summary>
        /// Determines the type of the database to be used by the server.
        /// Possible values are "sqlite" and "mssqlserver".
        /// </summary>
        public static string? DatabaseType { get; set; } = "sqlite";

        #region TypeSpecificDBOpts

        /// <summary>
        /// Determines the filename of the database cache to be used in case of SQLite.
        /// A ".db" extension is appended programmatically.
        /// </summary>
        public static string? SqliteDbCacheFilename { get; set; } = "App";

        /// <summary>
        /// Determines the username to be used in case of Microsoft SQL Server.
        /// Native Windows authentication is NOT supported.
        /// </summary>

        /// <summary>
        /// Determines the hostname to be used in case of Microsoft SQL Server.
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public static string? MssqlServerHost { get; set; } = "localhost";

        /// <summary>
        /// Determines the server port to be used in case of Microsoft SQL Server.
        /// By default, this property contains 1433.
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public static ushort MssqlServerPort { get; set; } = 1433;

        /// <summary>
        /// Determines the username to be used in case of Microsoft SQL Server.
        /// By default, this property contains an "sa".
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public static string? MssqlServerUsername { get; set; } = "sa";

        /// <summary>
        /// Determines the password to be used in case of Microsoft SQL Server.
        /// By default, this property contains an empty string.
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public static string? MssqlServerPassword { get; set; } = "";

        #endregion

        /// <summary>
        /// Determines the database connection string to be used.
        /// </summary>
        public static string? ConnectionString { get; set; } = "";

        /// <summary>
        /// Determines the initial catalog database to be used.
        /// </summary>
        public static string? InitialCatalog { get; set; } = "";

        #endregion

        #region LoggingOptions

        /// <summary>
        /// Determines the Serilog logger sink to be used.
        /// Possible values are "eventlog", "eventlog+mail*[address|login]" and "file*[filename]".
        /// </summary>
        public static string? Sink { get; set; } = "eventlog";

        /// <summary>
        /// Determines the prefix for error log files.
        /// By default, this property contains the current date of the system.
        /// </summary>
        public static string? ErrorLogPrefix { get; set; } = "";

        #region MailLoggingOptions

        /// <summary>
        /// Determines the password to be used in case of mail logging.
        /// By default, this property contains an empty string.
        /// </summary>
        public static string? MailPassword { get; set; } = "";

        /// <summary>
        /// Determines the email address for logging messages to be sent to.
        /// By default, this property contains an empty string.
        /// </summary>
        public static string? MailTo { get; set; } = "";
        /// <summary>
        /// Determines the SMTP server to be used in case of mail logging.
        /// By default, this property contains 127.0.0.1.
        /// </summary>
        public static string? MailHost { get; set; } = "127.0.0.1";

        /// <summary>
        /// Determines the SMTP server port to be used in case of mail logging.
        /// By default, this property contains 25.
        /// </summary>
        public static ushort MailPort { get; set; } = 25;

        /// <summary>
        /// Determines the SMTP Secure Sockets Layer (SSL) options to be used in case of mail logging.
        /// By default, this property contains "auto".
        /// Possible values are "auto", "none" and "STARTTLSavail".
        /// </summary>
        public static string? MailSSLOptions { get; set; } = "auto";

        #endregion

        #region SecretTransmissionOptions

        /// <summary>
        /// Determines how the OAuth2 client secret should be transferred to the server operator.
        /// Possible values are "file-INSECURE", "file-TEMP" and "email". 
        /// By default, this property contains "file-TEMP".
        /// </summary>
        public static string ClientSecretTransmissionMethod { get; set; } = "file-TEMP";

        #endregion

        #endregion

    }
}
