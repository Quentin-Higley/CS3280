using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for wndAddPassenger.xaml
    /// </summary>
    public partial class wndAddPassenger : Window
    {
        /// <summary>
        /// current mainwindow
        /// </summary>
        MainWindow mw;

        /// <summary>
        /// constructor for the add passenger window
        /// </summary>
        public wndAddPassenger()
        {
            try
            {
                InitializeComponent();
                mw = ((MainWindow)Application.Current.MainWindow);
                cmdSave.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// only allows letters to be input
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void txtLetterInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass">the class</param>
        /// <param name="sMethod">the method</param>
        /// <param name="sMessage">the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        /// <summary>
        /// gathers and passes the passenger info
        /// </summary>
        /// <param name="sender">objectg</param>
        /// <param name="e">event</param>
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] name = { txtFirstName.Text, txtLastName.Text };
                mw.pPass(name);
                this.Close();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// cancels adding passenger
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mw.cancel();
                this.Close();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// handles text checking
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                checkText();
            }
            catch(Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles text chekcing
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event</param>
        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                checkText();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// catches invalid input
        /// </summary>
        /// <exception cref="Exception">Exception</exception>
        private void checkText()
        {
            string strFirstName = txtFirstName.Text;
            string strLastName = txtLastName.Text;
            try
            {
                bool first = txtFirstName.Text.Trim() == "";
                bool last = txtLastName.Text.Trim() == "";
                if(first || last)
                    cmdSave.IsEnabled = false;
                else
                    cmdSave.IsEnabled = true;
            }
            catch (Exception ex)
            {
                cmdSave.IsEnabled = false;
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
