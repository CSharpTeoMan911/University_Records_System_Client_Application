using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    class Application_Cryptographic_Services
    {
        protected static Task<byte[]> Content_Hasher(string content)
        {
            byte[] Content_Hashing_Result = new byte[] { };
            System.Security.Cryptography.HashAlgorithm content_hasher = System.Security.Cryptography.SHA256.Create();

            try
            {
                Content_Hashing_Result = content_hasher.ComputeHash(Encoding.UTF8.GetBytes(content));
            }
            catch { }
            finally
            {
                if(content_hasher != null)
                {
                    content_hasher.Dispose();
                }
            }

            return Task.FromResult(Content_Hashing_Result);
        }


        protected static Task<bool> Load_X509_Certificate_Into_Store(byte[] certificate_binary_data, string certificate_password)
        {
            System.Security.Cryptography.X509Certificates.X509Certificate2 server_certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2();

            try
            {
                server_certificate.Import(certificate_binary_data, certificate_password, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.DefaultKeySet);

                System.Security.Cryptography.X509Certificates.X509Store certificate_store = new System.Security.Cryptography.X509Certificates.X509Store(System.Security.Cryptography.X509Certificates.StoreName.Root, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine);

                try
                {
                    certificate_store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadWrite);
                    certificate_store.Add(server_certificate);
                }
                catch
                {
                    
                    if(certificate_store != null)
                    {
                        certificate_store.Close();
                    }
                }
                finally
                {
                    if (certificate_store != null)
                    {
                        certificate_store.Close();
                        certificate_store.Dispose();
                    }
                }
            }
            catch
            {
               
            }
            finally
            {
                if(server_certificate != null)
                {
                    server_certificate.Dispose();
                }
            }

            return Task.FromResult(true);
        }


    }
}
