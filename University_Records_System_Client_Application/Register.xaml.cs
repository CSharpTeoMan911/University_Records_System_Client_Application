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
                                byte[] registration_result = await Server_Connections.Initiate_Server_Connection<string>(Email_TextBox.Text, Password_PasswordBox.Password, Client_Variables.Functions.Register);
                                Message_Displayer.Display_Message(registration_result);

                                if(Encoding.UTF8.GetString(registration_result) == "Registration successful")
                                {
                                    Log_In_Or_Register.Navigate("Log In Page");
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
                            Log_In_Or_Register.Navigate("Log In Page");
                        }
                    }
                }
            }
        }
    }
}
