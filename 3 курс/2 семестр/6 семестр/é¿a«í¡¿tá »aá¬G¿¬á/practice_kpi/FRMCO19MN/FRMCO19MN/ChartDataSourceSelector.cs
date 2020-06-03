using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FRMCO19MN
{
    public partial class ChartDataSourceSelector : XtraForm
    {
        private const string sEmptyColumnNumber = "Виберіть стовпчик даних";
        private static bool bEnableCreateForm = true;
        private bool bValidateSuccess = false;

        private string sColumnNumber = string.Empty;
        private string sColumnCaption = string.Empty;
        private string sColumnField = string.Empty;

        public ChartDataSourceSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Свойство - номер колонки
        /// </summary>
        public string ColumnNumber
        {
            get { return lcColumnNumber.Text; }
            set { lcColumnNumber.Text = value; }
        }

        
        /// <summary>
        /// Свойство - заголовок колонки
        /// </summary>
        public string ColumnCaption
        {
            get { return sColumnCaption; }
            set { sColumnCaption = value; }
        }

        /// <summary>
        /// Свойство - поле данных колонки
        /// </summary>
        public string ColumnField
        {
            get { return sColumnField; }
            set { sColumnField = value; }
        }

        /// <summary>
        /// Свойство - признак группировки сумм по остальным предприятиям
        /// </summary>
        public bool GroupSum
        {
            get { return ceGroupSum.Checked; }
        }

        public static bool CanCreateForm()
        {
            return bEnableCreateForm;
        }

        private void sbApply_Click(object sender, EventArgs e)
        {
            if (lcColumnNumber.Text != sEmptyColumnNumber)
            {
                bValidateSuccess = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(sEmptyColumnNumber);           
            }       
        }

        private void ChartDataSourceSelector_Load(object sender, EventArgs e)
        {
            bEnableCreateForm = false;
            lcColumnNumber.Text = sEmptyColumnNumber;
        }

        private void ChartDataSourceSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            bEnableCreateForm = true;
        }

        /// <summary>
        /// Метод устаналивает значения в начальное состояние
        /// </summary>
        public void ResetColumnSelection()
        {
            lcColumnNumber.Text = sEmptyColumnNumber;
            sColumnNumber = string.Empty;
            sColumnCaption = string.Empty;         
        }

        /// <summary>
        /// Метод определяет возможность построения диаграммы
        /// </summary>
        /// <returns></returns>
        public bool AllowCreateDiagram()
        {
            return bValidateSuccess;
        }
    }
}