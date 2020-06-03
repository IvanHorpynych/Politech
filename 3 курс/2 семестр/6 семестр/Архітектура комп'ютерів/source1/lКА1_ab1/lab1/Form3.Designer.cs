namespace lab1
{
    partial class Form3
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
            this.but = new System.Windows.Forms.Button();
            this.butext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // but
            // 
            this.but.Location = new System.Drawing.Point(12, 12);
            this.but.Name = "but";
            this.but.Size = new System.Drawing.Size(75, 23);
            this.but.TabIndex = 0;
            this.but.Text = "button1";
            this.but.UseVisualStyleBackColor = true;
            this.but.Click += new System.EventHandler(this.but_Click);
            // 
            // butext
            // 
            this.butext.Location = new System.Drawing.Point(106, 12);
            this.butext.Name = "butext";
            this.butext.Size = new System.Drawing.Size(75, 23);
            this.butext.TabIndex = 1;
            this.butext.Text = "button1";
            this.butext.UseVisualStyleBackColor = true;
            this.butext.Click += new System.EventHandler(this.butext_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 196);
            this.Controls.Add(this.butext);
            this.Controls.Add(this.but);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button but;
        private System.Windows.Forms.Button butext;
    }
}