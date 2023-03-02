using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    class Application_Cryptographic_Services : Client_Variables
    {

        // PRIVATE SEALED CLASSES THAT ACCESS SENSITIVE INFORMATION.
        //
        // THESE CLASSES INTERACT WITH THE SENSITIVE METHODS USING
        // INTERNAL METHODS, MENING THAT THESE METHODS CAN BE
        // ACCESSED ONLY IN THE MAIN CLASS THAT CONTAINS THE
        // SEALED CLASS.
        //
        //
        //
        // [ BEGIN ]

        // [ END ]







        // CREATE A HASH FROM A STRING INPUT USING THE SHA256 ALGORITHM
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









        // LOAD X509 SERVER CERTIFICATE INTO THE DEVICE'S CERTIFICATE STORE
        //
        // THIS IS DONE BECAUSE THE SERVER APPLICATION IS CREATING SELF SIGNED CERTIFICATES.
        // IN ORDER TO COMMUNICATE WITH THE SET SERVER APPLICATION ALL THE CLIENTS THAT 
        // COMMUNICATE WITH THAT SPECIFIC SERVER APPLICATION MUST LOAD ITS SSL CERTIFICATE
        // INTO THE DEVICE'S CERTIFICATE STORE TO COMMUNICATE WITH THE SERVER OVER THE
        // TLS 1.1 SECURE SOCKET LAYER PROTOCOL.

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







        // SAVE THE LOG IN SESSION KEY AND EMAIL INSIDE THE APPLICATION.
        // IF USER WANTS TO BE KEPT LOGGED IN, SAVE THE EMAIL AND THE
        // LOG IN SESSION KEY INSIDE A JSON CACHE FILE IN THE 
        // APPLICATION'S DIRECTORY

        protected static async Task<bool> Save_Log_In_Key(string Email, string log_in_code, bool keep_user_logged_in)
        {
            email = Email;
            log_in_session_key = log_in_code;

            Log_In_Key_Cache log_In_Key_Cache = new Log_In_Key_Cache();
            log_In_Key_Cache.email = email;
            log_In_Key_Cache.log_in_session_key = log_in_session_key;



            if(keep_user_logged_in == true)
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

                try
                {
                    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("user_cache.json");

                    try
                    {
                        Newtonsoft.Json.JsonWriter json_writer = new Newtonsoft.Json.JsonTextWriter(streamWriter);

                        try
                        {
                            serializer.Serialize(json_writer, log_In_Key_Cache);
                            await json_writer.FlushAsync();
                        }
                        catch
                        {
                            if (json_writer != null)
                            {
                                await json_writer.CloseAsync();
                            }
                        }

                        streamWriter.Flush();
                    }
                    catch
                    {
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                        }
                    }
                    finally
                    {
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                            streamWriter.Dispose();
                        }
                    }
                }
                catch
                {

                }
            }


            return true;
        }








        // IF A CACHE FILE EXISTS IN THE APPLICATION'S DIRECTORY
        // LOAD THE EMAIL AND LOG IN SESSION KEY IN THE 
        // APPLICATION

        protected static Task<bool> Load_Log_In_Session_Key()
        {
            bool log_in_session_result = false;


            if(System.IO.File.Exists("user_cache.json") == true)
            {

                Newtonsoft.Json.JsonSerializer deserializer = new Newtonsoft.Json.JsonSerializer();

                try
                {
                    System.IO.StreamReader streamReader = new System.IO.StreamReader("user_cache.json");

                    try
                    {
                        Newtonsoft.Json.JsonReader json_reader = new Newtonsoft.Json.JsonTextReader(streamReader);

                        try
                        {
                            Log_In_Key_Cache log_In_Key_Cache = (Log_In_Key_Cache)deserializer.Deserialize(json_reader, typeof(Log_In_Key_Cache));

                            email = log_In_Key_Cache.email;
                            log_in_session_key = log_In_Key_Cache.log_in_session_key;
                           
                            log_in_session_result = true;
                        }
                        catch(Exception E)
                        {
                            System.Diagnostics.Debug.WriteLine(E.ToString());
                        }
                    }
                    catch
                    {
                        if (streamReader != null)
                        {
                            streamReader.Close();
                        }
                    }
                    finally
                    {
                        if (streamReader != null)
                        {
                            streamReader.Close();
                            streamReader.Dispose();
                        }
                    }
                }
                catch
                {
                    
                }

            }

            return Task.FromResult(log_in_session_result);
        }



    }
}
