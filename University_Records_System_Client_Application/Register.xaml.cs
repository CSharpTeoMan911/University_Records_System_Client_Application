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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace University_Records_System_Client_Application
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private class Navigation_Mitigator:Log_In_Or_Register
        {
            internal static void Navigate()
            {
                Current_Page = "Log In Page";
            }
        }

        private class Server_Connection_Mitigator:Server_Connections
        {
            internal static async Task<byte[]> Connection_Initialisation_Procedure<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function, bool binary_file)
            {
                return await Initiate_Server_Connection<Password__Or__Binary_Content>(email__or__log_in_session_key as string, password__or__binary_content, function, binary_file);
            }
        }

        private async void Register_Credentials(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            if(Password_PasswordBox.Password == Password_PasswordBox_Repeated.Password)
                            {
                                byte[] registration_result = await Server_Connection_Mitigator.Connection_Initialisation_Procedure<string>(Email_TextBox.Text, Password_PasswordBox.Password, "Register", false);
                                Message_Displayer.Display_Message(registration_result);

                                if(Encoding.UTF8.GetString(registration_result) == "Registration successful")
                                {
                                    Navigation_Mitigator.Navigate();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Navigate_To_Log_In_Page(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            Navigation_Mitigator.Navigate();
                        }
                    }
                }
            }
        }
    }
}
