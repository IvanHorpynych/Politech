namespace KA4_client
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.LogName = new System.Windows.Forms.TextBox();
            this.LogPass = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.all_messages = new System.Windows.Forms.RichTextBox();
            this.curr_msg = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.history = new System.Windows.Forms.RichTextBox();
            this.online = new System.Windows.Forms.RichTextBox();
            this.ext = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(86, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LogName
            // 
            this.LogName.Location = new System.Drawing.Point(86, 29);
            this.LogName.Name = "LogName";
            this.LogName.Size = new System.Drawing.Size(154, 20);
            this.LogName.TabIndex = 1;
            // 
            // LogPass
            // 
            this.LogPass.Location = new System.Drawing.Point(86, 55);
            this.LogPass.Name = "LogPass";
            this.LogPass.Size = new System.Drawing.Size(154, 20);
            this.LogPass.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Register";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // all_messages
            // 
            this.all_messages.Location = new System.Drawing.Point(287, 3);
            this.all_messages.Name = "all_messages";
            this.all_messages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.all_messages.Size = new System.Drawing.Size(308, 278);
            this.all_messages.TabIndex = 4;
            this.all_messages.Text = "";
            // 
            // curr_msg
            // 
            this.curr_msg.Enabled = false;
            this.curr_msg.Location = new System.Drawing.Point(287, 326);
            this.curr_msg.Name = "curr_msg";
            this.curr_msg.Size = new System.Drawing.Size(308, 123);
            this.curr_msg.TabIndex = 5;
            this.curr_msg.Text = "";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(287, 465);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Send";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(601, 55);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(202, 226);
            this.history.TabIndex = 7;
            this.history.Text = "";
            // 
            // online
            // 
            this.online.Location = new System.Drawing.Point(601, 326);
            this.online.Name = "online";
            this.online.Size = new System.Drawing.Size(202, 123);
            this.online.TabIndex = 8;
            this.online.Text = "";
            // 
            // ext
            // 
            this.ext.Enabled = false;
            this.ext.Location = new System.Drawing.Point(71, 159);
            this.ext.Name = "ext";
            this.ext.Size = new System.Drawing.Size(75, 23);
            this.ext.TabIndex = 9;
            this.ext.Text = "Exit";
            this.ext.UseVisualStyleBackColor = true;
            this.ext.Click += new System.EventHandler(this.ext_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(27, 465);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Show history";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(27, 410);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 39);
            this.button5.TabIndex = 11;
            this.button5.Text = "Change status";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Control;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(27, 237);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 147);
            this.listBox1.TabIndex = 12;
            this.listBox1.Visible = false;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(598, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Users on-line:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(601, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Activity history:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Login:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Passwd:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 523);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ext);
            this.Controls.Add(this.online);
            this.Controls.Add(this.history);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.curr_msg);
            this.Controls.Add(this.all_messages);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LogPass);
            this.Controls.Add(this.LogName);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Close);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox LogName;
        private System.Windows.Forms.TextBox LogPass;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox all_messages;
        private System.Windows.Forms.RichTextBox curr_msg;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox history;
        private System.Windows.Forms.RichTextBox online;
        private System.Windows.Forms.Button ext;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

