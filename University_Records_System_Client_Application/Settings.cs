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
        private static string settings_file_name = "settings_file.json";

        public enum Option
        {
            email,
            log_in_session_key,
            keep_user_logged_in,
            endpoint_ip_address,
            endpoint_port
        }

        public enum File_Option
        {
            Load_Settings_From_File,
            Update_Settings_File
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

                case Option.endpoint_ip_address:
                    result = endpoint_ip_address;
                    break;

                case Option.endpoint_port:
                    result = endpoint_port;
                    break;
            }

            return Task.FromResult(result);
        }


        public static async Task<bool> Set_Value(Option option, object setter)
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

                case Option.endpoint_ip_address:
                    endpoint_ip_address = (setter.GetType() == endpoint_ip_address.GetType()) ? (string)setter : endpoint_ip_address;
                    break;

                case Option.endpoint_port:
                    endpoint_port = (setter.GetType() == endpoint_port.GetType()) ? (int)setter : endpoint_port;
                    break;
            }

            return true;
        }


        public static async Task<bool> Settings_File_Operation(File_Option file_option)
        {
            switch(file_option)
            {
                case File_Option.Load_Settings_From_File:
                    await Load_Settings_From_File();
                    break;

                case File_Option.Update_Settings_File:
                    await Update_Settings_File();
                    break;
            }

            return true;
        }


        private static Task<bool> Grant_Settings_File_Permissions()
        {
            if (System.IO.File.Exists(settings_file_name) == true)
            {
                System.Security.AccessControl.FileSecurity settings_file_security = System.IO.File.GetAccessControl(settings_file_name);
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Write, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Read, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Delete, System.Security.AccessControl.AccessControlType.Allow));

                System.IO.File.SetAccessControl(settings_file_name, settings_file_security);
            }

            return Task.FromResult(true);
        }





        private async static Task<bool> Create_Settings_File()
        {
            if(System.IO.File.Exists(settings_file_name) == false)
            {
                System.IO.FileStream file_stream = System.IO.File.Create(settings_file_name);

                try
                {

                    Settings_File settings = new Settings_File();

                    settings.email = email;
                    settings.keep_user_logged_in = keep_user_logged_in;
                    settings.endpoint_port = endpoint_port;
                    settings.log_in_session_key = log_in_session_key;
                    settings.endpoint_ip_address = endpoint_ip_address;


                    byte[] serialized_settings = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(settings));
                    await file_stream.WriteAsync(serialized_settings, 0, serialized_settings.Length);
                }
                catch
                {

                }
                finally
                {
                    if(file_stream != null)
                    {

                    }
                }


                await Grant_Settings_File_Permissions();
            }

            return true;
        }





        private async static Task<bool> Update_Settings_File()
        {
            try
            {
                await Grant_Settings_File_Permissions();


                if (System.IO.File.Exists(settings_file_name) == true)
                {
                    System.IO.File.Delete(settings_file_name);
                }


                await Create_Settings_File();
            }
            catch(Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.Message);
            }


            return true;
        }



        private async static Task<bool> Load_Settings_From_File()
        {
            await Grant_Settings_File_Permissions();


            if (System.IO.File.Exists(settings_file_name) == true)
            {

                System.IO.FileStream file_stream = System.IO.File.Open(settings_file_name, System.IO.FileMode.Open);

                try
                {
                    byte[] serialized_file = new byte[file_stream.Length];
                    await file_stream.ReadAsync(serialized_file, 0, serialized_file.Length);

                    string serialized_file_string = Encoding.UTF8.GetString(serialized_file);

                    Settings_File settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings_File>(serialized_file_string);

                    email = settings.email;
                    endpoint_ip_address = settings.endpoint_ip_address;
                    endpoint_port = settings.endpoint_port;
                    keep_user_logged_in = settings.keep_user_logged_in;
                    log_in_session_key = settings.log_in_session_key;
                }
                catch
                {

                }
                finally
                {
                    if (file_stream != null)
                    {
                        file_stream.Dispose();
                    }
                }
            }
            else
            {
                await Create_Settings_File();
            }

            return true;
        }
    }
}
