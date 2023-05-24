using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static University_Records_System_Client_Application.Settings;

namespace University_Records_System_Client_Application
{
    /// <summary>
    /// Interaction logic for Application_Settings.xaml
    /// </summary>
    public partial class Application_Settings : Window
    {
        Cryptographic_Serives_Dispatcher_Controller cryptographic_controller = new Cryptographic_Serives_Dispatcher_Controller();
        private bool Window_Closing;



        public Application_Settings()
        {
            InitializeComponent();
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


        private void Close_The_Window(object sender, RoutedEventArgs e)
        {
            if(Window_Closing == false)
            {
                if(Application.Current.Dispatcher != null)
                {
                    if(Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if(this != null)
                        {
                            this.Close();
                        }
                    }
                }
            }
        }


        private async void Window_Is_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window_Closing = true;
            await Update_Ip_TextBox();
            await Update_Port_TextBox();
            await Settings.Settings_File_Operation(File_Option.Update_Settings_File);
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string ip_address = (await Settings.Get_Value(Settings.Option.endpoint_ip_address)).ToString();
            System.Diagnostics.Debug.WriteLine(ip_address);

            Ip_TextBox.Text = ip_address;

            Server_Port_TextBox.Text = ((int)(await Settings.Get_Value(Settings.Option.endpoint_port))).ToString();
        }


        private void Ip_TextBox_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            if (Window_Closing == false)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            Style style = new Style();
                            style.Setters.Add((FindResource("Grey_Ip_TextBox_Background_TextBox") as Style).Setters[0]);
                            style.Setters.Add((FindResource("Grey_Ip_TextBox_Background_TextBox") as Style).Setters[1]);

                            Ip_TextBox.Clip = new RectangleGeometry(new Rect(0,0,Ip_TextBox.ActualWidth, 28), 5, 5);

                            Ip_TextBox.Style = style;

                            style.Seal();
                        }
                    }
                }
            }
        }


        private async void Ip_TextBox_Lost_Focus(object sender, RoutedEventArgs e)
        {
            if (Window_Closing == false)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                             await Update_Ip_TextBox();
                        }
                    }
                }
            }
        }


        private async void Port_TextBox_Lost_Focus(object sender, RoutedEventArgs e)
        {
            if (Window_Closing == false)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {
                            await Update_Port_TextBox();
                        }
                    }
                }
            }
        }


        private async Task<bool> Update_Ip_TextBox()
        {
            await Application.Current.Dispatcher.Invoke(async() =>
            {
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IPAddress.TryParse(Ip_TextBox.Text, out iPAddress);


                string iPAddress_String = iPAddress?.ToString();


                if (iPAddress_String != null)
                {
                    Ip_TextBox.Text = iPAddress_String;
                }
                else
                {
                    Ip_TextBox.Text = "127.0.0.1";
                }


                await Settings.Set_Value(Settings.Option.endpoint_ip_address, Ip_TextBox.Text);
            });


            return true;
        }


        private async Task<bool> Update_Port_TextBox()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                try
                {
                    double value = Convert.ToDouble(Server_Port_TextBox.Text);

                    if (value > 65535)
                    {
                        Server_Port_TextBox.Text = (await Settings.Get_Value(Settings.Option.endpoint_port)).ToString();
                    }
                }
                catch
                {
                    Server_Port_TextBox.Text = (await Settings.Get_Value(Settings.Option.endpoint_port)).ToString();
                }


                await Settings.Set_Value(Settings.Option.endpoint_port, Convert.ToInt32(Server_Port_TextBox.Text));
            });


            return true;
        }

        private async void Upload_Server_Certificate(object sender, RoutedEventArgs e)
        {
            if (Window_Closing == false)
            {
                if (Application.Current.Dispatcher != null)
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {
                        if (this != null)
                        {

                            // CREATE AN "OpenFileDialog" OBJECT. THIS OBJECT IS USED TO NAVIGATE THE WINDOWS OS FILE SYSTEM IN 
                            // ORDER TO SELECT THE SET SERVER'S X509 SSL CERTIFICATES.
                            System.Windows.Forms.OpenFileDialog certificate_selector = new System.Windows.Forms.OpenFileDialog();

                            try
                            {
                                // CREATE A FILTER FOR THE "OpenFileDialog" OBJECT IN ORDER FOR IT TO DISPLAY ONLY
                                // FILES THAT ARE IN THE ".pfx" FILE FORMAT.
                                certificate_selector.Filter = "ssl certificate files (*.crt)|*.crt";



                                // OPEN THE FILE DIALOG WINDOW.
                                if (certificate_selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {



                                    // FORMAT THE BINARY CONTENT OF THE SELECTED X509 CERTIFICATE IN X509 CERTIFICATE FORMAT AND LOAD THE CERTIFICATE
                                    // IN THE DEVICE'S CERTIFICATE STORE TRUSTED CERTIFICATE AUTHORITIES 
                                    bool certificate_load_result = await cryptographic_controller.Load_X509_Certificate_Into_Store_Controller(certificate_selector.FileName);

                                    if (certificate_load_result == true)
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
    }
}
