using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
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
using static University_Records_System_Client_Application.Students_Page;

namespace University_Records_System_Client_Application
{
    /// <summary>
    /// Interaction logic for Students_Page.xaml
    /// </summary>
    public partial class Students_Page : Page
    {
        private int OnOff;

        public Students_Page()
        {
            InitializeComponent();
        }

        public class Student_Data
        {
            public string student_ID { get; set; }
            public string full_name { get; set; }
            public string DOB { get; set; }
            public string course_ID { get; set; }
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load_All_Student_Data();
        }

        private void Edit_Grades(object sender, RoutedEventArgs e)
        {
            Student_Data student_data = ((Student_Data)Students_Data_Grid.SelectedItem);

            if(student_data != null)
            {
                Additional_Data_Window data = new Additional_Data_Window(Additional_Data_Window.Page.Grades_Page, student_data.student_ID);
                data.ShowDialog();
            }
        }

        private void Load_All_Students(object sender, RoutedEventArgs e)
        {
            Load_All_Student_Data();
        }

        private async void Load_All_Student_Data()
        {
            Students_Data_Grid.BeginInit();

            Students_Data_Grid.Items.Clear();

            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), "", Client_Variables.Functions.Select_Students);

            Students students = Newtonsoft.Json.JsonConvert.DeserializeObject<Students>(Encoding.UTF8.GetString(result));


            foreach (Student s in students.students)
            {
                if(Name_Filter.IsChecked == true)
                {
                    if (s.full_name == FullName_TextBox.Text)
                    {
                        Students_Data_Grid.Items.Add(new Student_Data { student_ID = s.student_ID, course_ID = s.course_ID, DOB = s.DOB.ToString("dd/MM/yyyy"), full_name = s.full_name });
                    }
                }
                else if (DOB_Filter.IsChecked == true)
                {
                    DateTime selected_date = new DateTime();

                    bool is_valid_date = DateTime.TryParse(DateOfBirth_DatePicker.Text, out selected_date);

                    if(is_valid_date == true)
                    {
                        if (s.DOB == selected_date)
                        {
                            Students_Data_Grid.Items.Add(new Student_Data { student_ID = s.student_ID, course_ID = s.course_ID, DOB = s.DOB.ToString("dd/MM/yyyy"), full_name = s.full_name });
                        }
                    }
                }
                else if (StudentID_Filter.IsChecked == true)
                {
                    if (s.student_ID == StudentID_TextBox.Text)
                    {
                        Students_Data_Grid.Items.Add(new Student_Data { student_ID = s.student_ID, course_ID = s.course_ID, DOB = s.DOB.ToString("dd/MM/yyyy"), full_name = s.full_name });
                    }
                }
                else if (Course_Filter.IsChecked == true)
                {
                    if(s.course_ID == CourseID_TextBox.Text)
                    {
                        Students_Data_Grid.Items.Add(new Student_Data { student_ID = s.student_ID, course_ID = s.course_ID, DOB = s.DOB.ToString("dd/MM/yyyy"), full_name = s.full_name });
                    }
                }
                else if (None_Filter.IsChecked == true)
                {
                    Students_Data_Grid.Items.Add(new Student_Data { student_ID = s.student_ID, course_ID = s.course_ID, DOB = s.DOB.ToString("dd/MM/yyyy"), full_name = s.full_name });
                }
                
            }

            Students_Data_Grid.EndInit();
        }

        private async void Insert_Student(object sender, RoutedEventArgs e)
        {
            if(StudentID_TextBox.Text != String.Empty)
            {
                if (FullName_TextBox.Text != String.Empty)
                {
                    if (CourseID_TextBox.Text != String.Empty)
                    {
                        DateTime selected_date = new DateTime();

                        bool is_valid_date = DateTime.TryParse(DateOfBirth_DatePicker.Text, out selected_date);


                        if (is_valid_date == true)
                        {
                            Student student = new Student();

                            student.full_name = FullName_TextBox.Text;
                            student.student_ID = StudentID_TextBox.Text;
                            student.course_ID = CourseID_TextBox.Text;
                            student.DOB = selected_date;

                            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(student), Client_Variables.Functions.Insert_Student_Data);
                        }
                    }
                }
            }

            Load_All_Student_Data();
        }

        private void Selected_Row(object sender, SelectionChangedEventArgs e)
        {
            Student_Data student_data = (Student_Data)Students_Data_Grid.SelectedItem;

            if(student_data != null)
            {
                FullName_TextBox.Text = student_data.full_name;
                StudentID_TextBox.Text = student_data.student_ID;
                CourseID_TextBox.Text = student_data.course_ID;
                DateOfBirth_DatePicker.Text = student_data.DOB;
            }
        }


        private async void Delete_Student(object sender, RoutedEventArgs e)
        {
            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), StudentID_TextBox.Text, Client_Variables.Functions.Delete_Student_Data);
            Clear_Fields();
            Load_All_Student_Data();
        }

        private void Clear_Fields(object sender, RoutedEventArgs e)
        {
            Clear_Fields();
        }

        private void Clear_Fields()
        {
            FullName_TextBox.Text = String.Empty;
            StudentID_TextBox.Text = String.Empty;
            CourseID_TextBox.Text = String.Empty;
            DateOfBirth_DatePicker.Text = String.Empty;
        }

        private async void Update_Student_Data(object sender, RoutedEventArgs e)
        {
            if (StudentID_TextBox.Text != String.Empty)
            {
                if (FullName_TextBox.Text != String.Empty)
                {
                    if (CourseID_TextBox.Text != String.Empty)
                    {
                        DateTime selected_date = new DateTime();

                        bool is_valid_date = DateTime.TryParse(DateOfBirth_DatePicker.Text, out selected_date);

                        if (is_valid_date == true)
                        {
                            Student student = new Student();

                            student.full_name = FullName_TextBox.Text;
                            student.student_ID = StudentID_TextBox.Text;
                            student.course_ID = CourseID_TextBox.Text;
                            student.DOB = selected_date;

                            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(student), Client_Variables.Functions.Update_Student_Data);
                            Load_All_Student_Data();
                        }
                    }
                }
            }
        }

   

        private void Filter_Menu(object sender, RoutedEventArgs e)
        {
            OnOff++;

            switch(OnOff)
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
