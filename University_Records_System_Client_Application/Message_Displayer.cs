using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace University_Records_System_Client_Application
{
    class Message_Displayer
    {
        public static void Display_Message(byte[] received_message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                switch(Encoding.UTF8.GetString(received_message))
                {
                    case "Registration successful":
                        MessageBox.Show("Check your email address for your registration code to validate your account. If not validated, your account will be deleted in 2 hours.", "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Account already exist":
                        MessageBox.Show("An account that is registered with this email already exist.", "Email address already exists", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Invalid email address":
                        MessageBox.Show("The email address provided is not valid.", "Invalid email address", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Password does not contain the amount of characters required":
                        MessageBox.Show("The password provided is less than 6 characters long", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Email server connection error":
                        MessageBox.Show("Server could not send the registration code to your email address. Please ensure your email address is valid.", "Email server error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Account validation successful":
                        MessageBox.Show("Log in into your account.", "Account validation successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Account validation not successful":
                        MessageBox.Show("Enter a valid account registration code.", "Account validation is not successful", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Log in successful":
                        MessageBox.Show("Check your email address for your one time log in code to log in into your account. The log in code expires in 5 minutes.", "Log in successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "":
                        break;
                }
            });
        }
    }
}
