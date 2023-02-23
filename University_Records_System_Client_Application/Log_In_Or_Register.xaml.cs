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
        protected static string Current_Page;

        private static Log_In log_in = new Log_In();
        private static Register register = new Register();
        

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Animation_Timer.Elapsed += Animation_Timer_Elapsed;
            Animation_Timer.Interval = 10;
            Animation_Timer.Start();

            Current_Page = "Log In Page";
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
                                    switch(Current_Page)
                                    {
                                        case "Log In Page":
                                            if(Navigation_Frame.NavigationService.CanGoBack == true)
                                            {
                                                Navigation_Frame.NavigationService.RemoveBackEntry();
                                                Navigation_Frame.RemoveBackEntry();
                                            }

                                            Navigation_Frame.NavigationService.Navigate(log_in);

                                            Current_Page = null;
                                            break;

                                        case "Register Page":
                                            if (Navigation_Frame.NavigationService.CanGoBack == true)
                                            {
                                                Navigation_Frame.NavigationService.RemoveBackEntry();
                                                Navigation_Frame.RemoveBackEntry();
                                            }

                                            Navigation_Frame.NavigationService.Navigate(register);

                                            Current_Page = null;
                                            break;
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

    }
}
