using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    internal class Cryptographic_Services_Main_Dispatcher:Application_Cryptographic_Services
    {
        // THE APPLICATIONS DISPATCHER CONTROLLER FOR CRYPTOGRAPHIC FUNCTIONS

        protected static async Task<object> Dispatcher(Cryptographic_Services_Dispatcher_Controller.Option option, object first_setter, object second_setter, object third_setter)
        {
            object result = new object();

            switch (option)
            {
                case Cryptographic_Services_Dispatcher_Controller.Option.Content_Hasher:
                    result = (first_setter != null && first_setter.GetType() == typeof(string)) ? await Content_Hasher(first_setter as string) : result;
                    break;

                case Cryptographic_Services_Dispatcher_Controller.Option.Load_X509_Certificate_Into_Store:
                    result = (first_setter != null && first_setter.GetType() == typeof(string)) ? await Load_X509_Certificate_Into_Store(first_setter as string) : false;
                    break;

                case Cryptographic_Services_Dispatcher_Controller.Option.Save_Log_In_Key:
                    result = (first_setter != null && first_setter.GetType() == typeof(string) && second_setter != null && second_setter.GetType() == typeof(string) && third_setter 
                              != null && third_setter.GetType() == typeof(bool)) ? await Save_Log_In_Key(first_setter as string, second_setter as string, (bool)third_setter) : false;
                    break;

                case Cryptographic_Services_Dispatcher_Controller.Option.Delete_Log_In_Sesion_Key:
                    result = await Delete_Log_In_Sesion_Key();
                    break;

                case Cryptographic_Services_Dispatcher_Controller.Option.Load_Log_In_Session_Key:
                    result = await Load_Log_In_Session_Key();
                    break;
            }

            return result;
        }
    }
}
