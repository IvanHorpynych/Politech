using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zorin_Lab_3 {
    public class Vector {
        public int Length { get; private set; }
        private double[] data;
        public Vector(int length) {
            this.Length = length;
            this.data = new double[Length];
        }

        public Vector(params double[] data) {
            this.data = data;
            this.Length = data.Length;
        }

        public double this[int i] {
            get { return data[i - 1]; }
            set { data[i - 1] = value; }
        }

        private static void CheckArguments(Vector v1, Vector v2) {
            if (v1 == null || v2 == null) {
                throw new ArgumentNullException();
            }

            if (v1.Length != v2.Length) {
                throw new ArgumentException("The length of arguments must be the same");
            }
        }

        public static Vector operator +(Vector v1, Vector v2) {
            CheckArguments(v1, v2);

            double[] newData = new double[v1.Length];
            v1.data.CopyTo(newData, 0);

            for (int i = 0; i < newData.Length; i++) {
                newData[i] += v2.data[i];
            }

            return new Vector(newData);
        }
        public static Vector operator -(Vector v1, Vector v2) {
            CheckArguments(v1, v2);

            double[] newData = new double[v1.Length];
            v1.data.CopyTo(newData, 0);

            for (int i = 0; i < newData.Length; i++) {
                newData[i] -= v2.data[i];
            }

            return new Vector(newData);
        }
        public static Vector operator +(Vector vect, double val) {

            double[] newData = new double[vect.Length];
            vect.data.CopyTo(newData, 0);

            for (int i = 0; i < newData.Length; i++) {
                newData[i] += val;
            }

            return new Vector(newData);
        }
        public static Vector operator -(Vector vect, double val) {

            double[] newData = new double[vect.Length];
            vect.data.CopyTo(newData, 0);

            for (int i = 0; i < newData.Length; i++) {
                newData[i] -= val;
            }

            return new Vector(newData);
        }
        public static Vector operator *(Vector vect, double val) {

            double[] newData = new double[vect.Length];
            vect.data.CopyTo(newData, 0);

            for (int i = 0; i < newData.Length; i++) {
                newData[i] *= val;
            }

            return new Vector(newData);
        }
        public static Vector operator /(Vector vect, double val) {

            double[] newData = new double[vect.Length];
            vect.data.CopyTo(newData, 0);

            for (int i = 0; i < newData.Length; i++) {
                newData[i] /= val;
            }

            return new Vector(newData);
        }

        public override string ToString() {
            StringBuilder outStr = new StringBuilder(data.Length);

            foreach (var item in data) {
                outStr.AppendFormat("{0:0.######} ", item);
            }
            outStr.AppendLine();

            return outStr.ToString();
        }

        public double[] ToArray() {
            return data;
        }
        public double MaxNorma() {
            double max = 0;
            foreach (var item in data) {
                if (Math.Abs(item) > max) {
                    max = Math.Abs(item);
                }
            }
            return max;
        }
    }
}
