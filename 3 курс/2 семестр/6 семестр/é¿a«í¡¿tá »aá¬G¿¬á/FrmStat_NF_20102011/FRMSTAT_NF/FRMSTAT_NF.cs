//----------------  AC  RODUZ NF - "...розподілу доходів по залізницях... "  -------------
//
// FRMSTAT_NF - Довідник змістів
//            
//ГІОЦ УЗ-відділ ФС         пр-т: Бейлинсон Л.М. т-н: 5-09-86                  май 2011 р
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
using DevExpress.XtraEditors.Controls;

namespace FRMSTAT_NF
{
	public partial class FRMSTAT_NF : DevExpress.XtraEditors.XtraForm
   {
        #region Declare SYSTEM     - системные объекты

        // Создаем DataSet oбъект, в ОП ПК-клиета, для набора таблиц и отношений между ними
        private System.Data.DataSet Ds;
        // Создаем OracleConnection - соединение клиента с базой данных Oracle
		//private OracleConnection OraCon;
        // Создаем объекты OracleDataAdapter для : 
        //     1. заполнения  Ds_таблиц данными  из Oracle_таблиц 
        //     2. изменения данных Oracle_таблиц из Ds_таблиц
        //  и объекты(OraDA..) OracleDataAdapter для записи изменений в базу данных
		private OracleDataAdapter OraDABuffer;  // "Ds_Buffer"-временная область для выборки данных из Oracle_таблиц
		private OracleDataAdapter OraDANsi;     // для выборки в "Ds_Nsi"    из Oracle_таблицы НСИ 
		private OracleDataAdapter OraDATmp;   // "Ds_Tmp" -временная область для построения иерархического меню системы
  
        // Создаем объекты OracleCommandBuilder для каждой таблицы
		private OracleCommandBuilder OraCBNsi;    // для обновления Oracle_таблицы  НСИ   БД РОДУЗ НФ  из "Ds_Nsi"     

        #endregion

        #region Declare Variables  - дополнительные переменные
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


        private char   cAcKey;   // Переданное значение кода доступа : cAcc=R-чтение\W-изменение\F-полный
		//private string sLang   = "U"; // Язык для выдачи сообщений: U-украинский, R-русский
		private long   uId     = 1; // Переданное значение UserID-ID_пользователя системы
		private string uName   = " "; // Переданное значение UserName-ФИО_пользователя системы

		private int    myRoad  = -2;     // Начальное значение кода выбираемой дороги 
		private bool   bRoad   = false;  // Признак отсутствия данных в NsiRoad
		private int   idxRoad  = 0;      // Индекс выбранной строки в НСИ дорог 

		private int    myOtdel = -1;     // Начальное значение кода выбираемого отдела по дороге 
		private bool   bOtdel  = false;  // Признак отсутствия данных в NsiOtdel
		private int    idxOtdel= 0;      // Индекс выбранной строки в НСИ отделов 

		private string myKey   = "";     // Переменная для передачи параметром названия ключа записи
		private bool   bNsiErr = false;  // Признак отсутствия ошибки в записи Ds_Buffer 
		private bool   bUpdBuff= false; // Признак отсутствия обновлений в Ds_Buffer 

		private int    rKey    = 0;      // Признак:=0-разрешено(=1-запрещено) изменения ключа или удаления записи, =-1-новая(=-2-удаленная) запись


		private string myTabl    = "NF_NSISTAT"; // Текущее имя обрабатываемой таблицы 
		private bool   bEditTabl = false;        // Признак запрета на редактирование таблицы
		private int    MaxNum    = 0;            // Наибольший номер для поля IDSTAT записей в Ds_Buffer
		private string myDoc     = "NSISTAT"; // Текущее имя обрабатываемой таблицы 
	
		// Создаем экземпляр(myView) класса ... AdvBandedGridView для получения доступа к свойствам вида
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView myView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
		private Office.Excel xls = null; // Рабочий экземпляр Excel приложения

		DataView dtV = new DataView();
		private RDA.RDF RdaFunc = new RDA.RDF();// Объявление личной библиотеки  RDA, содержащей общие :

		#endregion
    
        /// <summary>
        /// Конструктор класа
        /// </summary>
        /// <param name="OC">Экземпляр соединения с БД</param>
        /// <param name="UserID">Идентификатор пользователя</param>
        /// <param name="UserName">Имя пользователя</param>
        /// <param name="cAcc">Доступ пользователя</param>
        /// <param name="iMes">Выбраный месяц месяц</param>
        /// <param name="iGod">Выбраный год</param>
		public FRMSTAT_NF(OracleConnection OC, int UserID, string UserName, char cAcc, int iMes, int iGod)
		{
			InitializeComponent();
			//OraCon = OC;      // коннект к базе ORACLE-открыть при формировании DLL_кода
			cAcKey = cAcc;    // код доступа к объекту (cAccess из таблицы R_UserObj)
			uId = UserID;  // ID  пользователя системы(IdUser из таблицы AcUser)			
			uName = UserName;// имя пользователя системы(Naim   из таблицы AcUser)
		}

		private void FRMSTAT_NF_Load(object sender, EventArgs e)
		{

		#region myAccess- определение доступа("ALL","ADMIN","USER", "READ" ) к объектам
		//-------------------------------------------------------------------------------------		
		//  myAccess = RdaFunc.p_Access(uId.ToString()) : 
		//           = "ALL"   - администратор АС         (режим доступа cAcKey='F')
		//           = "ADMIN" - администратор БД дороги  (режим доступа cAcKey='F')
		//           = "USER"  - пользователь   БД дороги (режим доступа cAcKey='F')
		//           = "READ"  - только просмотр данных   (режим доступа cAcKey='R') 
		//           = "FALSE" - доступ ЗАКРЫТ
		//   Примечание :
		//      Обновление таблицы NF_ZMIST АС РОДУЗ НФ выполняется :
		//         - администратором БД АС (myAccess = "ALL" и RoadUser=-1) 
		//         - администратором(myAccess="ADMIN") или пользователем(myAccess="USER")
		//           имеющим полный доступ(F) и только на сервере своей дороги(RoadUser=RoadServ) 
		//-------------------------------------------------------------------------------------		

			myAccess = RDA.RDF.UserParam.myAccess;   // Получение из Rda значения myAccess 26/10/2010
			myRoad = RDA.RDF.UserParam.myRoad;       // Получение из Rda кода дороги пользователя

			if (myAccess == "ALL"      // администратор АС   
			|| ((myAccess == "ADMIN"   // администратор БД дороги
			|| (myAccess == "USER" && RDA.RDF.UserParam.Otdel == 102)) // пользователь дороги отдела 102 -НФДУ
			&& myRoad == RDA.RDF.UserParam.RoadServ))
			{
				bEditTabl = true; // Признак разрешения на редактирование таблицы
			}
			else
			{
				myAccess = "READ";
				bEditTabl = false; // Признак запрета на редактирование таблицы
			}

		#endregion


		#region Oпределение коллекции таблиц в DataSet(Ds)
			// Ds(DataSet)- RowNum_класс содержит коллекцию таблиц для хранения в ОП(кэше):             
			//              данных из БД, реляционных зависимостей и constraints
			Ds = new DataSet();
			Ds.Tables.Add("Ds_Buffer");    // OraDABuffer-таблица-шаблон для отображения данных в гриде
			Ds.Tables.Add("Ds_Nsi");       // OraDANsi    -> Временная область для выборки в "Ds_Nsi" из Oracle_таблицы НСИ 
			Ds.Tables.Add("Ds_Tmp");       // OraDATmp    -> Временная область для выборки данных из Oracle_таблиц
			Ds.Tables.Add("Ds_Fdu8");      // Данные из NF_NSIFDU8_STAT для формирования признака(ISFDU8) выбора сводов в ФДУ-8
		#endregion

		#region установка начальных значений

		//myTabl              = "";          // Текущее имя обрабатываемой таблицы 
		bUpdBuff            = false;       // Установим признак - нет изменений в Ds_Buffer
		bB_Save.Enabled     = false;       // Деактивизация кнопок : "Зберегти зміни" 
		bS_InfoLeft.Caption = "";          // Очистить информационное поле на форме

		#endregion	

		myView = advBandedGridView3;
		p_ReadFdu8_Stat();// загрузка данных из NF_NSIFDU8_STAT в "Ds_Fdu8" для использования признака ISFDU8

		p_NsiDoc();	// выбор списка входных(Tip=0) документов из NF_NSIDOC	
		p_LoadRoad(); //  загрузка списка предприятий в rI_Road_репозиторий поля bE_Road

		p_LanghInfo(); // Формируем всплывающие подсказки для элементов формы

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
			RdaFunc.RoadOut = (RDA.RDF.UserParam.RoadServ == 5) ? myRoad : 5;  // укажем направление репликации: на сервер дороги или УЗ 

			string vKey1  = dr["ROAD"].ToString();
			string vKey2  = dr["IDSTAT"].ToString();
			string Key1   = "ROAD="   + vKey1.ToString();
			string Key2   = "IDSTAT=" + vKey2.ToString();
			string Id_Key = vKey1 + vKey2 ; // + vKey3.ToString(); Key3,
			RdaFunc.p_SaveIntoInfo(new string[] { InTabl, OutTabl }, tModi, new object[] { Key1, Key2 },
			   Id_Key, "N", OpFlag);

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

		   bB_AddRow.Enabled = bEditTabl;
		   bB_CancelEditRow.Enabled = bEditTabl;
		   bB_DelRow.Enabled = bEditTabl;

		   myView.OptionsBehavior.Editable = bEditTabl; //Разрешить-запретить изменения в таблице

		}

		// p_ReadFdu8_Stat - загрузка данных из NF_NSIFDU8_STAT в "Ds_Fdu8" для использования признака ISFDU8
		private void p_ReadFdu8_Stat()
		{
			//-------------------------------------------------------------------------------------		
			// p_ReadFdu8_Stat - метод обеспечивает загрузку данных из архива NF_NSIFDU8_STAT  
			//                   в Ds_Fdu8 для формирования признака(ISFDU8) выбора сводов в ФДУ-8
			//-------------------------------------------------------------------------------------		
			try
			{
				using (OracleDataAdapter oraDA = new OracleDataAdapter(@" SELECT "
						+ @" t.* FROM NF_NSIFDU8_STAT t ", RDA.RDF.BDOraCon))
				{
					oraDA.Fill(Ds, "Ds_Fdu8");      // заполним  Ds_таблицу( Ds_Fdu8) данными 
				}
			}
			catch { }
		}
		// f_IsFdu8 - функция возвращает значение признака(ISFDU8)
		private int f_IsFdu8(string tDoc, int tOtdel)
		{
			//-------------------------------------------------------------------------------------
			// f_IsFdu8 - функция возвращает значение признака(ISFDU8)
			// Используется в rItLUE_NDOC_EditValueChanging ПЕРЕД выбором документа из NF_NSIDOC
			//-------------------------------------------------------------------------------------
			int Result    = 0 ;
			try
			{
				DataRow[] drN = Ds.Tables["Ds_Fdu8"].Select(string.Format("Doc='{0}' AND OTDEL={1}", tDoc, tOtdel));
				if (drN.Length > 0)                  // Если ЕСТЬ строка в Ds_Fdu8
					Result = Convert.ToInt32(drN[0]["ISFDU8"].ToString());

			}
			catch { }
			return (Result);
		}

		// p_ReadStat - выборка из NF_NSISTAT списка Бухгалтерські довідки(статьи)по відділам залізниць
		private void p_ReadStat()
		{
		   //-------------------------------------------------------------------------------------		
		   // p_ReadStat - метод используется для выборка из NF_NSISTAT списка Бухгалтерські 
		   //		       довідки (статьи)по відділам залізниць
		   //     Дополнительные поля для Ds_таблицы : 
		   //	-rModi - признак записи: ="I"-новая, ="U"-изменена, ="S"-без изменений
		   //  -OldKey- старый код Position для обновления в БД 	
		   //	-rKey  - признак:=0-разрешено(=1-запрещено) изменения ключа или удаления записи,
		   //           =-1-новая или =-2-удаленная запись	        
		   //   Для rKey  функция f_IsUsedKeyStat(a_Road, a_IdStat) возвращает 1, если значение  
		   //  КОДА: a_Road,a_IdStat родительской таблицы NF_NsiStat имеется в NF_Provodki**
		   // 
		   //-------------------------------------------------------------------------------------		
		   MaxNum = 0;     // Наибольший номер для поля IDSTAT записей в Ds_Buffer '' as NDOC,
				   //+ @"(SELECT Naim FROM AcUser WHERE IdUser=t.UserId) as nUser"
		   try
		   {
			   OraDABuffer = new OracleDataAdapter(@" SELECT  t.*, 'S' as rModi, "
				   + @" RD.f_IsUsedKeyStat(t.Road, t.IdStat) as rKey "
				   + @" FROM NF_NSISTAT t WHERE t.Road =" + myRoad.ToString()
				   + @" ORDER BY Road, Otdel, idstat", RDA.RDF.BDOraCon);

			   Ds.Tables["Ds_Buffer"].Dispose(); // освободим все ресурсы 
			   Ds.Tables["Ds_Buffer"].Clear();   // очистим   все данные в таблице  
			   if (!Ds.Tables["Ds_Buffer"].Columns.Contains("ISFDU8"))
				   Ds.Tables["Ds_Buffer"].Columns.Add("ISFDU8", typeof (bool));
			   OraDABuffer.Fill(Ds, "Ds_Buffer"); // заполним  Ds_таблицу (Ds_Buffer) данными НСИ

			   #region Определение MaxNum_ключа если есть данные и разрешены изменения

			   if (Ds.Tables["Ds_Buffer"].Rows.Count > 0 && bEditTabl)
			   {
				   foreach (DataRow dr in Ds.Tables["Ds_Buffer"].Rows)
				   {

					   if (MaxNum < Convert.ToInt32(dr["IDSTAT"].ToString()))
						   MaxNum = Convert.ToInt32(dr["IDSTAT"].ToString());
				   }
				   Ds.Tables["Ds_Buffer"].AcceptChanges(); // Установка статуса записей в Unchanged 
			   }
			   #endregion
		   }
		   catch { }
		}

		private void p_ViewPage1()
		{
		   //-------------------------------------------------------------------------------------
		   // p_ViewPage1 - метод используется для отображения на экране вида(advBandedGridView*) 
		   //		 	     соответствующего НСИ, загруженного в Ds_Buffer и  установки признака 
		   //			     bEditTabl=true - разрешения на редактирование таблицы в БД РОДУЗ НФ. 
		   //-------------------------------------------------------------------------------------
		   //  Примечания. 
		   //  2. Изменение Oracle_таблиц NF_NsiStat может выполнять как администратор
		   //     АС(myAccess=ALL) так администратор(myAccess= "ADMIN") ДОРОГИ(RdaFunc.RoadUser =
		   //     myRoad). Изменения разрешены в БД РОДУЗ НФ как на сервере УЗ так и серверах дорог 
		   //     Причем администратор ДОРОГИ может внести изменения только для своей дороги.
		   //
		   //	3. myView = advBandedGridView* - хранит экземпляр класса...AdvBandedGridView
		   //
		   //	4. В поле "RKEY" устанавливается признак : 
		   //	           = 0 -разрешено или = 1-запрещено изменения ключа или удаления записи,
		   //             =-1 -новая     или =-2-удаленная запись	        
		   //-------------------------------------------------------------------------------------
		   grid1.DataSource = null;            // очистим  старые отображения в гриде
		   myView = advBandedGridView3;
		   myView.GroupPanelText = "НДІ  Бухгалтерські довідки по відділу ";
		   myView.GroupPanelText += (myOtdel > -1) ? rI_Otdel.GetDataSourceValue("KOD", idxOtdel).ToString() : "***";
		   myView.GroupPanelText += " по " + rI_Road.GetDataSourceValue("NAIMD", idxRoad).ToString()+ " залізниці";

		   dtV.Table = Ds.Tables["Ds_Buffer"];
		   //dtV.RowFilter = "OTDEL= " + myOtdel ; // не удаленная строка 
			   dtV.RowFilter = "OTDEL= " + myOtdel + " AND RMODI <> 'D'"; // не удаленная строка 
		   grid1.DataSource = dtV;
		   p_NSDClick();                     // Де\Активируем кнопки: sB_Save и т.п.

		}

		// p_RowFocus - метод используется ПОСЛЕ установки фокуса на строку в grid1
		private void p_RowFocus()
		{
		   //-------------------------------------------------------------------------------------
		   // p_RowFocus - метод используется ПОСЛЕ установки фокуса на строку в grid1(...View1-4)
		   //              для запрета\разрешения установки фокуса на ключ записи  
		   //
		   // Обеспечивает cохранение для дальнейшего использования :
		   //	-rKey  - признак:=0-разрешено(=1-запрещено) изменения ключа или удаления записи,
		   //           =-1-новая или =-2-удаленная запись	        
		   //
		   // Вызывается в блоках: gridView_FocusedRowChanged, sB_AddRow_Click и sB_DelRow_Click 
		   //                      где предварительно определёно значение tRow-номера записи
		   //-------------------------------------------------------------------------------------
		   if (myView.FocusedRowHandle > -1)
		   {
			   myKey = "";
			   DataRowView dr1 = dtV[myView.FocusedRowHandle];
			   rKey = dr1["RKEY"] != DBNull.Value ? Convert.ToInt32(dr1["RKEY"]) : 0;
			   p_FooterBarInfo(dr1);              // Информация о статусе текущей записи 

			   #region Блок разрешения- запрета на изменение ключа или удаления записи в НСИ

			   myKey = "IDSTAT = " + dr1["IDSTAT"].ToString();
			   if (rKey == 1)                                   // Т.к. ключ статьи используется,то
			   {
				   this.v_IDSTAT.OptionsColumn.AllowFocus = false; //  запретить удаление или изменение кода статьи
				   this.v_SHIFR.OptionsColumn.AllowFocus = false;  // шифр 
			   }
			   else
			   {
				   this.v_IDSTAT.OptionsColumn.AllowFocus = true;   //  иначе разрешить
				   this.v_SHIFR.OptionsColumn.AllowFocus = true;
			   }

			   #endregion

		   }
		}

		// p_DsNsi - метод обеспечивает выбор данных из Oracle_таблиц NF_NSISTAT 
		private void p_DsNsi()
		{
			//-------------------------------------------------------------------------------------		
			// p_DsNsi - метод обеспечивает выбор данных из Oracle_таблиц NF_NSISTAT 
			//           в   Ds_Nsi(БД РОДУЗ НФ).
			//
			//	Вызывается в блоке sB_Save_Click перед обновление данных в Oracle_таблицах
			//-------------------------------------------------------------------------------------			
			if (bEditTabl) // Если разрешено изменение в таблицах
			{
				try
				{
					OraDANsi = new OracleDataAdapter(@" SELECT  t.* FROM NF_NSISTAT t "
					+ @" WHERE t.Road =" + myRoad.ToString() + " ORDER BY Road, Otdel", RDA.RDF.BDOraCon);
					Ds.Tables["Ds_Nsi"].Dispose();       // освободим все ресурсы 
					Ds.Tables["Ds_Nsi"].Clear();         // очистим   все данные в таблице    
					OraDANsi.Fill(Ds, "Ds_Nsi");         // заполним  Ds_таблицу 
					//OraCBNsi = new OracleCommandBuilder(OraDANsi);
					//// при паралельной работе записать в БД данные того, кто последний выполнил сохранение  
					//OraCBNsi.ConflictOption = System.Data.ConflictOption.OverwriteChanges;
					////OraCBNsi.SetAllValues = true;
					//OraDANsi.MissingSchemaAction = MissingSchemaAction.AddWithKey;
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
			drN["ROAD"]     = drB["ROAD"];      // код дороги
			drN["OTDEL"]    = drB["OTDEL"];     // код отдела 
			drN["IDSTAT"]   = drB["IDSTAT"];    // Код  статті (№ бух.довідки)   			
			drN["ISFDU8"]   = drB["ISFDU8"];    // Признак(ISFDU8=1) для выбора сводов в ФДУ-8
			
			drN["NAIM"]     = drB["NAIM"];      // Название статьи (либо из NF_ NsiDoc:= NAIM - название  документа, либо другое)
			drN["SHIFR"]    = drB["SHIFR"];     // Шифр документа  (либо из NF_ NsiDoc, например:ФДУ-6М, ФДУ-1,... , либо другое)				
			drN["DOC"]      = drB["DOC"];       // Код документа (либо из NF_ NsiDoc,либо=пусто )
			drN["USERID"]   = uId;             // Id_код пользователя 
			drN["LASTDATE"] = DateTime.Now;    //  и текущая дата изменения
			drN["IP_USER"]  = RdaFunc.f_Ip_User(); // IP_ПК с которого выполненно обновление

		}

		// bB_Save_ItemClick-  - реакция на нажатие кнопки "Зберегти" - сохранения данных
	   private void bB_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   // bB_Save_ItemClick - метод используется при нажатии на кнопку "Зберегти" для сохра-
		   //                     нения данных в Oracle_таблице- архиве tBArh 
		   //-------------------------------------------------------------------------------------

		   bS_InfoLeft.Caption = "";
		   p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения

		   if (bB_Save.Enabled)  // Если кнопка сохранения доступна, то выполнить дейсвия
		   {

			   #region Объявление переменных и общие операции
			   string txt1 = "";
			   string txt2 = "";

			   string sTxt1 = (RDA.RDF.sLang == "R") ? "Ошибка" : "Помилка";
			   string sTxt2 = (RDA.RDF.sLang == "R") ? "Возникла ошибка при сохранении данных в таблице "
											  : "Виникла помилка при збереженні данних до таблиці ";
			   string sTxt3 = "\r\n\r\n" + RdaFunc.f_Repl(" ", 20)
							 + (RDA.RDF.sLang == "R" ? "Повторите изменения !" : "Повторите зміни !");
			   string tRoad = "";     // Значение ключа в таблице НСИ для  Ds_Nsi
			   string tIdStat = "";
			   bool bOk = false;

			   string rModi = "";    // Признак записи в Ds_Buffer : rModi ="I"-новая, ="U"-изменена, ="S"-без изменений,="D"-удалена
			   string tKey = "";    // Значение ключа в таблице НСИ
			   string KeyB = "";    // Значение ключа в таблице НСИ для  Ds_Buffer


			   bool isSave = false;  // = true - выполнено накопление строк для файла InfoUpdSrv


			   #endregion

			   try
			   {
				   txt1 = (RDA.RDF.sLang == "R" ? " сохранение изменений" : " зберегти зміни ")+"\r\n";
				   txt2 = RDA.RDF.sLang == "R" ? "Отмененo" : "Відмінно" ;
				   bOk = f_ErrRowNsi(myTabl, txt2 + txt1); // Если в записи есть ошибки (bOk=true), то не выполнять перехода

				   if (bOk == false)
				   {
					   p_DsNsi();             // Выбор данных из Oracle_таблиц НСИ в Ds_таблицу Ds_Nsi 

					   #region Блок обновления строк в Ds_Nsi из Ds_Buffer

					   foreach (DataRow drB in Ds.Tables["Ds_Buffer"].Rows)
					   {

						   rModi = (DataRowState.Added == drB.RowState ? "I"  // Признак записи : ="I"-новая 
								 : (DataRowState.Deleted == drB.RowState ? "D"       // ="D"-удалена 
								 : (DataRowState.Modified == drB.RowState ? "U"       // ="U"-изменена,
								 : "S")));                                           // ="S"-без изменений

						   if (rModi == "S") continue; // Пропускаем  не изменённую(Unchanged) запись

						   #region Блок обработки строки не удаленой (rModi != "D")  из  Ds_Buffer


						   if (rModi != "D")
						   {
							   tRoad = drB["ROAD"].ToString();   // Значение ключа в Ds_Buffer: дорога
							   tIdStat = drB["IDSTAT"].ToString(); // - IDSTAT
							   DataRow[] drN = Ds.Tables["Ds_Nsi"].Select(string.Format("ROAD='{0}' AND  IDSTAT = '{1}'", tRoad, tIdStat));
							   if (drN.Length > 0) //  ЕСТЬ строка с этим же ключом  в Ds_Nsi
							   {
								   p_UpdRowNsi(drB, drN[0]);                // -обновим значения в полях записи Ds_Nsi из Ds_Buffer
							   }
							   else                //  НЕТ строки с этим же ключом  в Ds_Nsi :        
							   {
								   DataRow drK = Ds.Tables["Ds_Nsi"].NewRow();  // -определим новую запись в Ds_Nsi
								   p_UpdRowNsi(drB, drK);                       // -добавим значения в поля записи из Ds_Buffer
								   Ds.Tables["Ds_Nsi"].Rows.Add(drK);           // -сохраним новую запись в Ds_Nsi
							   }

							   if (rModi == "U")
							   {
								   KeyB = drB["ROAD", DataRowVersion.Original].ToString()   // Старый  ключ записи 
										+ drB["IDSTAT", DataRowVersion.Original].ToString();
								   tKey = tRoad + tIdStat;   // Текущий ключ записи:  дорога  + код + отдел+ tOtdel 
								   if (!Equals(KeyB, tKey))   // Если был изменен и ключ записи :
								   {
									   p_UpdInfo(drB, myTabl, myTabl, "D", 0);  // -в  InfoUpdSrv для перезаписи на сервер УЗ или дороги
									   rModi = "I"; 			   // -изменение признака rModi для добавление записи 
								   }
							   }
						   }


						   p_UpdInfo(drB, myTabl, myTabl, rModi, 0);      // -в InfoUpdSrv для перезаписи на сервер УЗ или дороги
						   isSave = true;  //- выполнено накопление строк для файла InfoUpdSrv


						   #endregion
					   }

					   #endregion

					   #region Блок удаления строк из Ds_Nsi

					   foreach (DataRow drN in Ds.Tables["Ds_Nsi"].Rows)
					   {
						   tRoad = drN["ROAD"].ToString();   // Значение ключа в Ds_Buffer: дорога
						   tIdStat = drN["IDSTAT"].ToString(); // -исходный ключ IDSTAT(для новой записи =-1)

						   DataRow[] drB = Ds.Tables["Ds_Buffer"].Select(string.Format("ROAD='{0}' AND  IDSTAT = '{1}'", tRoad,  tIdStat));
						   if (drB.Length < 1 || drB[0].RowState == DataRowState.Deleted) // Запись подлежит удалению из  Ds_Nsi
						   {
							   p_UpdInfo(drN, myTabl, myTabl, "D", 0);	// -в  InfoUpdSrv для удаления на сервере УЗ или дороги
							   drN.Delete();                           // -пометим в Ds_Nsi запись на удаление
							   isSave = true;  //- выполнено накопление строк для файла InfoUpdSrv
						   }
					   }
					   #endregion

					   #region Блок обновления Oracle_таблиц  из Ds_Nsi, Ds_Nsi_RD и Ds_Info
					   if (isSave)
					   {
						   p_SetCommandBuilder(OraDANsi); // назначение OracleCommandBuilder для экземпляра OracleDataAdapter

						   OraDANsi.Update(Ds, "Ds_Nsi");       // Обновим данные из Ds_Nsi в Oracle_таблице БД РОДУЗ НФ
						   RdaFunc.p_SaveIntoInfo(null, null, null, null, null, 2);

					   }


					   #endregion

					   if (sender != null) // Если была нажата кн. bB_Save, то :
						   bE_Road.EditValue = myRoad;
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

		// p_SetCommandBuilder назначение OracleCommandBuilder для экземпляра OracleDataAdapter
		private void p_SetCommandBuilder(OracleDataAdapter oraDA)
		{
			//-------------------------------------------------------------------------------------
			// p_SetCommandBuilder - метод обеспечивает назначение  OracleCommandBuilder для 
			//                       oraDA(экземпляра OracleDataAdapter)  и выставление ему всех 
			//                       параметров для присвоения команд SQL(Update,Delete,Insert)
			//                            
			// Вызывается при сохранении(p_UpdArh) или удалении(p_DelArh) документа из Oracle_таблиц
			//-------------------------------------------------------------------------------------
				// Применим автогенерацию команд SQL(Update,Delete,Insert) для обновления 
				// Oracle_таблицы tBArh  по первичному ключу Doc,God,Mes,Road
				OraCBNsi = new OracleCommandBuilder(oraDA);
				oraDA.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				OraCBNsi.SetAllValues = true;
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
		   p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
		   string txt2 = (RDA.RDF.sLang == "R") ? "Отменено создание новой записи !"
										: "Скасовано створення нового запису !";
		   if (!f_ErrRowNsi(myTabl, txt2)) // Если в предыдущей записи НЕТ ошибок-создание новой
		   {
			   MaxNum += 1;

			   DataRowView drN = dtV.AddNew();// Определим новую запись :
			   drN.BeginEdit();
			   drN["RMODI"] = "I";            //  признак "I"-новая запись
			   drN["RKEY"]     = 0;      // признак  новой записи 
			   drN["USERID"]   = uId;     //               и его Id_код 
			   drN["LASTDATE"] = DateTime.Now; //  текущая дата

			   drN["ROAD"]     = myRoad;  // код дороги
			   drN["OTDEL"]    = myOtdel; // код отдела 
			   drN["IDSTAT"]   = MaxNum;  // Код статті (№ бух.довідки)   			
			   drN["ISFDU8"]   = 0;       // Признак(ISFDU8=1) для выбора сводов в ФДУ-8		   drN["NAIM"]     = "*";     // Название статьи (либо из NF_ NsiDoc:= NAIM - название  документа, либо другое)
			   drN["SHIFR"]    = "";      // Шифр документа  (либо из NF_ NsiDoc, например:ФДУ-6М, ФДУ-1,... , либо другое)				
			   drN["DOC"]      = "";      // Код документа(либо из NF_ NsiDoc, либо=пусто )
			   drN.EndEdit();
			   p_FooterBarInfo(drN);      // Информация о статусе текущей записи 

			   bUpdBuff        = true;    //       признак для сохранения в БД
			   bB_Save.Enabled = true;    // ДAктивизация кнопок : "Зберегти" 

			   myView.RefreshData();      // Обновим вид View на экране
			   p_RowFocus();
		   }

	   }

	   //bB_CancelEditRow_ItemClick - реакция на нажатие кнопки "Відмінити змін" - отмена добавления или изменения записи 
	   private void bB_CancelEditRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   //bB_CancelEditRow_ItemClick - метод используется при нажатии на кнопку "Відмінити змін"  
		   //                          для отмены добавления новой либо  изменений старой записи
		   //-------------------------------------------------------------------------------------
		   p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения

		   if (myView.FocusedRowHandle > 0)
		   {
			   DataRowView dr3 = dtV[myView.FocusedRowHandle];
			   if (dr3["RMODI"].ToString() == "I")   // Если отменено добавление новой записи, то
				   MaxNum -= 1;                      // уменьшим значение макс. номера 
			   dr3.Row.RejectChanges();    // Отменяет все изменения в строке до последнего AcceptChanges 
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


		   if (myView.FocusedRowHandle > -1)
		   {
			   //DataRowView dr4 = dtV[myView.FocusedRowHandle];
			   DataRow dr4 = myView.GetFocusedDataRow(); // Ds.Tables["Ds_Buffer"].Rows[tRow]; 

			   if (dr4["RKEY"].ToString() == "0")        // Если разрешено удаление 
			   {
				   if (dr4["RMODI"].ToString() != "I")      // - и запись не новая, то установим
				   {
					   bB_Save.Enabled = true;                //  ДeAктивизация кнопок : "Зберегти" 
					   bUpdBuff = true;                       //  признак для сохранения в БД
					   dr4["RMODI"] = "D".ToString();         // установим признак удаления
				   }
				   else                                     // - и запись новая, то
					   MaxNum -= 1;                           // уменьшим значение макс. номера 

				   dr4.AcceptChanges();                  // Приведем в соответствие ...View и строку Ds_таблицы
				   dr4.Delete();                         // - пометим ee на удаление
				   dr4.AcceptChanges();
			   }

			   else if (dr4["RKEY"].ToString() == "1")   //  Если запись используется в других таблицах, то сообщить
			   {
				   dr4.RejectChanges();                  // Отменяет все изменения в строке до последнего AcceptChanges ???????
				   string txt2 = RDA.RDF.sLang == "R" ? "Отмененo " : "Відмінно ";
				   bool bOk    = f_ErrRowNsi("ERRDEL", ""); // Если в записи есть ошибки (bOk=true), то не выполнять перехода
			   }
		   }
	   }

	   // p_NsiDoc-выборки списка входных(Tip=0) документов из NF_NSIDOC 
		private void p_NsiDoc()
		{
		   //-------------------------------------------------------------------------------------		
		   // p_NsiDoc-метод используется для выборки списка входных(Tip=0) документов из NF_NSIDOC 
		   //-------------------------------------------------------------------------------------		
		   try
		   {
			   OraDATmp = new OracleDataAdapter(@"SELECT Shifr, Naim, Doc  FROM NF_NSIDOC "
											   + @" WHERE Tip=0  ORDER BY DOC", RDA.RDF.BDOraCon);
			   Ds.Tables["Ds_Tmp"].Clear();
			   Ds.Tables["Ds_Tmp"].Dispose();

			   OraDATmp.Fill(Ds, "Ds_Tmp");
			   foreach (DataRow dr1 in Ds.Tables["Ds_Tmp"].Rows)
			   {
				   if (dr1["DOC"].ToString().Trim() == "DNF0FDU8") // Исключение для ФДУ-8
				   {
					   dr1["DOC"] = " ";                        // в код документа --> пусто
					   break;
				   }
			   }
			   Ds.Tables["Ds_Nsi"].AcceptChanges(); // Установим статус записей в Unchanged 

			   rItLUE_DOC.DataSource = Ds.Tables["Ds_Tmp"];
			   rItLUE_DOC.ValueMember = "DOC".ToString();    // Для поиска по коду предприятия
			   rItLUE_DOC.DisplayMember = "DOC".ToString();    // и отображения названия 
		   }
		   catch { }
		}

		private void rItLUE_NDOC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   // rItLUE_NDOC_EditValueChanging-метод используется ПЕРЕД выбором документа из NF_NSIDOC
		   //-------------------------------------------------------------------------------------
		   string txt = e.NewValue.ToString().Trim(); // Код документа из NF_NSIDOC либо=пусто 
		   if (myView.FocusedRowHandle > -1)
		   {
			   DataRowView drB = dtV[myView.FocusedRowHandle];  // Если выбрана не пустая запись из NF_NSIDOCDs.Tables["Ds_Buffer"].Rows
			   int tOtdel = Convert.ToInt32(drB["OTDEL"].ToString().Trim());

			   DataRow[] drN = Ds.Tables["Ds_Tmp"].Select(string.Format("DOC = '{0}'", txt));
			   drB.BeginEdit();
			   drB["DOC"]    = txt;              // Код документа(либо из NF_ NsiDoc, либо=пусто )
			   drB["SHIFR"]  = drN[0]["SHIFR"];  // Шифр документа  (либо из NF_ NsiDoc, например:ФДУ-6М, ФДУ-1,... , либо другое)				
			   drB["NAIM"]   = drN[0]["NAIM"];   // Название статьи (либо из NF_ NsiDoc:= NAIM - название  документа, либо другое)
			   drB["ISFDU8"] = f_IsFdu8(txt, tOtdel); 	// возвращаетcя значение признака(ISFDU8)
			   drB.EndEdit();
		   }
	   }

	   // FRMSTAT_NF_FormClosing - контроль ПЕРЕД закрытием формы
	   private void FRMSTAT_NF_FormClosing(object sender, FormClosingEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   //FRMNSI_FormClosing - метод используется при нажатии на кнопку формы "Х" для 
		   //                     выполнения действий ПЕРЕД закрытием формы. Проверяется  выполнено 
		   //                     ли сохранение ("Зберегти") изменений
		   //-------------------------------------------------------------------------------------
		   p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения
		   string txt1 = (RDA.RDF.sLang == "R" ? " закрытие формы" : " закриття формі")+"\r\n";
		   string txt2 = RDA.RDF.sLang == "R" ? "Отмененo " : "Відмінно ";

		   bool bOk = f_ErrRowNsi(myTabl, txt2 + txt1); // Если в записи есть ошибки (bOk=true), то не выполнять перехода
		   if (bOk == false && bUpdBuff)
			   bOk = f_ErrN(txt1, (RDA.RDF.sLang == "R" ? "В `" : "У  `") + myView.GroupPanelText + "`", "", "BUFF");
		   e.Cancel = bOk;

	   }

	   //bB_Exit_ItemClick - действия при акрытии формы
	   private void bB_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   //bB_Exit_ItemClick- метод используется при нажатии на кнопку "Вихід" для закрытия формы
		   //-------------------------------------------------------------------------------------
		   this.Close();

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

	   private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   // gridView_CellValueChanged- метод используется ПОСЛЕ обновления данных в поле IDSTAT
		   //                            для определения максимального значения кода :  
		   //                            если значение поля IDSTAT > MaxNum, то  MaxNum  = IDSTAT 
		   //-------------------------------------------------------------------------------------
		   if (bUpdBuff == false)
		   {
			   bUpdBuff = true;    //       признак для сохранения в БД
			   bB_Save.Enabled = true;    // ДAктивизация кнопок : "Зберегти" 
		   }
		   
		   if (e.Column.FieldName == "IDSTAT")
		   {
			   if ((int)e.Value > MaxNum)
				   MaxNum = (int)e.Value;
		   }
	   }

	   private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   // gridView_FocusedRowChanged - метод  используется ПОСЛЕ установки фокуса на строку 
		   //                              в grid1(...View1-8) 
		   //-------------------------------------------------------------------------------------
		   p_RowFocus();
	   }

	   private void gridView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
	   {
		   //-------------------------------------------------------------------------------------
		   // gridView_BeforeLeaveRow - метод используется ПEPEД потерей фокуса в строке грида для 
		   //                           контроля на наличие ошибок в записи :
		   //                           - если есть ошибки - запретим(e.Allow=false) уход с записи
		   //-------------------------------------------------------------------------------------
		   string txt2 = (RDA.RDF.sLang == "R"? "Отменен выход с записи " : "Відмінно вихід з запису ") +"\r\n " ;

		   e.Allow = !f_ErrRowNsi(myTabl, txt2);
	   }


	   #endregion	

		#region Методы, связанные c выбором и отображение списков в окнах LookUpEdit для : дорог, отделов по дорогам 

	   // p_LoadRoad - загрузка списка предприятий в rI_Road_репозиторий поля bE_Road
	   private void p_LoadRoad()
	   {
		   //-------------------------------------------------------------------------------------		
		   // p_LoadRoad - метод используется для загрузки списка предприятий из NsiRoad
		   //              в rI_Road_репозиторий для выбора предприятий в поле bE_Road
		   //-------------------------------------------------------------------------------------		
		   idxRoad = -1;
		   bRoad = false;          // Установим признак отсутствия данных в NsiRoad
		   string txt = " WHERE Pd = 1 ";
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

			   // Дополнительные тексты для f_ErrN и  f_ErrRowNsi
			   int idxN = rI_Road.GetDataSourceRowIndex("ROAD", Convert.ToInt32(e.NewValue.ToString())); // индекс строки в коллекции
			   string sNDI = rI_Road.GetDataSourceValue("NAIMD", idxN).ToString(); // по индексу строки 
			   string txt1 = (RDA.RDF.sLang == "R" ? " переход к " : "  перехiд до ") + sNDI
						   + (rI_Road.GetDataSourceValue("PD", idxN).ToString() == "1"
						   ? (RDA.RDF.sLang == "R" ? " дорогe" : " залiзниці") : "") +"\r\n";
			   string txt2 = (RDA.RDF.sLang == "R" ? "Отменен " : "Відмінно") ;

			   bool bOk = f_ErrRowNsi(myTabl, txt2 + txt1); // При ошибках(bOk=true),то не выполнять перехода

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
		   bS_InfoLeft.Caption = "";
		   myRoad  = 0;
		   idxRoad = -1; // индекс строки в коллекции
		   if (bRoad)
		   {
			   myRoad = Convert.ToInt32(bE_Road.EditValue);            // Сохранение кода предприятия 
			   idxRoad = rI_Road.GetDataSourceRowIndex("ROAD", myRoad); // индекс строки в коллекции
		   }
		   p_DsNsi();   // выбор данных из Oracle_таблиц NF_NSISTAT 
		   p_ReadStat();
		   p_LoadOtdel();  // загрузка списка отделений по дороге в rI_Otdel_репозиторий поля bE_Otdel

	   }

		// p_LoadRoad - загрузка списка отделов в rI_Otdel_репозиторий поля bE_Otdel
		private void p_LoadOtdel()
		{
			//-------------------------------------------------------------------------------------		
			// p_LoadOtdel - метод используется для загрузки списка отделов по предприятию из 
			//               NsiOtdel в rI_Otdel_репозиторий для выбора отделов в поле bE_Otdel
			//-------------------------------------------------------------------------------------		

			idxOtdel = -1;      // индекс строки в коллекции
			bOtdel = false;   // Установим признак отсутствия данных в NsiOtdel
			try
			{
				using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM NsiOtdel "
										  + @" WHERE Road=" + myRoad.ToString()
										  + @" AND Otdel > 99 AND Otdel < 104 "
										  + @" ORDER BY Road, Otdel", RDA.RDF.BDOraCon))
				{
					using (DataTable dt = new DataTable())
					{
						oraDA.Fill(dt);
						rI_Otdel.DataSource = dt;
						if (dt.Rows.Count > 0) bOtdel = true; // Установим признак наличия данных в NsiRoad

						bE_Otdel.EditValue = dt.Rows[0]["OTDEL"]; // в списке-выбрана первая дорога 
						myOtdel = Convert.ToInt32(bE_Otdel.EditValue);
						idxOtdel = rI_Otdel.GetDataSourceRowIndex("OTDEL", myOtdel); // индекс строки в коллекции
					}
				}
			}
			catch { }

		}
		// rI_Otdel_EditValueChanging -контроль ПЕРЕД изменением отдела по предприятию
		private void rI_Otdel_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// rI_Otdel_EditValueChanging-метод используется ПЕРЕД изменением отдела по предприятию
			//            для принятия решения, если были изменения и не выполнено "Зберегти"
			//-------------------------------------------------------------------------------------
			//bool bOk = false;
			if (myOtdel != Convert.ToInt32(e.NewValue.ToString()))
			{
				p_CloseAllEditors();// закрытие редактора ячейки для сохранения последнего изменения

				// Дополнительные тексты для f_ErrN и  f_ErrRowNsi
				int idxN = rI_Otdel.GetDataSourceRowIndex("OTDEL", Convert.ToInt32(e.NewValue.ToString())); // индекс строки в коллекции
				string sNDI = rI_Otdel.GetDataSourceValue("NAIM", idxN).ToString(); // по индексу строки 
				string txt1 = (RDA.RDF.sLang == "R" ? " переход к отделу " : " перехiд до відділу ") + sNDI+"\r\n";
				string txt2 = RDA.RDF.sLang == "R" ? "Отменен " : "Відмінно" ;

				bool bOk = f_ErrRowNsi(myTabl, txt2 + txt1); // При ошибках(bOk=true),то не выполнять перехода
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
			myOtdel = 0;
			idxOtdel = -1;      // индекс строки в коллекции
			if (bOtdel)
			{
				myOtdel = Convert.ToInt32(bE_Otdel.EditValue); // сохраним код  
				idxOtdel = rI_Otdel.GetDataSourceRowIndex("OTDEL", myOtdel); // индекс строки в коллекции
			}
				p_ViewPage1(); // Отображаем на экране вид myView=advBandedGridView3 

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
			myView.FocusedRowHandle = 1;// перевод фокуса в нужную ячейку 
			myView.ShowEditor();        // вызов редактора ячейки 
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

		//  bB_ImpExp_ItemClick - вызов менеджера экспорта
		private void bB_ImpExp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//-------------------------------------------------------------------------------------
			// bB_ImpExp_ItemClick - метод используется при вызове менеджера экспорта-импорта
			//-------------------------------------------------------------------------------------
			dataManager1.ImportFileName = "NSISTAT";   // NSISTAT.xls- код документа xls
			dataManager1.CallManager();
		}

		private void p_XlsOut(bool bPrintDoc)
		{
			//-------------------------------------------------------------------------------------		
			// p_XlsOut( )- метод обеспечивает формирование и\или печать выходного Excel_документа
			//
			// Параметр: bPrintDoc = true-печать без отображения на экране Excel_документа
			//
			// Для печати шапки на каждой странице в Excel_документе следует выбрать:
			//     Файл\Параметры страницы\Лист-Сквозные строки и указать их (например,$1:$5) 
			//-------------------------------------------------------------------------------------		
			//using (xls = new Office.Excel(myDoc, Road.EditValue.ToString(), SGod.EditValue.ToString(), SMes.EditValue.ToString().PadLeft(2, '0')))
			p_CloseAllEditors(); // закрытие редактора ячейки 

			string tSpk = "НДІ Бухгалтерські довідки по "
					   + rI_Road.GetDataSourceValue("NAIMD", idxRoad).ToString() + " залізниці";

			string rFormat  = null; // в rFormat  - форматы ячеек данных : строка 1
			string rFormatO = null; // в rFormatO - форматы ячеек названия отдела : строка 2
			string rValue   = null; // в rValue  - формируются данные

			int    tOtdel   = -1 ;  // код отдела
			string txt      = "";   // текст названия отдела  
			int    idxN     = 0;    // индекс строки в коллекции отделов rI_Otdel
			int    i        = 6;    // № строки с которой начинается заполнение данных 

			using (xls = new Office.Excel("NSISTAT", "", "", ""))
			{
				//xls.Visible = true;
				xls.SelectPage("NSISTAT");
				rFormat  = "A1:E1";  // в rFormat  - форматы ячеек данных в строке 1 : A1-E1
				rFormatO = "A2:E2";  // в rFormatO - ... названия  отдела в строке 2 : A2-E2

				xls.SetValue("A3", tSpk); // Заголовок документа

				#region  Заполнение ячеек Excel_документа

				DataRow[] rows = Ds.Tables["Ds_Buffer"].Select("", "Road, Otdel, idstat");
				foreach (DataRow drSh in rows)  // Ds.Tables["Ds_Buffer"].Rows
				{
					if (tOtdel != Convert.ToInt32(drSh["OTDEL"].ToString())) // Изменен код отдела
					{
						i++;
						tOtdel = Convert.ToInt32(drSh["OTDEL"].ToString()); // код отдела
						idxN   = rI_Otdel.GetDataSourceRowIndex("OTDEL", tOtdel); // индекс строки в rI_Otdel
						txt = "Відділ " + tOtdel.ToString() + "( "
							+ rI_Otdel.GetDataSourceValue("KOD", idxN).ToString() + ") - " // Шифр отдела
							+ rI_Otdel.GetDataSourceValue("NAIM", idxN).ToString();         // Названия отдела

						rValue = string.Format("A{0}:E{0}", i);
						xls.CopyRangeTo(rFormatO, rValue); // Копируем формат в выделению строку 
						xls.SetValue("A" + i, txt);

						//xls.SetValue("A" + i, txt.Replace("\r", ""));
						xls.SetBorderStyle(rValue, Office.Excel.XlLineStyle.xlContinuous, Office.Excel.XlBorderWeight.xlThin);

					}
					i++;
					rValue = string.Format("A{0}:E{0}", i);
					xls.CopyRangeTo(rFormat, rValue); // Копируем формат в выделению строку 

					xls.SetValue("A" + i, drSh["IDSTAT"].ToString());
					xls.SetValue("B" + i, drSh["DOC"].ToString());
					xls.SetValue("C" + i, drSh["SHIFR"].ToString());
					xls.SetValue("D" + i, drSh["NAIM"].ToString().Replace("\r", ""));
					xls.SetValue("E" + i, Convert.ToInt16(drSh["ISFDU8"]));

					xls.SetBorderStyle(rValue, Office.Excel.XlLineStyle.xlContinuous, Office.Excel.XlBorderWeight.xlThin);
			   
				}
				#endregion
				xls.DeleteRow("A1:A2"); // удалить 1-ю и 2-ю  строки форматов 

				if (bPrintDoc)
				{
				   xls.Visible = false;
				   PRT.printset ps = new PRT.printset(xls); // -создать новый класс для выбора
				   ps.ShowDialog();                         //    принтера в режиме диалога
				}
				else
				{
				   xls.Visible = true;
				}
				xls.SaveDocument();

			}

		}


		private void dataManager1_OnExcelExport(RDA.ManagerElementsEventArgs args)
		{
			//-------------------------------------------------------------------------------------
			// dataManager1_OnExcelExport - метод используется при вызове менеджера экспорта-импорта 
			//		                        для отображения документа в виде EXCEL-файла, cформиро-
			//		                        ваного и сохраненного в папке Excel_Output(см. p_XlsOut)
			// При этом: Excel_документ отображается, но автоматически НЕ открывается окно диалога
			//	         для выбора принтера. Здесь для печати принтер выбирается в Excel 
			//-------------------------------------------------------------------------------------

			p_XlsOut(false);// Если Excel_документ НЕ cформирован, то сформировать и открыть...

		}
		private void dataManager1_OnFilePrint(RDA.ManagerElementsEventArgs args)
		{
			//-------------------------------------------------------------------------------------
			// dataManager1_OnFilePrint - метод используется при вызове менеджера экспорта-импорта  
			//	             для печати документа, сохраненного в папке Excel_Output(см. p_XlsOut).
			//При этом : Excel_документ НЕ отображается, но автоматически открывается окно диалога
			//		     для выбора принтера. 
			//-------------------------------------------------------------------------------------
			p_XlsOut(true);  // Excel_документ НЕ отображать, но показывать окно выбора принтера
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
		   //                 от выбранного языка : RDA.RDF.sLang = U-украинский, =R-русский
		   //-------------------------------------------------------------------------------------
		   try
		   {
			   gB_5.ToolTip = gB_5.Caption + ":\r\n   ";
			   #region Подсказки(ToolTip) на украинском языке

			   if (RDA.RDF.sLang == "U")   // Выбор украинского языка
			   {
				   bB_Save.Hint      = "    CTRL+S :  зберегти всі доданi, видалені та змінені записи в НДІ";
				   bB_ImpExp.Hint     = "     Перегляд та  друк  даних";
				   bB_AddRow.Hint    = "    створити новий запис";
				   bB_CancelEditRow.Hint = "    скасовати додавання нової або зміни обраного запису";
				   bB_DelRow.Hint    = "    CTRL+DELETE :  видалити запис з НДІ";
				   bB_Exit.Hint      = "    Alt+F4 :  закриття форми";
				   bE_Road.Hint      = "Виберіть залізницю";
				   bE_Otdel.Hint     = " Виберіть відділ(НФЛ, НФГ, НФДУ) в якому потрібно переглянути  або налаштувати  бухгалтерські довідки";

				   l_rKey1.Text      = "Неможливо змінити(Код, Шифр, Документ), якщо ключ (Код) використовується в інших таблицях ";  //для rKey =1

				   gB_IDSTAT.ToolTip = "Код формується автоматично, але може бути змінений, якщо він не використовується в інших таблицях";
				   gB_NAIM.ToolTip   = "Назва бухгалтерьскої довідки з НДІ документів(NF_ NsiDoc) або інше";
				   gB_DOC.ToolTip    = "Код документу з НДІ документів(NF_ NsiDoc) або пусто )";
				   gB_SHIFR.ToolTip  = "Шифр документу з НДІ документів(NF_ NsiDoc) або пусто)";
				   gB_5.ToolTip     += "√ (галочка)- ознака того, що дані документу викоритовуються\r\n  в якості зведень для формування відомостей ФДУ-8";
			   }
			   #endregion

			   #region Подсказки(ToolTip) на русском языке

			   else
			   {
				   bB_Save.Hint = "    CTRL+S :  сохранить все добавленные, удаленные и измененные записи в НСИ";
				   bB_ImpExp.Hint = "    Просмотр и печать данных";
				   bB_AddRow.Hint = "    Создать новую запись";
				   bB_CancelEditRow.Hint = "    отменить добавление новой или изменение выбранной записи";
				   bB_DelRow.Hint = "    CTRL+DELETE :  удалить запись из НСИ";
				   bB_Exit.Hint = "    Alt+F4 :  закрыть форму";
				   bE_Road.Hint      = "    виберите дорогу";
				   bE_Otdel.Hint     = "Виберите отдел(НФЛ, НФГ, НФДУ), в котором требуется просмотреть или изменить бухгалтерские проводки";

				   l_rKey1.Text      = "Нельзя заменить ключ(Код, Шифр, Документ), если ключ (Код) используемый в других таблицах. "; 

				   gB_IDSTAT.ToolTip = "Код формируется автоматически, но может быть изменен, если не используется в других таблицах";
				   gB_NAIM.ToolTip   = "Название бухгалтерской справки из НСИ документов(NF_ NsiDoc) или иное ";
				   gB_DOC.ToolTip    = "Код документа из НСИ документов(NF_ NsiDoc) или пусто )";
				   gB_SHIFR.ToolTip  = "Шифр документа из НСИ документов(NF_ NsiDoc) или пусто)";
				   gB_5.ToolTip      += "√ (галочка)- означает, что данные документа используются\r\n   в качестве сводов для формировании ведомости ФДУ-8 ";

			   }
			   #endregion
			   v_ISFDU8.ToolTip = gB_5.ToolTip ;
			   DataRowView dr1 = dtV[myView.FocusedRowHandle];
			   p_FooterBarInfo(dr1); // информация об режиме работы с документом 

		   }
		   catch { }

		}

		// p_FooterBarInfo - информация о допустимости изменений и статусе записи 
		private void p_FooterBarInfo(DataRowView drN)
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

			   DataRow dr1 = myView.GetFocusedDataRow(); // определим запись 

			   bS_Info.Caption = RdaFunc.f_RowChangeInfo(dr1);        // информация об изменении записи
			   bS_Info.Hint = (RDA.RDF.sLang == "U")
							   ? "  Дата останньої зміни запису, ПІБ користувача(що вніс зміни) та IP_ПК"
							   : "  Дата последнего изменения записи, ФИО пользователя(внесшего изменение) и IP_ПК";

		   }
		   catch { } // ИНАЧЕ отменяем показ статуса записи 

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
		   string sTxt3 = "\n\n   " + par2;
		   string sTxt4 = "\n\n" + RdaFunc.f_Repl(" ", 60);
		   string sTxt5 = "\nТак        - ";
		   string sTxt6 = "\nНі           - ";
		   string sTxt7 = "\nВідміна  - ";
		   string sTxt8 = par3 + "\n" + RdaFunc.f_Repl("-", 90);

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
				   bB_Save_ItemClick(null, null); // СОХРАНИМ изменения 
				   break;
			   case DialogResult.No:
				   bUpd = false;
				   break;
			   default:
				   bUpd = true;                   // ОТМЕНИМ указанное действие
				   break;
		   }

		   #endregion
		   bUpdBuff = bUpd;
		   return (bUpd);
		}


		// f_iSContainsDubl - возвращает true при наличие дубля кода IdStat в Ds_Buffer  
		private bool f_iSContainsDubl(int tRoad, int tIdStat)
		{
			//-------------------------------------------------------------------------------------		
			// f_iSContainsDubl - возвращает true при наличие дубля кода IdStat в Ds_Buffer
			//                    Используется для проверки на дубль для новой или старой записи
			// Параметры :  tIdStat - контролируемое значение IdStat
			//            
			//-------------------------------------------------------------------------------------		
			bool ResuLt = false;   // Возвращаемое false соответствует отсутствию ошибок 

			if (tIdStat >= 0)
			{
				DataRow[] drN = Ds.Tables["Ds_Buffer"].Select(string.Format("ROAD={0} AND IDSTAT = {1}", tRoad, tIdStat));

				if (drN.Length > 1) ResuLt = true;
			}
			return (ResuLt);

		}

		// f_ErrRowNsi-функция обеспечивает контроль в записях grid1
		private bool f_ErrRowNsi(string par1, string par2)
		{
			//-------------------------------------------------------------------------------------
			// f_ErrRowNsi-функция обеспечивает контроль в записях grid1
			//		на:   - не допущение дублирования уникальных ключей
			//            - ошибок при заполнении обязательных полей
			// При возникновении ошибки возвращается true и устанавливается bNsiErr = true
			// Если RDA.RDF.sLang ="R"-выдача сообщений на русском иначе -на украинском("U") языке
			//-------------------------------------------------------------------------------------
			// Параметры: par1= "ERRADD" -добавление, ="ERRMODI"-изменение,="ERRDEL"-удаление записи,
			//                = myTabl-имя таблицы(где контролируется запись) определяется в блоке
			//                      
			// par2= "" или информация об отмене действия(например, "отменяется выбор нового НСИ")
			//-------------------------------------------------------------------------------------
			// Функция вызывается в блоках: sB_DelRow_Click,      gridView_BeforeLeaveRow
			//                              gridView_ValidateRow, gridView_CellValueChanged
			//-------------------------------------------------------------------------------------
			bNsiErr = false;    // Признак отсутствия ошибки в записи Ds_Buffer 
			int iE = 0;
			int j = 0;
			int tIdStat = 0; // -исходный ключ IDSTAT
			string sTxt1 = (RDA.RDF.sLang == "R") ? "Ошибка в записи" : "Помилка в запису";
			string sTxt2 = "";
			string sTxt3 = "\r\n\r\n" + RdaFunc.f_Repl(" ", 20) +
						   (RDA.RDF.sLang == "R" ? "Исправьте ошибку и продолжите работу."
									   : "Виправте помилку і продовжить роботу.");
			try
			{
				#region Выбор значения par1
				switch (par1.ToUpper().Trim())
				{
					case "NF_NSISTAT":
						#region Контроль в НСИ
						j = 0;

						foreach (DataRow drN in Ds.Tables["Ds_Buffer"].Rows)
						{
							tIdStat = Convert.ToInt32(drN["IDSTAT"].ToString()); // -исходный ключ IDSTAT(для новой записи =-1)
					        bNsiErr = f_iSContainsDubl(myRoad, tIdStat );  // возвращает true при наличие дубля кода IdStat в Ds_Buffer  
							if(bNsiErr)
							{
								sTxt2 = (RDA.RDF.sLang == "R" ? " - дублирование" : " - дублювання")
												     + " в поле Код = " +tIdStat.ToString() + "\r\n";
						        break;
							}
						}

						break;
						#endregion
					case "ERRADD":
						#region Текст ошибки при добавлении записи
						sTxt1 = (RDA.RDF.sLang == "R") ? "Ошибка при добавлении записи" : "Помилка при додаванні запису";

						break;
						#endregion
					case "ERRMODI":
						#region Текст ошибки при изменении записи
						sTxt1 = (RDA.RDF.sLang == "R") ? "Ошибка при изменении записи" : "Помилка при змін запису";
						sTxt2 = (RDA.RDF.sLang == "R") ? "Ключ записи " + myKey.ToString() + " используется в других таблицах"
												: "Ключ запису " + myKey.ToString() + " використовується в інших таблицях";
						break;
						#endregion
					case "ERRDEL":
						#region Текст ошибки при удалении записи
						sTxt2  = (RDA.RDF.sLang == "R") ? "Нельзя удалить запись !" : " Неможливо видалить запис! ";
						sTxt2 += "\r\n\r\n     " + (RDA.RDF.sLang == "R" ? "Запись с кодом " + myKey.ToString() + " используется в других таблицах"
											: "Запис з кодом " + myKey.ToString() + " використовується в інших таблицях");

						break;
						#endregion
					case "ERRCELL":
						#region Текст ошибки в ячейке записи
						sTxt1 = RDA.RDF.sLang == "R" ? "Ошибка в поле записи" : "Помилка в поле запису";
						sTxt2 = RDA.RDF.sLang == "R" ? " - недопустимое значение в поле "
											: " - неприпустиме значення в поле ";
						sTxt3 += "\r\n\n" + par2;
						break;
						#endregion

					default:
						sTxt2 = "";
						break;
				}
				#endregion

				#region  Формирование сообщения об ошибке
				if (sTxt2 != "")
				{
					bNsiErr = true;  // Признак наличия ошибки в записи Ds_Buffer 
					ERR.Error err6 = new ERR.Error(sTxt1, ERR.ErrorImages.Examplemation, par2+ sTxt2 + sTxt3, null, 1);
					err6.ShowDialog();
				}
				#endregion
			}
			catch
			{
			}

			return (bNsiErr);
		}
	

	   #endregion

		#region Процедура реалізайії "гарячих" клавіш

		private void FRMSTAT_NF_KeyDown(object sender, KeyEventArgs e)
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