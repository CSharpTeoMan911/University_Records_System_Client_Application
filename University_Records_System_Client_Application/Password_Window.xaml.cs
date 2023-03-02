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


        private class Application_Cryptographic_Services_Mitigator : Application_Cryptographic_Services
        {
            internal static async Task<bool> Load_X509_Certificate_Into_Store_Initiator(byte[] certificate_binary_data, string certificate_password)
            {
                return await Load_X509_Certificate_Into_Store(certificate_binary_data, certificate_password);
            }
        }


        private class Server_Connection_Mitigator : Server_Connections
        {
            internal static async Task<byte[]> Connection_Initialisation_Procedure<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function, bool binary_file)
            {
                return await Initiate_Server_Connection<Password__Or__Binary_Content>(email__or__log_in_session_key as string, password__or__binary_content, function, binary_file);
            }
        }


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


                                    System.Windows.Forms.OpenFileDialog certificate_selector = new System.Windows.Forms.OpenFileDialog();

                                    try
                                    {
                                        certificate_selector.Filter = "ssl certificate files (*.pfx)|*.pfx";

                                        if (certificate_selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        {
                                            byte[] certificate_binary_data = new byte[certificate_selector.OpenFile().Length];

                                            await certificate_selector.OpenFile().ReadAsync(certificate_binary_data, 0, certificate_binary_data.Length);

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
                                    byte[] authentification_validation_result = await Server_Connection_Mitigator.Connection_Initialisation_Procedure<string>(email, Password_PasswordBox.Password, "Account validation", false);
                                    Message_Displayer.Display_Message(authentification_validation_result);

                                    if(Encoding.UTF8.GetString(authentification_validation_result) == "Account validation successful")
                                    {
                                        this.Close();
                                    }
                                    break;


                                case "Log in code":
                                    
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
