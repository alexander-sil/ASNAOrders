using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using ASNAOrders.Web.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ASNAOrders.Web.ConfigServiceExtensions;
using System.Collections.Generic;
using System.Net.Mail;
using Serilog;
using System.Reflection;
using MailKit;
using System.Net;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to generate and store private cryptographic credentials.
    /// </summary>
    public class SecretGeneratorService
    {
        public static string GetClientSecretHash(string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                RandomNumberGenerator rng = new RNGCryptoServiceProvider();

                byte[] secret = new byte[32];
                rng.GetBytes(secret);

                string hash = Convert.ToHexString(SHA256.Create().ComputeHash(secret));
                File.Create(path).Dispose();

                File.AppendAllText(path, hash + Environment.NewLine);

                if (StaticConfig.ClientSecretTransmissionMethod == "file-INSECURE")
                {
                    Log.Warning($"VERY IMPORTANT: A client secret has been written to the same location as its hash. This option is VERY insecure and should NEVER be used.");
                    File.AppendAllText(path, Convert.ToBase64String(secret));
                } else if (StaticConfig.ClientSecretTransmissionMethod == "file-TEMP")
                {
                    Log.Warning($"VERY IMPORTANT: Preserve your client secret immediately or it will be LOST FOREVER after restart! It is located at {Path.GetTempPath()}");
                    File.AppendAllText(Path.Combine(Path.GetTempPath(), path), Convert.ToBase64String(secret));
                } else
                {
                    using MailMessage message = new MailMessage()
                    {
                        Subject = "OAuth 2 Client Secret transferred",
                        From = new MailAddress(StaticConfig.Sink.Split("*")[1]),
                        Body = $"This is your OAuth2 client secret for logging on to the {Assembly.GetExecutingAssembly().GetName().Name} application.{Environment.NewLine}It is very important you store the below string in a secure place.{Environment.NewLine}{Environment.NewLine}{Convert.ToBase64String(secret)}{Environment.NewLine}{Environment.NewLine}In case this secret becomes lost, you WILL NOT be able to log on to the service specified."
                    };

                    message.To.Add(StaticConfig.MailTo);

                    using SmtpClient client = new SmtpClient(StaticConfig.MailHost, StaticConfig.MailPort);

                    client.Credentials = new NetworkCredential(StaticConfig.Sink.Split("*")[1], StaticConfig.MailPassword);
                    client.EnableSsl = StaticConfig.MailSSLOptions == "STARTTLSavail" ? true : StaticConfig.MailSSLOptions == "none" ? false : true;

                    client.Send(message);
                }
               
                return hash;
            }

            try
            {
                return File.ReadAllLines(path)[0].Trim();
            }
            catch (IndexOutOfRangeException)
            {
                return "[ERRORERRORERRORERRORERRORERROR]";
            }
        }

        /// <summary>
        /// Gets a mutable token signing key from a file within the specified path.
        /// </summary>
        /// <param name="path">The file path to be used.</param>
        /// <returns>The token issuer signing key retrieved.</returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static byte[] GetIssuerSigningKey(string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                RandomNumberGenerator rng = new RNGCryptoServiceProvider();

                byte[] key;

                if (StaticConfig.IssuerSigningKeySetToAuto == true)
                {
                    key = new byte[64];
                    rng.GetBytes(key);
                } else
                {
                    key = Encoding.Latin1.GetBytes(StaticConfig.IssuerSigningKey);
                }
                

                string encKey = Convert.ToHexString(ProtectedData.Protect(key, null, DataProtectionScope.CurrentUser));

                File.Create(path).Dispose();
                File.AppendAllText(path, encKey);
            }

            return ProtectedData.Unprotect(Convert.FromHexString(File.ReadAllText(path)), null, DataProtectionScope.CurrentUser);
        }

        /// <summary>
        /// Static method to issue a select OAuth client identifier, based on current configuration options.
        /// </summary>
        /// <returns>The </returns>
        public static string GetClientId()
        {
            return StaticConfig.ClientIdSetToAuto ? "ye-integration" : StaticConfig.ClientId;
        }
    }
}