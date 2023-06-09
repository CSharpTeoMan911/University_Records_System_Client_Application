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
    /// Interaction logic for Additional_Data_Window.xaml
    /// </summary>
    public partial class Additional_Data_Window : Window
    {
        string Id;
        public Page page;

        private bool Window_Closing;
        public int OnOff;


        public enum Page
        {
            Grades_Page,
            Modules_Page
        }

        public Additional_Data_Window()
        {
            InitializeComponent();
        }

        public Additional_Data_Window(Page page, string Id)
        {
            this.page = page;
            this.Id = Id;

            InitializeComponent();
        }

        private void Minimise_The_Window(object sender, RoutedEventArgs e)
        {
            if(Window_Closing == false)
            {
                if(Application.Current.Dispatcher != null)
                {
                    if(Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if(this != null)
                        {
                            this.WindowState = WindowState.Minimized;
                        }
                    }
                }
            }
        }

        private void Maximise_Or_Normalise_The_Window(object sender, RoutedEventArgs e)
        {
            if (Window_Closing == false)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            OnOff++;

                            switch(OnOff)
                            {
                                case 1:
                                    this.WindowState = WindowState.Maximized;
                                    break;

                                case 2:
                                    OnOff = 0;
                                    this.WindowState = WindowState.Normal;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void Close_The_Window(object sender, RoutedEventArgs e)
        {
            if (Window_Closing == false)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Page_Navigation_Frame.NavigationService.RemoveBackEntry();

            switch (page)
            {
                case Page.Grades_Page:
                    Page_Navigation_Frame.NavigationService.Navigate(new Grades_Page(Id));
                    break;

                case Page.Modules_Page:
                    Page_Navigation_Frame.NavigationService.Navigate(new Courses_Page(Id));
                    break;
            }
        }

        private void Window_Is_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Page_Navigation_Frame.NavigationService.RemoveBackEntry();
        }

        private void Move_The_Window(object sender, MouseButtonEventArgs e)
        {
            if (Window_Closing == false)
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
    }
}
