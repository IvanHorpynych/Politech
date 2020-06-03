using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zorin_Lab_3 {

    public class Matrix {

        private Vector[] data;
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public bool ShowLabels { get; set; }

        public Matrix(int rows, int columns) {
            this.Columns = columns;
            this.Rows = rows;

            this.data = new Vector[Rows];
            for (int i = 0; i < Rows; i++) {
                data[i] = new Vector(Columns);
            }
        }

        public Matrix(double[,] data) {
            this.Rows = data.GetLength(0);
            this.Columns = data.GetLength(1);
            this.data = new Vector[Rows];

            for (int i = 0; i < Rows; i++) {
                double[] arr = new double[Columns];
                for (int j = 0; j < Columns; j++) {
                    arr[j] = data[i, j];
                }
                this.data[i] = new Vector(arr);
            }
        }

        public Matrix(params string[] data) {
            this.Rows = data.Length;
            this.Columns = data[0].Split(' ').Length;
            this.data = new Vector[Rows];

            for (int i = 0; i < Rows; i++) {
                double[] arr = new double[Columns];
                for (int j = 0; j < Columns; j++) {
                    arr[j] = double.Parse(data[i].Split(' ')[j]);
                }
                this.data[i] = new Vector(arr);
            }
        }

        public static Matrix HilbertMatrix(int rank) {
            double[,] data = new double[rank, rank + 1];
            for (int i = 0; i < rank; i++) {
                double b = 0;
                for (int j = 0; j < rank; j++) {
                    data[i, j] = 1 / (i + j + 1.0f);
                    b += data[i, j];
                }
                data[i, rank] = b;
            }

            return new Matrix(data);
        }

        private Vector getRow(int i) {
            return data[i];
        }
        private void setRow(int i, Vector value) {
            data[i] = value;
        }

        private Vector getColumn(int i) {
            double[] column = new double[Rows];
            for (int j = 0; j < Rows; j++) {
                column[j] = data[j][i + 1];
            }
            return new Vector(column);
        }
        private void setColumn(int i, Vector value) {
            for (int j = 0; j < Rows; j++) {
                data[j][i + 1] = value[j + 1];
            }
        }

        private static void CheckIndexString(string input) {
            System.Text.RegularExpressions.Regex check =
                new System.Text.RegularExpressions.Regex("(Row|Column) [1-9][0-9]*", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!check.IsMatch(input)) {
                throw new ArgumentException("Ivalid index. Index must be like \"Row #\" or"
                    + " \"Сolumn #\" where # is row/column num, indexes start from 1.");
            }
        }

        public Vector this[string input] {

            get {
                CheckIndexString(input);

                string type = input.Split(' ')[0].ToLower();
                int num = int.Parse(input.Split(' ')[1]) - 1;

                return type == "row" ? getRow(num) : getColumn(num);
            }
            set {
                CheckIndexString(input);

                string type = input.Split(' ')[0].ToLower();
                int num = int.Parse(input.Split(' ')[1]) - 1;

                if (value == null) {
                    throw new ArgumentNullException();
                }

                if (value.Length != (type == "row" ? Columns : Rows)) {
                    throw new ArgumentException("The length of arguments must be the same");
                }

                if (type == "row") {
                    setRow(num, value);
                } else {
                    setColumn(num, value);
                }
            }
        }
        public static Matrix SolveGaussJordan(Matrix mat) {

            if (mat.Columns - mat.Rows != 1) {
                throw new ArgumentException("Invalid matrix");
            }

            for (int k = 1; k <= mat.Rows; k++) {
                mat["Row " + k] /= mat["Row " + k][k];
                for (int i = k + 1; i <= mat.Rows; i++) {
                    mat["Row " + i] -= mat["Row " + k] * mat["Row " + i][k];
                }
            }

            for (int k = mat.Rows; k > 0; k--) {
                for (int i = k - 1; i > 0; i--) {
                    mat["Row " + i] -= mat["Row " + k] * mat["Row " + i][k];
                }
            }

            return mat;
        }

        public static Vector SolveSimpleIteration(Matrix mat, Vector x, double eps) {
            Vector b = new Vector(mat.Rows);

            for (int i = 1; i <= b.Length; i++) {
                b[i] = x[i] / mat["Row " + i][i];
            }

            for (int i = 1; i <= mat.Rows; i++) {
                for (int j = 1; j <= mat.Columns; j++) {
                    if (i != j) {
                        mat["Row " + i][j] /= -mat["Row " + i][i];
                    }
                }
                mat["Row " + i][i] /= -mat["Row " + i][i];
                mat["Row " + i][i] = 0;
            }
            x = b;
            Vector prev_x;
            do {
                prev_x = x;
                x = b + mat * x;
            } while ((x - prev_x).MaxNorma() > ((1 - mat.MaxNorma()) / mat.MaxNorma()) * eps);

            return x;
        }

        public static Vector operator *(Matrix mat, Vector vect) {
            if (mat.Columns != vect.Length) {
                throw new ArgumentException("vect.Length != mat.Rows");
            }

            Vector result = new Vector(mat.Rows);

            for (int i = 1; i <= mat.Rows; i++) {
                for (int j = 1; j <= mat.Columns; j++) {
                    result[i] += vect[j] * mat["Row " + i][j];
                }
            }
            return result;
        }

        public double MaxNorma() {
            double max = 0;
            foreach (var item in data) {
                if (item.MaxNorma() > max) {
                    max = item.MaxNorma();
                }
            }
            return max;
        }

        public override string ToString() {
            StringBuilder outStr = new StringBuilder(Rows * Columns + Rows);

            string formatString = ShowLabels ?
                "[{0} {1}] {2,-6:0.##}" : "{2,-6:0.##}";

            for (int i = 0; i < Rows; i++) {
                for (int j = 1; j <= Columns; j++) {
                    outStr.AppendFormat(formatString, i + 1, j, data[i][j]);
                }
                outStr.AppendLine();
            }

            return outStr.ToString();
        }
    }
}
