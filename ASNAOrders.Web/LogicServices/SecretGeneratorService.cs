using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using ASNAOrders.Web.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ASNAOrders.Web.ConfigServiceExtensions;
using System.Collections.Generic;
using Serilog;

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
                    File.AppendAllText(path, Convert.ToBase64String(secret));
                } else if (StaticConfig.ClientSecretTransmissionMethod == "file-TEMP")
                {
                    Log.Warning($"VERY IMPORTANT: Preserve your client secret immediately or it will be LOST FOREVER after restart! It is located at {Path.GetTempPath()}");
                    File.AppendAllText(path, Convert.ToBase64String(secret));
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