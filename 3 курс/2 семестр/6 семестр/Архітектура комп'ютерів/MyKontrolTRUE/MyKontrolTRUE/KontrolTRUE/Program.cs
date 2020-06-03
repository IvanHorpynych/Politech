using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace KontrolTRUE
{
    class MyThread
    {
        public Thread Thrd;

        public MyThread(string name)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
        }

        void Run(object variant)
        {
            while (true)
            {
                var columnIndex = WaitHandle.WaitAny(Program.evtObj, 0);
                if (WaitHandle.WaitTimeout == columnIndex)
                {
                    Console.WriteLine(Thrd.Name + " completed!");
                    return;
                }
                Console.WriteLine("Inside thread " + Thrd.Name);                
                switch ((int)variant)
                {
                    case 1:
                        {
                            //сума по стовпцях
                            Program.result[columnIndex] = 0;
                            for (int i = 0; i < Program.matrixDimention; i++)                            
                                Program.result[columnIndex] += Program.matr[i, columnIndex];                            
                            break;
                        }
                    case 2:
                        {
                            //добуток по рядках
                            Program.result[columnIndex] = 1;
                            for (int i = 0; i < Program.matrixDimention; i++)                            
                                Program.result[columnIndex] *= Program.matr[columnIndex, i];                            
                            break;
                        }
                    case 3:
                        {
                            //пошук мінімаьлного по рядках
                            int min = int.MaxValue;                            
                            for (int i = 0; i < Program.matrixDimention; i++)
                                if (Program.matr[columnIndex, i] < min)
                                    min = Program.matr[columnIndex, i];
                            Program.result[columnIndex] = min;
                            break;
                        }
                    case 4:
                        {
                            //пошук масимального по стовпцях
                            int max = int.MaxValue;
                            for (int i = 0; i < Program.matrixDimention; i++)
                                if (Program.matr[columnIndex, i] > max)
                                    max = Program.matr[i, columnIndex];
                            Program.result[columnIndex] = max;
                            break;
                        }
                }
                Console.WriteLine(String.Format("cullumnIndex - {0}, result - {1}", columnIndex, Program.result[columnIndex]));
                Console.WriteLine(Thrd.Name + " completed!\n");
            }
        }
    }

    public class Program
    {
        public static int matrixDimention = 5;    //Number of rows and cols in matrix
        public static int threadAmount = 3;    //Number of threads
        public static int[,] matr = new int[matrixDimention, matrixDimention];
        public static int[] result = new int[matrixDimention];
        public static AutoResetEvent[] evtObj = new AutoResetEvent[matrixDimention];

        static void Main(string[] args)
        {
            int FinalResult = 0;
            Random dig = new Random();            
            String matrixStr = "Matrix:\n";
            for (int i = 0; i < matrixDimention; i++)
            {
                for (int j = 0; j < matrixDimention; j++)
                {
                    matr[i, j] = dig.Next(10);        //Add element to the matrix; 
                    matrixStr += String.Format(" {0}", matr[i, j]);
                }
                matrixStr += "\n";
                evtObj[i] = new AutoResetEvent(true);
            }
            Console.WriteLine(matrixStr);
            MyThread[] threads = new MyThread[threadAmount];
            for (int i = 0; i < threadAmount; i++ )            
                threads[i] = new MyThread("Thread " + i);            

            Console.WriteLine("Enter number of variant");
            string s = Console.ReadLine();
            int variant = int.Parse(s);

            for (int i = 0; i < threadAmount; i++)            
                threads[i].Thrd.Start(variant);            

            for (int i = 0; i < threadAmount; i++)
                threads[i].Thrd.Join();

            switch (variant)
            {
                case 1:
                    {
                        //добуток сум по стовпцях
                        FinalResult = 1;
                        for (int i = 0; i < result.Length; i++)
                        {
                            FinalResult *= result[i];                            
                        }
                        Console.WriteLine("Final result =" + FinalResult);
                        break;
                    }
                case 2:
                    {
                        //сума добутків по рядках
                        FinalResult = 0;
                        for (int i = 0; i < result.Length; i++)                        
                            FinalResult += result[i];
                        
                        Console.WriteLine("Final result =" + FinalResult);                        
                        break;
                    }
                case 3:
                    {
                        //максимальне серед мінімальних по рядках
                        int max = int.MinValue;
                        for (int i = 0; i < result.Length; i++)
                            if (result[i] > max)
                                max = result[i];
                        Console.WriteLine("Final result =" + max);
                        break;
                    }
                case 4:
                    {
                        //мінімальне серед максимальних по стовпцях
                        int min = int.MaxValue;
                        for (int i = 0; i < result.Length; i++)                        
                            if (result[i] < min)                           
                                min = result[i];                        
                        Console.WriteLine("Final result =" + min);
                        break;
                    }

            }
        }
    }
}
