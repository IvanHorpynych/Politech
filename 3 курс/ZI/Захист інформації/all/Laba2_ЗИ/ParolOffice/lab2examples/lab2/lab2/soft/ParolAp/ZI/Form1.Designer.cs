namespace ZI
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
            this.tServer = new System.Windows.Forms.TextBox();
            this.tPort = new System.Windows.Forms.TextBox();
            this.tRequest = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.tAns = new System.Windows.Forms.TextBox();
            this.listen = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbb = new System.Windows.Forms.TextBox();
            this.tlogin = new System.Windows.Forms.TextBox();
            this.tpass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tServer
            // 
            this.tServer.Location = new System.Drawing.Point(16, 18);
            this.tServer.Name = "tServer";
            this.tServer.Size = new System.Drawing.Size(93, 20);
            this.tServer.TabIndex = 0;
            this.tServer.Text = "127.0.0.1";
            // 
            // tPort
            // 
            this.tPort.Location = new System.Drawing.Point(115, 18);
            this.tPort.Name = "tPort";
            this.tPort.Size = new System.Drawing.Size(36, 20);
            this.tPort.TabIndex = 1;
            this.tPort.Text = "80";
            // 
            // tRequest
            // 
            this.tRequest.Location = new System.Drawing.Point(16, 44);
            this.tRequest.Multiline = true;
            this.tRequest.Name = "tRequest";
            this.tRequest.Size = new System.Drawing.Size(211, 73);
            this.tRequest.TabIndex = 2;
            this.tRequest.Text = "GET /index.html HTTP/1.0\r\nHost: localhost\r\n";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(244, 112);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(66, 26);
            this.Start.TabIndex = 3;
            this.Start.Text = "Conn";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // tAns
            // 
            this.tAns.Location = new System.Drawing.Point(16, 123);
            this.tAns.Multiline = true;
            this.tAns.Name = "tAns";
            this.tAns.Size = new System.Drawing.Size(211, 73);
            this.tAns.TabIndex = 4;
            // 
            // listen
            // 
            this.listen.Location = new System.Drawing.Point(244, 18);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(66, 26);
            this.listen.TabIndex = 5;
            this.listen.Text = "Conn";
            this.listen.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tbb
            // 
            this.tbb.Location = new System.Drawing.Point(16, 202);
            this.tbb.Multiline = true;
            this.tbb.Name = "tbb";
            this.tbb.Size = new System.Drawing.Size(211, 73);
            this.tbb.TabIndex = 7;
            // 
            // tlogin
            // 
            this.tlogin.Location = new System.Drawing.Point(244, 158);
            this.tlogin.Name = "tlogin";
            this.tlogin.Size = new System.Drawing.Size(93, 20);
            this.tlogin.TabIndex = 8;
            this.tlogin.Text = "mc";
            // 
            // tpass
            // 
            this.tpass.Location = new System.Drawing.Point(244, 184);
            this.tpass.Name = "tpass";
            this.tpass.Size = new System.Drawing.Size(93, 20);
            this.tpass.TabIndex = 9;
            this.tpass.Text = "1234";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 323);
            this.Controls.Add(this.tpass);
            this.Controls.Add(this.tlogin);
            this.Controls.Add(this.tbb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listen);
            this.Controls.Add(this.tAns);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.tRequest);
            this.Controls.Add(this.tPort);
            this.Controls.Add(this.tServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tServer;
        private System.Windows.Forms.TextBox tPort;
        private System.Windows.Forms.TextBox tRequest;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox tAns;
        private System.Windows.Forms.Button listen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbb;
        private System.Windows.Forms.TextBox tlogin;
        private System.Windows.Forms.TextBox tpass;
    }
}

