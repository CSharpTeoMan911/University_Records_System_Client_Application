using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        internal static async Task<byte[]> Content_Hasher_Controller(string content)
        {
            byte[] result = new byte[1];
            result = (await dispatcher.Dispatcher(Option.Content_Hasher, content, null, null, null, null, null, null)) as byte[];
            return result;
        }


        internal static async Task<bool> Load_X509_Certificate_Into_Store_Controller(byte[] certificate_binary_data, string certificate_password)
        {
            bool result = false;
            result = Convert.ToBoolean(await dispatcher.Dispatcher(Option.Load_X509_Certificate_Into_Store, null, certificate_binary_data, certificate_password, null, null, null, null));
            return result;
        }


        internal static async Task<bool> Save_Log_In_Key_Controller(string Email, string log_in_code, bool? keep_user_logged_in)
        {
            bool result = false;
            result = Convert.ToBoolean(await dispatcher.Dispatcher(Option.Save_Log_In_Key, null, null, null, Email, log_in_code, keep_user_logged_in, null));
            return result;
        }


        internal static async Task<bool> Delete_Log_In_Sesion_Key_Controller()
        {
            bool result = false;
            result = Convert.ToBoolean(await dispatcher.Dispatcher(Option.Delete_Log_In_Sesion_Key, null, null, null, null, null, null, null));
            return result;
        }


        internal static async Task<bool> Load_Log_In_Session_Key_Controller()
        {
            bool result = false;
            result = Convert.ToBoolean(await dispatcher.Dispatcher(Option.Content_Hasher, null, null, null, null, null, null, null));
            return result;
        }


        internal static async Task<Tuple<object, Type>> Get_Application_Global_Variable(Option option)
        {
            object result = new object();
            result = await dispatcher.Dispatcher(option, null, null, null, null, null, null, null);
            return new Tuple<object, Type>(result, result.GetType());
        }

        internal static async Task<bool> Set_Application_Global_Variable(Option option, object value)
        {
            bool result = false;
            result = Convert.ToBoolean(await dispatcher.Dispatcher(option, null, null, null, null, null, null, value));
            return result;
        }
    }
}
