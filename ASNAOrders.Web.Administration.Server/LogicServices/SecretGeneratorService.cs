using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ASNAOrders.Web.Administration.Server.LogicServices
{
    public class SecretGeneratorService
    {
        public static string GetClientSecretHash(string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                RandomNumberGenerator rng = new RNGCryptoServiceProvider();

                byte[] secret = new byte[32];
                rng.GetBytes(secret);

                string strScrt = Convert.ToHexString(SHA256.Create().ComputeHash(secret));
                File.Create(path).Dispose();

                File.AppendAllText(path, strScrt + Environment.NewLine);
                File.AppendAllText(path, Convert.ToBase64String(secret));

                return strScrt;
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

        public static byte[] GetIssuerSigningKey(string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                RandomNumberGenerator rng = new RNGCryptoServiceProvider();

                byte[] key = new byte[64];
                rng.GetBytes(key);

                string encKey = Convert.ToHexString(ProtectedData.Protect(key, null, DataProtectionScope.CurrentUser));

                File.Create(path).Dispose();
                File.AppendAllText(path, encKey);
            }

            return ProtectedData.Unprotect(Convert.FromHexString(File.ReadAllText(path)), null, DataProtectionScope.CurrentUser);
        }

        public static string GetClientId()
        {
            return "ye-integration";
        }
    }
}