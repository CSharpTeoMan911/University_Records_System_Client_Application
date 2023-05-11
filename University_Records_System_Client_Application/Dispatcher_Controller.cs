using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    internal class Dispatcher_Controller
    {
        private static Main_Dispatcher dispatcher = new Main_Dispatcher();



        public enum Option
        {
            Content_Hasher,
            Load_X509_Certificate_Into_Store,
            Save_Log_In_Key,
            Delete_Log_In_Sesion_Key,
            Load_Log_In_Session_Key,
            Get_Keep_User_Logged_In,
            Set_Keep_User_Logged_In
        }

        internal static async Task<byte[]> Content_Hashing_Controller(string content)
        {
            byte[] result = new byte[1];
            result = await dispatcher.Dispatcher(content);
            return result;
        }



    }
}
