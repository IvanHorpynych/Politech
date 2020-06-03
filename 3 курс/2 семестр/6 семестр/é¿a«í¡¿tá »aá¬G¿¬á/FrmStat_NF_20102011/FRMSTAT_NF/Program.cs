using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CoreLab.Oracle;

namespace FRMSTAT_NF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			RDA.RDF.BDOraCon = new OracleConnection("Server=vd.uz.gov.ua;User Id=roduz;Password=rd;Direct=True;Port=1522;Sid=VD;");
			Helpers.ConnectionHelper.InitializeConnection();
			
			//RDA.RDF.BDOraCon.Open();
			//RDA.RDF rdaf = new RDA.RDF();
			//rdaf.p_EditTabl("1");
			Application.Run(new FRMSTAT_NF(RDA.RDF.BDOraCon, 1, "Supervisor", 'F', 12, 2004));
			

        }
    }
}
