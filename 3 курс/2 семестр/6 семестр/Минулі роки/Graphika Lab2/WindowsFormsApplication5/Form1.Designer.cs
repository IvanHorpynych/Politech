namespace WindowsFormsApplication4
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
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.zoom = new System.Windows.Forms.TrackBar();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.angle2 = new System.Windows.Forms.Label();
            this.angle1 = new System.Windows.Forms.Label();
            this.Opacity = new System.Windows.Forms.Label();
            this.Op = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Op)).BeginInit();
            this.SuspendLayout();
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(12, 12);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(500, 500);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 0;
            // 
            // zoom
            // 
            this.zoom.Location = new System.Drawing.Point(532, 28);
            this.zoom.Maximum = 100;
            this.zoom.Name = "zoom";
            this.zoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.zoom.Size = new System.Drawing.Size(45, 464);
            this.zoom.TabIndex = 2;
            this.zoom.Scroll += new System.EventHandler(this.zoom_Scroll);
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 30;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(537, 495);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(529, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Zoom";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 537);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 67);
            this.button1.TabIndex = 5;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 19);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = -20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(542, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(5, 61);
            this.trackBar2.Maximum = 20;
            this.trackBar2.Minimum = -20;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(543, 45);
            this.trackBar2.TabIndex = 7;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.angle2);
            this.groupBox1.Controls.Add(this.angle1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Location = new System.Drawing.Point(10, 518);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 101);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Movement";
            // 
            // angle2
            // 
            this.angle2.AutoSize = true;
            this.angle2.Location = new System.Drawing.Point(554, 61);
            this.angle2.Name = "angle2";
            this.angle2.Size = new System.Drawing.Size(13, 13);
            this.angle2.TabIndex = 9;
            this.angle2.Text = "0";
            // 
            // angle1
            // 
            this.angle1.AutoSize = true;
            this.angle1.Location = new System.Drawing.Point(554, 19);
            this.angle1.Name = "angle1";
            this.angle1.Size = new System.Drawing.Size(13, 13);
            this.angle1.TabIndex = 8;
            this.angle1.Text = "0";
            // 
            // Opacity
            // 
            this.Opacity.AutoSize = true;
            this.Opacity.Location = new System.Drawing.Point(593, 12);
            this.Opacity.Name = "Opacity";
            this.Opacity.Size = new System.Drawing.Size(43, 13);
            this.Opacity.TabIndex = 10;
            this.Opacity.Text = "Opacity";
            // 
            // Op
            // 
            this.Op.Location = new System.Drawing.Point(593, 36);
            this.Op.Maximum = 100;
            this.Op.Name = "Op";
            this.Op.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Op.Size = new System.Drawing.Size(45, 456);
            this.Op.TabIndex = 11;
            this.Op.Value = 100;
            this.Op.Scroll += new System.EventHandler(this.Op_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(603, 495);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "100";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 635);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Op);
            this.Controls.Add(this.Opacity);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.AnT);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Op)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.TrackBar zoom;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label angle2;
        private System.Windows.Forms.Label angle1;
        private System.Windows.Forms.Label Opacity;
        private System.Windows.Forms.TrackBar Op;
        private System.Windows.Forms.Label label3;
    }
}

