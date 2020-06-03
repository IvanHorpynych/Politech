namespace ZI_lab2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxExcel = new System.Windows.Forms.TextBox();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonExcelDigitsLetters = new System.Windows.Forms.Button();
            this.buttonWordDigitsLetters = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(9, 25);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(121, 20);
            this.textBoxTime.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(9, 75);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(120, 20);
            this.textBoxPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // textBoxExcel
            // 
            this.textBoxExcel.Location = new System.Drawing.Point(302, 28);
            this.textBoxExcel.Name = "textBoxExcel";
            this.textBoxExcel.Size = new System.Drawing.Size(201, 20);
            this.textBoxExcel.TabIndex = 6;
            this.textBoxExcel.TextChanged += new System.EventHandler(this.textBoxExcel_TextChanged_1);
            // 
            // textBoxWord
            // 
            this.textBoxWord.Location = new System.Drawing.Point(302, 75);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(201, 20);
            this.textBoxWord.TabIndex = 7;
            this.textBoxWord.Text = "4";
            this.textBoxWord.TextChanged += new System.EventHandler(this.textBoxWord_TextChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Name of Excel document";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name of Word document";
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(302, 121);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(53, 20);
            this.textBoxLength.TabIndex = 10;
            this.textBoxLength.TextChanged += new System.EventHandler(this.textBoxLength_TextChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Length";
            // 
            // buttonExcelDigitsLetters
            // 
            this.buttonExcelDigitsLetters.Location = new System.Drawing.Point(76, 225);
            this.buttonExcelDigitsLetters.Name = "buttonExcelDigitsLetters";
            this.buttonExcelDigitsLetters.Size = new System.Drawing.Size(118, 46);
            this.buttonExcelDigitsLetters.TabIndex = 14;
            this.buttonExcelDigitsLetters.Text = "Excel Document \r\nDigits and Letters";
            this.buttonExcelDigitsLetters.UseVisualStyleBackColor = true;
            this.buttonExcelDigitsLetters.Click += new System.EventHandler(this.buttonExcelDigitsLetters_Click_1);
            // 
            // buttonWordDigitsLetters
            // 
            this.buttonWordDigitsLetters.Location = new System.Drawing.Point(259, 225);
            this.buttonWordDigitsLetters.Name = "buttonWordDigitsLetters";
            this.buttonWordDigitsLetters.Size = new System.Drawing.Size(118, 46);
            this.buttonWordDigitsLetters.TabIndex = 15;
            this.buttonWordDigitsLetters.Text = "Word Document\r\nDigits and Letters";
            this.buttonWordDigitsLetters.UseVisualStyleBackColor = true;
            this.buttonWordDigitsLetters.Click += new System.EventHandler(this.buttonWordDigitsLetters_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(385, 121);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(75, 17);
            this.radioButton1.TabIndex = 17;
            this.radioButton1.Text = "Max Lenth";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(385, 144);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 17);
            this.radioButton2.TabIndex = 18;
            this.radioButton2.Text = "Current Leth";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 326);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.buttonWordDigitsLetters);
            this.Controls.Add(this.buttonExcelDigitsLetters);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxWord);
            this.Controls.Add(this.textBoxExcel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxTime);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxExcel;
        private System.Windows.Forms.TextBox textBoxWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonExcelDigitsLetters;
        private System.Windows.Forms.Button buttonWordDigitsLetters;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}

