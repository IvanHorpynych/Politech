import java.io.*;

/**
 * Created by Eugenia on 05.12.2014.
 */
public class CubicSplineInterpolation {
    private double a, b;
    public static int N;

    SplineTuple[] splines; // Сплайн

    // сплайн на каждом промежутке
    private class SplineTuple {
        public double a, b, c, d, x;
    }

    public CubicSplineInterpolation() {
        a = 1.0;
        b = 10.0;
    }

    private double Function(double x) {
        return 7.5 * Math.log10(x) * Math.sin(x);
    }

    public void BuildSpline() {
        double h = (b-a) / N;
        double x = a;
        splines = new SplineTuple[N];
        for (int i = 0; i < N; i++)
            splines[i] = new SplineTuple();
        splines[0].x = a;
        splines[0].a = Function(a);
        for (int i = 1; i < N; ++i) {
            x += h;
            splines[i].x = x;
            splines[i].a = Function(x);
        }
        splines[0].c = splines[N - 1].c = 0.0;
        x = a;
        double[] alpha = new double[N - 1];
        double[] beta = new double[N - 1];
        alpha[0] = beta[0] = 0.0;
        for (int i = 1; i < N - 1; ++i) {
            // double hi = x[i] - x[i - 1];
            // double hi1 = x[i + 1] - x[i];
            double A = h;
            double C = 4.0 * h;//(hi + hi+1);
            double B = h;//hi1;
            double F = 6.0 * ((splines[i + 1].a - splines[i].a) / h - (splines[i].a - splines[i - 1].a) / h);
            double z = (A * alpha[i - 1] + C);
            alpha[i] = -B / z;
            beta[i] = (F - A * beta[i - 1]) / z;
        }

        // Нахождение решения - обратный ход метода прогонки
        for (int i = N - 2; i > 0; --i) {
            splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
        }

        // По известным коэффициентам c[i] находим значения b[i] и d[i]
        for (int i = N - 1; i > 0; --i) {
            //double hi = x[i] - x[i - 1];
            splines[i].d = (splines[i].c - splines[i - 1].c) / h;
            splines[i].b = h * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (splines[i].a - splines[i - 1].a) / h;
        }


    }

    private double Interpolation(double x) {

            if (splines == null) {
                return 0.0;
            }

            int n = splines.length;
            SplineTuple s;

            if (x <= splines[0].x) {
                s = splines[0];
            } else if (x >= splines[n - 1].x) {
                s = splines[n - 1];
            } else
            {
                int i = 0;
                int j = n - 1;
                while (i + 1 < j) {
                    int k = i + (j - i) / 2;
                    if (x <= splines[k].x) {
                        j = k;
                    } else {
                        i = k;
                    }
                }
                s = splines[j];
            }

            double dx = x - s.x;
            return s.a + s.b * dx + s.c / 2 * dx * dx + s.d / 6 * dx * dx * dx;


    }


    public void GetCsv() throws Exception{
        double Y = 0.0, x = a;
        File file = new File("output.csv");
        FileWriter wrt = new FileWriter(file);
        while (x <= b) {
            Y = Interpolation(x);
            wrt.append(x + ";" +Y+"\n");
            x += 0.1;
        }
        wrt.flush();
    }

}

