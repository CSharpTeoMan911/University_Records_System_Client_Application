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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace University_Records_System_Client_Application
{
    /// <summary>
    /// Interaction logic for Courses_Page.xaml
    /// </summary>
    public partial class Courses_Page : Page
    {
        private int OnOff;
        string course_Id;

        public Courses_Page()
        {
            InitializeComponent();
        }

        public Courses_Page(string course_ID)
        {
            course_Id = course_ID;
            InitializeComponent();
        }


        public class Course_Data
        {
            public string course_ID { get; set; }
            public string course_Department { get; set; }
            public bool postgraduate { get; set; }
            public string location { get; set; }
            public int duration { get; set; }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load_All_Courses_Data();
        }


        private async void Load_All_Courses_Data()
        {
            Courses_Data_Grid.BeginInit();

            Courses_Data_Grid.Items.Clear();

            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), "", Client_Variables.Functions.Select_Courses);

            string result_string = Encoding.UTF8.GetString(result);

            if (result_string != "Value selection failed")
            {
                Courses courses = Newtonsoft.Json.JsonConvert.DeserializeObject<Courses>(Encoding.UTF8.GetString(result));

                foreach (Course c in courses.courses)
                {
                    System.Diagnostics.Debug.WriteLine(c.location);

                    if (Course_ID_Filter.IsChecked == true)
                    {
                        if (CourseID_TextBox.Text == c.course_ID)
                        {
                            Courses_Data_Grid.Items.Add(new Course_Data { course_ID = c.course_ID, course_Department = c.course_Department, postgraduate = c.postgraduate, location = c.location, duration = c.duration });
                        }
                    }
                    else if (Department_Filter.IsChecked == true)
                    {
                        if (Department_TextBox.Text == c.course_Department)
                        {
                            Courses_Data_Grid.Items.Add(new Course_Data { course_ID = c.course_ID, course_Department = c.course_Department, postgraduate = c.postgraduate, location = c.location, duration = c.duration });
                        }
                    }
                    else if (Postgraduate_Filter.IsChecked == true)
                    {
                        bool postgraduate = false;

                        if (Postgraduate_Yes.IsChecked == true)
                        {
                            postgraduate = true;
                        }
                        else if (Postgraduate_No.IsChecked == true)
                        {
                            postgraduate = false;
                        }

                        if (postgraduate == c.postgraduate)
                        {
                            Courses_Data_Grid.Items.Add(new Course_Data { course_ID = c.course_ID, course_Department = c.course_Department, postgraduate = c.postgraduate, location = c.location, duration = c.duration });
                        }
                    }
                    else if (Location_Filter.IsChecked == true)
                    {
                        if (Location_TextBox.Text == c.location)
                        {
                            Courses_Data_Grid.Items.Add(new Course_Data { course_ID = c.course_ID, course_Department = c.course_Department, postgraduate = c.postgraduate, location = c.location, duration = c.duration });
                        }
                    }
                    else if (Duration_Filter.IsChecked == true)
                    {
                        try
                        {
                            double converted = Convert.ToDouble(Duration_TextBox.Text);

                            if (Convert.ToInt32(Duration_TextBox.Text) == c.duration)
                            {
                                Courses_Data_Grid.Items.Add(new Course_Data { course_ID = c.course_ID, course_Department = c.course_Department, postgraduate = c.postgraduate, location = c.location, duration = c.duration });
                            }
                        }
                        catch
                        {

                        }
                    }
                    else if (None_Filter.IsChecked == true)
                    {
                        Courses_Data_Grid.Items.Add(new Course_Data { course_ID = c.course_ID, course_Department = c.course_Department, postgraduate = c.postgraduate, location = c.location, duration = c.duration });
                    }

                }
            }
            else
            {
                Message_Displayer.Display_Message(result);
            }



            Courses_Data_Grid.EndInit();
        }

        private void Load_All_Courses(object sender, RoutedEventArgs e)
        {
            Load_All_Courses_Data();
        }

        private async void Delete_Course(object sender, RoutedEventArgs e)
        {
            byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), CourseID_TextBox.Text, Client_Variables.Functions.Delete_Course_Data);
            Message_Displayer.Display_Message(result);
            Clear_Fields();
            Load_All_Courses_Data();
        }

        private async void Insert_Course(object sender, RoutedEventArgs e)
        {
            if (CourseID_TextBox.Text != String.Empty)
            {
                if (Department_TextBox.Text != String.Empty)
                {
                    if (Location_TextBox.Text != String.Empty)
                    {
                        if(Duration_TextBox.Text != String.Empty)
                        {
                            Course course = new Course();

                            course.course_ID = CourseID_TextBox.Text;
                            course.location = Location_TextBox.Text;
                            course.course_Department = Department_TextBox.Text;

                            try
                            {
                                double converted = Convert.ToDouble(Duration_TextBox.Text);

                                course.duration = Convert.ToInt32(Duration_TextBox.Text);

                                course.location = Location_TextBox.Text;

                                if (Postgraduate_Yes.IsChecked == true)
                                {
                                    course.postgraduate = true;
                                }


                                if (Postgraduate_No.IsChecked == true)
                                {
                                    course.postgraduate = true;
                                }


                                byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(course), Client_Variables.Functions.Insert_Course_Data);
                                Message_Displayer.Display_Message(result);
                            }
                            catch
                            {
                                Clear_Fields();
                            }

                        }
                    }
                }
            }

            Load_All_Courses_Data();
        }

        private async void Update_Course_Data(object sender, RoutedEventArgs e)
        {
            Course course = new Course();

            course.course_ID = CourseID_TextBox.Text;
            course.location = Location_TextBox.Text;
            course.course_Department = Department_TextBox.Text;


            try
            {
                double converted = Convert.ToDouble(Duration_TextBox.Text);

                course.duration = Convert.ToInt32(Duration_TextBox.Text);

                course.location = Location_TextBox.Text;

                if (Postgraduate_Yes.IsChecked == true)
                {
                    course.postgraduate = true;
                }


                if (Postgraduate_No.IsChecked == false)
                {
                    course.postgraduate = true;
                }


                byte[] result = await Server_Connections.Initiate_Server_Connection<string>((await Settings.Get_Value(Settings.Option.log_in_session_key) as string), Newtonsoft.Json.JsonConvert.SerializeObject(course), Client_Variables.Functions.Update_Course_Data);
                Message_Displayer.Display_Message(result);
            }
            catch
            {
                Clear_Fields();
            }

            Load_All_Courses_Data();
        }

        private void Clear_Fields(object sender, RoutedEventArgs e)
        {
            Clear_Fields();
        }

        private void Selected_Row(object sender, SelectionChangedEventArgs e)
        {
            Course_Data course_data = (Course_Data)Courses_Data_Grid.SelectedItem;

            if (course_data != null)
            {
                CourseID_TextBox.Text = course_data.course_ID;
                Department_TextBox.Text = course_data.course_Department;
                Location_TextBox.Text = course_data.location;
                Duration_TextBox.Text = course_data.duration.ToString();

                if(course_data.postgraduate == true)
                {
                    Postgraduate_Yes.IsChecked = true;
                }
                else
                {
                    Postgraduate_No.IsChecked = true;
                }
            }
        }

        private void Clear_Fields()
        {
            CourseID_TextBox.Text = String.Empty;
            Department_TextBox.Text = String.Empty;
            Location_TextBox.Text = String.Empty;
            Duration_TextBox.Text = String.Empty;
        }

        private void Filter_Menu(object sender, RoutedEventArgs e)
        {
            OnOff++;

            switch (OnOff)
            {
                case 1:
                    Filters.Height = 90;
                    break;

                case 2:
                    OnOff = 0;
                    Filters.Height = 0;
                    break;
            }
        }
    }
}
