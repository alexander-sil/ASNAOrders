using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ASNAOrders.Web.Administration.Server.LogicServices
{
    public class SecretGeneratorService
    {

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
    }
}