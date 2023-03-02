using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace University_Records_System_Client_Application
{
    /// <summary>
    /// Interaction logic for Password_Window.xaml
    /// </summary>
    public partial class Password_Window : Window
    {

        private string Selected_Function;
        private string email;
        
        public Password_Window(string selected_function, string email_address)
        {
            Selected_Function = selected_function;
            email = email_address;
            InitializeComponent();
        }







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

        private sealed class Navigation_Mitigator : Log_In_Or_Register
        {
            internal static void Navigate()
            {
                Current_Page = "Main Window";
            }
        }

        private sealed class Application_Cryptographic_Services_Mitigator : Application_Cryptographic_Services
        {
            internal static async Task<bool> Load_X509_Certificate_Into_Store_Initiator(byte[] certificate_binary_data, string certificate_password)
            {
                return await Load_X509_Certificate_Into_Store(certificate_binary_data, certificate_password);
            }

            internal static async Task<bool> Save_Log_In_Key_Initiator(string email, string log_in_code, bool keep_user_logged_in)
            {
                return await Save_Log_In_Key(email, log_in_code, keep_user_logged_in);
            }
        }


        private sealed class Server_Connection_Mitigator : Server_Connections
        {
            internal static async Task<byte[]> Connection_Initialisation_Procedure<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function, bool binary_file)
            {
                return await Initiate_Server_Connection<Password__Or__Binary_Content>(email__or__log_in_session_key as string, password__or__binary_content, function, binary_file);
            }
        }

        // [ END ]











        private void Close_The_Window(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            this.Close();
                        }
                    }
                }
            }
        }





        private async void Enter_Password(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {

                            switch (Selected_Function)
                            {
                                case "Add X509 certificate":

                                    // CREATE AN "OpenFileDialog" OBJECT. THIS OBJECT IS USED TO NAVIGATE THE WINDOWS OS FILE SYSTEM IN 
                                    // ORDER TO SELECT THE SET SERVER'S X509 SSL CERTIFICATES.
                                    System.Windows.Forms.OpenFileDialog certificate_selector = new System.Windows.Forms.OpenFileDialog();

                                    try
                                    {
                                        // CREATE A FILTER FOR THE "OpenFileDialog" OBJECT IN ORDER FOR IT TO DISPLAY ONLY
                                        // FILES THAT ARE IN THE ".pfx" FILE FORMAT.
                                        certificate_selector.Filter = "ssl certificate files (*.pfx)|*.pfx";



                                        // OPEN THE FILE DIALOG WINDOW.
                                        if (certificate_selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        {


                                            //IF IT OPENED SUCCESSFULLY 



                                            // BUFFER THAT STORES THE FILE CONTENTS IN BYTE FORMAT
                                            byte[] certificate_binary_data = new byte[certificate_selector.OpenFile().Length];



                                            //READ THE BINARY CONTENT OF THE FILE AND SAVE IT IN BYTE FORMAT
                                            await certificate_selector.OpenFile().ReadAsync(certificate_binary_data, 0, certificate_binary_data.Length);



                                            // FORMAT THE BINARY CONTENT OF THE SELECTED X509 CERTIFICATE IN X509 CERTIFICATE FORMAT AND LOAD THE CERTIFICATE
                                            // IN THE DEVICE'S CERTIFICATE STORE TRUSTED CERTIFICATE AUTHORITIES 
                                            bool certificate_load_result = await Application_Cryptographic_Services_Mitigator.Load_X509_Certificate_Into_Store_Initiator(certificate_binary_data, Password_PasswordBox.Password);

                                            if (certificate_load_result == true)
                                            {
                                                this.Close();
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {
                                        if (certificate_selector != null)
                                        {
                                            certificate_selector.Dispose();
                                        }
                                    }
                                    break;







                                case "Account validation":
                                    // INITIATE THE ACCOUNT VALIDATION PROCEDURE BY TRANSMITTING THE USER SELECTED ACCOUNT VALIDATION CODE
                                    // IN BYTE FORMAT TO THE SERVER AND STORING THE RESULT IN A BUFFER.
                                    byte[] authentification_validation_result = await Server_Connection_Mitigator.Connection_Initialisation_Procedure<string>(email, Password_PasswordBox.Password, "Account validation", false);


                                    // DISPLAY THE RESULT OF THE ACCOUNT VALIDATION PROCEDURE EXECUTED BY THE SERVER
                                    Message_Displayer.Display_Message(authentification_validation_result);



                                    if(Encoding.UTF8.GetString(authentification_validation_result) == "Account validation successful")
                                    {
                                        this.Close();
                                    }
                                    break;








                                case "Log in code":
                                    // INITIATE THE LOG IN PROCEDURE BY TRANSMITTING THE USER SELECTED LOG IN CODE 
                                    // IN BYTE FORMAT TO THE SERVER AND STORING THE RESULT IN A BUFFER.
                                    byte[] log_in_code_validation_result = await Server_Connection_Mitigator.Connection_Initialisation_Procedure<string>(email, Password_PasswordBox.Password, "Account log in", false);


                                    // DISPLAY THE RESULT OF THE LOG IN PROCEDURE EXECUTED BY THE SERVER
                                    Message_Displayer.Display_Message(log_in_code_validation_result);



                                    if (Encoding.UTF8.GetString(log_in_code_validation_result) != "Connection error")
                                    {
                                        if(Encoding.UTF8.GetString(log_in_code_validation_result) != "Wrong log in code")
                                        {
                                            // IF NO ERROR MESSAGE WAS TRANSMITTED BY THE SERVER, THAN THE LOG IN OPERATION IS SUCCESSFUL AND
                                            // THE SERVER TRANSMITTED AS A RESULT OF THE OPERATION A LOG IN SESSION KEY. THE LOG IN SESSION
                                            // KEY MUST ME USED BY THE CLIENT IN A TRANSACTION WITH THE SERVER BEFORE ANY APPLICATION 
                                            // FUNCTION THAT REQUIRE THE USER TO BE AUTHENTIFICATED.
                                            await Application_Cryptographic_Services_Mitigator.Save_Log_In_Key_Initiator(email, Encoding.UTF8.GetString(log_in_code_validation_result), true);
                                            Navigation_Mitigator.Navigate();
                                            this.Close();
                                        }
                                    }
                                    break;
                            }

                        }
                    }
                }
            }
        }






        private void Move_The_Window(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            this.DragMove();
                        }
                    }
                }
            }
        }






        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            switch (Selected_Function)
                            {
                                case "Add X509 certificate":
                                    Main_TextBlock.Text = "Enter certificate password";
                                    break;


                                case "Account validation":
                                    Main_TextBlock.Text = "Enter registration code";
                                    break;

                                case "Log in code":
                                    Main_TextBlock.Text = "Enter log in code";
                                    break;
                            }
                        }
                    }
                }
            }
        }




    }
}
