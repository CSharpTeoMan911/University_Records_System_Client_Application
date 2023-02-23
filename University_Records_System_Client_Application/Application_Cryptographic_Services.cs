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
            System.Security.Cryptography.HashAlgorithm content_hasher = System.Security.Cryptography.SHA256Cng.Create();

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
    }
}
