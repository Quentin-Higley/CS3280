using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MessageBox
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowMsgBox_Click(object sender, EventArgs e)
        {
            //get result and output to the corresponding label
            label.Text = ShowMessageBox(0, txtMsgInput.Text);
            return;
        }

        private void btnShowMsgBox2_Click(object sender, EventArgs e)
        {
            //get result and output to the corresponding label
            lblOutput2.Text = ShowMessageBox(1, txtMsgInput2.Text);
            return;
        }

        private void btnShowMsgBox3_Click(object sender, EventArgs e)
        {
            //get result and output to the corresponding label
            lblOutput3.Text = ShowMessageBox(2, txtMsgInput3.Text);
            return;
        }

        private string ShowMessageBox( int MessageBoxType, string message)
        {
            //Declare Default Variables
            const string strBeginingOfMessage = "You Clicked : ";
            MessageBoxButtons[] buttons = new MessageBoxButtons[] { MessageBoxButtons.OKCancel, MessageBoxButtons.AbortRetryIgnore, MessageBoxButtons.CancelTryContinue };
            MessageBoxIcon[] icons = new MessageBoxIcon[] { MessageBoxIcon.Information, MessageBoxIcon.Question, MessageBoxIcon.Error };

            //Show dialog and store the result of the click
            DialogResult result = System.Windows.Forms.MessageBox.Show( message, "", buttons[MessageBoxType], icons[MessageBoxType] );

            //return output of message box
            return strBeginingOfMessage + result.ToString();
        }
    }
}