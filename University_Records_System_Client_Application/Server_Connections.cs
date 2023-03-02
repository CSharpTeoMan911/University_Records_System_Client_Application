using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    internal class Server_Connections
    {
        protected static string Server_Ip_Address = "127.0.0.1";
        protected static int port = 1024;

        private static System.Diagnostics.Stopwatch speed_checkup = new System.Diagnostics.Stopwatch();


        private class Payload_Serialisation_and_Deserialisation_Mitigator : Payload_Serialisation_and_Deserialisation
        {
            internal static async Task<byte[]> Serialise_Client_Payload_Initiator<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function)
            {
                return await Serialise_Client_Payload<Password__Or__Binary_Content>(email__or__log_in_session_key, password__or__binary_content, function);
            }

            internal static async Task<Server_WSDL_Payload> Derialise_Server_Payload_Initiator(byte[] payload)
            {
                return await Deserialise_Server_Payload(payload);
            }
        }


        private static bool ValidateServerCertificate( object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            return false;
        }


        protected static async Task<byte[]> Initiate_Server_Connection<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function, bool binary_file)
        {
            byte[] return_value = Encoding.UTF8.GetBytes("FAILED");
            byte[] client_response = Encoding.UTF8.GetBytes("OK");

            byte[] is_binary_file = new byte[1024];

            byte[] server_response = new byte[client_response.Length];


            System.Net.Sockets.Socket client = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);


            client.ReceiveBufferSize = 32768;
            client.SendBufferSize = 32768;

            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;




            try
            {
                client.Connect(Server_Ip_Address, port);




                byte[] serialised_payload = await Payload_Serialisation_and_Deserialisation_Mitigator.Serialise_Client_Payload_Initiator<Password__Or__Binary_Content>(email__or__log_in_session_key, password__or__binary_content, function);
                byte[] serialised_payload_length = BitConverter.GetBytes(serialised_payload.Length);




                System.Net.Sockets.NetworkStream client_stream = new System.Net.Sockets.NetworkStream(client);



                try
                {


                    System.Net.Security.SslStream secure_client_stream = new System.Net.Security.SslStream(client_stream, false, new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                    try
                    {



                        if (Encoding.UTF8.GetString(serialised_payload, 0, serialised_payload.Length) != "PAYLOAD SERIALISATION FAILED")
                        {
                            secure_client_stream.AuthenticateAsClient("University-Student-Records-System");


                            int bytes_per_second = await Rount_Trip_Time_Calculator(secure_client_stream);


                            switch(binary_file)
                            {
                                case true:
                                    is_binary_file = BitConverter.GetBytes(1);
                                    break;

                                case false:
                                    is_binary_file = BitConverter.GetBytes(0);
                                    break;
                            }



                            await Calculate_Connection_Timeout(client, is_binary_file.Length, bytes_per_second);
                            await secure_client_stream.WriteAsync(is_binary_file, 0, is_binary_file.Length);





                            await Calculate_Connection_Timeout(client, server_response.Length, bytes_per_second);
                            await secure_client_stream.ReadAsync(server_response, 0, server_response.Length);





                            await Calculate_Connection_Timeout(client, serialised_payload_length.Length, bytes_per_second);
                            await secure_client_stream.WriteAsync(serialised_payload_length, 0, serialised_payload_length.Length);





                            await Calculate_Connection_Timeout(client, server_response.Length, bytes_per_second);
                            await secure_client_stream.ReadAsync(server_response, 0, server_response.Length);






                            await Calculate_Connection_Timeout(client, serialised_payload.Length, bytes_per_second);

                            switch (binary_file)
                            {
                                case true:
                                    await Task.Run(() =>
                                    {
                                        for (int byte_index = 0; byte_index < serialised_payload.Length; byte_index++)
                                        {
                                            secure_client_stream.WriteByte(serialised_payload[byte_index]);
                                        }
                                    });
                                    break;

                                case false:
                                    await secure_client_stream.WriteAsync(serialised_payload, 0, serialised_payload.Length);
                                    break;
                            }







                            byte[] server_payload_length = new byte[1024];
                            await Calculate_Connection_Timeout(client, server_payload_length.Length, bytes_per_second);
                            await secure_client_stream.ReadAsync(server_payload_length, 0, server_payload_length.Length);





                            await Calculate_Connection_Timeout(client, client_response.Length, bytes_per_second);
                            await secure_client_stream.WriteAsync(client_response, 0, client_response.Length);






                            byte[] server_payload = new byte[BitConverter.ToInt32(server_payload_length, 0)];
                            await Calculate_Connection_Timeout(client, server_payload.Length, bytes_per_second);




                            switch (binary_file)
                            {
                                case true:
                                    await Task.Run(() =>
                                    {
                                        for (int byte_index = 0; byte_index < serialised_payload.Length; byte_index++)
                                        {
                                            server_payload[byte_index] = (byte)secure_client_stream.ReadByte();
                                        }
                                    });
                                    break;

                                case false:
                                    await secure_client_stream.ReadAsync(server_payload, 0, server_payload.Length);
                                    break;
                            }






                            Server_WSDL_Payload deserialised_server_payload = await Payload_Serialisation_and_Deserialisation_Mitigator.Derialise_Server_Payload_Initiator(server_payload);
                            return_value = Encoding.UTF8.GetBytes(deserialised_server_payload.response);
                        }
                    }
                    catch (Exception E)
                    {
                        
                        if (secure_client_stream != null)
                        {
                            secure_client_stream.Close();
                        }
                    }
                    finally
                    {
                        if (secure_client_stream != null)
                        {
                            secure_client_stream.Close();
                            secure_client_stream.Dispose();
                        }
                    }



                }
                catch
                {
                    if(client_stream != null)
                    {
                        client_stream.Close();
                    }
                }
                finally
                {
                    if (client_stream != null)
                    {
                        client_stream.Close();
                        client_stream.Dispose();
                    }
                }


                
            }
            catch
            {
                if(client != null)
                {
                    client.Close();
                }
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                    client.Dispose();
                }
            }
            
            



            return return_value;
        }





        private static async Task<int> Rount_Trip_Time_Calculator(System.Net.Security.SslStream secure_client_stream)
        {
            long time = 0;


            byte[] buffer = new byte[1500];

            int count = 0;


        RTT_Input:

            speed_checkup.Start();
            await secure_client_stream.ReadAsync(buffer, 0, buffer.Length);
            speed_checkup.Stop();


            time += speed_checkup.ElapsedMilliseconds;
            speed_checkup.Reset();


            speed_checkup.Start();
            await secure_client_stream.WriteAsync(buffer, 0, buffer.Length);
            speed_checkup.Stop();


            time += speed_checkup.ElapsedMilliseconds;
            speed_checkup.Reset();

            if (count < 10)
            {
                count++;
                goto RTT_Input;
            }

            if (time < 1)
            {
                time = 1;
            }

            time = 24 * (time / 10) * 250000;

            speed_checkup.Reset();

            if (time < 1)
            {
                time = 1;
            }

            return (int)time;

        }






        private static Task<int> Calculate_Connection_Timeout(System.Net.Sockets.Socket client, int payload_size, int bytes_per_second)
        {
            int seconds = 1000 * (payload_size / bytes_per_second) + 1000;

            if(seconds > 1000)
            {
                client.SendTimeout = seconds;
                client.ReceiveTimeout = seconds;
            }
            else
            {
                client.SendTimeout = 1000;
                client.ReceiveTimeout = 1000;
            }


            return Task.FromResult(0);
        }

    }
}
