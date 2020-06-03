using CoreLab.Oracle;           //  OraDirect для связи с базой Oracle


namespace WindowsApplication1
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode4 = new DevExpress.XtraGrid.GridLevelNode();
            this.bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BV2_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV2_1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BV2_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV2_2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BV2_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV2_3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.BV3_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV3_1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.BV3_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV3_2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridView4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV4_1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV4_2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV4_3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV4_4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV4_5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV4_6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridView5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV5_1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV5_2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV5_3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV5_4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV5_5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand18 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand22 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand23 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CV1_12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.lE_ListTabl = new DevExpress.XtraEditors.LookUpEdit();
            this.AddRow = new System.Windows.Forms.Button();
            this.lE_Comp = new DevExpress.XtraEditors.LookUpEdit();
            this.lE_Dep = new DevExpress.XtraEditors.LookUpEdit();
            this.DelRow = new System.Windows.Forms.Button();
            this.SaveTable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lE_ListTabl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lE_Comp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lE_Dep.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bandedGridView2
            // 
            this.bandedGridView2.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridView2.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridView2.Appearance.GroupPanel.Options.UseFont = true;
            this.bandedGridView2.Appearance.GroupPanel.Options.UseForeColor = true;
            this.bandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BV2_1,
            this.BV2_2,
            this.BV2_3});
            this.bandedGridView2.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.CV2_1,
            this.CV2_2,
            this.CV2_3});
            this.bandedGridView2.GridControl = this.gridControl1;
            this.bandedGridView2.GroupPanelText = "Список підприємств";
            this.bandedGridView2.Name = "bandedGridView2";
            // 
            // BV2_1
            // 
            this.BV2_1.AppearanceHeader.Options.UseTextOptions = true;
            this.BV2_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BV2_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BV2_1.Caption = "Код ЄДРПОУ";
            this.BV2_1.Columns.Add(this.CV2_1);
            this.BV2_1.Name = "BV2_1";
            this.BV2_1.OptionsBand.FixedWidth = true;
            this.BV2_1.Width = 77;
            // 
            // CV2_1
            // 
            this.CV2_1.AppearanceCell.Options.UseTextOptions = true;
            this.CV2_1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV2_1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV2_1.AppearanceHeader.Options.UseTextOptions = true;
            this.CV2_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV2_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV2_1.Caption = "1";
            this.CV2_1.FieldName = "ID_COMP";
            this.CV2_1.Name = "CV2_1";
            this.CV2_1.OptionsColumn.FixedWidth = true;
            this.CV2_1.Visible = true;
            this.CV2_1.Width = 77;
            // 
            // BV2_2
            // 
            this.BV2_2.AppearanceHeader.Options.UseTextOptions = true;
            this.BV2_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BV2_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BV2_2.Caption = "Назва";
            this.BV2_2.Columns.Add(this.CV2_2);
            this.BV2_2.Name = "BV2_2";
            this.BV2_2.OptionsBand.FixedWidth = true;
            this.BV2_2.Width = 427;
            // 
            // CV2_2
            // 
            this.CV2_2.AppearanceCell.Options.UseTextOptions = true;
            this.CV2_2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV2_2.AppearanceHeader.Options.UseTextOptions = true;
            this.CV2_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV2_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV2_2.Caption = "2";
            this.CV2_2.FieldName = "NAME";
            this.CV2_2.Name = "CV2_2";
            this.CV2_2.Visible = true;
            this.CV2_2.Width = 427;
            // 
            // BV2_3
            // 
            this.BV2_3.AppearanceHeader.Options.UseTextOptions = true;
            this.BV2_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BV2_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BV2_3.Caption = "Адреса";
            this.BV2_3.Columns.Add(this.CV2_3);
            this.BV2_3.Name = "BV2_3";
            this.BV2_3.OptionsBand.FixedWidth = true;
            this.BV2_3.Width = 461;
            // 
            // CV2_3
            // 
            this.CV2_3.AppearanceCell.Options.UseTextOptions = true;
            this.CV2_3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV2_3.AppearanceHeader.Options.UseTextOptions = true;
            this.CV2_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV2_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV2_3.Caption = "3";
            this.CV2_3.FieldName = "ADDRESS";
            this.CV2_3.Name = "CV2_3";
            this.CV2_3.Visible = true;
            this.CV2_3.Width = 461;
            // 
            // gridControl1
            // 
            gridLevelNode1.LevelTemplate = this.bandedGridView2;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.LevelTemplate = this.bandedGridView3;
            gridLevelNode2.RelationName = "Level2";
            gridLevelNode3.LevelTemplate = this.bandedGridView4;
            gridLevelNode3.RelationName = "Level3";
            gridLevelNode4.LevelTemplate = this.bandedGridView5;
            gridLevelNode4.RelationName = "Level4";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2,
            gridLevelNode3,
            gridLevelNode4});
            this.gridControl1.Location = new System.Drawing.Point(12, 64);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(983, 397);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView3,
            this.bandedGridView4,
            this.bandedGridView5,
            this.bandedGridView1,
            this.bandedGridView2});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // bandedGridView3
            // 
            this.bandedGridView3.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridView3.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridView3.Appearance.GroupPanel.Options.UseFont = true;
            this.bandedGridView3.Appearance.GroupPanel.Options.UseForeColor = true;
            this.bandedGridView3.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.BV3_1,
            this.BV3_2});
            this.bandedGridView3.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.CV3_1,
            this.CV3_2});
            this.bandedGridView3.GridControl = this.gridControl1;
            this.bandedGridView3.GroupPanelText = "Список посад";
            this.bandedGridView3.Name = "bandedGridView3";
            // 
            // BV3_1
            // 
            this.BV3_1.AppearanceHeader.Options.UseTextOptions = true;
            this.BV3_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BV3_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BV3_1.Caption = "Код посади";
            this.BV3_1.Columns.Add(this.CV3_1);
            this.BV3_1.Name = "BV3_1";
            this.BV3_1.Width = 231;
            // 
            // CV3_1
            // 
            this.CV3_1.AppearanceHeader.Options.UseTextOptions = true;
            this.CV3_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV3_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV3_1.Caption = "1";
            this.CV3_1.FieldName = "ID_POS";
            this.CV3_1.Name = "CV3_1";
            this.CV3_1.Visible = true;
            this.CV3_1.Width = 231;
            // 
            // BV3_2
            // 
            this.BV3_2.AppearanceHeader.Options.UseTextOptions = true;
            this.BV3_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BV3_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.BV3_2.Caption = "Назва посади";
            this.BV3_2.Columns.Add(this.CV3_2);
            this.BV3_2.Name = "BV3_2";
            this.BV3_2.Width = 570;
            // 
            // CV3_2
            // 
            this.CV3_2.AppearanceHeader.Options.UseTextOptions = true;
            this.CV3_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV3_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV3_2.Caption = "2";
            this.CV3_2.FieldName = "NAME";
            this.CV3_2.Name = "CV3_2";
            this.CV3_2.Visible = true;
            this.CV3_2.Width = 570;
            // 
            // bandedGridView4
            // 
            this.bandedGridView4.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridView4.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridView4.Appearance.GroupPanel.Options.UseFont = true;
            this.bandedGridView4.Appearance.GroupPanel.Options.UseForeColor = true;
            this.bandedGridView4.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5,
            this.gridBand6,
            this.gridBand7});
            this.bandedGridView4.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.CV4_1,
            this.CV4_2,
            this.CV4_3,
            this.CV4_4,
            this.CV4_5,
            this.CV4_6});
            this.bandedGridView4.GridControl = this.gridControl1;
            this.bandedGridView4.GroupPanelText = "Список посад по підприємствам";
            this.bandedGridView4.Name = "bandedGridView4";
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand2.Caption = "Код ЄДРПОУ";
            this.gridBand2.Columns.Add(this.CV4_1);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 104;
            // 
            // CV4_1
            // 
            this.CV4_1.AppearanceHeader.Options.UseTextOptions = true;
            this.CV4_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV4_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV4_1.Caption = "1";
            this.CV4_1.FieldName = "ID_COMP";
            this.CV4_1.Name = "CV4_1";
            this.CV4_1.Visible = true;
            this.CV4_1.Width = 104;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand3.Caption = "Назва підприємства";
            this.gridBand3.Columns.Add(this.CV4_2);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.Width = 194;
            // 
            // CV4_2
            // 
            this.CV4_2.AppearanceHeader.Options.UseTextOptions = true;
            this.CV4_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV4_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV4_2.Caption = "2";
            this.CV4_2.FieldName = "COMP_NAME";
            this.CV4_2.Name = "CV4_2";
            this.CV4_2.Visible = true;
            this.CV4_2.Width = 194;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand4.Caption = "Код посади";
            this.gridBand4.Columns.Add(this.CV4_3);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.Width = 109;
            // 
            // CV4_3
            // 
            this.CV4_3.AppearanceHeader.Options.UseTextOptions = true;
            this.CV4_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV4_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV4_3.Caption = "3";
            this.CV4_3.FieldName = "ID_POS";
            this.CV4_3.Name = "CV4_3";
            this.CV4_3.Visible = true;
            this.CV4_3.Width = 109;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand5.Caption = "Назва посади";
            this.gridBand5.Columns.Add(this.CV4_4);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.Width = 183;
            // 
            // CV4_4
            // 
            this.CV4_4.AppearanceHeader.Options.UseTextOptions = true;
            this.CV4_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV4_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV4_4.Caption = "4";
            this.CV4_4.FieldName = "POS_NAME";
            this.CV4_4.Name = "CV4_4";
            this.CV4_4.Visible = true;
            this.CV4_4.Width = 183;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand6.Caption = "Планова кількість";
            this.gridBand6.Columns.Add(this.CV4_5);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.Width = 107;
            // 
            // CV4_5
            // 
            this.CV4_5.AppearanceHeader.Options.UseTextOptions = true;
            this.CV4_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV4_5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV4_5.Caption = "5";
            this.CV4_5.FieldName = "PLAN_COUNT";
            this.CV4_5.Name = "CV4_5";
            this.CV4_5.Visible = true;
            this.CV4_5.Width = 107;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand7.Caption = "Наявна кількість";
            this.gridBand7.Columns.Add(this.CV4_6);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.Width = 104;
            // 
            // CV4_6
            // 
            this.CV4_6.AppearanceHeader.Options.UseTextOptions = true;
            this.CV4_6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV4_6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV4_6.Caption = "6";
            this.CV4_6.FieldName = "FACT_COUNT";
            this.CV4_6.Name = "CV4_6";
            this.CV4_6.Visible = true;
            this.CV4_6.Width = 104;
            // 
            // bandedGridView5
            // 
            this.bandedGridView5.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridView5.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridView5.Appearance.GroupPanel.Options.UseFont = true;
            this.bandedGridView5.Appearance.GroupPanel.Options.UseForeColor = true;
            this.bandedGridView5.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand8,
            this.gridBand9,
            this.gridBand10,
            this.gridBand11,
            this.gridBand12});
            this.bandedGridView5.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.CV5_1,
            this.CV5_2,
            this.CV5_3,
            this.CV5_4,
            this.CV5_5});
            this.bandedGridView5.GridControl = this.gridControl1;
            this.bandedGridView5.GroupPanelText = "Список відділів по підприємствам";
            this.bandedGridView5.Name = "bandedGridView5";
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand8.Caption = "Код ЄДРПОУ";
            this.gridBand8.Columns.Add(this.CV5_1);
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.Width = 75;
            // 
            // CV5_1
            // 
            this.CV5_1.AppearanceHeader.Options.UseTextOptions = true;
            this.CV5_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV5_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV5_1.Caption = "1";
            this.CV5_1.FieldName = "ID_COMP";
            this.CV5_1.Name = "CV5_1";
            this.CV5_1.Visible = true;
            // 
            // gridBand9
            // 
            this.gridBand9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand9.Caption = "Назва підприємства";
            this.gridBand9.Columns.Add(this.CV5_2);
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.Width = 75;
            // 
            // CV5_2
            // 
            this.CV5_2.AppearanceHeader.Options.UseTextOptions = true;
            this.CV5_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV5_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV5_2.Caption = "2";
            this.CV5_2.FieldName = "COMP_NAME";
            this.CV5_2.Name = "CV5_2";
            this.CV5_2.Visible = true;
            // 
            // gridBand10
            // 
            this.gridBand10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand10.Caption = "Код відділу";
            this.gridBand10.Columns.Add(this.CV5_3);
            this.gridBand10.Name = "gridBand10";
            this.gridBand10.Width = 75;
            // 
            // CV5_3
            // 
            this.CV5_3.AppearanceHeader.Options.UseTextOptions = true;
            this.CV5_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV5_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV5_3.Caption = "3";
            this.CV5_3.FieldName = "ID_DEP";
            this.CV5_3.Name = "CV5_3";
            this.CV5_3.Visible = true;
            // 
            // gridBand11
            // 
            this.gridBand11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand11.Caption = "Назва відділу";
            this.gridBand11.Columns.Add(this.CV5_4);
            this.gridBand11.Name = "gridBand11";
            this.gridBand11.Width = 75;
            // 
            // CV5_4
            // 
            this.CV5_4.AppearanceHeader.Options.UseTextOptions = true;
            this.CV5_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV5_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV5_4.Caption = "4";
            this.CV5_4.FieldName = "DEP_NAME";
            this.CV5_4.Name = "CV5_4";
            this.CV5_4.Visible = true;
            // 
            // gridBand12
            // 
            this.gridBand12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand12.Caption = "Шифр відділу";
            this.gridBand12.Columns.Add(this.CV5_5);
            this.gridBand12.Name = "gridBand12";
            this.gridBand12.Width = 75;
            // 
            // CV5_5
            // 
            this.CV5_5.AppearanceHeader.Options.UseTextOptions = true;
            this.CV5_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV5_5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV5_5.Caption = "5";
            this.CV5_5.FieldName = "SHIFR";
            this.CV5_5.Name = "CV5_5";
            this.CV5_5.Visible = true;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridView1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.bandedGridView1.Appearance.GroupPanel.Options.UseForeColor = true;
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand13,
            this.gridBand14,
            this.gridBand15,
            this.gridBand16,
            this.gridBand17,
            this.gridBand18,
            this.gridBand19,
            this.gridBand20,
            this.gridBand21,
            this.gridBand22,
            this.gridBand23});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.CV1_1,
            this.CV1_2,
            this.CV1_3,
            this.CV1_4,
            this.CV1_5,
            this.CV1_6,
            this.CV1_7,
            this.CV1_8,
            this.CV1_9,
            this.CV1_10,
            this.CV1_11,
            this.CV1_12});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.GroupPanelText = "Список співробітників";
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand1.Caption = "Код відділу";
            this.gridBand1.Columns.Add(this.CV1_1);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.RowCount = 2;
            this.gridBand1.Width = 53;
            // 
            // CV1_1
            // 
            this.CV1_1.AppearanceCell.Options.UseTextOptions = true;
            this.CV1_1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_1.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_1.Caption = "1";
            this.CV1_1.FieldName = "ID_DEP";
            this.CV1_1.Name = "CV1_1";
            this.CV1_1.Visible = true;
            this.CV1_1.Width = 53;
            // 
            // gridBand13
            // 
            this.gridBand13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand13.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand13.Caption = "Назва відділу";
            this.gridBand13.Columns.Add(this.CV1_2);
            this.gridBand13.Name = "gridBand13";
            this.gridBand13.Width = 106;
            // 
            // CV1_2
            // 
            this.CV1_2.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_2.Caption = "2";
            this.CV1_2.FieldName = "DEP_NAME";
            this.CV1_2.Name = "CV1_2";
            this.CV1_2.Visible = true;
            this.CV1_2.Width = 106;
            // 
            // gridBand14
            // 
            this.gridBand14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand14.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand14.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand14.Caption = "Код співробітника";
            this.gridBand14.Columns.Add(this.CV1_3);
            this.gridBand14.Name = "gridBand14";
            this.gridBand14.Width = 79;
            // 
            // CV1_3
            // 
            this.CV1_3.AppearanceCell.Options.UseTextOptions = true;
            this.CV1_3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_3.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_3.Caption = "3";
            this.CV1_3.FieldName = "ID_EMP";
            this.CV1_3.Name = "CV1_3";
            this.CV1_3.Visible = true;
            this.CV1_3.Width = 79;
            // 
            // gridBand15
            // 
            this.gridBand15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand15.Caption = "Прізвище";
            this.gridBand15.Columns.Add(this.CV1_4);
            this.gridBand15.Name = "gridBand15";
            this.gridBand15.Width = 122;
            // 
            // CV1_4
            // 
            this.CV1_4.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_4.Caption = "4";
            this.CV1_4.FieldName = "EMP_SURNAME";
            this.CV1_4.Name = "CV1_4";
            this.CV1_4.Visible = true;
            this.CV1_4.Width = 122;
            // 
            // gridBand16
            // 
            this.gridBand16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand16.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand16.Caption = "Ім\'я";
            this.gridBand16.Columns.Add(this.CV1_5);
            this.gridBand16.Name = "gridBand16";
            this.gridBand16.Width = 103;
            // 
            // CV1_5
            // 
            this.CV1_5.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_5.Caption = "5";
            this.CV1_5.FieldName = "EMP_NAME";
            this.CV1_5.Name = "CV1_5";
            this.CV1_5.Visible = true;
            this.CV1_5.Width = 103;
            // 
            // gridBand17
            // 
            this.gridBand17.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand17.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand17.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand17.Caption = "По батькові";
            this.gridBand17.Columns.Add(this.CV1_6);
            this.gridBand17.Name = "gridBand17";
            this.gridBand17.Width = 100;
            // 
            // CV1_6
            // 
            this.CV1_6.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_6.Caption = "6";
            this.CV1_6.FieldName = "EMP_FATHNAME";
            this.CV1_6.Name = "CV1_6";
            this.CV1_6.Visible = true;
            this.CV1_6.Width = 100;
            // 
            // gridBand18
            // 
            this.gridBand18.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand18.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand18.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand18.Caption = "Код посади";
            this.gridBand18.Columns.Add(this.CV1_7);
            this.gridBand18.Name = "gridBand18";
            this.gridBand18.Width = 56;
            // 
            // CV1_7
            // 
            this.CV1_7.AppearanceCell.Options.UseTextOptions = true;
            this.CV1_7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_7.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_7.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_7.Caption = "7";
            this.CV1_7.FieldName = "ID_POS";
            this.CV1_7.Name = "CV1_7";
            this.CV1_7.Visible = true;
            this.CV1_7.Width = 56;
            // 
            // gridBand19
            // 
            this.gridBand19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand19.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand19.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand19.Caption = "Назва посади";
            this.gridBand19.Columns.Add(this.CV1_8);
            this.gridBand19.Name = "gridBand19";
            this.gridBand19.Width = 103;
            // 
            // CV1_8
            // 
            this.CV1_8.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_8.Caption = "8";
            this.CV1_8.FieldName = "POS_NAME";
            this.CV1_8.Name = "CV1_8";
            this.CV1_8.Visible = true;
            this.CV1_8.Width = 103;
            // 
            // gridBand20
            // 
            this.gridBand20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand20.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand20.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand20.Caption = "Стать";
            this.gridBand20.Columns.Add(this.CV1_9);
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.Width = 41;
            // 
            // CV1_9
            // 
            this.CV1_9.AppearanceCell.Options.UseTextOptions = true;
            this.CV1_9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_9.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_9.Caption = "9";
            this.CV1_9.FieldName = "MALE_FEMALE";
            this.CV1_9.Name = "CV1_9";
            this.CV1_9.Visible = true;
            this.CV1_9.Width = 41;
            // 
            // gridBand21
            // 
            this.gridBand21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand21.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand21.Caption = "Рік народження";
            this.gridBand21.Columns.Add(this.CV1_10);
            this.gridBand21.Name = "gridBand21";
            this.gridBand21.Width = 76;
            // 
            // CV1_10
            // 
            this.CV1_10.AppearanceCell.Options.UseTextOptions = true;
            this.CV1_10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_10.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_10.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_10.Caption = "10";
            this.CV1_10.FieldName = "YEAR_BIRTH";
            this.CV1_10.Name = "CV1_10";
            this.CV1_10.Visible = true;
            this.CV1_10.Width = 76;
            // 
            // gridBand22
            // 
            this.gridBand22.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand22.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand22.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand22.Caption = "Освіта";
            this.gridBand22.Columns.Add(this.CV1_11);
            this.gridBand22.Name = "gridBand22";
            this.gridBand22.Width = 47;
            // 
            // CV1_11
            // 
            this.CV1_11.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_11.Caption = "11";
            this.CV1_11.FieldName = "EDU";
            this.CV1_11.Name = "CV1_11";
            this.CV1_11.Visible = true;
            this.CV1_11.Width = 47;
            // 
            // gridBand23
            // 
            this.gridBand23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand23.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand23.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand23.Caption = "Заробітня платня";
            this.gridBand23.Columns.Add(this.CV1_12);
            this.gridBand23.Name = "gridBand23";
            this.gridBand23.Width = 76;
            // 
            // CV1_12
            // 
            this.CV1_12.AppearanceCell.Options.UseTextOptions = true;
            this.CV1_12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_12.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_12.AppearanceHeader.Options.UseTextOptions = true;
            this.CV1_12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CV1_12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CV1_12.Caption = "12";
            this.CV1_12.FieldName = "PAY";
            this.CV1_12.Name = "CV1_12";
            this.CV1_12.Visible = true;
            this.CV1_12.Width = 76;
            // 
            // lE_ListTabl
            // 
            this.lE_ListTabl.EditValue = 0;
            this.lE_ListTabl.Location = new System.Drawing.Point(24, 23);
            this.lE_ListTabl.Name = "lE_ListTabl";
            this.lE_ListTabl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lE_ListTabl.Properties.DisplayMember = "NAIM";
            this.lE_ListTabl.Properties.NullText = "Список підприємств";
            this.lE_ListTabl.Properties.ValueMember = "ID";
            this.lE_ListTabl.Size = new System.Drawing.Size(236, 20);
            this.lE_ListTabl.TabIndex = 2;
            this.lE_ListTabl.EditValueChanged += new System.EventHandler(this.lE_ListTabl_EditValueChanged);
            // 
            // AddRow
            // 
            this.AddRow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AddRow.Location = new System.Drawing.Point(34, 484);
            this.AddRow.Name = "AddRow";
            this.AddRow.Size = new System.Drawing.Size(120, 35);
            this.AddRow.TabIndex = 3;
            this.AddRow.Text = "Додати запис";
            this.AddRow.UseVisualStyleBackColor = true;
            this.AddRow.Click += new System.EventHandler(this.AddRow_Click);
            // 
            // lE_Comp
            // 
            this.lE_Comp.Location = new System.Drawing.Point(290, 23);
            this.lE_Comp.Name = "lE_Comp";
            this.lE_Comp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lE_Comp.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_COMP", "ID", 50),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Назва", 100)});
            this.lE_Comp.Properties.DisplayMember = "NAME";
            this.lE_Comp.Properties.NullText = "";
            this.lE_Comp.Properties.ValueMember = "ID_COMP";
            this.lE_Comp.Size = new System.Drawing.Size(236, 20);
            this.lE_Comp.TabIndex = 4;
            this.lE_Comp.EditValueChanged += new System.EventHandler(this.lE_Comp_EditValueChanged);
            // 
            // lE_Dep
            // 
            this.lE_Dep.Location = new System.Drawing.Point(549, 23);
            this.lE_Dep.Name = "lE_Dep";
            this.lE_Dep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lE_Dep.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DEP", "Код відділу", 50),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Назва відділу", 100)});
            this.lE_Dep.Properties.DisplayMember = "NAME";
            this.lE_Dep.Properties.NullText = "";
            this.lE_Dep.Properties.ValueMember = "ID_DEP";
            this.lE_Dep.Size = new System.Drawing.Size(236, 20);
            this.lE_Dep.TabIndex = 5;
            this.lE_Dep.EditValueChanged += new System.EventHandler(this.lE_Dep_EditValueChanged);
            // 
            // DelRow
            // 
            this.DelRow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DelRow.Location = new System.Drawing.Point(208, 484);
            this.DelRow.Name = "DelRow";
            this.DelRow.Size = new System.Drawing.Size(120, 35);
            this.DelRow.TabIndex = 6;
            this.DelRow.Text = "Видалити запис";
            this.DelRow.UseVisualStyleBackColor = true;
            this.DelRow.Click += new System.EventHandler(this.DelRow_Click);
            // 
            // SaveTable
            // 
            this.SaveTable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SaveTable.Location = new System.Drawing.Point(380, 484);
            this.SaveTable.Name = "SaveTable";
            this.SaveTable.Size = new System.Drawing.Size(120, 35);
            this.SaveTable.TabIndex = 7;
            this.SaveTable.Text = "Зберегти зміни";
            this.SaveTable.UseVisualStyleBackColor = true;
            this.SaveTable.Click += new System.EventHandler(this.SaveTable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Список НДІ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Список підприємств";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(546, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Список відділів";
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 532);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveTable);
            this.Controls.Add(this.DelRow);
            this.Controls.Add(this.lE_Dep);
            this.Controls.Add(this.lE_Comp);
            this.Controls.Add(this.AddRow);
            this.Controls.Add(this.lE_ListTabl);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Our first database downloading";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lE_ListTabl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lE_Comp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lE_Dep.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.LookUpEdit lE_ListTabl;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BV2_1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV2_1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV2_3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV2_2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BV2_2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BV2_3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BV3_1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BV3_2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV3_1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV3_2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV4_1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV4_2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV4_3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV4_4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV4_5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV4_6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV5_1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV5_2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV5_3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV5_4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV5_5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand13;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand14;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand15;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand17;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand18;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand22;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand23;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CV1_12;
        private System.Windows.Forms.Button AddRow;
        private DevExpress.XtraEditors.LookUpEdit lE_Comp;
        private DevExpress.XtraEditors.LookUpEdit lE_Dep;
        private System.Windows.Forms.Button DelRow;
        private System.Windows.Forms.Button SaveTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

