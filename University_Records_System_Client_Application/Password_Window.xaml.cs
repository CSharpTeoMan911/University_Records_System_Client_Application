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

        Cryptographic_Services_Dispatcher_Controller cryptographic_controller = new Cryptographic_Services_Dispatcher_Controller();

        public Password_Window(string selected_function, string email_address)
        {
            Selected_Function = selected_function;
            email = email_address;
            InitializeComponent();
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
                                case "Account validation":
                                    // INITIATE THE ACCOUNT VALIDATION PROCEDURE BY TRANSMITTING THE USER SELECTED ACCOUNT VALIDATION CODE
                                    // IN BYTE FORMAT TO THE SERVER AND STORING THE RESULT IN A BUFFER.
                                    byte[] authentification_validation_result = await Server_Connections.Initiate_Server_Connection<string>(email, Password_PasswordBox.Password, "Account validation", false);


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
                                    byte[] log_in_code_validation_result = await Server_Connections.Initiate_Server_Connection<string>(email, Password_PasswordBox.Password, "Account log in", false);


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
                                            await cryptographic_controller.Save_Log_In_Key_Controller(email, Encoding.UTF8.GetString(log_in_code_validation_result), true);
                                            Log_In_Or_Register.Navigate("Main Window");
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
