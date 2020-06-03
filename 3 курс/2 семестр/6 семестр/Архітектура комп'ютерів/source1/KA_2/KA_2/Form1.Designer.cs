namespace KA_2
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
            this.FName = new System.Windows.Forms.TextBox();
            this.FPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.opf = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.srcline = new System.Windows.Forms.TextBox();
            this.but_ser = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.fileNames = new System.Windows.Forms.RichTextBox();
            this.result_box = new System.Windows.Forms.RichTextBox();
            this.time_box = new System.Windows.Forms.RichTextBox();
            this.bit_time = new System.Windows.Forms.RichTextBox();
            this.bit_res = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // FName
            // 
            this.FName.Location = new System.Drawing.Point(73, 12);
            this.FName.Name = "FName";
            this.FName.Size = new System.Drawing.Size(138, 20);
            this.FName.TabIndex = 0;
            // 
            // FPath
            // 
            this.FPath.Location = new System.Drawing.Point(73, 44);
            this.FPath.Name = "FPath";
            this.FPath.Size = new System.Drawing.Size(138, 20);
            this.FPath.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // opf
            // 
            this.opf.FileName = "openFileDialog1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(599, 214);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(242, 166);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(366, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(366, 44);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // srcline
            // 
            this.srcline.Location = new System.Drawing.Point(73, 87);
            this.srcline.Name = "srcline";
            this.srcline.Size = new System.Drawing.Size(138, 20);
            this.srcline.TabIndex = 6;
            this.srcline.Text = "говорить от всей души";
            // 
            // but_ser
            // 
            this.but_ser.Location = new System.Drawing.Point(228, 85);
            this.but_ser.Name = "but_ser";
            this.but_ser.Size = new System.Drawing.Size(75, 23);
            this.but_ser.TabIndex = 7;
            this.but_ser.Text = "search";
            this.but_ser.UseVisualStyleBackColor = true;
            this.but_ser.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(599, 9);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(299, 199);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // fileNames
            // 
            this.fileNames.Location = new System.Drawing.Point(12, 153);
            this.fileNames.Name = "fileNames";
            this.fileNames.Size = new System.Drawing.Size(134, 248);
            this.fileNames.TabIndex = 9;
            this.fileNames.Text = "";
            // 
            // result_box
            // 
            this.result_box.Location = new System.Drawing.Point(152, 153);
            this.result_box.Name = "result_box";
            this.result_box.Size = new System.Drawing.Size(47, 251);
            this.result_box.TabIndex = 10;
            this.result_box.Text = "";
            // 
            // time_box
            // 
            this.time_box.Location = new System.Drawing.Point(205, 153);
            this.time_box.Name = "time_box";
            this.time_box.Size = new System.Drawing.Size(85, 251);
            this.time_box.TabIndex = 11;
            this.time_box.Text = "";
            // 
            // bit_time
            // 
            this.bit_time.Location = new System.Drawing.Point(351, 153);
            this.bit_time.Name = "bit_time";
            this.bit_time.Size = new System.Drawing.Size(90, 251);
            this.bit_time.TabIndex = 13;
            this.bit_time.Text = "";
            // 
            // bit_res
            // 
            this.bit_res.Location = new System.Drawing.Point(296, 153);
            this.bit_res.Name = "bit_res";
            this.bit_res.Size = new System.Drawing.Size(49, 251);
            this.bit_res.TabIndex = 12;
            this.bit_res.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 413);
            this.Controls.Add(this.bit_time);
            this.Controls.Add(this.bit_res);
            this.Controls.Add(this.time_box);
            this.Controls.Add(this.result_box);
            this.Controls.Add(this.fileNames);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.but_ser);
            this.Controls.Add(this.srcline);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FPath);
            this.Controls.Add(this.FName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FName;
        private System.Windows.Forms.TextBox FPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog opf;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox srcline;
        private System.Windows.Forms.Button but_ser;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox fileNames;
        private System.Windows.Forms.RichTextBox result_box;
        private System.Windows.Forms.RichTextBox time_box;
        private System.Windows.Forms.RichTextBox bit_time;
        private System.Windows.Forms.RichTextBox bit_res;
    }
}

