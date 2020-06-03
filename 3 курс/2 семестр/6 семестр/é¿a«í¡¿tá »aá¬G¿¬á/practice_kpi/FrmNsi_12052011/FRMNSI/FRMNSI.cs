//----------------  AC  RODUZ NF - "...�������� ������ �� ���������... "  -------------
//
// ����� FRMNSI- �������  �Ĳ   ������������� ��� ������ ��������������� � 
//                  � ������������ ������� ������������ �� �������: 
//                  GRUSER    - �Ĳ ���� �������� ��  ������������(��������)
//                  NSIROAD   - �Ĳ ���� ��������
//                  NSIOTDEL  - �Ĳ ����� �� ��������
//                  R_ROADRESP- �Ĳ ������������ ��� ����� �� ��������
//
//   ò�� ��-���� ��    ��-�: ��������� �.�. �-�: 5-09-86                  ��� 2011 �
//----------------------------------------------------------------------------------------

using System;                   //��������� ������������ ���� System
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;     //  ---  ��� Form
using System.Data;              // ������� ������������ ���� ADO.NET
//    using System.Data.OracleClient; // ������ System.Data.OracleClient ���������
using CoreLab.Oracle;           //  OraDirect ��� ����� � ����� Oracle
using System.Data.OleDb;        // ������������ ���� ��� ���������� � OLE DB-����������� ������(������ �� EXCEL-����)
using System.Text;
using System.IO;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;





namespace FRMNSI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FRMNSI : DevExpress.XtraEditors.XtraForm // System.Windows.Forms.Form
	{
		#region Declare SYSTEM     - ��������� �������
	
		private System.ComponentModel.IContainer components;
		// ������� DataSet o�����, � �� ��-������, ��� ������ ������ � ��������� ����� ����
		private System.Data.DataSet Ds;

		// ������� OracleConnection - ���������� ������� � ����� ������ Oracle
		//private OracleConnection  OraCon;
		// ������� ������� OracleDataAdapter ��� : 
		//     1. ����������  Ds_������ �������  �� Oracle_������ 
		//     2. ��������� ������ Oracle_������ �� Ds_������
		
		private OracleDataAdapter OraDABuffer;  // "Ds_Buffer"-��������� ������� ��� ������� ������ �� Oracle_������
		private OracleDataAdapter OraDANsi;     // ��� ������� � "Ds_Nsi"    �� Oracle_������� ��� 

		// ������� ������� OracleCommandBuilder ��� ������ Ds_�������, ����� ���������� 
		// ������������� ������ SQL(Update,Delete,Insert) ��� ���������� Oracle_������.
		// ��� ���� ���������� ���������� 2-� ������� :
		//    1. ������ Ds_�������  ������ ��������������� ������ ���� Oracle_������� � �����
		//	 	    ����������� : OraCBArh = new OracleCommandBuilder(OraDAArh);
		//    2. ��� Oracle_������� ������ ����� ��������� ���� � �����
		//	  	    ����������� : OraDAArh.MissingSchemaAction = MissingSchemaAction.AddWithKey;
		// ����	������� �� ��������� , ��  ��� ������ �� ������ SQL(Update,Delete,Insert)
		// ������� ������� ������� OracleCommand, �������� :
		//			private OracleCommand OraIns; - ���������� ������ ...
		private OracleCommandBuilder OraCBNsi;     // ��� ���������� Oracle_�������  ���   �� ����� ��  �� "Ds_Nsi"     

		#endregion

		#region Declare FORMS      - ���� � ������� ����� ...
		//    ��� ������ 
	
		#endregion
	
		#region Declare Variables  - �������������� ����������
		private char   cAcKey  ;       // ���������� �������� ���� ������� : cAcc=R-������\W-���������\F-������
		private string myAccess = "";  // ��������������� ������ ��� ������ ��������� � �� `�����` 
		// = ALL  - ������ �� ���� �������� � ������������� :
		//		     AcUser.Road==NsiRoad.Road== -1 && GrUser.Gr==NsiRoad.Pd== 4 -->  ��� ��������������-������������  
		//		     AcUser.Road==NsiRoad.Road==  5 && AcUser.AdmUser==1         -->  ��� �������������� ���
		// = ADMIN- ������ � �������� � ������������� ������������ ����������� : -->  ��� �������������� �����������
		//		     AcUser.Road==NsiRoad.Road > -1 && GrUser.Gr==NsiRoad.Pd < 5 && AcUser.AdmUser==1 
		// = USER - ������ ������������ � �������� ������������ ����������� :
		//		     AcUser.Road==NsiRoad.Road > -1 && GrUser.Gr==NsiRoad.Pd < 5 && AcUser.AdmUser==0 
		// = FALSE- ������ ��� ������ � �� ������ ���  GrUser.Gr > 5 || AcUser.LockUser==1
		//
		//  ���� myAccess != FALSE, �� ����� ��������� ���� �������� ���� ������� : cAcKey = R-������,=F-������ 
		//               
		//private string sLang    = "U"; // ���� ��� ������ ���������: U-����������, R-�������
		private long   uId      = 1  ; // ���������� �������� UserID-ID_������������ �������
		private string uName    = " "; // ���������� �������� UserName-���_������������ �������

		private string ModiRow  = "";   // ������� ����������� �������� � InfoUpdSrv ���������� :
                                         //  = "" - ��� ��������� ������ �������(��� rModi: I,U,D)  
										 //  = "A" -� ����������� ������� (��� rModi: A)
		                                 //  = "*" -�������� ����������� �� ������� ����� 
		private string TransRoad= "";   // �������, ����������� ������� �� ������ �������� ��������� �� ��� ������:
									 	 //  ="N" - ���(No) - ����������� ��������� ������,
										 //  ="Y" - ��(Yes) - ����������� ��� �������� ������ ������ ������ �� ������� ��. 
		//private string SQLRow   = "";    // ���������� ��� ������������ ������  SQL - Insert\Update\Delete

		private string myErr    = "";    // ��������(ROAD,OTDEL) ������������ ��� ����� ������ p_ErrRead(...)
		private	string ToolGr   = "";    // ToolTip ��� ������ � �� �������� ��� �������� ����� � ������� PD ...View1
		private string ToolAC   = "";    // ToolTip ��� ID_ �� � �� �������� ��� �������� ����� � ������� LIST_AC ...View1
	
		private int    idxNSI   = 0;     // ������ ��������� ������ � ������ ���
		private int    myRoad   = 0;     // ��������� �������� ���� ���������� ������ 
		private bool   bRoad    = false; // ������� ���������� ������ � NsiRoad
		private int    idxRoad = 0;      // ������ ��������� ������ � ��� ����� 

		private int    myOtdel  = 0 ;    // ��������� �������� ���� ����������� ������ �� ������ 
		private bool   bOtdel   = false; // ������� ���������� ������ � NsiOtdel
		private int    idxOtdel = 0;     // ������ ��������� ������ � ��� ������� 

		private	string myKey    = "";    // ���������� ��� �������� ���������� �������� ����� ������
		private bool   bUpdBuff = false; // ������� ���������� ���������� � Ds_Buffer 
		private bool   bNsiErr  = false; // ������� ���������� ������ � ������ Ds_Buffer 
		private bool   bCtrlUpd = false; // ������� ���������� �������� �� ��������� ������ � Ds_Buffer 

		private int    rKey     = 0 ;    // ������� ��������� ����� ��� �������� ������ :=0-���������, =1-���������,
		private int    tRow     = -1;    // ����� ��������� ������ � grid2(...View9)
		private object[] RowObj     ; 
		private	string RowOld   = "";    // ���������� ��� �������� �������     �������� ��������� ������
		private	string RowNew   = "";    // ���������� ��� �������� ����������� �������� ��������� ������

		
		private bool   bEditTabl= false; // ������� ������� �� �������������� �������
		private	string myTabl   = "";    // ������� ��� �������������� ������� 
		private	string OldTabl  = "";    // ������  ��� ������������   ������� 

		
		// ������� ���������(myView) ������ ... AdvBandedGridView ��� ��������� ������� � ��������� ����
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView myView= new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
		
	
		
		private RDA.RDF RdaFunc=new RDA.RDF();// ���������� ������ ����������  RDA, ���������� ����� :

		#endregion
	
		#region Declare BandedGrid - �����, ������, ����,...

		private DevExpress.Utils.ToolTipController toolTipController1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraGrid.GridControl grid1;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView3;
		//private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit5;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView2;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2B1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2B1_1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2B1_2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v2C2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2B2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2B2_1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v2C3;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2B2_2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v2C4;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B1_1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3C1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B1_2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3C2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B1_3;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3C3;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B2_1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3C4;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B2_2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3C5;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B1_1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B1_2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B1_3;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C3;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B3;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C5;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView4;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C3;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C4;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C6;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C7;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C5;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B1_1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B1_2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B1_3;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B1_4;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B3;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B3_1;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4B3_2;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_L6;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_L15;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_L30;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_L80;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_127;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit9;
		private DevExpress.XtraEditors.LabelControl g1_l4;
		private DevExpress.XtraEditors.LabelControl g1_l1;
		private DevExpress.XtraEditors.LabelControl g1_l2;
		private DevExpress.XtraEditors.LabelControl g1_l3;
		private DevExpress.XtraEditors.LabelControl g1_l5;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3B3;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4C11;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
		private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
		private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rItC_PZ;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rItC_PD;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3CM;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1CM;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1BM;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v2CM;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v2BM;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v3BM;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v3CR;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4BR;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4CR;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4BO;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4CO;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v4BM;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v4CM;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_aV4_Position;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C4;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B5;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B5_1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C7;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B5_2;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C8;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C6;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B4;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
		internal DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit12;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1AC;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand v1B_AC;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit rICBE_AC;
		private DevExpress.XtraEditors.LabelControl l_rKey1;
		private DevExpress.XtraEditors.LabelControl l_rKey2;
		private DevExpress.XtraEditors.LabelControl l_rKey3;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rItE_Int;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn v1C_NAIMF;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
		private DevExpress.XtraBars.BarDockControl barDockControl1;
		private DevExpress.XtraBars.BarDockControl barDockControl2;
		private DevExpress.XtraBars.BarDockControl barDockControl3;
		private DevExpress.XtraBars.BarDockControl barDockControl4;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarEditItem bE_Otdel;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rI_Otdel;
		private DevExpress.XtraBars.BarEditItem bE_Road;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rI_Road;
		private DevExpress.XtraBars.BarButtonItem bB_Ok;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarButtonItem bB_Save;
		private DevExpress.XtraBars.BarButtonItem bB_DelRow;
		private DevExpress.XtraBars.BarButtonItem bB_Print;
		private DevExpress.XtraBars.BarButtonItem bB_Exit;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.BarStaticItem bS_InfoLeft;
		private DevExpress.XtraBars.BarStaticItem bS_Info;
		private DevExpress.XtraBars.BarStaticItem bS_Formula;
		private DevExpress.XtraBars.BarEditItem bE_NDI;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rI_NDI;
		public ImageList imageListResources;
		private DevExpress.XtraBars.BarButtonItem bB_AddRow;
		private DevExpress.XtraBars.BarButtonItem bB_CancelEditRow;
		private RDA.CellValueControl cellValueControl1;
		private BandedGridColumn v2C1;
		private BarEditItem bE_LeftFont;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rIt_LeftFont;
		private DevExpress.XtraBars.BarStaticItem bSI_Row;
		#endregion

		public FRMNSI(OracleConnection OC, long UserID, string UserName, char cAcc,int iMes,int iGod)
		{
			InitializeComponent();

			//OraCon = OC;      // ������� � ���� ORACLE-������� ��� ������������ DLL_����
			//if(OraCon.State != System.Data.ConnectionState.Open)
			//    OraCon.Open();
			cAcKey = cAcc;    // ��� ������� � ������� (cAccess �� ������� R_UserObj)
			uId    = UserID;  // ID  ������������ �������(IdUser �� ������� AcUser)			
			uName  = UserName;// ��� ������������ �������(Naim   �� ������� AcUser)
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode4 = new DevExpress.XtraGrid.GridLevelNode();
			DevExpress.XtraGrid.GridLevelNode gridLevelNode5 = new DevExpress.XtraGrid.GridLevelNode();
			DevExpress.XtraGrid.GridLevelNode gridLevelNode6 = new DevExpress.XtraGrid.GridLevelNode();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMNSI));
			RDA.ViewContent viewContent5 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent7 = new RDA.RepositoryContent();
			RDA.RepositoryContent repositoryContent8 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent6 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent9 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent7 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent10 = new RDA.RepositoryContent();
			RDA.RepositoryContent repositoryContent11 = new RDA.RepositoryContent();
			RDA.ViewContent viewContent8 = new RDA.ViewContent();
			RDA.RepositoryContent repositoryContent12 = new RDA.RepositoryContent();
			this.advBandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.v2B1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2B1_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2C1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItE_Int = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.v2B1_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2C2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItE_L30 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.v2B2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2B2_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2C3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v2B2_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2C4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v2BM = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v2CM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.grid1 = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.v3B1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3B1_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3C1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItE_L6 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.v3B1_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3C2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v3B1_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3C3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItE_L80 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.v3B2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3B2_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3C4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v3B2_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3C5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3B3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3CR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v3BM = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v3CM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.advBandedGridView4 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.v4B1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4B1_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v4B1_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItE_127 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.v4B1_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItE_L15 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.v4B1_4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItC_PZ = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.v4B2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v4B3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4B3_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v4B3_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v4BR = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4CR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v4BO = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4CO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v4BM = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4CM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v4C11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.v1B1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1B1_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1B1_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1B1_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1B2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1B3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rItC_PD = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.v1B_AC = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1AC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.rICBE_AC = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
			this.v1B4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C_NAIMF = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1B5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1B5_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1B5_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1C8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.v1BM = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.v1CM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemTextEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.rItE_aV4_Position = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit12 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
			this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.g1_l5 = new DevExpress.XtraEditors.LabelControl();
			this.g1_l3 = new DevExpress.XtraEditors.LabelControl();
			this.g1_l2 = new DevExpress.XtraEditors.LabelControl();
			this.g1_l4 = new DevExpress.XtraEditors.LabelControl();
			this.g1_l1 = new DevExpress.XtraEditors.LabelControl();
			this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
			this.printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
			this.gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.l_rKey2 = new DevExpress.XtraEditors.LabelControl();
			this.l_rKey1 = new DevExpress.XtraEditors.LabelControl();
			this.l_rKey3 = new DevExpress.XtraEditors.LabelControl();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bE_NDI = new DevExpress.XtraBars.BarEditItem();
			this.rI_NDI = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.bE_Road = new DevExpress.XtraBars.BarEditItem();
			this.rI_Road = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.bE_Otdel = new DevExpress.XtraBars.BarEditItem();
			this.rI_Otdel = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bB_Save = new DevExpress.XtraBars.BarButtonItem();
			this.bB_Print = new DevExpress.XtraBars.BarButtonItem();
			this.bSI_Row = new DevExpress.XtraBars.BarStaticItem();
			this.bB_AddRow = new DevExpress.XtraBars.BarButtonItem();
			this.bB_CancelEditRow = new DevExpress.XtraBars.BarButtonItem();
			this.bB_DelRow = new DevExpress.XtraBars.BarButtonItem();
			this.bB_Exit = new DevExpress.XtraBars.BarButtonItem();
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bS_InfoLeft = new DevExpress.XtraBars.BarStaticItem();
			this.bE_LeftFont = new DevExpress.XtraBars.BarEditItem();
			this.rIt_LeftFont = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.bS_Info = new DevExpress.XtraBars.BarStaticItem();
			this.imageListResources = new System.Windows.Forms.ImageList(this.components);
			this.bB_Ok = new DevExpress.XtraBars.BarButtonItem();
			this.bS_Formula = new DevExpress.XtraBars.BarStaticItem();
			this.cellValueControl1 = new RDA.CellValueControl(this.components);
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_Int)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L30)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L80)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_127)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L15)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItC_PZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItC_PD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rICBE_AC)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_aV4_Position)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_NDI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_Road)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_Otdel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rIt_LeftFont)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cellValueControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// advBandedGridView2
			// 
			this.advBandedGridView2.Appearance.GroupPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.advBandedGridView2.Appearance.GroupPanel.ForeColor = System.Drawing.Color.BlueViolet;
			this.advBandedGridView2.Appearance.GroupPanel.Options.UseFont = true;
			this.advBandedGridView2.Appearance.GroupPanel.Options.UseForeColor = true;
			this.advBandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v2B1,
            this.v2B2,
            this.v2BM});
			this.advBandedGridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.advBandedGridView2.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.v2C1,
            this.v2C2,
            this.v2C3,
            this.v2C4,
            this.v2CM});
			this.advBandedGridView2.GridControl = this.grid1;
			this.advBandedGridView2.GroupPanelText = "�Ĳ ���� ���i����� �� ������������";
			this.advBandedGridView2.Name = "advBandedGridView2";
			this.advBandedGridView2.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView2.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			this.advBandedGridView2.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
			this.advBandedGridView2.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
			this.advBandedGridView2.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView_BeforeLeaveRow);
			// 
			// v2B1
			// 
			this.v2B1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2B1.AppearanceHeader.Options.UseFont = true;
			this.v2B1.AppearanceHeader.Options.UseTextOptions = true;
			this.v2B1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2B1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2B1.Caption = "�����";
			this.v2B1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v2B1_1,
            this.v2B1_2});
			this.v2B1.Name = "v2B1";
			this.v2B1.OptionsBand.AllowHotTrack = false;
			this.v2B1.OptionsBand.AllowMove = false;
			this.v2B1.OptionsBand.AllowPress = false;
			this.v2B1.OptionsBand.AllowSize = false;
			this.v2B1.OptionsBand.ShowInCustomizationForm = false;
			this.v2B1.Width = 539;
			// 
			// v2B1_1
			// 
			this.v2B1_1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2B1_1.AppearanceHeader.Options.UseFont = true;
			this.v2B1_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v2B1_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2B1_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2B1_1.Caption = "���";
			this.v2B1_1.Columns.Add(this.v2C1);
			this.v2B1_1.Name = "v2B1_1";
			this.v2B1_1.OptionsBand.AllowHotTrack = false;
			this.v2B1_1.OptionsBand.AllowMove = false;
			this.v2B1_1.OptionsBand.AllowPress = false;
			this.v2B1_1.OptionsBand.AllowSize = false;
			this.v2B1_1.OptionsBand.ShowInCustomizationForm = false;
			this.v2B1_1.ToolTip = "��� �����";
			this.v2B1_1.Width = 54;
			// 
			// v2C1
			// 
			this.v2C1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C1.AppearanceCell.Options.UseFont = true;
			this.v2C1.AppearanceCell.Options.UseTextOptions = true;
			this.v2C1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2C1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C1.AppearanceHeader.Options.UseFont = true;
			this.v2C1.AppearanceHeader.Options.UseTextOptions = true;
			this.v2C1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2C1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C1.Caption = "1";
			this.v2C1.ColumnEdit = this.rItE_Int;
			this.v2C1.FieldName = "GR";
			this.v2C1.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.v2C1.Name = "v2C1";
			this.v2C1.OptionsColumn.AllowSize = false;
			this.v2C1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v2C1.OptionsFilter.AllowAutoFilter = false;
			this.v2C1.OptionsFilter.AllowFilter = false;
			this.v2C1.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v2C1.ToolTip = "��� �����";
			this.v2C1.Visible = true;
			this.v2C1.Width = 54;
			// 
			// rItE_Int
			// 
			this.rItE_Int.AutoHeight = false;
			this.rItE_Int.DisplayFormat.FormatString = "{0:f0}";
			this.rItE_Int.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItE_Int.EditFormat.FormatString = "{0:f0}";
			this.rItE_Int.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItE_Int.Name = "rItE_Int";
			// 
			// v2B1_2
			// 
			this.v2B1_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2B1_2.AppearanceHeader.Options.UseFont = true;
			this.v2B1_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v2B1_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2B1_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2B1_2.Caption = "�����";
			this.v2B1_2.Columns.Add(this.v2C2);
			this.v2B1_2.Name = "v2B1_2";
			this.v2B1_2.OptionsBand.AllowHotTrack = false;
			this.v2B1_2.OptionsBand.AllowMove = false;
			this.v2B1_2.OptionsBand.AllowPress = false;
			this.v2B1_2.OptionsBand.AllowSize = false;
			this.v2B1_2.OptionsBand.ShowInCustomizationForm = false;
			this.v2B1_2.ToolTip = "����� �����";
			this.v2B1_2.Width = 485;
			// 
			// v2C2
			// 
			this.v2C2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C2.AppearanceCell.Options.UseFont = true;
			this.v2C2.AppearanceCell.Options.UseTextOptions = true;
			this.v2C2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C2.AppearanceHeader.Options.UseFont = true;
			this.v2C2.AppearanceHeader.Options.UseTextOptions = true;
			this.v2C2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2C2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C2.Caption = "2";
			this.v2C2.ColumnEdit = this.rItE_L30;
			this.v2C2.FieldName = "NAIM";
			this.v2C2.Name = "v2C2";
			this.v2C2.OptionsColumn.AllowSize = false;
			this.v2C2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v2C2.OptionsFilter.AllowAutoFilter = false;
			this.v2C2.OptionsFilter.AllowFilter = false;
			this.v2C2.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v2C2.ToolTip = "����� �����";
			this.v2C2.Visible = true;
			this.v2C2.Width = 485;
			// 
			// rItE_L30
			// 
			this.rItE_L30.AutoHeight = false;
			this.rItE_L30.MaxLength = 30;
			this.rItE_L30.Name = "rItE_L30";
			// 
			// v2B2
			// 
			this.v2B2.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2B2.AppearanceHeader.Options.UseFont = true;
			this.v2B2.AppearanceHeader.Options.UseTextOptions = true;
			this.v2B2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2B2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2B2.Caption = "������   �����������   ������";
			this.v2B2.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v2B2_1,
            this.v2B2_2});
			this.v2B2.Name = "v2B2";
			this.v2B2.OptionsBand.AllowHotTrack = false;
			this.v2B2.OptionsBand.AllowMove = false;
			this.v2B2.OptionsBand.AllowPress = false;
			this.v2B2.OptionsBand.AllowSize = false;
			this.v2B2.OptionsBand.ShowInCustomizationForm = false;
			this.v2B2.Width = 437;
			// 
			// v2B2_1
			// 
			this.v2B2_1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2B2_1.AppearanceHeader.Options.UseFont = true;
			this.v2B2_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v2B2_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2B2_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2B2_1.Caption = "����";
			this.v2B2_1.Columns.Add(this.v2C3);
			this.v2B2_1.Name = "v2B2_1";
			this.v2B2_1.OptionsBand.AllowHotTrack = false;
			this.v2B2_1.OptionsBand.AllowMove = false;
			this.v2B2_1.OptionsBand.AllowPress = false;
			this.v2B2_1.OptionsBand.AllowSize = false;
			this.v2B2_1.OptionsBand.ShowInCustomizationForm = false;
			this.v2B2_1.Width = 111;
			// 
			// v2C3
			// 
			this.v2C3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C3.AppearanceCell.Options.UseFont = true;
			this.v2C3.AppearanceCell.Options.UseTextOptions = true;
			this.v2C3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2C3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C3.AppearanceHeader.Options.UseFont = true;
			this.v2C3.AppearanceHeader.Options.UseTextOptions = true;
			this.v2C3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2C3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C3.Caption = "3";
			this.v2C3.DisplayFormat.FormatString = "d";
			this.v2C3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.v2C3.FieldName = "LASTDATE";
			this.v2C3.Name = "v2C3";
			this.v2C3.OptionsColumn.AllowEdit = false;
			this.v2C3.OptionsColumn.AllowFocus = false;
			this.v2C3.OptionsColumn.AllowSize = false;
			this.v2C3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v2C3.OptionsColumn.ReadOnly = true;
			this.v2C3.OptionsFilter.AllowAutoFilter = false;
			this.v2C3.OptionsFilter.AllowFilter = false;
			this.v2C3.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v2C3.Visible = true;
			this.v2C3.Width = 111;
			// 
			// v2B2_2
			// 
			this.v2B2_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v2B2_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2B2_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2B2_2.Caption = "����������, ��� ��� ����";
			this.v2B2_2.Columns.Add(this.v2C4);
			this.v2B2_2.Name = "v2B2_2";
			this.v2B2_2.OptionsBand.AllowSize = false;
			this.v2B2_2.Width = 326;
			// 
			// v2C4
			// 
			this.v2C4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C4.AppearanceCell.Options.UseFont = true;
			this.v2C4.AppearanceCell.Options.UseTextOptions = true;
			this.v2C4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v2C4.AppearanceHeader.Options.UseFont = true;
			this.v2C4.AppearanceHeader.Options.UseTextOptions = true;
			this.v2C4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v2C4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v2C4.Caption = "4";
			this.v2C4.FieldName = "NUSER";
			this.v2C4.Name = "v2C4";
			this.v2C4.OptionsColumn.AllowEdit = false;
			this.v2C4.OptionsColumn.AllowFocus = false;
			this.v2C4.OptionsColumn.AllowSize = false;
			this.v2C4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v2C4.OptionsColumn.ReadOnly = true;
			this.v2C4.OptionsFilter.AllowAutoFilter = false;
			this.v2C4.OptionsFilter.AllowFilter = false;
			this.v2C4.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v2C4.Visible = true;
			this.v2C4.Width = 326;
			// 
			// v2BM
			// 
			this.v2BM.Caption = "rModi";
			this.v2BM.Columns.Add(this.v2CM);
			this.v2BM.Name = "v2BM";
			this.v2BM.Visible = false;
			this.v2BM.Width = 75;
			// 
			// v2CM
			// 
			this.v2CM.Caption = "5-rModi";
			this.v2CM.FieldName = "RMODI";
			this.v2CM.Name = "v2CM";
			this.v2CM.Visible = true;
			// 
			// grid1
			// 
			this.grid1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			gridLevelNode4.LevelTemplate = this.advBandedGridView2;
			gridLevelNode4.RelationName = "Level1";
			gridLevelNode5.LevelTemplate = this.advBandedGridView3;
			gridLevelNode5.RelationName = "Level2";
			gridLevelNode6.LevelTemplate = this.advBandedGridView4;
			gridLevelNode6.RelationName = "Level3";
			this.grid1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode4,
            gridLevelNode5,
            gridLevelNode6});
			this.grid1.Location = new System.Drawing.Point(3, 77);
			this.grid1.MainView = this.advBandedGridView1;
			this.grid1.Name = "grid1";
			this.grid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rItE_L30,
            this.rItE_L80,
            this.rItE_127,
            this.repositoryItemTextEdit9,
            this.rItC_PZ,
            this.rItC_PD,
            this.rItE_aV4_Position,
            this.rItE_L15,
            this.rItE_L6,
            this.repositoryItemTextEdit12,
            this.rICBE_AC,
            this.rItE_Int,
            this.repositoryItemTextEdit3});
			this.grid1.Size = new System.Drawing.Size(1018, 502);
			this.grid1.TabIndex = 2;
			this.grid1.ToolTipController = this.toolTipController1;
			this.grid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView3,
            this.advBandedGridView4,
            this.advBandedGridView1,
            this.advBandedGridView2});
			// 
			// advBandedGridView3
			// 
			this.advBandedGridView3.Appearance.GroupPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.advBandedGridView3.Appearance.GroupPanel.ForeColor = System.Drawing.Color.BlueViolet;
			this.advBandedGridView3.Appearance.GroupPanel.Options.UseFont = true;
			this.advBandedGridView3.Appearance.GroupPanel.Options.UseForeColor = true;
			this.advBandedGridView3.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v3B1,
            this.v3B2,
            this.gridBand16,
            this.v3B3,
            this.v3BM});
			this.advBandedGridView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.advBandedGridView3.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.v3C1,
            this.v3C2,
            this.v3C3,
            this.v3C4,
            this.v3C5,
            this.v3CR,
            this.v3CM});
			this.advBandedGridView3.GridControl = this.grid1;
			this.advBandedGridView3.GroupPanelText = " �Ĳ  ����� ��";
			this.advBandedGridView3.Name = "advBandedGridView3";
			this.advBandedGridView3.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView3.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView3.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			this.advBandedGridView3.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
			this.advBandedGridView3.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
			this.advBandedGridView3.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView_BeforeLeaveRow);
			// 
			// v3B1
			// 
			this.v3B1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v3B1.AppearanceHeader.Options.UseFont = true;
			this.v3B1.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B1.Caption = "³����";
			this.v3B1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v3B1_1,
            this.v3B1_2,
            this.v3B1_3});
			this.v3B1.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.v3B1.Name = "v3B1";
			this.v3B1.OptionsBand.AllowHotTrack = false;
			this.v3B1.OptionsBand.AllowMove = false;
			this.v3B1.OptionsBand.AllowPress = false;
			this.v3B1.OptionsBand.AllowSize = false;
			this.v3B1.OptionsBand.ShowInCustomizationForm = false;
			this.v3B1.Width = 682;
			// 
			// v3B1_1
			// 
			this.v3B1_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B1_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B1_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B1_1.Caption = "����";
			this.v3B1_1.Columns.Add(this.v3C1);
			this.v3B1_1.Name = "v3B1_1";
			this.v3B1_1.OptionsBand.AllowHotTrack = false;
			this.v3B1_1.OptionsBand.AllowMove = false;
			this.v3B1_1.OptionsBand.AllowPress = false;
			this.v3B1_1.OptionsBand.AllowSize = false;
			this.v3B1_1.OptionsBand.ShowInCustomizationForm = false;
			this.v3B1_1.Width = 61;
			// 
			// v3C1
			// 
			this.v3C1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.v3C1.AppearanceCell.Options.UseFont = true;
			this.v3C1.AppearanceCell.Options.UseTextOptions = true;
			this.v3C1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C1.AppearanceHeader.Options.UseTextOptions = true;
			this.v3C1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C1.Caption = "1";
			this.v3C1.ColumnEdit = this.rItE_L6;
			this.v3C1.FieldName = "KOD";
			this.v3C1.Name = "v3C1";
			this.v3C1.OptionsColumn.AllowSize = false;
			this.v3C1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v3C1.OptionsFilter.AllowAutoFilter = false;
			this.v3C1.OptionsFilter.AllowFilter = false;
			this.v3C1.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v3C1.Visible = true;
			this.v3C1.Width = 61;
			// 
			// rItE_L6
			// 
			this.rItE_L6.AutoHeight = false;
			this.rItE_L6.MaxLength = 6;
			this.rItE_L6.Name = "rItE_L6";
			// 
			// v3B1_2
			// 
			this.v3B1_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B1_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B1_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B1_2.Caption = "�����";
			this.v3B1_2.Columns.Add(this.v3C2);
			this.v3B1_2.Name = "v3B1_2";
			this.v3B1_2.OptionsBand.AllowHotTrack = false;
			this.v3B1_2.OptionsBand.AllowMove = false;
			this.v3B1_2.OptionsBand.AllowPress = false;
			this.v3B1_2.OptionsBand.AllowSize = false;
			this.v3B1_2.OptionsBand.ShowInCustomizationForm = false;
			this.v3B1_2.Width = 47;
			// 
			// v3C2
			// 
			this.v3C2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.v3C2.AppearanceCell.Options.UseFont = true;
			this.v3C2.AppearanceCell.Options.UseTextOptions = true;
			this.v3C2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C2.AppearanceHeader.Options.UseTextOptions = true;
			this.v3C2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C2.Caption = "2";
			this.v3C2.ColumnEdit = this.rItE_Int;
			this.v3C2.FieldName = "OTDEL";
			this.v3C2.Name = "v3C2";
			this.v3C2.OptionsColumn.AllowSize = false;
			this.v3C2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v3C2.OptionsFilter.AllowAutoFilter = false;
			this.v3C2.OptionsFilter.AllowFilter = false;
			this.v3C2.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v3C2.Visible = true;
			this.v3C2.Width = 47;
			// 
			// v3B1_3
			// 
			this.v3B1_3.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B1_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B1_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B1_3.Caption = "�����";
			this.v3B1_3.Columns.Add(this.v3C3);
			this.v3B1_3.Name = "v3B1_3";
			this.v3B1_3.OptionsBand.AllowHotTrack = false;
			this.v3B1_3.OptionsBand.AllowMove = false;
			this.v3B1_3.OptionsBand.AllowPress = false;
			this.v3B1_3.OptionsBand.AllowSize = false;
			this.v3B1_3.OptionsBand.ShowInCustomizationForm = false;
			this.v3B1_3.Width = 574;
			// 
			// v3C3
			// 
			this.v3C3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.v3C3.AppearanceCell.Options.UseFont = true;
			this.v3C3.AppearanceCell.Options.UseTextOptions = true;
			this.v3C3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C3.AppearanceHeader.Options.UseTextOptions = true;
			this.v3C3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C3.Caption = "3";
			this.v3C3.ColumnEdit = this.rItE_L80;
			this.v3C3.FieldName = "NAIM";
			this.v3C3.Name = "v3C3";
			this.v3C3.OptionsColumn.AllowSize = false;
			this.v3C3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v3C3.OptionsFilter.AllowAutoFilter = false;
			this.v3C3.OptionsFilter.AllowFilter = false;
			this.v3C3.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v3C3.Visible = true;
			this.v3C3.Width = 574;
			// 
			// rItE_L80
			// 
			this.rItE_L80.AccessibleDescription = "                                                                                 " +
				"                                                                                " +
				"";
			this.rItE_L80.AutoHeight = false;
			this.rItE_L80.MaxLength = 80;
			this.rItE_L80.Name = "rItE_L80";
			// 
			// v3B2
			// 
			this.v3B2.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v3B2.AppearanceHeader.Options.UseFont = true;
			this.v3B2.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B2.Caption = "������   �����������   ������";
			this.v3B2.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v3B2_1,
            this.v3B2_2});
			this.v3B2.Name = "v3B2";
			this.v3B2.OptionsBand.AllowHotTrack = false;
			this.v3B2.OptionsBand.AllowMove = false;
			this.v3B2.OptionsBand.AllowPress = false;
			this.v3B2.OptionsBand.AllowSize = false;
			this.v3B2.OptionsBand.ShowInCustomizationForm = false;
			this.v3B2.Width = 293;
			// 
			// v3B2_1
			// 
			this.v3B2_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B2_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B2_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B2_1.Caption = "����";
			this.v3B2_1.Columns.Add(this.v3C4);
			this.v3B2_1.Name = "v3B2_1";
			this.v3B2_1.OptionsBand.AllowHotTrack = false;
			this.v3B2_1.OptionsBand.AllowMove = false;
			this.v3B2_1.OptionsBand.AllowPress = false;
			this.v3B2_1.OptionsBand.AllowSize = false;
			this.v3B2_1.OptionsBand.ShowInCustomizationForm = false;
			this.v3B2_1.Width = 92;
			// 
			// v3C4
			// 
			this.v3C4.AppearanceCell.Options.UseTextOptions = true;
			this.v3C4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C4.AppearanceHeader.Options.UseTextOptions = true;
			this.v3C4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C4.Caption = "4";
			this.v3C4.DisplayFormat.FormatString = "d";
			this.v3C4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.v3C4.FieldName = "LASTDATE";
			this.v3C4.Name = "v3C4";
			this.v3C4.OptionsColumn.AllowEdit = false;
			this.v3C4.OptionsColumn.AllowFocus = false;
			this.v3C4.OptionsColumn.AllowSize = false;
			this.v3C4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v3C4.OptionsColumn.ReadOnly = true;
			this.v3C4.OptionsFilter.AllowAutoFilter = false;
			this.v3C4.OptionsFilter.AllowFilter = false;
			this.v3C4.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v3C4.Visible = true;
			this.v3C4.Width = 92;
			// 
			// v3B2_2
			// 
			this.v3B2_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v3B2_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3B2_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3B2_2.Caption = "����������, ��� ��� ����";
			this.v3B2_2.Columns.Add(this.v3C5);
			this.v3B2_2.Name = "v3B2_2";
			this.v3B2_2.OptionsBand.AllowHotTrack = false;
			this.v3B2_2.OptionsBand.AllowMove = false;
			this.v3B2_2.OptionsBand.AllowPress = false;
			this.v3B2_2.OptionsBand.AllowSize = false;
			this.v3B2_2.OptionsBand.ShowInCustomizationForm = false;
			this.v3B2_2.Width = 201;
			// 
			// v3C5
			// 
			this.v3C5.AppearanceCell.Options.UseTextOptions = true;
			this.v3C5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C5.AppearanceHeader.Options.UseTextOptions = true;
			this.v3C5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v3C5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v3C5.Caption = "5";
			this.v3C5.FieldName = "NUSER";
			this.v3C5.Name = "v3C5";
			this.v3C5.OptionsColumn.AllowEdit = false;
			this.v3C5.OptionsColumn.AllowFocus = false;
			this.v3C5.OptionsColumn.AllowSize = false;
			this.v3C5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v3C5.OptionsColumn.ReadOnly = true;
			this.v3C5.OptionsFilter.AllowAutoFilter = false;
			this.v3C5.OptionsFilter.AllowFilter = false;
			this.v3C5.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v3C5.Visible = true;
			this.v3C5.Width = 201;
			// 
			// gridBand16
			// 
			this.gridBand16.Caption = "����������";
			this.gridBand16.Name = "gridBand16";
			this.gridBand16.Visible = false;
			this.gridBand16.Width = 75;
			// 
			// v3B3
			// 
			this.v3B3.Caption = "ROAD";
			this.v3B3.Columns.Add(this.v3CR);
			this.v3B3.Name = "v3B3";
			this.v3B3.Visible = false;
			this.v3B3.Width = 75;
			// 
			// v3CR
			// 
			this.v3CR.Caption = "7";
			this.v3CR.FieldName = "ROAD";
			this.v3CR.Name = "v3CR";
			this.v3CR.OptionsColumn.AllowEdit = false;
			this.v3CR.OptionsColumn.AllowFocus = false;
			this.v3CR.OptionsColumn.ReadOnly = true;
			this.v3CR.Visible = true;
			// 
			// v3BM
			// 
			this.v3BM.Caption = "rmodi";
			this.v3BM.Columns.Add(this.v3CM);
			this.v3BM.Name = "v3BM";
			this.v3BM.Visible = false;
			this.v3BM.Width = 75;
			// 
			// v3CM
			// 
			this.v3CM.Caption = "8-rModi";
			this.v3CM.FieldName = "RMODI";
			this.v3CM.Name = "v3CM";
			this.v3CM.Visible = true;
			// 
			// advBandedGridView4
			// 
			this.advBandedGridView4.Appearance.GroupPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView4.Appearance.GroupPanel.ForeColor = System.Drawing.Color.BlueViolet;
			this.advBandedGridView4.Appearance.GroupPanel.Options.UseFont = true;
			this.advBandedGridView4.Appearance.GroupPanel.Options.UseForeColor = true;
			this.advBandedGridView4.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v4B1,
            this.v4B2,
            this.v4B3,
            this.v4BR,
            this.v4BO,
            this.v4BM,
            this.gridBand2});
			this.advBandedGridView4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.advBandedGridView4.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.v4C1,
            this.v4C2,
            this.v4C3,
            this.v4C4,
            this.v4C5,
            this.v4C6,
            this.v4C7,
            this.v4CR,
            this.v4CO,
            this.v4CM,
            this.v4C11});
			this.advBandedGridView4.GridControl = this.grid1;
			this.advBandedGridView4.GroupPanelText = " ������ ������������ ��� �����";
			this.advBandedGridView4.Name = "advBandedGridView4";
			this.advBandedGridView4.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView4.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView4.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			this.advBandedGridView4.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
			this.advBandedGridView4.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
			this.advBandedGridView4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView1_MouseMove);
			this.advBandedGridView4.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView_BeforeLeaveRow);
			// 
			// v4B1
			// 
			this.v4B1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4B1.AppearanceHeader.Options.UseFont = true;
			this.v4B1.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B1.Caption = "³���������� ����";
			this.v4B1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v4B1_1,
            this.v4B1_2,
            this.v4B1_3,
            this.v4B1_4});
			this.v4B1.Name = "v4B1";
			this.v4B1.OptionsBand.AllowSize = false;
			this.v4B1.Width = 940;
			// 
			// v4B1_1
			// 
			this.v4B1_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B1_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B1_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B1_1.Caption = "� � �";
			this.v4B1_1.Columns.Add(this.v4C1);
			this.v4B1_1.Name = "v4B1_1";
			this.v4B1_1.OptionsBand.AllowSize = false;
			this.v4B1_1.Width = 190;
			// 
			// v4C1
			// 
			this.v4C1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C1.AppearanceCell.Options.UseFont = true;
			this.v4C1.AppearanceCell.Options.UseTextOptions = true;
			this.v4C1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C1.AppearanceHeader.Options.UseFont = true;
			this.v4C1.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C1.Caption = "1";
			this.v4C1.ColumnEdit = this.rItE_L30;
			this.v4C1.FieldName = "FIO";
			this.v4C1.Name = "v4C1";
			this.v4C1.OptionsColumn.AllowSize = false;
			this.v4C1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C1.OptionsFilter.AllowAutoFilter = false;
			this.v4C1.OptionsFilter.AllowFilter = false;
			this.v4C1.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C1.Visible = true;
			this.v4C1.Width = 190;
			// 
			// v4B1_2
			// 
			this.v4B1_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B1_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B1_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B1_2.Caption = "������";
			this.v4B1_2.Columns.Add(this.v4C2);
			this.v4B1_2.Name = "v4B1_2";
			this.v4B1_2.OptionsBand.AllowSize = false;
			this.v4B1_2.Width = 620;
			// 
			// v4C2
			// 
			this.v4C2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C2.AppearanceCell.Options.UseFont = true;
			this.v4C2.AppearanceCell.Options.UseTextOptions = true;
			this.v4C2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C2.AppearanceHeader.Options.UseFont = true;
			this.v4C2.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C2.Caption = "2";
			this.v4C2.ColumnEdit = this.rItE_127;
			this.v4C2.FieldName = "POSADA";
			this.v4C2.Name = "v4C2";
			this.v4C2.OptionsColumn.AllowSize = false;
			this.v4C2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C2.OptionsFilter.AllowAutoFilter = false;
			this.v4C2.OptionsFilter.AllowFilter = false;
			this.v4C2.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C2.Visible = true;
			this.v4C2.Width = 620;
			// 
			// rItE_127
			// 
			this.rItE_127.AutoHeight = false;
			this.rItE_127.MaxLength = 127;
			this.rItE_127.Name = "rItE_127";
			// 
			// v4B1_3
			// 
			this.v4B1_3.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B1_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B1_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B1_3.Caption = "�������";
			this.v4B1_3.Columns.Add(this.v4C3);
			this.v4B1_3.Name = "v4B1_3";
			this.v4B1_3.OptionsBand.AllowSize = false;
			this.v4B1_3.Width = 87;
			// 
			// v4C3
			// 
			this.v4C3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C3.AppearanceCell.Options.UseFont = true;
			this.v4C3.AppearanceCell.Options.UseTextOptions = true;
			this.v4C3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C3.AppearanceHeader.Options.UseFont = true;
			this.v4C3.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C3.Caption = "3";
			this.v4C3.ColumnEdit = this.rItE_L15;
			this.v4C3.FieldName = "PHONE";
			this.v4C3.Name = "v4C3";
			this.v4C3.OptionsColumn.AllowSize = false;
			this.v4C3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C3.OptionsFilter.AllowAutoFilter = false;
			this.v4C3.OptionsFilter.AllowFilter = false;
			this.v4C3.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C3.Visible = true;
			this.v4C3.Width = 87;
			// 
			// rItE_L15
			// 
			this.rItE_L15.AutoHeight = false;
			this.rItE_L15.MaxLength = 15;
			this.rItE_L15.Name = "rItE_L15";
			// 
			// v4B1_4
			// 
			this.v4B1_4.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4B1_4.AppearanceHeader.Options.UseFont = true;
			this.v4B1_4.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B1_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B1_4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B1_4.Caption = "������";
			this.v4B1_4.Columns.Add(this.v4C4);
			this.v4B1_4.Name = "v4B1_4";
			this.v4B1_4.OptionsBand.AllowSize = false;
			this.v4B1_4.ToolTip = "0 - ������������� �� ��������, 1- �����������, 2- ���� ���������� ��������";
			this.v4B1_4.Width = 43;
			// 
			// v4C4
			// 
			this.v4C4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C4.AppearanceCell.Options.UseFont = true;
			this.v4C4.AppearanceCell.Options.UseTextOptions = true;
			this.v4C4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C4.AppearanceHeader.Options.UseFont = true;
			this.v4C4.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C4.Caption = "4";
			this.v4C4.ColumnEdit = this.rItC_PZ;
			this.v4C4.DisplayFormat.FormatString = "{0:f0}";
			this.v4C4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.v4C4.FieldName = "PZ";
			this.v4C4.Name = "v4C4";
			this.v4C4.OptionsColumn.AllowSize = false;
			this.v4C4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C4.OptionsFilter.AllowAutoFilter = false;
			this.v4C4.OptionsFilter.AllowFilter = false;
			this.v4C4.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C4.ToolTip = "0- ������������� �� ��������, 1- �����������, 2- ���� ���������� ��������";
			this.v4C4.Visible = true;
			this.v4C4.Width = 43;
			// 
			// rItC_PZ
			// 
			this.rItC_PZ.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.rItC_PZ.Appearance.Options.UseTextOptions = true;
			this.rItC_PZ.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PZ.AppearanceDisabled.Options.UseTextOptions = true;
			this.rItC_PZ.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PZ.AppearanceDropDown.ForeColor = System.Drawing.Color.DarkRed;
			this.rItC_PZ.AppearanceDropDown.Options.UseForeColor = true;
			this.rItC_PZ.AppearanceDropDown.Options.UseTextOptions = true;
			this.rItC_PZ.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PZ.AppearanceFocused.Options.UseTextOptions = true;
			this.rItC_PZ.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PZ.AutoHeight = false;
			this.rItC_PZ.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rItC_PZ.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.rItC_PZ.DisplayFormat.FormatString = "{0:f0}";
			this.rItC_PZ.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItC_PZ.EditFormat.FormatString = "{0:f0}";
			this.rItC_PZ.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItC_PZ.Items.AddRange(new object[] {
            "0- ������������� �� ��������",
            "1- �����������",
            "2- ���� ���������� ��������"});
			this.rItC_PZ.MaxLength = 1;
			this.rItC_PZ.Name = "rItC_PZ";
			this.rItC_PZ.NullText = "0";
			this.rItC_PZ.UseCtrlScroll = false;
			this.rItC_PZ.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rItC_PZ_EditValueChanging);
			// 
			// v4B2
			// 
			this.v4B2.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B2.Caption = "�";
			this.v4B2.Columns.Add(this.v4C5);
			this.v4B2.Name = "v4B2";
			this.v4B2.OptionsBand.AllowSize = false;
			this.v4B2.Width = 39;
			// 
			// v4C5
			// 
			this.v4C5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C5.AppearanceCell.Options.UseFont = true;
			this.v4C5.AppearanceCell.Options.UseTextOptions = true;
			this.v4C5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C5.AppearanceHeader.Options.UseFont = true;
			this.v4C5.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C5.Caption = "5";
			this.v4C5.ColumnEdit = this.rItE_Int;
			this.v4C5.FieldName = "POSITION";
			this.v4C5.Name = "v4C5";
			this.v4C5.OptionsColumn.AllowSize = false;
			this.v4C5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C5.OptionsFilter.AllowAutoFilter = false;
			this.v4C5.OptionsFilter.AllowFilter = false;
			this.v4C5.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C5.Visible = true;
			this.v4C5.Width = 39;
			// 
			// v4B3
			// 
			this.v4B3.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4B3.AppearanceHeader.Options.UseFont = true;
			this.v4B3.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B3.Caption = "������   �����������   ������";
			this.v4B3.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v4B3_1,
            this.v4B3_2});
			this.v4B3.Name = "v4B3";
			this.v4B3.OptionsBand.AllowSize = false;
			this.v4B3.Visible = false;
			this.v4B3.Width = 213;
			// 
			// v4B3_1
			// 
			this.v4B3_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B3_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B3_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B3_1.Caption = "����";
			this.v4B3_1.Columns.Add(this.v4C6);
			this.v4B3_1.Name = "v4B3_1";
			this.v4B3_1.OptionsBand.AllowSize = false;
			this.v4B3_1.Width = 64;
			// 
			// v4C6
			// 
			this.v4C6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C6.AppearanceCell.Options.UseFont = true;
			this.v4C6.AppearanceCell.Options.UseTextOptions = true;
			this.v4C6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C6.AppearanceHeader.Options.UseFont = true;
			this.v4C6.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C6.Caption = "6";
			this.v4C6.DisplayFormat.FormatString = "d";
			this.v4C6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.v4C6.FieldName = "LASTDATE";
			this.v4C6.Name = "v4C6";
			this.v4C6.OptionsColumn.AllowEdit = false;
			this.v4C6.OptionsColumn.AllowFocus = false;
			this.v4C6.OptionsColumn.AllowSize = false;
			this.v4C6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C6.OptionsColumn.ReadOnly = true;
			this.v4C6.OptionsFilter.AllowAutoFilter = false;
			this.v4C6.OptionsFilter.AllowFilter = false;
			this.v4C6.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C6.Visible = true;
			this.v4C6.Width = 64;
			// 
			// v4B3_2
			// 
			this.v4B3_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v4B3_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4B3_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4B3_2.Caption = "����������, ��� ��� ����";
			this.v4B3_2.Columns.Add(this.v4C7);
			this.v4B3_2.Name = "v4B3_2";
			this.v4B3_2.OptionsBand.AllowSize = false;
			this.v4B3_2.OptionsBand.FixedWidth = true;
			this.v4B3_2.Width = 149;
			// 
			// v4C7
			// 
			this.v4C7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C7.AppearanceCell.Options.UseFont = true;
			this.v4C7.AppearanceCell.Options.UseTextOptions = true;
			this.v4C7.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v4C7.AppearanceHeader.Options.UseFont = true;
			this.v4C7.AppearanceHeader.Options.UseTextOptions = true;
			this.v4C7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v4C7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v4C7.Caption = "7";
			this.v4C7.FieldName = "NUSER";
			this.v4C7.Name = "v4C7";
			this.v4C7.OptionsColumn.AllowEdit = false;
			this.v4C7.OptionsColumn.AllowFocus = false;
			this.v4C7.OptionsColumn.AllowSize = false;
			this.v4C7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v4C7.OptionsColumn.FixedWidth = true;
			this.v4C7.OptionsColumn.ReadOnly = true;
			this.v4C7.OptionsFilter.AllowAutoFilter = false;
			this.v4C7.OptionsFilter.AllowFilter = false;
			this.v4C7.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v4C7.Visible = true;
			this.v4C7.Width = 149;
			// 
			// v4BR
			// 
			this.v4BR.Caption = "ROAD";
			this.v4BR.Columns.Add(this.v4CR);
			this.v4BR.Name = "v4BR";
			this.v4BR.Visible = false;
			this.v4BR.Width = 22;
			// 
			// v4CR
			// 
			this.v4CR.Caption = "8-ROAD";
			this.v4CR.FieldName = "ROAD";
			this.v4CR.Name = "v4CR";
			this.v4CR.Visible = true;
			this.v4CR.Width = 22;
			// 
			// v4BO
			// 
			this.v4BO.Caption = "OTDEL";
			this.v4BO.Columns.Add(this.v4CO);
			this.v4BO.Name = "v4BO";
			this.v4BO.Visible = false;
			this.v4BO.Width = 20;
			// 
			// v4CO
			// 
			this.v4CO.Caption = "9-OTDEL";
			this.v4CO.FieldName = "OTDEL";
			this.v4CO.Name = "v4CO";
			this.v4CO.Visible = true;
			this.v4CO.Width = 20;
			// 
			// v4BM
			// 
			this.v4BM.Caption = "rmodi";
			this.v4BM.Columns.Add(this.v4CM);
			this.v4BM.CustomizationCaption = "10";
			this.v4BM.Name = "v4BM";
			this.v4BM.Visible = false;
			this.v4BM.Width = 75;
			// 
			// v4CM
			// 
			this.v4CM.Caption = "10-rModi";
			this.v4CM.FieldName = "RMODI";
			this.v4CM.Name = "v4CM";
			this.v4CM.Visible = true;
			// 
			// gridBand2
			// 
			this.gridBand2.Caption = "rkey";
			this.gridBand2.Columns.Add(this.v4C11);
			this.gridBand2.Name = "gridBand2";
			this.gridBand2.Visible = false;
			this.gridBand2.Width = 75;
			// 
			// v4C11
			// 
			this.v4C11.Caption = "11-rKey";
			this.v4C11.FieldName = "RKEY";
			this.v4C11.Name = "v4C11";
			this.v4C11.Visible = true;
			// 
			// advBandedGridView1
			// 
			this.advBandedGridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.advBandedGridView1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.BlueViolet;
			this.advBandedGridView1.Appearance.GroupPanel.Options.UseFont = true;
			this.advBandedGridView1.Appearance.GroupPanel.Options.UseForeColor = true;
			this.advBandedGridView1.Appearance.GroupPanel.Options.UseTextOptions = true;
			this.advBandedGridView1.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.advBandedGridView1.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v1B1,
            this.v1B2,
            this.v1B3,
            this.v1B_AC,
            this.v1B4,
            this.gridBand1,
            this.v1B5,
            this.v1BM});
			this.advBandedGridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.v1C1,
            this.v1C2,
            this.v1C3,
            this.v1C4,
            this.v1C5,
            this.v1AC,
            this.v1C6,
            this.v1C_NAIMF,
            this.v1C7,
            this.v1C8,
            this.v1CM});
			this.advBandedGridView1.GridControl = this.grid1;
			this.advBandedGridView1.GroupPanelText = "�Ĳ ���i����� ...";
			this.advBandedGridView1.Name = "advBandedGridView1";
			this.advBandedGridView1.OptionsNavigation.AutoFocusNewRow = true;
			this.advBandedGridView1.OptionsNavigation.EnterMoveNextColumn = true;
			this.advBandedGridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
			this.advBandedGridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
			this.advBandedGridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
			this.advBandedGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView1_MouseMove);
			this.advBandedGridView1.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView_BeforeLeaveRow);
			// 
			// v1B1
			// 
			this.v1B1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1B1.AppearanceHeader.Options.UseFont = true;
			this.v1B1.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B1.Caption = "���i����� �� ���� ";
			this.v1B1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v1B1_1,
            this.v1B1_2,
            this.v1B1_3});
			this.v1B1.Name = "v1B1";
			this.v1B1.OptionsBand.AllowHotTrack = false;
			this.v1B1.OptionsBand.AllowMove = false;
			this.v1B1.OptionsBand.AllowPress = false;
			this.v1B1.OptionsBand.AllowSize = false;
			this.v1B1.Width = 399;
			// 
			// v1B1_1
			// 
			this.v1B1_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B1_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B1_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B1_1.Caption = "���";
			this.v1B1_1.Columns.Add(this.v1C1);
			this.v1B1_1.Name = "v1B1_1";
			this.v1B1_1.OptionsBand.AllowHotTrack = false;
			this.v1B1_1.OptionsBand.AllowMove = false;
			this.v1B1_1.OptionsBand.AllowPress = false;
			this.v1B1_1.OptionsBand.AllowSize = false;
			this.v1B1_1.ToolTip = "��� ����������";
			this.v1B1_1.Width = 40;
			// 
			// v1C1
			// 
			this.v1C1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C1.AppearanceCell.Options.UseFont = true;
			this.v1C1.AppearanceCell.Options.UseTextOptions = true;
			this.v1C1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C1.AppearanceHeader.Options.UseFont = true;
			this.v1C1.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C1.Caption = "1";
			this.v1C1.ColumnEdit = this.rItE_Int;
			this.v1C1.FieldName = "ROAD";
			this.v1C1.Name = "v1C1";
			this.v1C1.OptionsColumn.AllowSize = false;
			this.v1C1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C1.OptionsFilter.AllowAutoFilter = false;
			this.v1C1.OptionsFilter.AllowFilter = false;
			this.v1C1.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C1.ToolTip = "��� ����������";
			this.v1C1.Visible = true;
			this.v1C1.Width = 40;
			// 
			// v1B1_2
			// 
			this.v1B1_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1B1_2.AppearanceHeader.Options.UseFont = true;
			this.v1B1_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B1_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B1_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B1_2.Caption = "����� � ��������� ������";
			this.v1B1_2.Columns.Add(this.v1C2);
			this.v1B1_2.Name = "v1B1_2";
			this.v1B1_2.OptionsBand.AllowHotTrack = false;
			this.v1B1_2.OptionsBand.AllowMove = false;
			this.v1B1_2.OptionsBand.AllowPress = false;
			this.v1B1_2.OptionsBand.AllowSize = false;
			this.v1B1_2.Width = 172;
			// 
			// v1C2
			// 
			this.v1C2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C2.AppearanceCell.Options.UseFont = true;
			this.v1C2.AppearanceCell.Options.UseTextOptions = true;
			this.v1C2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C2.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C2.Caption = "2";
			this.v1C2.ColumnEdit = this.rItE_L30;
			this.v1C2.FieldName = "NAIM";
			this.v1C2.Name = "v1C2";
			this.v1C2.OptionsColumn.AllowSize = false;
			this.v1C2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C2.OptionsColumn.FixedWidth = true;
			this.v1C2.OptionsFilter.AllowAutoFilter = false;
			this.v1C2.OptionsFilter.AllowFilter = false;
			this.v1C2.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C2.Visible = true;
			this.v1C2.Width = 172;
			// 
			// v1B1_3
			// 
			this.v1B1_3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1B1_3.AppearanceHeader.Options.UseFont = true;
			this.v1B1_3.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B1_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B1_3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B1_3.Caption = "����� � ���������� ������";
			this.v1B1_3.Columns.Add(this.v1C3);
			this.v1B1_3.Name = "v1B1_3";
			this.v1B1_3.OptionsBand.AllowHotTrack = false;
			this.v1B1_3.OptionsBand.AllowMove = false;
			this.v1B1_3.OptionsBand.AllowPress = false;
			this.v1B1_3.OptionsBand.AllowSize = false;
			this.v1B1_3.Width = 187;
			// 
			// v1C3
			// 
			this.v1C3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C3.AppearanceCell.Options.UseFont = true;
			this.v1C3.AppearanceCell.Options.UseTextOptions = true;
			this.v1C3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C3.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C3.Caption = "3";
			this.v1C3.ColumnEdit = this.rItE_L30;
			this.v1C3.FieldName = "NAIMD";
			this.v1C3.Name = "v1C3";
			this.v1C3.OptionsColumn.AllowSize = false;
			this.v1C3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C3.OptionsColumn.FixedWidth = true;
			this.v1C3.OptionsFilter.AllowAutoFilter = false;
			this.v1C3.OptionsFilter.AllowFilter = false;
			this.v1C3.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C3.Visible = true;
			this.v1C3.Width = 187;
			// 
			// v1B2
			// 
			this.v1B2.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.v1B2.Caption = "IP-�������";
			this.v1B2.Columns.Add(this.v1C4);
			this.v1B2.Name = "v1B2";
			this.v1B2.OptionsBand.AllowHotTrack = false;
			this.v1B2.OptionsBand.AllowMove = false;
			this.v1B2.OptionsBand.AllowPress = false;
			this.v1B2.OptionsBand.AllowSize = false;
			this.v1B2.RowCount = 2;
			this.v1B2.Width = 86;
			// 
			// v1C4
			// 
			this.v1C4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C4.AppearanceCell.Options.UseFont = true;
			this.v1C4.AppearanceCell.Options.UseTextOptions = true;
			this.v1C4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C4.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C4.Caption = "4";
			this.v1C4.ColumnEdit = this.rItE_L15;
			this.v1C4.FieldName = "IP_SERV";
			this.v1C4.Name = "v1C4";
			this.v1C4.OptionsColumn.AllowSize = false;
			this.v1C4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C4.OptionsColumn.FixedWidth = true;
			this.v1C4.OptionsFilter.AllowAutoFilter = false;
			this.v1C4.OptionsFilter.AllowFilter = false;
			this.v1C4.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C4.Visible = true;
			this.v1C4.Width = 86;
			// 
			// v1B3
			// 
			this.v1B3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1B3.AppearanceHeader.Options.UseFont = true;
			this.v1B3.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B3.Caption = "�����";
			this.v1B3.Columns.Add(this.v1C5);
			this.v1B3.Name = "v1B3";
			this.v1B3.OptionsBand.AllowHotTrack = false;
			this.v1B3.OptionsBand.AllowMove = false;
			this.v1B3.OptionsBand.AllowPress = false;
			this.v1B3.OptionsBand.AllowSize = false;
			this.v1B3.Width = 36;
			// 
			// v1C5
			// 
			this.v1C5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C5.AppearanceCell.Options.UseFont = true;
			this.v1C5.AppearanceCell.Options.UseTextOptions = true;
			this.v1C5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C5.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C5.Caption = "5";
			this.v1C5.ColumnEdit = this.rItC_PD;
			this.v1C5.FieldName = "PD";
			this.v1C5.Name = "v1C5";
			this.v1C5.OptionsColumn.AllowSize = false;
			this.v1C5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C5.OptionsColumn.FixedWidth = true;
			this.v1C5.OptionsFilter.AllowAutoFilter = false;
			this.v1C5.OptionsFilter.AllowFilter = false;
			this.v1C5.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C5.ToolTip = "����� �� ��� ���������� ����������";
			this.v1C5.Visible = true;
			this.v1C5.Width = 36;
			// 
			// rItC_PD
			// 
			this.rItC_PD.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.rItC_PD.Appearance.Options.UseTextOptions = true;
			this.rItC_PD.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PD.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
			this.rItC_PD.AppearanceDisabled.Options.UseBackColor = true;
			this.rItC_PD.AppearanceDisabled.Options.UseTextOptions = true;
			this.rItC_PD.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PD.AppearanceDropDown.ForeColor = System.Drawing.Color.DarkRed;
			this.rItC_PD.AppearanceDropDown.Options.UseForeColor = true;
			this.rItC_PD.AppearanceFocused.Options.UseTextOptions = true;
			this.rItC_PD.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rItC_PD.AutoHeight = false;
			this.rItC_PD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rItC_PD.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.rItC_PD.DropDownRows = 20;
			this.rItC_PD.EditFormat.FormatString = "{0:f0}";
			this.rItC_PD.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItC_PD.MaxLength = 2;
			this.rItC_PD.Name = "rItC_PD";
			this.rItC_PD.NullText = "0";
			this.rItC_PD.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rItC_PD_EditValueChanging);
			// 
			// v1B_AC
			// 
			this.v1B_AC.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B_AC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B_AC.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B_AC.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.v1B_AC.Caption = "������  ��";
			this.v1B_AC.Columns.Add(this.v1AC);
			this.v1B_AC.Name = "v1B_AC";
			this.v1B_AC.ToolTip = "������(�� ������� LIST_AC )  ID_�����   � ���� : RD, NF \\ RD \\  NF, ����������� �" +
				"���� �� �������� ������ (�����- ��� ��)";
			this.v1B_AC.Width = 47;
			// 
			// v1AC
			// 
			this.v1AC.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1AC.AppearanceCell.Options.UseFont = true;
			this.v1AC.AppearanceCell.Options.UseTextOptions = true;
			this.v1AC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1AC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1AC.AppearanceHeader.Options.UseTextOptions = true;
			this.v1AC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1AC.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1AC.Caption = "6";
			this.v1AC.ColumnEdit = this.rICBE_AC;
			this.v1AC.FieldName = "LIST_IDAC";
			this.v1AC.Name = "v1AC";
			this.v1AC.OptionsColumn.AllowSize = false;
			this.v1AC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1AC.OptionsColumn.FixedWidth = true;
			this.v1AC.OptionsFilter.AllowAutoFilter = false;
			this.v1AC.OptionsFilter.AllowFilter = false;
			this.v1AC.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1AC.ToolTip = "������(� ������� LIST_AC)  ID_���� � ������: RD,NF ! RD ! NF..., �� �������� ��" +
				"� �� ������� ����������(�����- �� ��)";
			this.v1AC.Visible = true;
			this.v1AC.Width = 47;
			// 
			// rICBE_AC
			// 
			this.rICBE_AC.Appearance.Options.UseTextOptions = true;
			this.rICBE_AC.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rICBE_AC.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
			this.rICBE_AC.AppearanceDisabled.Options.UseBackColor = true;
			this.rICBE_AC.AppearanceDisabled.Options.UseTextOptions = true;
			this.rICBE_AC.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rICBE_AC.AppearanceDropDown.ForeColor = System.Drawing.Color.DarkRed;
			this.rICBE_AC.AppearanceDropDown.Options.UseForeColor = true;
			this.rICBE_AC.AppearanceDropDown.Options.UseTextOptions = true;
			this.rICBE_AC.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rICBE_AC.AppearanceFocused.Options.UseTextOptions = true;
			this.rICBE_AC.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.rICBE_AC.AutoHeight = false;
			this.rICBE_AC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "������(�� ������� LIST_AC )  ID_�����   � ���� : RD, NF \\ RD \\  NF, ����������� �" +
                    "���� �� �������� ������ (�����- ��� ��)")});
			this.rICBE_AC.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.rICBE_AC.Name = "rICBE_AC";
			this.rICBE_AC.ShowAllItemCaption = "��� ��";
			this.rICBE_AC.ShowPopupCloseButton = false;
			// 
			// v1B4
			// 
			this.v1B4.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.v1B4.Caption = "���������  ��� ������ ";
			this.v1B4.Columns.Add(this.v1C6);
			this.v1B4.Name = "v1B4";
			this.v1B4.OptionsBand.AllowHotTrack = false;
			this.v1B4.OptionsBand.AllowMove = false;
			this.v1B4.OptionsBand.AllowPress = false;
			this.v1B4.OptionsBand.AllowSize = false;
			this.v1B4.RowCount = 3;
			this.v1B4.ToolTip = "���������  ��� ������ ��������� ����������� : 005RRRR00000000 (�.� (005*10000+R" +
				"oad)*100000000) ";
			this.v1B4.Width = 77;
			// 
			// v1C6
			// 
			this.v1C6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C6.AppearanceCell.Options.UseFont = true;
			this.v1C6.AppearanceCell.Options.UseTextOptions = true;
			this.v1C6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C6.AppearanceHeader.Options.UseFont = true;
			this.v1C6.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C6.Caption = "7";
			this.v1C6.FieldName = "ROADF";
			this.v1C6.Name = "v1C6";
			this.v1C6.OptionsColumn.AllowEdit = false;
			this.v1C6.OptionsColumn.AllowFocus = false;
			this.v1C6.OptionsColumn.AllowSize = false;
			this.v1C6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C6.OptionsColumn.FixedWidth = true;
			this.v1C6.OptionsColumn.ReadOnly = true;
			this.v1C6.OptionsFilter.AllowAutoFilter = false;
			this.v1C6.OptionsFilter.AllowFilter = false;
			this.v1C6.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C6.ToolTip = "���������  ��� ������ ��������� ����������� : 005RRRR00000000 (�.� (005*10000+R" +
				"oad)*100000000) ";
			this.v1C6.Visible = true;
			this.v1C6.Width = 77;
			// 
			// gridBand1
			// 
			this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridBand1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridBand1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridBand1.Caption = "����� �����";
			this.gridBand1.Columns.Add(this.v1C_NAIMF);
			this.gridBand1.Name = "gridBand1";
			this.gridBand1.Width = 331;
			// 
			// v1C_NAIMF
			// 
			this.v1C_NAIMF.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C_NAIMF.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C_NAIMF.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C_NAIMF.Caption = "8";
			this.v1C_NAIMF.ColumnEdit = this.rItE_127;
			this.v1C_NAIMF.FieldName = "NAIMF";
			this.v1C_NAIMF.Name = "v1C_NAIMF";
			this.v1C_NAIMF.OptionsColumn.AllowSize = false;
			this.v1C_NAIMF.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C_NAIMF.OptionsColumn.FixedWidth = true;
			this.v1C_NAIMF.OptionsFilter.AllowAutoFilter = false;
			this.v1C_NAIMF.OptionsFilter.AllowFilter = false;
			this.v1C_NAIMF.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C_NAIMF.Visible = true;
			this.v1C_NAIMF.Width = 331;
			// 
			// v1B5
			// 
			this.v1B5.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1B5.AppearanceHeader.Options.UseFont = true;
			this.v1B5.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B5.Caption = "������  �����������  ������";
			this.v1B5.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.v1B5_1,
            this.v1B5_2});
			this.v1B5.Name = "v1B5";
			this.v1B5.OptionsBand.AllowHotTrack = false;
			this.v1B5.OptionsBand.AllowMove = false;
			this.v1B5.OptionsBand.AllowPress = false;
			this.v1B5.OptionsBand.AllowSize = false;
			this.v1B5.Visible = false;
			// 
			// v1B5_1
			// 
			this.v1B5_1.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B5_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B5_1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B5_1.Caption = "����";
			this.v1B5_1.Columns.Add(this.v1C7);
			this.v1B5_1.Name = "v1B5_1";
			this.v1B5_1.OptionsBand.AllowHotTrack = false;
			this.v1B5_1.OptionsBand.AllowMove = false;
			this.v1B5_1.OptionsBand.AllowPress = false;
			this.v1B5_1.OptionsBand.AllowSize = false;
			// 
			// v1C7
			// 
			this.v1C7.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C7.AppearanceCell.Options.UseFont = true;
			this.v1C7.AppearanceCell.Options.UseTextOptions = true;
			this.v1C7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C7.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C7.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C7.Caption = "LASTDATE";
			this.v1C7.DisplayFormat.FormatString = "d";
			this.v1C7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.v1C7.FieldName = "LASTDATE";
			this.v1C7.Name = "v1C7";
			this.v1C7.OptionsColumn.AllowEdit = false;
			this.v1C7.OptionsColumn.AllowFocus = false;
			this.v1C7.OptionsColumn.AllowSize = false;
			this.v1C7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C7.OptionsColumn.FixedWidth = true;
			this.v1C7.OptionsColumn.ReadOnly = true;
			this.v1C7.OptionsFilter.AllowAutoFilter = false;
			this.v1C7.OptionsFilter.AllowFilter = false;
			this.v1C7.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C7.Visible = true;
			this.v1C7.Width = 70;
			// 
			// v1B5_2
			// 
			this.v1B5_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1B5_2.AppearanceHeader.Options.UseFont = true;
			this.v1B5_2.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B5_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B5_2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B5_2.Caption = "����������, ��� ��� ����";
			this.v1B5_2.Columns.Add(this.v1C8);
			this.v1B5_2.Name = "v1B5_2";
			this.v1B5_2.OptionsBand.AllowHotTrack = false;
			this.v1B5_2.OptionsBand.AllowMove = false;
			this.v1B5_2.OptionsBand.AllowPress = false;
			this.v1B5_2.OptionsBand.AllowSize = false;
			this.v1B5_2.Visible = false;
			this.v1B5_2.Width = 156;
			// 
			// v1C8
			// 
			this.v1C8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.v1C8.AppearanceCell.Options.UseFont = true;
			this.v1C8.AppearanceCell.Options.UseTextOptions = true;
			this.v1C8.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C8.AppearanceHeader.Options.UseTextOptions = true;
			this.v1C8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1C8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1C8.Caption = "NUSER";
			this.v1C8.FieldName = "NUSER";
			this.v1C8.Name = "v1C8";
			this.v1C8.OptionsColumn.AllowEdit = false;
			this.v1C8.OptionsColumn.AllowFocus = false;
			this.v1C8.OptionsColumn.AllowSize = false;
			this.v1C8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.v1C8.OptionsColumn.FixedWidth = true;
			this.v1C8.OptionsColumn.ReadOnly = true;
			this.v1C8.OptionsFilter.AllowAutoFilter = false;
			this.v1C8.OptionsFilter.AllowFilter = false;
			this.v1C8.OptionsFilter.ImmediateUpdateAutoFilter = false;
			this.v1C8.Visible = true;
			this.v1C8.Width = 156;
			// 
			// v1BM
			// 
			this.v1BM.Caption = "rModi";
			this.v1BM.Columns.Add(this.v1CM);
			this.v1BM.Name = "v1BM";
			this.v1BM.Visible = false;
			this.v1BM.Width = 75;
			// 
			// v1CM
			// 
			this.v1CM.Caption = "10-rModi";
			this.v1CM.FieldName = "RMODI";
			this.v1CM.Name = "v1CM";
			this.v1CM.Visible = true;
			// 
			// repositoryItemTextEdit9
			// 
			this.repositoryItemTextEdit9.AutoHeight = false;
			this.repositoryItemTextEdit9.MaxLength = 25;
			this.repositoryItemTextEdit9.Name = "repositoryItemTextEdit9";
			// 
			// rItE_aV4_Position
			// 
			this.rItE_aV4_Position.AutoHeight = false;
			this.rItE_aV4_Position.DisplayFormat.FormatString = "{0:f0}";
			this.rItE_aV4_Position.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItE_aV4_Position.EditFormat.FormatString = "{0:f0}";
			this.rItE_aV4_Position.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rItE_aV4_Position.MaxLength = 3;
			this.rItE_aV4_Position.Name = "rItE_aV4_Position";
			// 
			// repositoryItemTextEdit12
			// 
			this.repositoryItemTextEdit12.AutoHeight = false;
			this.repositoryItemTextEdit12.MaxLength = 15;
			this.repositoryItemTextEdit12.Name = "repositoryItemTextEdit12";
			// 
			// repositoryItemTextEdit3
			// 
			this.repositoryItemTextEdit3.AutoHeight = false;
			this.repositoryItemTextEdit3.MaxLength = 127;
			this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
			// 
			// toolTipController1
			// 
			this.toolTipController1.Appearance.BackColor = System.Drawing.SystemColors.Info;
			this.toolTipController1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(200)))));
			this.toolTipController1.Appearance.BorderColor = System.Drawing.SystemColors.WindowText;
			this.toolTipController1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolTipController1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.toolTipController1.Appearance.Options.UseBackColor = true;
			this.toolTipController1.Appearance.Options.UseBorderColor = true;
			this.toolTipController1.Appearance.Options.UseFont = true;
			this.toolTipController1.Appearance.Options.UseForeColor = true;
			this.toolTipController1.Appearance.Options.UseTextOptions = true;
			this.toolTipController1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.toolTipController1.AppearanceTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolTipController1.AppearanceTitle.ForeColor = System.Drawing.Color.Black;
			this.toolTipController1.AppearanceTitle.Options.UseFont = true;
			this.toolTipController1.AppearanceTitle.Options.UseForeColor = true;
			this.toolTipController1.AppearanceTitle.Options.UseImage = true;
			this.toolTipController1.AppearanceTitle.Options.UseTextOptions = true;
			this.toolTipController1.AppearanceTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.toolTipController1.Rounded = true;
			this.toolTipController1.RoundRadius = 10;
			this.toolTipController1.ShowBeak = true;
			this.toolTipController1.ToolTipType = DevExpress.Utils.ToolTipType.Standard;
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
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			this.repositoryItemTextEdit1.PasswordChar = '*';
			// 
			// g1_l5
			// 
			this.g1_l5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.g1_l5.Appearance.ForeColor = System.Drawing.Color.Red;
			this.g1_l5.Appearance.Options.UseFont = true;
			this.g1_l5.Appearance.Options.UseForeColor = true;
			this.g1_l5.Appearance.Options.UseTextOptions = true;
			this.g1_l5.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.g1_l5.Location = new System.Drawing.Point(192, 589);
			this.g1_l5.Name = "g1_l5";
			this.g1_l5.Size = new System.Drawing.Size(209, 13);
			this.g1_l5.TabIndex = 188;
			this.g1_l5.Text = "���������  ����\'��������  ���������� !";
			// 
			// g1_l3
			// 
			this.g1_l3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.g1_l3.Appearance.ForeColor = System.Drawing.Color.Red;
			this.g1_l3.Appearance.Options.UseFont = true;
			this.g1_l3.Appearance.Options.UseForeColor = true;
			this.g1_l3.Appearance.Options.UseTextOptions = true;
			this.g1_l3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.g1_l3.Location = new System.Drawing.Point(136, 589);
			this.g1_l3.Name = "g1_l3";
			this.g1_l3.Size = new System.Drawing.Size(20, 13);
			this.g1_l3.TabIndex = 187;
			this.g1_l3.Text = "���";
			// 
			// g1_l2
			// 
			this.g1_l2.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.g1_l2.Appearance.ForeColor = System.Drawing.Color.Black;
			this.g1_l2.Appearance.Options.UseFont = true;
			this.g1_l2.Appearance.Options.UseForeColor = true;
			this.g1_l2.Appearance.Options.UseTextOptions = true;
			this.g1_l2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.g1_l2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
			this.g1_l2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.g1_l2.Location = new System.Drawing.Point(112, 583);
			this.g1_l2.Name = "g1_l2";
			this.g1_l2.Size = new System.Drawing.Size(11, 24);
			this.g1_l2.TabIndex = 186;
			this.g1_l2.Text = "*";
			// 
			// g1_l4
			// 
			this.g1_l4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.g1_l4.Appearance.ForeColor = System.Drawing.Color.Black;
			this.g1_l4.Appearance.Options.UseFont = true;
			this.g1_l4.Appearance.Options.UseForeColor = true;
			this.g1_l4.Appearance.Options.UseTextOptions = true;
			this.g1_l4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.g1_l4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
			this.g1_l4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.g1_l4.Location = new System.Drawing.Point(168, 586);
			this.g1_l4.Name = "g1_l4";
			this.g1_l4.Size = new System.Drawing.Size(13, 19);
			this.g1_l4.TabIndex = 181;
			this.g1_l4.Text = "-2 ";
			// 
			// g1_l1
			// 
			this.g1_l1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.g1_l1.Appearance.ForeColor = System.Drawing.Color.Red;
			this.g1_l1.Appearance.Options.UseFont = true;
			this.g1_l1.Appearance.Options.UseForeColor = true;
			this.g1_l1.Appearance.Options.UseTextOptions = true;
			this.g1_l1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.g1_l1.Location = new System.Drawing.Point(16, 589);
			this.g1_l1.Name = "g1_l1";
			this.g1_l1.Size = new System.Drawing.Size(89, 13);
			this.g1_l1.TabIndex = 180;
			this.g1_l1.Text = "����, �� ������";
			// 
			// printingSystem1
			// 
			this.printingSystem1.Links.AddRange(new object[] {
            this.printableComponentLink1});
			// 
			// printableComponentLink1
			// 
			this.printableComponentLink1.Component = this.grid1;
			this.printableComponentLink1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("printableComponentLink1.ImageStream")));
			this.printableComponentLink1.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
			this.printableComponentLink1.PageHeaderFooter = new DevExpress.XtraPrinting.PageHeaderFooter(new DevExpress.XtraPrinting.PageHeaderArea(new string[] {
                "",
                "",
                "����. [Page #] "}, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))), DevExpress.XtraPrinting.BrickAlignment.Near), new DevExpress.XtraPrinting.PageFooterArea(new string[] {
                "",
                "",
                "[Date Printed]"}, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))), DevExpress.XtraPrinting.BrickAlignment.Near));
			this.printableComponentLink1.PaperKind = System.Drawing.Printing.PaperKind.A4Rotated;
			this.printableComponentLink1.PrintingSystem = this.printingSystem1;
			this.printableComponentLink1.CreateReportHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.printableComponentLink1_CreateReportHeaderArea);
			// 
			// gridBand10
			// 
			this.gridBand10.AppearanceHeader.Options.UseTextOptions = true;
			this.gridBand10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridBand10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridBand10.Caption = "�";
			this.gridBand10.Name = "gridBand10";
			this.gridBand10.OptionsBand.AllowHotTrack = false;
			this.gridBand10.OptionsBand.AllowMove = false;
			this.gridBand10.OptionsBand.AllowPress = false;
			this.gridBand10.OptionsBand.AllowSize = false;
			// 
			// l_rKey2
			// 
			this.l_rKey2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.l_rKey2.Appearance.ForeColor = System.Drawing.Color.Black;
			this.l_rKey2.Appearance.Options.UseFont = true;
			this.l_rKey2.Appearance.Options.UseForeColor = true;
			this.l_rKey2.Appearance.Options.UseTextOptions = true;
			this.l_rKey2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.l_rKey2.Location = new System.Drawing.Point(850, 588);
			this.l_rKey2.Name = "l_rKey2";
			this.l_rKey2.Size = new System.Drawing.Size(55, 15);
			this.l_rKey2.TabIndex = 189;
			this.l_rKey2.Text = "���. ���� ";
			// 
			// l_rKey1
			// 
			this.l_rKey1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.l_rKey1.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.l_rKey1.Appearance.Options.UseFont = true;
			this.l_rKey1.Appearance.Options.UseForeColor = true;
			this.l_rKey1.Appearance.Options.UseTextOptions = true;
			this.l_rKey1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.l_rKey1.Location = new System.Drawing.Point(501, 589);
			this.l_rKey1.Name = "l_rKey1";
			this.l_rKey1.Size = new System.Drawing.Size(344, 13);
			this.l_rKey1.TabIndex = 190;
			this.l_rKey1.Text = "��������� ������� ����, �� ��������������� � ����� ��������. ";
			// 
			// l_rKey3
			// 
			this.l_rKey3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.l_rKey3.Appearance.ForeColor = System.Drawing.Color.Red;
			this.l_rKey3.Appearance.Options.UseFont = true;
			this.l_rKey3.Appearance.Options.UseForeColor = true;
			this.l_rKey3.Appearance.Options.UseTextOptions = true;
			this.l_rKey3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.l_rKey3.Location = new System.Drawing.Point(913, 587);
			this.l_rKey3.Name = "l_rKey3";
			this.l_rKey3.Size = new System.Drawing.Size(32, 16);
			this.l_rKey3.TabIndex = 191;
			this.l_rKey3.Text = "____";
			this.l_rKey3.ToolTip = " ";
			// 
			// barManager
			// 
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3,
            this.bar2});
			this.barManager.DockControls.Add(this.barDockControl1);
			this.barManager.DockControls.Add(this.barDockControl2);
			this.barManager.DockControls.Add(this.barDockControl3);
			this.barManager.DockControls.Add(this.barDockControl4);
			this.barManager.Form = this;
			this.barManager.Images = this.imageListResources;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bB_Save,
            this.bB_DelRow,
            this.bB_Print,
            this.bB_Exit,
            this.bE_Otdel,
            this.bE_Road,
            this.bB_Ok,
            this.bS_Formula,
            this.bS_InfoLeft,
            this.bS_Info,
            this.bE_NDI,
            this.bB_AddRow,
            this.bB_CancelEditRow,
            this.bSI_Row,
            this.bE_LeftFont});
			this.barManager.MaxItemId = 61;
			this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rI_Otdel,
            this.rI_Road,
            this.rI_NDI,
            this.rIt_LeftFont});
			this.barManager.ToolTipController = this.toolTipController1;
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bE_NDI, "", false, true, true, 192, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bE_Road, "", true, true, true, 165, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.bE_Otdel, "", true, true, true, 239, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.bar1.OptionsBar.AllowQuickCustomization = false;
			this.bar1.OptionsBar.DrawDragBorder = false;
			this.bar1.OptionsBar.MultiLine = true;
			this.bar1.OptionsBar.UseWholeRow = true;
			this.bar1.Text = "Tools";
			// 
			// bE_NDI
			// 
			this.bE_NDI.Caption = "  ������ �Ĳ";
			this.bE_NDI.Edit = this.rI_NDI;
			this.bE_NDI.EditValue = 0;
			this.bE_NDI.Id = 55;
			this.bE_NDI.ImageIndex = 8;
			this.bE_NDI.Name = "bE_NDI";
			this.bE_NDI.EditValueChanged += new System.EventHandler(this.bE_NDI_EditValueChanged);
			// 
			// rI_NDI
			// 
			this.rI_NDI.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
			this.rI_NDI.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rI_NDI.Appearance.Options.UseFont = true;
			this.rI_NDI.Appearance.Options.UseForeColor = true;
			this.rI_NDI.AutoHeight = false;
			this.rI_NDI.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rI_NDI.DisplayMember = "NAIM";
			this.rI_NDI.DropDownRows = 4;
			this.rI_NDI.Name = "rI_NDI";
			this.rI_NDI.NullText = "<bE_NDI-> ����� ������ ���>";
			this.rI_NDI.ShowFooter = false;
			this.rI_NDI.ShowHeader = false;
			this.rI_NDI.ValueMember = "ID";
			this.rI_NDI.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_NDI_EditValueChanging);
			// 
			// bE_Road
			// 
			this.bE_Road.Caption = "���i�����";
			this.bE_Road.Edit = this.rI_Road;
			this.bE_Road.EditValue = 0;
			this.bE_Road.Id = 29;
			this.bE_Road.ImageIndex = 26;
			this.bE_Road.Name = "bE_Road";
			this.bE_Road.Tag = "";
			this.bE_Road.EditValueChanged += new System.EventHandler(this.bE_Road_EditValueChanged);
			// 
			// rI_Road
			// 
			this.rI_Road.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rI_Road.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rI_Road.Appearance.Options.UseFont = true;
			this.rI_Road.Appearance.Options.UseForeColor = true;
			this.rI_Road.Appearance.Options.UseTextOptions = true;
			this.rI_Road.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rI_Road.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.rI_Road.AppearanceDropDown.BackColor = System.Drawing.Color.AliceBlue;
			this.rI_Road.AppearanceDropDown.Options.UseBackColor = true;
			this.rI_Road.AutoHeight = false;
			this.rI_Road.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rI_Road.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ROAD", "���", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAIM", "�����", 90),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAIMD", "", 40, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PD", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
			this.rI_Road.DisplayMember = "NAIM";
			this.rI_Road.DropDownRows = 12;
			this.rI_Road.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_Road.Name = "rI_Road";
			this.rI_Road.NullText = "<bE_Road -> ����� ������>";
			this.rI_Road.ShowFooter = false;
			this.rI_Road.ValueMember = "ROAD";
			this.rI_Road.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_Road_EditValueChanging);
			// 
			// bE_Otdel
			// 
			this.bE_Otdel.Caption = "   ³���� ���i�����";
			this.bE_Otdel.Edit = this.rI_Otdel;
			this.bE_Otdel.EditValue = 0;
			this.bE_Otdel.Id = 42;
			this.bE_Otdel.Name = "bE_Otdel";
			this.bE_Otdel.Tag = "";
			this.bE_Otdel.Width = 100;
			this.bE_Otdel.EditValueChanged += new System.EventHandler(this.bE_Otdel_EditValueChanged);
			// 
			// rI_Otdel
			// 
			this.rI_Otdel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rI_Otdel.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.rI_Otdel.Appearance.Options.UseFont = true;
			this.rI_Otdel.Appearance.Options.UseForeColor = true;
			this.rI_Otdel.Appearance.Options.UseTextOptions = true;
			this.rI_Otdel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.rI_Otdel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.rI_Otdel.AutoHeight = false;
			this.rI_Otdel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.rI_Otdel.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OTDEL", "���", 30, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KOD", "����", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAIM", "�����", 160),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ROAD", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
			this.rI_Otdel.DisplayMember = "NAIM";
			this.rI_Otdel.DropDownRows = 10;
			this.rI_Otdel.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_Otdel.Name = "rI_Otdel";
			this.rI_Otdel.NullText = "<bE_Otdel ->����� ������ �������>";
			this.rI_Otdel.ShowFooter = false;
			this.rI_Otdel.ValueMember = "OTDEL";
			this.rI_Otdel.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_Otdel_EditValueChanging);
			// 
			// bar3
			// 
			this.bar3.BarName = "Main menu";
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_Save, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_Print, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bSI_Row, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_AddRow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_CancelEditRow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bB_DelRow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
			// bB_Print
			// 
			this.bB_Print.Caption = " ���� �����  ";
			this.bB_Print.Id = 23;
			this.bB_Print.ImageIndex = 7;
			this.bB_Print.Name = "bB_Print";
			this.bB_Print.Tag = "";
			this.bB_Print.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Print_ItemClick);
			// 
			// bSI_Row
			// 
			this.bSI_Row.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bSI_Row.Caption = "                                     ������� ������     ";
			this.bSI_Row.Id = 58;
			this.bSI_Row.Name = "bSI_Row";
			this.bSI_Row.OwnFont = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bSI_Row.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bSI_Row.UseOwnFont = true;
			// 
			// bB_AddRow
			// 
			this.bB_AddRow.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_AddRow.Caption = "����� �����  ";
			this.bB_AddRow.Id = 56;
			this.bB_AddRow.ImageIndex = 30;
			this.bB_AddRow.Name = "bB_AddRow";
			this.bB_AddRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_AddRow_ItemClick);
			// 
			// bB_CancelEditRow
			// 
			this.bB_CancelEditRow.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_CancelEditRow.Caption = "³������ ��� ";
			this.bB_CancelEditRow.Id = 57;
			this.bB_CancelEditRow.ImageIndex = 29;
			this.bB_CancelEditRow.Name = "bB_CancelEditRow";
			this.bB_CancelEditRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_CancelEditRow_ItemClick);
			// 
			// bB_DelRow
			// 
			this.bB_DelRow.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_DelRow.Caption = " �������� �����";
			this.bB_DelRow.Id = 47;
			this.bB_DelRow.ImageIndex = 22;
			this.bB_DelRow.Name = "bB_DelRow";
			this.bB_DelRow.Tag = "";
			this.bB_DelRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_DelRow_ItemClick);
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
			// bar2
			// 
			this.bar2.Appearance.Options.UseTextOptions = true;
			this.bar2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bar2.BarName = "Custom 4";
			this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bS_InfoLeft),
            new DevExpress.XtraBars.LinkPersistInfo(this.bE_LeftFont),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bS_Info, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
			this.bar2.OptionsBar.AllowQuickCustomization = false;
			this.bar2.OptionsBar.DrawDragBorder = false;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Custom 4";
			// 
			// bS_InfoLeft
			// 
			this.bS_InfoLeft.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			this.bS_InfoLeft.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bS_InfoLeft.Caption = "<bS_InfoLeft>  �������� �����������,...";
			this.bS_InfoLeft.Id = 52;
			this.bS_InfoLeft.Name = "bS_InfoLeft";
			this.bS_InfoLeft.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// bE_LeftFont
			// 
			this.bE_LeftFont.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			this.bE_LeftFont.AutoFillWidth = true;
			this.bE_LeftFont.AutoHideEdit = false;
			this.bE_LeftFont.CanOpenEdit = false;
			this.bE_LeftFont.Edit = this.rIt_LeftFont;
			this.bE_LeftFont.Id = 60;
			this.bE_LeftFont.Name = "bE_LeftFont";
			// 
			// rIt_LeftFont
			// 
			this.rIt_LeftFont.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.rIt_LeftFont.Appearance.Options.UseBackColor = true;
			this.rIt_LeftFont.Appearance.Options.UseFont = true;
			this.rIt_LeftFont.AutoHeight = false;
			this.rIt_LeftFont.Name = "rIt_LeftFont";
			this.rIt_LeftFont.NullText = "<rIt_LeftFont>";
			this.rIt_LeftFont.ReadOnly = true;
			// 
			// bS_Info
			// 
			this.bS_Info.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bS_Info.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bS_Info.Caption = "�����  ������������ :";
			this.bS_Info.Id = 4;
			this.bS_Info.Name = "bS_Info";
			this.bS_Info.TextAlignment = System.Drawing.StringAlignment.Near;
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
			this.imageListResources.Images.SetKeyName(22, "1295959569_DeleteRed.png");
			this.imageListResources.Images.SetKeyName(23, "1295603703_old-edit-undo.png");
			this.imageListResources.Images.SetKeyName(24, "Export_import_1.png");
			this.imageListResources.Images.SetKeyName(25, "imageres.11.ico");
			this.imageListResources.Images.SetKeyName(26, "1297325052_electric_locomotive.png");
			this.imageListResources.Images.SetKeyName(27, "48px-List-add.svg.png");
			this.imageListResources.Images.SetKeyName(28, "32px-List-remove.svg.png");
			this.imageListResources.Images.SetKeyName(29, "Undo48.png");
			this.imageListResources.Images.SetKeyName(30, "add_32.png");
			// 
			// bB_Ok
			// 
			this.bB_Ok.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bB_Ok.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_Ok.Caption = "    Ok ";
			this.bB_Ok.Id = 31;
			this.bB_Ok.ImageIndex = 15;
			this.bB_Ok.Name = "bB_Ok";
			this.bB_Ok.Tag = "";
			// 
			// bS_Formula
			// 
			this.bS_Formula.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bS_Formula.Caption = "������� ������� ����� �� ������ ";
			this.bS_Formula.Id = 51;
			this.bS_Formula.Name = "bS_Formula";
			this.bS_Formula.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// cellValueControl1
			// 
			repositoryContent7.FieldLen = 2;
			repositoryContent7.Fraction = 0;
			repositoryContent7.RepItem = this.rItE_Int;
			repositoryContent8.FieldLen = 2;
			repositoryContent8.Fraction = 0;
			repositoryContent8.RepItem = this.rItC_PD;
			viewContent5.RepItems.Add(repositoryContent7);
			viewContent5.RepItems.Add(repositoryContent8);
			viewContent5.View = this.advBandedGridView1;
			repositoryContent9.FieldLen = 2;
			repositoryContent9.Fraction = 0;
			repositoryContent9.RepItem = this.rItE_Int;
			viewContent6.RepItems.Add(repositoryContent9);
			viewContent6.View = this.advBandedGridView2;
			repositoryContent10.FieldLen = 1;
			repositoryContent10.Fraction = 0;
			repositoryContent10.RepItem = this.rItC_PZ;
			repositoryContent11.FieldLen = 3;
			repositoryContent11.Fraction = 0;
			repositoryContent11.RepItem = this.rItE_Int;
			viewContent7.RepItems.Add(repositoryContent10);
			viewContent7.RepItems.Add(repositoryContent11);
			viewContent7.View = this.advBandedGridView4;
			repositoryContent12.FieldLen = 3;
			repositoryContent12.Fraction = 0;
			repositoryContent12.RepItem = this.rItE_Int;
			viewContent8.RepItems.Add(repositoryContent12);
			viewContent8.View = this.advBandedGridView3;
			this.cellValueControl1.Views.Add(viewContent5);
			this.cellValueControl1.Views.Add(viewContent6);
			this.cellValueControl1.Views.Add(viewContent7);
			this.cellValueControl1.Views.Add(viewContent8);
			// 
			// FRMNSI
			// 
			this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Appearance.ForeColor = System.Drawing.Color.Black;
			this.Appearance.Options.UseFont = true;
			this.Appearance.Options.UseForeColor = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1022, 635);
			this.Controls.Add(this.l_rKey3);
			this.Controls.Add(this.l_rKey1);
			this.Controls.Add(this.l_rKey2);
			this.Controls.Add(this.g1_l1);
			this.Controls.Add(this.g1_l5);
			this.Controls.Add(this.g1_l3);
			this.Controls.Add(this.g1_l2);
			this.Controls.Add(this.g1_l4);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.barDockControl3);
			this.Controls.Add(this.barDockControl4);
			this.Controls.Add(this.barDockControl2);
			this.Controls.Add(this.barDockControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "FRMNSI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.toolTipController1.SetSuperTip(this, null);
			this.Text = "FRMNSI - �Ĳ ����,  ���i�����, ����� �� ������������ ����  ";
			this.Load += new System.EventHandler(this.FRMNSI_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMNSI_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FRMNSI_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_Int)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L30)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L80)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_127)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_L15)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItC_PZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItC_PD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rICBE_AC)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rItE_aV4_Position)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_NDI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_Road)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rI_Otdel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rIt_LeftFont)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cellValueControl1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		[STAThread]
		static void Main() 
		{   // ��������� ������� Main()� ����������� ����������� 
			// ���������� ������ � ������ ����������, � ����� ������������ DLL_���� ��� ������� ������������

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
			//System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
			//RDA.RDF.BDOraCon = new OracleConnection("Server=vd.uz.gov.ua;User Id=roduz;Password=rd;Direct=True;Port=1522;Sid=VD;");

			//RDA.RDF.BDOraCon.Open();
			//RDA.RDF rdaf = new RDA.RDF();
			//rdaf.p_EditTabl("21");
			//Application.Run(new FRMNSI(RDA.RDF.BDOraCon, 21, "32", 'F', 12, 2004));
			 Helpers.ConnectionHelper.InitializeConnection();
			Application.Run(new FRMNSI(RDA.RDF.BDOraCon, 1, "Supervisor", 'F', 12, 2004));
			
			
		}
			
		private void FRMNSI_Load(object sender, System.EventArgs e)
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

			// ������ �� �������������� ���������������� ��� ��. � �����  p_ViewPage1()
	
			myAccess = RDA.RDF.UserParam.myAccess;   // ��������� �� Rda �������� myAccess 26/10/2010
			myRoad   = RDA.RDF.UserParam.myRoad;     // ��������� �� Rda ���� ������ ������������

            #endregion

			#region O���������� ��������� ������ � DataSet(Ds)
			
            // Ds(DataSet)- RowNum_����� �������� ��������� ������ ��� �������� � ��(����):             
			//              ������ �� ��, ����������� ������������ � constraints
			Ds = new DataSet();
			
			Ds.Tables.Add("Ds_Nsi");       // OraDANsi    -> ��������� ������� ��� ������� � "Ds_Nsi" �� Oracle_������� ��� 
			Ds.Tables.Add("Ds_Buffer");    // OraDABuffer -> ��������� ������� ��� ������� ������ �� Oracle_������
			
            #endregion			

			#region ��������� ��������� �������� 
			
			myTabl  = "";                      // ������� ��� �������������� ������� 
			tRow    = -1;                      // ����� ��������� ������ � grid2(...View9)
			//			bBuff   = false;                   // ��������� ������� - ��� ������    � Ds_Buffer
			bUpdBuff= false;                   // ��������� ������� - ��� ��������� � Ds_Buffer
			bB_Save.Enabled = false ;          // ������������� ������ : "�������� ����" 
			bS_InfoLeft.Caption = "";          // �������� �������������� ���� �� �����
			
			bE_Road.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-������
			bE_Otdel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-������ ���� ������ ������� �� ����������� �� ������

			p_ListNDI(); // �������� ������ �������������� ���

			#endregion	
			p_LanghInfo(); // ��������� ����������� ��������� ��� ��������� �����

			bE_NDI_EditValueChanged(null, null);
				
	
		}
   
        #region ������ ��� ������ � ������ InfoUpdSrv

		private void p_UpdInfo(DataRow dr, string InTabl, string OutTabl, string tModi, int OpFlag)
        {
        //-------------------------------------------------------------------------------------		
        // p_UpdInfo - ����� ������������ ��� ������ � InfoUpdSrv ���������� �� ���o������ 
		//             ������ GRMENU_AC,GRUSER, NSIROAD, NSIOTDEL � R_ROADRESP � �� �����  �� 
        //             c������� �� � ����� 
        //
        //  ���������: 
        //    InTabl     - ��� ������� ���  ��������� ���������
		//    OutTabl    - ��� ������� ���� ��������� ���������("" ������������� ��� �� ������� InTabl)
		//    DataRow dr - �������������� ������ ������� � ������� ������� ���������  
        //    tModi      - ������� ������������� : I,U,D ��� ����������� A, F 
        //                 ��� ������ :  =I-���������, =U-��������, =D-������� 
        //                 ��� �������:  =A-������ ���� ������� �������(����������� ������ � ������� ��)
        //    OpFlag     - ��� �������� � �������� InfoUpdSrv :
        //                 = 0 - ���������� ����� � Ds_Info
        //                 = 1 - ���������� � InfoUpdSrv ���� ����������� ����� � Ds_Info  �  ������� ������ 
        //                 = 2 - ���������� � InfoUpdSrv ���� ����������� ����� � Ds_Info 
        //
		//	���������� � ����� sB_Save_Click ��� ������������ � ������� InfoUpdSrv �������,
		//			       ���������� ���������� �� ���������� ������ �� �������� �� � �����
		//-------------------------------------------------------------------------------------			
		//   ������ :     
		//    RoadOut   - �� ������ ������ p_SaveIntoInfo(...), ���������� ������� �������� 
		//                ����������  ����������  ���  ����������� ����������� ����������:
		//                RdaFunc.RoadOut =-2 - ���������� ���� �� ��� ������ 
		//                RdaFunc.RoadOut =�� - ���������� ���� ������ �� ��-� ������
		//
        //
		//    ModiRow   = "" -������� �������� � Ds_Info ���������� �� ��������� �������(��� rModi: I,U,D)  
		//              = "A"-������� �������� � Ds_Info ���������� � ���������� �������(��� rModi: A)
		//              = "*"-������� ������ �� �������� � Ds_Info ���������� 
		//
	    //    TransRoad  - ������� ��������� ������ ������ ���������� RoduzDbUpd
        //               = "N"-������� ����������� ��������� ������
        //               = "Y"-������� ��� �������� ������ ������ ������ �� ������� ��. 
        //                 ��������������� ������ ��� ���������� � �������� �� �� ������� ������,
        //                  ����� ���������� ��������� ������ �� ���� �� �� ��������� �������.
		//
        //    new object[] 
        //    { Key1,Key2,Key3,-1-3-�� ����� ��� ������� ������ � InTabl
        //      "","",""},    Key4-6 -��������� ����� ��� ������� (����� �� ������������)
        //
        //    Id_Key     - ���������� � ���������� ����� ������� InfoUpdSrv : 
        //               = vkey1+vkey2+vkey3+vkey4+vkey5+vkey6 (���  ="A" ��� tModi="A")
        //     ��� vKey1-vKey6 �������� � 1 �� 6 ����� ���������� ���������� ����� ������ � InTabl     
        //
		//-------------------------------------------------------------------------------------			

			ModiRow = "";      // -������, ��� �������� ������ �� �������(��� rModi: I,U,D )  

		    string tTabl  = InTabl.Trim().ToUpper();
            string vKey1  = "";                     // �������� � ����� Key* 
            string vKey2  = "";
            string vKey3  = "";
			string Key1   = "";
			string Key2   = "";
			string Key3   = "";
			string Id_Key = "";
			#region ������������ Key1-Key3 � ����� ������ Id_Key ���  GRUSER, NSIROAD, NSIOTDEL � R_ROADRESP 
			switch (tTabl)
			{

				case "GRUSER": 
					#region �Ĳ ����� ������������� 
					vKey1 = dr["GR"].ToString();
					Key1 = "GR=" + vKey1.ToString();
					Id_Key = vKey1.ToString();
					RdaFunc.RoadOut = -2;   // ���������� ������������� ��������� � �� �����  �� ������� ���� ����� 
					break;
					#endregion

				case "NSIROAD":
					#region  �Ĳ ��������� 
					vKey1  = dr["ROAD"].ToString();
					Key1   = "ROAD=" + vKey1.ToString();
					Id_Key = vKey1.ToString();
					RdaFunc.RoadOut = -2;   // ���������� ������������� ��������� � �� ����� �� ������� ���� ����� 
					break;
				#endregion

				case "NSIOTDEL":
                    #region �Ĳ ����� �� ���������� 
                    vKey1  = dr["ROAD"].ToString();
                    vKey2  = dr["OTDEL"].ToString();
                    Key1   = "ROAD=" + vKey1.ToString();
                    Key2   = "OTDEL=" + vKey2.ToString();
                    Id_Key = vKey1.ToString() + vKey2.ToString();
					RdaFunc.RoadOut = (RDA.RDF.UserParam.RoadServ == 5) ? myRoad : 5;  // ������ ����������� ����������: �� ������ ������ ��� �� 
					break;
                    #endregion
               
				 case "R_ROADRESP":
                    #region �Ĳ ������������ ��� ����������� 
					vKey1  = dr["ROAD"].ToString();
                    vKey2  = dr["OTDEL"].ToString();
                    vKey3  = dr["POSITION"].ToString();

                    Key1   = "ROAD=" + vKey1.ToString();
                    Key2   = "OTDEL=" + vKey2.ToString();
                    Key3   = "POSITION=" + vKey3.ToString();
                    Id_Key = vKey1.ToString() + vKey2.ToString() + vKey3.ToString();
					RdaFunc.RoadOut = (RDA.RDF.UserParam.RoadServ == 5) ? myRoad : 5;  // ������ ����������� ����������: �� ������ ������ ��� �� 
					break;
                    #endregion
                default:
                    Id_Key = "";
                    break;
            }
            #endregion      
			if (RdaFunc.RoadOut == -2 || RDA.RDF.UserParam.RoadServ != myRoad)
             RdaFunc.p_SaveIntoInfo(new string[] {InTabl, OutTabl }, tModi, new object[] {Key1, Key2, Key3},
				Id_Key, "N", OpFlag); 

        }

		#endregion

		#region  ������, ��������� c ������� � ����������� ������� � ����� LookUpEdit ��� : �����, ������� �� �������
			
		// p_ListNDI - �������� ������ �������������� ��� � rI_NDI_����������� ���� bE_NDI
		private void p_ListNDI()
		{
			//-------------------------------------------------------------------------------------
			// p_ListNDI - ������������ ������ �������������� ��� � RepositoryItemLookUpEdit(rI_NDI)
			//-------------------------------------------------------------------------------------
			DataTable tNdi = new DataTable();
			tNdi.Columns.AddRange(new DataColumn[] { new DataColumn("ID", typeof(int)), new DataColumn("NAIM", typeof(string)) });
			tNdi.LoadDataRow(new object[] { 0, "�Ĳ ���� ���i����� �� ������������" }, true);
			tNdi.LoadDataRow(new object[] { 1, "�Ĳ ���i����� ..." }, true);
			tNdi.LoadDataRow(new object[] { 2, "�Ĳ ����� �� ���i�����" }, true);
			tNdi.LoadDataRow(new object[] { 3, "�Ĳ ������������ ��� �� ������ ���i�����" }, true);
			rI_NDI.DataSource = tNdi;
			//bE_NDI.EditValue = 0;
			idxNSI = 0;
		}

		// rI_NDI_EditValueChanging - �������� ����� ������� ��� �� ������
		private void rI_NDI_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_NDI_EditValueChanging - ����� ������������ ����� ������� ������ ��� ��� ��������:
			// -���� � ���������� ��� ���� ������(bOk =true), �� �� ��������� ��������
			// -e��� � ���������� ��� ���  ������, �� �� ��������� ���������(bUpdBuff=true), �� 
			//                  ������ ��������� ��� �������� ������� � ���������� "��������"
			//-------------------------------------------------------------------------------------
			if (idxNSI != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
				p_CtrlRowNsi(); // ���� ���� ��������� � �������(tRow) ������ --> bUpdBuff=true

				// �������������� ������ ��� f_ErrN �  f_ErrRowNsi
				string sNDI = rI_NDI.GetDataSourceValue("NAIM", Convert.ToInt32(e.NewValue.ToString())).ToString(); // �� ������� ����� ��������� ������ � rI_NDI
				string txt1 = (RDA.RDF.sLang == "R" ? " ������� � " : "  �����i� �� ") + sNDI ;
				string txt2 = (RDA.RDF.sLang == "R" ? "������� " : "³�����") + txt1;

				bool bOk = f_ErrRowNsi(myTabl, txt2)  ; // ��� �������(bOk=true),�� �� ��������� ��������

				if (!bOk && bUpdBuff && idxNSI > -1) // ���� ����� ��������� �� ��������� ����������: 
				{                                                    // - �������� ������������.  
					bOk  = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "� `" : "�  `") + myView.GroupPanelText + "`", "", "BUFF");
					if (!bOk) bE_NDI.EditValue = e.NewValue;         // - ��� ������ YES-NO ������� � bE_NDI_EditValueChanged
				}
				e.Cancel = bOk;
			}

		}
	
		// bE_NDI_EditValueChanged - ��������  ����� ������ ��� �� ������ 
		private void bE_NDI_EditValueChanged(object sender, EventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bE_NDI_EditValueChanged - ����� ������������ ����� ������ ��� �� ������ ��� :
			//	    - ������ �  myTabl ����� �������������� �������
			//	    - ���������� � idxNSI ������� ��������� ������ � ������ ���
			//	    - �������\����������� ����� � ���� ��� ������ �����, �������,...
			//      - ������ � Ds_Tmp2 ������ ��� �������� �� ������������� � �������� ��������
			//-------------------------------------------------------------------------------------			
			// ����������.
			//  ����� � Ds_Tmp2 ����������� ��� ������ � �� ����� �� : 
			//  -(��� GRUSER \ NSIROAD)      �������������� ��(myAccess=ALL) �� ������� ��(RoadServ=5).
			//
			//  -(��� NSIOTDEL \ R_ROADRESP) �������������� ��(myAccess=ALL) ��� ������(myAccess=ADMIN)
			//   ������ ������ ���������  � �� �� ������� �� ��� ������ 
			//
			//-------------------------------------------------------------------------------------
			bS_InfoLeft.Caption = "";
			grid1.Focus();
			
			bE_Road.Visibility  = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-������  ���� ������ ����������� �� ������
			bE_Otdel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-������ ���� ������ ������� �� ����������� �� ������
			bUpdBuff        = false; // ��������� ������� - ��� ��������� � Ds_Buffer
			bB_Save.Enabled = false;          // ������������� ������ : "�������� ����" 

			myTabl = ""; // ������� ��� �������������� ������� 
			if (idxNSI != Convert.ToInt16(bE_NDI.EditValue.ToString())) // ������ ��������� ������ � ������ ���
				idxNSI = Convert.ToInt16(bE_NDI.EditValue.ToString());

			#region ���� ���������� � myTabl �������� ����� �������������� ������� ��� � �.�.

			switch (idxNSI)
			{
				case 0:
					#region Oracle_������� GRUSER  - �Ĳ �����
					myTabl = "GRUSER";
					l_rKey3.Text = " ��� ";    // ���������� ��� ���. c�������� � ����� � �����  l_rKey1 �� �����

					break;
					#endregion

				case 1:
					#region Oracle_������� NSIROAD - �Ĳ �����
					myTabl = "NSIROAD";
					l_rKey3.Text = " ��� "; // ���������� ��� ���. c�������� � ����� � �����  l_rKey1 �� �����
					p_ListGr();  // �������� ������ ����� ��� ����������� ToolTip ��� ��������� ����� �� ���� PD
					p_ListAC();  // �������� ������ ��    ��� ����������� ToolTip ��� ��������� ����� �� ���� LIST_AC

					break;
					#endregion

				case 2:
					#region Oracle_������� NSIOTDEL - �Ĳ ����� �� ������
					myTabl = "NSIOTDEL";
					l_rKey3.Text = " ���� - ����� ";    // ���������� ��� ���. c�������� � ����� � �����  l_rKey1 �� �����
					bE_Road.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;// = Always -������� ���� ������ ����������� �� ������
					break;
					#endregion

				case 3:
					#region Oracle_������� R_ROADRESP -�Ĳ ������������ ��� �����������
					myTabl = "R_ROADRESP";
					l_rKey3.Text = " � ";    // ���������� ��� ���. c�������� � ����� � �����  l_rKey1 �� �����
					bE_Road.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; // = Always - ������� ���� ������ ����������� �� ������
					bE_Otdel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;// = Always -  ������� ���� ������ ������� �� ����������� �� ������

					break;
					#endregion

				default:
					break;
			}
			#endregion

			p_NewNsi();   // �������� � "Ds_Buffer" ���, ���������� �� ������ bE_NDI
		}
	
		// p_LoadRoad - �������� ������ ����������� � rI_Road_����������� ���� bE_Road
		private void p_LoadRoad()
		{
			//-------------------------------------------------------------------------------------		
			// p_LoadRoad - ����� ������������ ��� �������� ������ ����������� �� NsiRoad
			//              � rI_Road_����������� ��� ������ ����������� � ���� bE_Road
			//-------------------------------------------------------------------------------------		
			idxRoad = -1;
			bRoad   = false;          // ��������� ������� ���������� ������ � NsiRoad
			string txt = " WHERE Road > -1 AND Pd < 4 ";
			// ���� myAccess <> "ALL",  �� ����������� ������ ������������ myRoad
			txt += (myAccess != "ALL".ToString()) ? " AND Road=" + myRoad.ToString() : "";
			try
			{
				using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM NSIROAD "
												+ txt + " ORDER BY Road", RDA.RDF.BDOraCon))
				{
					using (DataTable dt = new DataTable())
					{
						oraDA.Fill(dt);
						rI_Road.DataSource = dt;
						if (dt.Rows.Count > 0) bRoad = true; // ��������� ������� ������� ������ � NsiRoad

						bE_Road.EditValue = dt.Rows[0]["ROAD"]; // � ������-������� ������ ������ 
						myRoad = Convert.ToInt32(bE_Road.EditValue);
						idxRoad = rI_Road.GetDataSourceRowIndex("ROAD", myRoad); // ������ ������ � ���������
					}
				}
			}
			catch (System.Exception ex)
			{
				ERR.Error err = new ERR.Error("�������", ERR.ErrorImages.CryticalError,
								"������� ��� ���������� ����� � ������  NSIROAD", ex.ToString(), 1);
				err.ShowDialog();
			}

		}
		// rI_Road_EditValueChanging - �������� ����� ���������� �����������
		private void rI_Road_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_Road_EditValueChanging - ����� ������������ ����� ���������� ����������� ���
			//            �������� ������� � ���������� ��������� ���� �� ��������� "��������"
			//-------------------------------------------------------------------------------------
			if (myRoad != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
				p_CtrlRowNsi(); // ���� ���� ��������� � �������(tRow) ������ --> bUpdBuff=true

				// �������������� ������ ��� f_ErrN �  f_ErrRowNsi
				int    idxN = rI_Road.GetDataSourceRowIndex("ROAD", Convert.ToInt32(e.NewValue.ToString()) ); // ������ ������ � ���������
				string sNDI = rI_Road.GetDataSourceValue("NAIMD", idxN).ToString(); // �� ������� ������ 
				string txt1 = (RDA.RDF.sLang == "R" ? " ������� � " : "  �����i� �� ") + sNDI
							+ (rI_Road.GetDataSourceValue("PD", idxN).ToString()=="1" 
							? (RDA.RDF.sLang == "R" ? " �����e" : " ���i�����") :"") ;
				string txt2 = (RDA.RDF.sLang == "R" ? "������� " : "³�����") + txt1;

				bool bOk = f_ErrRowNsi(myTabl, txt2); // ��� �������(bOk=true),�� �� ��������� ��������

				if (!bOk && bUpdBuff && idxN > -1) // ���� ����� ��������� �� ��������� ����������: 
				{                                                    // - �������� ������������.  
					bOk = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "� `" : "�  `") + myView.GroupPanelText + "`", "", "BUFF");
					if (!bOk) bE_Road.EditValue = e.NewValue;         // - ��� ������ YES-NO ������� � bE_NDI_EditValueChanged
				}
				e.Cancel = bOk;
			}
		}
		// bE_Road_EditValueChanged - ��������  ����� ��������� ����������� 
		private void bE_Road_EditValueChanged(object sender, EventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bE_Road_EditValueChanged - ����� ������������ ����� ��������� ����������� ��� 
			//                           ��������� ���  ������� ��� ������������ ���
			//-------------------------------------------------------------------------------------
			grid1.Focus();
			bS_InfoLeft.Caption = "";
			myRoad  = 0;
			idxRoad = -1; // ������ ������ � ���������
			if (bRoad)
			{
				myRoad  = Convert.ToInt32(bE_Road.EditValue); // ���������� ���� ����������� 
				idxRoad = rI_Road.GetDataSourceRowIndex("ROAD", myRoad); // ������ ������ � ���������
			}

			switch (myTabl)
			{
				case "NSIOTDEL":
					#region �Ĳ ����� (p_ReadRoad--> p_NewOtdel) �� ����������

					p_NewOtdel();                // - ����� ������� ��� ���������
					break;
					#endregion

				case "R_ROADRESP":
					#region �Ĳ ������������ ���(p_LoadRoad--> p_LoadOtdel) �� ������� �����������

					p_LoadOtdel();               // - ����������� ������ ������� ��� ������ ���
					break;
					#endregion
				default:
					break;
			}

		}


		// p_LoadRoad - �������� ������ ������� � rI_Otdel_����������� ���� bE_Otdel
		private void p_LoadOtdel()
		{
			//-------------------------------------------------------------------------------------		
			// p_LoadOtdel - ����� ������������ ��� �������� ������ ������� �� ����������� �� 
			//               NsiOtdel � rI_Otdel_����������� ��� ������ ������� � ���� bE_Otdel
			//-------------------------------------------------------------------------------------		

			idxOtdel = -1;      // ������ ������ � ���������
			bOtdel   = false;   // ��������� ������� ���������� ������ � NsiOtdel
			try
			{
				using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM NsiOtdel "
											+ @" WHERE Road=" + myRoad.ToString()
											+ @" ORDER BY Road, Otdel", RDA.RDF.BDOraCon))
				{
					using (DataTable dt = new DataTable())
					{
						oraDA.Fill(dt);
						rI_Otdel.DataSource = dt;
						if (dt.Rows.Count > 0) bOtdel = true; // ��������� ������� ������� ������ � NsiRoad

						bE_Otdel.EditValue = dt.Rows[0]["OTDEL"]; // � ������-������� ������ ������ 
						myOtdel  = Convert.ToInt32(bE_Otdel.EditValue);
						idxOtdel = rI_Otdel.GetDataSourceRowIndex("OTDEL", myOtdel); // ������ ������ � ���������
					}
				}
			}
			catch { }

			//catch (System.Exception ex)
			//{
			//    ERR.Error err = new ERR.Error("�������", ERR.ErrorImages.CryticalError,
			//                    "������� ��� ���������� ����� � ������  NsiOtdel ", ex.ToString(), 1);
			//    err.ShowDialog();
			//}

		}
		// rI_Otdel_EditValueChanging -�������� ����� ���������� ������ �� �����������
		private void rI_Otdel_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_Otdel_EditValueChanging-����� ������������ ����� ���������� ������ �� �����������
			//            ��� �������� �������, ���� ���� ��������� � �� ��������� "��������"
			//-------------------------------------------------------------------------------------
			if (myOtdel != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
				p_CtrlRowNsi(); // ���� ���� ��������� � �������(tRow) ������ --> bUpdBuff=true

				// �������������� ������ ��� f_ErrN �  f_ErrRowNsi
				int idxN    = rI_Otdel.GetDataSourceRowIndex("OTDEL", Convert.ToInt32(e.NewValue.ToString())); // ������ ������ � ���������
				string sNDI = rI_Otdel.GetDataSourceValue("NAIM", idxN).ToString(); // �� ������� ������ 
				string txt1 = (RDA.RDF.sLang == "R" ? " ������� � ������ " : " �����i� �� ����� ") + sNDI ;
				string txt2 = (RDA.RDF.sLang == "R" ? "������� " : "³�����") + txt1;

				bool bOk = f_ErrRowNsi(myTabl, txt2); // ��� �������(bOk=true),�� �� ��������� ��������

				if (!bOk && bUpdBuff && idxN > -1) // ���� ����� ��������� �� ��������� ����������: 
				{                                                    // - �������� ������������.  
					bOk = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "� `" : "�  `") + myView.GroupPanelText + "`", "", "BUFF");
					if (!bOk) bE_Otdel.EditValue = e.NewValue;         // - ��� ������ YES-NO ������� � bE_NDI_EditValueChanged
				}
				e.Cancel = bOk;
			}
		}
		//  bE_Otdel_EditValueChanged -��������  ����� ��������� ������ �� �����������
		private void bE_Otdel_EditValueChanged(object sender, EventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//  bE_Otdel_EditValueChanged - ����� ������������ ����� ��������� ������ �� �����������
			//-------------------------------------------------------------------------------------
			grid1.Focus();
			myOtdel  = 0;
			idxOtdel = -1;      // ������ ������ � ���������
			if (bOtdel)
			{
				myOtdel  = Convert.ToInt32(bE_Otdel.EditValue); // �������� ���  
				idxOtdel = rI_Otdel.GetDataSourceRowIndex("OTDEL", myOtdel); // ������ ������ � ���������
			}
			p_NewResp();            // ������� ������������� ��� ������ ��� ��������� 

		}


		#endregion

		#region ����� ������ � ������ ��� ������ � ������: ������, ��������, ������, ����������, �������� �����...


		// p_NSDClick - �����������-������������� ������
		private void p_NSDClick()
		{
			//-------------------------------------------------------------------------------------
			// p_NSDClick - ����� ������������ ��� �������������-����������� ������:
			//		        sB_Save(�������� ����),... � ����������-������� ��������� 
			//                                 
			// ���������� � ����� p_ViewPage1
			//-------------------------------------------------------------------------------------

			bB_AddRow.Enabled        = bEditTabl;
			bB_CancelEditRow.Enabled = bEditTabl;
			bB_DelRow.Enabled        = bEditTabl;
			
			myView.OptionsBehavior.Editable = bEditTabl; //���������-��������� ��������� � �������

		}

		private void p_DsNsi()
		{
			//-------------------------------------------------------------------------------------		
			// p_DsNsi - ����� ������������ ����� ������ �� Oracle_������ GRUSER, NSIROAD, NSIOTDEL 
			//           ��� R_ROADRESP  �   Ds_Nsi(�� �����).
			//
			//	���������� � ����� sB_Save_Click ����� ���������� ������ � Oracle_��������
			//-------------------------------------------------------------------------------------			

			if (bEditTabl) // ���� ��������� ��������� � ��������
			{
				string nKey  = "";  // ��� ����-�����
				try
				{
					#region  ����� ������ �� Oracle_������ GRUSER, NSIROAD, NSIOTDEL ��� R_ROADRESP
					switch (myTabl)
					{
						case "GRUSER": 
						    #region �Ĳ ����� ������������� 
							nKey="GR";
							OraDANsi    = new OracleDataAdapter(@" SELECT 0 as OldKey, t.* FROM GrUser t "
								+ " ORDER BY Gr", RDA.RDF.BDOraCon);
							break;
                            #endregion

						case "NSIROAD":   
							#region �Ĳ ��������� 
							nKey = "ROAD";
							OraDANsi = new OracleDataAdapter(@" SELECT 0 as OldKey,t.* FROM NsiRoad t "
								+ @" WHERE t.Road > -1 ORDER BY Road", RDA.RDF.BDOraCon);
							break;
                            #endregion

						case "NSIOTDEL": 
							#region �Ĳ ����� �� ���������� 
							nKey = "OTDEL";
							OraDANsi = new OracleDataAdapter(@" SELECT 0 as OldKey, t.* FROM NsiOtdel  t "
								+ @" WHERE t.Road=" + myRoad.ToString() + " ORDER BY Road, Otdel", RDA.RDF.BDOraCon);
							break;
                            #endregion

						case "R_ROADRESP":
							#region �Ĳ ������������ ��� ����������� 
							nKey = "POSITION";
							OraDANsi = new OracleDataAdapter(@" SELECT 0 as OldKey, t.* FROM R_RoadResp t "
							+ @" WHERE t.Road =" + myRoad.ToString() + " AND t.Otdel=" + myOtdel.ToString()
							+ @" ORDER BY Road, Otdel,Position", RDA.RDF.BDOraCon);


							break;
                            #endregion
						default:
							break;

					}
					#endregion

					#region ���������� Ds_Nsi    ������� �� Oracle_������� �� �����

					Ds.Tables["Ds_Nsi"].Dispose();       // ��������� ��� ������� 
					Ds.Tables["Ds_Nsi"].Clear();         // �������   ��� ������ � �������    
					OraDANsi.Fill(Ds, "Ds_Nsi");         // ��������  Ds_������� 
					foreach (DataRow drN in Ds.Tables["Ds_Nsi"].Rows)
					{
						drN["OLDKEY"] = drN[nKey];       // ������� ������ �������� �����
					}
					Ds.Tables["Ds_Nsi"].AcceptChanges(); // ��������� ������ ������� � Unchanged 

					// �������� ������������� ������ SQL(Update,Delete,Insert)  
					//  ��� ���������� Oracle_������� ��� �� ���������� �����                	                                              
					OraCBNsi = new OracleCommandBuilder(OraDANsi);
					// ��� ����������� ������ �������� � �� ������ ����, ��� ��������� �������� ����������  
					OraCBNsi.ConflictOption = System.Data.ConflictOption.OverwriteChanges;
					//OraCBNsi.SetAllValues = true;
					OraDANsi.MissingSchemaAction = MissingSchemaAction.AddWithKey;

					#endregion
		
				}
				catch { }
			}
		}

		// p_UpdRowNsi - ���������� ������(drN) � Ds_Nsi �� ��������������� ������(drB) Ds_Buffer 
		private void p_UpdRowNsi(DataRow drB, DataRow drN)
		{
		//-------------------------------------------------------------------------------------
		// p_UpdRowNsi-����� ������������ ��� ���������� ������(drN) � Ds_Nsi �� ���������������
		//             ������(drB) Ds_Buffer 
		//
		// ���������:  drB  - ������� ������ �� "Ds_Buffer", drN - ���������� ������ � "Ds_Nsi" 
		//
		// ���������� � ����� : sB_Save_Click
		//-------------------------------------------------------------------------------------
			
			drN["USERID"]  = uId ;               // Id_��� ������������ 
			drN["LASTDATE"]= DateTime.Now ;                       //  � ������� ���� ���������
			drN["IP_USER"] = RdaFunc.f_Ip_User(); // IP_�� � �������� ���������� ����������
			if (drB["OLDKEY"].ToString() == "-1")			
				drN["OLDKEY"]  = drB["OLDKEY"];                  // ���� ������� ����� ������-  ��� ����� ������	
			switch (myTabl)
			{
				case "GRUSER": 
					#region ��� �����
					drN["GR"]   = drB["GR"]   ;   // ��� ������
					drN["NAIM"] = drB["NAIM"] ;   // ����� ������

					break ;				
					#endregion			
				
				case "NSIROAD":
					#region ��� �����������
					int i = Convert.ToInt32(drB["ROAD"].ToString());
					drN["ROAD"]   = drB["ROAD"]   ; // ��� �����������

					if (i >-1)
						drN["ROADF"] = f_IdRoad(i, Helpers.CommonFunctions.GetIDTable("NSIROAD")).ToString(); // Id_������ ������ � ����: 005RR0000000000 
					else drN["ROADF"]= "".ToString();
					drN["PD"]        = drB["PD"]     ;   // �������  ������
					drN["NAIM"]      = drB["NAIM"]   ;   // ����� ����������� � �.�.
					drN["NAIMD"]     = drB["NAIMD"]  ;   // ����� ����������� � �.�.
					drN["NAIMF"]     = drB["NAIMF"]  ;   // ������ �������� ������.
					drN["IP_SERV"]   = drB["IP_SERV"];   //  IP_������� 
					drN["LIST_IDAC"] = drB["LIST_IDAC"]; // ������(�� ������� LIST_AC )  ID_�����   � ���� : RD, NF \ RD \  NF, ����������� ����� �� �������� ������ 

					break;				
					#endregion			
			
				case "NSIOTDEL":
					#region ��� ������� �� �����������
				
					drN["ROAD"]  = myRoad;   // ��� ����������� = myRoad
					drN["OTDEL"] = drB["OTDEL"];        // ��� ������
					drN["NAIM"]  = drB["NAIM"] ;        // ����� ������
					drN["KOD"]   = drB["KOD"];          // ���� ������

                    break ;				
					#endregion			
				
				case "R_ROADRESP":
					#region ��� �������������(�� ������� ���������,...) ��� �����������
					drN["ROAD"]    = myRoad ;            // ��� ����������� = myRoad
					drN["OTDEL"]   = myOtdel ;           // ��� ������      = myOtdel
					drN["POSITION"]= drB["POSITION"]   ; // ���������� ����� ������ ������� ??????????
					drN["FIO"]     = drB["FIO"]        ; // ����� ��`� (ϲ�) ���������,  ������  ����� ��������  			
					drN["POSADA"]  = drB["POSADA"]     ; // ���������(������)				
					drN["PHONE"]   = drB["PHONE"]      ; // �������				
					drN["PZ"]      = drB["PZ"]         ; // �������: =1-�����������, 0 - ������������� �� ��������, 2-���� ���������� ��������(��� ��� ����������) � �.�.

					break ;				
					#endregion			
				
				default:
					break;
			}

		}

		// bB_Save_ItemClick - ������� �� ������� ������ "��������" - ���������� ������ 
		private void bB_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_Save_ItemClick- ����� ������������ ��� ������� �� ������ "�������� ����" ��� 
			//                    ���������� ������ � Oracle_��������, ���� ���� ���������
			//-------------------------------------------------------------------------------------			
			//  ���������� : 
			//
			//  1. ��������� Oracle_������ GRUSER ��� NSIROAD ����� ��������� ������ �������������
			//     ��(myAccess=ALL) � ������ �� ������� �� (RoadServ=5). 
			//     ��� ��������� ������������� � ������� �� RODUZ(�� ����� ��) �� ������� �����
			//
			//  2. ��������� Oracle_������ NSIOTDEL ��� R_ROADRESP ����� ��������� ��� �������������
			//     ��(myAccess=ALL) ��� �������������(myAccess= "ADMIN") ������(RdaFunc.RoadUser =
			//     myRoad). ������ ��������� ��������� ��� � �� ����� �� �� �������� �� ��� � �����. 
			//       - ��������� ����������� �� �������   ��   ������������� �� ������ ������(myRoad)
			//         �� ������ ��������������� ������(myRoad)
			//       - ��������� ����������� �� ������� ������ ������������� �� ������ ��
			//
			//  3. ���������� � ������������� ��������� � �������� ������������ �  ������� 
			//     InfoUpdSrv �� �������  �� ��� ������(��.  p_UpdInfo). 
			//
			//  4. ��� ��� ����� bB_Save_ItemClick ����� ���� ������ �� ������ ��� ������� ������  
			//     bB_Save, �� � �� ������ f_ErrN, �� � ����� �������������� �������� �� ������ 
			//     �������� �� ���������� ��������� � Oracle_������ ��� bB_Save.Enabled=false
			//-------------------------------------------------------------------------------------
			bS_InfoLeft.Caption = "";
			p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������

			if (bB_Save.Enabled)  // ���� ������ ���������� ��������, �� ��������� �������
			{
				#region ���������� ���������� � ����� ��������

				string sTxt1 = (RDA.RDF.sLang == "R") ? "������" : "�������";
				string sTxt2 = (RDA.RDF.sLang == "R") ? "�������� ������ ��� ���������� ������ � ������� "
											: "������� ������� ��� ��������� ������ �� ������� ";
				string sTxt3 = "\n\n" + RdaFunc.f_Repl(" ", 20)
											+ (RDA.RDF.sLang == "R" ? "��������� ��������� !" : "��������� ���� !");
				string rModi = "";     // ������� ������ : rModi ="I"-�����, ="U"-��������, ="S"-��� ���������,="D"-�������
				string tKey = "";     // �������� ����� � ������� ���
				string KeyN = "";     // �������� ����� � ������� ��� ���  Ds_Nsi
				string KeyB = "";     // �������� ����� � ������� ��� ���  Ds_Buffer


				#endregion

				try
				{
					p_CtrlRowNsi();     // ���� ���� ��������� � �������(tRow) ������ --> bUpdBuff=true

					string txt1 = RDA.RDF.sLang == "R" ? " ���������� ��������� !" : " �������� ���� !";
					string txt2 = (RDA.RDF.sLang == "R" ? "������� " : "³�����") + txt1;
					bool bOk = f_ErrRowNsi(myTabl, txt2); // ���� � ������ ���� ������ (bOk=true), �� �� ��������� ��������

					if (bOk == false)
					{
						p_DsNsi();                  // ����� ������ �� Oracle_������ ��� � Ds_������� Ds_Nsi 

						#region ���� ���������-���������� ����� ���������������� ���  � ������ � Ds_Info, ��� ���������� �� ��������
						tKey = (myTabl == "GRUSER" ? "GR"           // ��� �Ĳ ����� 
							 : (myTabl == "NSIROAD" ? "ROAD"        // ��� �Ĳ ���������
							 : (myTabl == "NSIOTDEL" ? "OTDEL"      // ��� �Ĳ ����� �� ����������
							 : (myTabl == "R_ROADRESP" ? "POSITION" // ��� �Ĳ ������������ ��� �����������
								 : ""))));

						foreach (DataRow drB in Ds.Tables["Ds_Buffer"].Rows)
						{
							rModi = drB["RMODI"].ToString(); // ������� ������ : ="I"-�����, ="U"-��������, ="S"-��� ���������,="D"-�������
							KeyB = drB["OLDKEY"].ToString(); //     ��� RMODI ="I" �����  OLDKEY=-1
							if (rModi == "S") continue;        // ����������  �� ��������� ������

							#region rModi=="U" -> ���� ���������  ������ ���������������� ��� � Ds_Nsi(��� �� �����)
							if (rModi == "U")                   // ������ ��������
							{

								DataRow[] drN = Ds.Tables["Ds_Nsi"].Select(string.Format("OLDKEY = '{0}'", KeyB));
								if (drN.Length > 0) // ������ ������� �  Ds_Nsi
								{
									#region ������������ 2-� ������� � Ds_Info ��� ��������� ����_�����
									if (ModiRow == "")                       // ���������� � Ds_Info ������ 
									{
										if (KeyB != drB[tKey].ToString())    // ���� ��� ������� ���� ������ ��� :
										{
											p_UpdInfo(drN[0], myTabl, myTabl, "D", 0);	// -��������   ������ �� ������� �� ��� ������			
											p_UpdInfo(drB, myTabl, myTabl, "I", 0);	// -���������� ������ 		
										}
										else
											p_UpdInfo(drB, myTabl, myTabl, "U", 0);      	// -���������� ������ 
									}
									#endregion

									p_UpdRowNsi(drB, drN[0]);    // O��������� �������� � ����� ������ Ds_Nsi �� Ds_Buffer
									bUpdBuff = true;
								}


							}
							#endregion

							#region rModi=="I" -> ���� ���������� ������ ���������������� ��� � Ds_Nsi(��� �� �����)
							if (rModi == "I")     // ������  ����� � Ds_Buffer
							{
								string nModi = "I";

								DataRow[] drK = Ds.Tables["Ds_Nsi"].Select(string.Format("OLDKEY = '{0}'", drB[tKey].ToString()));

								#region ���� ��� ������ ����������� � Ds_Buffer  ���� ������ � ���� �� ������  � Ds_Nsi
								if (drK.Length > 0) // �� ����������, �.�. � Ds_Buffer ��� ������ ���� ������� � ����� ��������� 
								{
									nModi = "U";  // ������������� ������� ������ "I" ��  "U"-��������
									p_UpdRowNsi(drB, drK[0]);    // O������ �������� � ����� ������ Ds_Nsi �� Ds_Buffer
								}
								#endregion

								#region ���� ��� ������ ����������� � Ds_Buffer  ���  ������ � ���� �� ������  � Ds_Nsi

								else                // ��� ������ � �� ����������, �.�. � Ds_Buffer ��� ������ ���� ������� � ����� ��������� 
								{
									DataRow drN = Ds.Tables["Ds_Nsi"].NewRow();  // ��������� ����� ������ � Ds_Nsi
									p_UpdRowNsi(drB, drN);                       // ������� �������� � ���� ������ �� Ds_Buffer
									Ds.Tables["Ds_Nsi"].Rows.Add(drN);           // C������� ����� ������ � Ds_Nsi

								}
								#endregion

								if (ModiRow == "") // ���������� � Ds_Info ������ � ������������� ���������� ��� ����������
									p_UpdInfo(drB, myTabl, myTabl, nModi, 0);    // ������ �� ������� �� ��� �����
								bUpdBuff = true;
							}
							#endregion
						}

						#endregion

						#region ���� �������� ����� ���������������� ��� � Ds_Nsi � ������ � Ds_Info ��� ���������� �� ��������

						foreach (DataRow drN in Ds.Tables["Ds_Nsi"].Rows)
						{
							KeyN = drN["OLDKEY"].ToString();  // � ������ ��� �������� ��� 
							if (KeyN == "-1") continue;        // ����� ������ ���������� ����� �� Ds_Buffer

							DataRow[] drB = Ds.Tables["Ds_Buffer"].Select(string.Format("OLDKEY = '{0}'", KeyN));
							if (drB.Length < 1 || drB[0]["RMODI"].ToString() == "D") // ������ �������� �������� ��  Ds_Nsi
							{
								if (ModiRow == "")  // ���������� � Ds_Info ������ � ������������� �������� ������
									p_UpdInfo(drN, myTabl, myTabl, "D", 0);	//  �� ������� �� ��� �����	
								bUpdBuff = true;
								drN.Delete();            // ������� � Ds_Nsi ������ �� ��������
							}
						}
						#endregion

						#region ���� ���������� Oracle_������  �� Ds_Nsi, Ds_Nsi_RD � Ds_Info
						if (bUpdBuff == true)
						{
							string sNDI = rI_NDI.GetDataSourceValue("NAIM", idxNSI).ToString();
							sTxt2 += sNDI; // �� ������ ���� ��� ����������(��� ERR.Error err6 =...)
							bUpdBuff = false;                     // ��������� ������� ���������� ��������� 
							OraDANsi.Update(Ds, "Ds_Nsi");       // ������� ������ �� Ds_Nsi � Oracle_������� �� ����� ��

							if (ModiRow == "")
								RdaFunc.p_SaveIntoInfo(null, null, null, null, null, 2);

							if (sender != null) // ���� ���� ������ ��. bB_Save, �� :
							{
								//int idx = 0;     
								//if(E_Road.Visible) idx = E_Road.ItemIndex; //  �������� ������  ������
								bE_NDI_EditValueChanged(null, null); // -����� ���������� ��� �� Oracle � Ds_Buffer � Ds_Nsi
								bE_Road.EditValue = myRoad;
								//E_NDI_SelectedIndexChanged(null,null);   

								//if(E_Road.Visible) E_Road.ItemIndex=idx;  // -����������� � ������� ����
							}
						}

						#endregion

						bB_Save.Enabled = false;  // ������������� ������ : "�������� ����" 
						bUpdBuff = false;         // ��������� ������� ���������� ��������� 
					}
				}
				catch (Exception ex)
				{

					ERR.Error err6 = new ERR.Error(sTxt1, ERR.ErrorImages.CryticalError, sTxt2 + sTxt3, ex.ToString(), 1);
					err6.ShowDialog();
				}
			}

		}

		// bB_AddRow_ItemClick - ������� �� ������� ������ "���� �����" - ���������� ������ 
		private void bB_AddRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_AddRow_ItemClick - ����� ������������ ��� ������� �� ������ "���� �����" ��� 
			//                      �������� � ���������� ��������� �����  ������ � Ds_�������� ���
			//		�������������� ���� ��� Ds_������� : 
			//	 RMODI - ������� ������: ="I"-�����, ="U"-��������, ="S"-��� ���������
			//	 RKEY  - ������� ��������� ����� ��� �������� ������ : 
			//           =0-���������, =1-���������, ��� ��� ���� ������������ � ������ �������� 
			//-------------------------------------------------------------------------------------
			//sB_Exit.Focus();
			string txt2 = (RDA.RDF.sLang == "R") ? "�������� �������� ����� ������ !"
										 : "��������� ��������� ������ ������ !";
			if (f_ErrRowNsi(myTabl, txt2))
				return;                 // ���� � ���������� ������ ������-������� �������� �����
			else
			{
				DataRow drN = Ds.Tables["Ds_Buffer"].NewRow();    // ��������� ����� ������ :

				drN["RMODI"]    = "I";            //  ������� "I"-����� ������
				drN["RKEY"]     = 0;              //  �� ������������ � �������� ��������
				drN["NUSER"]    = uName;          //  ��� ������������ ������� - ��� ����������� ������
				drN["USERID"]   = uId;            //               � ��� Id_��� 
				drN["LASTDATE"] = DateTime.Now; ; //  ������� ����
				drN["OLDKEY"]   = -1;             //  ���� ������� ����� ������: GR,ROAD, OTDEL ��� POSITION ��� ����� ������

				#region ���� ������������ �������� � ���� ������ ���������������� ��� 

				switch (myTabl)
				{
					case "GRUSER":
						#region �Ĳ ����� �������������

						drN["GR"] = -2;         // ��� ������
						drN["NAIM"] = "*";      // ����� ������
						break;

						#endregion

					case "NSIROAD":
						#region �Ĳ ���������

						drN["ROAD"]    = -2;     // ��� �����������
						drN["IP_SERV"] = "";     //  IP_������� 
						drN["NAIM"]    = "*";    // ����� ����������� � �.�.
						drN["NAIMD"]   = "*";    // ����� ����������� � �.�.
						drN["NAIMF"]   = "";     // ������ �������� ������.
						drN["PD"]      = -2;     // �������  ������
						break;

						#endregion

					case "NSIOTDEL":
						#region �Ĳ ����� �� ����������

						drN["ROAD"]  = myRoad;   // ��� �����������
						drN["KOD"]   = "*";      // ���� ������
						drN["OTDEL"] = -2;       // ��� ������
						drN["NAIM"]  = "*";      // ����� ������
						break;

						#endregion

					case "R_ROADRESP":
						#region �Ĳ ������������ ��� �����������

						drN["ROAD"]  = myRoad;   // ��� �����������
						drN["OTDEL"] = myOtdel;  // ��� ������
						drN["POSITION"] = -2;    // ���������� ����� ������ �������
						drN["FIO"]   = "*";      // ����� ��`� (ϲ�) ���������,  ������  ����� ��������  			
						drN["POSADA"]= "*";      // ���������(������)				
						drN["PHONE"] = "";       // �������				
						drN["PZ"]    = 0;        // �������: =1-�����������, 0 - ������������� �� ��������, 2-���� ���������� ��������(��� ��� ����������) � �.�.
						break;

						#endregion
					default:
						break;
				}

				#endregion

				Ds.Tables["Ds_Buffer"].Rows.Add(drN);    // C������� ����� ������ � "Ds_Buffer"
				p_FooterBarInfo(drN);                     // ���������� � ������� ������� ������ 

				tRow = myView.RowCount - 1;              // ��������� ����� ����� ������ 
				myView.RefreshData();                    // ������� ��� View �� ������
				p_RowFocus();
			}

		}

		//bB_CancelEditRow_ItemClick - ������� �� ������� ������ "³������ ���" - ������ ���������� ��� ��������� ������ 
		private void bB_CancelEditRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//bB_CancelEditRow_ItemClick - ����� ������������ ��� ������� �� ������ "³������ ��� "  ���
			//                         ������ ���������� ����� ����  ��������� ������ ������
			//
			//	dr3["RMODI"]-������� ������ : rModi ="I"-�����, ="U"-��������,="S"-��� ���������
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
			if (tRow > -1)
			{
				DataRow dr3 = Ds.Tables["Ds_Buffer"].Rows[tRow];
				p_FooterBarInfo(dr3);    // ���������� � ������� ������� ������ 

				dr3.RejectChanges();    // �������� ��� ��������� � ������ �� ���������� AcceptChanges 
			}
		}

		// bB_DelRow_ItemClick - ������� �� ������� ������ "�������� �����" - �������� ������ 
		private void bB_DelRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_DelRow_ItemClic - ����� ������������ ��� ������� �� ������ "�������� �����"  
			//
			//	�������������� ���� ��� Ds_������� : 
			//	 RMODI - ������� ������: ="I"-�����, ="U"-��������, ="S"-��� ���������
			//	 RKEY  - ������� ��������� ����� ��� �������� ������ : 
			//           =0-���������, =1-���������, ��� ��� ���� ������������ � ������ �������� 
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������

			//sB_Exit.Focus(); // ��������� ����� � ���� ������
			if (tRow > -1)
			{
				DataRow dr4 = Ds.Tables["Ds_Buffer"].Rows[tRow];
				if (dr4["RKEY"].ToString() == "0")      // ���� ��������� �������� 
				{
					if (dr4["RMODI"].ToString() != "I") // - � ������ �� �����, �� ���������
					{
						bUpdBuff        = true;            //       ������� ��� ���������� � ��
						bB_Save.Enabled = true;         // �A���������� ������ : "��������" 
						dr4["RMODI"] = "D".ToString();
					}
					p_FooterBarInfo(dr4);              // ���������� � ������� ������� ������ 

					dr4.AcceptChanges();              // �������� � ������������ ...View � ������ Ds_�������
					dr4.Delete();                     // - ������� ee �� ��������
					dr4.AcceptChanges();              // �������� � ������������ ...View � ������ Ds_�������
					if (myView.FocusedRowHandle > -1)
					{
						tRow = Convert.ToInt32(myView.FocusedRowHandle.ToString());
						p_RowFocus();
					}
				}
				else bNsiErr = f_ErrRowNsi("ERRDEL", "");
				bNsiErr = false;                      // ��������� ������� ���������� ������ � ������ Ds_Buffer 
			}
		}
		
		// FRMNSI_FormClosing - �������� ����� ��������� �����
        private void FRMNSI_FormClosing(object sender, FormClosingEventArgs e)
        {
			//-------------------------------------------------------------------------------------
			//FRMNSI_FormClosing - ����� ������������ ��� ������� �� ������ ����� "�" ��� 
			//                     ���������� �������� ����� ��������� �����. �����������  ��������� 
			//                     �� ���������� ("��������") ���������
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
			string txt1 = RDA.RDF.sLang == "R" ? " �������� ����� " : " �������� ���� ";
			string txt2=  RDA.RDF.sLang == "R" ? "�������o "          :"³����� "; 

			p_CtrlRowNsi(); // ���� ���� ��������� � �������(tRow) ������ --> bUpdBuff=true
	
			bool bOk  = f_ErrRowNsi(myTabl, txt2); // ���� � ������ ���� ������ (bOk=true), �� �� ��������� ��������
			if ( bOk == false && bUpdBuff )
				 bOk  = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "� `" : "�  `") + myView.GroupPanelText + "`", "", "BUFF");
			e.Cancel  =  bOk  ;

        }

		//bB_Exit_ItemClick - �������� ��� ������� �����
		private void bB_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//bB_Exit_ItemClick- ����� ������������ ��� ������� �� ������ "�����" ��� �������� �����
			//-------------------------------------------------------------------------------------
			this.Close();  

		}
		
	
		#endregion	
	
		#region ������, ��������� � ���������� ��� �����(GrUser), �����(NsiRoad), �������(NsiOtdel) � ���, ������������� �� ��������(R_RoadResp) 
	
		private bool f_ErrRowNsi(string par1, string par2)
		{	
		//-------------------------------------------------------------------------------------
		// f_ErrRowNsi-������� ������������ �������� � ������� grid1(...View1-8) � grid3(...View5)
		//		��:   - �� ��������� ������������ ���������� ������
		//            - ������ ��� ���������� ������������ �����
		// ��� ������������� ������ ������������ true � ��������������� bNsiErr = true
		// ���� RDA.RDF.sLang ="R"-������ ��������� �� ������� ����� -�� ����������("U") �����
		//-------------------------------------------------------------------------------------
		// ���������: par1= "ERRADD" -����������, ="ERRMODI"-���������,="ERRDEL"-�������� ������,
		//                = myTabl-��� �������(��� �������������� ������) ������������ � �����
		//                         E_NDI_SelectedIndexChanged
		// par2= "" ��� ���������� �� ������ ��������(��������, "���������� ����� ������ ���")
		//-------------------------------------------------------------------------------------
		// ������� ���������� � ������: sB_DelRow_Click,      gridView_BeforeLeaveRow
		//                              gridView_ValidateRow, gridView_CellValueChanged
		//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
			bNsiErr = false;    // ������� ���������� ������ � ������ Ds_Buffer 
			int iE   = 0;
			int j    = 0;
			string sTxt1 = (RDA.RDF.sLang=="R") ? "������ � ������" : "������� � ������";
			string sTxt2 = "" ;
			string sTxt3 = "\n" +RdaFunc.f_Repl(" ",20)+
						(RDA.RDF.sLang=="R" ? "��������� ������ � ���������� ������."
									: "�������� ������� � ���������� ������.");
			try
			{
				#region ����� �������� par1
				switch (par1.ToUpper().Trim())
				{
					case "GRUSER": 
						#region �������� � ��� �����
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							j = 0;
							iE = Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"GR").ToString());
							if (iE < 0) 	sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������ ��������  �����" 
												: " - ������������ �������� �����" )+" ���="+iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt16(drN["GR"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������ �����" 
												: " - ���������� �����")+" ���="+iE.ToString()+"\n";
											break;
										}
									}
								}
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString().Trim()=="")
								sTxt2+= (RDA.RDF.sLang=="R" ? " - ������������ �������� � ����" 
									: " - ������������ �������� � ����" )+" `�����`" ;
							sTxt3 += "\n\n"+par2;
						}
					
						break ;	
						#endregion			
					case "NSIROAD":
						#region �������� � ��� �����������
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							j = 0;
							iE = Convert.ToInt32(myView.GetRowCellValue(myView.FocusedRowHandle,"ROAD").ToString());
							if (iE < -1) 	sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������ �������� �����" 
												: " - ������������ �������� �����" )+" ���="+iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt32(drN["ROAD"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������ �����" 
												: " - ���������� �����")+" ���="+iE.ToString()+"\n";
											break;
										}
									}
								}

							if (myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString() =="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString().Trim() ==""||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIMD").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIMD").ToString().Trim()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - ������������ �������� � ����" 
									: " - ������������ �������� � ����" )+" `�����`"+"\n" ;
							if(Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"PD").ToString())==-2)
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - ������������ �������� � ����" 
									: " - ������������ �������� � ����" )+" `�����`" ;
							sTxt3 += "\n"+par2;
			
						}
						break ;				
						#endregion			
					case "NSIOTDEL":
						#region �������� � ��� ������� �� �����������
				
						j = 0;
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							iE = Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"OTDEL").ToString());
							if (iE < 0) 	sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������  � ������ " 
												: " - ������������  � ����� " )+" ���="+iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt16(drN["OTDEL"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������ � ������" 
												: " - ���������� � ����� ")+" ���="+iE.ToString()+"\n";
											break;
										}
									}
								}
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString().Trim()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - ������������ �������� � ����" 
									: " - ������������ �������� � ����" )+" `�����`"+"\n" ;
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"KOD").ToString()=="*")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - ������������ �������� � ����" 
									: " - ������������ �������� � ����" )+" `����`" ;
							sTxt3 += "\n"+par2;

						}
						break ;				
						#endregion			
					case "R_ROADRESP":
						#region �������� � ��� �������������(�� ������� ���������,...) ��� �����������
						j = 0;
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							iE = Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"POSITION").ToString());
							if (iE < 0)  sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������ ��������" : " - ������������ ��������" )
												+ " � ���� �  = " +iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt16(drN["POSITION"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - ������������" : " - ����������")
												+ " � ���� � = "+iE.ToString()+"\n";
											break;
										}
									}
								}
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"FIO").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"FIO").ToString().Trim()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - ������������ �������� " : " - ������������ �������� ")
									+ " � ���� `ϲ�`"+"\n" ;
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"POSADA").ToString()=="*" ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"POSADA").ToString()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - ������������ �������� " : " - ������������ �������� ")
									+" � ���� `������`" ;
							sTxt3 += "\n"+par2;
		
						}
					
					
						break ;				
						#endregion			
					case "ERRADD":
						#region ����� ������ ��� ���������� ������
						sTxt1 =(RDA.RDF.sLang=="R") ? "������ ��� ���������� ������": "������� ��� �������� ������";
							
						break ;	
						#endregion			
					case "ERRMODI":
						#region ����� ������ ��� ��������� ������
						sTxt1 =(RDA.RDF.sLang=="R") ? "������ ��� ��������� ������" : "������� ��� ��� ������";
						sTxt2 = (RDA.RDF.sLang == "R") ? "���� ������ " + myKey.ToString() + " ������������ � ������ ��������"
								     			: "���� ������ " + myKey.ToString() + " ��������������� � ����� ��������";
						break;	
						#endregion			
					case "ERRDEL":
						#region ����� ������ ��� �������� ������
						sTxt1 =(RDA.RDF.sLang=="R") ? "������ ��� �������� ������": "������� ��� �������� ������";
						sTxt2 =(RDA.RDF.sLang=="R") ? "���� ������ "+myKey.ToString()+" ������������ � ������ ��������" 
											: "���� ������ "+myKey.ToString()+" ��������������� � ����� ��������";
						
						break ;	
						#endregion		
					case "ERRCELL":
						#region ����� ������ � ������ ������
						sTxt1 = RDA.RDF.sLang =="R" ? "������ � ���� ������": "������� � ���� ������";
						sTxt2 = RDA.RDF.sLang =="R" ? " - ������������ �������� � ���� " 
											: " - ������������ �������� � ���� " ;
						sTxt3 += "\n\n"+par2;
						break ;	
						#endregion			

					default: 
						sTxt2 = "" ;
						break ;				
				}
				#endregion			

				#region  ������������ ��������� �� ������
				if(sTxt2 != "")
				{
					bNsiErr = true;  // ������� ������� ������ � ������ Ds_Buffer 
					ERR.Error err6 = new ERR.Error(sTxt1, ERR.ErrorImages.Examplemation, sTxt2 + sTxt3, null, 1);
					err6.ShowDialog();
				}			
				#endregion
			}
			catch
			{
			}

			return(bNsiErr);
		}

		// p_CtrlRowNsi - �������� �� ��������� ������ ��� � Ds_Buffer
		private void p_CtrlRowNsi()
		{
			//-------------------------------------------------------------------------------------
			// p_CtrlRowNsi - ����� ������������ ��� �������� �� ��������� ������ ��� � Ds_Buffer
			//-------------------------------------------------------------------------------------
			if(tRow >- 1 && RowOld != "" && bCtrlUpd == false)
			{
				try
				{
					RowNew      = "";   
					DataRow drC = Ds.Tables["Ds_Buffer"].Rows[tRow]; // ��������� ������ 
					RowNew      = string.Concat(@drC.ItemArray);     //  � �������� ��������� 
					if (@RowOld != @RowNew )                         // ���� ������ �������� :
					{
						if (drC["RMODI"].ToString()!="I")            // - � ���� ������ �� �����, ��
							drC["RMODI"] = "U".ToString();           //     ��������� �������� ="U"-��������,

						bB_Save.Enabled = true;                      // A����������� ������ : "�������� ����" 
						bUpdBuff = true;                             // - ��������� ������� ���������
						p_FooterBarInfo(drC);                        // ���������� � ������� ������� ������ 
					}
					bCtrlUpd = true; // ������� ���������� �������� �� ��������� ������ � Ds_Buffer 
				}
				catch
				{
				}
			}
		}

		// p_NewOtdel - ����� ������ �� NSIOtdel  ��� ����������� �\��� ��������� ������ ������� �� �����������
		private void p_NewOtdel()
		{	
		//-------------------------------------------------------------------------------------		
		// p_NewOtdel - ����� ������������ ��� ������� �� NSIOtdel ������ ������� �� �����������
		//		�������������� ���� ��� Ds_������� : 
		//	-rModi - ������� ������: ="I"-�����, ="U"-��������, ="S"-��� ���������
		//  -OldKey- ������ ��� ������ ��� ���������� � �� 	
		//	-rKey  - ������� ��������� ����� ��� �������� ������ : =0-���������,=1-��������� 
		// 	
		//   ��� rKey  ������� f_IsUsedKeyOtdel(a_Road, a_Otdel) ���������� 1, ���� �������� 
		// ����: a_Road,  a_Otdel ������������ ������� NsiRoad ������� � ������e R_RoadResp
		// ��� ��� ������ a_Otdel=100,101,102 ������� �  ������e NsiOtdel 
		//-------------------------------------------------------------------------------------		
			myErr  = ""; 
			try   
			{
				OraDABuffer = new OracleDataAdapter(@" SELECT 0 as OldKey,'S' as rModi, "
					+ @" RD.f_IsUsedKeyOtdel("+ myRoad.ToString()+", t.Otdel) as rKey, "
					+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser,"
					+ @" t.* FROM NsiOtdel t  WHERE t.Road=" + myRoad.ToString()
					+ @" ORDER BY Road, Otdel", RDA.RDF.BDOraCon); 
			
				myView = advBandedGridView3;
				myView.GroupPanelText="�Ĳ ����� " ;
				
				myView.GroupPanelText +=" �� " + rI_Road.GetDataSourceValue("NAIMD",idxRoad).ToString();
					
				//   - ���������� � �������� ��������
				myView.GroupPanelText +=
					rI_Road.GetDataSourceValue("PD", idxRoad).ToString() == "1" ? " ��������" : "";
					
				p_ViewPage1() ; // ���������� �� ������ ���(myView=advBandedGridView3) 
			}
			catch 	{}
		}

		// p_NewResp - ����� ������ �� R_RoadResp ��� ����������� �\��� ��������� ������ ������������� ��� ������
		private void p_NewResp()
		{	
			//-------------------------------------------------------------------------------------		
			// p_NewResp-����� ������������ ��� ������� �� R_RoadResp ������ ������������� ��� ������
			//		�������������� ���� ��� Ds_������� : 
			//	-rModi - ������� ������: ="I"-�����, ="U"-��������, ="S"-��� ���������
			//  -OldKey- ������ ��� Position ��� ���������� � �� 	
			//	-rKey  - ������� ��������� ����� ��� �������� ������ : =0-���������, =1-���������
			//        
			//   ��� rKey  ������� f_IsUsedKeyResp(a_Road, a_Otdel,a_Position) ���������� 1,  
			// ���� �������� ����: a_Road, a_Otdel, a_Position ������������ ������� R_RoadResp
			// ������� � � �������� ������e Doc_Resp_Arh
			//-------------------------------------------------------------------------------------		
			try
			{
				OraDABuffer = new OracleDataAdapter(@" SELECT 0 as OldKey,'S' as rModi, "
					+@" RD.f_IsUsedKeyResp("+ myRoad.ToString()+", "+ myOtdel.ToString() + ",t.Position ) as rKey, "
					+@"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser, t.* FROM R_RoadResp t"
					+@" WHERE t.Road ="+myRoad.ToString()+" AND t.Otdel="+myOtdel.ToString()
					+@" ORDER BY Road, Otdel,Position", RDA.RDF.BDOraCon); 
				myView = advBandedGridView4;
		
				myErr =(myOtdel > -1)? "" : "OTDEL"; // ��� ��������� �� ���������� ������ � NsiOtdel
				myView.GroupPanelText = "������ ������������ ��� ����� ";
				myView.GroupPanelText += (myOtdel > -1) ? rI_Otdel.GetDataSourceValue("KOD", idxOtdel).ToString() : "***";
				myView.GroupPanelText += " �� " + rI_Road.GetDataSourceValue("NAIMD", idxRoad).ToString();
				//   - ���������� � �������� ��������
				myView.GroupPanelText +=
					rI_Road.GetDataSourceValue("PD", idxRoad).ToString() == "1" ? " ��������" : "";

				p_ViewPage1() ; // ���������� �� ������ ��� myView=advBandedGridView4 
			}
			catch 	{}

		}

		// p_NewNsi - ����� ������ ��� � Ds_Buffer ��� ����������� �\��� ��������� ������ 
		private void p_NewNsi()
		{	
			//-------------------------------------------------------------------------------------		
			// p_NewNsi - ����� ������������ ��� ���������� Ds_�������(Ds_Buffer) ������� �� 
			//			  Oracle_������� ���.  ����������� ��� ���������� �� ������ bE_NDI � ����� 
			//			  ������������ ����� ������ --> Convert.ToInt16(bE_NDI.EditValue.ToString().
			//		�������������� ���� ��� Ds_������� : 
			//	-rModi - ������� ������: ="I"-�����, ="U"-��������, ="S"-��� ���������
			//	-rKey  - ������� ��������� ����� ��� �������� ������ : =0-���������, 
			//            =1-���������, ��� ��� ���� ������������ � ������ �������� 
			//-------------------------------------------------------------------------------------		
			try
			{
				switch (myTabl)
				{
					case "GRUSER":
 						#region �Ĳ ����� �������������
						//---------------------------------------------------------------------		
						//   ������� f_IsUsedKeyGrUser(a_Gr) ���������� 1, ���� �������� ����
						// a_Gr ������������ ������� GrUser ������� ���� �� � ����� �� �������� 
						// ������ : NsiRoad, R_GrUser
						//----------------------------------------------------------------------		

						OraDABuffer = new OracleDataAdapter(@" SELECT  0 as OldKey,'S' as rModi, "
							+ @" RD.f_IsUsedKeyGrUser(t.Gr) as rKey, "
							+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser,"
							+ @" t.* FROM GrUser t  ORDER BY Gr", RDA.RDF.BDOraCon);
						myView = advBandedGridView2; // � myView �������� ��������� ������ ... AdvBandedGridView ��� �Ĳ ����
						p_ViewPage1();              // ��������� �� ������(grid1) ���(myView=advBandedGridView*) 
						break;
						#endregion

					case "NSIROAD":
						#region �Ĳ ���������
						//---------------------------------------------------------------------		
						//   ������� f_IsUsedKeyRoad(a_Road) ���������� 1, ���� �������� ����
						// a_Road ������������ ������� NsiRoad ������� ���� �� � ����� �� 
						// �������� ������ : AcUser, NsiOtdel, NsiDeptRoad
						//----------------------------------------------------------------------		

						OraDABuffer = new OracleDataAdapter(@" SELECT 0 as OldKey,'S' as rModi,"
							+ @" RD.f_IsUsedKeyRoad(t.Road) as rKey, "
							+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser,"
							+ @" t.* FROM  NsiRoad t WHERE t.Road > -1 ORDER BY Road", RDA.RDF.BDOraCon);
						myView = advBandedGridView1;
						p_ViewPage1(); // ��������� �� ������(grid1) ���(myView=advBandedGridView*) 
						break;
						#endregion

					case "NSIOTDEL":
						#region �Ĳ ����� (p_ReadRoad--> p_NewOtdel) �� ����������

						p_LoadRoad(); // ������� ������ �������(���� p_NewOtdel)
						break;
						#endregion			

					case "R_ROADRESP":
						#region �Ĳ ������������ ���(p_ReadRoad--> p_ReadOtdel--> p_NewResp) �� ������� �����������

						p_LoadRoad(); // ������� ������ ������������ ��� (���� p_NewResp)
						break;
						#endregion
					default:
						break;
				}
		
			}
			catch { }
		}

		// p_ViewPage1 - ����� ������������ ��� ����������� ���������������� ��� 
		private void p_ViewPage1()
		{
			//-------------------------------------------------------------------------------------
			// p_ViewPage1 - ����� ������������ ��� ����������� �� ������ ����(advBandedGridView*) 
			//		 	     ���������������� ���, ������������ � Ds_Buffer �  ��������� �������� 
			//			     bEditTabl=true - ���������� �� �������������� ������� � �� ����� ��. 
			//-------------------------------------------------------------------------------------
			//  ����������. 
			//  1. ��������� Oracle_������ GRUSER ��� NSIROAD ����� ��������� ������ �������������
			//     ��(myAccess=ALL) � ������ �� ������� ��(RoadServ=5). 
			//
			//  2. ��������� Oracle_������ NSIOTDEL ��� R_ROADRESP ����� ��������� ��� �������������
			//     ��(myAccess=ALL) ��� �������������(myAccess= "ADMIN") ������(RdaFunc.RoadUser =
			//     myRoad). ��������� ��������� � �� ����� �� ��� �� ������� �� ��� � �������� ����� 
			//     ������ ������������� ������ ����� ������ ��������� ������ ��� ����� ������.
			//
			//	3. myView = advBandedGridView* - ������ ��������� ������...AdvBandedGridView
			//
			//	4. � ���� "RKEY" ��������������� ������� �� ��������� ����� ��� �������� ������ : 
			//     =0-���������, = 1-���������(��� ��� ���� ������������ � �������� ��������) 
			//-------------------------------------------------------------------------------------
			bUpdBuff        = false; // ��������� ������� - ��� ��������� � Ds_Buffer
			bB_Save.Enabled = false; // ������������� ������ : "�������� ����" 
			string tKey = "";
			try
			{
				#region ���������� Ds_Buffer � ��������� ��������(bEditTabl) �������-���������� �� �������������� �������  


				grid1.DataSource=null;            // �������  ������ ����������� � �����
				
				Ds.Tables["Ds_Buffer"].Dispose(); // ��������� ��� ������� 
				Ds.Tables["Ds_Buffer"].Clear();   // �������   ��� ������ � �������    
				OraDABuffer.Fill(Ds,"Ds_Buffer"); // ��������  Ds_������� (Ds_Buffer) ������� ���

				//if ((myAccess  == "ALL" && RDA.RDF.UserParam.RoadServ == 5 && (myTabl == "GRUSER" || myTabl == "NSIROAD"))
				//||  ((myAccess == "ALL" || (myAccess == "ADMIN"  && myRoad == RdaFunc.RoadUser))
				//&&  (myTabl == "NSIOTDEL" || myTabl == "R_ROADRESP"))) 


				if (((myTabl   == "GRUSER" || myTabl == "NSIROAD")    &&      // ��� ����� � ����� ����� ������������� 
				     (myAccess == "ALL" && RDA.RDF.UserParam.RoadServ == 5))  //  ����� �� � ������ �� ������� ��

				||  (( myTabl    == "NSIOTDEL" || myTabl == "R_ROADRESP") &&  // ��� ������� � ������������� �� ������� ����� ������������� :
					 (  myAccess == "ALL"    ||                               // - ����� ��
			          ( myAccess == "ADMIN"  ||                               // ������������� �� ������
			           (myAccess == "USER" && RDA.RDF.UserParam.Otdel == 102) //  ��� ������������ ������ ������ 102 -����
					  )          && myRoad == RDA.RDF.UserParam.RoadServ      //  � ������ �� ������� ������
					 )
					)
				   ) 

					bEditTabl = true;    // ������� ���������� �� �������������� �������

				else
					bEditTabl   = false;   // ������� ������� �� �������������� �������

				#endregion			

				if ( Ds.Tables["Ds_Buffer"].Rows.Count > 0 )   // ���� ���� ������ � ������� 
				{
					if (bEditTabl)                           //      � ��������� ���������  
					{
						#region  ������ ����� ������ � ���� OLDKEY
						tKey = (myTabl == "GRUSER" ? "GR"           // ��� �Ĳ ����� 
							 : (myTabl == "NSIROAD" ? "ROAD"        // ���  �Ĳ ���������
							 : (myTabl == "NSIOTDEL" ? "OTDEL"      // ��� �Ĳ ����� �� ����������
							 : (myTabl == "R_ROADRESP" ? "POSITION" // ��� �Ĳ ������������ ��� �����������
							 : ""))));
						foreach (DataRow dr in Ds.Tables["Ds_Buffer"].Rows)
						{
							dr["OLDKEY"] = dr[tKey];
						}

						Ds.Tables["Ds_Buffer"].AcceptChanges(); // ��������� ������� ������� � Unchanged 
						#endregion
					}
				}
				else p_ErrRead(myErr); //  �����, ������� �� ���������� ������

			}
			catch(Exception ex)
			{
				ERR.Error err6 = new ERR.Error("�������", ERR.ErrorImages.Examplemation, "?????", ex.ToString(), 1);
				err6.ShowDialog() ;
			}
			this.grid1.MainView   = myView;   // ��������� ��� ��� �������(MainView)
			this.grid1.DataSource = Ds.Tables["Ds_Buffer"];// � ��������� ������ � �����
			// ������������� ������ ����� �������� �� ����� ...View...
			gridView_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

			p_NSDClick();                     // ��\���������� ������: sB_Save � �.�.
		}

		// p_RowFocus - ����� ������������ ����� ��������� ������ �� ������ � grid1(...View1-4)
		private void p_RowFocus()
		{
			//-------------------------------------------------------------------------------------
			// p_RowFocus - ����� ������������ ����� ��������� ������ �� ������ � grid1(...View1-4)
			//              ��� �������\���������� ��������� ������ �� ���� ������  
			//
			// ������������ c��������� ��� ����������� ������������� :
			//    - � rKey  - �������� �������\���������� ��������� ����� ��� �������� ������:
			//              = 0-���������, =1-���������, �.� ���� ������������ � ������ �������� 
			//    - � rModi - �������� ������ : ="I"-�����, ="U"-��������, ="S"-��� ���������
			//    - � RowObj- ������� �������� ����� ��������� ������ �� � ���������
			//    - � RowOld- ������  �������� ����� ��������� ������ �� � ���������
			//
			// ���������� � ������: gridView_FocusedRowChanged, sB_AddRow_Click � sB_DelRow_Click 
			//                      ��� �������������� ��������� �������� tRow-������ ������
			//-------------------------------------------------------------------------------------

			RowOld = "";
			RowNew = "";
			if (tRow >-1)
			{
				OldTabl= myTabl;                  // �������� ��� �������������� ������� 
				RowNew = "";   
				myKey  = "" ;
				DataRow dr1 = Ds.Tables["Ds_Buffer"].Rows[tRow];
				p_FooterBarInfo(dr1);              // ���������� � ������� ������� ������ 

				rKey = Convert.ToInt16(dr1["RKEY"].ToString());
				RowObj = dr1.ItemArray;          // ������ ��������� ������ ������ ��� ��������������
				RowOld = string.Concat(@RowObj); // �������� c����� ������ ��� ���������
	
				#region ���� ����������� ������� �� ��������� ����� ��� �������� ������ � ���

				try
				{
					switch (myTabl)
					{
						case "GRUSER":
							#region �Ĳ ����� �������������
							myKey = "GR = " + dr1["GR"].ToString();          // ��� ������
							if (rKey == 1)                                   // �.�. ���� GR ������������,��
								 this.v2C1.OptionsColumn.AllowFocus = false; //  ��������� �������� ��� ��������� GR
							else this.v2C1.OptionsColumn.AllowFocus = true;  //   ����� ���������
							break;
							#endregion

						case "NSIROAD":
							#region �Ĳ ���������
							myKey = "ROAD = " + dr1["ROAD"].ToString();      // ��� ������
							if (rKey == 1)                                   // �.�. ���� Road ������������,��
								 this.v1C1.OptionsColumn.AllowFocus = false; //  ��������� �������� ��� ��������� Road
							else this.v1C1.OptionsColumn.AllowFocus = true;  //  ����� ���������
							break;
							#endregion

						case "NSIOTDEL":
							#region �Ĳ �����  �� ����������
							myKey = "OTDEL = " + dr1["OTDEL"].ToString();    // ��� ������
							if (rKey == 1)                                   // �.�. ���� Road ������������,��
							{
								this.v3C2.OptionsColumn.AllowFocus = false; //  ��������� �������� ��� ��������� Otdel
								if (dr1["OTDEL"].ToString()=="100" || dr1["OTDEL"].ToString()=="101" || dr1["OTDEL"].ToString()=="102")
									 this.v3C1.OptionsColumn.AllowFocus = false;  // ���� Kod-> ���� ������� ���(100),���(101), ����(102) �������� ������ 
								else this.v3C1.OptionsColumn.AllowFocus = true; 
							}
							else 
							{
								this.v3C2.OptionsColumn.AllowFocus = true;   //  ����� ���������
								this.v3C1.OptionsColumn.AllowFocus = true; ; //  ���� ������� 100(���),101(���), 102(����) �������� ������ 
							}
							break;
							#endregion

						case "R_ROADRESP":
							#region �Ĳ ������������ ��� �� ������� �����������
							myKey = "POSITION = " + dr1["POSITION"].ToString();    // ��� ������������ ��� 
							if (rKey == 1)                                   // �.�. ���� Road ������������,��
								 this.v4C5.OptionsColumn.AllowFocus = false; //  ��������� �������� ��� ��������� Road
							else this.v4C5.OptionsColumn.AllowFocus = true;  //  ����� ���������

							break;
							#endregion
						default:
							break;
					}
				}
				catch { }

				#endregion

			}
		}

		// gridView_FocusedRowChanged - �����  ������������ ����� ��������� ������ �� ������ 
		private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// gridView_FocusedRowChanged - �����  ������������ ����� ��������� ������ �� ������ 
			//                              � grid1(...View1-8) 
			//   � tRow - ����������� ����� ������
			//-------------------------------------------------------------------------------------
			bCtrlUpd  = false; // ������� ���������� �������� �� ��������� ������ � Ds_Buffer 
		
			if(myView.FocusedRowHandle >-1)
			{
				tRow  = Convert.ToInt16((myView.GetSelectedRows()).GetValue(0).ToString());
				
				p_RowFocus();
			}
		}

		// gridView_BeforeLeaveRow - ����� ������������ �EPE� ������� ������ � ������ ����� � 
		private void gridView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// gridView_BeforeLeaveRow - ����� ������������ �EPE� ������� ������ � ������ ����� � 
			//                           ���� ������ �� �������, � �������� ��� ���������, �� :
			//  -��� ���������� ������ ��������������� �������(bUpdBuff=true) ��� ���������� ������
			//	-�����, e.Allow = false ��� ������ �������� �� ��������� ������           
			//-------------------------------------------------------------------------------------
			try
			{
				string txt2=(RDA.RDF.sLang=="R") ? "������� ����� �  ������ !"	: "³����� ����� � ������ !";
				if (f_ErrRowNsi(myTabl, txt2)) 	// ���� � ������ ���� ������, �� ���������  
					e.Allow = false ;           //             bNsiErr=true � ������� ������� 
				else                            // �����, e��� ���� ��������� � �������(tRow)
					p_CtrlRowNsi();             //        ������, �� ���������  bUpdBuff=true
			}
			catch
			{
			}
		}

		// gridView_CellValueChanged - ����� ������������ ����� ���������� ������ � ������ 
		private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// gridView_CellValueChanged - ����� ������������ ����� ���������� ������ � ������ ��� 
			//                             ��������� ��������(bCtrlUpd=false) ������������� ������  
			//                             ������ p_CtrlRowNsi - �������� �� ��������� ������
			//-------------------------------------------------------------------------------------
			bCtrlUpd = false;
		}

		// rItC_PZ_EditValueChanging - ���� � �������� ���������� �������� ���� PZ � ...View4  
		private void rItC_PZ_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rItC_PZ_EditValueChanging - ����� ������������ ��� ���������� ��������� ����������
			//                             �������� ���� PZ � ...View4(��� ������� R_RoadResp) 
			//                             � ���������� ���������� 
			// �������� �������� PZ: =0-�������������, =1- �����������, =2- ���� ���������� ��������
			//-------------------------------------------------------------------------------------
			try   // ���������� �������������� 1-�� ������ ���� � �����
			{
				int i = Convert.ToInt16(e.NewValue.ToString().Substring(0, 1));
				if (i >= 0 && i <= 2)	 // ���� 1-�� ������ - ����� �� 0 �� 2 
					e.NewValue = i;  //  �� ��������� ��� �����
				else
					e.Cancel = true;   // ����� �������� �������� �����
			}
			catch // ����� �������� �������� �����
			{
				e.Cancel = true;
			}

		}

		// rItC_PD_EditValueChanging - ���� � �������� ���������� �������� ���� PD � ...View1 
		private void rItC_PD_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rItC_PD_EditValueChanging - ����� ������������ ��� ���������� ��������� ����������
			//                             �������� ���� PD( 0 - 99) � ...View1(��� ������� NsiRoad)
			//                             � ���������� ����������
			//-------------------------------------------------------------------------------------
			try   // ���������� �������������� 1-�� ������ ���� � �����
			{

				int i = Convert.ToInt16(e.NewValue.ToString().Substring(0, 2).Trim());

				if (i >= 0 && i <= 99 && rItC_PD.Items[i].ToString() != null)
					e.NewValue = i;   // ���� 2 ������� - ����� �� 0 �� 99, �� ��������� ��� �����
				else
					e.Cancel = true;  // ����� �������� �������� �����
			}
			catch // ����� �������� �������� �����
			{
				e.Cancel = true;
			}
		}

		//gridView_CustomDrawCell - ��������e ������ � ������ ����� � grid*
		private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//gridView_CustomDrawCell-����� ������������ ��� ��������� ������ � ������ ����� �  grid*
			//-------------------------------------------------------------------------------------
			//e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;//�����-�����
			//e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal; //�����-����
			//e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;      //�����-�������
			//-------------------------------------------------------------------------------------
			string tFlag = myView.GetRowCellValue(e.RowHandle, "RMODI").ToString().Trim().ToUpper();
			switch (tFlag)
			{
				case "I":    // ���������� ������
					e.Appearance.BackColor = Color.Khaki;        // 1-�� ���� ������  
					e.Appearance.BackColor2 = Color.WhiteSmoke;   // 2-�� ���� ������ c����
					e.Appearance.ForeColor = Color.Blue;         // ���� ������ � ������ c����
					e.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
					break;
				case "U":    // ��������� � ������
					e.Appearance.BackColor = Color.Honeydew;           // ���� ������  
					e.Appearance.BackColor2 = Color.WhiteSmoke;
					e.Appearance.ForeColor = Color.Black;
					break;
			}

		}



		#endregion		
	
		#region ������, ��������� � ���������� ������� ��� �������������� ����� �����

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



		#endregion

		#region ������ ��������� �����, ��������� � ������� ��������� �� ������

		private void bB_Print_ItemClick(object sender, ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//B_Print_Click- ����� ������������ ��� ������� �� ������ ��������(B_Print) ��� ������ 
			//		         ���������, ������������ � ����� Excel_Output(��. p_XlsOut).
			//		         ��� ���� :Excel_�������� �� ������������(bVis=false), �� �������������
			//		                   �����������(bPrt=true) ���� ������� ��� ������ ��������. 
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// �������� ��������� ������ ��� ���������� ���������� ���������
			printableComponentLink1.CreateDocument();    // ���������� ������ ��������
			printableComponentLink1.CreateReportHeaderArea += new CreateAreaEventHandler(printableComponentLink1_CreateReportHeaderArea);//���������� ���������� ������� ��� ���������� ������� �������
			printableComponentLink1.ShowPreviewDialog();//���������� ������ ������
			

		}
	
		private void printableComponentLink1_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		//--------------------------------------------------------------------------------------------------------------
		// ����� CreateReportHeaderArea ���������� �� ����� ���������� �������� ����� ���������.
		//
		// ������������ ��� ������ ��������� ���������
		//---------------------------------------------------------------------------------------------------------------
			string txt="";//���������� ��� �������� ��������� �������
			
			txt=myView.GroupPanelText;  //���������� ��� ������
			e.Graph.Font=new Font("Times New Roman",14,FontStyle.Bold | FontStyle.Italic);//������������� �����
			e.Graph.DrawString(txt, Color.BlueViolet, new Rectangle(0,0,(int)e.Graph.ClientPageSize.Width,30), BorderSide.None);//������ ����� �� �����	
		}

		#endregion			
		
		#region �������� tooltip ��� ����������� ������ ��, �����, ��������� �� ��������� ����� grid1(...View1,...View4) 

		private void p_ListAC()
		{
			//-------------------------------------------------------------------------------------		
			// p_ListAC - ����� ������������ �������� ������ �� �� LIST_AC � rICBE_AC_�����������
			//            ���� LIST_AC. ������������ ��� ��������� NsiRoad ��� ����������� ToolTip
			//            ��� ��������� ����� �� ���� LIST_AC
			//-------------------------------------------------------------------------------------		

			string txt = "";
			ToolAC = "";
			rICBE_AC.Items.Clear();
			try
			{
				using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM LIST_AC ORDER BY IDAC", RDA.RDF.BDOraCon))
				{
					using (DataTable dt = new DataTable())
					{
						oraDA.Fill(dt);

						if (dt.Rows.Count > 0)
						{
							foreach (DataRow dr in dt.Rows)
							{
								rICBE_AC.Items.Add(dr["IDAC"].ToString());
								txt += dr["IDAC"].ToString() + " - " + dr["SHIFR"].ToString();
								ToolAC += txt;    // ToolTip ��� ������ �� � ��� �������� ��� �������� ����� � ������� LIST_IDAC 
								txt = "\n";
							}
						}
					}
				}
			}
			catch { }

		}

		// p_ListGr - �������� ������ ����� ��� ����������� ToolTip ��� ��������� ����� �� ���� PD-...View1
		private void p_ListGr()
		{
			//-------------------------------------------------------------------------------------		
			// p_ListGr - ����� ������������ �������� ������ ����� �� GrUser � rItC_PD_�����������
			//            ���� PD. ������������ ��� ��������� NsiRoad ��� ����������� ToolTip
			//            ��� ��������� ����� �� ���� PD
			//-------------------------------------------------------------------------------------		

			string txt = "";
			ToolGr = "";
			rItC_PD.Items.Clear();
			try
			{
				using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM GrUser ORDER BY Gr", RDA.RDF.BDOraCon))
				{
					using (DataTable dt = new DataTable())
					{
						oraDA.Fill(dt);

						if (dt.Rows.Count > 0)
						{
							foreach (DataRow dr in dt.Rows)
							{

								rItC_PD.Items.Add(dr["GR"].ToString() + " - " + dr["NAIM"].ToString());
								txt += dr["GR"].ToString() + " - " + dr["NAIM"].ToString();
								ToolGr += txt;    // ToolTip ��� ������ � �� �������� ��� �������� ����� � ������� PD ...View1
								txt = "\n";
							}
						}
					}
				}
			}
			catch { }

		}

		// HideHintTool - �������e ToolTip ��������������� �������
		private void HideHintTool()
		{
			//-------------------------------------------------------------------------------------
			// HideHintTool - ����� ������������ ��� �������� ToolTip ��������������� �������
			//                                 
			// ���������� � ������: ...View*_MouseMove
			//-------------------------------------------------------------------------------------
			toolTipController1.AutoPopDelay=0;    // ������������� ��� �������� � 0 
			toolTipController1.ReshowDelay =0;    // ��� ������������ ������� tooltip
			toolTipController1.InitialDelay=0;    
			toolTipController1.HideHint();        // � ��������� tooltip 
			// ���������� ����������� �������� �������� :
			toolTipController1.AutoPopDelay=5000; // - ����� �������� ������ tooltip
			toolTipController1.ReshowDelay =500;  // - �����, ������� ������ ������ ������ ��� ����� ������� ����� tooltip
			toolTipController1.InitialDelay=100;  // - ����� �������� ����� ��������������(�������) ������ ������� 
		}

		// View_MouseMove - ������������ ����������� ��������� � ���� ��� �������� ������� � ������� ��������������  
		private bool View_MouseMove(int lX, int lY, int wX, int hY, string tool, string titl, System.Windows.Forms.MouseEventArgs e)
		{
		//---------------------------------------------------------------------------------------
		// ...View_MouseMove - ����� ���������� ��� �������� ������� � ������� ��������������
		//			           ��� ����������� ��������� � ����  
		//	��������� : lX - X(��������������)_���������� ������ �������� ���� �������������� 
		//		        lY - Y(������������)_����������   ������ �������� ���� ��������������
		//		        wX - ������(Width)  �������������� 
		//		        hY - ������(Height) ������������� �������: 
		//                   > 0- ��������;  = 0-�������������  
		//---------------------------------------------------------------------------------------
			Point pT= new Point(e.X, e.Y);   // ������� ����� � ����� ������������ ������� 
			if (hY == 0)                     // ���������� ������ ������������� �������  �� :
				hY = (myView.RowCount                         // -���������� ����� � myView
					*(myView.Appearance.Row.FontHeight        // -������  ������ � ������
					+(myView.Appearance.Row.FontHeight        
					-(int)myView.Appearance.Row.Font.Size))); // -������� ������ � ������
						
			Rectangle r1= new Rectangle(lX,lY,wX,hY); // ���������� ���������� ������������� �������
			bool isIn=r1.Contains(pT);
			if(isIn)             // ��� ��������� ������� � ������������� ������� �������������:
			{
				toolTipController1.AutoPopDelay=5000; // - ����� �������� ������ tooltip
				toolTipController1.ReshowDelay =5000; // - �����, ������� ������ ������ ������ ��� ����� ������� ����� tooltip
				pT.X =Cursor.Position.X;
				pT.Y =Cursor.Position.Y;
				toolTipController1.ShowHint(tool, titl, pT);
			}
			return isIn;
		}

		private void advBandedGridView1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		//---------------------------------------------------------------------------------------
		// ...View1_MouseMove-����� ���������� ��� �������� ������� � ������� advBandedGridView1
		//			          �Ĳ ����������� ��� ����������� ��������� � ������� "PD"-��� ������ 
		//---------------------------------------------------------------------------------------
			//this.Text = "�� �����������: X= " + e.X.ToString() + "  �� ���������: Y=" + e.Y.ToString(); // ���������� �������

			bool isoneIn = false;
			if (myView == advBandedGridView4)
			{
				isoneIn = View_MouseMove(916, 106, v4C4.Width,0,
				 "0 -������������� �� ��������\n1 -�����������\n2 -���� ���������� ��������", "������ ", e) ? true : isoneIn;
			}
			if (myView == advBandedGridView1)
			{
				isoneIn = View_MouseMove(503, 126, v1C5.Width, 0, ToolGr,"�����", e)    ? true : isoneIn;
				isoneIn = View_MouseMove(503, 48, v1C5.Width, 75, v1C5.ToolTip,"",e)    ? true : isoneIn;
				isoneIn = View_MouseMove(540, 126, v1AC.Width, 0, ToolAC,"C����� ��",e) ? true : isoneIn;
				isoneIn = View_MouseMove(540, 48, v1AC.Width, 75, v1AC.ToolTip,"", e)   ? true : isoneIn;

				isoneIn = View_MouseMove(540 + v1AC.Width, 48, v1C6.Width, 75, v1C6.ToolTip, "", e) ? true : isoneIn;
			
			}
			if (!isoneIn)
				HideHintTool();
		}


		#endregion

		#region �������, �������������� ��������� � ��������� ��������� �������� �������


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

		// p_LanghInfo - ��������� � ������� � ����� �� ����� 
		private void p_LanghInfo()
		{
			//-------------------------------------------------------------------------------------
			// p_LanghInfo - ����� ������������ ��� ������ ��������� � ���������  � ����������� 
			//                 �� ���������� ����� : RDA.RDF.RDA.RDF.sLang = U-����������, =R-�������
			//-------------------------------------------------------------------------------------
			try
			{
				#region ���������(ToolTip) �� ���������� �����

				if (RDA.RDF.sLang == "U")   // ����� ����������� �����
				{
					bB_Save.Hint    = "    CTRL+S :  �������� �� �����i, �������  ��  ����� ������ � �Ĳ";
					bB_Print.Hint    = "     �������� ��  ����  �����";
					bB_AddRow.Hint    = "    �������� ����� �����";
					bB_CancelEditRow.Hint    = "    ��������� ��������� ���� ��� ���� �������� ������";
					bB_DelRow.Hint    = "    CTRL+DELETE :  �������� ����� � �Ĳ";
					bB_Exit.Hint     = "    Alt+F4 :  �������� �����";
					bE_NDI.Hint      = "    ������� �Ĳ  ���� ������� �����������  ��� �����������";
					bE_Road.Hint     = "    ������� �������� �� ��� ���������� ����";
					bE_Otdel.Hint    = "    ������� ���� � ����� ������� ����������� ��� ����������� ������������ ���";

					v1C5.ToolTip = "����� �� ��� ���������� ���i�����";
					v1AC.ToolTip = "������(� ������� LIST_AC) ID_���� � ������: RD,NF ! RD ! NF..., �� �������� �� �� ������� ���i�����";
					v1C6.ToolTip = "���������  ��� ������ ��������� ����������� : 005RRRR00000000 (�.� (005*10000+Road)*100000000)";
					l_rKey1.Text = "��������� ������� ����, �� ��������������� � ����� ��������.";  //��� rKey =1
					l_rKey2.Text = "���. ���� ";
					
				}
				#endregion

				#region ���������(ToolTip) �� ������� �����

				else
				{
					bB_Save.Hint      = "    CTRL+S :  ��������� ��� �����������, ��������� � ���������� ������ � ���";
					bB_Print.Hint     = "    �������� � ������ ������";
					bB_AddRow.Hint    = "    ������� ����� ������";
					bB_CancelEditRow.Hint    = "    �������� ���������� ����� ��� ��������� ��������� ������";
					bB_DelRow.Hint    = "    CTRL+DELETE :  ������� ������ �� ���";
					bB_Exit.Hint     = "    Alt+F4 :  ������� �����";
					bE_NDI.Hint      = "   �������� ���, ������� ��������� ����������� ��� ��������";
					bE_Road.Hint     = "    �������� ������, � ������� ��������� ������";
					bE_Otdel.Hint    = "    �������� �����, � ������� ��������� ����������� ��� �������� ������������� ���";
					
					v1C5.ToolTip = "������, � ������� ��������� ������";
					v1AC.ToolTip = "������(�� ������� LIST_AC) ID_����� � ����: RD,NF ! RD ! NF..., ����������� ����� �� �������� ������";
					v1C6.ToolTip = "���������� ��� ������ ����������� ������������� : 005RRRR00000000 (�.� (005*10000+Road)*100000000)";
					l_rKey1.Text = "������ �������� ���� ������������ � ������ ��������. "; //��� rKey =1
					l_rKey2.Text = "c�. ���� ";

				}
				#endregion
				DataRow dr1 = Ds.Tables["Ds_Buffer"].Rows[tRow]; // ��������� ������ 

				p_FooterBarInfo(dr1); // ���������� �� ������ ������ � ���������� 

			}
			catch { }

		}

		// p_FooterBarInfo - ���������� � ������������ ��������� � ������� ������ 
		private void p_FooterBarInfo(DataRow drN)
		{
			//-------------------------------------------------------------------------------------		
			// p_FooterBarInfo - ����� ������������ ����������� ���������� � ��������� � ���������
			//                   ������, ������� ������ � ������ � ��������� ��������� ������
			//
			// ���������� � ������: p_LanghInfo, 
			//-------------------------------------------------------------------------------------	
			string tCapt = "����� ";
			string tHint = "";
			//rIt_LeftFont.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;      //�����-�������
			//rIt_LeftFont.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;      //�����-�������

			rIt_LeftFont.Appearance.BackColor = Color.Transparent;   // 1-�� ���� ������  
			rIt_LeftFont.Appearance.BackColor2 = Color.Transparent;  // 2-�� ���� ������ c����
			rIt_LeftFont.Appearance.ForeColor = Color.Black;         // ���� ������ � ������ 
			this.rIt_LeftFont.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			rIt_LeftFont.NullText = "";

			try   // ���� ���� RMODI ������������ � ������, �� �������  
			{
				tHint = (RDA.RDF.sLang == "R") ? " ����� ���������� ������ ������ �����   " : " ϳ��� ���������� ����� ����� ����   ";

				#region bEditTabl = true - ��������� ���������

				if (bEditTabl)
				{

					tCapt += RDA.RDF.sLang == "U" ? "�����������: " : "��������������: ";
					tHint = RDA.RDF.sLang == "R" ? " ����� ���������� ������ ������ ����� " : " ϳ��� ���������� ����� ����� ���� ";

					switch (drN["RMODI"].ToString())     //  ������ ������
					{
						case "S":                                //  ������ ��� ���������
							tHint = "";
							rIt_LeftFont.NullText = RDA.RDF.sLang == "R" ? " ������ ��� ���������" : " ����� ��� ���� ";
							break;
						case "U":                                //  ������     ��������
							tHint += RDA.RDF.sLang == "R" ? "��������   � �������." : "��I����   � �������.";

							rIt_LeftFont.Appearance.BackColor = Color.Honeydew;           // ���� ������  
							rIt_LeftFont.Appearance.BackColor2 = Color.WhiteSmoke;
							rIt_LeftFont.Appearance.ForeColor = Color.Black;
							rIt_LeftFont.NullText = RDA.RDF.sLang == "R" ? " � ������ ������� ���������" : " � ����� ������ ����";

							break;
						case "I":                                //  ������      �����
							tHint += RDA.RDF.sLang == "R" ? "��������� � �������." : "������ � �������.";
							rIt_LeftFont.Appearance.BackColor = Color.Khaki;        // 1-�� ���� ������  
							rIt_LeftFont.Appearance.BackColor2 = Color.WhiteSmoke;   // 2-�� ���� ������ c����
							rIt_LeftFont.Appearance.ForeColor = Color.Blue;         // ���� ������ � ������ c����
							rIt_LeftFont.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
							rIt_LeftFont.NullText = RDA.RDF.sLang == "R" ? " ����� ������ " : " ����� ����� ";

							break;
						case "D":                                //  ������      �������
							tCapt += RDA.RDF.sLang == "R" ? " ������ �������� �� �������� " : " ����� ���������� �� ���������";
							tHint += RDA.RDF.sLang == "R" ? "�������   �� �������." : "��������   � �������.";
							break;
						default:
							break;
					}

				}
				#endregion

				#region bEditTabl = false - ����������� ���������

				else
				{
					tCapt += (RDA.RDF.sLang == "U")
						? "���������: ���������� ������� ��� ���."
						: "���������: ������ ������ ��� ���������.";

					tHint = (RDA.RDF.sLang == "U")
						? "� ��� ���� ������� ��� ��� ��������� . ��������� �� ������������ ��."
						: "� ��� ��� ������� ��� ��������� ��������� . ���������� � �������������� ��.";
				}
				#endregion

				bS_InfoLeft.Caption = tCapt;
				bS_InfoLeft.Hint = tHint;

				bS_Info.Caption = RdaFunc.f_RowChangeInfo(drN); //.f_DocChangeInfo((drN).f_DocChangeInfo(drN);        // ���������� �� ��������� ������
				bS_Info.Hint = (RDA.RDF.sLang == "U")
								? "  ���� �������� ���� ������, ϲ� �����������(�� ��� ����) �� IP_��"
								: "  ���� ���������� ��������� ������, ��� ������������(�������� ���������) � IP_��";

			}
			catch { } // ����� �������� ����� ������� ������ 

		}

		// f_IdRoad - ������� ���������� Id_������ ������ 
		private string f_IdRoad(int tRoad, int tIdTabl)
		{
			//-------------------------------------------------------------------------------------
			// f_IdRoad - ������� ���������� Id_������ ������(NSIROAD) ��� ���� ROADF  � ���� 
			//             005RRRR00000000=(((005*10000+tRoad)*100000000 
			//
			// ���=3-����� 005            - ��� ������� NsiRoad �� List_Table_AC
			//     4-�����    RRRR        - tRoad  - ��� ������(���� Road)      
			//     8-������       00000000- ���������������  
			//  �������� Id_���� ������� �� RDA: ���=Helpers.CommonFunctions.GetIDTable(myTabl)
			//-------------------------------------------------------------------------------------
			decimal i = -1;
			if (tRoad > -1)
			{
				i = Convert.ToInt64(00500 + tRoad);
				i = Convert.ToInt64(i * 10000000000);
			}
			return (i.ToString());
		}

		// f_ErrN - �������e ������� �� ���������� ���������� ������, ���� �� ��������� "��������" 
		private bool f_ErrN(string par1, string par2, string par3, string par4)
		{
			//-------------------------------------------------------------------------------------
			// f_ErrN -������� ���������� ��� �������� ������� �� ���������� ���������� ������,����
			//         �� ��������� "��������" �� ������ ������, �����������, ������������,... 
			//         ��� ��� �������� �����.
			//         ������������ ��������� sB_Save_Click-"�������� ����".
			//         ������� ���������� TRUE ��� Cancel, ���� ��������� �������� ����������. 
			// ��� ������: ���(Yes)-   ����������� ��������� � ����������� ��������� ��������
			//             ͳ(No)  - ������������� ���������, �� ����������� ��������� ��������
			//             ³����(Cancel) � ���������� ��������� ��������  
			// ��������� : par1-����� ��������,��������: "��������� �����������"
			//             par2-����� ��������������, ��������,"�� �����������... "...
			//             par3-�������������� ��������� � ����� ���������
			//             par4= ="BUFF" ��� bUpdBuff
			//
			// ���� RDA.RDF.sLang="R" - ������ ��������� �� �������("R") ����� -�� ����������("U") �����
			//-------------------------------------------------------------------------------------

			bool bUpd = false;
			string sTxt1 = "";
			string sTxt2 = "";
			string sTxt3 = "\r\n\r\n   " + par2;
			string sTxt4 = "\r\n\r\n" + RdaFunc.f_Repl(" ", 60);
			string sTxt5 = "\r\n���        - ";
			string sTxt6 = "\r\nͳ           - ";
			string sTxt7 = "\r\n³����  - ";
			string sTxt8 = par3 + "\r\n" + RdaFunc.f_Repl("-", 90);

			#region ������������ ������ ��������� �� �������(RDA.RDF.sLang="R") �����
			if (RDA.RDF.sLang == "R")
			{
				sTxt1 = "��������������";
				sTxt2 = "   ��������: " + par1;
				sTxt3 += " ���� ��������� .";
				sTxt4 += "���������   ��������� ?";
				sTxt5 += "��������� ��������� � ��������� ��������� ��������";
				sTxt6 += "�� ��������� ���������,�� ��������� ��������� ��������";
				sTxt7 += "�������� ��������� ��������";
			}
			#endregion

			#region ������������ ������ ��������� �� ����������(RDA.RDF.sLang="U") �����
			else
			{
				sTxt1 = "������������";
				sTxt2 = "   ĳ� : " + par1;
				sTxt3 += " ���� ��i��.";
				sTxt4 += "��������    ��i�� ?";
				sTxt5 += "������� ���� �� �������� ������� ��";
				sTxt6 += "�� �������� ����, ��� �������� ������� ��";
				sTxt7 += "������� ������� ��";
			}
			#endregion

			#region ��������� �� ���������� ��� � �������� ������� � ���������� ������ � Oracle_�������

			ERR.Error err4 = new ERR.Error(sTxt1, ERR.ErrorImages.Question, sTxt2 + sTxt3 + sTxt4, sTxt8 + sTxt5 + sTxt6 + sTxt7, 3);
			err4.ShowDialog();

			switch (err4.DialogResult)
			{
				case DialogResult.Yes:
					bUpd = false;
					bB_Save_ItemClick(null, null);// �������� ��������� 
					break;
				case DialogResult.No:
					bUpd = false;
					break;
				default:
					bUpd = true;            // ������� ��������� ��������
					break;
			}

			#endregion
			bUpdBuff = bUpd;
		
			l_rKey3.Focus();
			return (bUpd);
		}

		// p_ErrRead - ��������e �� ���������� ������ � �������� �������
		private void p_ErrRead(string par1)
		{
			//-------------------------------------------------------------------------------------
			// p_ErrRead-����� ������������ ��� ��������� �� ���������� ������ � �������� �������
			//
			// ��������� : par1-����� ��������� �� ���������� ������ � �������:
			//   par1="ROAD" (��� p_ReadRoad) -� NsiRoad  ��� �����������   
			//       ="OTDEL"(��� p_ReadOtdel)-� NsiOtdel ��� ������        � �������� �����������
			//       ="USER" (��� p_ReadUser) -� AcUser   ��� ������������� � ������   �����������
			//
			// ���� RDA.RDF.sLang="R" - ������ ��������� �� �������("R") ����� -�� ����������("U") �����
			//-------------------------------------------------------------------------------------
			// ����� : p_ErrRead("ROAD"); // ��������� �� ���������� ������ �� ������
			//-------------------------------------------------------------------------------------
			string sTxt1 = (RDA.RDF.sLang == "R") ? "������" : "�������";
			string sTxt2 = (RDA.RDF.sLang == "R") ? "����������� " : "³����� ";
			string sTxt3 = (RDA.RDF.sLang == "R") ? "��� ����� " : "��� �������� ";
			string sTxt4 = "\r\n\r\n" + (RDA.RDF.sLang == "R" ? "������� " : "������ ");
			string sTxt5 = (RDA.RDF.sLang == "R")
				? "��������� � ` �Ĳ` \r\n " + RdaFunc.f_Repl(" ", 20) + " � �������� ����� - "
				: "�������� � ` �Ĳ` \r\n " + RdaFunc.f_Repl(" ", 20) + "� �������� ����� - ";

			switch (par1)
			{
				case "OTDEL":
					#region ����� ������ ��� �Ĳ ����� �� ����������
					sTxt2 += (RDA.RDF.sLang == "R") ? " ������!" : " �����!";
					sTxt2 += sTxt4 + (RDA.RDF.sLang == "R" ? " ����� �� ������  " : " ���� �� ���i����� ");
					sTxt3 += (RDA.RDF.sLang == "R") ? "������ " : "����� ";
					sTxt3 += sTxt5 + "�Ĳ ����� �� ���i����� ";
					break;
					#endregion
				case "RESP":
					#region ����� ������ ��� �Ĳ  ������������ ���
					sTxt2 += (RDA.RDF.sLang == "R") ? "������������� ����  " : "���������� ����� ";
					sTxt2 += sTxt4 + (RDA.RDF.sLang == "R" ? " ������������� ���� " : " ����������� ����� ");
					sTxt3 += (RDA.RDF.sLang == "R") ? "�������������� ���� " : "����������� ����� ";
					sTxt3 += sTxt5 + "�Ĳ ������������";
					break;
					#endregion
				default:
					sTxt1 = "";
					break;
			}
			#region  ������������ ��������� �� ������
			if (sTxt1 != "")
			{
				sTxt2 += (RDA.RDF.sLang == "R") ? "� ���������� ������." : " � ���������� ������.";
				ERR.Error err8 = new ERR.Error(sTxt1, ERR.ErrorImages.Examplemation, sTxt2, sTxt3, 1);
				err8.ShowDialog();
			}
			#endregion
		}
	

		#endregion

		#region ��������� ������鳿 "�������" �����

		private void FRMNSI_KeyDown(object sender, KeyEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// FRMBOD2_KeyDown - ����� ���������� "�������" ������
			//	 ���  ������������� ������� ������ �� ����� ������� :
			//		                 - ������� ��� �������� KeyPreview = true
			//                       - ���������� ���������� ������� KeyDown � ��������� ��� 
			//	                       ��������������� ������ ���������� "�������" �����
			//-------------------------------------------------------------------------------------

			if (e.Control && e.KeyCode == Keys.S) this.bB_Save_ItemClick(null, null);     //CTRL+S     : ���������
			if (e.Control && e.KeyCode == Keys.Delete) this.bB_DelRow_ItemClick(null, null); //CTRL+DELETE: �������
			if (e.Alt && e.KeyCode == Keys.F4) this.Close();                              //Alt+F4: �������� ����� 
		}

		#endregion

			
	}
}

