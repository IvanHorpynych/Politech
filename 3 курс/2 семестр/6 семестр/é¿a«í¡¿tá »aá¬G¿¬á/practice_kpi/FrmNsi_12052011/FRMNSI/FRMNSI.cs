//----------------  AC  RODUZ NF - "...розподілу доходів по залізницях... "  -------------
//
// Форма FRMNSI- Ведення  НДІ   предназначена для работы администраторов и 
//                  и обеспечивает ведение справочников на дорогах: 
//                  GRUSER    - НДІ груп залізниць та  користувачів(просмотр)
//                  NSIROAD   - НДІ груп залізниць
//                  NSIOTDEL  - НДІ відділів по залізниці
//                  R_ROADRESP- НДІ відповідальних осіб відділу по залізниць
//
//   ГІОЦ УЗ-відділ ФС    пр-т: Бейлинсон Л.М. т-н: 5-09-86                  май 2011 р
//----------------------------------------------------------------------------------------

using System;                   //Объявляем пространство имен System
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;     //  ---  для Form
using System.Data;              // Главное пространство имен ADO.NET
//    using System.Data.OracleClient; // Вместо System.Data.OracleClient указываем
using CoreLab.Oracle;           //  OraDirect для связи с базой Oracle
using System.Data.OleDb;        // Пространство имен для соединений с OLE DB-источниками данных(ИМПОРТ из EXCEL-форм)
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
		#region Declare SYSTEM     - системные объекты
	
		private System.ComponentModel.IContainer components;
		// Создаем DataSet oбъект, в ОП ПК-клиета, для набора таблиц и отношений между ними
		private System.Data.DataSet Ds;

		// Создаем OracleConnection - соединение клиента с базой данных Oracle
		//private OracleConnection  OraCon;
		// Создаем объекты OracleDataAdapter для : 
		//     1. заполнения  Ds_таблиц данными  из Oracle_таблиц 
		//     2. изменения данных Oracle_таблиц из Ds_таблиц
		
		private OracleDataAdapter OraDABuffer;  // "Ds_Buffer"-временная область для выборки данных из Oracle_таблиц
		private OracleDataAdapter OraDANsi;     // для выборки в "Ds_Nsi"    из Oracle_таблицы НСИ 

		// Создаем объекты OracleCommandBuilder для каждой Ds_таблицы, чтобы обеспечить 
		// автогенерацию команд SQL(Update,Delete,Insert) при обновлении Oracle_таблиц.
		// При этом необходимо выполнения 2-х условий :
		//    1. каждой Ds_таблицы  должна соответствовать только одна Oracle_таблица и тогда
		//	 	    указывается : OraCBArh = new OracleCommandBuilder(OraDAArh);
		//    2. эта Oracle_таблица должна иметь ПЕРВИЧНЫЙ ключ и тогда
		//	  	    указывается : OraDAArh.MissingSchemaAction = MissingSchemaAction.AddWithKey;
		// Если	условия не выполнимы , то  для каждой из команд SQL(Update,Delete,Insert)
		// следует создать объекты OracleCommand, например :
		//			private OracleCommand OraIns; - добавления записи ...
		private OracleCommandBuilder OraCBNsi;     // для обновления Oracle_таблицы  НСИ   БД РОДУЗ НФ  из "Ds_Nsi"     

		#endregion

		#region Declare FORMS      - поля и объекты формы ...
		//    код дороги 
	
		#endregion
	
		#region Declare Variables  - дополнительные переменные
		private char   cAcKey  ;       // Переданное значение кода доступа : cAcc=R-чтение\W-изменение\F-полный
		private string myAccess = "";  // Устанавливаемый доступ для работы программы в АС `РОДУЗ` 
		// = ALL  - доступ ко ВСЕМ объектам и пользователям :
		//		     AcUser.Road==NsiRoad.Road== -1 && GrUser.Gr==NsiRoad.Pd== 4 -->  для администратора-программиста  
		//		     AcUser.Road==NsiRoad.Road==  5 && AcUser.AdmUser==1         -->  для администратора ЦФД
		// = ADMIN- доступ к объектам и пользователям СОБСТВЕННОГО предприятия : -->  для администратора предприятия
		//		     AcUser.Road==NsiRoad.Road > -1 && GrUser.Gr==NsiRoad.Pd < 5 && AcUser.AdmUser==1 
		// = USER - доступ пользователя к объектам СОБСТВЕННОГО предприятия :
		//		     AcUser.Road==NsiRoad.Road > -1 && GrUser.Gr==NsiRoad.Pd < 5 && AcUser.AdmUser==0 
		// = FALSE- доступ для работы в АС ЗАКРЫТ при  GrUser.Gr > 5 || AcUser.LockUser==1
		//
		//  Если myAccess != FALSE, то далее обработка идет согласно вида доступа : cAcKey = R-чтение,=F-полный 
		//               
		//private string sLang    = "U"; // Язык для выдачи сообщений: U-украинский, R-русский
		private long   uId      = 1  ; // Переданное значение UserID-ID_пользователя системы
		private string uName    = " "; // Переданное значение UserName-ФИО_пользователя системы

		private string ModiRow  = "";   // Признак копирования внесения в InfoUpdSrv информации :
                                         //  = "" - при изменении сторок таблицы(при rModi: I,U,D)  
										 //  = "A" -о копировании таблицы (при rModi: A)
		                                 //  = "*" -отменить копирование на сервера дорог 
		private string TransRoad= "";   // Признак, указывающий следует ли внести подобные изменения на все дороги:
									 	 //  ="N" - НЕТ(No) - стандартная обработка записи,
										 //  ="Y" - ДА(Yes) - указывается для создания дублей данной записи на сервере УЗ. 
		//private string SQLRow   = "";    // Переменная для формирования строки  SQL - Insert\Update\Delete

		private string myErr    = "";    // Параметр(ROAD,OTDEL) передаваемый для блока ошибок p_ErrRead(...)
		private	string ToolGr   = "";    // ToolTip для группы и ее названия при движении мышки в колонке PD ...View1
		private string ToolAC   = "";    // ToolTip для ID_ АС и ее названия при движении мышки в колонке LIST_AC ...View1
	
		private int    idxNSI   = 0;     // Индекс выбранной строки в списке НСИ
		private int    myRoad   = 0;     // Начальное значение кода выбираемой дороги 
		private bool   bRoad    = false; // Признак отсутствия данных в NsiRoad
		private int    idxRoad = 0;      // Индекс выбранной строки в НСИ дорог 

		private int    myOtdel  = 0 ;    // Начальное значение кода выбираемого отдела по дороге 
		private bool   bOtdel   = false; // Признак отсутствия данных в NsiOtdel
		private int    idxOtdel = 0;     // Индекс выбранной строки в НСИ отделов 

		private	string myKey    = "";    // Переменная для передачи параметром названия ключа записи
		private bool   bUpdBuff = false; // Признак отсутствия обновлений в Ds_Buffer 
		private bool   bNsiErr  = false; // Признак отсутствия ошибки в записи Ds_Buffer 
		private bool   bCtrlUpd = false; // Признак отсутствия контроля на изменение записи в Ds_Buffer 

		private int    rKey     = 0 ;    // Признак изменения ключа или удаления записи :=0-разрешено, =1-запрещено,
		private int    tRow     = -1;    // НОМЕР выбранной записи в grid2(...View9)
		private object[] RowObj     ; 
		private	string RowOld   = "";    // Переменная для хранения старого     значения выбранной записи
		private	string RowNew   = "";    // Переменная для хранения измененного значения выбранной записи

		
		private bool   bEditTabl= false; // Признак запрета на редактирование таблицы
		private	string myTabl   = "";    // Текущее имя обрабатываемой таблицы 
		private	string OldTabl  = "";    // Старое  имя обработанной   таблицы 

		
		// Создаем экземпляр(myView) класса ... AdvBandedGridView для получения доступа к свойствам вида
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView myView= new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
		
	
		
		private RDA.RDF RdaFunc=new RDA.RDF();// Объявление личной библиотеки  RDA, содержащей общие :

		#endregion
	
		#region Declare BandedGrid - гриды, кнопки, поля,...

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

			//OraCon = OC;      // коннект к базе ORACLE-открыть при формировании DLL_кода
			//if(OraCon.State != System.Data.ConnectionState.Open)
			//    OraCon.Open();
			cAcKey = cAcc;    // код доступа к объекту (cAccess из таблицы R_UserObj)
			uId    = UserID;  // ID  пользователя системы(IdUser из таблицы AcUser)			
			uName  = UserName;// имя пользователя системы(Naim   из таблицы AcUser)
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
			this.advBandedGridView2.GroupPanelText = "НДІ груп залiзниць та користувачів";
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
			this.v2B1.Caption = "Групи";
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
			this.v2B1_1.Caption = "код";
			this.v2B1_1.Columns.Add(this.v2C1);
			this.v2B1_1.Name = "v2B1_1";
			this.v2B1_1.OptionsBand.AllowHotTrack = false;
			this.v2B1_1.OptionsBand.AllowMove = false;
			this.v2B1_1.OptionsBand.AllowPress = false;
			this.v2B1_1.OptionsBand.AllowSize = false;
			this.v2B1_1.OptionsBand.ShowInCustomizationForm = false;
			this.v2B1_1.ToolTip = "Код групи";
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
			this.v2C1.ToolTip = "Код групи";
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
			this.v2B1_2.Caption = "назва";
			this.v2B1_2.Columns.Add(this.v2C2);
			this.v2B1_2.Name = "v2B1_2";
			this.v2B1_2.OptionsBand.AllowHotTrack = false;
			this.v2B1_2.OptionsBand.AllowMove = false;
			this.v2B1_2.OptionsBand.AllowPress = false;
			this.v2B1_2.OptionsBand.AllowSize = false;
			this.v2B1_2.OptionsBand.ShowInCustomizationForm = false;
			this.v2B1_2.ToolTip = "Назва групи";
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
			this.v2C2.ToolTip = "Назва групи";
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
			this.v2B2.Caption = "Останнє   редагування   запису";
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
			this.v2B2_1.Caption = "дата";
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
			this.v2B2_2.Caption = "користувач, якій вніс зміни";
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
			this.advBandedGridView3.GroupPanelText = " НДІ  відділів по";
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
			this.v3B1.Caption = "Відділи";
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
			this.v3B1_1.Caption = "шифр";
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
			this.v3B1_2.Caption = "номер";
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
			this.v3B1_3.Caption = "назва";
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
			this.v3B2.Caption = "Останнє   редагування   запису";
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
			this.v3B2_1.Caption = "дата";
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
			this.v3B2_2.Caption = "користувач, якій вніс зміни";
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
			this.gridBand16.Caption = "Організація";
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
			this.advBandedGridView4.GroupPanelText = " Список відповідальних осіб відділу";
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
			this.v4B1.Caption = "Відповідальни особі";
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
			this.v4B1_1.Caption = "П І Б";
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
			this.v4B1_2.Caption = "посада";
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
			this.v4B1_3.Caption = "телефон";
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
			this.v4B1_4.Caption = "ознака";
			this.v4B1_4.Columns.Add(this.v4C4);
			this.v4B1_4.Name = "v4B1_4";
			this.v4B1_4.OptionsBand.AllowSize = false;
			this.v4B1_4.ToolTip = "0 - ответственный за документ, 1- исполнитель, 2- кому предзначен документ";
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
			this.v4C4.ToolTip = "0- ответственный за документ, 1- исполнитель, 2- кому предзначен документ";
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
            "0- ответственный за документ",
            "1- исполнитель",
            "2- кому предзначен документ"});
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
			this.v4B2.Caption = "№";
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
			this.v4B3.Caption = "Останнє   редагування   запису";
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
			this.v4B3_1.Caption = "дата";
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
			this.v4B3_2.Caption = "користувач, якій вніс зміни";
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
			this.advBandedGridView1.GroupPanelText = "НДІ залiзниць ...";
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
			this.v1B1.Caption = "Залiзниці та інші ";
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
			this.v1B1_1.Caption = "код";
			this.v1B1_1.Columns.Add(this.v1C1);
			this.v1B1_1.Name = "v1B1_1";
			this.v1B1_1.OptionsBand.AllowHotTrack = false;
			this.v1B1_1.OptionsBand.AllowMove = false;
			this.v1B1_1.OptionsBand.AllowPress = false;
			this.v1B1_1.OptionsBand.AllowSize = false;
			this.v1B1_1.ToolTip = "Код підприємства";
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
			this.v1C1.ToolTip = "Код підприємства";
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
			this.v1B1_2.Caption = "назва в називному відмінку";
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
			this.v1B1_3.Caption = "назва у давальному відмінку";
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
			this.v1B2.Caption = "IP-сервера";
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
			this.v1B3.Caption = "Група";
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
			this.v1C5.ToolTip = "Група до якої відноситься підприємство";
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
			this.v1B_AC.Caption = "Список  АС";
			this.v1B_AC.Columns.Add(this.v1AC);
			this.v1B_AC.Name = "v1B_AC";
			this.v1B_AC.ToolTip = "Список(из таблицы LIST_AC )  ID_кодов   в виде : RD, NF \\ RD \\  NF, указывающий к" +
				"акие АС доступны дороге (пусто- все АС)";
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
			this.v1AC.ToolTip = "Список(з таблиці LIST_AC)  ID_кодів у вигляді: RD,NF ! RD ! NF..., що вказують як" +
				"і АС доступні підприємству(пусто- всі АС)";
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "Список(из таблицы LIST_AC )  ID_кодов   в виде : RD, NF \\ RD \\  NF, указывающий к" +
                    "акие АС доступны дороге (пусто- все АС)")});
			this.rICBE_AC.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
			this.rICBE_AC.Name = "rICBE_AC";
			this.rICBE_AC.ShowAllItemCaption = "Все АС";
			this.rICBE_AC.ShowPopupCloseButton = false;
			// 
			// v1B4
			// 
			this.v1B4.AppearanceHeader.Options.UseTextOptions = true;
			this.v1B4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.v1B4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.v1B4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.v1B4.Caption = "Унікальний  код запису ";
			this.v1B4.Columns.Add(this.v1C6);
			this.v1B4.Name = "v1B4";
			this.v1B4.OptionsBand.AllowHotTrack = false;
			this.v1B4.OptionsBand.AllowMove = false;
			this.v1B4.OptionsBand.AllowPress = false;
			this.v1B4.OptionsBand.AllowSize = false;
			this.v1B4.RowCount = 3;
			this.v1B4.ToolTip = "Унікальний  код запису формується автоматично : 005RRRR00000000 (т.е (005*10000+R" +
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
			this.v1C6.ToolTip = "Унікальний  код запису формується автоматично : 005RRRR00000000 (т.е (005*10000+R" +
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
			this.gridBand1.Caption = "Повна назва";
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
			this.v1B5.Caption = "Останнє  редагування  запису";
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
			this.v1B5_1.Caption = "дата";
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
			this.v1B5_2.Caption = "користувач, якій вніс зміни";
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
			this.g1_l5.Text = "вимагають  обов\'язкового  заповнення !";
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
			this.g1_l3.Text = "або";
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
			this.g1_l1.Text = "Поля, що містять";
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
                "стор. [Page #] "}, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))), DevExpress.XtraPrinting.BrickAlignment.Near), new DevExpress.XtraPrinting.PageFooterArea(new string[] {
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
			this.gridBand10.Caption = "№";
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
			this.l_rKey2.Text = "див. поле ";
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
			this.l_rKey1.Text = "Неможливо замінити ключ, що використовується в інших таблицях. ";
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
			this.bE_NDI.Caption = "  Список НДІ";
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
			this.rI_NDI.NullText = "<bE_NDI-> выбор списка НСИ>";
			this.rI_NDI.ShowFooter = false;
			this.rI_NDI.ShowHeader = false;
			this.rI_NDI.ValueMember = "ID";
			this.rI_NDI.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_NDI_EditValueChanging);
			// 
			// bE_Road
			// 
			this.bE_Road.Caption = "Залiзниця";
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ROAD", "код", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAIM", "назва", 90),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAIMD", "", 40, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PD", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
			this.rI_Road.DisplayMember = "NAIM";
			this.rI_Road.DropDownRows = 12;
			this.rI_Road.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_Road.Name = "rI_Road";
			this.rI_Road.NullText = "<bE_Road -> выбор дороги>";
			this.rI_Road.ShowFooter = false;
			this.rI_Road.ValueMember = "ROAD";
			this.rI_Road.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rI_Road_EditValueChanging);
			// 
			// bE_Otdel
			// 
			this.bE_Otdel.Caption = "   Відділи залiзниці";
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OTDEL", "Код", 30, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KOD", "Шифр", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAIM", "Назва", 160),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ROAD", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
			this.rI_Otdel.DisplayMember = "NAIM";
			this.rI_Otdel.DropDownRows = 10;
			this.rI_Otdel.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.rI_Otdel.Name = "rI_Otdel";
			this.rI_Otdel.NullText = "<bE_Otdel ->выбор списка отделов>";
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
			this.bB_Save.Caption = "Зберегти";
			this.bB_Save.Id = 1;
			this.bB_Save.ImageIndex = 5;
			this.bB_Save.Name = "bB_Save";
			this.bB_Save.Tag = "";
			this.bB_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Save_ItemClick);
			// 
			// bB_Print
			// 
			this.bB_Print.Caption = " Друк даних  ";
			this.bB_Print.Id = 23;
			this.bB_Print.ImageIndex = 7;
			this.bB_Print.Name = "bB_Print";
			this.bB_Print.Tag = "";
			this.bB_Print.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_Print_ItemClick);
			// 
			// bSI_Row
			// 
			this.bSI_Row.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.bSI_Row.Caption = "                                     Обробка запису     ";
			this.bSI_Row.Id = 58;
			this.bSI_Row.Name = "bSI_Row";
			this.bSI_Row.OwnFont = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bSI_Row.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bSI_Row.UseOwnFont = true;
			// 
			// bB_AddRow
			// 
			this.bB_AddRow.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_AddRow.Caption = "Новий запис  ";
			this.bB_AddRow.Id = 56;
			this.bB_AddRow.ImageIndex = 30;
			this.bB_AddRow.Name = "bB_AddRow";
			this.bB_AddRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_AddRow_ItemClick);
			// 
			// bB_CancelEditRow
			// 
			this.bB_CancelEditRow.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_CancelEditRow.Caption = "Відмінити змін ";
			this.bB_CancelEditRow.Id = 57;
			this.bB_CancelEditRow.ImageIndex = 29;
			this.bB_CancelEditRow.Name = "bB_CancelEditRow";
			this.bB_CancelEditRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_CancelEditRow_ItemClick);
			// 
			// bB_DelRow
			// 
			this.bB_DelRow.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.bB_DelRow.Caption = " Видалити запис";
			this.bB_DelRow.Id = 47;
			this.bB_DelRow.ImageIndex = 22;
			this.bB_DelRow.Name = "bB_DelRow";
			this.bB_DelRow.Tag = "";
			this.bB_DelRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bB_DelRow_ItemClick);
			// 
			// bB_Exit
			// 
			this.bB_Exit.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.bB_Exit.Caption = "Вийти";
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
			this.bS_InfoLeft.Caption = "<bS_InfoLeft>  Документ заблоковано,...";
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
			this.bS_Info.Caption = "Запис  відредаговано :";
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
			this.bS_Formula.Caption = "Формула расчета суммы по строке ";
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
			this.Text = "FRMNSI - НДІ груп,  залiзниць, відділів та відповідальних особ  ";
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
		{   // Объявляем функцию Main()и перегружаем конструктор 
			// Используем только в режиме разработки, а после формирования DLL_кода эта функция игнорируется

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
			#region myAccess- определение доступа("ALL","ADMIN","USER", "READ" ) к объектам
			//-------------------------------------------------------------------------------------		
			//  myAccess = RdaFunc.p_Access(uId.ToString()) : 
			//           = "ALL"   - администратор АС         (режим доступа cAcKey='F')
			//           = "ADMIN" - администратор БД дороги  (режим доступа cAcKey='F')
			//           = "USER"  - пользователь   БД дороги (режим доступа cAcKey='F')
			//           = "READ"  - только просмотр данных   (режим доступа cAcKey='R') 
			//           = "FALSE" - доступ ЗАКРЫТ
			//-------------------------------------------------------------------------------------		

			// Доступ на редактирование соответствующего НСИ см. в блоке  p_ViewPage1()
	
			myAccess = RDA.RDF.UserParam.myAccess;   // Получение из Rda значения myAccess 26/10/2010
			myRoad   = RDA.RDF.UserParam.myRoad;     // Получение из Rda кода дороги пользователя

            #endregion

			#region Oпределение коллекции таблиц в DataSet(Ds)
			
            // Ds(DataSet)- RowNum_класс содержит коллекцию таблиц для хранения в ОП(кэше):             
			//              данных из БД, реляционных зависимостей и constraints
			Ds = new DataSet();
			
			Ds.Tables.Add("Ds_Nsi");       // OraDANsi    -> Временная область для выборки в "Ds_Nsi" из Oracle_таблицы НСИ 
			Ds.Tables.Add("Ds_Buffer");    // OraDABuffer -> Временная область для выборки данных из Oracle_таблиц
			
            #endregion			

			#region установка начальных значений 
			
			myTabl  = "";                      // Текущее имя обрабатываемой таблицы 
			tRow    = -1;                      // НОМЕР выбранной записи в grid2(...View9)
			//			bBuff   = false;                   // Установим признак - нет данных    в Ds_Buffer
			bUpdBuff= false;                   // Установим признак - нет изменений в Ds_Buffer
			bB_Save.Enabled = false ;          // Деактивизация кнопок : "Зберегти зміни" 
			bS_InfoLeft.Caption = "";          // Очистить информационное поле на форме
			
			bE_Road.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-скрыть
			bE_Otdel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-скрыть окно выбора отделов по организации из списка

			p_ListNDI(); // загрузка списка обрабатываемых НСИ

			#endregion	
			p_LanghInfo(); // Формируем всплывающие подсказки для элементов формы

			bE_NDI_EditValueChanged(null, null);
				
	
		}
   
        #region Методы для работы с файлом InfoUpdSrv

		private void p_UpdInfo(DataRow dr, string InTabl, string OutTabl, string tModi, int OpFlag)
        {
        //-------------------------------------------------------------------------------------		
        // p_UpdInfo - метод используется для записи в InfoUpdSrv информации об обнoвлении 
		//             таблиц GRMENU_AC,GRUSER, NSIROAD, NSIOTDEL и R_ROADRESP в БД РОДУЗ  на 
        //             cерверах УЗ и дорог 
        //
        //  Параметры: 
        //    InTabl     - имя таблицы где  выполнены изменения
		//    OutTabl    - имя таблицы куда направить изменения("" соответствует той же таблице InTabl)
		//    DataRow dr - обрабатываемая строка таблицы в которой внесены изменения  
        //    tModi      - признак корректировки : I,U,D или копирования A, F 
        //                 для записи :  =I-добавлена, =U-изменена, =D-удалена 
        //                 для таблицы:  =A-замена всех записей таблицы(выполняется только с сервера УЗ)
        //    OpFlag     - тип операции с таблицей InfoUpdSrv :
        //                 = 0 - накопление строк в Ds_Info
        //                 = 1 - сохранение в InfoUpdSrv всех накопленных строк в Ds_Info  и  текущей строки 
        //                 = 2 - сохранение в InfoUpdSrv всех накопленных строк в Ds_Info 
        //
		//	Вызывается в блоке sB_Save_Click для формирования в таблице InfoUpdSrv записей,
		//			       содержащих информации об обнавлении таблиц на серверах УЗ и дорог
		//-------------------------------------------------------------------------------------			
		//   Прочие :     
		//    RoadOut   - до вызова метода p_SaveIntoInfo(...), необходимо указать значение 
		//                глобальной  переменной  для  определения направления репликаций:
		//                RdaFunc.RoadOut =-2 - репликации идут на ВСЕ дороги 
		//                RdaFunc.RoadOut =ХХ - репликации идут ТОЛЬКО на ХХ-ю дорогу
		//
        //
		//    ModiRow   = "" -признак внесения в Ds_Info информации об обработке стороки(при rModi: I,U,D)  
		//              = "A"-признак внесения в Ds_Info информации о репликации таблицы(при rModi: A)
		//              = "*"-признак отказа от внесения в Ds_Info информации 
		//
	    //    TransRoad  - признак обработки данной записи программой RoduzDbUpd
        //               = "N"-признак стандартной обработки записи
        //               = "Y"-признак для создания дублей данной записи на сервере УЗ. 
        //                 Устанавливается только при изменениях в таблицах БД на сервере ДОРОГИ,
        //                  чтобы обеспечить изменения данных во всех БД на остальных дорогах.
		//
        //    new object[] 
        //    { Key1,Key2,Key3,-1-3-ий ключи для выборки записи в InTabl
        //      "","",""},    Key4-6 -следующие ключи для выборки (здесь не используются)
        //
        //    Id_Key     - дополнение к первичному ключу таблицы InfoUpdSrv : 
        //               = vkey1+vkey2+vkey3+vkey4+vkey5+vkey6 (или  ="A" при tModi="A")
        //     где vKey1-vKey6 значение с 1 по 6 полей составного первичного ключа записи в InTabl     
        //
		//-------------------------------------------------------------------------------------			

			ModiRow = "";      // -укажем, что копируем запись из таблицы(для rModi: I,U,D )  

		    string tTabl  = InTabl.Trim().ToUpper();
            string vKey1  = "";                     // Значение в полях Key* 
            string vKey2  = "";
            string vKey3  = "";
			string Key1   = "";
			string Key2   = "";
			string Key3   = "";
			string Id_Key = "";
			#region Формирование Key1-Key3 и ключа записи Id_Key для  GRUSER, NSIROAD, NSIOTDEL и R_ROADRESP 
			switch (tTabl)
			{

				case "GRUSER": 
					#region НДІ групп пользователей 
					vKey1 = dr["GR"].ToString();
					Key1 = "GR=" + vKey1.ToString();
					Id_Key = vKey1.ToString();
					RdaFunc.RoadOut = -2;   // Необходимо реплицировать изменения в БД РОДУЗ  на сервера ВСЕХ дорог 
					break;
					#endregion

				case "NSIROAD":
					#region  НДІ підприємств 
					vKey1  = dr["ROAD"].ToString();
					Key1   = "ROAD=" + vKey1.ToString();
					Id_Key = vKey1.ToString();
					RdaFunc.RoadOut = -2;   // Необходимо реплицировать изменения в БД РОДУЗ на сервера ВСЕХ дорог 
					break;
				#endregion

				case "NSIOTDEL":
                    #region НДІ відділів по підприємству 
                    vKey1  = dr["ROAD"].ToString();
                    vKey2  = dr["OTDEL"].ToString();
                    Key1   = "ROAD=" + vKey1.ToString();
                    Key2   = "OTDEL=" + vKey2.ToString();
                    Id_Key = vKey1.ToString() + vKey2.ToString();
					RdaFunc.RoadOut = (RDA.RDF.UserParam.RoadServ == 5) ? myRoad : 5;  // укажем направление репликации: на сервер дороги или УЗ 
					break;
                    #endregion
               
				 case "R_ROADRESP":
                    #region НДІ відповідальних осіб предприятия 
					vKey1  = dr["ROAD"].ToString();
                    vKey2  = dr["OTDEL"].ToString();
                    vKey3  = dr["POSITION"].ToString();

                    Key1   = "ROAD=" + vKey1.ToString();
                    Key2   = "OTDEL=" + vKey2.ToString();
                    Key3   = "POSITION=" + vKey3.ToString();
                    Id_Key = vKey1.ToString() + vKey2.ToString() + vKey3.ToString();
					RdaFunc.RoadOut = (RDA.RDF.UserParam.RoadServ == 5) ? myRoad : 5;  // укажем направление репликации: на сервер дороги или УЗ 
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

		#region  Методы, связанные c выбором и отображение списков в окнах LookUpEdit для : дорог, отделов по дорогам
			
		// p_ListNDI - загрузка списка обрабатываемых НСИ в rI_NDI_репозиторий поля bE_NDI
		private void p_ListNDI()
		{
			//-------------------------------------------------------------------------------------
			// p_ListNDI - формирование списка обрабатываемых НСИ в RepositoryItemLookUpEdit(rI_NDI)
			//-------------------------------------------------------------------------------------
			DataTable tNdi = new DataTable();
			tNdi.Columns.AddRange(new DataColumn[] { new DataColumn("ID", typeof(int)), new DataColumn("NAIM", typeof(string)) });
			tNdi.LoadDataRow(new object[] { 0, "НДІ груп залiзниць та користувачів" }, true);
			tNdi.LoadDataRow(new object[] { 1, "НДІ залiзниць ..." }, true);
			tNdi.LoadDataRow(new object[] { 2, "НДІ відділів по залiзниці" }, true);
			tNdi.LoadDataRow(new object[] { 3, "НДІ відповідальних осіб по відділам залiзниці" }, true);
			rI_NDI.DataSource = tNdi;
			//bE_NDI.EditValue = 0;
			idxNSI = 0;
		}

		// rI_NDI_EditValueChanging - контроль ПЕРЕД выбором НСИ из списка
		private void rI_NDI_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_NDI_EditValueChanging - метод используется ПЕРЕД выбором нового НСИ для контроля:
			// -если в предыдущем НСИ были ошибки(bOk =true), то не выполнять перехода
			// -eсли в предыдущем НСИ нет  ошибок, но не сохранены изменения(bUpdBuff=true), то 
			//                  выдать сообщение для принятия решения о выполнении "Зберегти"
			//-------------------------------------------------------------------------------------
			if (idxNSI != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
				p_CtrlRowNsi(); // Если были изменения в текущей(tRow) записи --> bUpdBuff=true

				// Дополнительные тексты для f_ErrN и  f_ErrRowNsi
				string sNDI = rI_NDI.GetDataSourceValue("NAIM", Convert.ToInt32(e.NewValue.ToString())).ToString(); // по индексу ранее выбранной строки в rI_NDI
				string txt1 = (RDA.RDF.sLang == "R" ? " переход к " : "  перехiд до ") + sNDI ;
				string txt2 = (RDA.RDF.sLang == "R" ? "Отменен " : "Відмінно") + txt1;

				bool bOk = f_ErrRowNsi(myTabl, txt2)  ; // При ошибках(bOk=true),то не выполнять перехода

				if (!bOk && bUpdBuff && idxNSI > -1) // Если после изменений НЕ ВЫПОЛНЕНО сохранение: 
				{                                                    // - сообщить пользователю.  
					bOk  = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "В `" : "У  `") + myView.GroupPanelText + "`", "", "BUFF");
					if (!bOk) bE_NDI.EditValue = e.NewValue;         // - при ответе YES-NO перейти в bE_NDI_EditValueChanged
				}
				e.Cancel = bOk;
			}

		}
	
		// bE_NDI_EditValueChanged - действия  ПОСЛЕ выбора НСИ из списка 
		private void bE_NDI_EditValueChanged(object sender, EventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bE_NDI_EditValueChanged - метод используется ПОСЛЕ выбора НСИ из списка для :
			//	    - записи в  myTabl имени обрабатываемой таблицы
			//	    - сохранения в idxNSI индекса выбранной строки в списке НСИ
			//	    - скрытия\отображения меток и окон для выбора дорог, отделов,...
			//      - выбора в Ds_Tmp2 ключей для контроля на использование в дочерних таблицах
			//-------------------------------------------------------------------------------------			
			// Примечание.
			//  Выбор в Ds_Tmp2 выполняется при работе в БД РОДУЗ НФ : 
			//  -(для GRUSER \ NSIROAD)      администратора АС(myAccess=ALL) на сервере УЗ(RoadServ=5).
			//
			//  -(для NSIOTDEL \ R_ROADRESP) администратора АС(myAccess=ALL) или ДОРОГИ(myAccess=ADMIN)
			//   Причем работа разрешена  в БД на сервере УЗ или дороги 
			//
			//-------------------------------------------------------------------------------------
			bS_InfoLeft.Caption = "";
			grid1.Focus();
			
			bE_Road.Visibility  = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-скрыть  окно выбора организации из списка
			bE_Otdel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// = Never-скрыть окно выбора отделов по организации из списка
			bUpdBuff        = false; // Установим признак - нет изменений в Ds_Buffer
			bB_Save.Enabled = false;          // Деактивизация кнопок : "Зберегти зміни" 

			myTabl = ""; // Текущее имя обрабатываемой таблицы 
			if (idxNSI != Convert.ToInt16(bE_NDI.EditValue.ToString())) // Индекс выбранной строки в списке НСИ
				idxNSI = Convert.ToInt16(bE_NDI.EditValue.ToString());

			#region Блок сохранения в myTabl текущего имени обрабатываемой таблицы НСИ и т.п.

			switch (idxNSI)
			{
				case 0:
					#region Oracle_таблица GRUSER  - НДІ групп
					myTabl = "GRUSER";
					l_rKey3.Text = " код ";    // Переменная для доп. cообщения о ключе к метке  l_rKey1 на гриде

					break;
					#endregion

				case 1:
					#region Oracle_таблица NSIROAD - НДІ дорог
					myTabl = "NSIROAD";
					l_rKey3.Text = " код "; // Переменная для доп. cообщения о ключе к метке  l_rKey1 на гриде
					p_ListGr();  // загрузка списка групп для отображения ToolTip при наведении мышки на поле PD
					p_ListAC();  // загрузка списка АС    для отображения ToolTip при наведении мышки на поле LIST_AC

					break;
					#endregion

				case 2:
					#region Oracle_таблица NSIOTDEL - НДІ відділів по дороге
					myTabl = "NSIOTDEL";
					l_rKey3.Text = " шифр - номер ";    // Переменная для доп. cообщения о ключе к метке  l_rKey1 на гриде
					bE_Road.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;// = Always -Открыть окно выбора организации из списка
					break;
					#endregion

				case 3:
					#region Oracle_таблица R_ROADRESP -НДІ відповідальних осіб предприятия
					myTabl = "R_ROADRESP";
					l_rKey3.Text = " № ";    // Переменная для доп. cообщения о ключе к метке  l_rKey1 на гриде
					bE_Road.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; // = Always - Открыть окно выбора организации из списка
					bE_Otdel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;// = Always -  Открыть окно выбора отделов по организации из списка

					break;
					#endregion

				default:
					break;
			}
			#endregion

			p_NewNsi();   // Загрузка в "Ds_Buffer" НСИ, выбранного из списка bE_NDI
		}
	
		// p_LoadRoad - загрузка списка предприятий в rI_Road_репозиторий поля bE_Road
		private void p_LoadRoad()
		{
			//-------------------------------------------------------------------------------------		
			// p_LoadRoad - метод используется для загрузки списка предприятий из NsiRoad
			//              в rI_Road_репозиторий для выбора предприятий в поле bE_Road
			//-------------------------------------------------------------------------------------		
			idxRoad = -1;
			bRoad   = false;          // Установим признак отсутствия данных в NsiRoad
			string txt = " WHERE Road > -1 AND Pd < 4 ";
			// Если myAccess <> "ALL",  то ДОограничим доступ предприятием myRoad
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
						if (dt.Rows.Count > 0) bRoad = true; // Установим признак наличия данных в NsiRoad

						bE_Road.EditValue = dt.Rows[0]["ROAD"]; // в списке-выбрана первая дорога 
						myRoad = Convert.ToInt32(bE_Road.EditValue);
						idxRoad = rI_Road.GetDataSourceRowIndex("ROAD", myRoad); // индекс строки в коллекции
					}
				}
			}
			catch (System.Exception ex)
			{
				ERR.Error err = new ERR.Error("Помилка", ERR.ErrorImages.CryticalError,
								"Помилка при завантажені даних з табліци  NSIROAD", ex.ToString(), 1);
				err.ShowDialog();
			}

		}
		// rI_Road_EditValueChanging - контроль ПЕРЕД изменением предприятия
		private void rI_Road_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_Road_EditValueChanging - метод используется ПЕРЕД изменением предприятия для
			//            принятия решения о сохранении изменений если не выполнено "Зберегти"
			//-------------------------------------------------------------------------------------
			if (myRoad != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
				p_CtrlRowNsi(); // Если были изменения в текущей(tRow) записи --> bUpdBuff=true

				// Дополнительные тексты для f_ErrN и  f_ErrRowNsi
				int    idxN = rI_Road.GetDataSourceRowIndex("ROAD", Convert.ToInt32(e.NewValue.ToString()) ); // индекс строки в коллекции
				string sNDI = rI_Road.GetDataSourceValue("NAIMD", idxN).ToString(); // по индексу строки 
				string txt1 = (RDA.RDF.sLang == "R" ? " переход к " : "  перехiд до ") + sNDI
							+ (rI_Road.GetDataSourceValue("PD", idxN).ToString()=="1" 
							? (RDA.RDF.sLang == "R" ? " дорогe" : " залiзниці") :"") ;
				string txt2 = (RDA.RDF.sLang == "R" ? "Отменен " : "Відмінно") + txt1;

				bool bOk = f_ErrRowNsi(myTabl, txt2); // При ошибках(bOk=true),то не выполнять перехода

				if (!bOk && bUpdBuff && idxN > -1) // Если после изменений НЕ ВЫПОЛНЕНО сохранение: 
				{                                                    // - сообщить пользователю.  
					bOk = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "В `" : "У  `") + myView.GroupPanelText + "`", "", "BUFF");
					if (!bOk) bE_Road.EditValue = e.NewValue;         // - при ответе YES-NO перейти в bE_NDI_EditValueChanged
				}
				e.Cancel = bOk;
			}
		}
		// bE_Road_EditValueChanged - действия  ПОСЛЕ изменения предприятия 
		private void bE_Road_EditValueChanged(object sender, EventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bE_Road_EditValueChanged - метод используется ПОСЛЕ изменения предприятия при 
			//                           обработке НСИ  отделов или відповідальних осіб
			//-------------------------------------------------------------------------------------
			grid1.Focus();
			bS_InfoLeft.Caption = "";
			myRoad  = 0;
			idxRoad = -1; // индекс строки в коллекции
			if (bRoad)
			{
				myRoad  = Convert.ToInt32(bE_Road.EditValue); // Сохранение кода предприятия 
				idxRoad = rI_Road.GetDataSourceRowIndex("ROAD", myRoad); // индекс строки в коллекции
			}

			switch (myTabl)
			{
				case "NSIOTDEL":
					#region НДІ відділів (p_ReadRoad--> p_NewOtdel) по підприємству

					p_NewOtdel();                // - выбор отделов для изменений
					break;
					#endregion

				case "R_ROADRESP":
					#region НДІ відповідальних осіб(p_LoadRoad--> p_LoadOtdel) по отделам предприятия

					p_LoadOtdel();               // - определение списка отделов для выбора лиц
					break;
					#endregion
				default:
					break;
			}

		}


		// p_LoadRoad - загрузка списка отделов в rI_Otdel_репозиторий поля bE_Otdel
		private void p_LoadOtdel()
		{
			//-------------------------------------------------------------------------------------		
			// p_LoadOtdel - метод используется для загрузки списка отделов по предприятию из 
			//               NsiOtdel в rI_Otdel_репозиторий для выбора отделов в поле bE_Otdel
			//-------------------------------------------------------------------------------------		

			idxOtdel = -1;      // индекс строки в коллекции
			bOtdel   = false;   // Установим признак отсутствия данных в NsiOtdel
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
						if (dt.Rows.Count > 0) bOtdel = true; // Установим признак наличия данных в NsiRoad

						bE_Otdel.EditValue = dt.Rows[0]["OTDEL"]; // в списке-выбрана первая дорога 
						myOtdel  = Convert.ToInt32(bE_Otdel.EditValue);
						idxOtdel = rI_Otdel.GetDataSourceRowIndex("OTDEL", myOtdel); // индекс строки в коллекции
					}
				}
			}
			catch { }

			//catch (System.Exception ex)
			//{
			//    ERR.Error err = new ERR.Error("Помилка", ERR.ErrorImages.CryticalError,
			//                    "Помилка при завантажені даних з табліци  NsiOtdel ", ex.ToString(), 1);
			//    err.ShowDialog();
			//}

		}
		// rI_Otdel_EditValueChanging -контроль ПЕРЕД изменением отдела по предприятию
		private void rI_Otdel_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_Otdel_EditValueChanging-метод используется ПЕРЕД изменением отдела по предприятию
			//            для принятия решения, если были изменения и не выполнено "Зберегти"
			//-------------------------------------------------------------------------------------
			if (myOtdel != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
				p_CtrlRowNsi(); // Если были изменения в текущей(tRow) записи --> bUpdBuff=true

				// Дополнительные тексты для f_ErrN и  f_ErrRowNsi
				int idxN    = rI_Otdel.GetDataSourceRowIndex("OTDEL", Convert.ToInt32(e.NewValue.ToString())); // индекс строки в коллекции
				string sNDI = rI_Otdel.GetDataSourceValue("NAIM", idxN).ToString(); // по индексу строки 
				string txt1 = (RDA.RDF.sLang == "R" ? " переход к отделу " : " перехiд до відділу ") + sNDI ;
				string txt2 = (RDA.RDF.sLang == "R" ? "Отменен " : "Відмінно") + txt1;

				bool bOk = f_ErrRowNsi(myTabl, txt2); // При ошибках(bOk=true),то не выполнять перехода

				if (!bOk && bUpdBuff && idxN > -1) // Если после изменений НЕ ВЫПОЛНЕНО сохранение: 
				{                                                    // - сообщить пользователю.  
					bOk = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "В `" : "У  `") + myView.GroupPanelText + "`", "", "BUFF");
					if (!bOk) bE_Otdel.EditValue = e.NewValue;         // - при ответе YES-NO перейти в bE_NDI_EditValueChanged
				}
				e.Cancel = bOk;
			}
		}
		//  bE_Otdel_EditValueChanged -действия  ПОСЛЕ изменения отдела по предприятию
		private void bE_Otdel_EditValueChanged(object sender, EventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//  bE_Otdel_EditValueChanged - метод используется ПОСЛЕ изменения отдела по предприятию
			//-------------------------------------------------------------------------------------
			grid1.Focus();
			myOtdel  = 0;
			idxOtdel = -1;      // индекс строки в коллекции
			if (bOtdel)
			{
				myOtdel  = Convert.ToInt32(bE_Otdel.EditValue); // сохраним код  
				idxOtdel = rI_Otdel.GetDataSourceRowIndex("OTDEL", myOtdel); // индекс строки в коллекции
			}
			p_NewResp();            // Выборка ответственных лиц отдела для изменений 

		}


		#endregion

		#region Общие методы и методы для работы с формой: доступ, страницы, кнопки, обновление, закрытие формы...


		// p_NSDClick - Активизация-деАктивизация кнопок
		private void p_NSDClick()
		{
			//-------------------------------------------------------------------------------------
			// p_NSDClick - метод используется для деактивизация-Активизация кнопок:
			//		        sB_Save(Зберегти зміни),... и разрешения-запрета изменений 
			//                                 
			// Вызывается в блоке p_ViewPage1
			//-------------------------------------------------------------------------------------

			bB_AddRow.Enabled        = bEditTabl;
			bB_CancelEditRow.Enabled = bEditTabl;
			bB_DelRow.Enabled        = bEditTabl;
			
			myView.OptionsBehavior.Editable = bEditTabl; //Разрешить-запретить изменения в таблице

		}

		private void p_DsNsi()
		{
			//-------------------------------------------------------------------------------------		
			// p_DsNsi - метод обеспечивает выбор данных из Oracle_таблиц GRUSER, NSIROAD, NSIOTDEL 
			//           или R_ROADRESP  в   Ds_Nsi(БД РОДУЗ).
			//
			//	Вызывается в блоке sB_Save_Click перед обновление данных в Oracle_таблицах
			//-------------------------------------------------------------------------------------			

			if (bEditTabl) // Если разрешено изменение в таблицах
			{
				string nKey  = "";  // Имя поля-ключа
				try
				{
					#region  Выбор данных из Oracle_таблиц GRUSER, NSIROAD, NSIOTDEL или R_ROADRESP
					switch (myTabl)
					{
						case "GRUSER": 
						    #region НДІ групп пользователей 
							nKey="GR";
							OraDANsi    = new OracleDataAdapter(@" SELECT 0 as OldKey, t.* FROM GrUser t "
								+ " ORDER BY Gr", RDA.RDF.BDOraCon);
							break;
                            #endregion

						case "NSIROAD":   
							#region НДІ підприємств 
							nKey = "ROAD";
							OraDANsi = new OracleDataAdapter(@" SELECT 0 as OldKey,t.* FROM NsiRoad t "
								+ @" WHERE t.Road > -1 ORDER BY Road", RDA.RDF.BDOraCon);
							break;
                            #endregion

						case "NSIOTDEL": 
							#region НДІ відділів по підприємству 
							nKey = "OTDEL";
							OraDANsi = new OracleDataAdapter(@" SELECT 0 as OldKey, t.* FROM NsiOtdel  t "
								+ @" WHERE t.Road=" + myRoad.ToString() + " ORDER BY Road, Otdel", RDA.RDF.BDOraCon);
							break;
                            #endregion

						case "R_ROADRESP":
							#region НДІ відповідальних осіб предприятия 
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

					#region Заполнение Ds_Nsi    данными из Oracle_таблицы БД РОДУЗ

					Ds.Tables["Ds_Nsi"].Dispose();       // освободим все ресурсы 
					Ds.Tables["Ds_Nsi"].Clear();         // очистим   все данные в таблице    
					OraDANsi.Fill(Ds, "Ds_Nsi");         // заполним  Ds_таблицу 
					foreach (DataRow drN in Ds.Tables["Ds_Nsi"].Rows)
					{
						drN["OLDKEY"] = drN[nKey];       // Запишем старое значение ключа
					}
					Ds.Tables["Ds_Nsi"].AcceptChanges(); // Установим статус записей в Unchanged 

					// Применим автогенерацию команд SQL(Update,Delete,Insert)  
					//  для обновления Oracle_таблицы НСИ по первичному ключу                	                                              
					OraCBNsi = new OracleCommandBuilder(OraDANsi);
					// при паралельной работе записать в БД данные того, кто последний выполнил сохранение  
					OraCBNsi.ConflictOption = System.Data.ConflictOption.OverwriteChanges;
					//OraCBNsi.SetAllValues = true;
					OraDANsi.MissingSchemaAction = MissingSchemaAction.AddWithKey;

					#endregion
		
				}
				catch { }
			}
		}

		// p_UpdRowNsi - обновление строки(drN) в Ds_Nsi из соответствующей строки(drB) Ds_Buffer 
		private void p_UpdRowNsi(DataRow drB, DataRow drN)
		{
		//-------------------------------------------------------------------------------------
		// p_UpdRowNsi-метод используется для обновления строки(drN) в Ds_Nsi из соответствующей
		//             строки(drB) Ds_Buffer 
		//
		// Параметры:  drB  - входная запись из "Ds_Buffer", drN - измененная запись в "Ds_Nsi" 
		//
		// Вызывается в блоке : sB_Save_Click
		//-------------------------------------------------------------------------------------
			
			drN["USERID"]  = uId ;               // Id_код пользователя 
			drN["LASTDATE"]= DateTime.Now ;                       //  и текущая дата изменения
			drN["IP_USER"] = RdaFunc.f_Ip_User(); // IP_ПК с которого выполненно обновление
			if (drB["OLDKEY"].ToString() == "-1")			
				drN["OLDKEY"]  = drB["OLDKEY"];                  // поле старого ключа записи-  для новой записи	
			switch (myTabl)
			{
				case "GRUSER": 
					#region НСИ групп
					drN["GR"]   = drB["GR"]   ;   // код группы
					drN["NAIM"] = drB["NAIM"] ;   // назва группы

					break ;				
					#endregion			
				
				case "NSIROAD":
					#region НСИ организаций
					int i = Convert.ToInt32(drB["ROAD"].ToString());
					drN["ROAD"]   = drB["ROAD"]   ; // код организации

					if (i >-1)
						drN["ROADF"] = f_IdRoad(i, Helpers.CommonFunctions.GetIDTable("NSIROAD")).ToString(); // Id_записи дороги в виде: 005RR0000000000 
					else drN["ROADF"]= "".ToString();
					drN["PD"]        = drB["PD"]     ;   // признак  группы
					drN["NAIM"]      = drB["NAIM"]   ;   // назва организации в И.П.
					drN["NAIMD"]     = drB["NAIMD"]  ;   // назва организации в Д.П.
					drN["NAIMF"]     = drB["NAIMF"]  ;   // Полное название дороги.
					drN["IP_SERV"]   = drB["IP_SERV"];   //  IP_сервера 
					drN["LIST_IDAC"] = drB["LIST_IDAC"]; // Список(из таблицы LIST_AC )  ID_кодов   в виде : RD, NF \ RD \  NF, указывающий какие АС доступны дороге 

					break;				
					#endregion			
			
				case "NSIOTDEL":
					#region НСИ отделов по организации
				
					drN["ROAD"]  = myRoad;   // код организации = myRoad
					drN["OTDEL"] = drB["OTDEL"];        // код отдела
					drN["NAIM"]  = drB["NAIM"] ;        // назва отдела
					drN["KOD"]   = drB["KOD"];          // шифр отдела

                    break ;				
					#endregion			
				
				case "R_ROADRESP":
					#region НСИ ответственных(за подписи документа,...) лиц организации
					drN["ROAD"]    = myRoad ;            // код организации = myRoad
					drN["OTDEL"]   = myOtdel ;           // код отдела      = myOtdel
					drN["POSITION"]= drB["POSITION"]   ; // Порядковый номер внутри записей ??????????
					drN["FIO"]     = drB["FIO"]        ; // Повне Ім`я (ПІБ) наприклад,  Петров  Петро Петрович  			
					drN["POSADA"]  = drB["POSADA"]     ; // Должность(посада)				
					drN["PHONE"]   = drB["PHONE"]      ; // Телефон				
					drN["PZ"]      = drB["PZ"]         ; // Признак: =1-исполнитель, 0 - ответственный за документ, 2-кому предзначен документ(или кто утверждает) и т.п.

					break ;				
					#endregion			
				
				default:
					break;
			}

		}

		// bB_Save_ItemClick - реакция на нажатие кнопки "Зберегти" - сохранение записи 
		private void bB_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_Save_ItemClick- метод используется при нажатии на кнопку "Зберегти зміни" для 
			//                    сохранения данных в Oracle_таблицах, если были изменения
			//-------------------------------------------------------------------------------------			
			//  Примечание : 
			//
			//  1. Изменение Oracle_таблиц GRUSER или NSIROAD может выполнять ТОЛЬКО администратор
			//     АС(myAccess=ALL) и только на сервере УЗ (RoadServ=5). 
			//     Все изменения реплицируются в таблицы БД RODUZ(АС РОДУЗ НФ) на сервера дорог
			//
			//  2. Изменение Oracle_таблиц NSIOTDEL или R_ROADRESP может выполнять как администратор
			//     АС(myAccess=ALL) так администратор(myAccess= "ADMIN") ДОРОГИ(RdaFunc.RoadUser =
			//     myRoad). Причем изменения разрешены как в АС РОДУЗ НФ на серверах УЗ так и дорог. 
			//       - изменения выполненные на сервере   УЗ   реплицируются на сервер дороги(myRoad)
			//         на сервер соответствующей дороги(myRoad)
			//       - изменения выполненные на сервере ДОРОГИ реплицируются на сервер УЗ
			//
			//  3. Информация о необходимости изменений в таблицах записывается в  таблицу 
			//     InfoUpdSrv на сервере  УЗ или ДОРОГИ(см.  p_UpdInfo). 
			//
			//  4. Так как метод bB_Save_ItemClick может быть вызван не только при нажатия кнопки  
			//     bB_Save, но и из метода f_ErrN, то в блоке осуществляется контроль на запрет 
			//     действия по сохранению изменений в Oracle_таблиц при bB_Save.Enabled=false
			//-------------------------------------------------------------------------------------
			bS_InfoLeft.Caption = "";
			p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения

			if (bB_Save.Enabled)  // Если кнопка сохранения доступна, то выполнить дейсвия
			{
				#region Объявление переменных и общие операции

				string sTxt1 = (RDA.RDF.sLang == "R") ? "Ошибка" : "Помилка";
				string sTxt2 = (RDA.RDF.sLang == "R") ? "Возникла ошибка при сохранении данных в таблице "
											: "Виникла помилка при збереженні данних до таблиці ";
				string sTxt3 = "\n\n" + RdaFunc.f_Repl(" ", 20)
											+ (RDA.RDF.sLang == "R" ? "Повторите изменения !" : "Повторите зміни !");
				string rModi = "";     // Признак записи : rModi ="I"-новая, ="U"-изменена, ="S"-без изменений,="D"-удалена
				string tKey = "";     // Значение ключа в таблице НСИ
				string KeyN = "";     // Значение ключа в таблице НСИ для  Ds_Nsi
				string KeyB = "";     // Значение ключа в таблице НСИ для  Ds_Buffer


				#endregion

				try
				{
					p_CtrlRowNsi();     // Если были изменения в текущей(tRow) записи --> bUpdBuff=true

					string txt1 = RDA.RDF.sLang == "R" ? " сохранение изменений !" : " зберегти зміни !";
					string txt2 = (RDA.RDF.sLang == "R" ? "Отменен " : "Відмінно") + txt1;
					bool bOk = f_ErrRowNsi(myTabl, txt2); // Если в записи есть ошибки (bOk=true), то не выполнять перехода

					if (bOk == false)
					{
						p_DsNsi();                  // Выбор данных из Oracle_таблиц НСИ в Ds_таблицу Ds_Nsi 

						#region Блок изменения-добавления строк соответствующего НСИ  и запись в Ds_Info, для обновлении на серверах
						tKey = (myTabl == "GRUSER" ? "GR"           // для НДІ групп 
							 : (myTabl == "NSIROAD" ? "ROAD"        // для НДІ підприємств
							 : (myTabl == "NSIOTDEL" ? "OTDEL"      // для НДІ відділів по підприємству
							 : (myTabl == "R_ROADRESP" ? "POSITION" // для НДІ відповідальних осіб предприятия
								 : ""))));

						foreach (DataRow drB in Ds.Tables["Ds_Buffer"].Rows)
						{
							rModi = drB["RMODI"].ToString(); // Признак записи : ="I"-новая, ="U"-изменена, ="S"-без изменений,="D"-удалена
							KeyB = drB["OLDKEY"].ToString(); //     для RMODI ="I" имеем  OLDKEY=-1
							if (rModi == "S") continue;        // Пропускаем  не изменённую запись

							#region rModi=="U" -> блок изменения  записи соответствующего НСИ в Ds_Nsi(для БД РОДУЗ)
							if (rModi == "U")                   // Запись изменена
							{

								DataRow[] drN = Ds.Tables["Ds_Nsi"].Select(string.Format("OLDKEY = '{0}'", KeyB));
								if (drN.Length > 0) // Запись найдена в  Ds_Nsi
								{
									#region формирование 2-х записей в Ds_Info при изменении поля_КЛЮЧА
									if (ModiRow == "")                       // Сформируем в Ds_Info строку 
									{
										if (KeyB != drB[tKey].ToString())    // Если был изменен ключ записи НСИ :
										{
											p_UpdInfo(drN[0], myTabl, myTabl, "D", 0);	// -удаление   записи на сервере УЗ или дороги			
											p_UpdInfo(drB, myTabl, myTabl, "I", 0);	// -добавление записи 		
										}
										else
											p_UpdInfo(drB, myTabl, myTabl, "U", 0);      	// -обновления записи 
									}
									#endregion

									p_UpdRowNsi(drB, drN[0]);    // Oбновление значения в полях записи Ds_Nsi из Ds_Buffer
									bUpdBuff = true;
								}


							}
							#endregion

							#region rModi=="I" -> блок добавления записи соответствующего НСИ в Ds_Nsi(для БД РОДУЗ)
							if (rModi == "I")     // Запись  новая в Ds_Buffer
							{
								string nModi = "I";

								DataRow[] drK = Ds.Tables["Ds_Nsi"].Select(string.Format("OLDKEY = '{0}'", drB[tKey].ToString()));

								#region Если для строки добавленной в Ds_Buffer  ЕСТЬ строка с этим же ключом  в Ds_Nsi
								if (drK.Length > 0) // да существует, т.е. в Ds_Buffer эта запись была удалена и вновь добавлена 
								{
									nModi = "U";  // Переустановим признак записи "I" на  "U"-изменена
									p_UpdRowNsi(drB, drK[0]);    // Oбновим значения в полях записи Ds_Nsi из Ds_Buffer
								}
								#endregion

								#region Если для строки добавленной в Ds_Buffer  НЕТ  строки с этим же ключом  в Ds_Nsi

								else                // нет строки с да существует, т.е. в Ds_Buffer эта запись была удалена и вновь добавлена 
								{
									DataRow drN = Ds.Tables["Ds_Nsi"].NewRow();  // Определим новую запись в Ds_Nsi
									p_UpdRowNsi(drB, drN);                       // Добавим значения в поля записи из Ds_Buffer
									Ds.Tables["Ds_Nsi"].Rows.Add(drN);           // Cохраним новую запись в Ds_Nsi

								}
								#endregion

								if (ModiRow == "") // Сформируем в Ds_Info строку о необходимости добавления или обновления
									p_UpdInfo(drB, myTabl, myTabl, nModi, 0);    // записи на сервере УЗ или дорог
								bUpdBuff = true;
							}
							#endregion
						}

						#endregion

						#region Блок удаления строк соответствующего НСИ в Ds_Nsi и запись в Ds_Info для обновлении на серверах

						foreach (DataRow drN in Ds.Tables["Ds_Nsi"].Rows)
						{
							KeyN = drN["OLDKEY"].ToString();  // № записи при загрузке НСИ 
							if (KeyN == "-1") continue;        // Новая запись полученная ранее из Ds_Buffer

							DataRow[] drB = Ds.Tables["Ds_Buffer"].Select(string.Format("OLDKEY = '{0}'", KeyN));
							if (drB.Length < 1 || drB[0]["RMODI"].ToString() == "D") // Запись подлежит удалению из  Ds_Nsi
							{
								if (ModiRow == "")  // Сформируем в Ds_Info строку о необходимости удаления записи
									p_UpdInfo(drN, myTabl, myTabl, "D", 0);	//  на сервере УЗ или дорог	
								bUpdBuff = true;
								drN.Delete();            // Пометим в Ds_Nsi запись на удаление
							}
						}
						#endregion

						#region Блок обновления Oracle_таблиц  из Ds_Nsi, Ds_Nsi_RD и Ds_Info
						if (bUpdBuff == true)
						{
							string sNDI = rI_NDI.GetDataSourceValue("NAIM", idxNSI).ToString();
							sTxt2 += sNDI; // На случай сбоя при обновлении(для ERR.Error err6 =...)
							bUpdBuff = false;                     // Установим признак отсутствия изменений 
							OraDANsi.Update(Ds, "Ds_Nsi");       // Обновим данные из Ds_Nsi в Oracle_таблице БД РОДУЗ НФ

							if (ModiRow == "")
								RdaFunc.p_SaveIntoInfo(null, null, null, null, null, 2);

							if (sender != null) // Если была нажата кн. bB_Save, то :
							{
								//int idx = 0;     
								//if(E_Road.Visible) idx = E_Road.ItemIndex; //  сохраним индекс  дороги
								bE_NDI_EditValueChanged(null, null); // -вновь перечитаем НСИ из Oracle в Ds_Buffer и Ds_Nsi
								bE_Road.EditValue = myRoad;
								//E_NDI_SelectedIndexChanged(null,null);   

								//if(E_Road.Visible) E_Road.ItemIndex=idx;  // -возвратимся в текущее окно
							}
						}

						#endregion

						bB_Save.Enabled = false;  // Деактивизация кнопок : "Зберегти зміни" 
						bUpdBuff = false;         // Установим признак отсутствия изменений 
					}
				}
				catch (Exception ex)
				{

					ERR.Error err6 = new ERR.Error(sTxt1, ERR.ErrorImages.CryticalError, sTxt2 + sTxt3, ex.ToString(), 1);
					err6.ShowDialog();
				}
			}

		}

		// bB_AddRow_ItemClick - реакция на нажатие кнопки "Нова запис" - добавление записи 
		private void bB_AddRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_AddRow_ItemClick - метод используется при нажатии на кнопку "Нова запис" для 
			//                      создания и заполнения некоторых полей  записи в Ds_таблицах НСИ
			//		Дополнительные поля для Ds_таблицы : 
			//	 RMODI - признак записи: ="I"-новая, ="U"-изменена, ="S"-без изменений
			//	 RKEY  - признак изменения ключа или удаления записи : 
			//           =0-разрешено, =1-запрещено, так как ключ используется в других таблицах 
			//-------------------------------------------------------------------------------------
			//sB_Exit.Focus();
			string txt2 = (RDA.RDF.sLang == "R") ? "Отменено создание новой записи !"
										 : "Скасовано створення нового запису !";
			if (f_ErrRowNsi(myTabl, txt2))
				return;                 // Если в предыдущей записи ошибки-отменим создание новой
			else
			{
				DataRow drN = Ds.Tables["Ds_Buffer"].NewRow();    // Определим новую запись :

				drN["RMODI"]    = "I";            //  признак "I"-новая запись
				drN["RKEY"]     = 0;              //  не используется в дочерних таблицах
				drN["NUSER"]    = uName;          //  имя пользователя системы - кем добавляется запись
				drN["USERID"]   = uId;            //               и его Id_код 
				drN["LASTDATE"] = DateTime.Now; ; //  текущая дата
				drN["OLDKEY"]   = -1;             //  поле старого ключа записи: GR,ROAD, OTDEL или POSITION для новой записи

				#region Блок установления значений в поля записи соответствующего НСИ 

				switch (myTabl)
				{
					case "GRUSER":
						#region НДІ групп пользователей

						drN["GR"] = -2;         // код группы
						drN["NAIM"] = "*";      // назва группы
						break;

						#endregion

					case "NSIROAD":
						#region НДІ підприємств

						drN["ROAD"]    = -2;     // код организации
						drN["IP_SERV"] = "";     //  IP_сервера 
						drN["NAIM"]    = "*";    // назва организации в И.П.
						drN["NAIMD"]   = "*";    // назва организации в Д.П.
						drN["NAIMF"]   = "";     // Полное название дороги.
						drN["PD"]      = -2;     // Признак  группы
						break;

						#endregion

					case "NSIOTDEL":
						#region НДІ відділів по підприємству

						drN["ROAD"]  = myRoad;   // код организации
						drN["KOD"]   = "*";      // шифр отдела
						drN["OTDEL"] = -2;       // код отдела
						drN["NAIM"]  = "*";      // назва отдела
						break;

						#endregion

					case "R_ROADRESP":
						#region НДІ відповідальних осіб предприятия

						drN["ROAD"]  = myRoad;   // код организации
						drN["OTDEL"] = myOtdel;  // код отдела
						drN["POSITION"] = -2;    // Порядковый номер внутри записей
						drN["FIO"]   = "*";      // Повне Ім`я (ПІБ) наприклад,  Петров  Петро Петрович  			
						drN["POSADA"]= "*";      // Должность(посада)				
						drN["PHONE"] = "";       // Телефон				
						drN["PZ"]    = 0;        // Признак: =1-исполнитель, 0 - ответственный за документ, 2-кому предзначен документ(или кто утверждает) и т.п.
						break;

						#endregion
					default:
						break;
				}

				#endregion

				Ds.Tables["Ds_Buffer"].Rows.Add(drN);    // Cохраним новую запись в "Ds_Buffer"
				p_FooterBarInfo(drN);                     // Информация о статусе текущей записи 

				tRow = myView.RowCount - 1;              // Определим номер новай записи 
				myView.RefreshData();                    // Обновим вид View на экране
				p_RowFocus();
			}

		}

		//bB_CancelEditRow_ItemClick - реакция на нажатие кнопки "Відмінити змін" - отмена добавления или изменения записи 
		private void bB_CancelEditRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//bB_CancelEditRow_ItemClick - метод используется при нажатии на кнопку "Відмінити змін "  для
			//                         отмены добавления новой либо  изменений старой записи
			//
			//	dr3["RMODI"]-признак записи : rModi ="I"-новая, ="U"-изменена,="S"-без изменений
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
			if (tRow > -1)
			{
				DataRow dr3 = Ds.Tables["Ds_Buffer"].Rows[tRow];
				p_FooterBarInfo(dr3);    // Информация о статусе текущей записи 

				dr3.RejectChanges();    // Отменяет все изменения в строке до последнего AcceptChanges 
			}
		}

		// bB_DelRow_ItemClick - реакция на нажатие кнопки "Видалити запис" - удаление записи 
		private void bB_DelRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_DelRow_ItemClic - метод используется при нажатии на кнопку "Видалити запис"  
			//
			//	Дополнительные поля для Ds_таблицы : 
			//	 RMODI - признак записи: ="I"-новая, ="U"-изменена, ="S"-без изменений
			//	 RKEY  - признак изменения ключа или удаления записи : 
			//           =0-разрешено, =1-запрещено, так как ключ используется в других таблицах 
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения

			//sB_Exit.Focus(); // Переводим фокус в поле выхода
			if (tRow > -1)
			{
				DataRow dr4 = Ds.Tables["Ds_Buffer"].Rows[tRow];
				if (dr4["RKEY"].ToString() == "0")      // Если разрешено удаление 
				{
					if (dr4["RMODI"].ToString() != "I") // - и запись не новая, то установим
					{
						bUpdBuff        = true;            //       признак для сохранения в БД
						bB_Save.Enabled = true;         // ДAктивизация кнопок : "Зберегти" 
						dr4["RMODI"] = "D".ToString();
					}
					p_FooterBarInfo(dr4);              // Информация о статусе текущей записи 

					dr4.AcceptChanges();              // Приведем в соответствие ...View и строку Ds_таблицы
					dr4.Delete();                     // - пометим ee на удаление
					dr4.AcceptChanges();              // Приведем в соответствие ...View и строку Ds_таблицы
					if (myView.FocusedRowHandle > -1)
					{
						tRow = Convert.ToInt32(myView.FocusedRowHandle.ToString());
						p_RowFocus();
					}
				}
				else bNsiErr = f_ErrRowNsi("ERRDEL", "");
				bNsiErr = false;                      // Установим признак отсутствия ошибки в записи Ds_Buffer 
			}
		}
		
		// FRMNSI_FormClosing - контроль ПЕРЕД закрытием формы
        private void FRMNSI_FormClosing(object sender, FormClosingEventArgs e)
        {
			//-------------------------------------------------------------------------------------
			//FRMNSI_FormClosing - метод используется при нажатии на кнопку формы "Х" для 
			//                     выполнения действий ПЕРЕД закрытием формы. Проверяется  выполнено 
			//                     ли сохранение ("Зберегти") изменений
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
			string txt1 = RDA.RDF.sLang == "R" ? " закрытие формы " : " закриття формі ";
			string txt2=  RDA.RDF.sLang == "R" ? "Отмененo "          :"Відмінно "; 

			p_CtrlRowNsi(); // Если были изменения в текущей(tRow) записи --> bUpdBuff=true
	
			bool bOk  = f_ErrRowNsi(myTabl, txt2); // Если в записи есть ошибки (bOk=true), то не выполнять перехода
			if ( bOk == false && bUpdBuff )
				 bOk  = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "В `" : "У  `") + myView.GroupPanelText + "`", "", "BUFF");
			e.Cancel  =  bOk  ;

        }

		//bB_Exit_ItemClick - действия при акрытии формы
		private void bB_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//bB_Exit_ItemClick- метод используется при нажатии на кнопку "Вихід" для закрытия формы
			//-------------------------------------------------------------------------------------
			this.Close();  

		}
		
	
		#endregion	
	
		#region Методы, связанные с обработкой НСИ групп(GrUser), дорог(NsiRoad), отделов(NsiOtdel) и лиц, ответственных за документ(R_RoadResp) 
	
		private bool f_ErrRowNsi(string par1, string par2)
		{	
		//-------------------------------------------------------------------------------------
		// f_ErrRowNsi-функция обеспечивает контроль в записях grid1(...View1-8) и grid3(...View5)
		//		на:   - не допущение дублирования уникальных ключей
		//            - ошибок при заполнении обязательных полей
		// При возникновении ошибки возвращается true и устанавливается bNsiErr = true
		// Если RDA.RDF.sLang ="R"-выдача сообщений на русском иначе -на украинском("U") языке
		//-------------------------------------------------------------------------------------
		// Параметры: par1= "ERRADD" -добавление, ="ERRMODI"-изменение,="ERRDEL"-удаление записи,
		//                = myTabl-имя таблицы(где контролируется запись) определяется в блоке
		//                         E_NDI_SelectedIndexChanged
		// par2= "" или информация об отмене действия(например, "отменяется выбор нового НСИ")
		//-------------------------------------------------------------------------------------
		// Функция вызывается в блоках: sB_DelRow_Click,      gridView_BeforeLeaveRow
		//                              gridView_ValidateRow, gridView_CellValueChanged
		//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
			bNsiErr = false;    // Признак отсутствия ошибки в записи Ds_Buffer 
			int iE   = 0;
			int j    = 0;
			string sTxt1 = (RDA.RDF.sLang=="R") ? "Ошибка в записи" : "Помилка в запису";
			string sTxt2 = "" ;
			string sTxt3 = "\n" +RdaFunc.f_Repl(" ",20)+
						(RDA.RDF.sLang=="R" ? "Исправьте ошибку и продолжите работу."
									: "Виправте помилку і продовжить роботу.");
			try
			{
				#region Выбор значения par1
				switch (par1.ToUpper().Trim())
				{
					case "GRUSER": 
						#region Контроль в НСИ групп
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							j = 0;
							iE = Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"GR").ToString());
							if (iE < 0) 	sTxt2 = (RDA.RDF.sLang=="R" ? " - недопустимое значение  ключа" 
												: " - неприпустиме значення ключа" )+" код="+iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt16(drN["GR"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - дублирование ключа" 
												: " - дублювання ключа")+" код="+iE.ToString()+"\n";
											break;
										}
									}
								}
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString().Trim()=="")
								sTxt2+= (RDA.RDF.sLang=="R" ? " - недопустимое значение в поле" 
									: " - неприпустиме значення в поле" )+" `НАЗВА`" ;
							sTxt3 += "\n\n"+par2;
						}
					
						break ;	
						#endregion			
					case "NSIROAD":
						#region Контроль в НСИ организаций
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							j = 0;
							iE = Convert.ToInt32(myView.GetRowCellValue(myView.FocusedRowHandle,"ROAD").ToString());
							if (iE < -1) 	sTxt2 = (RDA.RDF.sLang=="R" ? " - недопустимое значение ключа" 
												: " - неприпустиме значення ключа" )+" код="+iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt32(drN["ROAD"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - дублирование ключа" 
												: " - дублювання ключа")+" код="+iE.ToString()+"\n";
											break;
										}
									}
								}

							if (myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString() =="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString().Trim() ==""||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIMD").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIMD").ToString().Trim()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - недопустимое значение в поле" 
									: " - неприпустиме значення в поле" )+" `НАЗВА`"+"\n" ;
							if(Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"PD").ToString())==-2)
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - недопустимое значение в поле" 
									: " - неприпустиме значення в поле" )+" `ГРУПА`" ;
							sTxt3 += "\n"+par2;
			
						}
						break ;				
						#endregion			
					case "NSIOTDEL":
						#region Контроль в НСИ отделов по организации
				
						j = 0;
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							iE = Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"OTDEL").ToString());
							if (iE < 0) 	sTxt2 = (RDA.RDF.sLang=="R" ? " - недопустимое  № отдела " 
												: " - неприпустиме  № відділу " )+" код="+iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt16(drN["OTDEL"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - дублирование № отдела" 
												: " - дублювання № відділу ")+" код="+iE.ToString()+"\n";
											break;
										}
									}
								}
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"NAIM").ToString().Trim()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - недопустимое значение в поле" 
									: " - неприпустиме значення в поле" )+" `НАЗВА`"+"\n" ;
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"KOD").ToString()=="*")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - недопустимое значение в поле" 
									: " - неприпустиме значення в поле" )+" `ЩИФР`" ;
							sTxt3 += "\n"+par2;

						}
						break ;				
						#endregion			
					case "R_ROADRESP":
						#region Контроль в НСИ ответственных(за подписи документа,...) лиц организации
						j = 0;
						if(myTabl==OldTabl && myView.FocusedRowHandle > -1)
						{
							iE = Convert.ToInt16(myView.GetRowCellValue(myView.FocusedRowHandle,"POSITION").ToString());
							if (iE < 0)  sTxt2 = (RDA.RDF.sLang=="R" ? " - недопустимое значение" : " - неприпустиме значення" )
												+ " в поле №  = " +iE.ToString()+"\n";
							else
								foreach(DataRow drN in Ds.Tables["Ds_Buffer"].Rows) 
								{
									if(drN.RowState.ToString().ToUpper()!="DELETED")
									{
										j += Convert.ToInt16(drN["POSITION"].ToString())==iE ? 1 : 0; 
										if(j > 1)
										{
											sTxt2 = (RDA.RDF.sLang=="R" ? " - дублирование" : " - дублювання")
												+ " в поле № = "+iE.ToString()+"\n";
											break;
										}
									}
								}
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"FIO").ToString()=="*"  ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"FIO").ToString().Trim()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - недопустимое значение " : " - неприпустиме значення ")
									+ " в поле `ПІБ`"+"\n" ;
							if (myView.GetRowCellValue(myView.FocusedRowHandle,"POSADA").ToString()=="*" ||
								myView.GetRowCellValue(myView.FocusedRowHandle,"POSADA").ToString()=="")
								sTxt2 +=(RDA.RDF.sLang=="R" ? " - недопустимое значение " : " - неприпустиме значення ")
									+" в поле `ПОСАДА`" ;
							sTxt3 += "\n"+par2;
		
						}
					
					
						break ;				
						#endregion			
					case "ERRADD":
						#region Текст ошибки при добавлении записи
						sTxt1 =(RDA.RDF.sLang=="R") ? "Ошибка при добавлении записи": "Помилка при додаванні запису";
							
						break ;	
						#endregion			
					case "ERRMODI":
						#region Текст ошибки при изменении записи
						sTxt1 =(RDA.RDF.sLang=="R") ? "Ошибка при изменении записи" : "Помилка при змін запису";
						sTxt2 = (RDA.RDF.sLang == "R") ? "Ключ записи " + myKey.ToString() + " используется в других таблицах"
								     			: "Ключ запису " + myKey.ToString() + " використовується в інших таблицях";
						break;	
						#endregion			
					case "ERRDEL":
						#region Текст ошибки при удалении записи
						sTxt1 =(RDA.RDF.sLang=="R") ? "Ошибка при удалении записи": "Помилка при видаленні запису";
						sTxt2 =(RDA.RDF.sLang=="R") ? "Ключ записи "+myKey.ToString()+" используется в других таблицах" 
											: "Ключ запису "+myKey.ToString()+" використовується в інших таблицях";
						
						break ;	
						#endregion		
					case "ERRCELL":
						#region Текст ошибки в ячейке записи
						sTxt1 = RDA.RDF.sLang =="R" ? "Ошибка в поле записи": "Помилка в поле запису";
						sTxt2 = RDA.RDF.sLang =="R" ? " - недопустимое значение в поле " 
											: " - неприпустиме значення в поле " ;
						sTxt3 += "\n\n"+par2;
						break ;	
						#endregion			

					default: 
						sTxt2 = "" ;
						break ;				
				}
				#endregion			

				#region  Формирование сообщения об ошибке
				if(sTxt2 != "")
				{
					bNsiErr = true;  // Признак наличия ошибки в записи Ds_Buffer 
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

		// p_CtrlRowNsi - контроль на изменение записи НСИ в Ds_Buffer
		private void p_CtrlRowNsi()
		{
			//-------------------------------------------------------------------------------------
			// p_CtrlRowNsi - метод используется для контроля на изменение записи НСИ в Ds_Buffer
			//-------------------------------------------------------------------------------------
			if(tRow >- 1 && RowOld != "" && bCtrlUpd == false)
			{
				try
				{
					RowNew      = "";   
					DataRow drC = Ds.Tables["Ds_Buffer"].Rows[tRow]; // определим запись 
					RowNew      = string.Concat(@drC.ItemArray);     //  и сохраним изменения 
					if (@RowOld != @RowNew )                         // Если запись изменена :
					{
						if (drC["RMODI"].ToString()!="I")            // - и если запись не новая, то
							drC["RMODI"] = "U".ToString();           //     установим значение ="U"-изменена,

						bB_Save.Enabled = true;                      // Aактивизация кнопок : "Зберегти зміни" 
						bUpdBuff = true;                             // - установим признак изменения
						p_FooterBarInfo(drC);                        // Информация о статусе текущей записи 
					}
					bCtrlUpd = true; // Признак проведения контроля на изменение записи в Ds_Buffer 
				}
				catch
				{
				}
			}
		}

		// p_NewOtdel - Выбор данных из NSIOtdel  для отображения и\или изменения списка отделов по предприятию
		private void p_NewOtdel()
		{	
		//-------------------------------------------------------------------------------------		
		// p_NewOtdel - метод используется для выборка из NSIOtdel списка отделов по предприятию
		//		Дополнительные поля для Ds_таблицы : 
		//	-rModi - признак записи: ="I"-новая, ="U"-изменена, ="S"-без изменений
		//  -OldKey- старый код отдела для обновления в БД 	
		//	-rKey  - признак изменения ключа или удаления записи : =0-разрешено,=1-запрещено 
		// 	
		//   Для rKey  функция f_IsUsedKeyOtdel(a_Road, a_Otdel) возвращает 1, если значение 
		// КОДА: a_Road,  a_Otdel родительской таблицы NsiRoad имеется в таблицe R_RoadResp
		// или КОД отдела a_Otdel=100,101,102 имеется в  таблицe NsiOtdel 
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
				myView.GroupPanelText="НДІ відділів " ;
				
				myView.GroupPanelText +=" по " + rI_Road.GetDataSourceValue("NAIMD",idxRoad).ToString();
					
				//   - дополнение к названию зализниц
				myView.GroupPanelText +=
					rI_Road.GetDataSourceValue("PD", idxRoad).ToString() == "1" ? " залізниці" : "";
					
				p_ViewPage1() ; // Отображаем на экране вид(myView=advBandedGridView3) 
			}
			catch 	{}
		}

		// p_NewResp - Выбор данных из R_RoadResp для отображения и\или изменения списка ответственных лиц отдела
		private void p_NewResp()
		{	
			//-------------------------------------------------------------------------------------		
			// p_NewResp-метод используется для выборка из R_RoadResp списка ответственных лиц отдела
			//		Дополнительные поля для Ds_таблицы : 
			//	-rModi - признак записи: ="I"-новая, ="U"-изменена, ="S"-без изменений
			//  -OldKey- старый код Position для обновления в БД 	
			//	-rKey  - признак изменения ключа или удаления записи : =0-разрешено, =1-запрещено
			//        
			//   Для rKey  функция f_IsUsedKeyResp(a_Road, a_Otdel,a_Position) возвращает 1,  
			// если значение КОДА: a_Road, a_Otdel, a_Position родительской таблицы R_RoadResp
			// имеется в в дочерней таблицe Doc_Resp_Arh
			//-------------------------------------------------------------------------------------		
			try
			{
				OraDABuffer = new OracleDataAdapter(@" SELECT 0 as OldKey,'S' as rModi, "
					+@" RD.f_IsUsedKeyResp("+ myRoad.ToString()+", "+ myOtdel.ToString() + ",t.Position ) as rKey, "
					+@"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser, t.* FROM R_RoadResp t"
					+@" WHERE t.Road ="+myRoad.ToString()+" AND t.Otdel="+myOtdel.ToString()
					+@" ORDER BY Road, Otdel,Position", RDA.RDF.BDOraCon); 
				myView = advBandedGridView4;
		
				myErr =(myOtdel > -1)? "" : "OTDEL"; // Для сообщение об отсутствии данных в NsiOtdel
				myView.GroupPanelText = "Список відповідальних осіб відділу ";
				myView.GroupPanelText += (myOtdel > -1) ? rI_Otdel.GetDataSourceValue("KOD", idxOtdel).ToString() : "***";
				myView.GroupPanelText += " по " + rI_Road.GetDataSourceValue("NAIMD", idxRoad).ToString();
				//   - дополнение к названию зализниц
				myView.GroupPanelText +=
					rI_Road.GetDataSourceValue("PD", idxRoad).ToString() == "1" ? " залізниці" : "";

				p_ViewPage1() ; // Отображаем на экране вид myView=advBandedGridView4 
			}
			catch 	{}

		}

		// p_NewNsi - Выбор данных НСИ в Ds_Buffer для отображение и\или изменения списка 
		private void p_NewNsi()
		{	
			//-------------------------------------------------------------------------------------		
			// p_NewNsi - метод используется для заполнения Ds_таблицы(Ds_Buffer) данными из 
			//			  Oracle_таблицы НСИ.  Необходимое НСИ выбирается из списка bE_NDI и далее 
			//			  определяется через индекс --> Convert.ToInt16(bE_NDI.EditValue.ToString().
			//		Дополнительные поля для Ds_таблицы : 
			//	-rModi - признак записи: ="I"-новая, ="U"-изменена, ="S"-без изменений
			//	-rKey  - признак изменения ключа или удаления записи : =0-разрешено, 
			//            =1-запрещено, так как ключ используется в других таблицах 
			//-------------------------------------------------------------------------------------		
			try
			{
				switch (myTabl)
				{
					case "GRUSER":
 						#region НДІ групп пользователей
						//---------------------------------------------------------------------		
						//   Функция f_IsUsedKeyGrUser(a_Gr) возвращает 1, если значение КОДА
						// a_Gr родительской таблицы GrUser имеется хотя бы в одной из дочерних 
						// таблиц : NsiRoad, R_GrUser
						//----------------------------------------------------------------------		

						OraDABuffer = new OracleDataAdapter(@" SELECT  0 as OldKey,'S' as rModi, "
							+ @" RD.f_IsUsedKeyGrUser(t.Gr) as rKey, "
							+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser,"
							+ @" t.* FROM GrUser t  ORDER BY Gr", RDA.RDF.BDOraCon);
						myView = advBandedGridView2; // в myView сохраним экземпляр класса ... AdvBandedGridView для НДІ груп
						p_ViewPage1();              // отобразим на экране(grid1) вид(myView=advBandedGridView*) 
						break;
						#endregion

					case "NSIROAD":
						#region НДІ підприємств
						//---------------------------------------------------------------------		
						//   Функция f_IsUsedKeyRoad(a_Road) возвращает 1, если значение КОДА
						// a_Road родительской таблицы NsiRoad имеется хотя бы в одной из 
						// дочерних таблиц : AcUser, NsiOtdel, NsiDeptRoad
						//----------------------------------------------------------------------		

						OraDABuffer = new OracleDataAdapter(@" SELECT 0 as OldKey,'S' as rModi,"
							+ @" RD.f_IsUsedKeyRoad(t.Road) as rKey, "
							+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser,"
							+ @" t.* FROM  NsiRoad t WHERE t.Road > -1 ORDER BY Road", RDA.RDF.BDOraCon);
						myView = advBandedGridView1;
						p_ViewPage1(); // отобразим на экране(grid1) вид(myView=advBandedGridView*) 
						break;
						#endregion

					case "NSIOTDEL":
						#region НДІ відділів (p_ReadRoad--> p_NewOtdel) по підприємству

						p_LoadRoad(); // выберем список отделов(блок p_NewOtdel)
						break;
						#endregion			

					case "R_ROADRESP":
						#region НДІ відповідальних осіб(p_ReadRoad--> p_ReadOtdel--> p_NewResp) по отделам предприятия

						p_LoadRoad(); // выберем список відповідальних осіб (блок p_NewResp)
						break;
						#endregion
					default:
						break;
				}
		
			}
			catch { }
		}

		// p_ViewPage1 - метод используется для отображения соответствующего НСИ 
		private void p_ViewPage1()
		{
			//-------------------------------------------------------------------------------------
			// p_ViewPage1 - метод используется для отображения на экране вида(advBandedGridView*) 
			//		 	     соответствующего НСИ, загруженного в Ds_Buffer и  установки признака 
			//			     bEditTabl=true - разрешения на редактирование таблицы в БД РОДУЗ НФ. 
			//-------------------------------------------------------------------------------------
			//  Примечания. 
			//  1. Изменение Oracle_таблиц GRUSER или NSIROAD может выполнять ТОЛЬКО администратор
			//     АС(myAccess=ALL) и только на сервере УЗ(RoadServ=5). 
			//
			//  2. Изменение Oracle_таблиц NSIOTDEL или R_ROADRESP может выполнять как администратор
			//     АС(myAccess=ALL) так администратор(myAccess= "ADMIN") ДОРОГИ(RdaFunc.RoadUser =
			//     myRoad). Изменения разрешены в БД РОДУЗ НФ как на сервере УЗ так и серверах дорог 
			//     Причем администратор ДОРОГИ может внести изменения только для своей дороги.
			//
			//	3. myView = advBandedGridView* - хранит экземпляр класса...AdvBandedGridView
			//
			//	4. В поле "RKEY" устанавливается признак на изменение ключа или удаление записи : 
			//     =0-разрешено, = 1-запрещено(так как ключ используется в дочерних таблицах) 
			//-------------------------------------------------------------------------------------
			bUpdBuff        = false; // Установим признак - нет изменений в Ds_Buffer
			bB_Save.Enabled = false; // Деактивизация кнопок : "Зберегти зміни" 
			string tKey = "";
			try
			{
				#region Заполнение Ds_Buffer и установка признака(bEditTabl) запрета-разрешения на редактирование таблицы  


				grid1.DataSource=null;            // очистим  старые отображения в гриде
				
				Ds.Tables["Ds_Buffer"].Dispose(); // освободим все ресурсы 
				Ds.Tables["Ds_Buffer"].Clear();   // очистим   все данные в таблице    
				OraDABuffer.Fill(Ds,"Ds_Buffer"); // заполним  Ds_таблицу (Ds_Buffer) данными НСИ

				//if ((myAccess  == "ALL" && RDA.RDF.UserParam.RoadServ == 5 && (myTabl == "GRUSER" || myTabl == "NSIROAD"))
				//||  ((myAccess == "ALL" || (myAccess == "ADMIN"  && myRoad == RdaFunc.RoadUser))
				//&&  (myTabl == "NSIOTDEL" || myTabl == "R_ROADRESP"))) 


				if (((myTabl   == "GRUSER" || myTabl == "NSIROAD")    &&      // НСИ групп и дорог может редактировать 
				     (myAccess == "ALL" && RDA.RDF.UserParam.RoadServ == 5))  //  админ АС и только на сервере УЗ

				||  (( myTabl    == "NSIOTDEL" || myTabl == "R_ROADRESP") &&  // НСИ отделов и пользователей по дорогам могут редактировать :
					 (  myAccess == "ALL"    ||                               // - админ АС
			          ( myAccess == "ADMIN"  ||                               // администратор БД дороги
			           (myAccess == "USER" && RDA.RDF.UserParam.Otdel == 102) //  или пользователь дороги отдела 102 -НФДУ
					  )          && myRoad == RDA.RDF.UserParam.RoadServ      //  и только на сервере дороги
					 )
					)
				   ) 

					bEditTabl = true;    // Признак разрешения на редактирование таблицы

				else
					bEditTabl   = false;   // Признак запрета на редактирование таблицы

				#endregion			

				if ( Ds.Tables["Ds_Buffer"].Rows.Count > 0 )   // Если есть данные в таблице 
				{
					if (bEditTabl)                           //      и разрешено изменение  
					{
						#region  Запись ключа строки в поле OLDKEY
						tKey = (myTabl == "GRUSER" ? "GR"           // для НДІ групп 
							 : (myTabl == "NSIROAD" ? "ROAD"        // для  НДІ підприємств
							 : (myTabl == "NSIOTDEL" ? "OTDEL"      // для НДІ відділів по підприємству
							 : (myTabl == "R_ROADRESP" ? "POSITION" // для НДІ відповідальних осіб предприятия
							 : ""))));
						foreach (DataRow dr in Ds.Tables["Ds_Buffer"].Rows)
						{
							dr["OLDKEY"] = dr[tKey];
						}

						Ds.Tables["Ds_Buffer"].AcceptChanges(); // Установка статуса записей в Unchanged 
						#endregion
					}
				}
				else p_ErrRead(myErr); //  иначе, сообщим об отсутствии данных

			}
			catch(Exception ex)
			{
				ERR.Error err6 = new ERR.Error("Помилка", ERR.ErrorImages.Examplemation, "?????", ex.ToString(), 1);
				err6.ShowDialog() ;
			}
			this.grid1.MainView   = myView;   // Устанавим вид как главный(MainView)
			this.grid1.DataSource = Ds.Tables["Ds_Buffer"];// и отобразим данные в гриде
			// Переустановка фокуса после перехода на новую ...View...
			gridView_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

			p_NSDClick();                     // Де\Активируем кнопки: sB_Save и т.п.
		}

		// p_RowFocus - метод используется ПОСЛЕ установки фокуса на строку в grid1(...View1-4)
		private void p_RowFocus()
		{
			//-------------------------------------------------------------------------------------
			// p_RowFocus - метод используется ПОСЛЕ установки фокуса на строку в grid1(...View1-4)
			//              для запрета\разрешения установки фокуса на ключ записи  
			//
			// Обеспечивает cохранение для дальнейшего использования :
			//    - в rKey  - признака запрета\разрешения обработки ключа или удаления записи:
			//              = 0-разрешено, =1-запрещено, т.к ключ используется в других таблицах 
			//    - в rModi - признака записи : ="I"-новая, ="U"-изменена, ="S"-без изменений
			//    - в RowObj- массива значений полей выбранной строки до её изменения
			//    - в RowOld- строку  значение полей выбранной строки до её изменения
			//
			// Вызывается в блоках: gridView_FocusedRowChanged, sB_AddRow_Click и sB_DelRow_Click 
			//                      где предварительно определёно значение tRow-номера записи
			//-------------------------------------------------------------------------------------

			RowOld = "";
			RowNew = "";
			if (tRow >-1)
			{
				OldTabl= myTabl;                  // Сохраним имя обрабатываемой таблицы 
				RowNew = "";   
				myKey  = "" ;
				DataRow dr1 = Ds.Tables["Ds_Buffer"].Rows[tRow];
				p_FooterBarInfo(dr1);              // Информация о статусе текущей записи 

				rKey = Convert.ToInt16(dr1["RKEY"].ToString());
				RowObj = dr1.ItemArray;          // Массив элементов старой строки для восстановления
				RowOld = string.Concat(@RowObj); // Сохраним cтарую запись для сравнения
	
				#region Блок определения запрета на изменение ключа или удаления записи в НСИ

				try
				{
					switch (myTabl)
					{
						case "GRUSER":
							#region НДІ групп пользователей
							myKey = "GR = " + dr1["GR"].ToString();          // Код группы
							if (rKey == 1)                                   // Т.к. ключ GR используется,то
								 this.v2C1.OptionsColumn.AllowFocus = false; //  запретить удаление или изменение GR
							else this.v2C1.OptionsColumn.AllowFocus = true;  //   иначе разрешить
							break;
							#endregion

						case "NSIROAD":
							#region НДІ підприємств
							myKey = "ROAD = " + dr1["ROAD"].ToString();      // Код дороги
							if (rKey == 1)                                   // Т.к. ключ Road используется,то
								 this.v1C1.OptionsColumn.AllowFocus = false; //  запретить удаление или изменение Road
							else this.v1C1.OptionsColumn.AllowFocus = true;  //  иначе разрешить
							break;
							#endregion

						case "NSIOTDEL":
							#region НДІ відділів  по підприємству
							myKey = "OTDEL = " + dr1["OTDEL"].ToString();    // Код отдела
							if (rKey == 1)                                   // Т.к. ключ Road используется,то
							{
								this.v3C2.OptionsColumn.AllowFocus = false; //  запретить удаление или изменение Otdel
								if (dr1["OTDEL"].ToString()=="100" || dr1["OTDEL"].ToString()=="101" || dr1["OTDEL"].ToString()=="102")
									 this.v3C1.OptionsColumn.AllowFocus = false;  // Поле Kod-> шифр отделов НФЛ(100),НФГ(101), НФДУ(102) заменить нельзя 
								else this.v3C1.OptionsColumn.AllowFocus = true; 
							}
							else 
							{
								this.v3C2.OptionsColumn.AllowFocus = true;   //  иначе разрешить
								this.v3C1.OptionsColumn.AllowFocus = true; ; //  шифр отделов 100(НФЛ),101(НФГ), 102(НФДУ) заменить нельзя 
							}
							break;
							#endregion

						case "R_ROADRESP":
							#region НДІ відповідальних осіб по отделам предприятия
							myKey = "POSITION = " + dr1["POSITION"].ToString();    // Код відповідальних осіб 
							if (rKey == 1)                                   // Т.к. ключ Road используется,то
								 this.v4C5.OptionsColumn.AllowFocus = false; //  запретить удаление или изменение Road
							else this.v4C5.OptionsColumn.AllowFocus = true;  //  иначе разрешить

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

		// gridView_FocusedRowChanged - метод  используется ПОСЛЕ установки фокуса на строку 
		private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// gridView_FocusedRowChanged - метод  используется ПОСЛЕ установки фокуса на строку 
			//                              в grid1(...View1-8) 
			//   в tRow - сохраняется номер записи
			//-------------------------------------------------------------------------------------
			bCtrlUpd  = false; // Признак отсутствия контроля на изменение записи в Ds_Buffer 
		
			if(myView.FocusedRowHandle >-1)
			{
				tRow  = Convert.ToInt16((myView.GetSelectedRows()).GetValue(0).ToString());
				
				p_RowFocus();
			}
		}

		// gridView_BeforeLeaveRow - метод используется ПEPEД потерей фокуса в строке грида и 
		private void gridView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// gridView_BeforeLeaveRow - метод используется ПEPEД потерей фокуса в строке грида и 
			//                           если запись не удалена, а изменена или добавлена, то :
			//  -при отсутствии ошибок устанавливается признак(bUpdBuff=true) для сохранения записи
			//	-иначе, e.Allow = false для отмены перехода на следующую запись           
			//-------------------------------------------------------------------------------------
			try
			{
				string txt2=(RDA.RDF.sLang=="R") ? "Отменен выход с  записи !"	: "Відмінно вихід з запису !";
				if (f_ErrRowNsi(myTabl, txt2)) 	// Если в записи есть ошибки, то возвратим  
					e.Allow = false ;           //             bNsiErr=true и отменим переход 
				else                            // Иначе, eсли были изменения в текущей(tRow)
					p_CtrlRowNsi();             //        записи, то установим  bUpdBuff=true
			}
			catch
			{
			}
		}

		// gridView_CellValueChanged - метод используется ПОСЛЕ обновления данных в ячейке 
		private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// gridView_CellValueChanged - метод используется ПОСЛЕ обновления данных в ячейке для 
			//                             установки признака(bCtrlUpd=false) необходимости вызова  
			//                             метода p_CtrlRowNsi - контроля на изменение записи
			//-------------------------------------------------------------------------------------
			bCtrlUpd = false;
		}

		// rItC_PZ_EditValueChanging - ввод и контроль допустимых значений поля PZ в ...View4  
		private void rItC_PZ_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rItC_PZ_EditValueChanging - метод используется для блокировки изменений допустимых
			//                             значений поля PZ в ...View4(для таблицы R_RoadResp) 
			//                             и сохранения выбранного 
			// Возможны значения PZ: =0-ответственный, =1- исполнитель, =2- кому предзначен документ
			//-------------------------------------------------------------------------------------
			try   // Попытаемся конвертировать 1-ый символ поля в число
			{
				int i = Convert.ToInt16(e.NewValue.ToString().Substring(0, 1));
				if (i >= 0 && i <= 2)	 // Если 1-ый символ - число от 0 до 2 
					e.NewValue = i;  //  то сохраняем это число
				else
					e.Cancel = true;   // иначе отменяем действие ввода
			}
			catch // ИНАЧЕ отменяем действие ввода
			{
				e.Cancel = true;
			}

		}

		// rItC_PD_EditValueChanging - ввод и контроль допустимых значений поля PD в ...View1 
		private void rItC_PD_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rItC_PD_EditValueChanging - метод используется для блокировки изменений допустимых
			//                             значений поля PD( 0 - 99) в ...View1(для таблицы NsiRoad)
			//                             и сохранения выбранного
			//-------------------------------------------------------------------------------------
			try   // Попытаемся конвертировать 1-ый символ поля в число
			{

				int i = Convert.ToInt16(e.NewValue.ToString().Substring(0, 2).Trim());

				if (i >= 0 && i <= 99 && rItC_PD.Items[i].ToString() != null)
					e.NewValue = i;   // Если 2 символа - число от 0 до 99, то сохраняем это число
				else
					e.Cancel = true;  // иначе отменяем действие ввода
			}
			catch // ИНАЧЕ отменяем действие ввода
			{
				e.Cancel = true;
			}
		}

		//gridView_CustomDrawCell - выделениe цветом и фонтом строк в grid*
		private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//gridView_CustomDrawCell-метод используется для выделения цветом и фонтом строк в  grid*
			//-------------------------------------------------------------------------------------
			//e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;//Слева-вверх
			//e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal; //Слева-вниз
			//e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;      //Слева-направо
			//-------------------------------------------------------------------------------------
			string tFlag = myView.GetRowCellValue(e.RowHandle, "RMODI").ToString().Trim().ToUpper();
			switch (tFlag)
			{
				case "I":    // Добавление строки
					e.Appearance.BackColor = Color.Khaki;        // 1-ый цвет строки  
					e.Appearance.BackColor2 = Color.WhiteSmoke;   // 2-ой цвет строки cерый
					e.Appearance.ForeColor = Color.Blue;         // Цвет текста в строке cиний
					e.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
					break;
				case "U":    // Изменение в строке
					e.Appearance.BackColor = Color.Honeydew;           // Цвет строки  
					e.Appearance.BackColor2 = Color.WhiteSmoke;
					e.Appearance.ForeColor = Color.Black;
					break;
			}

		}



		#endregion		
	
		#region Методы, связанные с обработкой событий при редактировании полей грида

		//Перевод и отображение курсора в нужной ячейке 
		protected override void OnShown(EventArgs e)
		{
			//-------------------------------------------------------------------------------------		
			// OnShown - переопределенный(override) метод срабатывающий при отображении формы на 
			//		     экране  обеспечивает перевод и отображение курсора в нужной ячейке 
			//-------------------------------------------------------------------------------------

			base.OnShown(e); // Вызов метода из  базового класса 
			//myView.FocusedRowHandle = 1;// перевод фокуса в нужную ячейку 
			//myView.ShowEditor();        // вызов редактора ячейки 
		}

		// Закрытие редактора ячейки 
		private void p_CloseAllEditors()
		{
			//-------------------------------------------------------------------------------------
			// p_CloseAllEditors - метод используется для закрытия редактора ячейки для сохранения 
			//                     значения последнего изменения.
			// Вызывается при сохранении, удалении, изменении фильтра, закрытие формы, печати
			//-------------------------------------------------------------------------------------
			myView.CloseEditor();
		}



		#endregion

		#region Методы обработки полей, связанных с выводом документа на печать

		private void bB_Print_ItemClick(object sender, ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			//B_Print_Click- метод используется при нажатии на кнопку ПРИНТЕРА(B_Print) для печати 
			//		         документа, сохраненного в папке Excel_Output(см. p_XlsOut).
			//		         При этом :Excel_документ НЕ отображается(bVis=false), но автоматически
			//		                   открывается(bPrt=true) окно диалога для выбора принтера. 
			//-------------------------------------------------------------------------------------
			p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
			printableComponentLink1.CreateDocument();    // уничтожаем старый документ
			printableComponentLink1.CreateReportHeaderArea += new CreateAreaEventHandler(printableComponentLink1_CreateReportHeaderArea);//подключяем обработчик события для прорисовки рабочей области
			printableComponentLink1.ShowPreviewDialog();//показываем диалог печати
			

		}
	
		private void printableComponentLink1_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		//--------------------------------------------------------------------------------------------------------------
		// Метод CreateReportHeaderArea вызывается во время прорисовки основной части документа.
		//
		// Используется для вывода заголовка документа
		//---------------------------------------------------------------------------------------------------------------
			string txt="";//переменная для хранения выводимой надписи
			
			txt=myView.GroupPanelText;  //Определяем что писать
			e.Graph.Font=new Font("Times New Roman",14,FontStyle.Bold | FontStyle.Italic);//Устанавливаем шрифт
			e.Graph.DrawString(txt, Color.BlueViolet, new Rectangle(0,0,(int)e.Graph.ClientPageSize.Width,30), BorderSide.None);//выводи текст на екран	
		}

		#endregion			
		
		#region Создание tooltip для отображения списка АС, групп, признаков на элементах грида grid1(...View1,...View4) 

		private void p_ListAC()
		{
			//-------------------------------------------------------------------------------------		
			// p_ListAC - метод обеспечивает загрузку списка АС из LIST_AC в rICBE_AC_репозиторий
			//            поля LIST_AC. Используется при обработке NsiRoad для отображения ToolTip
			//            при наведении мышки на поле LIST_AC
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
								ToolAC += txt;    // ToolTip для списка АС и его названия при движении мышки в колонке LIST_IDAC 
								txt = "\n";
							}
						}
					}
				}
			}
			catch { }

		}

		// p_ListGr - загрузка списка групп для отображения ToolTip при наведении мышки на поле PD-...View1
		private void p_ListGr()
		{
			//-------------------------------------------------------------------------------------		
			// p_ListGr - метод обеспечивает загрузку списка групп из GrUser в rItC_PD_репозиторий
			//            поля PD. Используется при обработке NsiRoad для отображения ToolTip
			//            при наведении мышки на поле PD
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
								ToolGr += txt;    // ToolTip для группы и ее названия при движении мышки в колонке PD ...View1
								txt = "\n";
							}
						}
					}
				}
			}
			catch { }

		}

		// HideHintTool - закрытиe ToolTip соответствующей колонки
		private void HideHintTool()
		{
			//-------------------------------------------------------------------------------------
			// HideHintTool - метод используется для закрытия ToolTip соответствующей колонки
			//                                 
			// Вызывается в блоках: ...View*_MouseMove
			//-------------------------------------------------------------------------------------
			toolTipController1.AutoPopDelay=0;    // Устанавливаем все задержки в 0 
			toolTipController1.ReshowDelay =0;    // для немедленного скрытия tooltip
			toolTipController1.InitialDelay=0;    
			toolTipController1.HideHint();        // и закрываем tooltip 
			// Возвращяем стандартные значения задержек :
			toolTipController1.AutoPopDelay=5000; // - время задержки показа tooltip
			toolTipController1.ReshowDelay =500;  // - время, которое должно пройти прежде чем будет показан новый tooltip
			toolTipController1.InitialDelay=100;  // - время задержки перед инициализацией(показом) нового объекта 
		}

		// View_MouseMove - обеспечивает отображение подсказки к полю при движении курсора в области прямоугольника  
		private bool View_MouseMove(int lX, int lY, int wX, int hY, string tool, string titl, System.Windows.Forms.MouseEventArgs e)
		{
		//---------------------------------------------------------------------------------------
		// ...View_MouseMove - метод вызывается при движении курсора в области прямоугольника
		//			           для отображения подсказки к полю  
		//	Параметры : lX - X(горизонтальная)_координата левого верхнего угла прямоугольника 
		//		        lY - Y(вертикальная)_координата   левого верхнего угла прямоугольника
		//		        wX - ширина(Width)  прямоугольника 
		//		        hY - высота(Height) прямоугольной области: 
		//                   > 0- заданная;  = 0-расчитываемая  
		//---------------------------------------------------------------------------------------
			Point pT= new Point(e.X, e.Y);   // Создаем точку в месте расположения курсора 
			if (hY == 0)                     // Определяем высоту прямоугольной области  по :
				hY = (myView.RowCount                         // -количеству строк в myView
					*(myView.Appearance.Row.FontHeight        // -высоте  шрифта в строке
					+(myView.Appearance.Row.FontHeight        
					-(int)myView.Appearance.Row.Font.Size))); // -размеру шрифта в строке
						
			Rectangle r1= new Rectangle(lX,lY,wX,hY); // Определяем допустимую прямоугольную область
			bool isIn=r1.Contains(pT);
			if(isIn)             // При попадании курсора в прямоугольную область устанавливаем:
			{
				toolTipController1.AutoPopDelay=5000; // - время задержки показа tooltip
				toolTipController1.ReshowDelay =5000; // - время, которое должно пройти прежде чем будет показан новый tooltip
				pT.X =Cursor.Position.X;
				pT.Y =Cursor.Position.Y;
				toolTipController1.ShowHint(tool, titl, pT);
			}
			return isIn;
		}

		private void advBandedGridView1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		//---------------------------------------------------------------------------------------
		// ...View1_MouseMove-метод вызывается при движении курсора в области advBandedGridView1
		//			          НДІ организаций для отображения подсказки к колонке "PD"-код группы 
		//---------------------------------------------------------------------------------------
			//this.Text = "По горизонтали: X= " + e.X.ToString() + "  По вертикали: Y=" + e.Y.ToString(); // Координаты курсора

			bool isoneIn = false;
			if (myView == advBandedGridView4)
			{
				isoneIn = View_MouseMove(916, 106, v4C4.Width,0,
				 "0 -ответственный за документ\n1 -исполнитель\n2 -кому предзначен документ", "ознака ", e) ? true : isoneIn;
			}
			if (myView == advBandedGridView1)
			{
				isoneIn = View_MouseMove(503, 126, v1C5.Width, 0, ToolGr,"Група", e)    ? true : isoneIn;
				isoneIn = View_MouseMove(503, 48, v1C5.Width, 75, v1C5.ToolTip,"",e)    ? true : isoneIn;
				isoneIn = View_MouseMove(540, 126, v1AC.Width, 0, ToolAC,"Cписок АС",e) ? true : isoneIn;
				isoneIn = View_MouseMove(540, 48, v1AC.Width, 75, v1AC.ToolTip,"", e)   ? true : isoneIn;

				isoneIn = View_MouseMove(540 + v1AC.Width, 48, v1C6.Width, 75, v1C6.ToolTip, "", e) ? true : isoneIn;
			
			}
			if (!isoneIn)
				HideHintTool();
		}


		#endregion

		#region Функции, информационные сообщения и сообщения требующие принятия решения


		/// <summary> WndProc - перехват сообщений
		/// Метод перехвата всех сообщений, которые адресованы приложению из вне.
		/// Используется для обработки данных сообщений в частности для изменения языка подсказок
		/// </summary>
		/// <param name="m">Структура сообщения
		/// m.Msg - Код сообщения
		/// m.HWnd - адрес(код, дескриптор) получателя
		/// m.LParam - пользовательская информация
		/// m.WParam - системная информация
		/// </param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == RDA.RDF.GM_LANGCHANGE && m.HWnd == this.Handle)
			{
				p_LanghInfo(); // Формируем всплывающие подсказки для элементов формы
			}
			base.WndProc(ref m);
		}

		// p_LanghInfo - подсказки к кнопкам и полям на форме 
		private void p_LanghInfo()
		{
			//-------------------------------------------------------------------------------------
			// p_LanghInfo - метод используется для выдачи подсказок и сообщений  в зависимости 
			//                 от выбранного языка : RDA.RDF.RDA.RDF.sLang = U-украинский, =R-русский
			//-------------------------------------------------------------------------------------
			try
			{
				#region Подсказки(ToolTip) на украинском языке

				if (RDA.RDF.sLang == "U")   // Выбор украинского языка
				{
					bB_Save.Hint    = "    CTRL+S :  зберегти всі доданi, видалені  та  змінені записи в НДІ";
					bB_Print.Hint    = "     Перегляд та  друк  даних";
					bB_AddRow.Hint    = "    Створити новий запис";
					bB_CancelEditRow.Hint    = "    скасовати додавання нової або зміни обраного запису";
					bB_DelRow.Hint    = "    CTRL+DELETE :  видалити запис з НДІ";
					bB_Exit.Hint     = "    Alt+F4 :  закриття форми";
					bE_NDI.Hint      = "    Виберіть НДІ  який потрібно переглянути  або налаштувати";
					bE_Road.Hint     = "    Виберіть залізницю до якої відносяться відділ";
					bE_Otdel.Hint    = "    Виберіть відділ в якому потрібно переглянути або налаштувати відповідальних осіб";

					v1C5.ToolTip = "Група до якої відноситься залiзниця";
					v1AC.ToolTip = "Список(з таблиці LIST_AC) ID_кодів у вигляді: RD,NF ! RD ! NF..., що вказують які АС доступні залiзниці";
					v1C6.ToolTip = "Унікальний  код запису формується автоматично : 005RRRR00000000 (т.е (005*10000+Road)*100000000)";
					l_rKey1.Text = "Неможливо замінити ключ, що використовується в інших таблицях.";  //для rKey =1
					l_rKey2.Text = "див. поле ";
					
				}
				#endregion

				#region Подсказки(ToolTip) на русском языке

				else
				{
					bB_Save.Hint      = "    CTRL+S :  сохранить все добавленные, удаленные и измененные записи в НСИ";
					bB_Print.Hint     = "    Просмотр и печать данных";
					bB_AddRow.Hint    = "    Создать новую запись";
					bB_CancelEditRow.Hint    = "    отменить добавление новой или изменение выбранной записи";
					bB_DelRow.Hint    = "    CTRL+DELETE :  удалить запись из НСИ";
					bB_Exit.Hint     = "    Alt+F4 :  закрыть форму";
					bE_NDI.Hint      = "   выберите НСИ, которую требуется просмотреть или изменить";
					bE_Road.Hint     = "    виберите дорогу, к которой относятся отделы";
					bE_Otdel.Hint    = "    виберите отдел, в котором требуется просмотреть или изменить ответственных лиц";
					
					v1C5.ToolTip = "Группа, к которой относится дорога";
					v1AC.ToolTip = "Список(из таблицы LIST_AC) ID_кодов в виде: RD,NF ! RD ! NF..., указывающий какие АС доступны дороге";
					v1C6.ToolTip = "Уникальный код записи формируется автоматически : 005RRRR00000000 (т.е (005*10000+Road)*100000000)";
					l_rKey1.Text = "Нельзя заменить ключ используемый в других таблицах. "; //для rKey =1
					l_rKey2.Text = "cм. поле ";

				}
				#endregion
				DataRow dr1 = Ds.Tables["Ds_Buffer"].Rows[tRow]; // определим запись 

				p_FooterBarInfo(dr1); // информация об режиме работы с документом 

			}
			catch { }

		}

		// p_FooterBarInfo - информация о допустимости изменений и статусе записи 
		private void p_FooterBarInfo(DataRow drN)
		{
			//-------------------------------------------------------------------------------------		
			// p_FooterBarInfo - метод обеспечивает отображение информации о допуступе к изменению
			//                   записи, статусу записи и данных о последнем изменении записи
			//
			// Вызывается в блоках: p_LanghInfo, 
			//-------------------------------------------------------------------------------------	
			string tCapt = "Режим ";
			string tHint = "";
			//rIt_LeftFont.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;      //Слева-направо
			//rIt_LeftFont.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;      //Слева-направо

			rIt_LeftFont.Appearance.BackColor = Color.Transparent;   // 1-ый цвет строки  
			rIt_LeftFont.Appearance.BackColor2 = Color.Transparent;  // 2-ой цвет строки cерый
			rIt_LeftFont.Appearance.ForeColor = Color.Black;         // Цвет текста в строке 
			this.rIt_LeftFont.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			rIt_LeftFont.NullText = "";

			try   // Если поле RMODI присутствует в записи, то покажем  
			{
				tHint = (RDA.RDF.sLang == "R") ? " После сохранения данных запись будет   " : " Після збереження даних запис буде   ";

				#region bEditTabl = true - допустимы изменения

				if (bEditTabl)
				{

					tCapt += RDA.RDF.sLang == "U" ? "редагування: " : "редактирования: ";
					tHint = RDA.RDF.sLang == "R" ? " После сохранения данных запись будет " : " Після збереження даних запис буде ";

					switch (drN["RMODI"].ToString())     //  статус записи
					{
						case "S":                                //  запись без изменения
							tHint = "";
							rIt_LeftFont.NullText = RDA.RDF.sLang == "R" ? " запись без изменений" : " запис без зміни ";
							break;
						case "U":                                //  запись     изменена
							tHint += RDA.RDF.sLang == "R" ? "ИЗМЕНЕНА   в таблице." : "ЗМIНЕНА   в таблиці.";

							rIt_LeftFont.Appearance.BackColor = Color.Honeydew;           // Цвет строки  
							rIt_LeftFont.Appearance.BackColor2 = Color.WhiteSmoke;
							rIt_LeftFont.Appearance.ForeColor = Color.Black;
							rIt_LeftFont.NullText = RDA.RDF.sLang == "R" ? " в запись внесены изменения" : " у запис внесені зміни";

							break;
						case "I":                                //  запись      новая
							tHint += RDA.RDF.sLang == "R" ? "ДОБАВЛЕНА в таблицу." : "ДОДАНА в таблицю.";
							rIt_LeftFont.Appearance.BackColor = Color.Khaki;        // 1-ый цвет строки  
							rIt_LeftFont.Appearance.BackColor2 = Color.WhiteSmoke;   // 2-ой цвет строки cерый
							rIt_LeftFont.Appearance.ForeColor = Color.Blue;         // Цвет текста в строке cиний
							rIt_LeftFont.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
							rIt_LeftFont.NullText = RDA.RDF.sLang == "R" ? " новая запись " : " новий запис ";

							break;
						case "D":                                //  запись      удалена
							tCapt += RDA.RDF.sLang == "R" ? " запись отмечена на удаление " : " запис відзначений на видалення";
							tHint += RDA.RDF.sLang == "R" ? "УДАЛЕНА   из таблицы." : "ВИЛУЧЕНА   з таблиці.";
							break;
						default:
							break;
					}

				}
				#endregion

				#region bEditTabl = false - недопустимы изменения

				else
				{
					tCapt += (RDA.RDF.sLang == "U")
						? "перегляду: заборонено ДОСТУПУ для змін."
						: "просмотра: закрыт ДОСТУП для изменений.";

					tHint = (RDA.RDF.sLang == "U")
						? "У ВАС немає ДОСТУПУ для змін документу . Зверніться до адміністратору БД."
						: "У ВАС нет ДОСТУПА для изменения документа . Обратитесь к администратору БД.";
				}
				#endregion

				bS_InfoLeft.Caption = tCapt;
				bS_InfoLeft.Hint = tHint;

				bS_Info.Caption = RdaFunc.f_RowChangeInfo(drN); //.f_DocChangeInfo((drN).f_DocChangeInfo(drN);        // информация об изменении записи
				bS_Info.Hint = (RDA.RDF.sLang == "U")
								? "  Дата останньої зміни запису, ПІБ користувача(що вніс зміни) та IP_ПК"
								: "  Дата последнего изменения записи, ФИО пользователя(внесшего изменение) и IP_ПК";

			}
			catch { } // ИНАЧЕ отменяем показ статуса записи 

		}

		// f_IdRoad - функция возвращает Id_записи дороги 
		private string f_IdRoad(int tRoad, int tIdTabl)
		{
			//-------------------------------------------------------------------------------------
			// f_IdRoad - функция возвращает Id_записи дороги(NSIROAD) для поля ROADF  в виде 
			//             005RRRR00000000=(((005*10000+tRoad)*100000000 
			//
			// ххх=3-знака 005            - код таблицы NsiRoad из List_Table_AC
			//     4-знака    RRRR        - tRoad  - код дороги(поле Road)      
			//     8-знаков       00000000- зарезервировано  
			//  Значение Id_кода таблицы из RDA: ххх=Helpers.CommonFunctions.GetIDTable(myTabl)
			//-------------------------------------------------------------------------------------
			decimal i = -1;
			if (tRoad > -1)
			{
				i = Convert.ToInt64(00500 + tRoad);
				i = Convert.ToInt64(i * 10000000000);
			}
			return (i.ToString());
		}

		// f_ErrN - принятиe решения по СОХРАНЕНИЮ ИЗМЕНЕННЫХ данных, если не выполнено "Зберегти" 
		private bool f_ErrN(string par1, string par2, string par3, string par4)
		{
			//-------------------------------------------------------------------------------------
			// f_ErrN -функция вызывается для принятия решения по СОХРАНЕНИЮ ИЗМЕНЕННЫХ данных,если
			//         не выполнено "Зберегти" до выбора группы, предприятия, пользователя,... 
			//         или при закрытии формы.
			//         Предлагается выполнить sB_Save_Click-"Зберегти зміни".
			//         Функция возвращает TRUE для Cancel, если указанное действие ОТМЕНЯЕТСЯ. 
			// При ответе: Так(Yes)-   СОХРАНЯЮТСЯ изменения и ВЫПОЛНЯЕТСЯ указанное действие
			//             Ні(No)  - НЕСОХРАНЯЮТСЯ изменения, но ВЫПОЛНЯЕТСЯ указанное действие
			//             Відміна(Cancel) – ОТМЕНЯЕТСЯ указанное действие  
			// Параметры : par1-текст действия,например: "изменение предприятия"
			//             par2-текст предупреждения, например,"по предприятию... "...
			//             par3-дополнительные сообщения в блоке Подробиці
			//             par4= ="BUFF" для bUpdBuff
			//
			// Если RDA.RDF.sLang="R" - выдача сообщений на русском("R") иначе -на украинском("U") языке
			//-------------------------------------------------------------------------------------

			bool bUpd = false;
			string sTxt1 = "";
			string sTxt2 = "";
			string sTxt3 = "\r\n\r\n   " + par2;
			string sTxt4 = "\r\n\r\n" + RdaFunc.f_Repl(" ", 60);
			string sTxt5 = "\r\nТак        - ";
			string sTxt6 = "\r\nНі           - ";
			string sTxt7 = "\r\nВідміна  - ";
			string sTxt8 = par3 + "\r\n" + RdaFunc.f_Repl("-", 90);

			#region Формирование текста сообщения на русском(RDA.RDF.sLang="R") языке
			if (RDA.RDF.sLang == "R")
			{
				sTxt1 = "Предупреждение";
				sTxt2 = "   Действие: " + par1;
				sTxt3 += " были изменения .";
				sTxt4 += "Сохранить   изменения ?";
				sTxt5 += "сохранить изменения и выполнить указанное действие";
				sTxt6 += "не сохранять изменения,но выполнить указанное действие";
				sTxt7 += "отменить указанное действие";
			}
			#endregion

			#region Формирование текста сообщения на украинском(RDA.RDF.sLang="U") языке
			else
			{
				sTxt1 = "Попередження";
				sTxt2 = "   Дія : " + par1;
				sTxt3 += " були змiни.";
				sTxt4 += "Зберегти    змiни ?";
				sTxt5 += "зберігти зміни та виконати вказану дію";
				sTxt6 += "не зберігати зміни, але виконати вказану дію";
				sTxt7 += "відмінити вказану дію";
			}
			#endregion

			#region Сообщение об изменениях для и принятия решения о сохранении данных в Oracle_таблице

			ERR.Error err4 = new ERR.Error(sTxt1, ERR.ErrorImages.Question, sTxt2 + sTxt3 + sTxt4, sTxt8 + sTxt5 + sTxt6 + sTxt7, 3);
			err4.ShowDialog();

			switch (err4.DialogResult)
			{
				case DialogResult.Yes:
					bUpd = false;
					bB_Save_ItemClick(null, null);// СОХРАНИМ изменения 
					break;
				case DialogResult.No:
					bUpd = false;
					break;
				default:
					bUpd = true;            // ОТМЕНИМ указанное действие
					break;
			}

			#endregion
			bUpdBuff = bUpd;
		
			l_rKey3.Focus();
			return (bUpd);
		}

		// p_ErrRead - сообщениe об отсутствии данных в исходной таблице
		private void p_ErrRead(string par1)
		{
			//-------------------------------------------------------------------------------------
			// p_ErrRead-метод используется для сообщения об отсутствии данных в исходной таблице
			//
			// Параметры : par1-текст сообщения об отсутствии данных в таблице:
			//   par1="ROAD" (для p_ReadRoad) -в NsiRoad  нет предприятия   
			//       ="OTDEL"(для p_ReadOtdel)-в NsiOtdel нет отдела        в заданном предприятии
			//       ="USER" (для p_ReadUser) -в AcUser   нет пользователей в отделе   предприятия
			//
			// Если RDA.RDF.sLang="R" - выдача сообщений на русском("R") иначе -на украинском("U") языке
			//-------------------------------------------------------------------------------------
			// Вызов : p_ErrRead("ROAD"); // Сообщение об отсутствии данных по дороге
			//-------------------------------------------------------------------------------------
			string sTxt1 = (RDA.RDF.sLang == "R") ? "Ошибка" : "Помилка";
			string sTxt2 = (RDA.RDF.sLang == "R") ? "Отсутствуют " : "Відсутні ";
			string sTxt3 = (RDA.RDF.sLang == "R") ? "Для ввода " : "Для уведення ";
			string sTxt4 = "\r\n\r\n" + (RDA.RDF.sLang == "R" ? "Введите " : "Уведіть ");
			string sTxt5 = (RDA.RDF.sLang == "R")
				? "перейдите в ` НДІ` \r\n " + RdaFunc.f_Repl(" ", 20) + " и выберете пункт - "
				: "перейдіть у ` НДІ` \r\n " + RdaFunc.f_Repl(" ", 20) + "і вибереть пункт - ";

			switch (par1)
			{
				case "OTDEL":
					#region Текст ошибки для НДІ відділів по підприємству
					sTxt2 += (RDA.RDF.sLang == "R") ? " отделы!" : " відділи!";
					sTxt2 += sTxt4 + (RDA.RDF.sLang == "R" ? " отдел по дороге  " : " відділ по залiзниці ");
					sTxt3 += (RDA.RDF.sLang == "R") ? "отдела " : "відділу ";
					sTxt3 += sTxt5 + "НДІ відділів по залiзниці ";
					break;
					#endregion
				case "RESP":
					#region Текст ошибки для НДІ  відповідальних осіб
					sTxt2 += (RDA.RDF.sLang == "R") ? "ответственные лица  " : "відповідальні особи ";
					sTxt2 += sTxt4 + (RDA.RDF.sLang == "R" ? " ответственное лицо " : " відповідальну особу ");
					sTxt3 += (RDA.RDF.sLang == "R") ? "ответственного лица " : "відповідальної особи ";
					sTxt3 += sTxt5 + "НДІ користувачів";
					break;
					#endregion
				default:
					sTxt1 = "";
					break;
			}
			#region  Формирование сообщения об ошибке
			if (sTxt1 != "")
			{
				sTxt2 += (RDA.RDF.sLang == "R") ? "и продолжите работу." : " і продовжите роботу.";
				ERR.Error err8 = new ERR.Error(sTxt1, ERR.ErrorImages.Examplemation, sTxt2, sTxt3, 1);
				err8.ShowDialog();
			}
			#endregion
		}
	

		#endregion

		#region Процедура реалізайії "гарячих" клавіш

		private void FRMNSI_KeyDown(object sender, KeyEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// FRMBOD2_KeyDown - метод реализации "гарячих" клавиш
			//	 Для  использования горячих клавиш на форме следует :
			//		                 - указать для свойства KeyPreview = true
			//                       - установить обработчик события KeyDown и прописать для 
			//	                       соответствующих кнопок комбинацию "гарячих" клавіш
			//-------------------------------------------------------------------------------------

			if (e.Control && e.KeyCode == Keys.S) this.bB_Save_ItemClick(null, null);     //CTRL+S     : Сохранить
			if (e.Control && e.KeyCode == Keys.Delete) this.bB_DelRow_ItemClick(null, null); //CTRL+DELETE: Удалить
			if (e.Alt && e.KeyCode == Keys.F4) this.Close();                              //Alt+F4: закриття форми 
		}

		#endregion

			
	}
}

