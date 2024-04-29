using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ASNAOrders.Web.Administration.Server.AbstrModels
{
    /// <summary>
    /// Determines the configuraion of the this project. Issued via RabbitMQ by ASNAOrders.Web.Administration.Server
    /// </summary>
    [Serializable]
    public class Config
    {

        /// <summary>
        /// Constructor method for Config, StaticConfig and IssuibleConfig service extension model classes.
        /// </summary>
        public Config()
        {
            this.ConnectionString = this.DatabaseType == "sqlite" ? $"Data Source={this.SqliteDbCacheFilename};Cache=Shared" : $"Data Source={this.MssqlServerHost},{this.MssqlServerPort};Initial Catalog={InitialCatalog};User ID={MssqlServerUsername};Password={MssqlServerPassword};TrustServerCertificate=True;App=ASNAOrders";
            this.ErrorLogPrefix = $"hs_log_id{Task.Run(() => { Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL"); return DateTime.Now.ToShortDateString(); }).Result}_";
        }

        #nullable enable
        #region RabbitMQOptions 

            /// <summary>
            /// Determines the RabbitMQ Virtual Host to be used.
            /// </summary>
        public string MQVHost { get; set; } = Properties.Resources.ConfigMQVHost;

        /// <summary>
        /// Determines the hostname of the RabbitMQ instance to be used.
        /// </summary>
        public string MQHostname { get; set; } = Properties.Resources.ConfigMQHostname;

        /// <summary>
        /// Determines the listening port of the RabbitMQ instance to be used.
        /// </summary>
        public ushort MQPort { get; set; } = ushort.Parse(Properties.Resources.ConfigMQPort);

        #endregion

        #region SecretGeneratorServiceOptions

        /// <summary>
        /// Determines whether the token issuer signing key is generated automatically.
        /// </summary>
        public bool IssuerSigningKeySetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the token issuer signing key in case of manual generation.
        /// </summary>
        public string? IssuerSigningKey { get; set; } = "";

        /// <summary>
        /// Determines whether the signing key storage file is named by the SecretGeneratorService.
        /// </summary>
        public bool SigningKeyFileSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the OAuth client ID in case of manual generation.
        /// </summary>
        public string? ClientId { get; set; } = "";

        /// <summary>
        /// Determines whether the OAuth client ID is named by the SecretGeneratorService.
        /// </summary>
        public bool ClientIdSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the key storage file name in case of manual generation.
        /// </summary>
        public string? SigningKeyFile { get; set; } = Properties.Resources.ConfigSigningKeyFile;


        /// <summary>
        /// Determines whether the client secret storage file name is generated programmatically.
        /// </summary>
        public bool ClientSecretSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the client secret data in case of manual generation.
        /// Must be a valid Base64 string.
        /// </summary>
        public string? ClientSecret { get; set; } = "";

        /// <summary>
        /// Determines whether the client secret storage file name is generated programmatically.
        /// </summary>
        public bool ClientSecretFilenameSetToAuto { get; set; } = true;

        /// <summary>
        /// Determines the client secret storage file name in case of manual generation.
        /// </summary>
        public string? ClientSecretFilename { get; set; } = Properties.Resources.ConfigClientSecretFilename;


        #endregion

        #region DatabaseOptions

        /// <summary>
        /// Determines the type of the database to be used by the server.
        /// Possible values are "sqlite" and "mssqlserver".
        /// </summary>
        public string? DatabaseType { get; set; } = Properties.Resources.ConfigDatabaseType;

        #region TypeSpecificDBOpts

        /// <summary>
        /// Determines the directory for XML-formatted stock uploads.
        /// By default, the directory is located in the user's profile directory.
        /// </summary>
        public string? XMLStockPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Properties.Resources.ConfigXMLStockPath);

        /// <summary>
        /// Determines the filename of the database cache to be used in case of SQLite.
        /// A ".db" extension is appended programmatically.
        /// </summary>
        public string? SqliteDbCacheFilename { get; set; } = Properties.Resources.ConfigSqliteDbCacheFilename;

        /// <summary>
        /// Determines the username to be used in case of Microsoft SQL Server.
        /// Native Windows authentication is NOT supported.
        /// </summary>

        /// <summary>
        /// Determines the hostname to be used in case of Microsoft SQL Server.
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public string? MssqlServerHost { get; set; } = Properties.Resources.ConfigMssqlServerHost;

        /// <summary>
        /// Determines the server port to be used in case of Microsoft SQL Server.
        /// By default, this property contains 1433.
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public ushort MssqlServerPort { get; set; } = ushort.Parse(Properties.Resources.ConfigMssqlServerPort);

        /// <summary>
        /// Determines the username to be used in case of Microsoft SQL Server.
        /// By default, this property contains an "sa".
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public string? MssqlServerUsername { get; set; } = Properties.Resources.ConfigMssqlUsername;

        /// <summary>
        /// Determines the password to be used in case of Microsoft SQL Server.
        /// By default, this property contains an empty string.
        /// Native Windows authentication is NOT supported.
        /// </summary>
        public string? MssqlServerPassword { get; set; } = "";

        /// <summary>
        /// Determines the database connection string to be used.
        /// </summary>
        public string? ConnectionString { get; set; } = "";

        /// <summary>
        /// Determines the initial catalog database to be used.
        /// </summary>
        public string? InitialCatalog { get; set; } = "";

        #endregion

        #endregion

        #region LoggingOptions

        /// <summary>
        /// Determines the Serilog logger sink to be used.
        /// Possible values are "eventlog", "eventlog+mail*[address]" and "file*[filename]".
        /// </summary>
        public string? Sink { get; set; } = Properties.Resources.ConfigSerilogSink;

        /// <summary>
        /// Determines the prefix for error log files.
        /// By default, this property contains the current date of the system.
        /// </summary>
        public string? ErrorLogPrefix { get; set; } = $"";

        #region MailLoggingOptions

        /// <summary>
        /// Determines the password to be used in case of mail logging.
        /// By default, this property contains an empty string.
        /// </summary>
        public string? MailPassword { get; set; } = "";

        /// <summary>
        /// Determines the email address for logging messages to be sent to.
        /// By default, this property contains an empty string.
        /// </summary>
        public string? MailTo { get; set; } = "";

        /// <summary>
        /// Determines the SMTP server to be used in case of mail logging.
        /// By default, this property contains localhost.
        /// </summary>
        public string? MailHost { get; set; } = Properties.Resources.ConfigMailHost;

        /// <summary>
        /// Determines the SMTP server port to be used in case of mail logging.
        /// By default, this property contains 25.
        /// </summary>
        public ushort MailPort { get; set; } = ushort.Parse(Properties.Resources.ConfigMailPort);

        /// <summary>
        /// Determines the SMTP Secure Sockets Layer (SSL) options to be used in case of mail logging.
        /// By default, this property contains "auto".
        /// Possible values are "none", "auto", "SSL" and "STARTTLSavail".
        /// </summary>
        public string? MailSSLOptions { get; set; } = Properties.Resources.ConfigMailSSLOptions;

        #endregion

        #region SecretTransmissionOptions

        /// <summary>
        /// Determines how the OAuth2 client secret should be transferred to the server operator.
        /// Possible values are "file-INSECURE", "file-TEMP" and "email". 
        /// By default, this property contains "file-TEMP".
        /// </summary>
        public string ClientSecretTransmissionMethod { get; set; } = Properties.Resources.ConfigClientSecretTransmissionMethod;

        #endregion

        #endregion

    }
}
