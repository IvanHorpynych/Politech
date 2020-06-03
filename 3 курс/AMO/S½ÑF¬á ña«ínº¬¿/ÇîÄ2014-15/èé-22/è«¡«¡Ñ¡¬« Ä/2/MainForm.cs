using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Forms
{


    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        AlgorithmsCompareData resData = new AlgorithmsCompareData();
        private void MainForm_Load(object sender, EventArgs e)
        {
            lTextBox.Text = MathRoutine.l1.ToString();
            rTextBox.Text = MathRoutine.r1.ToString();
            minTextBox.Text = MathRoutine.firstIntervalMin.ToString();
            maxTextBox.Text= MathRoutine.firstIntervalMax.ToString();
            
           
            //AnalyzeAlgs();
        }

        //0.1*x*x - sin(x - pi/4) - 1.5 = 0;
        public void AnalyzeAlgs(double l, double r, double min, double max)
        {

            Iteration iterSolver = new Iteration();
            Hordes hordeSolver = new Hordes();
            double epsilon = 10e-2;
             
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    resData.eps = epsilon;
                    resData.iterRoot = iterSolver.Solve(MathRoutine.F, MathRoutine.FirstDerivative, resData.eps, l, r, min, max, 
                        out resData.iterCount, out resData.iterPrecision);

                    resData.hordeRoot = hordeSolver.Solve(MathRoutine.F, MathRoutine.SecondDerivative, min,
                        resData.eps, l, r, out resData.hordeCount, out resData.hordePrecision);

                    dataGridView.Rows.Add(resData.eps, resData.iterRoot, resData.iterPrecision, resData.iterCount,
                        resData.hordeRoot, resData.hordePrecision, resData.hordeCount);
                    epsilon /= 10e3;
                }
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }




        private void gridButtonClick(object sender, EventArgs e)
        {
            try
            {
                double l = double.Parse(lTextBox.Text);
                double r = double.Parse(rTextBox.Text);
                double m = double.Parse(minTextBox.Text);
                double M = double.Parse(maxTextBox.Text);
                if (l > r) throw new ArgumentException();
                dataGridView.Rows.Clear();
                AnalyzeAlgs(l, r, m, M);
            }
            catch (FormatException)
            {
                MessageBox.Show("Couldn't parse entered values as double numbers");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid borders");
            }
        }

        private void fxButtonClick(object sender, EventArgs e)
        {
           
        }
    }
}
