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
    /// Interaction logic for Log_In.xaml
    /// </summary>
    public partial class Log_In : Page
    {
        public Log_In()
        {
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

  

        private sealed class Client_Variables_Mitigator:Client_Variables
        {
            internal static void Keep_User_Logged_In()
            {
                keep_user_logged_in = true;
            }

            internal static void Do_Not_Keep_User_Logged_In()
            {
                keep_user_logged_in = false;
            }
        }

        // [ END ]










        private void Navigate_To_Register_Page(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            Log_In_Or_Register.Navigate("Register Page");
                        }
                    }
                }
            }
        }





        private void Log_In_Credentials(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if(Application.Current.Dispatcher != null)
                {
                    if(Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if(this != null)
                        {
                            string email = Email_TextBox.Text;
                            string password = Password_PasswordBox.Password;


                            // INITIATE A LOG IN OR ACCOUNT VALIDATION PROCEDURE BY COMMUNICATING WITH THE SERVER
                            // ON ANOTHER THREAD
                            System.Threading.Thread connection_thread = new System.Threading.Thread(async () =>
                            {
                                byte[] log_in_result = await Server_Connections.Initiate_Server_Connection<string>(email, password, "Log In", false);



                                // INVOKE THE USER INTERFACE THREAD IN ORDER TO MANIPULATE UI OBJECTS
                                Application.Current.Dispatcher.Invoke(() =>
                                {



                                    //DISPLAY THE RESULT OF THE PROCEDURE
                                    Message_Displayer.Display_Message(log_in_result);




                                    // OPEN THE PASSWORD WINDOW IN ORDER TO VALIDATE OR LOG IN AN ACCOUNT BY COMMUNICATING
                                    // THE CODE TO THE SERVER IF THE PROCEDURE RESULT MEETS ONE OF THE SELECTED PARAMETERS.
                                    if (Encoding.UTF8.GetString(log_in_result) == "Account not validated")
                                    {
                                        Password_Window certificate_Password_Selector = new Password_Window("Account validation", email);
                                        certificate_Password_Selector.ShowDialog();
                                    }
                                    else if(Encoding.UTF8.GetString(log_in_result) == "Log in successful")
                                    {
                                        Password_Window certificate_Password_Selector = new Password_Window("Log in code", email);
                                        certificate_Password_Selector.ShowDialog();
                                    }



                                });



                            });
                            connection_thread.SetApartmentState(System.Threading.ApartmentState.STA);
                            connection_thread.Priority = System.Threading.ThreadPriority.Highest;
                            connection_thread.IsBackground = true;
                            connection_thread.Start();
                        }
                    }
                }
            }
        }





        
        private void Load_Certificate_Authority(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            // OPEN THE PASSWORD WINDOW IN ORDER TO INSERT THE PASSWORD OF THE X509 CERTIFICATE AND LOAD
                            // IT INSIDE THE DEVICE'S CERTIFICATE STORE
                            Password_Window certificate_Password_Selector = new Password_Window("Add X509 certificate", null);
                            certificate_Password_Selector.ShowDialog();
                        }
                    }
                }
            }
        }





        private void Keep_User_Logged_In(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            Client_Variables_Mitigator.Keep_User_Logged_In();
                        }
                    }
                }
            }
        }





        private void Do_Not_Keep_User_Logged_In(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            Client_Variables_Mitigator.Do_Not_Keep_User_Logged_In();
                        }
                    }
                }
            }

        }
    }
}
