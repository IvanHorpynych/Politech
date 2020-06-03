namespace Lab2_Forms
{
    partial class MainForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Eps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IterationRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IterPrecision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IterationCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HordeRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HordePrecision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HordeCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lTextBox = new System.Windows.Forms.TextBox();
            this.rTextBox = new System.Windows.Forms.TextBox();
            this.gridFillButton = new System.Windows.Forms.Button();
            this.lBorderLabel = new System.Windows.Forms.Label();
            this.rBorderLabel = new System.Windows.Forms.Label();
            this.minFxLabel = new System.Windows.Forms.Label();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.maxFxLabel = new System.Windows.Forms.Label();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Eps,
            this.IterationRoot,
            this.IterPrecision,
            this.IterationCount,
            this.HordeRoot,
            this.HordePrecision,
            this.HordeCount});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(750, 161);
            this.dataGridView.TabIndex = 0;
            // 
            // Eps
            // 
            this.Eps.HeaderText = "Precision";
            this.Eps.Name = "Eps";
            this.Eps.ReadOnly = true;
            this.Eps.Width = 75;
            // 
            // IterationRoot
            // 
            this.IterationRoot.HeaderText = "Iteration Root";
            this.IterationRoot.Name = "IterationRoot";
            this.IterationRoot.ReadOnly = true;
            this.IterationRoot.Width = 88;
            // 
            // IterPrecision
            // 
            this.IterPrecision.HeaderText = "Precision of Iteration root";
            this.IterPrecision.Name = "IterPrecision";
            this.IterPrecision.ReadOnly = true;
            this.IterPrecision.Width = 120;
            // 
            // IterationCount
            // 
            this.IterationCount.HeaderText = "Iteration Count";
            this.IterationCount.Name = "IterationCount";
            this.IterationCount.ReadOnly = true;
            this.IterationCount.Width = 93;
            // 
            // HordeRoot
            // 
            this.HordeRoot.HeaderText = "Horde Root";
            this.HordeRoot.Name = "HordeRoot";
            this.HordeRoot.ReadOnly = true;
            this.HordeRoot.Width = 80;
            // 
            // HordePrecision
            // 
            this.HordePrecision.HeaderText = "Precision of Horde root";
            this.HordePrecision.Name = "HordePrecision";
            this.HordePrecision.ReadOnly = true;
            this.HordePrecision.Width = 112;
            // 
            // HordeCount
            // 
            this.HordeCount.HeaderText = "HordeCount";
            this.HordeCount.Name = "HordeCount";
            this.HordeCount.ReadOnly = true;
            this.HordeCount.Width = 89;
            // 
            // lTextBox
            // 
            this.lTextBox.Location = new System.Drawing.Point(258, 179);
            this.lTextBox.Name = "lTextBox";
            this.lTextBox.Size = new System.Drawing.Size(100, 20);
            this.lTextBox.TabIndex = 1;
            // 
            // rTextBox
            // 
            this.rTextBox.Location = new System.Drawing.Point(258, 205);
            this.rTextBox.Name = "rTextBox";
            this.rTextBox.Size = new System.Drawing.Size(100, 20);
            this.rTextBox.TabIndex = 2;
            // 
            // gridFillButton
            // 
            this.gridFillButton.Location = new System.Drawing.Point(257, 282);
            this.gridFillButton.Name = "gridFillButton";
            this.gridFillButton.Size = new System.Drawing.Size(101, 23);
            this.gridFillButton.TabIndex = 3;
            this.gridFillButton.Text = "Go!";
            this.gridFillButton.UseVisualStyleBackColor = true;
            this.gridFillButton.Click += new System.EventHandler(this.gridButtonClick);
            // 
            // lBorderLabel
            // 
            this.lBorderLabel.AutoSize = true;
            this.lBorderLabel.Location = new System.Drawing.Point(194, 182);
            this.lBorderLabel.Name = "lBorderLabel";
            this.lBorderLabel.Size = new System.Drawing.Size(58, 13);
            this.lBorderLabel.TabIndex = 4;
            this.lBorderLabel.Text = "Left border";
            // 
            // rBorderLabel
            // 
            this.rBorderLabel.AutoSize = true;
            this.rBorderLabel.Location = new System.Drawing.Point(187, 208);
            this.rBorderLabel.Name = "rBorderLabel";
            this.rBorderLabel.Size = new System.Drawing.Size(65, 13);
            this.rBorderLabel.TabIndex = 5;
            this.rBorderLabel.Text = "Right border";
            // 
            // minFxLabel
            // 
            this.minFxLabel.AutoSize = true;
            this.minFxLabel.Location = new System.Drawing.Point(205, 234);
            this.minFxLabel.Name = "minFxLabel";
            this.minFxLabel.Size = new System.Drawing.Size(46, 13);
            this.minFxLabel.TabIndex = 13;
            this.minFxLabel.Text = "Min(f\'(x))";
            // 
            // minTextBox
            // 
            this.minTextBox.Location = new System.Drawing.Point(257, 231);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.Size = new System.Drawing.Size(100, 20);
            this.minTextBox.TabIndex = 12;
            // 
            // maxFxLabel
            // 
            this.maxFxLabel.AutoSize = true;
            this.maxFxLabel.Location = new System.Drawing.Point(203, 260);
            this.maxFxLabel.Name = "maxFxLabel";
            this.maxFxLabel.Size = new System.Drawing.Size(49, 13);
            this.maxFxLabel.TabIndex = 15;
            this.maxFxLabel.Text = "Max(f\'(x))";
            // 
            // maxTextBox
            // 
            this.maxTextBox.Location = new System.Drawing.Point(257, 257);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxTextBox.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 52);
            this.label1.TabIndex = 16;
            this.label1.Text = "l1 = -1; r1 = 0\r\nl2 = 0; r2 = 1\r\nm1 = -3; M1 = 6.96\r\nm2 = 1.03; M2 = 3";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 378);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxFxLabel);
            this.Controls.Add(this.maxTextBox);
            this.Controls.Add(this.minFxLabel);
            this.Controls.Add(this.minTextBox);
            this.Controls.Add(this.rBorderLabel);
            this.Controls.Add(this.lBorderLabel);
            this.Controls.Add(this.gridFillButton);
            this.Controls.Add(this.rTextBox);
            this.Controls.Add(this.lTextBox);
            this.Controls.Add(this.dataGridView);
            this.Name = "MainForm";
            this.Text = "Iteration, Horde methods analyzer!";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eps;
        private System.Windows.Forms.DataGridViewTextBoxColumn IterationRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn IterPrecision;
        private System.Windows.Forms.DataGridViewTextBoxColumn IterationCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn HordeRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn HordePrecision;
        private System.Windows.Forms.DataGridViewTextBoxColumn HordeCount;
        private System.Windows.Forms.TextBox lTextBox;
        private System.Windows.Forms.TextBox rTextBox;
        private System.Windows.Forms.Button gridFillButton;
        private System.Windows.Forms.Label lBorderLabel;
        private System.Windows.Forms.Label rBorderLabel;
        private System.Windows.Forms.Label minFxLabel;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.Label maxFxLabel;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label label1;


    }
}

