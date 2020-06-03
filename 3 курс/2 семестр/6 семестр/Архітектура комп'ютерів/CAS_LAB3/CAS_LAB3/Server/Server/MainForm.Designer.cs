namespace Server
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClear = new System.Windows.Forms.Button();
            this.labelConnectedClients = new System.Windows.Forms.Label();
            this.listBoxClientList = new System.Windows.Forms.ListBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelMessagesFromClients = new System.Windows.Forms.Label();
            this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();
            this.labelBroadcast = new System.Windows.Forms.Label();
            this.buttonSendMsg = new System.Windows.Forms.Button();
            this.richTextBoxSendMsg = new System.Windows.Forms.RichTextBox();
            this.buttonStopListen = new System.Windows.Forms.Button();
            this.buttonStartListen = new System.Windows.Forms.Button();
            this.labelServerIP = new System.Windows.Forms.Label();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(293, 380);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(63, 24);
            this.btnClear.TabIndex = 34;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labelConnectedClients
            // 
            this.labelConnectedClients.Location = new System.Drawing.Point(319, 39);
            this.labelConnectedClients.Name = "labelConnectedClients";
            this.labelConnectedClients.Size = new System.Drawing.Size(50, 16);
            this.labelConnectedClients.TabIndex = 33;
            this.labelConnectedClients.Text = "Clients :";
            // 
            // listBoxClientList
            // 
            this.listBoxClientList.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxClientList.Location = new System.Drawing.Point(322, 58);
            this.listBoxClientList.Name = "listBoxClientList";
            this.listBoxClientList.ScrollAlwaysVisible = true;
            this.listBoxClientList.Size = new System.Drawing.Size(97, 316);
            this.listBoxClientList.TabIndex = 32;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(44, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxIP.TabIndex = 29;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(206, 12);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(40, 20);
            this.textBoxPort.TabIndex = 18;
            this.textBoxPort.Text = "8000";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(362, 380);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(62, 24);
            this.buttonClose.TabIndex = 28;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelMessagesFromClients
            // 
            this.labelMessagesFromClients.Location = new System.Drawing.Point(5, 39);
            this.labelMessagesFromClients.Name = "labelMessagesFromClients";
            this.labelMessagesFromClients.Size = new System.Drawing.Size(70, 16);
            this.labelMessagesFromClients.TabIndex = 27;
            this.labelMessagesFromClients.Text = "From Clients:";
            // 
            // richTextBoxReceivedMsg
            // 
            this.richTextBoxReceivedMsg.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(8, 58);
            this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
            this.richTextBoxReceivedMsg.ReadOnly = true;
            this.richTextBoxReceivedMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(308, 316);
            this.richTextBoxReceivedMsg.TabIndex = 26;
            this.richTextBoxReceivedMsg.Text = "";
            // 
            // labelBroadcast
            // 
            this.labelBroadcast.Location = new System.Drawing.Point(573, 20);
            this.labelBroadcast.Name = "labelBroadcast";
            this.labelBroadcast.Size = new System.Drawing.Size(192, 16);
            this.labelBroadcast.TabIndex = 25;
            this.labelBroadcast.Text = "Broadcast Message To Clients:";
            this.labelBroadcast.Visible = false;
            // 
            // buttonSendMsg
            // 
            this.buttonSendMsg.Location = new System.Drawing.Point(690, 111);
            this.buttonSendMsg.Name = "buttonSendMsg";
            this.buttonSendMsg.Size = new System.Drawing.Size(88, 24);
            this.buttonSendMsg.TabIndex = 24;
            this.buttonSendMsg.Text = "Send";
            this.buttonSendMsg.Visible = false;
            this.buttonSendMsg.Click += new System.EventHandler(this.buttonSendMsg_Click);
            // 
            // richTextBoxSendMsg
            // 
            this.richTextBoxSendMsg.Location = new System.Drawing.Point(573, 36);
            this.richTextBoxSendMsg.Name = "richTextBoxSendMsg";
            this.richTextBoxSendMsg.Size = new System.Drawing.Size(205, 69);
            this.richTextBoxSendMsg.TabIndex = 23;
            this.richTextBoxSendMsg.Text = "";
            this.richTextBoxSendMsg.Visible = false;
            // 
            // buttonStopListen
            // 
            this.buttonStopListen.Enabled = false;
            this.buttonStopListen.Location = new System.Drawing.Point(350, 9);
            this.buttonStopListen.Name = "buttonStopListen";
            this.buttonStopListen.Size = new System.Drawing.Size(68, 24);
            this.buttonStopListen.TabIndex = 22;
            this.buttonStopListen.Text = "Stop";
            this.buttonStopListen.Click += new System.EventHandler(this.buttonStopListen_Click);
            // 
            // buttonStartListen
            // 
            this.buttonStartListen.Location = new System.Drawing.Point(278, 9);
            this.buttonStartListen.Name = "buttonStartListen";
            this.buttonStartListen.Size = new System.Drawing.Size(66, 24);
            this.buttonStartListen.TabIndex = 24;
            this.buttonStartListen.Text = "Start";
            this.buttonStartListen.Click += new System.EventHandler(this.buttonStartListen_Click);
            // 
            // labelServerIP
            // 
            this.labelServerIP.Location = new System.Drawing.Point(8, 15);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(30, 16);
            this.labelServerIP.TabIndex = 20;
            this.labelServerIP.Text = "IP :";
            // 
            // labelServerPort
            // 
            this.labelServerPort.Location = new System.Drawing.Point(170, 15);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(35, 16);
            this.labelServerPort.TabIndex = 19;
            this.labelServerPort.Text = "Port :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 412);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labelConnectedClients);
            this.Controls.Add(this.listBoxClientList);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelMessagesFromClients);
            this.Controls.Add(this.richTextBoxReceivedMsg);
            this.Controls.Add(this.labelBroadcast);
            this.Controls.Add(this.buttonSendMsg);
            this.Controls.Add(this.richTextBoxSendMsg);
            this.Controls.Add(this.buttonStopListen);
            this.Controls.Add(this.buttonStartListen);
            this.Controls.Add(this.labelServerIP);
            this.Controls.Add(this.labelServerPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "(c)L.B. CAS LAB3 Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label labelConnectedClients;
        private System.Windows.Forms.ListBox listBoxClientList;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelMessagesFromClients;
        private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;
        private System.Windows.Forms.Label labelBroadcast;
        private System.Windows.Forms.Button buttonSendMsg;
        private System.Windows.Forms.RichTextBox richTextBoxSendMsg;
        private System.Windows.Forms.Button buttonStopListen;
        private System.Windows.Forms.Button buttonStartListen;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.Label labelServerPort;
    }
}