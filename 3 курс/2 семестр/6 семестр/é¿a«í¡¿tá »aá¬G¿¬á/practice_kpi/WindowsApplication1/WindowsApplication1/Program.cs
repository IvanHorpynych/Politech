using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CoreLab.Oracle;           //  OraDirect для связи с базой Oracle

namespace WindowsApplication1
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
            Application.Run(new Form1(
                new OracleConnection("Server=vd.uz.gov.ua;User Id=tmproduz;Password=rd;Direct=True;Port=1522;Sid=VD;")
                ));
		}
	}
}