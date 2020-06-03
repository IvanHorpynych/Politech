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
            this.button1 = new System.Windows.Forms.Button();
            this.opf = new System.Windows.Forms.OpenFileDialog();
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
            this.FPath = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FName
            // 
            this.FName.Location = new System.Drawing.Point(433, 453);
            this.FName.Name = "FName";
            this.FName.Size = new System.Drawing.Size(138, 20);
            this.FName.TabIndex = 0;
            this.FName.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Додати файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // opf
            // 
            this.opf.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(196, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Ініціалізувати всі потоки";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(341, 33);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(214, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Зупинити всі потоки і програму вцілому";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // srcline
            // 
            this.srcline.Location = new System.Drawing.Point(8, 9);
            this.srcline.Name = "srcline";
            this.srcline.Size = new System.Drawing.Size(327, 20);
            this.srcline.TabIndex = 6;
            this.srcline.Text = "Введіть текст";
            // 
            // but_ser
            // 
            this.but_ser.Location = new System.Drawing.Point(8, 33);
            this.but_ser.Name = "but_ser";
            this.but_ser.Size = new System.Drawing.Size(95, 23);
            this.but_ser.TabIndex = 7;
            this.but_ser.Text = "Записати слово";
            this.but_ser.UseVisualStyleBackColor = true;
            this.but_ser.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(34, 62);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(521, 160);
            this.listBox1.TabIndex = 8;
            // 
            // fileNames
            // 
            this.fileNames.Location = new System.Drawing.Point(11, 62);
            this.fileNames.Name = "fileNames";
            this.fileNames.Size = new System.Drawing.Size(26, 161);
            this.fileNames.TabIndex = 9;
            this.fileNames.Text = "";
            // 
            // result_box
            // 
            this.result_box.Location = new System.Drawing.Point(10, 228);
            this.result_box.Name = "result_box";
            this.result_box.Size = new System.Drawing.Size(47, 163);
            this.result_box.TabIndex = 10;
            this.result_box.Text = "";
            // 
            // time_box
            // 
            this.time_box.Location = new System.Drawing.Point(63, 228);
            this.time_box.Name = "time_box";
            this.time_box.Size = new System.Drawing.Size(194, 163);
            this.time_box.TabIndex = 11;
            this.time_box.Text = "";
            // 
            // bit_time
            // 
            this.bit_time.Location = new System.Drawing.Point(318, 228);
            this.bit_time.Name = "bit_time";
            this.bit_time.Size = new System.Drawing.Size(237, 163);
            this.bit_time.TabIndex = 13;
            this.bit_time.Text = "";
            // 
            // bit_res
            // 
            this.bit_res.Location = new System.Drawing.Point(263, 228);
            this.bit_res.Name = "bit_res";
            this.bit_res.Size = new System.Drawing.Size(49, 163);
            this.bit_res.TabIndex = 12;
            this.bit_res.Text = "";
            // 
            // FPath
            // 
            this.FPath.Location = new System.Drawing.Point(433, 479);
            this.FPath.Name = "FPath";
            this.FPath.Size = new System.Drawing.Size(138, 20);
            this.FPath.TabIndex = 1;
            this.FPath.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(475, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 20);
            this.textBox1.TabIndex = 14;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(341, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(128, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Додаткове завдання";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 395);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FPath);
            this.Controls.Add(this.FName);
            this.Name = "Form1";
            this.Text = "Лабораторна робота #2 з комп\'ютерної архітектури, частини 1 Степанюка Михайла";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog opf;
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
        private System.Windows.Forms.TextBox FPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
    }
}

