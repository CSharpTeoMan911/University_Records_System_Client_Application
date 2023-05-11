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
    /// Interaction logic for Courses.xaml
    /// </summary>
    public partial class Courses : Page
    {
        public Courses()
        {
            InitializeComponent();
        }


        private sealed class Client_Variables_Mitigator:Client_Variables
        {
            internal static Task<string> Get_Log_In_Session_Key()
            {
                return Task.FromResult(log_in_session_key);
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
