using System.Security.Cryptography.X509Certificates;

namespace PnPCore
{
    public static class Utilities
    {
        public static X509Certificate2 LoadCertificate(string keyVaultCertificate, string certificateThumbprint)
        {
            Console.WriteLine("Loading certificate.");
            // Will only be populated correctly when running in the Azure Function host
            string certBase64Encoded = keyVaultCertificate;

            if (!string.IsNullOrEmpty(certBase64Encoded))
            {
                return CloudFlow(certBase64Encoded);
            }
            else
            {
                return LocalFlow(certificateThumbprint);
            }
        }

        private static X509Certificate2 LocalFlow(string thumbprint)
        {
            Console.WriteLine("Using local flow.");
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            store.Close();

            return certificateCollection.First();
        }

        private static X509Certificate2 CloudFlow(string certBase64Encoded)
        {
            Console.WriteLine($"Using Azure Function flow. '{certBase64Encoded}'");
            // Azure Function flow
            return new X509Certificate2(Convert.FromBase64String(certBase64Encoded),
                                        "",
                                        X509KeyStorageFlags.Exportable |
                                        X509KeyStorageFlags.MachineKeySet |
                                        X509KeyStorageFlags.EphemeralKeySet);
        }
    }
}
