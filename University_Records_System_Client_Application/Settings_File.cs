using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    internal class Settings_File
    {
        public string email;
        public string log_in_session_key;
        public bool keep_user_logged_in;
        public string endpoint_ip_address;
        public int endpoint_port = 1024;
    }
}
