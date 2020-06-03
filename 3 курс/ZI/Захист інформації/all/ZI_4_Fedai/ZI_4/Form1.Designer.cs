namespace ZI_4
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
            this.button1 = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.TextBox();
            this.Pass = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Cons = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Table = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.DBname = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.UsPWD = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.UsPriv = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.butgrant = new System.Windows.Forms.Button();
            this.UsName = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CMDline = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(21, 150);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Make Connection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(183, 62);
            this.Login.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(176, 21);
            this.Login.TabIndex = 2;
            // 
            // Pass
            // 
            this.Pass.Location = new System.Drawing.Point(182, 113);
            this.Pass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(176, 21);
            this.Pass.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(46, 62);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(99, 14);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "User Login:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(21, 99);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(130, 14);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "User Password:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(24, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(438, 254);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter To MySQL:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(158, 150);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 38);
            this.button2.TabIndex = 8;
            this.button2.Text = "Quit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Cons
            // 
            this.Cons.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Cons.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Cons.Location = new System.Drawing.Point(24, 414);
            this.Cons.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Cons.Name = "Cons";
            this.Cons.Size = new System.Drawing.Size(595, 276);
            this.Cons.TabIndex = 9;
            this.Cons.Text = "";
            this.Cons.TextChanged += new System.EventHandler(this.Cons_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Table);
            this.groupBox2.Controls.Add(this.textBox10);
            this.groupBox2.Controls.Add(this.DBname);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.UsPWD);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.UsPriv);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.butgrant);
            this.groupBox2.Controls.Add(this.UsName);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(524, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(438, 254);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Privileges";
            // 
            // Table
            // 
            this.Table.Location = new System.Drawing.Point(151, 145);
            this.Table.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Table.Name = "Table";
            this.Table.Size = new System.Drawing.Size(176, 22);
            this.Table.TabIndex = 21;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox10.Location = new System.Drawing.Point(18, 144);
            this.textBox10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(99, 23);
            this.textBox10.TabIndex = 20;
            this.textBox10.Text = "Table";
            // 
            // DBname
            // 
            this.DBname.Location = new System.Drawing.Point(151, 116);
            this.DBname.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DBname.Name = "DBname";
            this.DBname.Size = new System.Drawing.Size(176, 22);
            this.DBname.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(18, 115);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(99, 23);
            this.textBox7.TabIndex = 18;
            this.textBox7.Text = "Data Base";
            // 
            // UsPWD
            // 
            this.UsPWD.Location = new System.Drawing.Point(151, 49);
            this.UsPWD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UsPWD.Name = "UsPWD";
            this.UsPWD.Size = new System.Drawing.Size(176, 22);
            this.UsPWD.TabIndex = 17;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox8.Location = new System.Drawing.Point(18, 52);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(99, 14);
            this.textBox8.TabIndex = 16;
            this.textBox8.Text = "User PWD";
            // 
            // UsPriv
            // 
            this.UsPriv.Location = new System.Drawing.Point(151, 82);
            this.UsPriv.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UsPriv.Name = "UsPriv";
            this.UsPriv.Size = new System.Drawing.Size(176, 22);
            this.UsPriv.TabIndex = 15;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(18, 85);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(99, 23);
            this.textBox6.TabIndex = 14;
            this.textBox6.Text = "Privileges";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(151, 187);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 38);
            this.button3.TabIndex = 13;
            this.button3.Text = "Revoke";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // butgrant
            // 
            this.butgrant.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butgrant.Location = new System.Drawing.Point(24, 187);
            this.butgrant.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.butgrant.Name = "butgrant";
            this.butgrant.Size = new System.Drawing.Size(94, 38);
            this.butgrant.TabIndex = 12;
            this.butgrant.Text = "Grant";
            this.butgrant.UseVisualStyleBackColor = true;
            this.butgrant.Click += new System.EventHandler(this.butgrant_Click);
            // 
            // UsName
            // 
            this.UsName.Location = new System.Drawing.Point(151, 15);
            this.UsName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UsName.Name = "UsName";
            this.UsName.Size = new System.Drawing.Size(176, 22);
            this.UsName.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(18, 19);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 14);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "User Name";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(24, 354);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 15);
            this.textBox2.TabIndex = 21;
            this.textBox2.Text = "Command Line";
            // 
            // CMDline
            // 
            this.CMDline.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CMDline.Location = new System.Drawing.Point(133, 351);
            this.CMDline.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CMDline.Name = "CMDline";
            this.CMDline.Size = new System.Drawing.Size(486, 21);
            this.CMDline.TabIndex = 22;
            this.CMDline.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            // 
            // OK
            // 
            this.OK.Enabled = false;
            this.OK.Location = new System.Drawing.Point(636, 351);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(38, 21);
            this.OK.TabIndex = 23;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(675, 378);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(403, 274);
            this.dataGridView1.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1124, 702);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.CMDline);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Cons);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox Cons;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button butgrant;
        private System.Windows.Forms.TextBox UsName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox UsPriv;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox UsPWD;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox DBname;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox CMDline;
        private System.Windows.Forms.Button OK;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Table;
        private System.Windows.Forms.TextBox textBox10;
    }
}

