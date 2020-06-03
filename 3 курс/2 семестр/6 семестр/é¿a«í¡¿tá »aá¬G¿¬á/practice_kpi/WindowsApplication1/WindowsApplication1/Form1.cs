using System;                   // Объявляем пространство имен System
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;     //  ---  для Form
using System.Data;
using CoreLab.Oracle;   // OraDirect для связи с базой Oracle
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using Repository = DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using System.Text.RegularExpressions;

namespace WindowsApplication1
{
	public partial class Form1 : DevExpress.XtraEditors.XtraForm
	{
        private OracleConnection oraCon;
        private int idxNSI = -1;     // Индекс выбранной строки в списке НСИ
        private int idxComp = -1;     // Индекс выбранной строки в списке підприємств
        private int myComp = -1;     //код  підприємства
        private int myDep = -1;     //код відділу
        private DataSet Ds;
        private OracleDataAdapter OraDABuffer;  // "Ds_Buffer"-временная область для выборки данных из Oracle_таблиц
        // Создаем экземпляр(myView) класса ... AdvBandedGridView для получения доступа к свойствам вида
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView myView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();

        private string myTabl = "";    // Текущее имя обрабатываемой таблицы 
        
        public Form1(OracleConnection _oraCon)
        {
            InitializeComponent();
            oraCon = _oraCon;
            oraCon.Open();
            
        }

		private void Form1_Load(object sender, EventArgs e)
		{
            Ds = new DataSet();

            Ds.Tables.Add("Ds_Buffer");
            myView = bandedGridView2;
            p_LoadComp();
            p_ListNDI();

            lE_ListTabl_EditValueChanged(null, null);
            
                    
		}

		// p_ListNDI - загрузка списка обрабатываемых НСИ в rI_NDI_репозиторий поля bE_NDI
		private void p_ListNDI()
		{
			//-------------------------------------------------------------------------------------
			// p_ListNDI - формирование списка обрабатываемых НСИ в RepositoryItemLookUpEdit(rI_NDI)
			//-------------------------------------------------------------------------------------
			DataTable tNdi = new DataTable();
			tNdi.Columns.AddRange(new DataColumn[] { new DataColumn("ID", typeof(int)), new DataColumn("NAIM", typeof(string)) });
			tNdi.LoadDataRow(new object[] { 0, "Список підприємств" }, true);
			tNdi.LoadDataRow(new object[] { 1, "Список посад" }, true);
            tNdi.LoadDataRow(new object[] { 2, "Список посад по підприємствам" }, true);
            tNdi.LoadDataRow(new object[] { 3, "Список відділів по підприємствам" }, true);
			tNdi.LoadDataRow(new object[] { 4, "Список співробітників підприємств по відділам" }, true);
			lE_ListTabl.Properties.DataSource = tNdi;
			idxNSI = 0;
		}

		// lE_ListTabl_EditValueChanged- действия  ПОСЛЕ выбора НСИ из списка 
		private void lE_ListTabl_EditValueChanged(object sender, EventArgs e)
		{
           lE_Comp.Visible = false;
           lE_Dep.Visible = false;
           label2.Visible = false;
           label3.Visible = false;
            myTabl = ""; // Текущее имя обрабатываемой таблицы 
            if (idxNSI != Convert.ToInt16(lE_ListTabl.EditValue.ToString())) // Индекс выбранной строки в списке НСИ
                idxNSI = Convert.ToInt16(lE_ListTabl.EditValue.ToString());

            switch (idxNSI)
            {
                case 0:
                    myTabl = "ANSICOMP";
                    myView = bandedGridView2;
                    

                    OraDABuffer = new OracleDataAdapter(@"SELECT * FROM ANSICOMP ", oraCon);
                    break;

                case 1:
                    myTabl = "ANSIPOS";
                    myView = bandedGridView3;

                    OraDABuffer = new OracleDataAdapter(@"SELECT * FROM ANSIPOS ", oraCon);
                    break;

                case 2:
                    myTabl = "ANSICP";
                    myView = bandedGridView4;
                    lE_Comp.Visible = true;
                    label2.Visible = true;
                    label3.Visible = false;
                    lE_Comp_EditValueChanged(null, null);
                    //OraDABuffer = new OracleDataAdapter(@"SELECT t1.id_comp, t2.name comp_name, t1.id_pos, t3.name pos_name, t1.plan_count, t1.fact_count FROM ansicp t1 "
                    //                                    +@"LEFT JOIN (SELECT id_comp, name FROM ansicomp) t2 ON t2.id_comp=t1.id_comp "
                    //                                    +@"LEFT JOIN (SELECT id_pos, name FROM ansipos) t3 ON t3.id_pos=t1.id_pos "
                    //                                    +@"ORDER BY t1.id_comp", oraCon);
                    break;

                case 3:
                    myTabl = "ANSIDEP";
                    myView = bandedGridView5;
                    lE_Comp.Visible = true;
                    label2.Visible = true;
                    label3.Visible = false;
                    lE_Comp_EditValueChanged(null, null);
                    //OraDABuffer = new OracleDataAdapter(@"SELECT t1.id_comp, t2.name comp_name, t1.id_dep, t1.name dep_name, t1.shifr FROM ansidep t1 "
                    //                                    + @"LEFT JOIN (SELECT id_comp, name FROM ansicomp) t2 ON t2.id_comp=t1.id_comp", oraCon);
                    break;

                case 4:
                    myTabl = "ANSIEMP";
                    myView = bandedGridView1;
                    lE_Comp.Visible = true;
                    lE_Dep.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    lE_Comp_EditValueChanged(null, null);
                    break;

                default:
                    break;
            }

            if (idxNSI > -1 && idxNSI < 2 || idxNSI == 4)
            {
                Ds.Tables["Ds_Buffer"].Dispose(); // освободим все ресурсы 
                Ds.Tables["Ds_Buffer"].Clear();   // очистим   все данные в таблице    
                OraDABuffer.Fill(Ds, "Ds_Buffer"); // заполним  Ds_таблицу (Ds_Buffer) данными НСИ
                gridControl1.MainView = myView;   // Устанавим вид как главный(MainView)
                gridControl1.DataSource = Ds.Tables["Ds_Buffer"];// и отобразим данные в гриде
            }

		}

        // p_LoadComp - загрузка списка предприятий в rI_Road_репозиторий поля bE_Road
        private void p_LoadComp()
        {
            idxComp = -1;
            try
            {
                using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM ANSICOMP "
                                                 + " ORDER BY ID_COMP", oraCon))
                {
                    using (DataTable dt = new DataTable())
                    {
                        oraDA.Fill(dt);
                        lE_Comp.Properties.DataSource = dt;
                        lE_Comp.EditValue = dt.Rows[0]["ID_COMP"]; // в списке-выбрана первая дорога 
                        myComp = Convert.ToInt32(lE_Comp.EditValue);
//                        idxComp = lE_Comp.GetDataSourceRowIndex("ROAD", myRoad); // индекс строки в коллекции
                    }
                }
            }
            catch {}

        }

        private void lE_Comp_EditValueChanged(object sender, EventArgs e)
        {
            myComp = Convert.ToInt32(lE_Comp.EditValue);
            switch (myTabl)
            {
                case "ANSICP":
                    p_NewPos();
                    break;
                case "ANSIDEP":
                    p_NewDep();
                    break;
                case "ANSIEMP":
                   // lE_Dep_EditValueChanged(null, null);
                    p_LoadDep(); 
                    break;
                default:
                    //p_NewPos();
                    break;
            }
            

        }
        private void p_NewPos()
        {

            OraDABuffer = new OracleDataAdapter(@"SELECT t1.id_comp, t2.name comp_name, t1.id_pos, t3.name pos_name, t1.plan_count, t1.fact_count FROM ansicp t1 "
                                    + @"LEFT JOIN (SELECT id_comp, name FROM ansicomp) t2 ON t2.id_comp=t1.id_comp "
                                    + @"LEFT JOIN (SELECT id_pos, name FROM ansipos) t3 ON t3.id_pos=t1.id_pos "
                                    + @"WHERE t1.ID_Comp=" + myComp 
                                    + @"ORDER BY t1.id_comp", oraCon);
            Ds.Tables["Ds_Buffer"].Dispose(); // освободим все ресурсы 
            Ds.Tables["Ds_Buffer"].Clear();   // очистим   все данные в таблице    
            OraDABuffer.Fill(Ds, "Ds_Buffer"); // заполним  Ds_таблицу (Ds_Buffer) данными НСИ
            gridControl1.MainView = myView;   // Устанавим вид как главный(MainView)
            gridControl1.DataSource = Ds.Tables["Ds_Buffer"];// и отобразим данные в гриде

        }
        private void p_LoadDep()
        {
            //idxDep = -1;                    

            try
            {
                using (OracleDataAdapter oraDA = new OracleDataAdapter(@"SELECT * FROM ANSIDEP "
                                                 + " WHERE id_comp=" + myComp + " ORDER BY ID_DEP", oraCon))
                {
                    using (DataTable dt = new DataTable())
                    {
                        oraDA.Fill(dt);
                        lE_Dep.Properties.DataSource = dt;
                        lE_Dep.EditValue = dt.Rows[0]["ID_DEP"]; // в списке-выбрана первая дорога 
                        myDep = Convert.ToInt32(lE_Dep.EditValue);
                        //                        idxComp = lE_Comp.GetDataSourceRowIndex("ROAD", myRoad); // индекс строки в коллекции
                    }
                }
            }
            catch { }

        }

        private void lE_Dep_EditValueChanged(object sender, EventArgs e)
        {
            myDep = Convert.ToInt32(lE_Dep.EditValue);
            p_NewEmp();
        }

        private void p_NewDep()
        {

            OraDABuffer = new OracleDataAdapter(@"SELECT t1.id_comp, t2.name comp_name, t1.id_dep, t1.name dep_name, t1.shifr FROM ansidep t1 "
                                                        + @"LEFT JOIN (SELECT id_comp, name FROM ansicomp) t2 ON t2.id_comp=t1.id_comp "
                                                        + @"WHERE t1.ID_Comp= " + myComp
                                                        + @" ORDER BY t1.id_dep", oraCon);
            Ds.Tables["Ds_Buffer"].Dispose(); // освободим все ресурсы 
            Ds.Tables["Ds_Buffer"].Clear();   // очистим   все данные в таблице    
            OraDABuffer.Fill(Ds, "Ds_Buffer"); // заполним  Ds_таблицу (Ds_Buffer) данными НСИ
            gridControl1.MainView = myView;   // Устанавим вид как главный(MainView)
            gridControl1.DataSource = Ds.Tables["Ds_Buffer"];// и отобразим данные в гриде

        }

        private void p_NewEmp()
        {
            string xxx = myDep.ToString() + "---" + myComp.ToString();
            OraDABuffer = new OracleDataAdapter(@"SELECT t2.id_comp, t1.id_dep, t2.name dep_name, t1.id_emp, t1.surname emp_surname, t1.name emp_name, t1.fathname emp_fathname, "
                                                        + @"t1.id_pos, t3.name pos_name, t1.male_female, t1.year_birth, t1.edu, t1.pay FROM ansiemp t1 "
                                                        + @"LEFT JOIN (SELECT id_dep, name, id_comp FROM ansidep) t2 ON t2.id_dep=t1.id_dep "
                                                        + @"LEFT JOIN (SELECT id_pos, name FROM ansipos) t3 ON t3.id_pos=t1.id_pos "
                                                        + @"WHERE t2.id_comp = " + myComp + " AND t1.ID_DEP =" + myDep
                                                        + @" ORDER BY t1.id_dep", oraCon);
            Ds.Tables["Ds_Buffer"].Dispose(); // освободим все ресурсы 
            Ds.Tables["Ds_Buffer"].Clear();   // очистим   все данные в таблице    
            OraDABuffer.Fill(Ds, "Ds_Buffer"); // заполним  Ds_таблицу (Ds_Buffer) данными НСИ
            gridControl1.MainView = myView;   // Устанавим вид как главный(MainView)
            gridControl1.DataSource = Ds.Tables["Ds_Buffer"];// и отобразим данные в гриде

        }


        private void AddRow_Click(object sender, EventArgs e)
        {
            switch (myTabl)
            {
                case "ANSICOMP":
                    //myView = bandedGridView2;

                    break;

                default:
                    break;
            }
        }

        private void DelRow_Click(object sender, EventArgs e)
        {

        }

        private void SaveTable_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        


	}
}