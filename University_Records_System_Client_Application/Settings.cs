using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Records_System_Client_Application
{
    internal class Settings:Client_Variables
    {
        public enum Option
        {
            email,
            log_in_session_key,
            keep_user_logged_in,
            endpoint_ip_sddress,
            endpoint_port
        }

        public static Task<object> Get_Value(Option option)
        {
            object result = new object();

            switch(option)
            {
                case Option.email:
                    result = email;
                    break;

                case Option.log_in_session_key:
                    result = log_in_session_key;
                    break;

                case Option.keep_user_logged_in:
                    result = keep_user_logged_in;
                    break;

                case Option.endpoint_ip_sddress:
                    result = endpoint_ip_sddress;
                    break;

                case Option.endpoint_port:
                    result = endpoint_port;
                    break;
            }

            return Task.FromResult(result);
        }


        public static Task<bool> Set_Value(Option option, object setter)
        {
            switch (option)
            {
                case Option.email:
                    email = (setter.GetType() == email.GetType()) ? (string)setter : email;
                    break;

                case Option.log_in_session_key:
                    log_in_session_key = (setter.GetType() == log_in_session_key.GetType()) ? (string)setter : log_in_session_key;
                    break;

                case Option.keep_user_logged_in:
                    keep_user_logged_in = (setter.GetType() == keep_user_logged_in.GetType()) ? (bool)setter : keep_user_logged_in;
                    break;

                case Option.endpoint_ip_sddress:
                    endpoint_ip_sddress = (setter.GetType() == endpoint_ip_sddress.GetType()) ? (string)setter : endpoint_ip_sddress;
                    break;

                case Option.endpoint_port:
                    endpoint_port = (setter.GetType() == endpoint_port.GetType()) ? (int)setter : endpoint_port;
                    break;
            }

            return Task.FromResult(true);
        }



    }
}
