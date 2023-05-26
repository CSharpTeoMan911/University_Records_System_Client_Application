using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    internal class Server_Connections : Payload_Serialisation_and_Deserialisation
    {
      
        private static System.Diagnostics.Stopwatch speed_checkup = new System.Diagnostics.Stopwatch();





        private static bool ValidateServerCertificate( object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, 
                                                       System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // IF THE CERTIFICATE MATCHES ANY TRUSTED ROOT CERTIFICATE AUTHORITY
            // WITHIN THE DEVICE'S CERTIFICATE STORE, VALIDATE THE CERTIFICATE

            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            return false;
        }



        internal static async Task<byte[]> Initiate_Server_Connection<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, Functions function)
        {
            byte[] return_value = Encoding.UTF8.GetBytes("Connection error");


            byte[] client_response = Encoding.UTF8.GetBytes("OK");
            byte[] server_response = new byte[client_response.Length];


            System.Net.Sockets.Socket client = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);


            client.ReceiveBufferSize = 32768;
            client.SendBufferSize = 32768;

            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;




            try
            {
                client.Connect(endpoint_ip_address, endpoint_port);


                string Function = String.Empty;
                Function_string.TryGetValue(function, out Function);


                byte[] serialised_payload = await Serialise_Client_Payload<Password__Or__Binary_Content>(email__or__log_in_session_key, password__or__binary_content, Function);
                byte[] serialised_payload_length = BitConverter.GetBytes(serialised_payload.Length);




                System.Net.Sockets.NetworkStream client_stream = new System.Net.Sockets.NetworkStream(client);



                try
                {


                    System.Net.Security.SslStream secure_client_stream = new System.Net.Security.SslStream(client_stream, false, new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                    try
                    {



                        if (Encoding.UTF8.GetString(serialised_payload, 0, serialised_payload.Length) != "PAYLOAD SERIALISATION FAILED")
                        {
                            secure_client_stream.AuthenticateAsClient("Student_Records_System", null, System.Security.Authentication.SslProtocols.Tls12, false);


                            int bytes_per_second = await Rount_Trip_Time_Calculator(secure_client_stream);




                            await Calculate_Connection_Timeout(secure_client_stream, serialised_payload_length.Length, bytes_per_second);
                            await secure_client_stream.WriteAsync(serialised_payload_length, 0, serialised_payload_length.Length);





                            await Calculate_Connection_Timeout(secure_client_stream, server_response.Length, bytes_per_second);
                            await secure_client_stream.ReadAsync(server_response, 0, server_response.Length);






                            await Calculate_Connection_Timeout(secure_client_stream, serialised_payload.Length, bytes_per_second);
                            await secure_client_stream.WriteAsync(serialised_payload, 0, serialised_payload.Length);







                            byte[] server_payload_length = new byte[1024];
                            await Calculate_Connection_Timeout(secure_client_stream, server_payload_length.Length, bytes_per_second);
                            await secure_client_stream.ReadAsync(server_payload_length, 0, server_payload_length.Length);





                            await Calculate_Connection_Timeout(secure_client_stream, client_response.Length, bytes_per_second);
                            await secure_client_stream.WriteAsync(client_response, 0, client_response.Length);






                            byte[] server_payload = new byte[BitConverter.ToInt32(server_payload_length, 0)];
                            await Calculate_Connection_Timeout(secure_client_stream, server_payload.Length, bytes_per_second);

                            int total_bytes_read = 0;

                            while (total_bytes_read < server_payload.Length)
                            {
                                total_bytes_read += await secure_client_stream.ReadAsync(server_payload, total_bytes_read, server_payload.Length - total_bytes_read);
                            }





                            Server_WSDL_Payload deserialised_server_payload = await Deserialise_Server_Payload(server_payload);
                            return_value = Encoding.UTF8.GetBytes(deserialised_server_payload.response);
                        }
                    }
                    catch (Exception E)
                    {
                        System.Diagnostics.Debug.WriteLine(E.Message);
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
                catch (Exception E)
                {
                    System.Diagnostics.Debug.WriteLine(E.Message);
                    if (client_stream != null)
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
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.Message);
                if (client != null)
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





        // CALCULATOR THAT CALCULATES THE TIME TOOK TO TRANSMIT AND RECEIVE A PACKET OF A SET SIZE OVER THE MAIN CONNECTION.
        // THE PACKET TRANSMISSION PROCEDURE IS REPEATED 10 TIMES OVER THE CONNECTION AND THE MEDIAN VALUE OF THE TIME
        // IS CALCULATED. THIS MUST BE DONE IN ORDER TO ADJUST THE MAIN CONNECTION'S SOCKET RECEIVE AND SEND TIMEOUT
        // IN ACCORDACE WITH THE SIZE OF THE PACKET TO BE RECEIVED OR SENT RESPECTIVELY.
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






        // CALCULATE THE MAIN CONNECTION'S SEND OR RECEIVE TIMEOUT BASED ON THE RTT CALCULATED VALUE
        // IN ACCORDANCE WITH THE PACKET SIZE TO BE SENT OR RECEIVED.
        /*



         */

        private static Task<int> Calculate_Connection_Timeout(System.Net.Security.SslStream secure_client_stream, int payload_size, int bytes_per_second)
        {
            int seconds = 1000 * (payload_size / bytes_per_second) + 1000;

            if (seconds > 1000)
            {
                secure_client_stream.WriteTimeout = seconds;
                secure_client_stream.ReadTimeout = seconds;
            }
            else
            {
                secure_client_stream.WriteTimeout = 1000;
                secure_client_stream.ReadTimeout = 1000;
            }


            return Task.FromResult(0);
        }

    }
}
