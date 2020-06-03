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
            this.Start = new System.Windows.Forms.Button();
            this.tSyms = new System.Windows.Forms.TextBox();
            this.tlogin = new System.Windows.Forms.TextBox();
            this.tUri = new System.Windows.Forms.TextBox();
            this.tHost = new System.Windows.Forms.TextBox();
            this.udPrc = new System.Windows.Forms.NumericUpDown();
            this.tPwd1 = new System.Windows.Forms.TextBox();
            this.tPwd2 = new System.Windows.Forms.TextBox();
            this.lpwdc = new System.Windows.Forms.Label();
            this.Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dict = new System.Windows.Forms.TextBox();
            this.usdic = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lpcount = new System.Windows.Forms.Label();
            this.lSpeed = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tResult = new System.Windows.Forms.TextBox();
            this.Pause = new System.Windows.Forms.Button();
            this.tTime = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.usedig = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.udPrc)).BeginInit();
            this.SuspendLayout();
            // 
            // tServer
            // 
            this.tServer.Location = new System.Drawing.Point(22, 23);
            this.tServer.Name = "tServer";
            this.tServer.Size = new System.Drawing.Size(138, 20);
            this.tServer.TabIndex = 0;
            this.tServer.Text = "127.0.0.1";
            // 
            // tPort
            // 
            this.tPort.Location = new System.Drawing.Point(166, 23);
            this.tPort.Name = "tPort";
            this.tPort.Size = new System.Drawing.Size(44, 20);
            this.tPort.TabIndex = 1;
            this.tPort.Text = "80";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 268);
            this.Start.MaximumSize = new System.Drawing.Size(59, 26);
            this.Start.MinimumSize = new System.Drawing.Size(59, 26);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(59, 26);
            this.Start.TabIndex = 3;
            this.Start.Text = "Старт";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // tSyms
            // 
            this.tSyms.Location = new System.Drawing.Point(16, 143);
            this.tSyms.Multiline = true;
            this.tSyms.Name = "tSyms";
            this.tSyms.Size = new System.Drawing.Size(194, 33);
            this.tSyms.TabIndex = 4;
            this.tSyms.Text = "0123456789";
            // 
            // tlogin
            // 
            this.tlogin.Location = new System.Drawing.Point(59, 101);
            this.tlogin.Name = "tlogin";
            this.tlogin.Size = new System.Drawing.Size(151, 20);
            this.tlogin.TabIndex = 8;
            this.tlogin.Text = "mc";
            // 
            // tUri
            // 
            this.tUri.Location = new System.Drawing.Point(59, 49);
            this.tUri.Name = "tUri";
            this.tUri.Size = new System.Drawing.Size(151, 20);
            this.tUri.TabIndex = 9;
            this.tUri.Text = "/";
            // 
            // tHost
            // 
            this.tHost.Location = new System.Drawing.Point(59, 75);
            this.tHost.Name = "tHost";
            this.tHost.Size = new System.Drawing.Size(151, 20);
            this.tHost.TabIndex = 10;
            this.tHost.Text = "localhost";
            // 
            // udPrc
            // 
            this.udPrc.Location = new System.Drawing.Point(115, 221);
            this.udPrc.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udPrc.Name = "udPrc";
            this.udPrc.Size = new System.Drawing.Size(95, 20);
            this.udPrc.TabIndex = 11;
            this.udPrc.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // tPwd1
            // 
            this.tPwd1.Location = new System.Drawing.Point(16, 195);
            this.tPwd1.Name = "tPwd1";
            this.tPwd1.Size = new System.Drawing.Size(93, 20);
            this.tPwd1.TabIndex = 12;
            this.tPwd1.Text = "0000";
            this.tPwd1.TextChanged += new System.EventHandler(this.tPwd1_TextChanged);
            // 
            // tPwd2
            // 
            this.tPwd2.Location = new System.Drawing.Point(115, 195);
            this.tPwd2.Name = "tPwd2";
            this.tPwd2.Size = new System.Drawing.Size(95, 20);
            this.tPwd2.TabIndex = 13;
            this.tPwd2.Text = "9999";
            // 
            // lpwdc
            // 
            this.lpwdc.AutoSize = true;
            this.lpwdc.Location = new System.Drawing.Point(126, 303);
            this.lpwdc.Name = "lpwdc";
            this.lpwdc.Size = new System.Drawing.Size(0, 13);
            this.lpwdc.TabIndex = 14;
            // 
            // Stop
            // 
            this.Stop.Enabled = false;
            this.Stop.Location = new System.Drawing.Point(146, 268);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(64, 26);
            this.Stop.TabIndex = 16;
            this.Stop.Text = "Стоп";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Паролів перевірено:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Поточний пароль:";
            // 
            // lPwd
            // 
            this.lPwd.Location = new System.Drawing.Point(116, 321);
            this.lPwd.Name = "lPwd";
            this.lPwd.ReadOnly = true;
            this.lPwd.Size = new System.Drawing.Size(93, 20);
            this.lPwd.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Початковий:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Кінцевий:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Набір символів:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Хост:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "URI:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Логін:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Сервер:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(163, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Порт:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 223);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Потоків:";
            // 
            // dict
            // 
            this.dict.Location = new System.Drawing.Point(216, 32);
            this.dict.Multiline = true;
            this.dict.Name = "dict";
            this.dict.ReadOnly = true;
            this.dict.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dict.Size = new System.Drawing.Size(213, 309);
            this.dict.TabIndex = 29;
            this.dict.TextChanged += new System.EventHandler(this.dict_TextChanged);
            // 
            // usdic
            // 
            this.usdic.AutoSize = true;
            this.usdic.Location = new System.Drawing.Point(217, 9);
            this.usdic.Name = "usdic";
            this.usdic.Size = new System.Drawing.Size(159, 17);
            this.usdic.TabIndex = 30;
            this.usdic.Text = "Використовувати словник";
            this.usdic.UseVisualStyleBackColor = true;
            this.usdic.CheckedChanged += new System.EventHandler(this.usdic_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 354);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Всього:";
            // 
            // lpcount
            // 
            this.lpcount.AutoSize = true;
            this.lpcount.Location = new System.Drawing.Point(277, 354);
            this.lpcount.Name = "lpcount";
            this.lpcount.Size = new System.Drawing.Size(0, 13);
            this.lpcount.TabIndex = 31;
            // 
            // lSpeed
            // 
            this.lSpeed.AutoSize = true;
            this.lSpeed.Location = new System.Drawing.Point(127, 354);
            this.lSpeed.Name = "lSpeed";
            this.lSpeed.Size = new System.Drawing.Size(0, 13);
            this.lSpeed.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 354);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(102, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "Швидкість підбору:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 376);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 37;
            this.label13.Text = "Результат:";
            // 
            // tResult
            // 
            this.tResult.Location = new System.Drawing.Point(81, 373);
            this.tResult.Name = "tResult";
            this.tResult.ReadOnly = true;
            this.tResult.Size = new System.Drawing.Size(129, 20);
            this.tResult.TabIndex = 38;
            // 
            // Pause
            // 
            this.Pause.Enabled = false;
            this.Pause.Location = new System.Drawing.Point(77, 268);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(64, 26);
            this.Pause.TabIndex = 39;
            this.Pause.Text = "Пауза";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.button1_Click);
            // 
            // tTime
            // 
            this.tTime.AutoSize = true;
            this.tTime.Location = new System.Drawing.Point(301, 380);
            this.tTime.Name = "tTime";
            this.tTime.Size = new System.Drawing.Size(0, 13);
            this.tTime.TabIndex = 41;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(225, 380);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 40;
            this.label16.Text = "Час підбору:";
            // 
            // usedig
            // 
            this.usedig.AutoSize = true;
            this.usedig.Location = new System.Drawing.Point(12, 245);
            this.usedig.Name = "usedig";
            this.usedig.Size = new System.Drawing.Size(136, 17);
            this.usedig.TabIndex = 43;
            this.usedig.Text = "Цифрова авторизація";
            this.usedig.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 408);
            this.Controls.Add(this.usedig);
            this.Controls.Add(this.tTime);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.tResult);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lSpeed);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lpcount);
            this.Controls.Add(this.usdic);
            this.Controls.Add(this.dict);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.lpwdc);
            this.Controls.Add(this.tPwd2);
            this.Controls.Add(this.tPwd1);
            this.Controls.Add(this.udPrc);
            this.Controls.Add(this.tHost);
            this.Controls.Add(this.tUri);
            this.Controls.Add(this.tlogin);
            this.Controls.Add(this.tSyms);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.tPort);
            this.Controls.Add(this.tServer);
            this.Name = "Form1";
            this.Text = "Пароль 0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udPrc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tServer;
        private System.Windows.Forms.TextBox tPort;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox tSyms;
        private System.Windows.Forms.TextBox tlogin;
        private System.Windows.Forms.TextBox tUri;
        private System.Windows.Forms.TextBox tHost;
        private System.Windows.Forms.NumericUpDown udPrc;
        private System.Windows.Forms.TextBox tPwd1;
        private System.Windows.Forms.TextBox tPwd2;
        private System.Windows.Forms.Label lpwdc;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox dict;
        private System.Windows.Forms.CheckBox usdic;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lpcount;
        private System.Windows.Forms.Label lSpeed;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tResult;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Label tTime;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox usedig;
    }
}

