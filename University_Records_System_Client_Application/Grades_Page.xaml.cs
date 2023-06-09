using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace University_Records_System_Client_Application
{
    /// <summary>
    /// Interaction logic for Grades_Page.xaml
    /// </summary>
    public partial class Grades_Page : Page
    {
        private string ID;

        public class Grade_Data
        {
            public string grade_id { get; set; }
            public string student_ID { get; set; }
            public string course_ID { get; set; }
            public string subject_module { get; set; }
            public string student_grade { get; set; }
        }

        public Grades_Page()
        {
            InitializeComponent();
        }

        public Grades_Page(string student_Id)
        {
            ID = student_Id;
            InitializeComponent();
        }

        private void Selected_Row(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Load_All_Grades(object sender, RoutedEventArgs e)
        {
            Load_Grades();
        }


        private async void Load_Grades()
        {
            Grades_Data_Grid.BeginInit();

            Grades_Data_Grid.Items.Clear();

            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), "", Client_Variables.Functions.Select_Grades);

            System.Diagnostics.Debug.WriteLine(Encoding.UTF8.GetString(result));

            Grades grades = Newtonsoft.Json.JsonConvert.DeserializeObject<Grades>(Encoding.UTF8.GetString(result));

            foreach (Grade g in grades.grades)
            {
                Grades_Data_Grid.Items.Add(new Grade_Data { grade_id = g.grade_id.ToString(), student_ID = g.student_ID, course_ID = g.course_ID, subject_module = g.subject_module, student_grade = g.student_grade.ToString() });
            }

            Grades_Data_Grid.EndInit();
        }



        private void Delete_Grade(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Grade_Data(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Grades_By_Criteria(object sender, RoutedEventArgs e)
        {

        }

        private void Insert_Grade(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Fields(object sender, RoutedEventArgs e)
        {
            GradeID_TextBox.Text = String.Empty;
            StudentID_TextBox.Text = String.Empty;
            CourseID_TextBox.Text = String.Empty;
            Module_TextBox.Text = String.Empty;
            GradeID_TextBox.Text = String.Empty;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Grades();
        }
    }
}
