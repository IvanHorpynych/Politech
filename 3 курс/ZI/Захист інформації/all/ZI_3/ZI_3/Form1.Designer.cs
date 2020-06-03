namespace ZI_3
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
            this.path = new System.Windows.Forms.TextBox();
            this.code = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.path2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.decode = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.error1 = new System.Windows.Forms.TextBox();
            this.error2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Len = new System.Windows.Forms.TextBox();
            this.test = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(113, 49);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(178, 20);
            this.path.TabIndex = 2;
            this.path.Text = "pic1.jpg";
            this.path.TextChanged += new System.EventHandler(this.path_TextChanged);
            // 
            // code
            // 
            this.code.Location = new System.Drawing.Point(113, 86);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(178, 20);
            this.code.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(286, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // path2
            // 
            this.path2.Location = new System.Drawing.Point(113, 182);
            this.path2.Name = "path2";
            this.path2.Size = new System.Drawing.Size(178, 20);
            this.path2.TabIndex = 5;
            this.path2.Text = "pic1.jpg";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(286, 245);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // decode
            // 
            this.decode.Location = new System.Drawing.Point(113, 219);
            this.decode.Name = "decode";
            this.decode.Size = new System.Drawing.Size(178, 20);
            this.decode.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 15);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "File Path:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(151, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(211, 15);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "Inserting Secret Data Into File";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(138, 154);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(236, 15);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "Retrieving Secret Data Out Of File";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(8, 86);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(96, 15);
            this.textBox4.TabIndex = 11;
            this.textBox4.Text = " Secret Text:";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox5.Location = new System.Drawing.Point(8, 220);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(99, 15);
            this.textBox5.TabIndex = 13;
            this.textBox5.Text = " Secret Text is:";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(12, 183);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(95, 15);
            this.textBox6.TabIndex = 12;
            this.textBox6.Text = "File Path:";
            // 
            // error1
            // 
            this.error1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.error1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.error1.ForeColor = System.Drawing.Color.Red;
            this.error1.Location = new System.Drawing.Point(309, 50);
            this.error1.Name = "error1";
            this.error1.Size = new System.Drawing.Size(169, 15);
            this.error1.TabIndex = 14;
            this.error1.Text = "File doesn\'t exist!";
            this.error1.Visible = false;
            // 
            // error2
            // 
            this.error2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.error2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.error2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.error2.ForeColor = System.Drawing.Color.Red;
            this.error2.Location = new System.Drawing.Point(309, 183);
            this.error2.Name = "error2";
            this.error2.Size = new System.Drawing.Size(154, 15);
            this.error2.TabIndex = 15;
            this.error2.Text = "File doesn\'t exist!";
            this.error2.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(193, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "FindMaxLen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Len
            // 
            this.Len.Location = new System.Drawing.Point(56, 355);
            this.Len.Name = "Len";
            this.Len.Size = new System.Drawing.Size(100, 20);
            this.Len.TabIndex = 17;
            this.Len.Text = "0";
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(56, 313);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(183, 20);
            this.test.TabIndex = 18;
            this.test.Text = "pic2.jpg";
            this.test.TextChanged += new System.EventHandler(this.test_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 435);
            this.Controls.Add(this.test);
            this.Controls.Add(this.Len);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.error2);
            this.Controls.Add(this.error1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.decode);
            this.Controls.Add(this.path2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.code);
            this.Controls.Add(this.path);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.TextBox code;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox path2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox decode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox error1;
        private System.Windows.Forms.TextBox error2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Len;
        private System.Windows.Forms.TextBox test;


    }
}

