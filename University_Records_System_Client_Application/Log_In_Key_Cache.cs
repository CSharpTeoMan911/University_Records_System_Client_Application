using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    // CLASS THAT IS USED TO BE SERIALIZED IN A JSON FORMAT
    // IN ORDER TO STORE THE APPLICATION'S CREDENTIALS CACHE 
    class Log_In_Key_Cache
    {
        public string email;
        public string log_in_session_key;
    }
}
