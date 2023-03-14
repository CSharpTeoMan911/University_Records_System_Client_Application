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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool Window_Closing;
        private short Maximised_Or_Normalised;
        private short Main_Menu_Expanded_Or_Contracted;
        private System.Timers.Timer Animation_Timer;

        private static Students students = new Students();
        private static Courses courses = new Courses();


        // UI RELATED OBJECT VALUES
        //
        // [ BEGIN ]

        private bool Switch_Offset;
        private int Gradient_Value;

        // [ END ]


        public MainWindow()
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

        private class Server_Connections_Mitigator:Server_Connections
        {
            internal static async Task<byte[]> Connection_Initialisation_Procedure<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function, bool binary_file)
            {
                return await Initiate_Server_Connection<Password__Or__Binary_Content>(email__or__log_in_session_key as string, password__or__binary_content, function, binary_file);
            }
        }

        private sealed class Client_Variables_Mitigator:Client_Variables
        {
            internal static string Get_User_Email()
            {
                return email;
            }

            internal static string Get_User_Log_In_Session_Key()
            {
                return log_in_session_key;
            }
        }

        private sealed class Application_Cryptographic_Services_Mitigator : Application_Cryptographic_Services
        {
            internal async static Task<bool> Delete_User_Credential_Cache()
            {
                return await Delete_Log_In_Sesion_Key();
            }
        }

        // [ END ]







        private void Move_The_Window(object sender, MouseButtonEventArgs e)
        {
            if(Application.Current != null)
            {
                if(Application.Current.Dispatcher != null)
                {
                    if(Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (this != null)
                        {
                            if(Window_Closing != true)
                            {
                                this.DragMove();
                            }
                        }
                    }
                }
            }
        }

        private void Minimise_The_Window(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (this != null)
                        {
                            if (Window_Closing != true)
                            {
                                this.WindowState = WindowState.Minimized;
                            }
                        }
                    }
                }
            }
        }

        private void Maximise_Or_Normalise_The_Window(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (this != null)
                        {
                            if (Window_Closing != true)
                            {
                                Maximised_Or_Normalised++;

                                switch(Maximised_Or_Normalised)
                                {
                                    case 1:
                                        this.WindowState = WindowState.Maximized;
                                        Maximise_Or_Normalise_The_Window_Button.Content = "\xEF2F";
                                        break;

                                    case 2:
                                        Maximised_Or_Normalised = 0;
                                        this.WindowState = WindowState.Normal;
                                        Maximise_Or_Normalise_The_Window_Button.Content = "\xEF2E";
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Close_The_Window(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (this != null)
                        {
                            if (Window_Closing != true)
                            {
                                this.Close();
                            }
                        }
                    }
                }
            }
        }

        private void Animation_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (this != null)
                            {
                                if (Window_Closing != true)
                                {
                                    switch (Switch_Offset)
                                    {
                                        case true:

                                            switch (Gradient_Value < 50)
                                            {
                                                case true:
                                                    Gradient_Value++;

                                                    Window_Offset.Offset += 0.025;
                                                    Window_Handle_Offset.Offset += 0.025;
                                                    Minimise_The_Window_Button_Offset.Offset += 0.025;
                                                    Maximise_Or_Normalise_The_Window_Button_Offset.Offset += 0.025;
                                                    Close_The_Window_Button_Offset.Offset += 0.025;
                                                    Main_Menu_Button_Offset.Offset += 0.025;
                                                    Main_Menu_Panel_Offset.Offset += 0.025;
                                                    break;

                                                case false:
                                                    Switch_Offset = false;
                                                    break;
                                            }
                                            break;

                                        case false:

                                            switch (Gradient_Value > 0)
                                            {
                                                case true:
                                                    Gradient_Value--;

                                                    Window_Offset.Offset -= 0.025;
                                                    Window_Handle_Offset.Offset -= 0.025;
                                                    Minimise_The_Window_Button_Offset.Offset -= 0.025;
                                                    Maximise_Or_Normalise_The_Window_Button_Offset.Offset -= 0.025;
                                                    Close_The_Window_Button_Offset.Offset -= 0.025;
                                                    Main_Menu_Button_Offset.Offset -= 0.025;
                                                    Main_Menu_Panel_Offset.Offset -= 0.025;
                                                    break;

                                                case false:
                                                    Switch_Offset = true;
                                                    break;
                                            }
                                            break;
                                    }



                                    switch(Main_Menu_Expanded_Or_Contracted)
                                    {
                                        case 1:
                                            if(Main_Menu_Panel.Height < 160)
                                            {
                                                Main_Menu_Panel.Height += 20;
                                            }
                                            break;

                                        case 2:
                                            Main_Menu_Expanded_Or_Contracted = 0;
                                            Main_Menu_Panel.Height = 0;
                                            break;
                                    }
                                }
                                else
                                {
                                    if (Animation_Timer != null)
                                    {
                                        try
                                        {
                                            Animation_Timer.Close();
                                            Animation_Timer.Dispose();
                                        }
                                        catch { }
                                    }
                                }
                            }
                            else
                            {
                                if (Animation_Timer != null)
                                {
                                    try
                                    {
                                        Animation_Timer.Close();
                                        Animation_Timer.Dispose();
                                    }
                                    catch { }
                                }
                            }
                        });
                    }
                    else
                    {
                        if (Animation_Timer != null)
                        {
                            try
                            {
                                Animation_Timer.Close();
                                Animation_Timer.Dispose();
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    if (Animation_Timer != null)
                    {
                        try
                        {
                            Animation_Timer.Close();
                            Animation_Timer.Dispose();
                        }
                        catch { }
                    }
                }
            }
            else
            {
                if(Animation_Timer != null)
                {
                    try
                    {
                        Animation_Timer.Close();
                        Animation_Timer.Dispose();
                    }
                    catch { }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window_Closing = false;

            Animation_Timer = new System.Timers.Timer();
            Animation_Timer.Elapsed += Animation_Timer_Elapsed;
            Animation_Timer.Interval = 20;
            Animation_Timer.Start();


            if (Page_Navigation_Frame.NavigationService.CanGoBack == true)
            {
                Page_Navigation_Frame.NavigationService.RemoveBackEntry();
            }

            Page_Navigation_Frame.NavigationService.Navigate(courses);
        }

        private void Window_Is_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window_Closing = true;

            if(Animation_Timer != null)
            {
                try
                {
                    Animation_Timer.Close();
                    Animation_Timer.Dispose();
                }
                catch { }
            }
        }

        private void Expand_The_Main_Menu(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (Window_Closing != true)
                            {
                                Main_Menu_Expanded_Or_Contracted++;
                            }
                        }
                    }
                }
            }
        }

        private async void Log_Out_Account(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (Window_Closing != true)
                            {
                                string log_in_session_key = Client_Variables_Mitigator.Get_User_Log_In_Session_Key();

                                byte[] log_out_result = await Server_Connections_Mitigator.Connection_Initialisation_Procedure<string>(log_in_session_key, String.Empty, "Account log out", false);

                                Message_Displayer.Display_Message(log_out_result);

                                if(Encoding.UTF8.GetString(log_out_result) == "Logged out")
                                {
                                    await Application_Cryptographic_Services_Mitigator.Delete_User_Credential_Cache();

                                    Log_In_Or_Register log_In_Or_Register = new Log_In_Or_Register();
                                    log_In_Or_Register.Show();

                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Navigate_To_Students_Page(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (Window_Closing != true)
                            {
                                if (Page_Navigation_Frame.NavigationService.CanGoBack == true)
                                {
                                    Page_Navigation_Frame.NavigationService.RemoveBackEntry();
                                }

                                Page_Navigation_Frame.NavigationService.Navigate(students);
                            }
                        }
                    }
                }
            }
        }

        private void Navigate_To_Courses_Page(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted != true)
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (Window_Closing != true)
                            {
                                if (Page_Navigation_Frame.NavigationService.CanGoBack == true)
                                {
                                    Page_Navigation_Frame.NavigationService.RemoveBackEntry();
                                }

                                Page_Navigation_Frame.NavigationService.Navigate(courses);
                            }
                        }
                    }
                }
            }
        }
    }
}
