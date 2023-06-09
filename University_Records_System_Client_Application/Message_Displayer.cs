using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace University_Records_System_Client_Application
{
    class Message_Displayer
    {

        // THIS IS A GLOBAL CLASS THAT IS DISPLAYING THE RESULTS OF FUNCTIONS
        // EXECUTED BY THE APPLICATION IN THE FORM OF MESSAGES
        public static void Display_Message(byte[] received_message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                switch(Encoding.UTF8.GetString(received_message))
                {
                    case "Registration successful":
                        MessageBox.Show("Check your email address for your registration code to validate your account. If not validated, your account will be deleted in 2 hours.", "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Account already exist":
                        MessageBox.Show("An account that is registered with this email already exist.", "Email address already exists", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Invalid email address":
                        MessageBox.Show("The email address provided is not valid.", "Invalid email address", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Invalid password":
                        MessageBox.Show("The password provided is not valid.", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Password does not contain the amount of characters required":
                        MessageBox.Show("The password provided is less than 6 characters long", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Email server connection error":
                        MessageBox.Show("Server could not send the registration code to your email address. Please ensure your email address is valid.", "Email server error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Account validation successful":
                        MessageBox.Show("Log in into your account.", "Account validation successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Account validation not successful":
                        MessageBox.Show("Enter a valid account registration code.", "Account validation is not successful", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Log in successful":
                        MessageBox.Show("Check your email address for your one time log in code to log in into your account. The log in code expires in 5 minutes.", "Log in successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Connection error":
                        MessageBox.Show("Cannot communicate with the server. Check your internet connection.", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Account not validated":
                        MessageBox.Show("Account is not validated. Enter the registration code sent to the email address specified for this account.", "Account not validated", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Wrong log in code":
                        MessageBox.Show("Enter a valid log in code.", "Log in unsuccessful", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Invalid log in session key":
                        MessageBox.Show("Please log in again.", "Log in session expired", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Value selection failed":
                        MessageBox.Show("Data could not be loaded", "Check your connection or log in again", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Course already exists":
                        MessageBox.Show("Course already exists", "The course that you entered already exists", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Exceeded maximum duration":
                        MessageBox.Show("Maximum course duration exceeded", "The selected duration for this course is over 7 years", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Value insertion failed":
                        MessageBox.Show("Data could not be inserted", "Check your connection or log in again", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Value inserted":
                        MessageBox.Show("Data inserted successfuly", "Data was inserted succesfuly", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Value deletion failed":
                        MessageBox.Show("Data could not be deleted", "Check your connection or log in again", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Value modification successful":
                        MessageBox.Show("Data modificated successfuly", "Data was modificated succesfuly", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        break;

                    case "Course does not exist":
                        MessageBox.Show("Invalid course", "The course inserted does not exist", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Student already exists":
                        MessageBox.Show("Student already exists", "The student inserted already exists", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Grade already exists":
                        MessageBox.Show("Grade already exists", "The grade inserted already exists", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;

                    case "Grade exceeded maximum value":
                        MessageBox.Show("Maximum grade value exceeded", "The value for this grade exceeds the 100 points maximum value", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;
                }
            });
        }
    }
}
