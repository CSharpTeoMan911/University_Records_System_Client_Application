using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace University_Records_System_Client_Application
{
    internal class Main_Dispatcher:Application_Cryptographic_Services
    {


        internal async Task<object> Dispatcher(Dispatcher_Controller.Option option, string content, byte[] certificate_binary_data, string certificate_password, string Email, string log_in_code, bool? keep_user_logged_in, object setter)
        {
            object result = new object();

            switch(option)
            {
                case Dispatcher_Controller.Option.Content_Hasher:
                    result = (content != null) ? await Content_Hasher(content) : result;
                    break;

                case Dispatcher_Controller.Option.Load_X509_Certificate_Into_Store:
                    result = (content != null & certificate_password != null) ? await Load_X509_Certificate_Into_Store(certificate_binary_data, certificate_password) : result;
                    break;

                case Dispatcher_Controller.Option.Save_Log_In_Key:
                    result = (keep_user_logged_in != null) ? await Save_Log_In_Key(Email, log_in_code, Convert.ToBoolean(keep_user_logged_in)) : result;
                    break;

                case Dispatcher_Controller.Option.Delete_Log_In_Sesion_Key:
                    result = await Delete_Log_In_Sesion_Key();
                    break;

                case Dispatcher_Controller.Option.Load_Log_In_Session_Key:
                    result = await Load_Log_In_Session_Key();
                    break;

                case Dispatcher_Controller.Option.Get_Keep_User_Logged_In:
                    result = keep_user_logged_in;
                    break;

                case Dispatcher_Controller.Option.Set_Keep_User_Logged_In:
                    keep_user_logged_in = (setter.GetType() == typeof(bool)) ? Convert.ToBoolean(setter) : keep_user_logged_in;
                    result = Convert.ToBoolean(true);
                    break;
            }

            return result;
        }



    }
}
