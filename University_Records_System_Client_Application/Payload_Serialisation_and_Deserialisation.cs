using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    class Payload_Serialisation_and_Deserialisation
    {
        private class Application_Cryptographic_Services_Mitigator : Application_Cryptographic_Services
        {
            internal static async Task<byte[]> Content_Hasher_Initiator(string content)
            {
                return await Content_Hasher(content);
            }
        }


        protected static async Task<byte[]> Serialise_Client_Payload<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function)
        {
            byte[] payload_binary_content = Encoding.UTF8.GetBytes("PAYLOAD SERIALISATION FAILED");
            Client_WSDL_Payload payload = new Client_WSDL_Payload();




            payload.email__or__log_in_session_key = Convert.ToBase64String(Encoding.UTF8.GetBytes(email__or__log_in_session_key));




            if (typeof(Password__Or__Binary_Content) == typeof(byte[]))
            {
                payload.password__or__binary_content = Convert.ToBase64String(password__or__binary_content as byte[]);
            }
            else if (typeof(Password__Or__Binary_Content) == typeof(string))
            {
                payload.password__or__binary_content = Convert.ToBase64String(Encoding.UTF8.GetBytes(password__or__binary_content as string));
            }
 

            payload.function = Convert.ToBase64String(Encoding.UTF8.GetBytes(function));




            System.IO.MemoryStream payload_stream = new System.IO.MemoryStream();

            try
            {

                System.Xml.Serialization.XmlSerializer payload_generator = new System.Xml.Serialization.XmlSerializer(payload.GetType());
                payload_generator.Serialize(payload_stream, payload);



                payload_binary_content = payload_stream.ToArray();

                await payload_stream.FlushAsync();
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine("Client payload serialization error: " + E.ToString());
                if(payload_stream != null)
                {
                    payload_stream.Close();
                }
            }
            finally
            {
                if (payload_stream != null)
                {
                    payload_stream.Close();
                    payload_stream.Dispose();
                }
            }

            return payload_binary_content;
        }


        protected static Task<Server_WSDL_Payload> Deserialise_Server_Payload(byte[] payload)
        {
            Server_WSDL_Payload server_payload = new Server_WSDL_Payload();

            System.IO.TextReader payload_stream = new System.IO.StringReader(Encoding.UTF8.GetString(payload));

            try
            {


                System.Xml.Serialization.XmlSerializer payload_deserialiser = new System.Xml.Serialization.XmlSerializer(server_payload.GetType());
                server_payload = (Server_WSDL_Payload)payload_deserialiser?.Deserialize(payload_stream);


                server_payload.response = Encoding.UTF8.GetString(Convert.FromBase64String(server_payload.response));
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine("Server payload deserialization error: " + E.ToString());
                if (payload_stream != null)
                {
                    payload_stream.Close();
                }
            }
            finally
            {
                if (payload_stream != null)
                {
                    payload_stream.Close();
                    payload_stream.Dispose();
                }
            }


            return Task.FromResult(server_payload);
        }



    }
}
