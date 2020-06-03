//----------------  AC  RODUZ - "...�������� ������ �� ���������... "  -------------
//
// FRMCO19MN - ����� ��-19 �����Ĳ� ����Ĳ� ²� ����������� �����Ʋ� �� �����  ���������� 
//              �� <....> ����� <....> ����                    ��������� � ������ 2012 �
//
// ò�� ��-���� ��         ��-�: ��������� �.�., �-�: 5-09-86          ������ 2012 �
//-------------------------------------------------------------------------------------
using System;                   //��������� ������������ ���� System
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;     //  ---  ��� Form
using System.Data;
using  CoreLab.Oracle ;   //  OraDirect  ��� ����� � ����� Oracle
//using System.Data.OracleClient; //  ---  ��� ����� � ����� Oracle
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using Repository = DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using Chart;
using System.Text.RegularExpressions;

namespace FRMCO19MN
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FRMCO19MN : DevExpress.XtraEditors.XtraForm // System.Windows.Forms.Form
	{
		#region Declare SYSTEM     - ��������� �������

		private System.Data.DataSet Ds;
		private System.ComponentModel.IContainer components;
		// ������� ���������� ������� � ����� ������ Oracle
		//private OracleConnection  OraCon;
		//  � �������(OraDA..) OracleDataAdapter ��� ������ ��������� � ���� ������
		private OracleDataAdapter OraDAArh;
		private OracleDataAdapter OraDAShabl;
		private OracleDataAdapter OraDAImp;		
		// ������� ������� OracleCommandBuilder ��� ������ �������
		private OracleCommandBuilder OraCBArh;
        //private OracleCommandBuilder OraCBShabl;

		private DevExpress.Utils.ToolTipController toolTipController1;
		//			private System.Data.OracleClient.OracleCommand OraUp;

		#endregion

		#region Declare FORMS      - ���� � ������� ����� ...
		//    ��� ������ 
		#endregion

		#region Declare Variables  - �������������� ����������

			// ������� ���������(myView) ������ ... AdvBandedGridView ��� ��������� ������� � ��������� ����
			// ��� �������� �� ����� ��������(���������� � xtraTabControl1_SelectedPageChanged(...) )
			private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView myView= new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();

			private char   cAcKey  ;        // ���������� �������� ���� ������� : cAcc=R-������\W-���������\F-������
			private string sLang   = "U";   // ���� ��� ������ ���������: U-����������, R-�������
			private long   uId     = 1 ;    // ���������� �������� UserID-ID_������������ �������
			private string uName   = " ";   // ���������� �������� UserName-���_������������ �������
			private string myAccess = "";   // ��������������� ������ ��� ������ ��������� � �� `�����` 

			private string myDoc = "D0CO19M"; // ��� ��������� � ������ nGr - �������� ����� ����
			private string[] nGr = new string[46] {"V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V10","V11", 
												"M2", "M3", "M4", "M5", "M6", "M6A", "M7", "M8","M8A", "M9", "M10","M11",
												"M12","M13","M13A","M14","M15","M15A","M16","M17","M18","M19","M20","M21",
												"M22","M23","M23A","M24","M25",
												"X2", "X3", "X4", "X5", "X6", "X7", "X8"};
			private string tBShabl = "S0CO19MN";  // �������� Ora � Ds �������-������� ��� ������� ���������
			private string tBArh   = "F0CO19V";  // �������� Ora � Ds �������-������  ��� ������� ���������
			private string tPage14 = "�����Ĳ� ����Ĳ�  ²�  �����������  �����Ʋ�  ��  ����� ����������";
			private string tPage5 = "���I�����  �������  ��  ��I��  �. ��-19   ��  ������I�� ����Ĳ�";
			private string myPage = "Page5";   // �������� ������: Page1...Page5(����� �����:  grid1...grid5
		
		
			//private string inRoad = "('32','35','40','43','45','48') ";   // ������ ���������� �����  


			// ����������,  ������������ ��� �������: ������ �������, ������
			private decimal mySum = 0;
			private int sMes = 0;     // ������    ������� - �����  
			private int sGod = 0;     //                     ���
			private int eMes = 0;     // ��������� ������� - �����
			private int eGod = 0;     //                     ���
		
			// ����������, ������������ ��� ������������ �������� bView - ����������� ��������� ���������
			private bool bView   = false;    // =true - ������� ������ ��������� ���������
			private bool bArh    = false;    // =true - �������  �������  ������� � Oracle_������� ������
			private bool bSum    = false;    // =true - ������� ������� ����� <> 0 ��� ���������� ���������
			private bool one_MG  = true;     // =true - ������� �������(���, �����) : ������ = ��������� 
			private bool bTag    = true;     // =true - ������� ���������� ��������� � ...._EditValueChanging
			private bool bUpdate = false;    // =true - ������� ���������� ������ 	
			private bool bEditTabl = false;  // =true - ������� ���������� ��������� ��������� � ����������� �� ������� myAccess
			private bool bLockDoc  = false;  // =true - ������� ������� ��������� ���������, �.�. ������ ������
			private bool bDateDoc = false; // =true - ������� ����, ��� �������� ��������� �� ��������� ����

			private Office.Excel xls = null;
			private RDA.RDF RdaFunc = new RDA.RDF();
			private RDA.FilterControl RdaFiltr; // ������� ���������� ������ ������ FilterControl ��� �������� ��������� ������



		#endregion

		#region Declare BandedGrid - ������ � ������ �� ����� 

		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gB_6;
		private System.Windows.Forms.Label l_Period;
		public ImageList imageListResources;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarEditItem bE_Mes;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rI_Mes;
		private DevExpress.XtraBars.BarEditItem bE_God;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rI_God;
		private DevExpress.XtraBars.BarButtonItem bB_Ok;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarButtonItem bB_Save;
		private DevExpress.XtraBars.BarButtonItem bB_ImpExp;
		private DevExpress.XtraBars.BarButtonItem bB_Del;
		private DevExpress.XtraBars.BarButtonItem bB_Exit;
		private DevExpress.XtraBars.Bar barBottom;
		private DevExpress.XtraBars.BarStaticItem bS_Info;
		private DevExpress.XtraBars.BarDockControl barDockControl1;
		private DevExpress.XtraBars.BarDockControl barDockControl2;
		private DevExpress.XtraBars.BarDockControl barDockControl3;
		private DevExpress.XtraBars.BarDockControl barDockControl4;
		private DevExpress.XtraBars.BarStaticItem bS_InfoLeft;
		private DevExpress.XtraBars.BarEditItem bS_Mes;
		private DevExpress.XtraBars.BarEditItem bS_God;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rIs_Mes;
		private RDA.CellValueControl cellValueControl1;
		private RDA.DataManager dataManager1;
		private LabelControl lb_Shapka;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage Page1;
		private DevExpress.XtraGrid.GridControl grid1;
		private AdvBandedGridView advBandedGridView1;
		private GridBand gB1_r;
		private BandedGridColumn C0;
		private GridBand gB1_n;
		private BandedGridColumn C1;
		private GridBand gB1_2_11;
		private GridBand gB1_2;
		private BandedGridColumn C2;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit9;
		private GridBand gB1_3_4;
		private BandedGridColumn C3_N;
		private BandedGridColumn C4;
		private GridBand gB1_5;
		private BandedGridColumn C5;
		private GridBand gB1_6;
		private BandedGridColumn C6;
		private GridBand gB1_7;
		private BandedGridColumn C7;
		private GridBand gB1_8;
		private BandedGridColumn C8;
		private GridBand gB1_9;
		private BandedGridColumn C9;
		private GridBand gB1_10;
		private BandedGridColumn C10;
		private GridBand gB1_11;
		private BandedGridColumn C11;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
		private DevExpress.XtraTab.XtraTabPage Page2;
		private DevExpress.XtraGrid.GridControl grid2;
		private AdvBandedGridView advBandedGridView2;
		private GridBand gB2_r;
		private BandedGridColumn C0E;
		private GridBand gB2_n;
		private BandedGridColumn C1E;
		private GridBand gB2_M;
		private GridBand gB2_E;
		private GridBand gB2_2;
		private BandedGridColumn C12;
		private GridBand gB2_3;
		private BandedGridColumn C13;
		private GridBand gB2_4;
		private BandedGridColumn C14;
		private GridBand gB2_5;
		private BandedGridColumn C15;
		private GridBand gB2_D;
		private GridBand gB2_6;
		private BandedGridColumn C16;
		private GridBand gB2_6A;
		private BandedGridColumn C6A;
		private GridBand gB2_7;
		private BandedGridColumn C17;
		private GridBand gB2_8;
		private BandedGridColumn C18;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
		private DevExpress.XtraTab.XtraTabPage Page3;
		private DevExpress.XtraGrid.GridControl grid3;
		private AdvBandedGridView advBandedGridView3;
		private GridBand gB3_r;
		private BandedGridColumn C0I;
		private GridBand gB3_n;
		private BandedGridColumn C1I;
		private GridBand gB3_M;
		private GridBand gB3_I;
		private GridBand gB3_9;
		private BandedGridColumn C19;
		private GridBand gB3_10;
		private BandedGridColumn C20;
		private GridBand gB3_11;
		private BandedGridColumn C21;
		private GridBand gB3_12;
		private BandedGridColumn C22;
		private GridBand gB3_D;
		private GridBand gB3_13;
		private BandedGridColumn C23;
		private GridBand gB3_13A;
		private BandedGridColumn C13A;
		private GridBand gB3_14;
		private BandedGridColumn C24;
		private GridBand gB3_15;
		private BandedGridColumn C25;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit5;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit6;
		private DevExpress.XtraTab.XtraTabPage Page4;
		private DevExpress.XtraGrid.GridControl grid4;
		private AdvBandedGridView advBandedGridView4;
		private GridBand gB4_r;
		private BandedGridColumn C0T;
		private GridBand gB4_n;
		private BandedGridColumn C1T;
		private GridBand gB4_M;
		private GridBand gB4_16_21;
		private GridBand gB4_16;
		private BandedGridColumn C26;
		private GridBand gB4_17;
		private BandedGridColumn C27;
		private GridBand gB4_18;
		private BandedGridColumn C28;
		private GridBand gB4_19;
		private BandedGridColumn C29;
		private GridBand gB4_20;
		private BandedGridColumn C30;
		private GridBand gB4_21;
		private BandedGridColumn C31;
		private GridBand gB4_22_23A;
		private GridBand gB4_22;
		private BandedGridColumn C32;
		private GridBand gB4_23;
		private BandedGridColumn C33;
		private GridBand gB4_23A;
		private BandedGridColumn C34;
		private GridBand gB4_24;
		private BandedGridColumn C35;
		private GridBand gB4_25;
		private BandedGridColumn C36;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit7;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit8;
		private DevExpress.XtraTab.XtraTabPage Page5;
		private DevExpress.XtraGrid.GridControl grid5;
		private AdvBandedGridView advBandedGridView9;
		private GridBand gB5_r;
		private BandedGridColumn C0X;
		private GridBand gB5_n;
		private BandedGridColumn C1X;
		private GridBand gB5_2_8;
		private GridBand gB5_2_4;
		private GridBand gB5_2;
		private BandedGridColumn C37;
		private GridBand gB5_3;
		private BandedGridColumn C38;
		private GridBand gB5_4;
		private BandedGridColumn C39;
		private GridBand gB5_5_7;
		private GridBand gB5_5;
		private BandedGridColumn C40;
		private GridBand gB5_6;
		private BandedGridColumn C41;
		private GridBand gB5_7;
		private BandedGridColumn C42;
		private GridBand gB5_8;
		private BandedGridColumn C43;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit17;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit18;
		private Label l_Grn;
		private BarStaticItem bI_Forma;
		private BarButtonItem bB_PlotDiagram;
		private Label label1;
		private GridBand gB1_3N;
		private GridBand gB2_8A;
		private BandedGridColumn C8A;
		private GridBand gB3_15A;
		private BandedGridColumn C15A;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rIs_God;
		#endregion

		public FRMCO19MN(OracleConnection OC, long UserID, string UserName, char cAcc, int iMes, int iGod)
		{
			InitializeComponent();

			//OraCon = OC;      // ������� � ���� ORACLE-������� ��� ������������ DLL_����
			//if(OraCon.State != System.Data.ConnectionState.Open)
			//    OraCon.Open();
			cAcKey = cAcc;    // ��� ������� � ������� (cAccess �� ������� R_UserObj)
			uId    = UserID;  // ID  ������������ �������(IdUser �� ������� GrUser)			
			uName  = UserName;// ��� ������������ �������(Naim   �� ������� GrUser)

			if (iMes.ToString().Trim()!="") sMes = (iMes > 0 && iMes < 13) ? iMes  : 0 ;  // ������ ��������� ������ 
			if (iGod.ToString().Trim()!="") sGod = (iGod > 2000)           ? iGod  : 0 ;  //                  � ����
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMCO19MN));
			RDA.ViewContent viewContent1 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent1 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent2 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent2 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent3 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent3 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent4 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent4 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent5 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent5 = new RDA.RepositoryContent();
			this.repositoryItemTextEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gB1_r = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C0 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_n = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_2_11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB1_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_3_4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB1_3N = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C3_N = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.grid1 = new DevExpress.XtraGrid.GridControl();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
			this.l_Period = new System.Windows.Forms.Label();
			this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
			this.l_Grn = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.advBandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gB2_r = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C0E = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_n = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C1E = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_M = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB2_E = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB2_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_D = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB2_6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_6A = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C6A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB2_8A = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C8A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.grid2 = new DevExpress.XtraGrid.GridControl();
			this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.advBandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gB3_r = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C0I = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_n = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C1I = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_M = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB3_I = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB3_9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C20 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C21 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C22 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_D = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB3_13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C23 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_13A = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C13A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C24 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C25 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB3_15A = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C15A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.grid3 = new DevExpress.XtraGrid.GridControl();
			this.repositoryItemTextEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.advBandedGridView4 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gB4_r = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C0T = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_n = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C1T = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_M = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB4_16_21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB4_16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C26 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C27 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_18 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C28 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C29 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C30 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C31 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_22_23A = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB4_22 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C32 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_23 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C33 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_23A = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C34 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_24 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C35 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB4_25 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C36 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.grid4 = new DevExpress.XtraGrid.GridControl();
			this.repositoryItemTextEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.advBandedGridView9 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gB5_r = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C0X = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_n = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C1X = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_2_8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB5_2_4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB5_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C37 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C38 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C39 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_5_7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gB5_5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C40 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C41 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C42 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gB5_8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.C43 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.grid5 = new DevExpress.XtraGrid.GridControl();
			this.repositoryItemTextEdit17 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit18 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.gB_6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.imageListResources = new System.Windows.Forms.ImageList(this.components);
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bS_Mes = new DevExpress.XtraBars.BarEditItem();
			this.rIs_Mes = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.bS_God = new DevExpress.XtraBars.BarEditItem();
			this.rIs_God = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bE_Mes = new DevExpress.XtraBars.BarEditItem();
			this.rI_Mes = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.bE_God = new DevExpress.XtraBars.BarEditItem();
			this.rI_God = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bB_Ok = new DevExpress.XtraBars.BarButtonItem();
			this.bI_Forma = new DevExpress.XtraBars.BarStaticItem();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bB_Save = new DevExpress.XtraBars.BarButtonItem();
			this.bB_ImpExp = new DevExpress.XtraBars.BarButtonItem();
			this.bB_Del = new DevExpress.XtraBars.BarButtonItem();
			this.bB_PlotDiagram = new DevExpress.XtraBars.BarButtonItem();
			this.bB_Exit = new DevExpress.XtraBars.BarButtonItem();
			this.barBottom = new DevExpress.XtraBars.Bar();
			this.bS_InfoLeft = new DevExpress.XtraBars.BarStaticItem();
			this.bS_Info = new DevExpress.XtraBars.BarStaticItem();
			this.cellValueControl1 = new RDA.CellValueControl(this.components);
			this.dataManager1 = new RDA.DataManager(this.components);
			this.lb_Shapka = new DevExpress.XtraEditors.LabelControl();
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.Page1 = new DevExpress.XtraTab.XtraTabPage();
			this.Page2 = new DevExpress.XtraTab.XtraTabPage();
			this.Page3 = new DevExpress.XtraTab.XtraTabPage();
			this.Page4 = new DevExpress.XtraTab.XtraTabPage();
			this.Page5 = new DevExpress.XtraTab.XtraTabPage();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit17)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit18)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rIs_Mes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rIs_God)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_Mes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_God)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cellValueControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.xtraTabControl1.SuspendLayout();
			this.Page1.SuspendLayout();
			this.Page2.SuspendLayout();
			this.Page3.SuspendLayout();
			this.Page4.SuspendLayout();
			this.Page5.SuspendLayout();
			this.SuspendLayout();
			// 
			// repositoryItemTextEdit9
			// 
			this.repositoryItemTextEdit9.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.repositoryItemTextEdit9.AutoHeight = false;
			this.repositoryItemTextEdit9.DisplayFormat.FormatString = "{0:n0}";
			this.repositoryItemTextEdit9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemTextEdit9.EditFormat.FormatString = "{0:f0}";
			this.repositoryItemTextEdit9.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemTextEdit9.HideSelection = false;
			this.repositoryItemTextEdit9.Name = "repositoryItemTextEdit9";
			// 
			// advBandedGridView1
			// 
			this.advBandedGridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView1.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView1.AppearancePrint.BandPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView1.AppearancePrint.BandPanel.Options.UseBackColor = true;
			this.advBandedGridView1.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView1.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView1.AppearancePrint.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridView1.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView1.AppearancePrint.FooterPanel.Options.UseBackColor = true;
			this.advBandedGridView1.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView1.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
			this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB1_r,
            this.gB1_n,
            this.gB1_2_11});
			this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.C0,
            this.C1,
            this.C2,
            this.C3_N,
            this.C4,
            this.C5,
            this.C6,
            this.C7,
            this.C8,
            this.C9,
            this.C10,
            this.C11});
			this.advBandedGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.advBandedGridView1.GridControl = this.grid1;
			this.advBandedGridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
			this.advBandedGridView1.HorzScrollStep = 1;
			this.advBandedGridView1.Name = "advBandedGridView1";
			this.advBandedGridView1.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView1.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView1.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView1.OptionsCustomization.AllowSort = false;
			this.advBandedGridView1.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView1.OptionsLayout.StoreAllOptions = true;
			this.advBandedGridView1.OptionsLayout.StoreAppearance = true;
			this.advBandedGridView1.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView1.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView1.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView1.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView1.OptionsPrint.ExpandAllDetails = true;
			this.advBandedGridView1.OptionsPrint.PrintDetails = true;
			this.advBandedGridView1.OptionsPrint.PrintPreview = true;
			this.advBandedGridView1.OptionsPrint.UsePrintStyles = true;
			this.advBandedGridView1.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView1.OptionsSelection.InvertSelection = true;
			this.advBandedGridView1.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView1.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridView1.OptionsView.ShowFooter = true;
			this.advBandedGridView1.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView1.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
			this.advBandedGridView1.SynchronizeClones = false;
			this.advBandedGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseUp);
			// 
			// gB1_r
			// 
			this.gB1_r.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_r.AppearanceHeader.Options.UseFont = true;
			this.gB1_r.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_r.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_r.Caption = "���";
			this.gB1_r.Columns.Add(this.C0);
			this.gB1_r.Name = "gB1_r";
			this.gB1_r.OptionsBand.AllowMove = false;
			this.gB1_r.OptionsBand.AllowPress = false;
			this.gB1_r.OptionsBand.AllowSize = false;
			this.gB1_r.RowCount = 6;
			this.gB1_r.ToolTip = "��� �������i";
			this.gB1_r.Width = 34;
			// 
			// C0
			// 
			this.C0.AppearanceCell.Options.UseTextOptions = true;
			this.C0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0.AppearanceHeader.Options.UseTextOptions = true;
			this.C0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0.FieldName = "ROAD";
			this.C0.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C0.Name = "C0";
			this.C0.OptionsColumn.AllowEdit = false;
			this.C0.OptionsColumn.AllowFocus = false;
			this.C0.OptionsColumn.AllowMove = false;
			this.C0.OptionsColumn.AllowSize = false;
			this.C0.OptionsColumn.FixedWidth = true;
			this.C0.OptionsFilter.AllowAutoFilter = false;
			this.C0.OptionsFilter.AllowFilter = false;
			this.C0.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C0.ToolTip = "���";
			this.C0.Visible = true;
			this.C0.Width = 34;
			// 
			// gB1_n
			// 
			this.gB1_n.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_n.AppearanceHeader.Options.UseFont = true;
			this.gB1_n.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_n.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_n.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_n.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_n.Caption = "����� �������� (����������)";
			this.gB1_n.Columns.Add(this.C1);
			this.gB1_n.Name = "gB1_n";
			this.gB1_n.OptionsBand.AllowMove = false;
			this.gB1_n.OptionsBand.AllowPress = false;
			this.gB1_n.OptionsBand.AllowSize = false;
			this.gB1_n.RowCount = 6;
			this.gB1_n.ToolTip = "������ ��������";
			this.gB1_n.Width = 177;
			// 
			// C1
			// 
			this.C1.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C1.AppearanceHeader.Options.UseFont = true;
			this.C1.AppearanceHeader.Options.UseTextOptions = true;
			this.C1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C1.Caption = "1";
			this.C1.FieldName = "NAIM";
			this.C1.Name = "C1";
			this.C1.OptionsColumn.AllowEdit = false;
			this.C1.OptionsColumn.AllowFocus = false;
			this.C1.OptionsColumn.AllowMove = false;
			this.C1.OptionsColumn.AllowSize = false;
			this.C1.OptionsColumn.FixedWidth = true;
			this.C1.OptionsFilter.AllowAutoFilter = false;
			this.C1.OptionsFilter.AllowFilter = false;
			this.C1.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C1.SummaryItem.DisplayFormat = "������";
			this.C1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
			this.C1.ToolTip = "����� ��������";
			this.C1.UnboundType = DevExpress.Data.UnboundColumnType.String;
			this.C1.Visible = true;
			this.C1.Width = 177;
			// 
			// gB1_2_11
			// 
			this.gB1_2_11.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_2_11.AppearanceHeader.Options.UseFont = true;
			this.gB1_2_11.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_2_11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_2_11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_2_11.AutoFillDown = false;
			this.gB1_2_11.Caption = "�  �  �  �  �  I  �  �  �           �  �  �  �  �  �  �  �  �  �";
			this.gB1_2_11.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB1_2,
            this.gB1_3_4,
            this.gB1_5,
            this.gB1_6,
            this.gB1_7,
            this.gB1_8,
            this.gB1_9,
            this.gB1_10,
            this.gB1_11,
            this.gB1_3N});
			this.gB1_2_11.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB1_2_11.MinWidth = 20;
			this.gB1_2_11.Name = "gB1_2_11";
			this.gB1_2_11.OptionsBand.AllowMove = false;
			this.gB1_2_11.OptionsBand.AllowPress = false;
			this.gB1_2_11.OptionsBand.AllowSize = false;
			this.gB1_2_11.Width = 779;
			// 
			// gB1_2
			// 
			this.gB1_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_2.AppearanceHeader.Options.UseFont = true;
			this.gB1_2.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_2.Caption = "������ ������I�";
			this.gB1_2.Columns.Add(this.C2);
			this.gB1_2.Name = "gB1_2";
			this.gB1_2.OptionsBand.AllowMove = false;
			this.gB1_2.OptionsBand.AllowPress = false;
			this.gB1_2.OptionsBand.AllowSize = false;
			this.gB1_2.RowCount = 5;
			this.gB1_2.Width = 82;
			// 
			// C2
			// 
			this.C2.AppearanceHeader.Options.UseTextOptions = true;
			this.C2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C2.Caption = "2";
			this.C2.ColumnEdit = this.repositoryItemTextEdit9;
			this.C2.FieldName = "V2";
			this.C2.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C2.Name = "C2";
			this.C2.OptionsFilter.AllowAutoFilter = false;
			this.C2.OptionsFilter.AllowFilter = false;
			this.C2.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C2.SummaryItem.DisplayFormat = "{0:n0}";
			this.C2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C2.Visible = true;
			this.C2.Width = 82;
			// 
			// gB1_3_4
			// 
			this.gB1_3_4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_3_4.AppearanceHeader.Options.UseFont = true;
			this.gB1_3_4.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_3_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_3_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_3_4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_3_4.Caption = "�� ���  �����- �������";
			this.gB1_3_4.Columns.Add(this.C4);
			this.gB1_3_4.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB1_3_4.Name = "gB1_3_4";
			this.gB1_3_4.OptionsBand.AllowMove = false;
			this.gB1_3_4.OptionsBand.AllowPress = false;
			this.gB1_3_4.OptionsBand.AllowSize = false;
			this.gB1_3_4.RowCount = 2;
			this.gB1_3_4.Width = 56;
			// 
			// C4
			// 
			this.C4.AppearanceHeader.Options.UseTextOptions = true;
			this.C4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C4.Caption = "4";
			this.C4.ColumnEdit = this.repositoryItemTextEdit9;
			this.C4.FieldName = "V4";
			this.C4.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C4.Name = "C4";
			this.C4.OptionsFilter.AllowAutoFilter = false;
			this.C4.OptionsFilter.AllowFilter = false;
			this.C4.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C4.SummaryItem.DisplayFormat = "{0:n0}";
			this.C4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C4.Visible = true;
			this.C4.Width = 56;
			// 
			// gB1_5
			// 
			this.gB1_5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_5.AppearanceHeader.Options.UseFont = true;
			this.gB1_5.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_5.Caption = "�� ϲ������� ��-12";
			this.gB1_5.Columns.Add(this.C5);
			this.gB1_5.Name = "gB1_5";
			this.gB1_5.OptionsBand.AllowMove = false;
			this.gB1_5.OptionsBand.AllowPress = false;
			this.gB1_5.OptionsBand.AllowSize = false;
			this.gB1_5.RowCount = 5;
			this.gB1_5.Width = 75;
			// 
			// C5
			// 
			this.C5.AppearanceHeader.Options.UseTextOptions = true;
			this.C5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C5.Caption = "5";
			this.C5.ColumnEdit = this.repositoryItemTextEdit9;
			this.C5.FieldName = "V5";
			this.C5.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C5.Name = "C5";
			this.C5.OptionsFilter.AllowAutoFilter = false;
			this.C5.OptionsFilter.AllowFilter = false;
			this.C5.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C5.SummaryItem.DisplayFormat = "{0:n0}";
			this.C5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C5.Visible = true;
			// 
			// gB1_6
			// 
			this.gB1_6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_6.AppearanceHeader.Options.UseFont = true;
			this.gB1_6.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_6.Caption = "��������� �����ֲ�";
			this.gB1_6.Columns.Add(this.C6);
			this.gB1_6.Name = "gB1_6";
			this.gB1_6.OptionsBand.AllowMove = false;
			this.gB1_6.OptionsBand.AllowPress = false;
			this.gB1_6.OptionsBand.AllowSize = false;
			this.gB1_6.RowCount = 5;
			this.gB1_6.Width = 76;
			// 
			// C6
			// 
			this.C6.AppearanceHeader.Options.UseTextOptions = true;
			this.C6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C6.Caption = "6";
			this.C6.ColumnEdit = this.repositoryItemTextEdit9;
			this.C6.FieldName = "V6";
			this.C6.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C6.Name = "C6";
			this.C6.OptionsFilter.AllowAutoFilter = false;
			this.C6.OptionsFilter.AllowFilter = false;
			this.C6.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C6.SummaryItem.DisplayFormat = "{0:n0}";
			this.C6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C6.Visible = true;
			this.C6.Width = 76;
			// 
			// gB1_7
			// 
			this.gB1_7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_7.AppearanceHeader.Options.UseFont = true;
			this.gB1_7.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_7.Caption = "ʲ����� �����ֲ�";
			this.gB1_7.Columns.Add(this.C7);
			this.gB1_7.Name = "gB1_7";
			this.gB1_7.OptionsBand.AllowMove = false;
			this.gB1_7.OptionsBand.AllowPress = false;
			this.gB1_7.OptionsBand.AllowSize = false;
			this.gB1_7.RowCount = 5;
			this.gB1_7.Width = 65;
			// 
			// C7
			// 
			this.C7.AppearanceHeader.Options.UseTextOptions = true;
			this.C7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C7.Caption = "7";
			this.C7.ColumnEdit = this.repositoryItemTextEdit9;
			this.C7.FieldName = "V7";
			this.C7.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C7.Name = "C7";
			this.C7.OptionsFilter.AllowAutoFilter = false;
			this.C7.OptionsFilter.AllowFilter = false;
			this.C7.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C7.SummaryItem.DisplayFormat = "{0:n0}";
			this.C7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C7.Visible = true;
			this.C7.Width = 65;
			// 
			// gB1_8
			// 
			this.gB1_8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_8.AppearanceHeader.Options.UseFont = true;
			this.gB1_8.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_8.Caption = "����������ʲ   �����������";
			this.gB1_8.Columns.Add(this.C8);
			this.gB1_8.Name = "gB1_8";
			this.gB1_8.OptionsBand.AllowMove = false;
			this.gB1_8.OptionsBand.AllowPress = false;
			this.gB1_8.OptionsBand.AllowSize = false;
			this.gB1_8.RowCount = 5;
			this.gB1_8.Width = 93;
			// 
			// C8
			// 
			this.C8.AppearanceHeader.Options.UseTextOptions = true;
			this.C8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C8.Caption = "8";
			this.C8.ColumnEdit = this.repositoryItemTextEdit9;
			this.C8.FieldName = "V8";
			this.C8.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C8.Name = "C8";
			this.C8.OptionsFilter.AllowAutoFilter = false;
			this.C8.OptionsFilter.AllowFilter = false;
			this.C8.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C8.SummaryItem.DisplayFormat = "{0:n0}";
			this.C8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C8.Visible = true;
			this.C8.Width = 93;
			// 
			// gB1_9
			// 
			this.gB1_9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_9.AppearanceHeader.Options.UseFont = true;
			this.gB1_9.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_9.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_9.Caption = "������ ̲������� ����������";
			this.gB1_9.Columns.Add(this.C9);
			this.gB1_9.Name = "gB1_9";
			this.gB1_9.OptionsBand.AllowMove = false;
			this.gB1_9.OptionsBand.AllowPress = false;
			this.gB1_9.OptionsBand.AllowSize = false;
			this.gB1_9.RowCount = 5;
			this.gB1_9.Width = 80;
			// 
			// C9
			// 
			this.C9.AppearanceHeader.Options.UseTextOptions = true;
			this.C9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C9.Caption = "9";
			this.C9.ColumnEdit = this.repositoryItemTextEdit9;
			this.C9.FieldName = "V9";
			this.C9.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C9.Name = "C9";
			this.C9.OptionsFilter.AllowAutoFilter = false;
			this.C9.OptionsFilter.AllowFilter = false;
			this.C9.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C9.SummaryItem.DisplayFormat = "{0:n0}";
			this.C9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C9.Visible = true;
			this.C9.Width = 80;
			// 
			// gB1_10
			// 
			this.gB1_10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_10.AppearanceHeader.Options.UseFont = true;
			this.gB1_10.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_10.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_10.Caption = "�������²  ����� ̲������� ����������";
			this.gB1_10.Columns.Add(this.C10);
			this.gB1_10.Name = "gB1_10";
			this.gB1_10.OptionsBand.AllowMove = false;
			this.gB1_10.OptionsBand.AllowPress = false;
			this.gB1_10.OptionsBand.AllowSize = false;
			this.gB1_10.RowCount = 5;
			this.gB1_10.Width = 85;
			// 
			// C10
			// 
			this.C10.AppearanceHeader.Options.UseTextOptions = true;
			this.C10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C10.Caption = "10";
			this.C10.ColumnEdit = this.repositoryItemTextEdit9;
			this.C10.FieldName = "V10";
			this.C10.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C10.Name = "C10";
			this.C10.OptionsFilter.AllowAutoFilter = false;
			this.C10.OptionsFilter.AllowFilter = false;
			this.C10.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C10.SummaryItem.DisplayFormat = "{0:n0}";
			this.C10.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C10.Visible = true;
			this.C10.Width = 85;
			// 
			// gB1_11
			// 
			this.gB1_11.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_11.AppearanceHeader.Options.UseFont = true;
			this.gB1_11.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_11.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_11.Caption = " ������           ²�  �����������  �����Ʋ�";
			this.gB1_11.Columns.Add(this.C11);
			this.gB1_11.Name = "gB1_11";
			this.gB1_11.OptionsBand.AllowMove = false;
			this.gB1_11.OptionsBand.AllowPress = false;
			this.gB1_11.OptionsBand.AllowSize = false;
			this.gB1_11.RowCount = 5;
			this.gB1_11.Width = 94;
			// 
			// C11
			// 
			this.C11.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C11.AppearanceCell.Options.UseFont = true;
			this.C11.AppearanceHeader.Options.UseTextOptions = true;
			this.C11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C11.Caption = "11";
			this.C11.ColumnEdit = this.repositoryItemTextEdit9;
			this.C11.FieldName = "V11";
			this.C11.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C11.Name = "C11";
			this.C11.OptionsFilter.AllowAutoFilter = false;
			this.C11.OptionsFilter.AllowFilter = false;
			this.C11.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C11.SummaryItem.DisplayFormat = "{0:n0}";
			this.C11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C11.Visible = true;
			this.C11.Width = 94;
			// 
			// gB1_3N
			// 
			this.gB1_3N.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB1_3N.AppearanceHeader.Options.UseFont = true;
			this.gB1_3N.AppearanceHeader.Options.UseTextOptions = true;
			this.gB1_3N.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB1_3N.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB1_3N.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB1_3N.Caption = "���� �� �����. ����� (�����,  �����)";
			this.gB1_3N.Columns.Add(this.C3_N);
			this.gB1_3N.Name = "gB1_3N";
			this.gB1_3N.Width = 73;
			// 
			// C3_N
			// 
			this.C3_N.AppearanceHeader.Options.UseTextOptions = true;
			this.C3_N.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C3_N.Caption = "3*";
			this.C3_N.ColumnEdit = this.repositoryItemTextEdit9;
			this.C3_N.FieldName = "V3";
			this.C3_N.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C3_N.Name = "C3_N";
			this.C3_N.OptionsFilter.AllowAutoFilter = false;
			this.C3_N.OptionsFilter.AllowFilter = false;
			this.C3_N.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C3_N.SummaryItem.DisplayFormat = "{0:n0}";
			this.C3_N.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C3_N.Visible = true;
			this.C3_N.Width = 73;
			// 
			// grid1
			// 
			this.grid1.Location = new System.Drawing.Point(1, 1);
			this.grid1.MainView = this.advBandedGridView1;
			this.grid1.Name = "grid1";
			this.grid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit9});
			this.grid1.Size = new System.Drawing.Size(1019, 512);
			this.grid1.TabIndex = 0;
			this.grid1.ToolTipController = this.toolTipController1;
			this.grid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			this.repositoryItemTextEdit1.PasswordChar = '*';
			// 
			// repositoryItemTextEdit2
			// 
			this.repositoryItemTextEdit2.AutoHeight = false;
			this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
			// 
			// toolTipController1
			// 
			this.toolTipController1.Appearance.BackColor = System.Drawing.Color.PaleGoldenrod;
			this.toolTipController1.Appearance.BackColor2 = System.Drawing.Color.LemonChiffon;
			this.toolTipController1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
			this.toolTipController1.Appearance.ForeColor = System.Drawing.Color.DarkRed;
			this.toolTipController1.Appearance.Options.UseBackColor = true;
			this.toolTipController1.Appearance.Options.UseFont = true;
			this.toolTipController1.Appearance.Options.UseForeColor = true;
			this.toolTipController1.AppearanceTitle.Font = new System.Drawing.Font("Tahoma", 9F);
			this.toolTipController1.AppearanceTitle.Options.UseFont = true;
			this.toolTipController1.AppearanceTitle.Options.UseImage = true;
			this.toolTipController1.AppearanceTitle.Options.UseTextOptions = true;
			this.toolTipController1.AppearanceTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.toolTipController1.Rounded = true;
			this.toolTipController1.RoundRadius = 10;
			this.toolTipController1.ShowBeak = true;
			this.toolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.Standard;
			// 
			// l_Period
			// 
			this.l_Period.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.l_Period.ForeColor = System.Drawing.Color.Black;
			this.l_Period.Location = new System.Drawing.Point(193, 93);
			this.l_Period.Name = "l_Period";
			this.l_Period.Size = new System.Drawing.Size(641, 29);
			this.toolTipController1.SetSuperTip(this.l_Period, null);
			this.l_Period.TabIndex = 158;
			this.l_Period.Text = "<l_Period - ������ ��������� ��������� >";
			this.l_Period.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// barDockControl1
			// 
			this.toolTipController1.SetSuperTip(this.barDockControl1, null);
			// 
			// barDockControl2
			// 
			this.toolTipController1.SetSuperTip(this.barDockControl2, null);
			// 
			// barDockControl3
			// 
			this.toolTipController1.SetSuperTip(this.barDockControl3, null);
			// 
			// barDockControl4
			// 
			this.toolTipController1.SetSuperTip(this.barDockControl4, null);
			// 
			// l_Grn
			// 
			this.l_Grn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.l_Grn.ForeColor = System.Drawing.Color.Black;
			this.l_Grn.Location = new System.Drawing.Point(949, 121);
			this.l_Grn.Name = "l_Grn";
			this.l_Grn.Size = new System.Drawing.Size(56, 21);
			this.toolTipController1.SetSuperTip(this.l_Grn, null);
			this.l_Grn.TabIndex = 90;
			this.l_Grn.Text = "���. ���.";
			this.l_Grn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label1.Location = new System.Drawing.Point(904, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 15);
			this.toolTipController1.SetSuperTip(this.label1, null);
			this.label1.TabIndex = 169;
			this.label1.Text = "ĳ�   �  01.2012";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// advBandedGridView2
			// 
			this.advBandedGridView2.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView2.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView2.AppearancePrint.BandPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView2.AppearancePrint.BandPanel.Options.UseBackColor = true;
			this.advBandedGridView2.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView2.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView2.AppearancePrint.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridView2.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView2.AppearancePrint.FooterPanel.Options.UseBackColor = true;
			this.advBandedGridView2.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView2.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
			this.advBandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB2_r,
            this.gB2_n,
            this.gB2_M});
			this.advBandedGridView2.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.C0E,
            this.C1E,
            this.C12,
            this.C13,
            this.C14,
            this.C15,
            this.C16,
            this.C6A,
            this.C17,
            this.C18,
            this.C8A});
			this.advBandedGridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.advBandedGridView2.GridControl = this.grid2;
			this.advBandedGridView2.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
			this.advBandedGridView2.HorzScrollStep = 1;
			this.advBandedGridView2.Name = "advBandedGridView2";
			this.advBandedGridView2.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView2.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView2.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView2.OptionsCustomization.AllowSort = false;
			this.advBandedGridView2.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView2.OptionsLayout.StoreAllOptions = true;
			this.advBandedGridView2.OptionsLayout.StoreAppearance = true;
			this.advBandedGridView2.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView2.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView2.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView2.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView2.OptionsPrint.ExpandAllDetails = true;
			this.advBandedGridView2.OptionsPrint.PrintDetails = true;
			this.advBandedGridView2.OptionsPrint.PrintPreview = true;
			this.advBandedGridView2.OptionsPrint.UsePrintStyles = true;
			this.advBandedGridView2.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView2.OptionsSelection.InvertSelection = true;
			this.advBandedGridView2.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView2.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridView2.OptionsView.ShowFooter = true;
			this.advBandedGridView2.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView2.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
			this.advBandedGridView2.SynchronizeClones = false;
			this.advBandedGridView2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseUp);
			// 
			// gB2_r
			// 
			this.gB2_r.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_r.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_r.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_r.Caption = "���";
			this.gB2_r.Columns.Add(this.C0E);
			this.gB2_r.Name = "gB2_r";
			this.gB2_r.OptionsBand.AllowMove = false;
			this.gB2_r.OptionsBand.AllowPress = false;
			this.gB2_r.OptionsBand.AllowSize = false;
			this.gB2_r.RowCount = 5;
			this.gB2_r.ToolTip = "��� �������i";
			this.gB2_r.Width = 34;
			// 
			// C0E
			// 
			this.C0E.AppearanceCell.Options.UseTextOptions = true;
			this.C0E.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0E.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0E.AppearanceHeader.Options.UseTextOptions = true;
			this.C0E.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0E.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0E.FieldName = "ROAD";
			this.C0E.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C0E.Name = "C0E";
			this.C0E.OptionsColumn.AllowEdit = false;
			this.C0E.OptionsColumn.AllowFocus = false;
			this.C0E.OptionsColumn.AllowMove = false;
			this.C0E.OptionsColumn.AllowSize = false;
			this.C0E.OptionsColumn.FixedWidth = true;
			this.C0E.OptionsFilter.AllowAutoFilter = false;
			this.C0E.OptionsFilter.AllowFilter = false;
			this.C0E.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C0E.ToolTip = "���";
			this.C0E.Visible = true;
			this.C0E.Width = 34;
			// 
			// gB2_n
			// 
			this.gB2_n.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_n.AppearanceHeader.Options.UseFont = true;
			this.gB2_n.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_n.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_n.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_n.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_n.AutoFillDown = false;
			this.gB2_n.Caption = "����� �������� (����������)";
			this.gB2_n.Columns.Add(this.C1E);
			this.gB2_n.Name = "gB2_n";
			this.gB2_n.OptionsBand.AllowMove = false;
			this.gB2_n.OptionsBand.AllowPress = false;
			this.gB2_n.OptionsBand.AllowSize = false;
			this.gB2_n.RowCount = 6;
			this.gB2_n.ToolTip = "������ ��������";
			this.gB2_n.Width = 190;
			// 
			// C1E
			// 
			this.C1E.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C1E.AppearanceHeader.Options.UseFont = true;
			this.C1E.AppearanceHeader.Options.UseTextOptions = true;
			this.C1E.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C1E.Caption = "1";
			this.C1E.FieldName = "NAIM";
			this.C1E.Name = "C1E";
			this.C1E.OptionsColumn.AllowEdit = false;
			this.C1E.OptionsColumn.AllowFocus = false;
			this.C1E.OptionsColumn.AllowMove = false;
			this.C1E.OptionsColumn.AllowSize = false;
			this.C1E.OptionsColumn.FixedWidth = true;
			this.C1E.OptionsFilter.AllowAutoFilter = false;
			this.C1E.OptionsFilter.AllowFilter = false;
			this.C1E.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C1E.SummaryItem.DisplayFormat = "������";
			this.C1E.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
			this.C1E.ToolTip = "����� ��������";
			this.C1E.UnboundType = DevExpress.Data.UnboundColumnType.String;
			this.C1E.Visible = true;
			this.C1E.Width = 190;
			// 
			// gB2_M
			// 
			this.gB2_M.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_M.AppearanceHeader.Options.UseFont = true;
			this.gB2_M.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_M.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_M.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_M.AutoFillDown = false;
			this.gB2_M.Caption = "�  �  �  �  �  �  �  �  �  �    � � � � � � � � � � ";
			this.gB2_M.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB2_E});
			this.gB2_M.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB2_M.MinWidth = 20;
			this.gB2_M.Name = "gB2_M";
			this.gB2_M.OptionsBand.AllowMove = false;
			this.gB2_M.OptionsBand.AllowPress = false;
			this.gB2_M.OptionsBand.AllowSize = false;
			this.gB2_M.Width = 767;
			// 
			// gB2_E
			// 
			this.gB2_E.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_E.AppearanceHeader.Options.UseFont = true;
			this.gB2_E.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_E.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_E.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_E.AutoFillDown = false;
			this.gB2_E.Caption = "� � � � � � �";
			this.gB2_E.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB2_2,
            this.gB2_3,
            this.gB2_4,
            this.gB2_5,
            this.gB2_D,
            this.gB2_7,
            this.gB2_8,
            this.gB2_8A});
			this.gB2_E.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB2_E.Name = "gB2_E";
			this.gB2_E.OptionsBand.AllowMove = false;
			this.gB2_E.OptionsBand.AllowPress = false;
			this.gB2_E.OptionsBand.AllowSize = false;
			this.gB2_E.Width = 767;
			// 
			// gB2_2
			// 
			this.gB2_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_2.AppearanceHeader.Options.UseFont = true;
			this.gB2_2.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_2.AutoFillDown = false;
			this.gB2_2.Caption = "������          ������I�";
			this.gB2_2.Columns.Add(this.C12);
			this.gB2_2.Name = "gB2_2";
			this.gB2_2.OptionsBand.AllowMove = false;
			this.gB2_2.OptionsBand.AllowPress = false;
			this.gB2_2.OptionsBand.AllowSize = false;
			this.gB2_2.RowCount = 4;
			this.gB2_2.Width = 98;
			// 
			// C12
			// 
			this.C12.AppearanceHeader.Options.UseTextOptions = true;
			this.C12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C12.Caption = "2";
			this.C12.ColumnEdit = this.repositoryItemTextEdit9;
			this.C12.FieldName = "M2";
			this.C12.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C12.Name = "C12";
			this.C12.OptionsFilter.AllowAutoFilter = false;
			this.C12.OptionsFilter.AllowFilter = false;
			this.C12.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C12.SummaryItem.DisplayFormat = "{0:n0}";
			this.C12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C12.Visible = true;
			this.C12.Width = 98;
			// 
			// gB2_3
			// 
			this.gB2_3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_3.AppearanceHeader.Options.UseFont = true;
			this.gB2_3.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_3.AutoFillDown = false;
			this.gB2_3.Caption = "I�   ���  ������������";
			this.gB2_3.Columns.Add(this.C13);
			this.gB2_3.Name = "gB2_3";
			this.gB2_3.OptionsBand.AllowMove = false;
			this.gB2_3.OptionsBand.AllowPress = false;
			this.gB2_3.OptionsBand.AllowSize = false;
			this.gB2_3.RowCount = 4;
			this.gB2_3.Width = 90;
			// 
			// C13
			// 
			this.C13.AppearanceHeader.Options.UseTextOptions = true;
			this.C13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C13.Caption = "3";
			this.C13.ColumnEdit = this.repositoryItemTextEdit9;
			this.C13.FieldName = "M3";
			this.C13.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C13.Name = "C13";
			this.C13.OptionsFilter.AllowAutoFilter = false;
			this.C13.OptionsFilter.AllowFilter = false;
			this.C13.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C13.SummaryItem.DisplayFormat = "{0:n0}";
			this.C13.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C13.Visible = true;
			this.C13.Width = 90;
			// 
			// gB2_4
			// 
			this.gB2_4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_4.AppearanceHeader.Options.UseFont = true;
			this.gB2_4.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_4.AutoFillDown = false;
			this.gB2_4.Caption = "��������� �����ֲ�";
			this.gB2_4.Columns.Add(this.C14);
			this.gB2_4.Name = "gB2_4";
			this.gB2_4.OptionsBand.AllowMove = false;
			this.gB2_4.OptionsBand.AllowPress = false;
			this.gB2_4.OptionsBand.AllowSize = false;
			this.gB2_4.RowCount = 4;
			this.gB2_4.Width = 85;
			// 
			// C14
			// 
			this.C14.AppearanceHeader.Options.UseTextOptions = true;
			this.C14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C14.Caption = "4";
			this.C14.ColumnEdit = this.repositoryItemTextEdit9;
			this.C14.FieldName = "M4";
			this.C14.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C14.Name = "C14";
			this.C14.OptionsFilter.AllowAutoFilter = false;
			this.C14.OptionsFilter.AllowFilter = false;
			this.C14.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C14.SummaryItem.DisplayFormat = "{0:n0}";
			this.C14.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C14.Visible = true;
			this.C14.Width = 85;
			// 
			// gB2_5
			// 
			this.gB2_5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_5.AppearanceHeader.Options.UseFont = true;
			this.gB2_5.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_5.AutoFillDown = false;
			this.gB2_5.Caption = "ʲ�����      �����ֲ�";
			this.gB2_5.Columns.Add(this.C15);
			this.gB2_5.Name = "gB2_5";
			this.gB2_5.OptionsBand.AllowMove = false;
			this.gB2_5.OptionsBand.AllowPress = false;
			this.gB2_5.OptionsBand.AllowSize = false;
			this.gB2_5.RowCount = 4;
			this.gB2_5.Width = 78;
			// 
			// C15
			// 
			this.C15.AppearanceHeader.Options.UseTextOptions = true;
			this.C15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C15.Caption = "5";
			this.C15.ColumnEdit = this.repositoryItemTextEdit9;
			this.C15.FieldName = "M5";
			this.C15.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C15.Name = "C15";
			this.C15.OptionsFilter.AllowAutoFilter = false;
			this.C15.OptionsFilter.AllowFilter = false;
			this.C15.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C15.SummaryItem.DisplayFormat = "{0:n0}";
			this.C15.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C15.Visible = true;
			this.C15.Width = 78;
			// 
			// gB2_D
			// 
			this.gB2_D.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
			this.gB2_D.AppearanceHeader.Options.UseFont = true;
			this.gB2_D.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_D.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_D.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_D.Caption = "�������²         ����� ";
			this.gB2_D.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB2_6,
            this.gB2_6A});
			this.gB2_D.Name = "gB2_D";
			this.gB2_D.Width = 147;
			// 
			// gB2_6
			// 
			this.gB2_6.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_6.AutoFillDown = false;
			this.gB2_6.Caption = "�����";
			this.gB2_6.Columns.Add(this.C16);
			this.gB2_6.Name = "gB2_6";
			this.gB2_6.OptionsBand.AllowMove = false;
			this.gB2_6.OptionsBand.AllowPress = false;
			this.gB2_6.OptionsBand.AllowSize = false;
			this.gB2_6.RowCount = 3;
			this.gB2_6.Width = 58;
			// 
			// C16
			// 
			this.C16.AppearanceHeader.Options.UseTextOptions = true;
			this.C16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C16.Caption = "6";
			this.C16.ColumnEdit = this.repositoryItemTextEdit9;
			this.C16.FieldName = "M6";
			this.C16.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C16.Name = "C16";
			this.C16.OptionsFilter.AllowAutoFilter = false;
			this.C16.OptionsFilter.AllowFilter = false;
			this.C16.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C16.SummaryItem.DisplayFormat = "{0:n0}";
			this.C16.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C16.Visible = true;
			this.C16.Width = 58;
			// 
			// gB2_6A
			// 
			this.gB2_6A.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_6A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_6A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_6A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_6A.Caption = "�� �������� ��  ����������� ��������";
			this.gB2_6A.Columns.Add(this.C6A);
			this.gB2_6A.Name = "gB2_6A";
			this.gB2_6A.RowCount = 3;
			this.gB2_6A.Width = 89;
			// 
			// C6A
			// 
			this.C6A.AppearanceHeader.Options.UseTextOptions = true;
			this.C6A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C6A.Caption = "6A";
			this.C6A.ColumnEdit = this.repositoryItemTextEdit9;
			this.C6A.FieldName = "M6A";
			this.C6A.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C6A.Name = "C6A";
			this.C6A.SummaryItem.DisplayFormat = "{0:n0}";
			this.C6A.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C6A.Visible = true;
			this.C6A.Width = 89;
			// 
			// gB2_7
			// 
			this.gB2_7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_7.AppearanceHeader.Options.UseFont = true;
			this.gB2_7.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_7.AutoFillDown = false;
			this.gB2_7.Caption = " ������      ����Ĳ� ";
			this.gB2_7.Columns.Add(this.C17);
			this.gB2_7.Name = "gB2_7";
			this.gB2_7.OptionsBand.AllowMove = false;
			this.gB2_7.OptionsBand.AllowPress = false;
			this.gB2_7.OptionsBand.AllowSize = false;
			this.gB2_7.RowCount = 4;
			this.gB2_7.Width = 103;
			// 
			// C17
			// 
			this.C17.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C17.AppearanceCell.Options.UseFont = true;
			this.C17.AppearanceHeader.Options.UseTextOptions = true;
			this.C17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C17.Caption = "7";
			this.C17.ColumnEdit = this.repositoryItemTextEdit9;
			this.C17.FieldName = "M7";
			this.C17.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C17.Name = "C17";
			this.C17.OptionsFilter.AllowAutoFilter = false;
			this.C17.OptionsFilter.AllowFilter = false;
			this.C17.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C17.SummaryItem.DisplayFormat = "{0:n0}";
			this.C17.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C17.Visible = true;
			this.C17.Width = 103;
			// 
			// gB2_8
			// 
			this.gB2_8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB2_8.AppearanceHeader.Options.UseFont = true;
			this.gB2_8.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_8.AutoFillDown = false;
			this.gB2_8.Caption = " �в�   ���   �� �������- �������        ��  ������������";
			this.gB2_8.Columns.Add(this.C18);
			this.gB2_8.Name = "gB2_8";
			this.gB2_8.OptionsBand.AllowMove = false;
			this.gB2_8.OptionsBand.AllowPress = false;
			this.gB2_8.OptionsBand.AllowSize = false;
			this.gB2_8.RowCount = 4;
			this.gB2_8.Width = 91;
			// 
			// C18
			// 
			this.C18.AppearanceHeader.Options.UseTextOptions = true;
			this.C18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C18.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C18.Caption = "8";
			this.C18.ColumnEdit = this.repositoryItemTextEdit9;
			this.C18.FieldName = "M8";
			this.C18.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C18.Name = "C18";
			this.C18.OptionsFilter.AllowAutoFilter = false;
			this.C18.OptionsFilter.AllowFilter = false;
			this.C18.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C18.SummaryItem.DisplayFormat = "{0:n0}";
			this.C18.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C18.Visible = true;
			this.C18.Width = 91;
			// 
			// gB2_8A
			// 
			this.gB2_8A.AppearanceHeader.Options.UseTextOptions = true;
			this.gB2_8A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB2_8A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB2_8A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB2_8A.Caption = "���� �� �����. ����� (�����,  �����)";
			this.gB2_8A.Columns.Add(this.C8A);
			this.gB2_8A.Name = "gB2_8A";
			this.gB2_8A.Width = 75;
			// 
			// C8A
			// 
			this.C8A.AppearanceHeader.Options.UseTextOptions = true;
			this.C8A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C8A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C8A.Caption = "8A";
			this.C8A.ColumnEdit = this.repositoryItemTextEdit9;
			this.C8A.FieldName = "M8A";
			this.C8A.Name = "C8A";
			this.C8A.OptionsFilter.AllowAutoFilter = false;
			this.C8A.OptionsFilter.AllowFilter = false;
			this.C8A.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C8A.SummaryItem.DisplayFormat = "{0:n0}";
			this.C8A.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C8A.Visible = true;
			// 
			// grid2
			// 
			this.grid2.Location = new System.Drawing.Point(1, 1);
			this.grid2.MainView = this.advBandedGridView2;
			this.grid2.Name = "grid2";
			this.grid2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit3,
            this.repositoryItemTextEdit4,
            this.repositoryItemTextEdit9});
			this.grid2.Size = new System.Drawing.Size(1019, 512);
			this.grid2.TabIndex = 1;
			this.grid2.ToolTipController = this.toolTipController1;
			this.grid2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView2});
			// 
			// repositoryItemTextEdit3
			// 
			this.repositoryItemTextEdit3.AutoHeight = false;
			this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
			this.repositoryItemTextEdit3.PasswordChar = '*';
			// 
			// repositoryItemTextEdit4
			// 
			this.repositoryItemTextEdit4.AutoHeight = false;
			this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
			// 
			// advBandedGridView3
			// 
			this.advBandedGridView3.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView3.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView3.AppearancePrint.BandPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView3.AppearancePrint.BandPanel.Options.UseBackColor = true;
			this.advBandedGridView3.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView3.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView3.AppearancePrint.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridView3.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView3.AppearancePrint.FooterPanel.Options.UseBackColor = true;
			this.advBandedGridView3.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView3.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
			this.advBandedGridView3.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB3_r,
            this.gB3_n,
            this.gB3_M});
			this.advBandedGridView3.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.C0I,
            this.C1I,
            this.C19,
            this.C20,
            this.C21,
            this.C22,
            this.C23,
            this.C13A,
            this.C24,
            this.C25,
            this.C15A});
			this.advBandedGridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.advBandedGridView3.GridControl = this.grid3;
			this.advBandedGridView3.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
			this.advBandedGridView3.HorzScrollStep = 1;
			this.advBandedGridView3.Name = "advBandedGridView3";
			this.advBandedGridView3.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView3.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView3.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView3.OptionsCustomization.AllowSort = false;
			this.advBandedGridView3.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView3.OptionsLayout.StoreAllOptions = true;
			this.advBandedGridView3.OptionsLayout.StoreAppearance = true;
			this.advBandedGridView3.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView3.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView3.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView3.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView3.OptionsPrint.ExpandAllDetails = true;
			this.advBandedGridView3.OptionsPrint.PrintDetails = true;
			this.advBandedGridView3.OptionsPrint.PrintPreview = true;
			this.advBandedGridView3.OptionsPrint.UsePrintStyles = true;
			this.advBandedGridView3.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView3.OptionsSelection.InvertSelection = true;
			this.advBandedGridView3.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView3.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridView3.OptionsView.ShowFooter = true;
			this.advBandedGridView3.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView3.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
			this.advBandedGridView3.SynchronizeClones = false;
			this.advBandedGridView3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseUp);
			// 
			// gB3_r
			// 
			this.gB3_r.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_r.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_r.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_r.Caption = "���";
			this.gB3_r.Columns.Add(this.C0I);
			this.gB3_r.Name = "gB3_r";
			this.gB3_r.OptionsBand.AllowMove = false;
			this.gB3_r.OptionsBand.AllowPress = false;
			this.gB3_r.OptionsBand.AllowSize = false;
			this.gB3_r.ToolTip = "��� �������i";
			this.gB3_r.Width = 34;
			// 
			// C0I
			// 
			this.C0I.AppearanceCell.Options.UseTextOptions = true;
			this.C0I.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0I.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0I.AppearanceHeader.Options.UseTextOptions = true;
			this.C0I.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0I.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0I.FieldName = "ROAD";
			this.C0I.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C0I.Name = "C0I";
			this.C0I.OptionsColumn.AllowEdit = false;
			this.C0I.OptionsColumn.AllowFocus = false;
			this.C0I.OptionsColumn.AllowMove = false;
			this.C0I.OptionsColumn.AllowSize = false;
			this.C0I.OptionsColumn.FixedWidth = true;
			this.C0I.OptionsFilter.AllowAutoFilter = false;
			this.C0I.OptionsFilter.AllowFilter = false;
			this.C0I.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C0I.ToolTip = "���";
			this.C0I.Visible = true;
			this.C0I.Width = 34;
			// 
			// gB3_n
			// 
			this.gB3_n.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB3_n.AppearanceHeader.Options.UseFont = true;
			this.gB3_n.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_n.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_n.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_n.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_n.Caption = "����� �������� (����������)";
			this.gB3_n.Columns.Add(this.C1I);
			this.gB3_n.Name = "gB3_n";
			this.gB3_n.OptionsBand.AllowMove = false;
			this.gB3_n.OptionsBand.AllowPress = false;
			this.gB3_n.OptionsBand.AllowSize = false;
			this.gB3_n.ToolTip = "������ ��������";
			this.gB3_n.Width = 190;
			// 
			// C1I
			// 
			this.C1I.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C1I.AppearanceHeader.Options.UseFont = true;
			this.C1I.AppearanceHeader.Options.UseTextOptions = true;
			this.C1I.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C1I.Caption = "1";
			this.C1I.FieldName = "NAIM";
			this.C1I.Name = "C1I";
			this.C1I.OptionsColumn.AllowEdit = false;
			this.C1I.OptionsColumn.AllowFocus = false;
			this.C1I.OptionsColumn.AllowMove = false;
			this.C1I.OptionsColumn.AllowSize = false;
			this.C1I.OptionsColumn.FixedWidth = true;
			this.C1I.OptionsFilter.AllowAutoFilter = false;
			this.C1I.OptionsFilter.AllowFilter = false;
			this.C1I.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C1I.SummaryItem.DisplayFormat = "������";
			this.C1I.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
			this.C1I.ToolTip = "����� ��������";
			this.C1I.UnboundType = DevExpress.Data.UnboundColumnType.String;
			this.C1I.Visible = true;
			this.C1I.Width = 190;
			// 
			// gB3_M
			// 
			this.gB3_M.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB3_M.AppearanceHeader.Options.UseFont = true;
			this.gB3_M.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_M.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_M.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_M.AutoFillDown = false;
			this.gB3_M.Caption = "�  �  �  �  �  �  �  �  �  �    � � � � � � � � � � ";
			this.gB3_M.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB3_I});
			this.gB3_M.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB3_M.MinWidth = 20;
			this.gB3_M.Name = "gB3_M";
			this.gB3_M.OptionsBand.AllowMove = false;
			this.gB3_M.OptionsBand.AllowPress = false;
			this.gB3_M.OptionsBand.AllowSize = false;
			this.gB3_M.Width = 762;
			// 
			// gB3_I
			// 
			this.gB3_I.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB3_I.AppearanceHeader.Options.UseFont = true;
			this.gB3_I.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_I.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_I.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_I.AutoFillDown = false;
			this.gB3_I.Caption = " I � � � � �";
			this.gB3_I.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB3_9,
            this.gB3_10,
            this.gB3_11,
            this.gB3_12,
            this.gB3_D,
            this.gB3_14,
            this.gB3_15,
            this.gB3_15A});
			this.gB3_I.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB3_I.Name = "gB3_I";
			this.gB3_I.OptionsBand.AllowMove = false;
			this.gB3_I.OptionsBand.AllowPress = false;
			this.gB3_I.OptionsBand.AllowSize = false;
			this.gB3_I.Width = 762;
			// 
			// gB3_9
			// 
			this.gB3_9.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_9.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_9.Caption = "������          ������I�";
			this.gB3_9.Columns.Add(this.C19);
			this.gB3_9.Name = "gB3_9";
			this.gB3_9.OptionsBand.AllowMove = false;
			this.gB3_9.OptionsBand.AllowPress = false;
			this.gB3_9.OptionsBand.AllowSize = false;
			this.gB3_9.Width = 90;
			// 
			// C19
			// 
			this.C19.AppearanceHeader.Options.UseTextOptions = true;
			this.C19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C19.Caption = "9";
			this.C19.ColumnEdit = this.repositoryItemTextEdit9;
			this.C19.FieldName = "M9";
			this.C19.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C19.Name = "C19";
			this.C19.OptionsFilter.AllowAutoFilter = false;
			this.C19.OptionsFilter.AllowFilter = false;
			this.C19.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C19.SummaryItem.DisplayFormat = "{0:n0}";
			this.C19.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C19.Visible = true;
			this.C19.Width = 90;
			// 
			// gB3_10
			// 
			this.gB3_10.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_10.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_10.Caption = "I�   ���  �����- �������";
			this.gB3_10.Columns.Add(this.C20);
			this.gB3_10.Name = "gB3_10";
			this.gB3_10.OptionsBand.AllowMove = false;
			this.gB3_10.OptionsBand.AllowPress = false;
			this.gB3_10.OptionsBand.AllowSize = false;
			this.gB3_10.Width = 86;
			// 
			// C20
			// 
			this.C20.AppearanceHeader.Options.UseTextOptions = true;
			this.C20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C20.Caption = "10";
			this.C20.ColumnEdit = this.repositoryItemTextEdit9;
			this.C20.FieldName = "M10";
			this.C20.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C20.Name = "C20";
			this.C20.OptionsFilter.AllowAutoFilter = false;
			this.C20.OptionsFilter.AllowFilter = false;
			this.C20.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C20.SummaryItem.DisplayFormat = "{0:n0}";
			this.C20.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C20.Visible = true;
			this.C20.Width = 86;
			// 
			// gB3_11
			// 
			this.gB3_11.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_11.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_11.Caption = "��������� �����ֲ�";
			this.gB3_11.Columns.Add(this.C21);
			this.gB3_11.Name = "gB3_11";
			this.gB3_11.OptionsBand.AllowMove = false;
			this.gB3_11.OptionsBand.AllowPress = false;
			this.gB3_11.OptionsBand.AllowSize = false;
			this.gB3_11.Width = 80;
			// 
			// C21
			// 
			this.C21.AppearanceHeader.Options.UseTextOptions = true;
			this.C21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C21.Caption = "11";
			this.C21.ColumnEdit = this.repositoryItemTextEdit9;
			this.C21.FieldName = "M11";
			this.C21.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C21.Name = "C21";
			this.C21.OptionsFilter.AllowAutoFilter = false;
			this.C21.OptionsFilter.AllowFilter = false;
			this.C21.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C21.SummaryItem.DisplayFormat = "{0:n0}";
			this.C21.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C21.Visible = true;
			this.C21.Width = 80;
			// 
			// gB3_12
			// 
			this.gB3_12.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_12.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_12.Caption = "ʲ�����      �����ֲ�";
			this.gB3_12.Columns.Add(this.C22);
			this.gB3_12.Name = "gB3_12";
			this.gB3_12.OptionsBand.AllowMove = false;
			this.gB3_12.OptionsBand.AllowPress = false;
			this.gB3_12.OptionsBand.AllowSize = false;
			this.gB3_12.Width = 84;
			// 
			// C22
			// 
			this.C22.AppearanceHeader.Options.UseTextOptions = true;
			this.C22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C22.Caption = "12";
			this.C22.ColumnEdit = this.repositoryItemTextEdit9;
			this.C22.FieldName = "M12";
			this.C22.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C22.Name = "C22";
			this.C22.OptionsFilter.AllowAutoFilter = false;
			this.C22.OptionsFilter.AllowFilter = false;
			this.C22.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C22.SummaryItem.DisplayFormat = "{0:n0}";
			this.C22.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C22.Visible = true;
			this.C22.Width = 84;
			// 
			// gB3_D
			// 
			this.gB3_D.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
			this.gB3_D.AppearanceHeader.Options.UseFont = true;
			this.gB3_D.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_D.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_D.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_D.Caption = "�������²     ����� ";
			this.gB3_D.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB3_13,
            this.gB3_13A});
			this.gB3_D.Name = "gB3_D";
			this.gB3_D.Width = 158;
			// 
			// gB3_13
			// 
			this.gB3_13.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_13.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_13.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_13.Caption = "�����";
			this.gB3_13.Columns.Add(this.C23);
			this.gB3_13.Name = "gB3_13";
			this.gB3_13.OptionsBand.AllowMove = false;
			this.gB3_13.OptionsBand.AllowPress = false;
			this.gB3_13.OptionsBand.AllowSize = false;
			this.gB3_13.Width = 73;
			// 
			// C23
			// 
			this.C23.AppearanceHeader.Options.UseTextOptions = true;
			this.C23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C23.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C23.Caption = "13";
			this.C23.ColumnEdit = this.repositoryItemTextEdit9;
			this.C23.FieldName = "M13";
			this.C23.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C23.Name = "C23";
			this.C23.OptionsFilter.AllowAutoFilter = false;
			this.C23.OptionsFilter.AllowFilter = false;
			this.C23.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C23.SummaryItem.DisplayFormat = "{0:n0}";
			this.C23.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C23.Visible = true;
			this.C23.Width = 73;
			// 
			// gB3_13A
			// 
			this.gB3_13A.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_13A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_13A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_13A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_13A.AutoFillDown = false;
			this.gB3_13A.Caption = "�� �������� ��  �����������  ��������";
			this.gB3_13A.Columns.Add(this.C13A);
			this.gB3_13A.Name = "gB3_13A";
			this.gB3_13A.RowCount = 3;
			this.gB3_13A.Width = 85;
			// 
			// C13A
			// 
			this.C13A.AppearanceHeader.Options.UseTextOptions = true;
			this.C13A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C13A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C13A.Caption = "13A";
			this.C13A.ColumnEdit = this.repositoryItemTextEdit9;
			this.C13A.FieldName = "M13A";
			this.C13A.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C13A.Name = "C13A";
			this.C13A.SummaryItem.DisplayFormat = "{0:n0}";
			this.C13A.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C13A.Visible = true;
			this.C13A.Width = 85;
			// 
			// gB3_14
			// 
			this.gB3_14.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB3_14.AppearanceHeader.Options.UseFont = true;
			this.gB3_14.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_14.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_14.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_14.Caption = " ������      ����Ĳ� ";
			this.gB3_14.Columns.Add(this.C24);
			this.gB3_14.Name = "gB3_14";
			this.gB3_14.OptionsBand.AllowMove = false;
			this.gB3_14.OptionsBand.AllowPress = false;
			this.gB3_14.OptionsBand.AllowSize = false;
			this.gB3_14.Width = 106;
			// 
			// C24
			// 
			this.C24.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C24.AppearanceCell.Options.UseFont = true;
			this.C24.AppearanceHeader.Options.UseTextOptions = true;
			this.C24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C24.Caption = "14";
			this.C24.ColumnEdit = this.repositoryItemTextEdit9;
			this.C24.FieldName = "M14";
			this.C24.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C24.Name = "C24";
			this.C24.OptionsFilter.AllowAutoFilter = false;
			this.C24.OptionsFilter.AllowFilter = false;
			this.C24.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C24.SummaryItem.DisplayFormat = "{0:n0}";
			this.C24.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C24.Visible = true;
			this.C24.Width = 106;
			// 
			// gB3_15
			// 
			this.gB3_15.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB3_15.AppearanceHeader.Options.UseFont = true;
			this.gB3_15.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_15.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_15.Caption = " �в�   ���  �� �������- �������         ��  ������������";
			this.gB3_15.Columns.Add(this.C25);
			this.gB3_15.Name = "gB3_15";
			this.gB3_15.OptionsBand.AllowMove = false;
			this.gB3_15.OptionsBand.AllowPress = false;
			this.gB3_15.OptionsBand.AllowSize = false;
			this.gB3_15.Width = 90;
			// 
			// C25
			// 
			this.C25.AppearanceHeader.Options.UseTextOptions = true;
			this.C25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C25.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C25.Caption = "15";
			this.C25.ColumnEdit = this.repositoryItemTextEdit9;
			this.C25.FieldName = "M15";
			this.C25.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C25.Name = "C25";
			this.C25.OptionsFilter.AllowAutoFilter = false;
			this.C25.OptionsFilter.AllowFilter = false;
			this.C25.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C25.SummaryItem.DisplayFormat = "{0:n0}";
			this.C25.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C25.Visible = true;
			this.C25.Width = 90;
			// 
			// gB3_15A
			// 
			this.gB3_15A.AppearanceHeader.Options.UseTextOptions = true;
			this.gB3_15A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB3_15A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB3_15A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB3_15A.Caption = "���� �� �����. ����� (�����,  �����)";
			this.gB3_15A.Columns.Add(this.C15A);
			this.gB3_15A.Name = "gB3_15A";
			this.gB3_15A.Width = 68;
			// 
			// C15A
			// 
			this.C15A.AppearanceHeader.Options.UseTextOptions = true;
			this.C15A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C15A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.C15A.Caption = "15A";
			this.C15A.ColumnEdit = this.repositoryItemTextEdit9;
			this.C15A.FieldName = "M15A";
			this.C15A.Name = "C15A";
			this.C15A.OptionsFilter.AllowAutoFilter = false;
			this.C15A.OptionsFilter.AllowFilter = false;
			this.C15A.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C15A.SummaryItem.DisplayFormat = "{0:n0}";
			this.C15A.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C15A.Visible = true;
			this.C15A.Width = 68;
			// 
			// grid3
			// 
			this.grid3.Location = new System.Drawing.Point(1, 1);
			this.grid3.MainView = this.advBandedGridView3;
			this.grid3.Name = "grid3";
			this.grid3.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit5,
            this.repositoryItemTextEdit6,
            this.repositoryItemTextEdit9});
			this.grid3.Size = new System.Drawing.Size(1019, 512);
			this.grid3.TabIndex = 2;
			this.grid3.ToolTipController = this.toolTipController1;
			this.grid3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView3});
			// 
			// repositoryItemTextEdit5
			// 
			this.repositoryItemTextEdit5.AutoHeight = false;
			this.repositoryItemTextEdit5.Name = "repositoryItemTextEdit5";
			this.repositoryItemTextEdit5.PasswordChar = '*';
			// 
			// repositoryItemTextEdit6
			// 
			this.repositoryItemTextEdit6.AutoHeight = false;
			this.repositoryItemTextEdit6.Name = "repositoryItemTextEdit6";
			// 
			// advBandedGridView4
			// 
			this.advBandedGridView4.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView4.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView4.AppearancePrint.BandPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView4.AppearancePrint.BandPanel.Options.UseBackColor = true;
			this.advBandedGridView4.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView4.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView4.AppearancePrint.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridView4.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView4.AppearancePrint.FooterPanel.Options.UseBackColor = true;
			this.advBandedGridView4.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView4.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
			this.advBandedGridView4.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB4_r,
            this.gB4_n,
            this.gB4_M,
            this.gB4_25});
			this.advBandedGridView4.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.C0T,
            this.C1T,
            this.C26,
            this.C27,
            this.C28,
            this.C29,
            this.C30,
            this.C31,
            this.C32,
            this.C33,
            this.C34,
            this.C35,
            this.C36});
			this.advBandedGridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.advBandedGridView4.GridControl = this.grid4;
			this.advBandedGridView4.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
			this.advBandedGridView4.HorzScrollStep = 1;
			this.advBandedGridView4.Name = "advBandedGridView4";
			this.advBandedGridView4.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView4.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView4.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView4.OptionsCustomization.AllowSort = false;
			this.advBandedGridView4.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView4.OptionsLayout.StoreAllOptions = true;
			this.advBandedGridView4.OptionsLayout.StoreAppearance = true;
			this.advBandedGridView4.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView4.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView4.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView4.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView4.OptionsPrint.ExpandAllDetails = true;
			this.advBandedGridView4.OptionsPrint.PrintDetails = true;
			this.advBandedGridView4.OptionsPrint.PrintPreview = true;
			this.advBandedGridView4.OptionsPrint.UsePrintStyles = true;
			this.advBandedGridView4.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView4.OptionsSelection.InvertSelection = true;
			this.advBandedGridView4.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView4.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView4.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridView4.OptionsView.ShowFooter = true;
			this.advBandedGridView4.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView4.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
			this.advBandedGridView4.SynchronizeClones = false;
			this.advBandedGridView4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseUp);
			// 
			// gB4_r
			// 
			this.gB4_r.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_r.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_r.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_r.Caption = "���";
			this.gB4_r.Columns.Add(this.C0T);
			this.gB4_r.Name = "gB4_r";
			this.gB4_r.OptionsBand.AllowMove = false;
			this.gB4_r.OptionsBand.AllowPress = false;
			this.gB4_r.OptionsBand.AllowSize = false;
			this.gB4_r.RowCount = 5;
			this.gB4_r.ToolTip = "��� �������i";
			this.gB4_r.Width = 34;
			// 
			// C0T
			// 
			this.C0T.AppearanceCell.Options.UseTextOptions = true;
			this.C0T.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0T.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0T.AppearanceHeader.Options.UseTextOptions = true;
			this.C0T.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0T.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0T.FieldName = "ROAD";
			this.C0T.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C0T.Name = "C0T";
			this.C0T.OptionsColumn.AllowEdit = false;
			this.C0T.OptionsColumn.AllowFocus = false;
			this.C0T.OptionsColumn.AllowMove = false;
			this.C0T.OptionsColumn.AllowSize = false;
			this.C0T.OptionsColumn.FixedWidth = true;
			this.C0T.OptionsFilter.AllowAutoFilter = false;
			this.C0T.OptionsFilter.AllowFilter = false;
			this.C0T.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C0T.ToolTip = "���";
			this.C0T.Visible = true;
			this.C0T.Width = 34;
			// 
			// gB4_n
			// 
			this.gB4_n.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.gB4_n.AppearanceHeader.Options.UseFont = true;
			this.gB4_n.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_n.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_n.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_n.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_n.AutoFillDown = false;
			this.gB4_n.Caption = "����� �������� (����������)";
			this.gB4_n.Columns.Add(this.C1T);
			this.gB4_n.Name = "gB4_n";
			this.gB4_n.OptionsBand.AllowMove = false;
			this.gB4_n.OptionsBand.AllowPress = false;
			this.gB4_n.OptionsBand.AllowSize = false;
			this.gB4_n.RowCount = 6;
			this.gB4_n.ToolTip = "������ ��������";
			this.gB4_n.Width = 184;
			// 
			// C1T
			// 
			this.C1T.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C1T.AppearanceHeader.Options.UseFont = true;
			this.C1T.AppearanceHeader.Options.UseTextOptions = true;
			this.C1T.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C1T.Caption = "1";
			this.C1T.FieldName = "NAIM";
			this.C1T.Name = "C1T";
			this.C1T.OptionsColumn.AllowEdit = false;
			this.C1T.OptionsColumn.AllowFocus = false;
			this.C1T.OptionsColumn.AllowMove = false;
			this.C1T.OptionsColumn.AllowSize = false;
			this.C1T.OptionsColumn.FixedWidth = true;
			this.C1T.OptionsFilter.AllowAutoFilter = false;
			this.C1T.OptionsFilter.AllowFilter = false;
			this.C1T.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C1T.SummaryItem.DisplayFormat = "������";
			this.C1T.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
			this.C1T.ToolTip = "����� ��������";
			this.C1T.UnboundType = DevExpress.Data.UnboundColumnType.String;
			this.C1T.Visible = true;
			this.C1T.Width = 184;
			// 
			// gB4_M
			// 
			this.gB4_M.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_M.AppearanceHeader.Options.UseFont = true;
			this.gB4_M.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_M.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_M.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_M.AutoFillDown = false;
			this.gB4_M.Caption = "�  �  �  �  �  �  �  �  �  �    � � � � � � � � � � ";
			this.gB4_M.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB4_16_21,
            this.gB4_22_23A,
            this.gB4_24});
			this.gB4_M.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB4_M.MinWidth = 20;
			this.gB4_M.Name = "gB4_M";
			this.gB4_M.OptionsBand.AllowMove = false;
			this.gB4_M.OptionsBand.AllowPress = false;
			this.gB4_M.OptionsBand.AllowSize = false;
			this.gB4_M.RowCount = 2;
			this.gB4_M.Width = 670;
			// 
			// gB4_16_21
			// 
			this.gB4_16_21.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_16_21.AppearanceHeader.Options.UseFont = true;
			this.gB4_16_21.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_16_21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_16_21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_16_21.AutoFillDown = false;
			this.gB4_16_21.Caption = "� � � � � � �    � � � � �    �����";
			this.gB4_16_21.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB4_16,
            this.gB4_17,
            this.gB4_18,
            this.gB4_19,
            this.gB4_20,
            this.gB4_21});
			this.gB4_16_21.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB4_16_21.Name = "gB4_16_21";
			this.gB4_16_21.OptionsBand.AllowMove = false;
			this.gB4_16_21.OptionsBand.AllowPress = false;
			this.gB4_16_21.OptionsBand.AllowSize = false;
			this.gB4_16_21.Width = 378;
			// 
			// gB4_16
			// 
			this.gB4_16.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
			this.gB4_16.AppearanceHeader.Options.UseFont = true;
			this.gB4_16.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_16.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_16.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_16.AutoFillDown = false;
			this.gB4_16.Caption = "������          ������I�";
			this.gB4_16.Columns.Add(this.C26);
			this.gB4_16.Name = "gB4_16";
			this.gB4_16.OptionsBand.AllowMove = false;
			this.gB4_16.OptionsBand.AllowPress = false;
			this.gB4_16.OptionsBand.AllowSize = false;
			this.gB4_16.RowCount = 3;
			this.gB4_16.Width = 61;
			// 
			// C26
			// 
			this.C26.AppearanceHeader.Options.UseTextOptions = true;
			this.C26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C26.Caption = "16";
			this.C26.ColumnEdit = this.repositoryItemTextEdit9;
			this.C26.FieldName = "M16";
			this.C26.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C26.Name = "C26";
			this.C26.OptionsFilter.AllowAutoFilter = false;
			this.C26.OptionsFilter.AllowFilter = false;
			this.C26.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C26.SummaryItem.DisplayFormat = "{0:n0}";
			this.C26.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C26.Visible = true;
			this.C26.Width = 61;
			// 
			// gB4_17
			// 
			this.gB4_17.AppearanceHeader.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_17.AppearanceHeader.Options.UseFont = true;
			this.gB4_17.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_17.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_17.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_17.AutoFillDown = false;
			this.gB4_17.Caption = "��������� �����ֲ�";
			this.gB4_17.Columns.Add(this.C27);
			this.gB4_17.Name = "gB4_17";
			this.gB4_17.OptionsBand.AllowMove = false;
			this.gB4_17.OptionsBand.AllowPress = false;
			this.gB4_17.OptionsBand.AllowSize = false;
			this.gB4_17.RowCount = 3;
			this.gB4_17.Width = 66;
			// 
			// C27
			// 
			this.C27.AppearanceHeader.Options.UseTextOptions = true;
			this.C27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C27.Caption = "17";
			this.C27.ColumnEdit = this.repositoryItemTextEdit9;
			this.C27.FieldName = "M17";
			this.C27.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C27.Name = "C27";
			this.C27.OptionsFilter.AllowAutoFilter = false;
			this.C27.OptionsFilter.AllowFilter = false;
			this.C27.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C27.SummaryItem.DisplayFormat = "{0:n0}";
			this.C27.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C27.Visible = true;
			this.C27.Width = 66;
			// 
			// gB4_18
			// 
			this.gB4_18.AppearanceHeader.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_18.AppearanceHeader.Options.UseFont = true;
			this.gB4_18.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_18.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_18.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_18.AutoFillDown = false;
			this.gB4_18.Caption = "ʲ�����      �����ֲ�";
			this.gB4_18.Columns.Add(this.C28);
			this.gB4_18.Name = "gB4_18";
			this.gB4_18.OptionsBand.AllowMove = false;
			this.gB4_18.OptionsBand.AllowPress = false;
			this.gB4_18.OptionsBand.AllowSize = false;
			this.gB4_18.RowCount = 3;
			this.gB4_18.Width = 59;
			// 
			// C28
			// 
			this.C28.AppearanceHeader.Options.UseTextOptions = true;
			this.C28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C28.Caption = "18";
			this.C28.ColumnEdit = this.repositoryItemTextEdit9;
			this.C28.FieldName = "M18";
			this.C28.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C28.Name = "C28";
			this.C28.OptionsFilter.AllowAutoFilter = false;
			this.C28.OptionsFilter.AllowFilter = false;
			this.C28.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C28.SummaryItem.DisplayFormat = "{0:n0}";
			this.C28.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C28.Visible = true;
			this.C28.Width = 59;
			// 
			// gB4_19
			// 
			this.gB4_19.AppearanceHeader.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_19.AppearanceHeader.Options.UseFont = true;
			this.gB4_19.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_19.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_19.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_19.AutoFillDown = false;
			this.gB4_19.Caption = "�������²         �����                    �����";
			this.gB4_19.Columns.Add(this.C29);
			this.gB4_19.Name = "gB4_19";
			this.gB4_19.OptionsBand.AllowMove = false;
			this.gB4_19.OptionsBand.AllowPress = false;
			this.gB4_19.OptionsBand.AllowSize = false;
			this.gB4_19.RowCount = 3;
			this.gB4_19.Width = 64;
			// 
			// C29
			// 
			this.C29.AppearanceHeader.Options.UseTextOptions = true;
			this.C29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C29.Caption = "19";
			this.C29.ColumnEdit = this.repositoryItemTextEdit9;
			this.C29.FieldName = "M19";
			this.C29.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C29.Name = "C29";
			this.C29.OptionsFilter.AllowAutoFilter = false;
			this.C29.OptionsFilter.AllowFilter = false;
			this.C29.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C29.SummaryItem.DisplayFormat = "{0:n0}";
			this.C29.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C29.Visible = true;
			this.C29.Width = 64;
			// 
			// gB4_20
			// 
			this.gB4_20.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_20.AppearanceHeader.Options.UseFont = true;
			this.gB4_20.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_20.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_20.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_20.AutoFillDown = false;
			this.gB4_20.Caption = " ������      ����Ĳ� ";
			this.gB4_20.Columns.Add(this.C30);
			this.gB4_20.Name = "gB4_20";
			this.gB4_20.OptionsBand.AllowMove = false;
			this.gB4_20.OptionsBand.AllowPress = false;
			this.gB4_20.OptionsBand.AllowSize = false;
			this.gB4_20.RowCount = 3;
			this.gB4_20.Width = 57;
			// 
			// C30
			// 
			this.C30.AppearanceHeader.Options.UseTextOptions = true;
			this.C30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C30.Caption = "20";
			this.C30.ColumnEdit = this.repositoryItemTextEdit9;
			this.C30.FieldName = "M20";
			this.C30.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C30.Name = "C30";
			this.C30.OptionsFilter.AllowAutoFilter = false;
			this.C30.OptionsFilter.AllowFilter = false;
			this.C30.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C30.SummaryItem.DisplayFormat = "{0:n0}";
			this.C30.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C30.Visible = true;
			this.C30.Width = 57;
			// 
			// gB4_21
			// 
			this.gB4_21.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_21.AppearanceHeader.Options.UseFont = true;
			this.gB4_21.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_21.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_21.AutoFillDown = false;
			this.gB4_21.Caption = "���  ���  �� ������� ��  ��������";
			this.gB4_21.Columns.Add(this.C31);
			this.gB4_21.Name = "gB4_21";
			this.gB4_21.OptionsBand.AllowMove = false;
			this.gB4_21.OptionsBand.AllowPress = false;
			this.gB4_21.OptionsBand.AllowSize = false;
			this.gB4_21.RowCount = 3;
			this.gB4_21.Width = 71;
			// 
			// C31
			// 
			this.C31.AppearanceHeader.Options.UseTextOptions = true;
			this.C31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C31.Caption = "21";
			this.C31.ColumnEdit = this.repositoryItemTextEdit9;
			this.C31.FieldName = "M21";
			this.C31.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C31.Name = "C31";
			this.C31.OptionsFilter.AllowAutoFilter = false;
			this.C31.OptionsFilter.AllowFilter = false;
			this.C31.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C31.SummaryItem.DisplayFormat = "{0:n0}";
			this.C31.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C31.Visible = true;
			this.C31.Width = 71;
			// 
			// gB4_22_23A
			// 
			this.gB4_22_23A.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_22_23A.AppearanceHeader.Options.UseFont = true;
			this.gB4_22_23A.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_22_23A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_22_23A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_22_23A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_22_23A.AutoFillDown = false;
			this.gB4_22_23A.Caption = "�������  �  �����  ��ͲŲ  ��˲���ֲ";
			this.gB4_22_23A.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB4_22,
            this.gB4_23,
            this.gB4_23A});
			this.gB4_22_23A.Name = "gB4_22_23A";
			this.gB4_22_23A.Width = 213;
			// 
			// gB4_22
			// 
			this.gB4_22.AppearanceHeader.Font = new System.Drawing.Font("Arial", 6.75F);
			this.gB4_22.AppearanceHeader.Options.UseFont = true;
			this.gB4_22.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_22.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_22.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_22.AutoFillDown = false;
			this.gB4_22.Caption = " ������      ����Ĳ� ";
			this.gB4_22.Columns.Add(this.C32);
			this.gB4_22.Name = "gB4_22";
			this.gB4_22.OptionsBand.AllowMove = false;
			this.gB4_22.OptionsBand.AllowPress = false;
			this.gB4_22.OptionsBand.AllowSize = false;
			this.gB4_22.RowCount = 3;
			this.gB4_22.Width = 66;
			// 
			// C32
			// 
			this.C32.AppearanceHeader.Options.UseTextOptions = true;
			this.C32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C32.Caption = "22";
			this.C32.ColumnEdit = this.repositoryItemTextEdit9;
			this.C32.FieldName = "M22";
			this.C32.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C32.Name = "C32";
			this.C32.OptionsFilter.AllowAutoFilter = false;
			this.C32.OptionsFilter.AllowFilter = false;
			this.C32.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C32.SummaryItem.DisplayFormat = "{0:n0}";
			this.C32.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C32.Visible = true;
			this.C32.Width = 66;
			// 
			// gB4_23
			// 
			this.gB4_23.AppearanceHeader.Font = new System.Drawing.Font("Arial", 6.75F);
			this.gB4_23.AppearanceHeader.Options.UseFont = true;
			this.gB4_23.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_23.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_23.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_23.AutoFillDown = false;
			this.gB4_23.Caption = "�   ���  �������²  ����� ";
			this.gB4_23.Columns.Add(this.C33);
			this.gB4_23.Name = "gB4_23";
			this.gB4_23.OptionsBand.AllowMove = false;
			this.gB4_23.OptionsBand.AllowPress = false;
			this.gB4_23.OptionsBand.AllowSize = false;
			this.gB4_23.RowCount = 3;
			this.gB4_23.Width = 65;
			// 
			// C33
			// 
			this.C33.AppearanceHeader.Options.UseTextOptions = true;
			this.C33.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C33.Caption = "23";
			this.C33.ColumnEdit = this.repositoryItemTextEdit9;
			this.C33.FieldName = "M23";
			this.C33.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C33.Name = "C33";
			this.C33.OptionsFilter.AllowAutoFilter = false;
			this.C33.OptionsFilter.AllowFilter = false;
			this.C33.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C33.SummaryItem.DisplayFormat = "{0:n0}";
			this.C33.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C33.Visible = true;
			this.C33.Width = 65;
			// 
			// gB4_23A
			// 
			this.gB4_23A.AppearanceHeader.Font = new System.Drawing.Font("Arial", 6.75F);
			this.gB4_23A.AppearanceHeader.Options.UseFont = true;
			this.gB4_23A.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_23A.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_23A.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_23A.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_23A.AutoFillDown = false;
			this.gB4_23A.Caption = "�  �.�.  ��  ����������  ��  ����������";
			this.gB4_23A.Columns.Add(this.C34);
			this.gB4_23A.Name = "gB4_23A";
			this.gB4_23A.OptionsBand.AllowMove = false;
			this.gB4_23A.OptionsBand.AllowPress = false;
			this.gB4_23A.OptionsBand.AllowSize = false;
			this.gB4_23A.RowCount = 3;
			this.gB4_23A.Width = 82;
			// 
			// C34
			// 
			this.C34.AppearanceHeader.Options.UseTextOptions = true;
			this.C34.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C34.Caption = "23A";
			this.C34.ColumnEdit = this.repositoryItemTextEdit9;
			this.C34.FieldName = "M23A";
			this.C34.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C34.Name = "C34";
			this.C34.OptionsFilter.AllowAutoFilter = false;
			this.C34.OptionsFilter.AllowFilter = false;
			this.C34.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C34.SummaryItem.DisplayFormat = "{0:n0}";
			this.C34.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C34.Visible = true;
			this.C34.Width = 82;
			// 
			// gB4_24
			// 
			this.gB4_24.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_24.AppearanceHeader.Options.UseFont = true;
			this.gB4_24.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_24.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_24.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_24.AutoFillDown = false;
			this.gB4_24.Caption = "������ ����Ĳ� � ̲���- ������� ��������Ͳ ";
			this.gB4_24.Columns.Add(this.C35);
			this.gB4_24.Name = "gB4_24";
			this.gB4_24.OptionsBand.AllowMove = false;
			this.gB4_24.OptionsBand.AllowPress = false;
			this.gB4_24.OptionsBand.AllowSize = false;
			this.gB4_24.RowCount = 4;
			this.gB4_24.Width = 79;
			// 
			// C35
			// 
			this.C35.AppearanceHeader.Options.UseTextOptions = true;
			this.C35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C35.Caption = "24";
			this.C35.ColumnEdit = this.repositoryItemTextEdit9;
			this.C35.FieldName = "M24";
			this.C35.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C35.Name = "C35";
			this.C35.OptionsFilter.AllowAutoFilter = false;
			this.C35.OptionsFilter.AllowFilter = false;
			this.C35.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C35.SummaryItem.DisplayFormat = "{0:n0}";
			this.C35.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C35.Visible = true;
			this.C35.Width = 79;
			// 
			// gB4_25
			// 
			this.gB4_25.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB4_25.AppearanceHeader.Options.UseFont = true;
			this.gB4_25.AppearanceHeader.Options.UseTextOptions = true;
			this.gB4_25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB4_25.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB4_25.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB4_25.AutoFillDown = false;
			this.gB4_25.Caption = "������ ����Ĳ� �  �Ѳ�  �����  ����������  � ����������� ���  �� ���������� �� ��" +
				"����������";
			this.gB4_25.Columns.Add(this.C36);
			this.gB4_25.Name = "gB4_25";
			this.gB4_25.OptionsBand.AllowMove = false;
			this.gB4_25.OptionsBand.AllowPress = false;
			this.gB4_25.OptionsBand.AllowSize = false;
			this.gB4_25.RowCount = 6;
			this.gB4_25.Width = 101;
			// 
			// C36
			// 
			this.C36.AppearanceHeader.Options.UseTextOptions = true;
			this.C36.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C36.Caption = "25";
			this.C36.ColumnEdit = this.repositoryItemTextEdit9;
			this.C36.FieldName = "M25";
			this.C36.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C36.Name = "C36";
			this.C36.OptionsFilter.AllowAutoFilter = false;
			this.C36.OptionsFilter.AllowFilter = false;
			this.C36.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C36.SummaryItem.DisplayFormat = "{0:n0}";
			this.C36.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C36.Visible = true;
			this.C36.Width = 101;
			// 
			// grid4
			// 
			this.grid4.Location = new System.Drawing.Point(1, 1);
			this.grid4.MainView = this.advBandedGridView4;
			this.grid4.Name = "grid4";
			this.grid4.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit7,
            this.repositoryItemTextEdit8,
            this.repositoryItemTextEdit9});
			this.grid4.Size = new System.Drawing.Size(1020, 512);
			this.grid4.TabIndex = 3;
			this.grid4.ToolTipController = this.toolTipController1;
			this.grid4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView4});
			// 
			// repositoryItemTextEdit7
			// 
			this.repositoryItemTextEdit7.AutoHeight = false;
			this.repositoryItemTextEdit7.Name = "repositoryItemTextEdit7";
			this.repositoryItemTextEdit7.PasswordChar = '*';
			// 
			// repositoryItemTextEdit8
			// 
			this.repositoryItemTextEdit8.AutoHeight = false;
			this.repositoryItemTextEdit8.Name = "repositoryItemTextEdit8";
			// 
			// advBandedGridView9
			// 
			this.advBandedGridView9.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView9.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView9.AppearancePrint.BandPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView9.AppearancePrint.BandPanel.Options.UseBackColor = true;
			this.advBandedGridView9.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView9.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView9.AppearancePrint.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.advBandedGridView9.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView9.AppearancePrint.FooterPanel.Options.UseBackColor = true;
			this.advBandedGridView9.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.advBandedGridView9.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
			this.advBandedGridView9.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB5_r,
            this.gB5_n,
            this.gB5_2_8});
			this.advBandedGridView9.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.C0X,
            this.C1X,
            this.C37,
            this.C38,
            this.C39,
            this.C40,
            this.C41,
            this.C42,
            this.C43});
			this.advBandedGridView9.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.advBandedGridView9.GridControl = this.grid5;
			this.advBandedGridView9.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
			this.advBandedGridView9.HorzScrollStep = 1;
			this.advBandedGridView9.Name = "advBandedGridView9";
			this.advBandedGridView9.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView9.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView9.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView9.OptionsCustomization.AllowSort = false;
			this.advBandedGridView9.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView9.OptionsLayout.StoreAllOptions = true;
			this.advBandedGridView9.OptionsLayout.StoreAppearance = true;
			this.advBandedGridView9.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView9.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView9.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView9.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView9.OptionsPrint.ExpandAllDetails = true;
			this.advBandedGridView9.OptionsPrint.PrintDetails = true;
			this.advBandedGridView9.OptionsPrint.PrintPreview = true;
			this.advBandedGridView9.OptionsPrint.UsePrintStyles = true;
			this.advBandedGridView9.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView9.OptionsSelection.InvertSelection = true;
			this.advBandedGridView9.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView9.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView9.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridView9.OptionsView.ShowFooter = true;
			this.advBandedGridView9.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView9.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
			this.advBandedGridView9.SynchronizeClones = false;
			this.advBandedGridView9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseUp);
			// 
			// gB5_r
			// 
			this.gB5_r.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_r.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_r.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_r.Caption = "���";
			this.gB5_r.Columns.Add(this.C0X);
			this.gB5_r.Name = "gB5_r";
			this.gB5_r.OptionsBand.AllowMove = false;
			this.gB5_r.OptionsBand.AllowPress = false;
			this.gB5_r.OptionsBand.AllowSize = false;
			this.gB5_r.RowCount = 5;
			this.gB5_r.ToolTip = "��� �������i";
			this.gB5_r.Width = 34;
			// 
			// C0X
			// 
			this.C0X.AppearanceCell.Options.UseTextOptions = true;
			this.C0X.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0X.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.C0X.AppearanceHeader.Options.UseTextOptions = true;
			this.C0X.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C0X.FieldName = "ROAD";
			this.C0X.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C0X.Name = "C0X";
			this.C0X.OptionsColumn.AllowEdit = false;
			this.C0X.OptionsColumn.AllowFocus = false;
			this.C0X.OptionsColumn.AllowMove = false;
			this.C0X.OptionsColumn.AllowSize = false;
			this.C0X.OptionsColumn.FixedWidth = true;
			this.C0X.OptionsFilter.AllowAutoFilter = false;
			this.C0X.OptionsFilter.AllowFilter = false;
			this.C0X.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C0X.ToolTip = "���";
			this.C0X.Visible = true;
			this.C0X.Width = 34;
			// 
			// gB5_n
			// 
			this.gB5_n.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.gB5_n.AppearanceHeader.Options.UseFont = true;
			this.gB5_n.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_n.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_n.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_n.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_n.AutoFillDown = false;
			this.gB5_n.Caption = "����� �������� (����������)";
			this.gB5_n.Columns.Add(this.C1X);
			this.gB5_n.Name = "gB5_n";
			this.gB5_n.OptionsBand.AllowMove = false;
			this.gB5_n.OptionsBand.AllowPress = false;
			this.gB5_n.OptionsBand.AllowSize = false;
			this.gB5_n.RowCount = 6;
			this.gB5_n.ToolTip = "������ ��������";
			this.gB5_n.Width = 190;
			// 
			// C1X
			// 
			this.C1X.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C1X.AppearanceHeader.Options.UseFont = true;
			this.C1X.AppearanceHeader.Options.UseTextOptions = true;
			this.C1X.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C1X.Caption = "1";
			this.C1X.FieldName = "NAIM";
			this.C1X.Name = "C1X";
			this.C1X.OptionsColumn.AllowEdit = false;
			this.C1X.OptionsColumn.AllowFocus = false;
			this.C1X.OptionsColumn.AllowMove = false;
			this.C1X.OptionsColumn.AllowSize = false;
			this.C1X.OptionsColumn.FixedWidth = true;
			this.C1X.OptionsFilter.AllowAutoFilter = false;
			this.C1X.OptionsFilter.AllowFilter = false;
			this.C1X.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C1X.SummaryItem.DisplayFormat = "������";
			this.C1X.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
			this.C1X.ToolTip = "����� ��������";
			this.C1X.UnboundType = DevExpress.Data.UnboundColumnType.String;
			this.C1X.Visible = true;
			this.C1X.Width = 190;
			// 
			// gB5_2_8
			// 
			this.gB5_2_8.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_2_8.AppearanceHeader.Options.UseFont = true;
			this.gB5_2_8.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_2_8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_2_8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_2_8.AutoFillDown = false;
			this.gB5_2_8.Caption = " ��    � � � � � � �      � � � � � � I � ";
			this.gB5_2_8.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB5_2_4,
            this.gB5_5_7,
            this.gB5_8});
			this.gB5_2_8.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.gB5_2_8.MinWidth = 20;
			this.gB5_2_8.Name = "gB5_2_8";
			this.gB5_2_8.OptionsBand.AllowMove = false;
			this.gB5_2_8.OptionsBand.AllowPress = false;
			this.gB5_2_8.OptionsBand.AllowSize = false;
			this.gB5_2_8.RowCount = 2;
			this.gB5_2_8.Width = 765;
			// 
			// gB5_2_4
			// 
			this.gB5_2_4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_2_4.AppearanceHeader.Options.UseFont = true;
			this.gB5_2_4.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_2_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_2_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_2_4.Caption = "� � � � � I � � � ";
			this.gB5_2_4.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB5_2,
            this.gB5_3,
            this.gB5_4});
			this.gB5_2_4.Name = "gB5_2_4";
			this.gB5_2_4.OptionsBand.AllowMove = false;
			this.gB5_2_4.OptionsBand.AllowPress = false;
			this.gB5_2_4.OptionsBand.AllowSize = false;
			this.gB5_2_4.RowCount = 2;
			this.gB5_2_4.Width = 336;
			// 
			// gB5_2
			// 
			this.gB5_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_2.AppearanceHeader.Options.UseFont = true;
			this.gB5_2.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_2.AutoFillDown = false;
			this.gB5_2.Caption = "�I�����   ����������";
			this.gB5_2.Columns.Add(this.C37);
			this.gB5_2.Name = "gB5_2";
			this.gB5_2.OptionsBand.AllowMove = false;
			this.gB5_2.OptionsBand.AllowPress = false;
			this.gB5_2.OptionsBand.AllowSize = false;
			this.gB5_2.RowCount = 2;
			this.gB5_2.Width = 107;
			// 
			// C37
			// 
			this.C37.AppearanceHeader.Options.UseTextOptions = true;
			this.C37.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C37.Caption = "2";
			this.C37.ColumnEdit = this.repositoryItemTextEdit9;
			this.C37.FieldName = "X2";
			this.C37.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C37.Name = "C37";
			this.C37.OptionsFilter.AllowAutoFilter = false;
			this.C37.OptionsFilter.AllowFilter = false;
			this.C37.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C37.SummaryItem.DisplayFormat = "{0:n0}";
			this.C37.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C37.Visible = true;
			this.C37.Width = 107;
			// 
			// gB5_3
			// 
			this.gB5_3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_3.AppearanceHeader.Options.UseFont = true;
			this.gB5_3.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_3.AutoFillDown = false;
			this.gB5_3.Caption = "�����     ����������";
			this.gB5_3.Columns.Add(this.C38);
			this.gB5_3.Name = "gB5_3";
			this.gB5_3.OptionsBand.AllowMove = false;
			this.gB5_3.OptionsBand.AllowPress = false;
			this.gB5_3.OptionsBand.AllowSize = false;
			this.gB5_3.RowCount = 2;
			this.gB5_3.Width = 106;
			// 
			// C38
			// 
			this.C38.AppearanceHeader.Options.UseTextOptions = true;
			this.C38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C38.Caption = "3";
			this.C38.ColumnEdit = this.repositoryItemTextEdit9;
			this.C38.FieldName = "X3";
			this.C38.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C38.Name = "C38";
			this.C38.OptionsFilter.AllowAutoFilter = false;
			this.C38.OptionsFilter.AllowFilter = false;
			this.C38.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C38.SummaryItem.DisplayFormat = "{0:n0}";
			this.C38.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C38.Visible = true;
			this.C38.Width = 106;
			// 
			// gB5_4
			// 
			this.gB5_4.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_4.AppearanceHeader.Options.UseFont = true;
			this.gB5_4.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_4.Caption = "������";
			this.gB5_4.Columns.Add(this.C39);
			this.gB5_4.Name = "gB5_4";
			this.gB5_4.OptionsBand.AllowMove = false;
			this.gB5_4.OptionsBand.AllowPress = false;
			this.gB5_4.OptionsBand.AllowSize = false;
			this.gB5_4.RowCount = 2;
			this.gB5_4.Width = 123;
			// 
			// C39
			// 
			this.C39.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C39.AppearanceCell.Options.UseFont = true;
			this.C39.AppearanceHeader.Options.UseTextOptions = true;
			this.C39.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C39.Caption = "4";
			this.C39.ColumnEdit = this.repositoryItemTextEdit9;
			this.C39.FieldName = "X4";
			this.C39.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C39.Name = "C39";
			this.C39.OptionsFilter.AllowAutoFilter = false;
			this.C39.OptionsFilter.AllowFilter = false;
			this.C39.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C39.SummaryItem.DisplayFormat = "{0:n0}";
			this.C39.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C39.Visible = true;
			this.C39.Width = 123;
			// 
			// gB5_5_7
			// 
			this.gB5_5_7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_5_7.AppearanceHeader.Options.UseFont = true;
			this.gB5_5_7.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_5_7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_5_7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_5_7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_5_7.Caption = "� I � � � � � � � � ";
			this.gB5_5_7.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gB5_5,
            this.gB5_6,
            this.gB5_7});
			this.gB5_5_7.Name = "gB5_5_7";
			this.gB5_5_7.OptionsBand.AllowMove = false;
			this.gB5_5_7.OptionsBand.AllowPress = false;
			this.gB5_5_7.OptionsBand.AllowSize = false;
			this.gB5_5_7.RowCount = 2;
			this.gB5_5_7.Width = 298;
			// 
			// gB5_5
			// 
			this.gB5_5.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_5.AppearanceHeader.Options.UseFont = true;
			this.gB5_5.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_5.AutoFillDown = false;
			this.gB5_5.Caption = "�������";
			this.gB5_5.Columns.Add(this.C40);
			this.gB5_5.Name = "gB5_5";
			this.gB5_5.OptionsBand.AllowMove = false;
			this.gB5_5.OptionsBand.AllowPress = false;
			this.gB5_5.OptionsBand.AllowSize = false;
			this.gB5_5.RowCount = 2;
			this.gB5_5.Width = 95;
			// 
			// C40
			// 
			this.C40.AppearanceHeader.Options.UseTextOptions = true;
			this.C40.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C40.Caption = "5";
			this.C40.ColumnEdit = this.repositoryItemTextEdit9;
			this.C40.FieldName = "X5";
			this.C40.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C40.Name = "C40";
			this.C40.OptionsFilter.AllowAutoFilter = false;
			this.C40.OptionsFilter.AllowFilter = false;
			this.C40.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C40.SummaryItem.DisplayFormat = "{0:n0}";
			this.C40.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C40.Visible = true;
			this.C40.Width = 95;
			// 
			// gB5_6
			// 
			this.gB5_6.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_6.AppearanceHeader.Options.UseFont = true;
			this.gB5_6.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_6.AutoFillDown = false;
			this.gB5_6.Caption = "I�����";
			this.gB5_6.Columns.Add(this.C41);
			this.gB5_6.Name = "gB5_6";
			this.gB5_6.OptionsBand.AllowMove = false;
			this.gB5_6.OptionsBand.AllowPress = false;
			this.gB5_6.OptionsBand.AllowSize = false;
			this.gB5_6.RowCount = 2;
			this.gB5_6.Width = 99;
			// 
			// C41
			// 
			this.C41.AppearanceHeader.Options.UseTextOptions = true;
			this.C41.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C41.Caption = "6";
			this.C41.ColumnEdit = this.repositoryItemTextEdit9;
			this.C41.FieldName = "X6";
			this.C41.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C41.Name = "C41";
			this.C41.OptionsFilter.AllowAutoFilter = false;
			this.C41.OptionsFilter.AllowFilter = false;
			this.C41.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C41.SummaryItem.DisplayFormat = "{0:n0}";
			this.C41.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C41.Visible = true;
			this.C41.Width = 99;
			// 
			// gB5_7
			// 
			this.gB5_7.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_7.AppearanceHeader.Options.UseFont = true;
			this.gB5_7.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_7.AutoFillDown = false;
			this.gB5_7.Caption = "�������  ";
			this.gB5_7.Columns.Add(this.C42);
			this.gB5_7.Name = "gB5_7";
			this.gB5_7.OptionsBand.AllowMove = false;
			this.gB5_7.OptionsBand.AllowPress = false;
			this.gB5_7.OptionsBand.AllowSize = false;
			this.gB5_7.RowCount = 2;
			this.gB5_7.Width = 104;
			// 
			// C42
			// 
			this.C42.AppearanceHeader.Options.UseTextOptions = true;
			this.C42.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C42.Caption = "7";
			this.C42.ColumnEdit = this.repositoryItemTextEdit9;
			this.C42.FieldName = "X7";
			this.C42.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C42.Name = "C42";
			this.C42.OptionsFilter.AllowAutoFilter = false;
			this.C42.OptionsFilter.AllowFilter = false;
			this.C42.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C42.SummaryItem.DisplayFormat = "{0:n0}";
			this.C42.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C42.Visible = true;
			this.C42.Width = 104;
			// 
			// gB5_8
			// 
			this.gB5_8.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gB5_8.AppearanceHeader.Options.UseFont = true;
			this.gB5_8.AppearanceHeader.Options.UseTextOptions = true;
			this.gB5_8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB5_8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB5_8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB5_8.AutoFillDown = false;
			this.gB5_8.Caption = "������";
			this.gB5_8.Columns.Add(this.C43);
			this.gB5_8.Name = "gB5_8";
			this.gB5_8.OptionsBand.AllowMove = false;
			this.gB5_8.OptionsBand.AllowPress = false;
			this.gB5_8.OptionsBand.AllowSize = false;
			this.gB5_8.RowCount = 4;
			this.gB5_8.Width = 131;
			// 
			// C43
			// 
			this.C43.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.C43.AppearanceCell.Options.UseFont = true;
			this.C43.AppearanceHeader.Options.UseTextOptions = true;
			this.C43.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.C43.Caption = "8";
			this.C43.ColumnEdit = this.repositoryItemTextEdit9;
			this.C43.FieldName = "X8";
			this.C43.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.C43.Name = "C43";
			this.C43.OptionsFilter.AllowAutoFilter = false;
			this.C43.OptionsFilter.AllowFilter = false;
			this.C43.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.C43.SummaryItem.DisplayFormat = "{0:n0}";
			this.C43.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.C43.Visible = true;
			this.C43.Width = 131;
			// 
			// grid5
			// 
			this.grid5.Location = new System.Drawing.Point(1, 1);
			this.grid5.MainView = this.advBandedGridView9;
			this.grid5.Name = "grid5";
			this.grid5.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit17,
            this.repositoryItemTextEdit18,
            this.repositoryItemTextEdit9});
			this.grid5.Size = new System.Drawing.Size(1019, 512);
			this.grid5.TabIndex = 3;
			this.grid5.ToolTipController = this.toolTipController1;
			this.grid5.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView9});
			// 
			// repositoryItemTextEdit17
			// 
			this.repositoryItemTextEdit17.AutoHeight = false;
			this.repositoryItemTextEdit17.Name = "repositoryItemTextEdit17";
			this.repositoryItemTextEdit17.PasswordChar = '*';
			// 
			// repositoryItemTextEdit18
			// 
			this.repositoryItemTextEdit18.AutoHeight = false;
			this.repositoryItemTextEdit18.Name = "repositoryItemTextEdit18";
			// 
			// gB_6
			// 
			this.gB_6.AppearanceHeader.Options.UseTextOptions = true;
			this.gB_6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gB_6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gB_6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gB_6.Caption = "������ �������";
			this.gB_6.Name = "gB_6";
			this.gB_6.Width = 75;
			// 
			// imageListResources
			// 
			this.imageListResources.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListResources.ImageStream")));
			this.imageListResources.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListResources.Images.SetKeyName(0, "Crystal_Clear_app_korganizer.png");
			this.imageListResources.Images.SetKeyName(1, "");
			this.imageListResources.Images.SetKeyName(2, "");
			this.imageListResources.Images.SetKeyName(3, "Export_import.png");
			this.imageListResources.Images.SetKeyName(4, "");
			this.imageListResources.Images.SetKeyName(5, "");
			this.imageListResources.Images.SetKeyName(6, "Crystal_Clear_action_apply.png");
			this.imageListResources.Images.SetKeyName(7, "");
			this.imageListResources.Images.SetKeyName(8, "Crystal_Clear_app_kdict.png");
			this.imageListResources.Images.SetKeyName(9, "Crystal_Clear_action_2leftarrow.png");
			this.imageListResources.Images.SetKeyName(10, "Crystal_Clear_action_2rightarrow.png");
			this.imageListResources.Images.SetKeyName(11, "120px-Crystal_Clear_action_button_cancel.png");
			this.imageListResources.Images.SetKeyName(12, "32px-List-remove.svg.png");
			this.imageListResources.Images.SetKeyName(13, "48px-List-add.svg.png");
			this.imageListResources.Images.SetKeyName(14, "wait.gif");
			this.imageListResources.Images.SetKeyName(15, "Apply_1.png");
			this.imageListResources.Images.SetKeyName(16, "excel_2003_01.png");
			this.imageListResources.Images.SetKeyName(17, "Export_import_1.png");
			this.imageListResources.Images.SetKeyName(18, "BookC.gif");
			this.imageListResources.Images.SetKeyName(19, "BookO.gif");
			this.imageListResources.Images.SetKeyName(20, "UA.png");
			this.imageListResources.Images.SetKeyName(21, "RU.png");
			this.imageListResources.Images.SetKeyName(22, "_pie_chart.png");
			// 
			// barManager
			// 
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3,
            this.barBottom});
			this.barManager.DockControls.Add(this.barDockControl1);
			this.barManager.DockControls.Add(this.barDockControl2);
			this.barManager.DockControls.Add(this.barDockControl3);
			this.barManager.DockControls.Add(this.barDockControl4);
			this.barManager.Form = this;
			this.barManager.Images = this.imageListResources;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bB_Save,
            this.bB_Exit,
            this.bS_Mes,
            this.bS_God,
            this.bE_Mes,
            this.bE_God,
            this.bB_ImpExp,
            this.bB_Ok,
            this.bB_Del,
            this.bS_InfoLeft,
            this.bS_Info,
            this.bI_Forma,
            this.bB_PlotDiagram});
			this.barManager.MaxItemId = 54;
			this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rIs_God,
            this.rIs_Mes,
            this.rI_God,
            this.rI_Mes});
			this.barManager.ToolTipController = this.toolTipController1;
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bS_Mes, "", true, true, true, 87, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bS_God, "", false, true, true, 60, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bE_Mes, "", false, true, true, 80, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bE_God, "", false, true, true, 60, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_Ok, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bI_Forma)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawDragBorder = false;
			this.bar1.OptionsBar.MultiLine = true;
			this.bar1.OptionsBar.UseWholeRow = true;
			this.bar1.Text = "Tools";
			// 
			// bS_Mes
			// 
			this.bS_Mes.Caption = " ����� �";
			this.bS_Mes.Edit = this.rIs_Mes;
			this.bS_Mes.EditValue = 0;
			this.bS_Mes.Id = 52;
			this.bS_Mes.Name = "bS_Mes";
			this.bS_Mes.Width = 100;
			// 
			// rIs_Mes
			// 
			this.rIs_Mes.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rIs_Mes.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rIs_Mes.Appearance.Options.UseFont = true;
			this.rIs_Mes.Appearance.Options.UseForeColor = true;
			this.rIs_Mes.Appearance.Options.UseTextOptions = true;
			this.rIs_Mes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rIs_Mes.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.rIs_Mes.AutoHeight = false;
			this.rIs_Mes.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rIs_Mes.DropDownRows = 12;
			this.rIs_Mes.Name = "rIs_Mes";
			this.rIs_Mes.NullText = "����";
			this.rIs_Mes.ShowFooter = false;
			this.rIs_Mes.ShowHeader = false;
			this.rIs_Mes.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rIs_Mes_EditValueChanging);
			// 
			// bS_God
			// 
			this.bS_God.Edit = this.rIs_God;
			this.bS_God.Id = 53;
			this.bS_God.Name = "bS_God";
			// 
			// rIs_God
			// 
			this.rIs_God.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.rIs_God.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rIs_God.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rIs_God.Appearance.Options.UseFont = true;
			this.rIs_God.Appearance.Options.UseForeColor = true;
			this.rIs_God.Appearance.Options.UseTextOptions = true;
			this.rIs_God.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rIs_God.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.rIs_God.AutoHeight = false;
			this.rIs_God.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.rIs_God.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rIs_God.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rIs_God.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
			this.rIs_God.Mask.EditMask = "N00";
			this.rIs_God.MaxLength = 4;
			this.rIs_God.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
			this.rIs_God.MinValue = new decimal(new int[] {
            2012,
            0,
            0,
            0});
			this.rIs_God.Name = "rIs_God";
			this.rIs_God.NullText = "2012";
			this.rIs_God.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.rIs_God.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rIs_God_EditValueChanging);
			// 
			// bE_Mes
			// 
			this.bE_Mes.Caption = "��";
			this.bE_Mes.Edit = this.rI_Mes;
			this.bE_Mes.EditValue = 0;
			this.bE_Mes.Id = 42;
			this.bE_Mes.Name = "bE_Mes";
			this.bE_Mes.Width = 100;
			// 
			// rI_Mes
			// 
			this.rI_Mes.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
			this.rI_Mes.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rI_Mes.Appearance.Options.UseFont = true;
			this.rI_Mes.Appearance.Options.UseForeColor = true;
			this.rI_Mes.Appearance.Options.UseTextOptions = true;
			this.rI_Mes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rI_Mes.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.rI_Mes.AutoHeight = false;
			this.rI_Mes.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rI_Mes.DropDownRows = 12;
			this.rI_Mes.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_Mes.Name = "rI_Mes";
			this.rI_Mes.NullText = "�����";
			this.rI_Mes.ShowFooter = false;
			this.rI_Mes.ShowHeader = false;
			this.rI_Mes.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_Mes_EditValueChanging);
			// 
			// bE_God
			// 
			this.bE_God.Edit = this.rI_God;
			this.bE_God.Id = 6;
			this.bE_God.Name = "bE_God";
			// 
			// rI_God
			// 
			this.rI_God.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.rI_God.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
			this.rI_God.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rI_God.Appearance.Options.UseFont = true;
			this.rI_God.Appearance.Options.UseForeColor = true;
			this.rI_God.Appearance.Options.UseTextOptions = true;
			this.rI_God.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rI_God.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.rI_God.AutoHeight = false;
			this.rI_God.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.rI_God.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_God.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_God.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
			this.rI_God.IsFloatValue = false;
			this.rI_God.Mask.EditMask = "N00";
			this.rI_God.MaxLength = 4;
			this.rI_God.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
			this.rI_God.MinValue = new decimal(new int[] {
            2012,
            0,
            0,
            0});
			this.rI_God.Name = "rI_God";
			this.rI_God.NullText = "2012";
			this.rI_God.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.rI_God.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_God_EditValueChanging);
			// 
			// bB_Ok
			// 
			this.bB_Ok.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_Ok.Caption = " Ok ";
			this.bB_Ok.Id = 31;
			this.bB_Ok.ImageIndex = 15;
			this.bB_Ok.Name = "bB_Ok";
			this.bB_Ok.Tag = "";
			this.bB_Ok.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Ok_ItemClick);
			// 
			// bI_Forma
			// 
			this.bI_Forma.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bI_Forma.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bI_Forma.Caption = "�����  ��-19    ";
			this.bI_Forma.Id = 52;
			this.bI_Forma.Name = "bI_Forma";
			this.bI_Forma.OwnFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
			this.bI_Forma.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bI_Forma.UseOwnFont = true;
			// 
			// bar3
			// 
			this.bar3.BarName = "Main menu";
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_Save, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_ImpExp, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_Del, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_PlotDiagram, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_Exit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Main menu";
			// 
			// bB_Save
			// 
			this.bB_Save.Caption = "��������";
			this.bB_Save.Id = 1;
			this.bB_Save.ImageIndex = 5;
			this.bB_Save.Name = "bB_Save";
			this.bB_Save.Tag = "";
			this.bB_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Save_ItemClick);
			// 
			// bB_ImpExp
			// 
			this.bB_ImpExp.Caption = " ������-������� �����  ";
			this.bB_ImpExp.Id = 23;
			this.bB_ImpExp.ImageIndex = 3;
			this.bB_ImpExp.Name = "bB_ImpExp";
			this.bB_ImpExp.Tag = "";
			this.bB_ImpExp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_ImpExp_ItemClick);
			// 
			// bB_Del
			// 
			this.bB_Del.Caption = " ��������";
			this.bB_Del.Id = 47;
			this.bB_Del.ImageIndex = 11;
			this.bB_Del.Name = "bB_Del";
			this.bB_Del.Tag = "";
			this.bB_Del.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Del_ItemClick);
			// 
			// bB_PlotDiagram
			// 
			this.bB_PlotDiagram.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bB_PlotDiagram.Caption = "  ĳ������    ";
			this.bB_PlotDiagram.Id = 53;
			this.bB_PlotDiagram.ImageIndex = 22;
			this.bB_PlotDiagram.Name = "bB_PlotDiagram";
			this.bB_PlotDiagram.OwnFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bB_PlotDiagram.UseOwnFont = true;
			this.bB_PlotDiagram.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_PlotDiagram_ItemClick);
			// 
			// bB_Exit
			// 
			this.bB_Exit.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bB_Exit.Caption = "�����";
			this.bB_Exit.Id = 4;
			this.bB_Exit.ImageIndex = 4;
			this.bB_Exit.Name = "bB_Exit";
			this.bB_Exit.Tag = "";
			this.bB_Exit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Exit_ItemClick);
			// 
			// barBottom
			// 
			this.barBottom.Appearance.Options.UseTextOptions = true;
			this.barBottom.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.barBottom.BarName = "Custom 4";
			this.barBottom.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.barBottom.DockCol = 0;
			this.barBottom.DockRow = 0;
			this.barBottom.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.barBottom.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bS_InfoLeft),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bS_Info, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.barBottom.OptionsBar.AllowQuickCustomization = false;
			this.barBottom.OptionsBar.DrawDragBorder = false;
			this.barBottom.OptionsBar.UseWholeRow = true;
			this.barBottom.Text = "Custom 4";
			// 
			// bS_InfoLeft
			// 
			this.bS_InfoLeft.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			this.bS_InfoLeft.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bS_InfoLeft.Caption = "<bS_InfoLeft   -  ����������   �   ���������>";
			this.bS_InfoLeft.Id = 0;
			this.bS_InfoLeft.Name = "bS_InfoLeft";
			this.bS_InfoLeft.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// bS_Info
			// 
			this.bS_Info.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bS_Info.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bS_Info.Caption = "�������� ����������� ";
			this.bS_Info.Id = 4;
			this.bS_Info.Name = "bS_Info";
			this.bS_Info.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// cellValueControl1
			// 
			repositoryContent1.FieldLen = 15;
			repositoryContent1.Fraction = 0;
			repositoryContent1.RepItem = this.repositoryItemTextEdit9;
			viewContent1.RepItems.Add(repositoryContent1);
			viewContent1.View = this.advBandedGridView1;
			repositoryContent2.FieldLen = 15;
			repositoryContent2.Fraction = 0;
			repositoryContent2.RepItem = this.repositoryItemTextEdit9;
			viewContent2.RepItems.Add(repositoryContent2);
			viewContent2.View = this.advBandedGridView2;
			repositoryContent3.FieldLen = 15;
			repositoryContent3.Fraction = 0;
			repositoryContent3.RepItem = this.repositoryItemTextEdit9;
			viewContent3.RepItems.Add(repositoryContent3);
			viewContent3.View = this.advBandedGridView3;
			repositoryContent4.FieldLen = 15;
			repositoryContent4.Fraction = 0;
			repositoryContent4.RepItem = this.repositoryItemTextEdit9;
			viewContent4.RepItems.Add(repositoryContent4);
			viewContent4.View = this.advBandedGridView4;
			repositoryContent5.FieldLen = 15;
			repositoryContent5.Fraction = 0;
			repositoryContent5.RepItem = this.repositoryItemTextEdit9;
			viewContent5.RepItems.Add(repositoryContent5);
			viewContent5.View = this.advBandedGridView9;
			this.cellValueControl1.Views.Add(viewContent1);
			this.cellValueControl1.Views.Add(viewContent2);
			this.cellValueControl1.Views.Add(viewContent3);
			this.cellValueControl1.Views.Add(viewContent4);
			this.cellValueControl1.Views.Add(viewContent5);
			this.cellValueControl1.EndEditCell += new RDA.CellValueControl.EndEditCellEventHandler(this.cellValueControl1_EndEditCell);
			// 
			// dataManager1
			// 
			this.dataManager1.ExportToExcel.Caption = "������� � Excel ����";
			this.dataManager1.ExportToExcel.Visible = true;
			this.dataManager1.ImportFileName = "";
			this.dataManager1.ImportFromDB.Caption = "I�����  �  ��  UZC1";
			this.dataManager1.ImportFromDB.Visible = true;
			this.dataManager1.ImportFromExcel.Caption = "������ � Excel �����";
			this.dataManager1.ImportFromExcel.Visible = false;
			this.dataManager1.ImportFromFile.Caption = "������ � ���������� �����";
			this.dataManager1.ImportFromFile.Visible = false;
			this.dataManager1.PrintFile.Caption = "���� ���������";
			this.dataManager1.PrintFile.Visible = true;
			this.dataManager1.OnDBImport += new RDA.ManagerEventHandler(this.dataManager1_OnDBImport);
			this.dataManager1.OnFilePrint += new RDA.ManagerEventHandler(this.dataManager1_OnFilePrint);
			this.dataManager1.OnExcelExport += new RDA.ManagerEventHandler(this.dataManager1_OnExcelExport);
			// 
			// lb_Shapka
			// 
			this.lb_Shapka.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lb_Shapka.Appearance.Options.UseFont = true;
			this.lb_Shapka.Appearance.Options.UseTextOptions = true;
			this.lb_Shapka.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lb_Shapka.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.lb_Shapka.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.lb_Shapka.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lb_Shapka.Location = new System.Drawing.Point(157, 70);
			this.lb_Shapka.Name = "lb_Shapka";
			this.lb_Shapka.Size = new System.Drawing.Size(713, 29);
			this.lb_Shapka.TabIndex = 166;
			this.lb_Shapka.Text = "�����Ĳ�  ����Ĳ�   ²�   �����������   �����Ʋ�   ��   �����  ���������� ";
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.Location = new System.Drawing.Point(3, 124);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.SelectedTabPage = this.Page1;
			this.xtraTabControl1.Size = new System.Drawing.Size(1032, 544);
			this.xtraTabControl1.TabIndex = 167;
			this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Page1,
            this.Page2,
            this.Page3,
            this.Page4,
            this.Page5});
			this.xtraTabControl1.ToolTipController = this.toolTipController1;
			this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
			// 
			// Page1
			// 
			this.Page1.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Control;
			this.Page1.Appearance.PageClient.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Page1.Appearance.PageClient.Options.UseBackColor = true;
			this.Page1.Appearance.PageClient.Options.UseForeColor = true;
			this.Page1.Controls.Add(this.grid1);
			this.Page1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Page1.Name = "Page1";
			this.Page1.Size = new System.Drawing.Size(1023, 513);
			this.Page1.Text = "1.�����i�. ����.";
			this.Page1.Tooltip = "�����i������������ ���������� ";
			// 
			// Page2
			// 
			this.Page2.Controls.Add(this.grid2);
			this.Page2.Name = "Page2";
			this.Page2.Size = new System.Drawing.Size(1023, 513);
			this.Page2.Text = "2. �i�������� �������";
			this.Page2.Tooltip = "�i��������  ���������� - �������";
			// 
			// Page3
			// 
			this.Page3.Controls.Add(this.grid3);
			this.Page3.Name = "Page3";
			this.Page3.Size = new System.Drawing.Size(1023, 513);
			this.Page3.Text = "3. �i�������� IM����";
			this.Page3.Tooltip = "�i��������  ���������� - IM����";
			// 
			// Page4
			// 
			this.Page4.Controls.Add(this.grid4);
			this.Page4.Name = "Page4";
			this.Page4.Size = new System.Drawing.Size(1023, 513);
			this.Page4.Text = "4. �i�������� �������";
			this.Page4.Tooltip = "�i��������  ���������� � �������  ����i �����  ��  ������� � ����� ���i�i �����i" +
				"�i";
			// 
			// Page5
			// 
			this.Page5.Controls.Add(this.grid5);
			this.Page5.Name = "Page5";
			this.Page5.Size = new System.Drawing.Size(1023, 513);
			this.Page5.Text = "5.������� ������i�";
			this.Page5.Tooltip = "�������� ��� ��  ������� ������i�";
			// 
			// FRMCO19MN
			// 
			this.Appearance.ForeColor = System.Drawing.Color.Black;
			this.Appearance.Options.UseForeColor = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(1042, 698);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.l_Grn);
			this.Controls.Add(this.xtraTabControl1);
			this.Controls.Add(this.lb_Shapka);
			this.Controls.Add(this.l_Period);
			this.Controls.Add(this.barDockControl3);
			this.Controls.Add(this.barDockControl4);
			this.Controls.Add(this.barDockControl2);
			this.Controls.Add(this.barDockControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FRMCO19MN";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.toolTipController1.SetSuperTip(this, null);
			this.Text = "FRMCO19MN(D0CO19M)  �����  ��-19 -������� ������ �� ����������� ������� �� ��" +
				"���  ����������";
			this.Load += new System.EventHandler(this.FRMCO19MN_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMCO19MN_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FRMCO19MN_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit17)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit18)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rIs_Mes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rIs_God)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_Mes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_God)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cellValueControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.xtraTabControl1.ResumeLayout(false);
			this.Page1.ResumeLayout(false);
			this.Page2.ResumeLayout(false);
			this.Page3.ResumeLayout(false);
			this.Page4.ResumeLayout(false);
			this.Page5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		[STAThread]
		static void Main() 
		{   // ��������� ������� Main()� ����������� ����������� 
			// ���������� ������ � ������ ����������, � �����
			// ������������ DLL_���� ��� ������� ������������

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
			//System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
			//RDA.RDF.BDOraCon = new OracleConnection("Server=vd.uz.gov.ua;User Id=roduz;Password=rd;Direct=True;Port=1522;Sid=VD;");
			//RDA.RDF.BDOraCon.Open();
			//RDA.RDF rdaf = new RDA.RDF();
			//rdaf.p_EditTabl("1");

			// Application.Run(new FRMCO19MN(new OracleConnection("user id=VD;data source=MCP;password=buh"),1, "Supervisor", 'F'));
			// Application.Run(new FRMCO19MN(new OracleConnection("user id=RD;data source=MCP;password=RD"),1, "Supervisor", 'F'));
//			Application.Run(new FRMCO19MN(new OracleConnection("user id=RODUZ;data source=UZB2;password=rd"),1, "Supervisor", 'F', 0, 2008));
//			Application.Run(new FRMCO19MN(new OracleConnection("user id=RODUZ;data source=UZB2;password=rd"),1, "Supervisor", 'F'));		
			Helpers.ConnectionHelper.InitializeConnection();
            Application.Run(new FRMCO19MN(RDA.RDF.BDOraCon, 1, "Supervisor", 'F', 1, 2012));
		}
		private void FRMCO19MN_Load(object sender, System.EventArgs e)
		{
			#region myAccess- ����������� �������("ALL","ADMIN","USER", "READ" ) � ��������
			//-------------------------------------------------------------------------------------		
			//  myAccess = RdaFunc.p_Access(uId.ToString()) : 
			//           = "ALL"   - ������������� ��         (����� ������� cAcKey='F')
			//           = "ADMIN" - ������������� �� ������  (����� ������� cAcKey='F')
			//           = "USER"  - ������������   �� ������ (����� ������� cAcKey='F')
			//           = "READ"  - ������ �������� ������   (����� ������� cAcKey='R') 
			//           = "FALSE" - ������ ������
			//-------------------------------------------------------------------------------------		
			myAccess = RDA.RDF.UserParam.myAccess;   // ��������� �� Rda �������� myAccess 26/10/2010

			// ������� �������-���������� �� �������������� ������� 
			bEditTabl = (cAcKey == 'R' || RDA.RDF.UserParam.myAccess == "READ") ? false : true; 

			#endregion

			#region ����������� ��������� ������ � DataSet(Ds)
			// Ds(DataSet)- RowNum_����� �������� ��������� ������ ��� �������� � ��(����):             
			//              ������ �� ��, ����������� ������������ � constraints
			Ds = new DataSet();

			Ds.Tables.Add("Ds_Import");   // OraDAImp  -������� ������������� ������ � ���. �� ����(����� ��)
			Ds.Tables.Add(tBShabl);       // OraDAShabl-�������-������ ��� ����������� ������ � �����
			Ds.Tables.Add(tBArh);         // OraDAArh-�������-������ ���������� 
			#endregion			

			#region �������� �������� ��������� � ���������� ������ �������
			// ��������� ������ ������� :=true-�������� � ��;= false - � �� 

			RDA.RDF.p_FillMonthList(bE_Mes.Edit as Repository.RepositoryItemLookUpEdit, false);
			RDA.RDF.p_FillMonthList(bS_Mes.Edit as Repository.RepositoryItemLookUpEdit, true);

			// ������� ������� � Ds � �������� OracleDataAdapter ��� ������ ������� SELECT

			Ds.Tables[tBShabl].Clear();
			OraDAShabl = new OracleDataAdapter(@"SELECT * FROM "+ tBShabl +
				@" WHERE DOC='"+ myDoc + "' ORDER BY Doc, Position", RDA.RDF.BDOraCon); 
			#endregion

			#region ��������� ��������� �������� � �������� ������� ������ � �������

			if(sMes==0) sMes = DateTime.Today.Month;  // ��������� �������    �����
			if(sGod==0) sGod = DateTime.Today.Year ;   //  � ���   ��� �������� ����
			if (sGod < 2012)
			{
				sGod = 2012;
				sMes = 1;
			}
			eMes    = sMes ;  // ��������� ������� - �����
			eGod    = sGod ;  //                     ���
			bS_God.EditValue = sGod;    // �������� ���� ���� �� ������
			bS_Mes.EditValue = sMes;
			bE_God.EditValue = eGod;
			bE_Mes.EditValue = eMes;

			//p_LanghInfo();  // ��� ������ ���������
			
			myView=advBandedGridView1; // ����������� ���������� ���� � Grid1 
			// ������������� ������ FilterControl ��� �������� ��������� ������
			RdaFiltr=new RDA.FilterControl(bE_Mes, bE_God, SetOKEnableStatus, p_Save);

			bB_Ok_ItemClick(null, null);
				
			#endregion				
		}
	
		#region ������, ��������� � ��������: ������, ������  � ��. ��

		// ..._EditValueChanging - ������ �������������� ����� ���������� ����� �������
		private void rIs_Mes_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rIs_Mes_EditValueChanging - ����� ������������ ����� ���������� ���������� ������
			//                             �������� ������ ������ ��� ���������� ��������� ������ 
			//-------------------------------------------------------------------------------------
			if (bTag)
			{
				p_CloseAllEditors();//�������� ��������� ������ ��� ���������� ���������� ���������
				e.Cancel = RdaFiltr.f_ControlFilterChanging(false, e.NewValue, bS_Mes, bE_Mes, bView, bUpdate, out bTag);
			}
		}
		private void rIs_God_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rIs_God_EditValueChanging - ����� ������������ ����� ���������� ���������� ����
			//                             �������� ������ ������ ��� ���������� ��������� ����
			//-------------------------------------------------------------------------------------
			if (bTag)
			{
				p_CloseAllEditors();//�������� ��������� ������ ��� ���������� ���������� ���������
				e.Cancel = RdaFiltr.f_ControlFilterChanging(false, e.NewValue, bS_God, bE_God, bView, bUpdate, out bTag) ;
			}


		}
		private void rI_Mes_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_Mes_EditValueChanging - ����� ������������ ����� ���������� ��������� ������
			//-------------------------------------------------------------------------------------
			if (bTag)
			{
				p_CloseAllEditors();//�������� ��������� ������ ��� ���������� ���������� ���������
				e.Cancel = RdaFiltr.f_ControlFilterChanging(false, e.NewValue,  bE_Mes, null, bView, bUpdate, out bTag);
			}
		}
		private void rI_God_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_God_EditValueChanging - ����� ������������ ����� ���������� ��������� ����
			//-------------------------------------------------------------------------------------
			if (bTag)
			{
				p_CloseAllEditors();//�������� ��������� ������ ��� ���������� ���������� ���������
				e.Cancel = RdaFiltr.f_ControlFilterChanging(false, e.NewValue, bE_God, null, bView, bUpdate, out bTag);
			}
		}

		// p_bOkEnabled - ��\�������������� ������, �����,... ���\����� ��������� ������� 
		private void p_bOkEnabled(bool bOk)
		{
			//-------------------------------------------------------------------------------------
			// p_bOkEnabled - ����� ������������ ����������� ��� ������������� ������, ����,... 
			//                ���(bOk=false) ��� �����(bOk=true) ��������� ������� 
			// �������� bOk : = true - ����������� ����� ; =false - ������������� ��� ��������� 
			//
			// ���������� � ������: SetOKEnableStatus --> bOk=false  �  bB_Ok_ItemClick --> bOk=true
			//-------------------------------------------------------------------------------------

			#region ��������, ��������� � �������� �� ��������� ���������(� ������� ������� ..._EditValueChanging)
			bB_Ok.Enabled = !bOk;       // ��\�������������� ������ bB_Ok 
			this.bB_Ok.Border = bOk ? DevExpress.XtraEditors.Controls.BorderStyles.Style3D
									: DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			if (!bOk)
			{
				l_Period.Text = "";
				p_NewShabl();     // �������� ������(������ � �����)
			}
			#endregion

			string tCapt = "";
			string tHint = "";
			if (!bOk)
			{
				tCapt = RDA.RDF.sLang == "U" ? "�̲�� �������" : "��������� �������";
				tHint = (RDA.RDF.sLang == "U")
					? "����������� ������ � ����������. ��������� ��. �� !"
					: "������������� ������ � ����������. ������� ��. �� ! ";
			}
			bS_InfoLeft.Caption = tCapt;
			bS_InfoLeft.Hint = tHint;


			bB_ImpExp.Enabled = bOk;  //                            ������-�������
			bB_Save.Enabled = bOk;  //                            �������� 
			bB_Del.Enabled = bOk;  //                            ��������
			xtraTabControl1.Enabled = bOk;  // ��\�������������� xtraTabControl1
		}

		// SetOKEnableStatus - ����� �������������� ����� ��������� � ������� ��� ����������� ��
		private void SetOKEnableStatus()
		{
			//-------------------------------------------------------------------------------------
			// SetOKEnableStatus - ����� ������������ ����� ��������� � ������� ��� ����������� ��
			//                     ���������� � ������� �������� � ������  FilterControl
			//-------------------------------------------------------------------------------------
			if (bB_Ok.Enabled == false)
				p_bOkEnabled(false); // ��a������������� ������, �����,...  ��� ��������� �������
		}

		// bB_Ok_ItemClick - ����� ������������ ��� ������� �� ������ �� ��� ��������� ���������
		private void bB_Ok_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_Ok_temClick - ����� ������������ ��� ������� �� ������ �� ��� ��������� ���������
			//	                ����� ��������� ��������� �������(���a �\��� ������)
			//-------------------------------------------------------------------------------------
			bArh = false; // =false- ������� ���������� ������ � ������
			bSum = false; // =false- ������� ���������� ������ � ������
			bUpdate = false;  // =true - ������� ���������� ������ 	

			sMes = Convert.ToInt32(bS_Mes.EditValue);   // ����� ������ �������
			sGod = Convert.ToInt32(bS_God.EditValue);   // ���   ������ �������
			eMes = Convert.ToInt32(bE_Mes.EditValue);   // ����� �����  �������
			eGod = Convert.ToInt32(bE_God.EditValue);   // ���   �����  �������

			// ������ �������  ��� ��������� ���������
			l_Period.Text = RDA.RDF.f_GetPeriod(sMes, sGod, eMes, eGod);
			if (l_Period.Text == "") return;
			// ������� ����������(=true) ��� ���������(=false) ��� � ������� 
			one_MG = sMes.Equals(eMes) && sGod.Equals(eGod) ? true : false;

			// ������� ������������ ���������: =true-����������, =false-������������ 
			bLockDoc = (one_MG) ? RdaFunc.f_IsPeriodClose(5, eGod, eMes) : false;

			// =true - ������� ����, ��� �������� ��������� �� ��������� ����
			bDateDoc = (one_MG) ? RDA.DocumentLock.isDocumentUsedForDate(myDoc, eGod, eMes) : true;

			// ������� ����������� �������������� ���������: =true - ������ ��������
			bView = (!bEditTabl || !bDateDoc || bLockDoc || !one_MG) ? true : false;

			p_bOkEnabled(true); // �������������� ������, �����,...  ����� �������

			// ��� bView = true  ��������� "���������� � ��"
			dataManager1.ImportFromDB.Visible =  !bView;

			p_NewGrid();   // ��������� �������� � ���� 1 
		}

		#endregion

		#region ������, ��������� � ���������� ������� : ���������, �������, ������, ������, ������� � Excel, �����
		
		//  bB_ImpExp_ItemClick - ����� ��������� ��������-�������
		private void bB_ImpExp_ItemClick(object sender, ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_ImpExp_ItemClick - ����� ������������ ��� ������ ��������� ��������-�������
			//-------------------------------------------------------------------------------------
			//dataManager1.ImportFileName = myDoc.ToString() + "_"  // D0CO19PM - ��� ���������
			//            + eMes.ToString().PadLeft(2, '0')         // MM  - ��������� ����� ������
			//            + eGod.ToString().Substring(2, 2);        // PP  - ��������� ���   ������
			dataManager1.CallManager();
		}
		
		// p_SetCommandBuilder ���������� OracleCommandBuilder ��� ���������� OracleDataAdapter
		private void p_SetCommandBuilder(OracleDataAdapter oraDA)
		{
			//-------------------------------------------------------------------------------------
			// p_SetCommandBuilder - ����� ������������ ����������  OracleCommandBuilder ��� 
			//                       oraDA(���������� OracleDataAdapter)  � ����������� ��� ���� 
			//                       ���������� ��� ���������� ������ SQL(Update,Delete,Insert)
			//                            
			// ���������� ��� ����������(p_UpdArh) ��� ��������(p_DelArh) ��������� �� Oracle_������
			//-------------------------------------------------------------------------------------
			if (!bView)
			{
				// �������� ������������� ������ SQL(Update,Delete,Insert) ��� ���������� 
				// Oracle_������� tBArh  �� ���������� ����� Doc,God,Mes,Road
				OraCBArh = new OracleCommandBuilder(oraDA);
				oraDA.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				OraCBArh.SetAllValues = true;
			}
		}

		// bB_Save_ItemClick - ������� ��� ������� �� ������ "��������" ��� ���������� ��������� � Oracle_�������
		private void bB_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_Save_ItemClick - ����� ������������ ��� ������� �� ������ "��������" ��� �����-
			//                     ����� ������ � Oracle_�������- ������ tBArh 
			//-------------------------------------------------------------------------------------
			p_Save(sender);
		}

		// p_Save - ����� ������������ ��� ���������� ������ � Oracle_�������-������ tBArh
		private void p_Save(object sender)
		{
			//-------------------------------------------------------------------------------------
			// p_Save - ����� ������������ ��� ���������� ������ � Oracle_�������- ������ tBArh
			//        ���������� � bB_Save_ItemClick ��� � ������� �������� � ������  FilterControl
			//-------------------------------------------------------------------------------------

			if (!bView)
			{
				p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
				p_SumI();           // ��������� ��������(bSum)- ������� ���� �� ����� ����� <> 0

				string Result = "";
				bool bFlag = true;

				if (bUpdate)  //  ���� ���� ��������� ������
				{
					if (bArh)
					{
						#region ��� ������� ������ � ������ ������������ ������������ ����� ��������(��������� ��� ��� ��������)

						// sender=null - ������� ����, ��� ����� bB_Save_ItemClick ������ � �����(��������,f_ErrN),
						if (sender != null) //  �.�. �� ����� ��������� ������ �� ����������(RdaFunc.f_ErrArh) 
							Result = RdaFunc.f_ErrArh(RDA.RDF.sLang, "SAVE", eGod.ToString(), rI_Mes.GetDisplayText(eMes), bSum);
						else Result = "YES";

						switch (Result)
						{
							case "YES":
								if (bSum) p_UpdArh();      // ������� �������� � ������
								else p_DelArh();  // ������  �������� � ������
								break;
							case "NO":                     //  �E ������ �������� � ������
								break;
							default:                       //  ���������� ��������� ��������
								bFlag = false;
								break;
						}
						#endregion
					}
					else // ��� ���������� ������ � ������
					{
						if (bSum) p_UpdArh();   // �������� �������� � ������
						else bFlag = false;     // �� ��������� ������� �������
					}
				}
				if (bFlag) bB_Ok_ItemClick(null, null);
			}
		}

		// bB_Del_ItemClick - ������� ��� ������� �� ������ "X" - �������� ��������� 
		private void bB_Del_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_Del_ItemClick - ����� ������������ ��� ������� �� ������ "X" ��� �������� 
			//                    �������� � Oracle_�������-������
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();//�������� ��������� ������ ��� ���������� ���������� ���������
			grid1.Focus();                     // ��������� ����� � ����
			string Result = "";
			if (!bView)
			{
				if (bArh)
				{
					// f_ErrArh - ������� ���������� ��� �������� ������� �� �������� ������ �� ������
					Result = RdaFunc.f_ErrArh(RDA.RDF.sLang, "DEL", eGod.ToString(), rI_Mes.GetDisplayText(eMes), bSum);
					if (Result == "YES")
					{
						p_DelArh(); // ������  �������� � ������
						bB_Ok_ItemClick(null, null);
					}
				}
				else if (bSum)
				{
					p_NewShabl(); // �������  Ds_������� �������-tBShabl � ��������� ������ � �����
					p_Enabled(false);    // ������������� ������
					bUpdate = false;     // =true - ������� ��������� ������
				}
			}

		}

		//bB_Exit_ItemClick -  �������e �����
		private void bB_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//bB_Exit_ItemClick - ����� ������������ ��� ������� �� ������ "�����" ��� �������� �����
			//-------------------------------------------------------------------------------------

			this.Close();
		}

		// p_UpdArh - ���������� ��������� � ������ ��
		private void p_UpdArh()
		{
			//-------------------------------------------------------------------------------------
			// p_UpdArh - ����� ������������ ��� ���������� ������ �� Ds-������� tBShabl 
			//		      � Oracle_������� �������� ������ 
			//-------------------------------------------------------------------------------------
			int tRoad = 0;
			foreach (DataRow dr in Ds.Tables[tBShabl].Rows)
			{	// ��� ��� Doc,God,Mes ������ � ������� �������(��. ����� p_NewArh ),
				//   �� ������� ���� ������ �� ���������������� ���� ������ ROAD
				tRoad = Convert.ToInt32(dr["ROAD"]);

				DataRow[] drN = Ds.Tables[tBArh].Select(string.Format("ROAD = {0}", tRoad));
				if (drN.Length > 0)       // ��� ��������� ������ �������:
				{
					#region ���������� ������ �� Ds-������� � Ds-������

					drN[0]["USERID"] = uId;            //  ID-��� ������������
					drN[0]["LASTDATE"] = DateTime.Now;  //   � ������� ���� ���������					
					foreach (string myGr in nGr)            // ����� �� ��������
					{
						drN[0][myGr] = RdaFunc.f_Conv(dr[myGr].ToString());//  + ����� �� ����� ������
					}
					#endregion
				}
				else
				{
					#region ���������� � tBArh ������� �� ������� ������ ���������

					DataRow drS     = Ds.Tables[tBArh].NewRow();
					drS["USERID"]   = uId;                 //  ID-��� ������������
					drS["LASTDATE"] = DateTime.Now;        //   � ������� ���� ���������					
					drS["DOC"]      = myDoc;               // ��� ���������
					drS["GOD"]      = eGod;                // �������� ���
					drS["MES"]      = eMes;                //        �����
					drS["ROAD"]     = dr["ROAD"];          // ��� ������
					foreach (string myGr in nGr)                  // ����� �� ��������
					{

						drS[myGr] = RdaFunc.f_Conv(dr[myGr].ToString()); // - ����� �� ��������������� ����� ������
					}
					Ds.Tables[tBArh].Rows.Add(drS);     // �������� ����� ������
					#endregion
				}
			}

			#region ���������� ������ � ������  �� �����: Doc,God,Mes,Road 
            try
            {
				p_SetCommandBuilder(OraDAArh);    // ���������� CommandBuilder ��� ���������� OracleDataAdapter 
				// ������� ������ � ������ �� �����: Doc,God,Mes,Road
                OraDAArh.Update(Ds, tBArh);
            }
            catch { }

			#endregion
		}

		// p_DelArh - �������� ��������� �� ������ ��
		private void p_DelArh()
		{
			//-------------------------------------------------------------------------------------
			// p_DelArh - �������� ������� ��������� �� ��������� ���� � Oracle_������
			//-------------------------------------------------------------------------------------
            try
            {
				foreach(DataRow drN in Ds.Tables[tBArh].Rows)     
				{	
					drN.Delete();                             //     �������� ������ 
				}
				p_SetCommandBuilder(OraDAArh);    // ���������� CommandBuilder ��� ���������� OracleDataAdapter 
				OraDAArh.Update(Ds, tBArh); // ������� ������ � Oracle_������
			}
			catch { }
		}


		#endregion

		#region ������, ��������� � ���������, �����������, ��������� ��������� � ��������� �����

		// p_Update - ����������-������ �������������� ����� ����� 
		private void p_Update(bool bOk)
		{
			//-------------------------------------------------------------------------------------
			// p_Update - ����� ������������ ��� ���������� ��� ������� ������� � ���� ���� �����
			//  �������� bOk : = true - ���������� ; =false - ����������
			//-------------------------------------------------------------------------------------
			if (bView) bOk = false;

			#region ����������� ������� � ����� ������  Grid1 (advBandedGridView1)
			advBandedGridView1.OptionsBehavior.Editable = bOk;
			C0.OptionsColumn.AllowFocus = false;
			C1.OptionsColumn.AllowFocus = false;
			C11.OptionsColumn.AllowFocus = false;
			#endregion

			#region ����������� ������� � ����� ������  Grid2 (advBandedGridView2)
			advBandedGridView2.OptionsBehavior.Editable = bOk;
			C0E.OptionsColumn.AllowFocus = false;
			C1E.OptionsColumn.AllowFocus = false;
			C17.OptionsColumn.AllowFocus = false;
			#endregion

			#region ����������� ������� � ����� ������  Grid3 (advBandedGridView3)
			advBandedGridView3.OptionsBehavior.Editable = bOk;
			C0I.OptionsColumn.AllowFocus = false;
			C1I.OptionsColumn.AllowFocus = false;
			C24.OptionsColumn.AllowFocus = false;
			#endregion

			#region ����������� ������� � ����� ������  Grid4 (advBandedGridView4)
			advBandedGridView4.OptionsBehavior.Editable = bOk;
			C0T.OptionsColumn.AllowFocus = false;
			C1T.OptionsColumn.AllowFocus = false;
			C30.OptionsColumn.AllowFocus = false;
			C35.OptionsColumn.AllowFocus = false;
			C36.OptionsColumn.AllowFocus = false;
			#endregion

			#region ����������� ������� � ����� ������  Grid5 (advBandedGridView9)
			advBandedGridView9.OptionsBehavior.Editable = bOk;
			C0X.OptionsColumn.AllowFocus = false;
			C1X.OptionsColumn.AllowFocus = false;
			C39.OptionsColumn.AllowFocus = false;
			C43.OptionsColumn.AllowFocus = false;
			#endregion
		}

		private void p_Enabled(bool bOk)
		{
			//-------------------------------------------------------------------------------------
			// p_Enabled - ����� ������������ ��������� ��� ����������� ������ 
			//             �������� bOk : = true - ��������� ; =false - �����������
			// ���������� � ������: p_NewGrid(),p_DelShabl(),p_DelArh(),sB_Save_Click(),
			//                                  myView_CellValueChanged()
			//-------------------------------------------------------------------------------------

			if (bView)
			{
				bUpdate = false;
				bB_Save.Enabled = false; // ������������� ������ : "��������" 
				bB_Del.Enabled = false;                //   "��������"
			}
			else
			{
				bB_Save.Enabled = bOk;    //  ����������� ������ : "��������" 
				bB_Del.Enabled = bSum;                //   "��������"
			}
		}

		//  p_NewShabl - ����� ������������ ��� �������  Ds_������� �������
		private void p_NewShabl()
		{
			//-------------------------------------------------------------------------------------		
			//  p_NewShabl - ����� ������������ ��� ������� � ���������� Ds_������ ��������  
			//-------------------------------------------------------------------------------------	

			Ds.Tables[tBShabl].Clear();    // �������  �������_������ � Ds 
			OraDAShabl.Fill(Ds, tBShabl);  // �������� �������_������ � Ds
			this.grid1.DataSource = Ds.Tables[tBShabl]; // ��������� ������ � ����� 1
			this.grid2.DataSource = Ds.Tables[tBShabl]; // ��������� ������ � ����� 2
			this.grid3.DataSource = Ds.Tables[tBShabl]; // ��������� ������ � ����� 3
			this.grid4.DataSource = Ds.Tables[tBShabl]; // ��������� ������ � ����� 4
			this.grid5.DataSource = Ds.Tables[tBShabl]; // ��������� ������ � ����� 5
		}

		// p_DataToShabl - ������ �� Ds_������(tBArh) ��� Excel_��������� � Ds_������(tBShabl) 
		private void p_DataToShabl(DataTable dt)
		{
			//-------------------------------------------------------------------------------------		
			// p_DataToShabl - ����� ������������ ��� ������� ������ �� Ds_������(tBArh) ��� 
			//                 Excel_��������� � ��������������� ������ Ds_�������(tBShabl) 
			//                 �� ����� :    DOC, ROAD, GOD, MES, GRARH
			// 	
			// �������� DataTable dt : Ds.Tables[tBArh]);// ���������� ������ �� Ds_������(tBArh)

			//-------------------------------------------------------------------------------------
			int tRoad = 0;
			foreach (DataRow dr in dt.Rows)
			{	// ��� ��� Doc,God,Mes ������ � ������� �������(��. ����� p_NewArh ),
				//   �� ������� ���� ������ �� ���������������� ���� ������ ROAD
				tRoad = Convert.ToInt32(dr["ROAD"]);

				DataRow[] drN = Ds.Tables[tBShabl].Select(string.Format("ROAD = {0}", tRoad));
				if (drN.Length > 0)       // ��� ��������� ������ �������:
				{
					foreach (string myGr in nGr)            // ����� �� ��������
					{
						mySum = RdaFunc.f_Conv(drN[0][myGr].ToString()); // ����� �� ����� �������
						drN[0][myGr] = mySum + RdaFunc.f_Conv(dr[myGr].ToString());// + ����� �������
					}
				}
			}
		}

		// p_NewArh - �������� ������ �� Oracle_������ � Ds_����� 
		private void p_NewArh()
		{
			//-------------------------------------------------------------------------------------		
			//  p_NewArh - ����� ������������ ��� ������ � ���������� D0000F4 �� ������ tBArh   
			//             ��������� ���� ������ � ������ tBArh : Doc, God, Mes, Road.
			// ���������� � ������: p_NewGrid()
			//-------------------------------------------------------------------------------------		
			int pS = sGod*100 + sMes; // ������ �������
			int pE = eGod*100 + eMes; // ����� �������

			Ds.Tables[tBArh].Clear(); 
			OraDAArh=new OracleDataAdapter(@"SELECT "
					+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser, "
					+ @" t.* FROM "  + tBArh   + " t"
					+ @" WHERE DOC='"+ myDoc   + "'" 
					+ @"   AND (God*100+Mes>=" + pS.ToString()
					+ @"   AND  God*100+Mes<=" + pE.ToString() + ")", RDA.RDF.BDOraCon); 
			OraDAArh.Fill(Ds,tBArh);  	// �������� ������� � Ds


			// ����������� �������� �������� bArh:  = true - ������ ���� � ������
			bArh = Ds.Tables[tBArh].Rows.Count > 0 ? true : false; // =true- ������� ������� ������ � ������
			p_DataToShabl(Ds.Tables[tBArh]); // ���������� ������ �� Ds_������(tBArh) � Ds_������(tBShabl)
			p_SumI();                        // ������ ������ � ��������� ��������(bSum)
		}

		// p_SumI - ������ ������ � ��������� ��������(bSum) ������� ���� �� ����� ����� <> 0
		private void p_SumI()
		{
			//-------------------------------------------------------------------------------------
			// p_SumI - �������� Ds_������� tBShabl �� �������-���������� ���� �� ����� ����� <> 0,
			//          �  ������������ ��������(bSum) ��� ����������� ����������� ����������
			//          (��� bSum=true) ��� ��������(bSum=false) ��������� � ������� ��� ������
			// ���������� � ������: p_NewShabl(),advBandedGridView1_CellValueChanged(),sB_Save_Click
			//-------------------------------------------------------------------------------------
			bSum = false;                       // ��������� ������� ����, ��� ��� ����� = 0			
			foreach (DataRow dr in Ds.Tables[tBShabl].Rows)
			{
				foreach (string myGr in nGr)
				{
					if (dr[myGr].ToString() != "" && Convert.ToDecimal(dr[myGr]) != 0)
					{
						bSum = true; // ������� ���� �� ����� �� ������� �����
						break;
					}
				}
				if (bSum) break;
			}
			if (bSum)
			{
				foreach (DataRow dr in Ds.Tables[tBShabl].Rows)
				{
					// ���� �� ������� Grid1 - ����������������� ����������
					dr["V11"] = RdaFunc.f_Conv(dr["V2"].ToString()) + RdaFunc.f_Conv(dr["V5"].ToString())
						+ RdaFunc.f_Conv(dr["V6"].ToString()) + RdaFunc.f_Conv(dr["V7"].ToString())
						+ RdaFunc.f_Conv(dr["V8"].ToString()) + RdaFunc.f_Conv(dr["V9"].ToString())
						+ RdaFunc.f_Conv(dr["V10"].ToString());
					// ���� �� ������� Grid2 - ̳�������� ���������� �������
					dr["M7"] = RdaFunc.f_Conv(dr["M2"].ToString()) + RdaFunc.f_Conv(dr["M4"].ToString())
						+ RdaFunc.f_Conv(dr["M5"].ToString()) + RdaFunc.f_Conv(dr["M6"].ToString()) + RdaFunc.f_Conv(dr["M6A"].ToString());

					// ���� �� ������� Grid3 - ̳�������� ���������� ������
					dr["M14"] = RdaFunc.f_Conv(dr["M9"].ToString()) + RdaFunc.f_Conv(dr["M11"].ToString())
						+ RdaFunc.f_Conv(dr["M12"].ToString()) + RdaFunc.f_Conv(dr["M13"].ToString()) + RdaFunc.f_Conv(dr["M13A"].ToString());
					// ���� �� ������� Grid4 - ̳�������� ���������� �������+������+������� ����� � ������
					dr["M20"] = RdaFunc.f_Conv(dr["M16"].ToString()) + RdaFunc.f_Conv(dr["M17"].ToString()) //������� �����
						+ RdaFunc.f_Conv(dr["M18"].ToString()) + RdaFunc.f_Conv(dr["M19"].ToString());
					dr["M24"] = RdaFunc.f_Conv(dr["M7"].ToString()) + RdaFunc.f_Conv(dr["M14"].ToString()) //̳�������� ����������
						+ RdaFunc.f_Conv(dr["M20"].ToString()) + RdaFunc.f_Conv(dr["M22"].ToString());

					dr["M25"] = RdaFunc.f_Conv(dr["V11"].ToString()) + RdaFunc.f_Conv(dr["M24"].ToString()) //������
						+ RdaFunc.f_Conv(dr["M8"].ToString()) + RdaFunc.f_Conv(dr["M15"].ToString())
						+ RdaFunc.f_Conv(dr["M8A"].ToString()) + RdaFunc.f_Conv(dr["M15A"].ToString())
					    + RdaFunc.f_Conv(dr["V3"].ToString()); // ���.02.03.2012


					//if (dr["ROAD"].ToString() == "10" || dr["ROAD"].ToString() == "83")                 // ���.10.02.2012
					//    dr["M25"] = RdaFunc.f_Conv(dr["M25"].ToString()) + RdaFunc.f_Conv(dr["V3"].ToString()); 


					// ���� �� ������� Grid5 - �� ������� �����Ʋ� : 
					dr["X4"] = RdaFunc.f_Conv(dr["X2"].ToString()) + RdaFunc.f_Conv(dr["X3"].ToString()); // ����������������� ����������
					dr["X8"] = RdaFunc.f_Conv(dr["X4"].ToString()) + RdaFunc.f_Conv(dr["X5"].ToString())  // �����������������+��������� 
						+ RdaFunc.f_Conv(dr["X6"].ToString()) + RdaFunc.f_Conv(dr["X7"].ToString());
				}
			}
		}	

		// p_NewGrid - ������� � ����������� ��������� � �����
		private void p_NewGrid()
		{
			//-------------------------------------------------------------------------------------		
			// p_NewGrid - ����� ������������ ��� ������� � ����������� ��������� � �����
			//-------------------------------------------------------------------------------------
			p_NewShabl(); // �������  Ds_������� �������-tBShabl � ��������� ������ � �����
			p_NewArh();    // ������� �������� �� ���� �� ������   =true-�������� ����

			p_Update(!bView);    // ��������\�������� ��������� ������
			p_Enabled(false);    // ������������� ������
			bUpdate = false;     // =true - ������� ��������� ������
			p_LanghInfo();       // ������ ��������� � ��������� � ����������� �� ���������� �����

		}

		// xtraTabControl1_SelectedPageChanged - ����� ������������ ����� �������� �� �����  ��������(��������):
		private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// xtraTabControl1_SelectedPageChanged - ����� ������������ ����� �������� �� ����� 
			//	                    ��������(��������):
			//		- ��� ��������� �������� ����� ���������
			//	    - ��� ���������� ������������ �������: advBandedGridView*_CellValueChanged(...),
			//	      advBandedGridView*_CellValueChanging(...),advBandedGridView*_ShownEditor(...)
			//        �� ��������� ��������������� ������� � ������ ������ � ...View. � ���� �����
			//	      ������������ myView, ���������� �������� ��������� ������ ...advBandedGridView
			//
			//-------------------------------------------------------------------------------------
            if (cdss != null)
                cdss.ResetColumnSelection();


			if (e.Page == Page5)
				lb_Shapka.Text = tPage5;
			else lb_Shapka.Text = tPage14;

			myPage = e.Page.Name.ToString(); // �������� ������: Page1...Page5

			// ������� ���������(myGrid) ������ ... GridControl ��� ��������� ������� � ��� ��������� 
			DevExpress.XtraGrid.GridControl myGrid = new DevExpress.XtraGrid.GridControl();

			foreach (System.Windows.Forms.Control dr in e.Page.Controls) // ���� �� �������� �������� ��������
			{
				// ���������� ���� ... Control � ���� ...GridControl
				myGrid = (DevExpress.XtraGrid.GridControl)dr;             // ��� ����������  ���� � ���������������
				// ���������� ���� ... BaseView � ���� ...AdvBandedGridView
				myView = (DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)myGrid.MainView; // ��� ... View*
			}

		}

		private void FRMCO19MN_FormClosing(object sender, FormClosingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//FRMCO19MN_FormClosing-����� ������������ ��� ������� �� ������ ����� "�" ��� 
			//           ���������� �������� ����� ��������� �����. ����������� ��������� �� 
			//           ���������� ���������, �.� ���� �� ��������� sB_Save_Click("�������� ����") 
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();//�������� ��������� ������ ��� ���������� ���������� ���������
			e.Cancel = RdaFiltr.f_ControlFilterChanging(true, 0, bE_God, null, bView, bUpdate, out bTag);
		}


		#endregion

		#region ������, ��������� � �������� ��������� c ������a UZC1 �� VR_CO19M@RD_MDVU

		// dataManager1_OnDBImport - �������� ������� ������ �� �� ��� ������������ ��������� 
		private void dataManager1_OnDBImport(RDA.ManagerElementsEventArgs args)
		{
			p_Import();
		}

		//  p_Import - ������� ������(� ���.) �� ������� ����(����� ��)-VR_CO19M(����� MDVU ������a UZC1)
		private void p_Import()
		{
			//-------------------------------------------------------------------------------------		
			//  p_Import - ����� ������������ ��� ������� ������(� ���.) ��� ��������� D0CO19V  ��
			//			   ������� ����(����� ��)-VR_CO19M(����� MDVU ������a UZC1) � ������ tBShabl. 
			//             �� ������� UZB2(��� ����� RODUZ ���� VD) ������� ����������(DATABASE LINK)
			//			   � ������ RD_MDVU.
			//		       ��������� � ������� ������� :  VR_CO19M@RD_MDVU
			//             ��������� ���� ������ � VR_CO19M : God, Mes, Road.
			//  ���������� � ������: p_NewGrid()
			//-------------------------------------------------------------------------------------		
			// ������� ������� � Ds � �������� OracleDataAdapter ��� ������ ������� SELECT
			p_NewShabl();  // ������� �������� �� ���� �� ������� tBShabl(bShabl=true-�������� ����)
			Ds.Tables["Ds_Import"].Clear();
			decimal mySum = 0;
			try
			{
			    OraDAImp = new OracleDataAdapter(@"SELECT * FROM VR_CO19M@RD_MDVU" +
			    @" WHERE GOD=" + eGod.ToString() + " AND MES=" + eMes.ToString(), RDA.RDF.BDOraCon);
			    OraDAImp.Fill(Ds, "Ds_Import");  	// �������� ������� � Ds

			    if (Ds.Tables["Ds_Import"].Rows.Count > 0)
			    {
			        #region  ����� ������ �� ������� ������� � ����������� � ���.���. �� 0,1
			        foreach (DataRow dr in Ds.Tables["Ds_Import"].Rows)
			        {	// ��� ��� God,Mes ������ � ������� �������,�� ������� ���� ������ 
			            //   �� ���������������� ���� ������ ROAD
			            foreach (DataRow drN in Ds.Tables[tBShabl].Rows)
			            {
			                if (Convert.ToInt32(drN["ROAD"]) == Convert.ToInt32(dr["ROAD"]))
			                {

			                    drN["USERID"]   = uId;                 //  ID-��� ������������
			                    drN["LASTDATE"] = DateTime.Now;        //   � ������� ���� ���������					
			                    drN["GOD"]      = eGod;                // �������� ���
			                    drN["MES"]      = eMes;                //        �����

			                    foreach (string myGr in nGr)            // ����� �� ��������
			                    {
			                        mySum = Math.Round(RdaFunc.f_Conv(dr[myGr].ToString()) / 100000) ;
									if (mySum != 0)
			                            drN[myGr] = mySum ;
			                    }
			                    break;
			                }
			            }
			        }
			        #endregion

			        #region ���������� ����� God � Mes, ���� ��� ������ �� �����-���� ������(Road
			        foreach (DataRow drN in Ds.Tables[tBShabl].Rows)
			        {
			            if (Convert.ToInt32(drN["GOD"]) == 0 || Convert.ToInt32(drN["MES"]) == 0)
			            {
			                drN["GOD"] = eGod;      // �������� ���
			                drN["MES"] = eMes;      //        �����
			                drN["USERID"]   = uId;                 //  ID-��� ������������
			                drN["LASTDATE"] = DateTime.Now;        //   � ������� ���� ���������					
			            }
			        }
			        #endregion
					bS_InfoLeft.Caption = RDA.RDF.sLang == "U" ? "��� i��������� � ������i �� UZC1 "
															   : "������ ������������� �� ������� �� UZC1";
					bS_InfoLeft.Hint = "";

					p_SumI();                         // ��������� ����� �� �������  
					if (bSum)
					{
						bUpdate = true;         // ��������� ������� ���������� ������
						p_Enabled(bUpdate);     // ������������-��������������  ������ "��������" � "��������"

					}

				}
				else
				{
					ERR.Error err6 = new ERR.Error("�������", ERR.ErrorImages.Information, RdaFunc.f_Repl(" ", 15) +
									"³����� ��� � ������� �������.", null, 1);
					err6.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				ERR.Error err6 = new ERR.Error("�������", ERR.ErrorImages.CryticalError, RdaFunc.f_Repl(" ", 10) +
								@"ͳ �`������� � ����� ������������ ������.", ex.ToString(), 1);
				err6.ShowDialog();
			}
		}

		#endregion	

		#region ������, ��������� � ���������� ������� ��� �������������� �������� ����� �����

		//������� � ����������� ������� � ������ ������ 
		protected override void OnShown(EventArgs e)
		{
			//-------------------------------------------------------------------------------------		
			// OnShown - ����������������(override) ����� ������������� ��� ����������� ����� �� 
			//		     ������  ������������ ������� � ����������� ������� � ������ ������ 
			//-------------------------------------------------------------------------------------

			base.OnShown(e); // ����� ������ ��  �������� ������ 
			//myView.FocusedRowHandle = 1;// ������� ������ � ������ ������ 
			//myView.ShowEditor();        // ����� ��������� ������ 
		}

		// �������� ��������� ������ 
		private void p_CloseAllEditors()
		{
			//-------------------------------------------------------------------------------------
			// p_CloseAllEditors - ����� ������������ ��� �������� ��������� ������ ��� ���������� 
			//                     �������� ���������� ���������.
			// ���������� ��� ����������, ��������, ��������� �������, �������� �����, ������
			//-------------------------------------------------------------------------------------
			myView.CloseEditor();
		}

		// �������� ����� ���������� ������ � ������ 
		private void cellValueControl1_EndEditCell(object sender, string value, bool isNewValue)
		{
			//-------------------------------------------------------------------------------------
			// cellValueControl1_EndEditCell - ����� ������������ ��� ������������ ��������
			//                                 (bUpdate,... ) ����� ���������� ������ � ������ 
			//-------------------------------------------------------------------------------------
			if (isNewValue)     // ���� ���������� �������� � ������
			{
				p_SumI();
				if (bUpdate == false)
				{
					p_Enabled(true);   // ������������ ������ 
					bUpdate = true;    // ��������� ������� ���������� ������
				}
			}

		}

		#endregion
				
		#region ������ ��������� �����, ��������� � ������� ���������(�� ������ �  Excel-����)

		private void p_XlsOut(bool bPrintDoc)
		{
			//-------------------------------------------------------------------------------------		
			// p_XlsOut( )- ����� ������������ ������������ �\��� ������ ��������� Excel_���������
			//
			// ��������: bPrintDoc = true-������ ��� ����������� �� ������ Excel_���������
			//
			// ��� ������ ����� �� ������ �������� � Excel_��������� ������� �������:
			//     ����\��������� ��������\����-�������� ������ � ������� �� (��������,$1:$5) 
			//-------------------------------------------------------------------------------------		
			string[]  nGrN;  // ��� ����� �����
			string[]  nExl;  // ��� ������� � Excel

			// ������������ txt ��� �������� Excel_��������� 
			string txt = myDoc + "_" + sMes.ToString().PadLeft(2, '0') + sGod.ToString().Substring(2, 2);
			txt += (one_MG) ? "" : "-" + eMes.ToString().PadLeft(2, '0') + eGod.ToString().Substring(2, 2);
			txt += "~����� ��-19-������� ������ �� ����������� �������";

			using (xls = new Office.Excel("D0CO19MN", txt))
			{
				xls.SelectPage(myDoc);// �������� �������� Excel_���������

				#region �������� ��������� � ������������ ������ C7-I7

				xls.SetValue("C4", l_Period.Text);
				xls.SetValue("B37", l_Period.Text);
				xls.SetValue("C85", l_Period.Text);
				xls.SetValue("B110", l_Period.Text);

				#endregion

				#region ���������� 1- �����Iت ���������� ������ ������ �� advBandedGridView1 �  Excel_�������� 

				nGrN = new string[10] { "V2", "V4", "V5", "V6", "V7", "V8", "V9", "V10", "V11", "V3" }; // ������� � Grid1
				nExl = new string[10] { "C",  "D",  "E",  "F",  "G",  "H",  "I",   "J",  "K",   "L"};  // ������� � Excel

				Office.ExtraExcelTools.ViewToXls(advBandedGridView1, xls, 11, 27, nGrN, nExl);

				#endregion	
			
				#region ���������� 2- ̲�������� ����������(�������) ������ ������ �� advBandedGridView2 �  Excel_�������� 

				nGrN = new string[9] { "M2", "M3", "M4", "M5", "M6", "M6A", "M7", "M8", "M8A" }; // ������� � Grid2
				nExl = new string[9] { "C", "D", "E", "F", "G", "H", "I", "J", "K" };  // ������� � Excel
				Office.ExtraExcelTools.ViewToXls(advBandedGridView2, xls, 43, 59, nGrN, nExl);
	
			
				#endregion			

				#region ���������� 3- ̲�������� ����������(I�����) ������ ������ �� advBandedGridView3 �  Excel_�������� 

				nGrN = new string[9] { "M9", "M10", "M11", "M12", "M13", "M13A", "M14", "M15", "M15A" };// ������� � Grid3
				nExl = new string[9] { "C", "D", "E", "F", "G", "H", "I", "J", "K" };  // ������� � Excel

				Office.ExtraExcelTools.ViewToXls(advBandedGridView3, xls, 66, 82, nGrN, nExl);
	
				#endregion			

				#region ���������� 4- ̲�������� ����������(�������) ������ ������ �� advBandedGridView4 �  Excel_�������� 
			
				nGrN = new string[11] { "M16", "M17", "M18", "M19", "M20", "M21", "M22", "M23", "M23A", "M24", "M25" };// ������� � Grid4
				nExl = new string[11] { "C",   "D",   "E",   "F",   "G",   "H",   "I",   "J",   "K",    "L",   "M" };  // ������� � Excel

				Office.ExtraExcelTools.ViewToXls(advBandedGridView4, xls, 91, 107, nGrN, nExl);

				#endregion

				#region ���������� 5- �� ������� ������I� ������ ������ �� advBandedGridView9 �  Excel_�������� 
				
				nGrN = new string[7]  { "X2", "X3", "X4", "X5", "X6", "X7", "X8" }; // ������� � Grid5
				nExl = new string[7] { "C",   "D",  "E",  "F",  "G",  "H",  "I" };  // ������� � Excel

				Office.ExtraExcelTools.ViewToXls(advBandedGridView9, xls, 116, 132, nGrN, nExl);

				#endregion

				#region ����������� ����� ��������� -> ������:

				if (bPrintDoc)
				{
					xls.Visible = false;
					PRT.printset ps = new PRT.printset(xls); // -������� ����� ����� ��� ������
					ps.ShowDialog();                         //    �������� � ������ �������
				}
				else xls.Visible = true;
				xls.SaveDocument();

				#endregion
			}

		}
			
		private void dataManager1_OnExcelExport(RDA.ManagerElementsEventArgs args)
		{
			//-------------------------------------------------------------------------------------
			// dataManager1_OnExcelExport - ����� ������������ ��� ������ ��������� ��������-������� 
			//		                        ��� ����������� ��������� � ���� EXCEL-�����, c�������-
			//		                        ������ � ������������ � ����� Excel_Output(��. p_XlsOut)
			// ��� ����: Excel_�������� ������������, �� ������������� �� ����������� ���� �������
			//	         ��� ������ ��������. ����� ��� ������ ������� ���������� � Excel 
			//-------------------------------------------------------------------------------------

			p_XlsOut(false);// ���� Excel_�������� �� c����������, �� ������������ � �������...

		}

		private void dataManager1_OnFilePrint(RDA.ManagerElementsEventArgs args)
		{
			//-------------------------------------------------------------------------------------
			// dataManager1_OnFilePrint - ����� ������������ ��� ������ ��������� ��������-�������  
			//	             ��� ������ ���������, ������������ � ����� Excel_Output(��. p_XlsOut).
			//��� ���� : Excel_�������� �� ������������, �� ������������� ����������� ���� �������
			//		     ��� ������ ��������. 
			//-------------------------------------------------------------------------------------
			p_XlsOut(true);  // Excel_�������� �� ����������, �� ���������� ���� ������ ��������
		}
	
		#endregion		

		#region ��������� ������鳿 "�������" �����

		private void FRMCO19MN_KeyDown(object sender, KeyEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// FRMCO19MN_KeyDown - ����� ���������� "�������" ������
			//	 ���  ������������� ������� ������ �� ����� ������� :
			//		                 - ������� ��� �������� KeyPreview = true
			//                       - ���������� ���������� ������� KeyDown � ��������� ��� 
			//	                       ��������������� ������ ���������� "�������" �����
			//-------------------------------------------------------------------------------------

			if (e.Control && e.KeyCode == Keys.S) this.bB_Save_ItemClick(null, null);     //CTRL+S     : ���������
			if (e.Control && e.KeyCode == Keys.Delete) this.bB_Del_ItemClick(null, null); //CTRL+DELETE: �������
			if (e.Control && e.KeyCode == Keys.F) this.bB_ImpExp_ItemClick(null, null);   //CTRL+F     : ����� - �������
			if (e.Alt && e.KeyCode == Keys.F4) this.Close();                              //Alt+F4: �������� ����� 
			if (e.KeyCode == Keys.F5) this.bB_Ok_ItemClick(null, null);                   // F5: ���������(��)
			if (e.Control && e.KeyCode == Keys.D) this.bB_PlotDiagram_ItemClick(null, null); //CTRL+D: ���������

		}

		#endregion

		#region ������� � ������ ��� �������������� ��������� � ���������

		/// <summary> WndProc - �������� ���������
		/// ����� ��������� ���� ���������, ������� ���������� ���������� �� ���.
		/// ������������ ��� ��������� ������ ��������� � ��������� ��� ��������� ����� ���������
		/// </summary>
		/// <param name="m">��������� ���������
		/// m.Msg - ��� ���������
		/// m.HWnd - �����(���, ����������) ����������
		/// m.LParam - ���������������� ����������
		/// m.WParam - ��������� ����������
		/// </param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == RDA.RDF.GM_LANGCHANGE && m.HWnd == this.Handle)
			{
				p_LanghInfo(); // ��������� ����������� ��������� ��� ��������� �����
			}
			base.WndProc(ref m);
		}

		// p_BarInfoLeft - ���������� � ������������ ��������� ��������� 
		private void p_BarInfoLeft()
		{
			//-------------------------------------------------------------------------------------		
			// p_BarInfoLeft - ����� ������������ ��� ���������� � ������������ ��������� ��������� 
			//
			// ���������� � ������: p_LanghInfo, 
			//-------------------------------------------------------------------------------------	

			string tCapt = "����� ";
			string tHint = "";

			#region bView = true - ����������� ���������

			if (!bView)
			{
				tCapt += RDA.RDF.sLang == "U" ? "�����������: " : "��������������: ";
				if (bArh)                             // � ������ ���� ������
				{
					tCapt += (RDA.RDF.sLang == "U") ? " � ���I�I  � ���"
													: " � ������  ���� ������";
					tHint = (RDA.RDF.sLang == "U")
						  ? " ������ ������ ��� ����  ��������� ."
						  : " �������� ������ ��� ���������  ��������� .";
				}
				else                                 // � ������ ��� ������
				{
					tCapt += (RDA.RDF.sLang == "U") ? " � ��ղ²  ���� �����"
													: " � ������  ��� ������";
					tHint = (RDA.RDF.sLang == "U")
						  ? " ��������� �������� ������ �����  !"
						  : " ���������� ��������� ������ ������  !";
				}
			}
			#endregion

			#region bView = false - ��������� ���������
			else
			{
				tCapt += RDA.RDF.sLang == "U" ? "���������: " : "���������: ";
				if (one_MG == false)
				{
					tCapt += (RDA.RDF.sLang == "U"
						? "��� ���� � �i����." : "������ ���� � �������.");

					tHint = (RDA.RDF.sLang == "U")
						? "��������� ������� ���� ������� �� ���� ������."
						: "���������� ���������� ���� ������ � ����� �������.";
				}
				else if (!bDateDoc)
				{
					tCapt = RDA.DocumentLock.GetDocumentUsePeriod(myDoc);
					tHint = (RDA.RDF.sLang == "U")
							? "����������� ��������� ������� ��� ������� ���������. "
							: "������������� ����������� ������� ��� ��������� ���������. ";
				}
				else if (bLockDoc)
				{
					tCapt += RDA.RDF.sLang == "U" ? "�������� ���I��" : "������ ������";

					tHint = (RDA.RDF.sLang == "U")
						? "����������� ��������� ������� ��� ������� ���������. ������� ������� �����."
						: "������������� ����������� ������� ��� ��������� ���������. ����� ������� ������.";
				}
				else if (bEditTabl == false)
				{
					tCapt += (RDA.RDF.sLang == "U")
						? "���������� ������� ��� ���." : "������ ������ ��� ���������.";

					tHint = (RDA.RDF.sLang == "U")
						? "� ��� ���� ������� ��� ��� ��������� . ��������� �� ������������ ��."
						: "� ��� ��� ������� ��� ��������� ��������� . ���������� � �������������� ��.";
				}
			}
			#endregion

			bS_InfoLeft.Caption = tCapt;
			bS_InfoLeft.Hint = tHint;

		}

		// p_BarInfo - ���������� �� ��������� ��������� 
		private void p_BarInfo()
		{
			//-------------------------------------------------------------------------------------		
			// p_BarInfo - ����� ������������ ��� ���������� �� ��������� ���������. 
			//             ���������� ����������:
			//              - RdaFunc.f_DocChangeInfo(DocLock.LockTable) �� ������� CloseDoc ������
			//                ��� ��� ����������, ������� ����� ���� ������������� ��� ���� ����
			//                USERID(ID-��� ������������), LASTDATE(���� ���������) � 	IP_USER				
			//                (IP_�� � �������� ���������� ����������) �� ������������  �
			//                �������_������ ���������
			//
			//              - RdaFunc.f_DocChangeInfo(Ds.Tables[tBArh]) �� ������� tBArh 
			//                ��� ��� ����������, ������� �� ����������  ��� ���� USERID
			//                (ID-��� ������������), LASTDATE(���� ���������) � IP_USER				
			//                (IP_�� � �������� ���������� ����������) ������������ � 
			//                �������_������ ���������

			//
			// ���������� � ������: p_LanghInfo, p_NewArh
			//-------------------------------------------------------------------------------------		
			bS_Info.Caption = "";
			bS_Info.Hint = "";
			if (one_MG && bArh)
			{
				//bS_Info.Caption = bArh ? RdaFunc.f_DocChangeInfo(DocLock.LockTable)
				bS_Info.Caption = bArh ? RdaFunc.f_DocChangeInfo(Ds.Tables[tBArh])
								: (RDA.RDF.sLang == "U") ? "������ ����:" : "��������� ���������:";
				bS_Info.Hint = (RDA.RDF.sLang == "U")
						? "  ���� �������� ����, IP-������ �� �� ϲ� �����������, �� ��� ����"
						: "  ���� ���������� ���������, IP-����� �� � ��� ������������, �������� ���������";
			}

		}

		// p_LanghInfo - ��������� � ������� � ����� �� ����� 
		private void p_LanghInfo()
		{
			//-------------------------------------------------------------------------------------
			// p_LanghInfo - ����� ������������ ��� ������ ��������� � ���������  � ����������� 
			//                 �� ���������� ����� : RDA.RDF.sLang = U-����������, =R-�������
			//-------------------------------------------------------------------------------------
			try
			{
				#region ���������(ToolTip) �� ���������� �����

				if (RDA.RDF.sLang == "U")   // ����� ����������� �����
				{
					bB_Save.Caption = " �������� ";
					bB_Save.Hint = "CTRL+S :  �������� �������� � �����";

					bB_ImpExp.Caption = " ������-������� ��������� ";
					bB_ImpExp.Hint = "CTRL+F : ������������ � ���� Excel, �����������, ����������� � �������� Oracle_������� ��";

					bB_Del.Caption = " �������� ";
					bB_Del.Hint = "CTRL+DELETE :  �������� �������� �� ������ �� �������� ������";

					bB_PlotDiagram.Caption = "  ĳ������    ";
					bB_PlotDiagram.Hint = "CTRL+D : ���������� ���������� ������� �����";				
					
					bB_Exit.Caption = " ����� ";
					bB_Exit.Hint = "Alt+F4 :  �������� �����";

					// ������
					bS_Mes.Hint = "    ������� ����� ������� ������� ������";
					bS_God.Hint = "    ������� ��  ������� ������� ������";
					bE_Mes.Hint = "    ������� ����� ��������� ������� ������";
					bE_God.Hint = "    ������� �� ��������� ������� ������";
					bB_Ok.Hint = "F5 :  ����������� ������ �� ������� ��� �� �����";

				}
				#endregion

				#region ���������(ToolTip) �� ������� �����

				else
				{
					bB_Save.Caption = " ��������� ";
					bB_Save.Hint = "CTRL+S :  ��������� �������� � ������";

					bB_ImpExp.Caption = " ������-������� ��������� ";
					bB_ImpExp.Hint = "CTRL+F : �������������� � ���� Excel, �����������, ������������ �� �������� Oracle_������ ��";

					bB_Del.Caption = " ������� ";
					bB_Del.Hint = "CTRL+DELETE :  ������� �������� �� ������ � �������� ������";

					bB_PlotDiagram.Caption = "  ���������    ";
					bB_PlotDiagram.Hint = "CTRL+D : ������������ ������������� ��������� ������";				

					bB_Exit.Caption = " ����� ";
					bB_Exit.Hint = "Alt+F4 :  ������� �����";

					// ������
					bS_Mes.Hint = "    �������� ����� ������ ��������� �������";
					bS_God.Hint = "    �������� ��� ������ ��������� ������� ";
					bE_Mes.Hint = "    �������� ����� ��������� ��������� �������";
					bE_God.Hint = "    �������� ��� ���������  ��������� ������� ";
					bB_Ok.Hint = "F5 :  ��������� ������ � ������� ������ �� ����";

				}
				#endregion

				p_BarInfo();     // ���������� � ��������� ��������� ��������� 
				p_BarInfoLeft(); // ���������� �� ������ ������ � ���������� 
			}

			catch { }

		}
	
		#endregion

		#region ������� � ������ ��� ���������� �������� 

		ChartDataSourceSelector cdss = null;

		// bB_PlotDiagram_ItemClick - ����� ������������ ��� ������� �� ������ ���������
		private void bB_PlotDiagram_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (ChartDataSourceSelector.CanCreateForm())
			{
				cdss = new ChartDataSourceSelector();
				cdss.Owner = this;
				cdss.FormClosed += new FormClosedEventHandler(cdss_FormClosed);
				cdss.Show();
			}

		}

        /// <summary>
        /// ����� ��������������� ��������� � "����������� ���"
        /// </summary>
        /// <param name="sEntryTitle"></param>
        /// <returns></returns>
        private string PrepareTitle(string sEntryTitle)
        {
            string sOutTitle = sEntryTitle;
            sEntryTitle = Regex.Replace(sEntryTitle, @"(?<=\S)(-(\s*)?)", "");
            sEntryTitle = Regex.Replace(sEntryTitle, @"\s+", " ");

            return sEntryTitle;
        }

        /// <summary>
        /// ���������� ������� �������� ����� �������� ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cdss_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cdss.AllowCreateDiagram())
                CreateDiagram();

            cdss = null;
        }

        /// <summary>
        /// ����� ��������� ������ ��� ���������� ���������
        /// </summary>
        private void CreateDiagram()
        {
            ChartManager chmgr = ChartManager.GetHandler();

            SetChartSettings();
            SetChartData("POSITION >= 10 AND POSITION <= 60");
            SetGroupSumSettings();

            chmgr.ShowChart();
        }

        /// <summary>
        /// ���������� ������ � ��������� �� ��������� ������������ � ����������� �� �����������
        /// </summary>
        private void SetGroupSumSettings()
        {
            if (!cdss.GroupSum)       
                SetChartData("POSITION > 60");
            else
                ChartManager.GetHandler().SetData("���� ����������", Convert.ToDecimal(Ds.Tables[tBShabl].Compute(string.Format("SUM({0})", cdss.ColumnField), "POSITION > 60")));   
        }

        /// <summary>
        /// ����� ������������� ��������� ���������
        /// </summary>
        private void SetChartSettings()
        {
            ChartManager chmgr = ChartManager.GetHandler();

            chmgr.PrepareData();
            chmgr.TitleDoc = string.Format("{0}\n{1}", lb_Shapka.Text, l_Period.Text);
            chmgr.TitleGraph = string.Format("{0}\n����� � {1}\n{2}", xtraTabControl1.SelectedTabPage.Text.Trim(), cdss.ColumnNumber, PrepareTitle(cdss.ColumnCaption));
            chmgr.ShifrDoc = bI_Forma.Caption.Trim();
            chmgr.Currency = l_Grn.Text;
        }

        /// <summary>
        /// ����� �� ������� �������� ������ �� ��������� � ��������� ������ ��� ���������
        /// </summary>
        /// <param name="sRowFilter"></param>
        private void SetChartData(string sRowFilter)
        { 
            DataRow[] drSum = Ds.Tables[tBShabl].Select(sRowFilter);
            foreach (DataRow dr in drSum)
            {
               ChartManager.GetHandler().SetData(dr["NAIM"].ToString(), Convert.ToDecimal(dr[cdss.ColumnField]));
            }
        }

        private void advBandedGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (cdss != null && e.Button == MouseButtons.Left)
            {
                BandedGridHitInfo bgHI = (sender as BandedGridView).CalcHitInfo(new Point(e.X, e.Y));

                if (bgHI.HitTest == BandedGridHitTest.Column)
                {
                    // ���� ��������� ������� - ��� �����
                    if (Ds.Tables[tBShabl].Columns[bgHI.Column.FieldName].DataType == typeof(double))
                    {
                        cdss.ColumnField = bgHI.Column.FieldName;
                        cdss.ColumnNumber = bgHI.Column.Caption.Trim();
                        cdss.ColumnCaption = bgHI.Column.OwnerBand.Caption.Trim();
                    }
                    // ����� ���������� ��������� � �� ��������� ���������
                    else
                    {
                        cdss.ResetColumnSelection();
                    }     
                } 
            }
		}

		#endregion

	}
}
