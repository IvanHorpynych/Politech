namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новийФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.відкритиФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зберегтиФайлЯкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.функціїToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.транслюванняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оперуючіКонструкціїToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вирізатиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копіюватиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститиБагаторівневийБуферToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.порахуватиКількістьЦифрУТекстіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.функціїToolStripMenuItem,
            this.оперуючіКонструкціїToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(610, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новийФайлToolStripMenuItem,
            this.відкритиФайлToolStripMenuItem,
            this.зберегтиФайлЯкToolStripMenuItem,
            this.вихідToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новийФайлToolStripMenuItem
            // 
            this.новийФайлToolStripMenuItem.Name = "новийФайлToolStripMenuItem";
            this.новийФайлToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.новийФайлToolStripMenuItem.Text = "Новий файл";
            this.новийФайлToolStripMenuItem.Click += new System.EventHandler(this.новийФайлToolStripMenuItem_Click);
            // 
            // відкритиФайлToolStripMenuItem
            // 
            this.відкритиФайлToolStripMenuItem.Name = "відкритиФайлToolStripMenuItem";
            this.відкритиФайлToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.відкритиФайлToolStripMenuItem.Text = "Відкрити файл";
            this.відкритиФайлToolStripMenuItem.Click += new System.EventHandler(this.відкритиФайлToolStripMenuItem_Click);
            // 
            // зберегтиФайлЯкToolStripMenuItem
            // 
            this.зберегтиФайлЯкToolStripMenuItem.Name = "зберегтиФайлЯкToolStripMenuItem";
            this.зберегтиФайлЯкToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.зберегтиФайлЯкToolStripMenuItem.Text = "Зберегти файл...";
            this.зберегтиФайлЯкToolStripMenuItem.Click += new System.EventHandler(this.зберегтиФайлЯкToolStripMenuItem_Click);
            // 
            // вихідToolStripMenuItem
            // 
            this.вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            this.вихідToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.вихідToolStripMenuItem.Text = "Вихід";
            this.вихідToolStripMenuItem.Click += new System.EventHandler(this.вихідToolStripMenuItem_Click);
            // 
            // функціїToolStripMenuItem
            // 
            this.функціїToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.транслюванняToolStripMenuItem,
            this.порахуватиКількістьЦифрУТекстіToolStripMenuItem});
            this.функціїToolStripMenuItem.Name = "функціїToolStripMenuItem";
            this.функціїToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.функціїToolStripMenuItem.Text = "Функції";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(465, 308);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(463, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 19);
            this.button1.TabIndex = 2;
            this.button1.Text = "Шукати наступний";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(463, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(146, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(463, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 19);
            this.button2.TabIndex = 4;
            this.button2.Text = "Шукати всі";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(464, 94);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(124, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Включаючи регістр";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(463, 117);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(146, 17);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Не включаючи регістра";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(534, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 19);
            this.button3.TabIndex = 7;
            this.button3.Text = "Очистити";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(610, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // транслюванняToolStripMenuItem
            // 
            this.транслюванняToolStripMenuItem.Name = "транслюванняToolStripMenuItem";
            this.транслюванняToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.транслюванняToolStripMenuItem.Text = "Транслювання";
            this.транслюванняToolStripMenuItem.Click += new System.EventHandler(this.транслюванняToolStripMenuItem_Click);
            // 
            // оперуючіКонструкціїToolStripMenuItem
            // 
            this.оперуючіКонструкціїToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вирізатиToolStripMenuItem,
            this.копіюватиToolStripMenuItem,
            this.вставитиToolStripMenuItem,
            this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem,
            this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem,
            this.очиститиБагаторівневийБуферToolStripMenuItem});
            this.оперуючіКонструкціїToolStripMenuItem.Name = "оперуючіКонструкціїToolStripMenuItem";
            this.оперуючіКонструкціїToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.оперуючіКонструкціїToolStripMenuItem.Text = "Оперуючі конструкції";
            // 
            // вирізатиToolStripMenuItem
            // 
            this.вирізатиToolStripMenuItem.Name = "вирізатиToolStripMenuItem";
            this.вирізатиToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.вирізатиToolStripMenuItem.Text = "Вирізати";
            this.вирізатиToolStripMenuItem.Click += new System.EventHandler(this.вирізатиToolStripMenuItem_Click);
            // 
            // копіюватиToolStripMenuItem
            // 
            this.копіюватиToolStripMenuItem.Name = "копіюватиToolStripMenuItem";
            this.копіюватиToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.копіюватиToolStripMenuItem.Text = "Копіювати";
            this.копіюватиToolStripMenuItem.Click += new System.EventHandler(this.копіюватиToolStripMenuItem_Click);
            // 
            // копіюватиДоБагаторівневогоБуфераToolStripMenuItem
            // 
            this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem.Name = "копіюватиДоБагаторівневогоБуфераToolStripMenuItem";
            this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem.Text = "Копіювати до багаторівневого буфера";
            this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem.Click += new System.EventHandler(this.копіюватиДоБагаторівневогоБуфераToolStripMenuItem_Click);
            // 
            // вставитиToolStripMenuItem
            // 
            this.вставитиToolStripMenuItem.Name = "вставитиToolStripMenuItem";
            this.вставитиToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.вставитиToolStripMenuItem.Text = "Вставити";
            this.вставитиToolStripMenuItem.Click += new System.EventHandler(this.вставитиToolStripMenuItem_Click);
            // 
            // очиститиБагаторівневийБуферToolStripMenuItem
            // 
            this.очиститиБагаторівневийБуферToolStripMenuItem.Name = "очиститиБагаторівневийБуферToolStripMenuItem";
            this.очиститиБагаторівневийБуферToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.очиститиБагаторівневийБуферToolStripMenuItem.Text = "Очистити багаторівневий буфер";
            this.очиститиБагаторівневийБуферToolStripMenuItem.Click += new System.EventHandler(this.очиститиБагаторівневийБуферToolStripMenuItem_Click);
            // 
            // вирізатиДоБагаторівневогоБуфераToolStripMenuItem
            // 
            this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem.Name = "вирізатиДоБагаторівневогоБуфераToolStripMenuItem";
            this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem.Text = "Вирізати до багаторівневого буфера";
            this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem.Click += new System.EventHandler(this.вирізатиДоБагаторівневогоБуфераToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(468, 140);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(141, 160);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Натисніть по елементу";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "буфера щоб вставити";
            // 
            // порахуватиКількістьЦифрУТекстіToolStripMenuItem
            // 
            this.порахуватиКількістьЦифрУТекстіToolStripMenuItem.Name = "порахуватиКількістьЦифрУТекстіToolStripMenuItem";
            this.порахуватиКількістьЦифрУТекстіToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.порахуватиКількістьЦифрУТекстіToolStripMenuItem.Text = "Порахувати кількість чисел у тексті";
            this.порахуватиКількістьЦифрУТекстіToolStripMenuItem.Click += new System.EventHandler(this.порахуватиКількістьЦифрУТекстіToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 359);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Лабораторна робота №1 з Архітектури комп\'ютерів, частини 1 Степанюка Михайла";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новийФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem відкритиФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зберегтиФайлЯкToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вихідToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem функціїToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem транслюванняToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оперуючіКонструкціїToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вирізатиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копіюватиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вирізатиДоБагаторівневогоБуфераToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копіюватиДоБагаторівневогоБуфераToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститиБагаторівневийБуферToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem порахуватиКількістьЦифрУТекстіToolStripMenuItem;
    }
}

