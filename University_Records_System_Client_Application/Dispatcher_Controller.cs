using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace University_Records_System_Client_Application
{
    internal class Dispatcher_Controller:Main_Dispatcher
    {
      
        public enum Option
        {
            Content_Hasher,
            Load_X509_Certificate_Into_Store,
            Save_Log_In_Key,
            Delete_Log_In_Sesion_Key,
            Load_Log_In_Session_Key
        }

        internal async Task<byte[]> Content_Hasher_Controller(string content)
        {
            byte[] result = new byte[1];
            result = (await Dispatcher(Option.Content_Hasher, content, null, null)) as byte[];
            return result;
        }


        internal async Task<bool> Load_X509_Certificate_Into_Store_Controller(string certificate_path)
        {
            bool result = false;
            try
            {
                result = (bool)(await Dispatcher(Option.Load_X509_Certificate_Into_Store, certificate_path, null, null));
            }
            catch(Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.Message);
            }
            return result;
        }


        internal async Task<bool> Save_Log_In_Key_Controller(string Email, string log_in_code, bool? keep_user_logged_in)
        {
            bool result = false;
            result = (bool)(await Dispatcher(Option.Save_Log_In_Key, Email, log_in_code, keep_user_logged_in));
            return result;
        }


        internal async Task<bool> Delete_Log_In_Sesion_Key_Controller()
        {
            bool result = false;
            result = (bool)(await Dispatcher(Option.Delete_Log_In_Sesion_Key, null, null, null));
            return result;
        }


        internal async Task<bool> Load_Log_In_Session_Key_Controller()
        {
            bool result = false;
            result = (bool)(await Dispatcher(Option.Load_Log_In_Session_Key, null, null, null));
            return result;
        }
    }
}
