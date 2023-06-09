using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static University_Records_System_Client_Application.Client_Variables;

namespace University_Records_System_Client_Application
{
    internal class Server_Connections : Payload_Serialisation_and_Deserialisation
    {
      
        private static System.Diagnostics.Stopwatch speed_checkup = new System.Diagnostics.Stopwatch();
        private static readonly byte[] client_response = Encoding.UTF8.GetBytes("OK");





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
            // "Function" OBJECT THAT HOLDS THE VALUE OF THE FUNCTION TO BE EXECUTED 
            string Function = String.Empty;

            // GET THE VALUE ASSOCIATED WITH THE ENUM VALUE OF THE FUNCTION GIVEN AS A PARAMETER FROM A DICTIONARY
            Function_string.TryGetValue(function, out Function);

            // "serialised_payload" BUFFER THAT STORES THE SERIALISED WSDL PAYLOAD
            byte[] serialised_payload = await Serialise_Client_Payload<Password__Or__Binary_Content>(email__or__log_in_session_key, password__or__binary_content, Function);

            // "serialised_payload_length" BUFFER THAT STORES THE LENGTH OF THE "serialised_payload" BUFFER
            byte[] serialised_payload_length = BitConverter.GetBytes(serialised_payload.Length);




            // PRESET RETURN VALUE OF THE METHOD
            byte[] return_value = Encoding.UTF8.GetBytes("Connection error");

            // BUFFER THAT STORES THE SERVER RESPONSE
            byte[] server_response = new byte[client_response.Length];




            // CREATE A NEW "Socket" OBJECT THAT IS SET TO USE IPV4 ON THE TCP PROTOCOL AND SET THE SOCKET TO SEND AND RECEIVE MESSAGES
            System.Net.Sockets.Socket client = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);


            // SET THE RECEIVE BUFFER AND THE SEND BUFFER TO HAVE A SIZE OF 32.768 Kilobytes
            client.ReceiveBufferSize = 32768;
            client.SendBufferSize = 32768;


            // SET THE RECEIVE AND SEND TIMOUT OF THE SOCKET TO 1 SECOND
            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;




            try
            {
                // CONNECT TO THE STUDENT RECORDS SERVER USING THE SELECTED SERVER IP ADDRESS AND PORT
                client.Connect(endpoint_ip_address, endpoint_port);


                // WRAP THE "Socket" OBJECT INTO A "NetworkStream" OBJECT
                System.Net.Sockets.NetworkStream client_stream = new System.Net.Sockets.NetworkStream(client);



                try
                {

                    // "SslStream" OBJECT WHICH WRAPS THE "NetworkStream" OBJECT TO ESTABLISH A SECURE TLS1.2 CONNECTION BETWEEN THE CLIENT AND THE SERVER, AND THAT USES X509 CERTIFICATE VALIDATION 
                    System.Net.Security.SslStream secure_client_stream = new System.Net.Security.SslStream(client_stream, false, new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                    try
                    {


                        // IF THE CLIENT PAYLOAD SERIALISATION PROCEDURE DID NOT FAIL
                        if (Encoding.UTF8.GetString(serialised_payload, 0, serialised_payload.Length) != "PAYLOAD SERIALISATION FAILED")
                        {

                            // AUTHENTIFICATE AS A CLIENT ON THIS CONNECTION, SET THE PROTOCOL OF THE CONNECTION AS TLS1.2, AND SPECIFY THAT THE CERTIFICATE AUTHORITY OF THE TARGET SERVER MUST BE "Student_Records_System"
                            secure_client_stream.AuthenticateAsClient("Student_Records_System", null, System.Security.Authentication.SslProtocols.Tls12, false);



                            // AUTHENTICATE THE SERVER AS THE SERVER OF THIS CONNECTION, SET THE ENCRYPTION PROTOCOL AS TLS1.2, AND PASS THE PUBLIC KEY OF THE SERVER TO THE CLIENT
                            int bytes_per_second = await Rount_Trip_Time_Calculator(secure_client_stream);






                            // CALCULATE THE TIMEOUT IN WHICH THE CONNECTION SHOULD BE DROPPED IF THE MESSAGE CONTAINING THE LENGTH OF THE PAYLOAD TO BE SENT TO SERVER IS NOT SENT ACCORDING WITH THE CONNECTION SPEED
                            await Calculate_Connection_Timeout(secure_client_stream, serialised_payload_length.Length, bytes_per_second);

                            // SEND THE LENGTH OF THE PAYLOAD TO BE SENT TO THE SERVER
                            await secure_client_stream.WriteAsync(serialised_payload_length, 0, serialised_payload_length.Length);






                            // CALCULATE THE TIMEOUT IN WHICH THE CONNECTION SHOULD BE DROPPED IF "SYN-ACK" MESSAGE IS NOT RECEIVED FROM THE SERVER ACCORDING WITH THE CONNECTION SPEED
                            await Calculate_Connection_Timeout(secure_client_stream, server_response.Length, bytes_per_second);

                            // READ THE "SYN-ACK" MESSAGE SENT BY THE SERVER
                            await secure_client_stream.ReadAsync(server_response, 0, server_response.Length);






                            // CALCULATE THE TIMEOUT IN WHICH THE CONNECTION SHOULD BE DROPPED IF THE PAYLOAD TO BE SENT TO SERVER IS NOT SENT ACCORDING WITH THE CONNECTION SPEED
                            await Calculate_Connection_Timeout(secure_client_stream, serialised_payload.Length, bytes_per_second);

                            // SEND THE PAYLOAD TO THE SERVER
                            await secure_client_stream.WriteAsync(serialised_payload, 0, serialised_payload.Length);






                            // BUFFER THAT HOLDS THE LENGTH OF THE PAYLOAD TO BE RECEIVED FROM THE SERVER
                            byte[] server_payload_length = new byte[1024];

                            // CALCULATE THE TIMEOUT IN WHICH THE CONNECTION SHOULD BE DROPPED IF THE MESSAGE CONTAINING THE LENGTH OF THE SERVER PAYLOAD IS NOT RECEIVED FROM THE SERVER ACCORDING WITH THE CONNECTION SPEED
                            await Calculate_Connection_Timeout(secure_client_stream, server_payload_length.Length, bytes_per_second);

                            // READ THE LENGTH OF THE PAYLOAD TO BE RECEIVED FROM THE SERVER
                            await secure_client_stream.ReadAsync(server_payload_length, 0, server_payload_length.Length);






                            // CALCULATE THE TIMEOUT IN WHICH THE CONNECTION SHOULD BE DROPPED IF THE "SYN-ACK" MESSAGE TO BE SENT TO SERVER IS NOT SENT ACCORDING WITH THE CONNECTION SPEED
                            await Calculate_Connection_Timeout(secure_client_stream, client_response.Length, bytes_per_second);

                            // SEND THE "SYN-ACK" MESSAGE TO THE SERVER
                            await secure_client_stream.WriteAsync(client_response, 0, client_response.Length);






                            // BUFFER THAT HOLDS THE SERVER PAYLOAD
                            byte[] server_payload = new byte[BitConverter.ToInt32(server_payload_length, 0)];

                            // CALCULATE THE TIMEOUT IN WHICH THE CONNECTION SHOULD BE DROPPED IF THE SERVER PAYLOAD IS NOT RECEIVED FROM THE SERVER ACCORDING WITH THE CONNECTION SPEED
                            await Calculate_Connection_Timeout(secure_client_stream, server_payload.Length, bytes_per_second);


                            // READ THE SERVER PAYLOAD RECURSIVELY UNTIL THE NUMBER OF BYTES READ EQUALS WITH THE LENGTH OF THE PAYLOAD
                            int total_bytes_read = 0;

                            while (total_bytes_read < server_payload.Length)
                            {
                                total_bytes_read += await secure_client_stream.ReadAsync(server_payload, total_bytes_read, server_payload.Length - total_bytes_read);
                            }




                            // DESERIALISE THE PAYLOAD RECEIVED FROM THE SERVER
                            Server_WSDL_Payload deserialised_server_payload = await Deserialise_Server_Payload(server_payload);

                            // RETURN THE CONTENT OF THE PAYLOAD SENT BY THE SERVER IN BINARY FORMAT
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
        private static Task<int> Calculate_Connection_Timeout(System.Net.Security.SslStream secure_client_stream, int payload_size, int bytes_per_second)
        {
            int milliseconds = 1000 * (payload_size / bytes_per_second) + 1000;

            if (milliseconds > 1000)
            {
                secure_client_stream.WriteTimeout = milliseconds;
                secure_client_stream.ReadTimeout = milliseconds;
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
