using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Records_System_Client_Application
{
    // GLOBAL APPLICATION VARIABLES ACCESSED THROUGH
    // SEALED CLASSES THAT USE INTERNAL METHODS


    class Client_Variables
    {
        protected static string email;
        protected static string log_in_session_key;
        protected static bool keep_user_logged_in;

        protected static string endpoint_ip_address = "127.0.0.1";
        protected static int endpoint_port = 1024;


        public enum Functions
        {
            Log_In,
            Register,
            Account_validation,
            Account_log_in,
            Account_log_out,
            Log_in_session_key_validation,
            Insert_Student_Data,
            Insert_Course_Data,
            Delete_Student_Data,
            Delete_Course_Data,
            Update_Student_Data,
            Update_Course_Data,
            Select_Students,
            Select_Courses,
            Select_Grades,
        }

        protected static System.Collections.Generic.Dictionary<Functions, string> Function_string = new System.Collections.Generic.Dictionary<Functions, string>()
        {
            {Functions.Log_In, "Log In"},
            {Functions.Register, "Register"},
            {Functions.Account_validation, "Account validation"},
            {Functions.Account_log_in, "Account log in"},
            {Functions.Account_log_out, "Account log out"},
            {Functions.Log_in_session_key_validation, "Log in session key validation"},
            {Functions.Insert_Student_Data, "Insert student"},
            {Functions.Delete_Student_Data, "Delete student"},
            {Functions.Update_Student_Data, "Update student data"},
            {Functions.Select_Students, "Select students"},
            {Functions.Select_Courses, "Select courses"},
            {Functions.Insert_Course_Data, "Insert course"},
            {Functions.Delete_Course_Data, "Delete course"},
            {Functions.Update_Course_Data, "Update course data"},
            {Functions.Select_Grades, "Select grades"},
        };
    }
}
