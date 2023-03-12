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
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        public Students()
        {
            InitializeComponent();
        }


        private sealed class Client_Variables_Mitigator : Client_Variables
        {
            internal static Task<string> Get_Log_In_Session_Key()
            {
                return Task.FromResult(log_in_session_key);
            }
        }


        private class Server_Connections_Mitigator : Server_Connections
        {
            internal static async Task<byte[]> Connection_Initialisation_Procedure<Password__Or__Binary_Content>(string email__or__log_in_session_key, Password__Or__Binary_Content password__or__binary_content, string function, bool binary_file)
            {
                return await Initiate_Server_Connection<Password__Or__Binary_Content>(email__or__log_in_session_key as string, password__or__binary_content, function, binary_file);
            }
        }
    }
}
