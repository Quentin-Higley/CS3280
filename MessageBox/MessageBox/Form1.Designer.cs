namespace MessageBox
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnShowMsgBox = new System.Windows.Forms.Button();
            this.txtMsgInput = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMsgInput2 = new System.Windows.Forms.TextBox();
            this.lblOutput2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMsgInput3 = new System.Windows.Forms.TextBox();
            this.lblOutput3 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnShowMsgBox2 = new System.Windows.Forms.Button();
            this.btnShowMsgBox3 = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnShowMsgBox
            // 
            this.btnShowMsgBox.Location = new System.Drawing.Point(126, 36);
            this.btnShowMsgBox.Name = "btnShowMsgBox";
            this.btnShowMsgBox.Size = new System.Drawing.Size(75, 23);
            this.btnShowMsgBox.TabIndex = 1;
            this.btnShowMsgBox.Text = "Show MessageBox";
            this.btnShowMsgBox.UseVisualStyleBackColor = true;
            this.btnShowMsgBox.Click += new System.EventHandler(this.btnShowMsgBox_Click);
            // 
            // txtMsgInput
            // 
            this.txtMsgInput.Location = new System.Drawing.Point(20, 36);
            this.txtMsgInput.Name = "txtMsgInput";
            this.txtMsgInput.Size = new System.Drawing.Size(100, 23);
            this.txtMsgInput.TabIndex = 0;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(12, 88);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(375, 15);
            this.label.TabIndex = 2;
            this.label.Text = "Show MessageBox with Abort Retry Ignore Buttons and Question Icon";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Show MessageBox with Ok Cancel Buttons and Information Icon";
            // 
            // txtMsgInput2
            // 
            this.txtMsgInput2.Location = new System.Drawing.Point(20, 106);
            this.txtMsgInput2.Name = "txtMsgInput2";
            this.txtMsgInput2.Size = new System.Drawing.Size(100, 23);
            this.txtMsgInput2.TabIndex = 2;
            // 
            // lblOutput2
            // 
            this.lblOutput2.AutoSize = true;
            this.lblOutput2.Location = new System.Drawing.Point(207, 110);
            this.lblOutput2.Name = "lblOutput2";
            this.lblOutput2.Size = new System.Drawing.Size(78, 15);
            this.lblOutput2.TabIndex = 2;
            this.lblOutput2.Text = "You Clicked : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 2;
            // 
            // txtMsgInput3
            // 
            this.txtMsgInput3.Location = new System.Drawing.Point(20, 177);
            this.txtMsgInput3.Name = "txtMsgInput3";
            this.txtMsgInput3.Size = new System.Drawing.Size(100, 23);
            this.txtMsgInput3.TabIndex = 4;
            // 
            // lblOutput3
            // 
            this.lblOutput3.AutoSize = true;
            this.lblOutput3.Location = new System.Drawing.Point(207, 180);
            this.lblOutput3.Name = "lblOutput3";
            this.lblOutput3.Size = new System.Drawing.Size(78, 15);
            this.lblOutput3.TabIndex = 2;
            this.lblOutput3.Text = "You Clicked : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Show MesageBox with Cancel Try Continue and Error Icon";
            // 
            // btnShowMsgBox2
            // 
            this.btnShowMsgBox2.Location = new System.Drawing.Point(126, 106);
            this.btnShowMsgBox2.Name = "btnShowMsgBox2";
            this.btnShowMsgBox2.Size = new System.Drawing.Size(75, 23);
            this.btnShowMsgBox2.TabIndex = 3;
            this.btnShowMsgBox2.Text = "Show";
            this.btnShowMsgBox2.UseVisualStyleBackColor = true;
            this.btnShowMsgBox2.Click += new System.EventHandler(this.btnShowMsgBox2_Click);
            // 
            // btnShowMsgBox3
            // 
            this.btnShowMsgBox3.Location = new System.Drawing.Point(126, 177);
            this.btnShowMsgBox3.Name = "btnShowMsgBox3";
            this.btnShowMsgBox3.Size = new System.Drawing.Size(75, 23);
            this.btnShowMsgBox3.TabIndex = 5;
            this.btnShowMsgBox3.Text = "Show";
            this.btnShowMsgBox3.UseVisualStyleBackColor = true;
            this.btnShowMsgBox3.Click += new System.EventHandler(this.btnShowMsgBox3_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(207, 40);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(78, 15);
            this.lblOutput.TabIndex = 2;
            this.lblOutput.Text = "You Clicked : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 216);
            this.Controls.Add(this.btnShowMsgBox3);
            this.Controls.Add(this.btnShowMsgBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOutput3);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblOutput2);
            this.Controls.Add(this.label);
            this.Controls.Add(this.txtMsgInput3);
            this.Controls.Add(this.txtMsgInput2);
            this.Controls.Add(this.txtMsgInput);
            this.Controls.Add(this.btnShowMsgBox);
            this.Name = "Form1";
            this.Text = "Message Box Practice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnShowMsgBox;
        private TextBox txtMsgInput;
        private Label label;
        private Label label1;
        private TextBox txtMsgInput2;
        private Label lblOutput2;
        private Label label2;
        private TextBox txtMsgInput3;
        private Label lblOutput3;
        private Label label3;
        private Button btnShowMsgBox2;
        private Button btnShowMsgBox3;
        private Label lblOutput;
    }
}