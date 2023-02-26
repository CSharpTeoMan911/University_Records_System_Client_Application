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
    /// Interaction logic for Certificate_Password_Selector.xaml
    /// </summary>
    public partial class Certificate_Password_Selector : Window
    {
      
        public Certificate_Password_Selector()
        {
            InitializeComponent();
        }


        private class Application_Cryptographic_Services_Mitigator : Application_Cryptographic_Services
        {
            internal static async Task<bool> Load_X509_Certificate_Into_Store_Initiator(byte[] certificate_binary_data, string certificate_password)
            {
                return await Load_X509_Certificate_Into_Store(certificate_binary_data, certificate_password);
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
                            
                            System.Windows.Forms.OpenFileDialog certificate_selector = new System.Windows.Forms.OpenFileDialog();

                            try
                            {
                                certificate_selector.Filter = "ssl certificate files (*.pfx)|*.pfx";

                                if(certificate_selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    byte[] certificate_binary_data = new byte[certificate_selector.OpenFile().Length];

                                    await certificate_selector.OpenFile().ReadAsync(certificate_binary_data, 0, certificate_binary_data.Length);

                                    bool certificate_load_result = await Application_Cryptographic_Services_Mitigator.Load_X509_Certificate_Into_Store_Initiator(certificate_binary_data, Password_PasswordBox.Password);

                                    if(certificate_load_result == true)
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
    }
}
