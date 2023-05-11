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
    /// Interaction logic for Log_In_Or_Register.xaml
    /// </summary>
    public partial class Log_In_Or_Register : Window
    {
        private bool Window_Closing;
        private System.Timers.Timer Animation_Timer = new System.Timers.Timer();


        private static Log_In log_in = new Log_In();
        private static Register register = new Register();


        private static Frame MainWindow_Frame;
        private static Log_In_Or_Register log_in_or_register;


        private static Dispatcher_Controller controller = new Dispatcher_Controller();






        public Log_In_Or_Register()
        {
            InitializeComponent();
        }





        private void Move_The_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }





        private void Minimse_The_Window(object sender, RoutedEventArgs e)
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





        private void Close_The_Window_Button_Click(object sender, RoutedEventArgs e)
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
                                this.DragMove();
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





        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Animation_Timer.Elapsed += Animation_Timer_Elapsed;
            Animation_Timer.Interval = 1000;
            Animation_Timer.Start();

            MainWindow_Frame = Navigation_Frame;
            log_in_or_register = this;


            // IF A CACHE FILE EXISTS IN THE APPLICATION'S DIRECTORY NAVIGATE TO THE APPLICATION'S MAIN WINDOW
            // OTHERWISE NAVIGATE TO THE LOG IN PAGE
            if (await controller.Load_Log_In_Session_Key_Controller() == true)
            {
                Navigate("Main Window");
            }
            else
            {
                Navigate("Log In Page");
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

                                    
                                }
                                else
                                {
                                    if (Animation_Timer != null)
                                    {
                                        Animation_Timer.Close();
                                        Animation_Timer.Dispose();
                                    }
                                }
                            }
                            else
                            {
                                if (Animation_Timer != null)
                                {
                                    Animation_Timer.Close();
                                    Animation_Timer.Dispose();
                                }
                            }
                        });
                    }
                    else
                    {
                        if (Animation_Timer != null)
                        {
                            Animation_Timer.Close();
                            Animation_Timer.Dispose();
                        }
                    }
                }
                else
                {
                    if (Animation_Timer != null)
                    {
                        Animation_Timer.Close();
                        Animation_Timer.Dispose();
                    }
                }
            }
            else
            {
                if(Animation_Timer != null)
                {
                    Animation_Timer.Close();
                    Animation_Timer.Dispose();
                }
            }
        }





        private void Window_Is_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window_Closing = false;

            if (Animation_Timer != null)
            {
                Animation_Timer.Close();
                Animation_Timer.Dispose();
            }
        }



        public static void Navigate(string option)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (MainWindow_Frame.NavigationService.CanGoBack == true)
                {
                    MainWindow_Frame.NavigationService.RemoveBackEntry();
                    MainWindow_Frame.RemoveBackEntry();
                }

                switch (option)
                {
                    case "Log In Page":
                        MainWindow_Frame.NavigationService.Navigate(log_in);
                        break;

                    case "Register Page":
                        MainWindow_Frame.NavigationService.Navigate(register);
                        break;

                    case "Main Window":
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();

                        log_in_or_register.Close();
                        break;
                }
            });
        }

    }
}
