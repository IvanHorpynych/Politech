using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zorin_Lab_3 {
    class Lab3 {

        [STAThread]
        static void Main(string[] args) {

            //StringWriter sw = new StringWriter();
            //TextWriter standartOut = Console.Out;
            //Console.SetOut(sw);

            Matrix mat = new Matrix(new double[,] { 
            { 10, 4, 20, 19, 37}, 
            { 1, 30, 18, 10, 71}, 
            { 16, 4, 26, 5,  29}, 
            { 1, 10, 12, 16, 37}});
            Console.WriteLine("1) Gauss-Jordan method\n");
            Console.WriteLine("Input matrix\n");
            Console.WriteLine(mat);
            Console.WriteLine("Solved matrix\n");
            Console.WriteLine(mat = Matrix.SolveGaussJordan(mat));
            Console.WriteLine("Answer Gauss: " + mat["Column " + mat.Columns]);

            //mat["ROW 2"] -= mat["ROW 1"];
            //mat["ROW 1"] -= mat["ROW 4"];
            //mat["ROW 2"] += mat["ROW 1"];
            //mat["ROW 4"] -= mat["ROW 2"];
            //mat["ROW 1"] -= mat["ROW 3"] / 4;
            //mat["ROW 1"] += mat["ROW 2"] * (7f / 20);

            //После преобразований
            //5     0     3.6   -0.35 4.65  
            //0     20    6     -6    34    
            //16    4     26    5     29    
            //1     -10   6     22    3     

            string[] data = {
            "5 0 3.6 -0.35",
            "0 20 6 -6",
            "16 4 26 5",
            "1 -10 6 22"};

            mat = new Matrix(data);
            Vector x = new Vector(4.65, 34, 29, 3);

            Console.WriteLine("\n2) Iteration method\n");
            Console.WriteLine("Input matrix\n");
            Console.WriteLine(mat);
            Console.WriteLine("Input vector\n");
            Console.WriteLine(x);
            Console.WriteLine("Answer Iterations: " + Matrix.SolveSimpleIteration(mat, x, 10E-8));

            //System.Windows.Forms.Clipboard.SetText(sw.ToString());
            //Console.SetOut(standartOut);
            //Console.Write(sw.ToString());
            //sw.Close();
        }


    }
}
