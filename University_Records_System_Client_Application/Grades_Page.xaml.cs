using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int OnOff;
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
            Grade_Data grade_data = (Grade_Data)Grades_Data_Grid.SelectedItem;

            if (grade_data != null)
            {
                GradeID_TextBox.Text = grade_data.grade_id;
                CourseID_TextBox.Text = grade_data.course_ID;
                Module_TextBox.Text = grade_data.subject_module;
                Grade_TextBox.Text = grade_data.student_grade;
            }
        }

        private void Load_All_Grades(object sender, RoutedEventArgs e)
        {
            Load_Grades();
        }


        private async void Load_Grades()
        {
            Grades_Data_Grid.BeginInit();

            Grades_Data_Grid.Items.Clear();

            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), ID, Client_Variables.Functions.Select_Grades);

            Grades grades = Newtonsoft.Json.JsonConvert.DeserializeObject<Grades>(Encoding.UTF8.GetString(result));

            foreach (Grade g in grades.grades)
            {
                if(GradeID_Filter.IsChecked == true)
                {
                    try
                    {
                        double converted = Convert.ToDouble(GradeID_TextBox.Text);

                        if (Convert.ToInt32(GradeID_TextBox.Text) == g.grade_id)
                        {
                            Grades_Data_Grid.Items.Add(new Grade_Data { grade_id = g.grade_id.ToString(), student_ID = g.student_ID, course_ID = g.course_ID, subject_module = g.subject_module, student_grade = g.student_grade.ToString() });
                        }
                    }
                    catch
                    {

                    }
                }
                else if (CourseID_Filter.IsChecked == true)
                {
                    if(CourseID_TextBox.Text == g.course_ID)
                    {
                        Grades_Data_Grid.Items.Add(new Grade_Data { grade_id = g.grade_id.ToString(), student_ID = g.student_ID, course_ID = g.course_ID, subject_module = g.subject_module, student_grade = g.student_grade.ToString() });
                    }
                }
                else if (Module_Filter.IsChecked == true)
                {
                    if (Module_TextBox.Text == g.subject_module)
                    {
                        Grades_Data_Grid.Items.Add(new Grade_Data { grade_id = g.grade_id.ToString(), student_ID = g.student_ID, course_ID = g.course_ID, subject_module = g.subject_module, student_grade = g.student_grade.ToString() });
                    }
                }
                else if (Grade_Filter.IsChecked == true)
                {
                    try
                    {
                        double converted = Convert.ToDouble(Grade_TextBox.Text);

                        if (Convert.ToInt32(Grade_TextBox.Text) == g.student_grade)
                        {
                            Grades_Data_Grid.Items.Add(new Grade_Data { grade_id = g.grade_id.ToString(), student_ID = g.student_ID, course_ID = g.course_ID, subject_module = g.subject_module, student_grade = g.student_grade.ToString() });
                        }
                    }
                    catch
                    {

                    }
                }
                else if(None_Filter.IsChecked == true)
                {
                    Grades_Data_Grid.Items.Add(new Grade_Data { grade_id = g.grade_id.ToString(), student_ID = g.student_ID, course_ID = g.course_ID, subject_module = g.subject_module, student_grade = g.student_grade.ToString() });
                }
            }

            Grades_Data_Grid.EndInit();
        }



        private async void Delete_Grade(object sender, RoutedEventArgs e)
        {
            if (GradeID_TextBox.Text != String.Empty)
            {
                if (CourseID_TextBox.Text != String.Empty)
                {
                    if (Module_TextBox.Text != String.Empty)
                    {
                        if (Grade_TextBox.Text != String.Empty)
                        {
                            try
                            {
                                double converted = Convert.ToDouble(Grade_TextBox.Text);

                                converted = Convert.ToDouble(GradeID_TextBox.Text);

                                Grade grade = new Grade();

                                grade.grade_id = Convert.ToInt32(GradeID_TextBox.Text);
                                grade.course_ID = CourseID_TextBox.Text;
                                grade.student_ID = ID;
                                grade.subject_module = Module_TextBox.Text;
                                grade.student_grade = Convert.ToInt32(Grade_TextBox.Text);


                                byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(grade), Client_Variables.Functions.Delete_Grades_Data);
                                Clear_Fields();
                                Load_Grades();
                            }
                            catch
                            {
                                Clear_Fields();
                            }
                        }
                    }
                }
            }
        }

        private async void Update_Grade_Data(object sender, RoutedEventArgs e)
        {
            if (GradeID_TextBox.Text != String.Empty)
            {
                if (CourseID_TextBox.Text != String.Empty)
                {
                    if (Module_TextBox.Text != String.Empty)
                    {
                        if (Grade_TextBox.Text != String.Empty)
                        {
                            try
                            {
                                double converted = Convert.ToDouble(Grade_TextBox.Text);

                                converted = Convert.ToDouble(GradeID_TextBox.Text);

                                Grade grade = new Grade();

                                grade.grade_id = Convert.ToInt32(GradeID_TextBox.Text);
                                grade.course_ID = CourseID_TextBox.Text;
                                grade.student_ID = ID;
                                grade.subject_module = Module_TextBox.Text;
                                grade.student_grade = Convert.ToInt32(Grade_TextBox.Text);


                                byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(grade), Client_Variables.Functions.Update_Grade_Data);

                                Load_Grades();
                            }
                            catch
                            {
                                Clear_Fields();
                            }
                        }
                    }
                }
            }
        }

        private void Search_Grades_By_Criteria(object sender, RoutedEventArgs e)
        {

        }

        private async void Insert_Grade(object sender, RoutedEventArgs e)
        {
            if (GradeID_TextBox.Text != String.Empty)
            {
                if (CourseID_TextBox.Text != String.Empty)
                {
                    if (Module_TextBox.Text != String.Empty)
                    {
                        if (Grade_TextBox.Text != String.Empty)
                        {
                            try
                            {
                                double converted = Convert.ToDouble(Grade_TextBox.Text);

                                converted = Convert.ToDouble(GradeID_TextBox.Text);

                                Grade grade = new Grade();

                                grade.grade_id = Convert.ToInt32(GradeID_TextBox.Text);
                                grade.course_ID = CourseID_TextBox.Text;
                                grade.student_ID = ID;
                                grade.subject_module = Module_TextBox.Text;
                                grade.student_grade = Convert.ToInt32(Grade_TextBox.Text);


                                byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(grade), Client_Variables.Functions.Insert_Grade_Data);

                                Load_Grades();
                            }
                            catch
                            {
                                Clear_Fields();
                            }
                        }
                    }
                }
            }
        }

        private void Clear_Fields(object sender, RoutedEventArgs e)
        {
            Clear_Fields();
        }

        private void Clear_Fields()
        {
            GradeID_TextBox.Text = String.Empty;
            CourseID_TextBox.Text = String.Empty;
            Module_TextBox.Text = String.Empty;
            GradeID_TextBox.Text = String.Empty;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Grades();
        }

        private void Filter_Menu(object sender, RoutedEventArgs e)
        {
            OnOff++;

            switch (OnOff)
            {
                case 1:
                    Filters.Height = 80;
                    break;

                case 2:
                    OnOff = 0;
                    Filters.Height = 0;
                    break;
            }
        }
    }
}
