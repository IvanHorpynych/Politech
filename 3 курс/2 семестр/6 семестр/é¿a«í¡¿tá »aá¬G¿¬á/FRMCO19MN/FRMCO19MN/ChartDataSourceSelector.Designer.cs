namespace FRMCO19MN
{
    partial class ChartDataSourceSelector
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
            this.sbApply = new DevExpress.XtraEditors.SimpleButton();
            this.ceGroupSum = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lcColumnNumber = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ceGroupSum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbApply
            // 
            this.sbApply.Location = new System.Drawing.Point(174, 43);
            this.sbApply.Name = "sbApply";
            this.sbApply.Size = new System.Drawing.Size(75, 23);
            this.sbApply.TabIndex = 0;
            this.sbApply.Text = "Прийняти";
            this.sbApply.Click += new System.EventHandler(this.sbApply_Click);
            // 
            // ceGroupSum
            // 
            this.ceGroupSum.Location = new System.Drawing.Point(0, 20);
            this.ceGroupSum.Name = "ceGroupSum";
            this.ceGroupSum.Properties.Caption = "Чи групувати суми по іншим підприємствам ?";
            this.ceGroupSum.Size = new System.Drawing.Size(249, 19);
            this.ceGroupSum.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(2, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(113, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Номер поля даних :";
            // 
            // lcColumnNumber
            // 
            this.lcColumnNumber.Location = new System.Drawing.Point(118, 4);
            this.lcColumnNumber.Name = "lcColumnNumber";
            this.lcColumnNumber.Size = new System.Drawing.Size(75, 13);
            this.lcColumnNumber.TabIndex = 3;
            this.lcColumnNumber.Tag = "";
            this.lcColumnNumber.Text = "Column Number";
            // 
            // ChartDataSourceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 66);
            this.Controls.Add(this.lcColumnNumber);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.ceGroupSum);
            this.Controls.Add(this.sbApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ChartDataSourceSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштування параметрів діаграми";
            this.Load += new System.EventHandler(this.ChartDataSourceSelector_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChartDataSourceSelector_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ceGroupSum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbApply;
        private DevExpress.XtraEditors.CheckEdit ceGroupSum;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lcColumnNumber;
    }
}