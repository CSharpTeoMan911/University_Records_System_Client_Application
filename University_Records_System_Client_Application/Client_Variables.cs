using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    // GLOBAL APPLICATION VARIABLES ACCESSED THROUGH
    // SEALED CLASSES THAT USE INTERNAL METHODS


    class Client_Variables
    {
        protected static string email;
        protected static string log_in_session_key;
        protected static bool keep_user_logged_in;

        protected static string endpoint_ip_sddress = "127.0.0.1";
        protected static int endpoint_port = 1024;
    }
}
