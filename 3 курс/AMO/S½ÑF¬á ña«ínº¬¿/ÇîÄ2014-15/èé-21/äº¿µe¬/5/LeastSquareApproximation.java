/**
 * Created by Eugenia on 30.11.2014.
 */
public class LeastSquareApproximation {
    public static int n; // Matrix size;
    private double[][] Matrix;
    private double[] FreeMembers;
    private double[] Polynomials; // set of Chebyshev Polynomials
    private double a, b;  // Limits of integration
    private double eps; // Persentage error

    public LeastSquareApproximation()
    {
        Matrix = new double[n][n];
        FreeMembers = new double[n];
        Polynomials = new double[n];
        eps = 1E-5;
        a = 1;
        b = 10;
    }


    private double Function(double x) // Basic function
    {
        return 7.5*Math.log10(x)  * Math.sin(x);
    }

    private double ErrorFixFunction(double x)
    {
        ChebyshevPolynomials(x);
        double Sum = FreeMembers[0] * Polynomials[0];
        for (int i = 1; i < n; i++)
        {
            Sum += FreeMembers[i] * Polynomials[i];
        }
        return Sum;
    }

    private void ChebyshevPolynomials(double x)
    {
        Polynomials[0] = 1;
        Polynomials[1] = x;  // the first two polynomials are equal 1 and x

        for (int i = 2; i < n; i++)
        {
            Polynomials[i] = 2 * x * Polynomials[i - 1] - Polynomials[i - 2];
        }
    }


    private double FixErrorIntergrand(double x) {
        return Math.pow((Function(x) - ErrorFixFunction(x)), 2);
    }


    private double IntegrandFunction(double x, int i, int j)  //Integrand
    {
        ChebyshevPolynomials(x);

        if (j > n)
            return Polynomials[i] * Function(x);

        return Polynomials[i] * Polynomials[j];
    }

    private double SimpsonMethod(double n, double h, double x, int i, int j, int flag)
    {

        double Sigma1 = 0.0, Sigma2 = 0.0;

        if (flag == 0)
        {
            for (int k = 1; k < n; k++)
            {
                x += h;

                if (k % 2 != 0)
                    Sigma1 += IntegrandFunction(x, i, j);
                if (k % 2 == 0)
                    Sigma2 += IntegrandFunction(x, i, j);
            }
            return ((h / 3) * (IntegrandFunction(a, i, j) + IntegrandFunction(b, i, j) + 4 * Sigma1 + 2 * Sigma2));
        }
        else
        {
            for (int k = 1; k < n; k++)
            {
                x += h;

                if (k % 2 != 0)
                    Sigma1 += FixErrorIntergrand(x);
                if (k % 2 == 0)
                    Sigma2 += FixErrorIntergrand(x);
            }
            return (h / 3) * (FixErrorIntergrand(a) + FixErrorIntergrand(b) + 4 * Sigma1 + 2 * Sigma2);
        }
    }

    private double RunSimpsonMethod(int i, int j)
    {
        int n = (int) Math.pow(eps, - 0.25);
        if (n % 2 != 0)
            n++;
        double h = (b - a) / n;
        int flag = 0;

        double In = SimpsonMethod(n, h, a, i, j, flag);
        h = (b - a) / (2 * n);
        double I2n = SimpsonMethod(2 * n, h, a, i, j, flag);

        while (eps < Math.abs((In - I2n) / I2n))
        {
            n *= 2;
            h = (b - a) / n;
            In = SimpsonMethod(n, h, a, i, j,flag);
            h = (b - a) / (2 * n);
            I2n = SimpsonMethod(2 * n, h, a, i, j, flag);
        }

        return I2n;
    }

    public void GetNormalEquations()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Matrix[i][ j] = RunSimpsonMethod(i, j);
            }
        }

        for (int i = 0; i < n; i++)
        {
            FreeMembers[i] = RunSimpsonMethod(i, n + 1);
        }

        PrintNormalEquations(Matrix);

    }

    public void PrintNormalEquations(double[][] Matrix)
    {

        System.out.println("\nNormal equations:\n");

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; j++)
                {
                    System.out.println(Matrix[i][ j]);

                }
                System.out.println("Free Member = " + FreeMembers[i]+"\t");

            }
    }


//    public void PrintMonomials()
//    {
//        System.out.println("\n\nMonomials:");
//        for (int i = 0; i < n; i++)
//            System.out.println("\n\n"+"a" + i + "=  " + FreeMembers[i]);
//    }

    private void CopyMatrix(double[][] Matrix, double[][] MatrixCopy) // aditional method for GaussianElimination
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                MatrixCopy[i][j] = Matrix[i][j];
            }
        }
    }

    public void GaussianElimination()
    {
        double[][] MatrixCopy = new double[n][n];
        CopyMatrix(Matrix, MatrixCopy);

//            PrintNormalEquations(MatrixCopy);

        for (int i = 0; i < n - 1; i++)    // Direct elimination
        {
            double temp = MatrixCopy[i][i];
            FreeMembers[i] /= temp;

            for (int j = i; j < n; j++)
            {
                MatrixCopy[i][j] /= temp;
            }


            for (int k = i; k < n; k++)
            {
                if (k == i)
                    continue;
                double temp1 = MatrixCopy[k][i];
                FreeMembers[k] -= FreeMembers[i] * temp1;
                for (int z = 0; z < n; z++)
                {
                    MatrixCopy[k][z] -= MatrixCopy[i][z] * temp1;
                }
            }

            if (i == (n - 2))
            {
                FreeMembers[i+1] /= MatrixCopy[i+1][i+1];
                MatrixCopy[i + 1][i + 1] = 1;
            }

        }


        for (int i = n-2; i >= 0; i--)  // Backward sustitution
        {
            int j = 0;
            for (j = n-1; j > i; j--)
            {
                FreeMembers[i] -= MatrixCopy[i][j] * FreeMembers[j];
            }
        }

    }
    public void ErrorFix()
    {
        int n = (int)Math.pow(eps, -0.25);
        if (n % 2 != 0)
            n++;
        double h = (b - a) / n;
        int flag = 1;

        double In = SimpsonMethod(n, h, a, flag, flag, flag);
        h = (b - a) / (2 * n);
        double I2n = SimpsonMethod(2 * n, h, a, flag, flag, flag);

        while (eps < Math.abs((In - I2n)/I2n))
        {
            n *= 2;
            h = (b - a) / n;
            In = SimpsonMethod(n, h, a, flag, flag, flag);
            h = (b - a) / (2 * n);
            I2n = SimpsonMethod(2 * n, h, a, flag, flag, flag);
        }

        System.out.println("\n\n\n" + "Error= " + Math.sqrt((I2n)/(b - a)));

    }

    public void ShowXY()
    {
        double Y = 0.0, x = a;
        while (x <= b)
            {
                Y = ErrorFixFunction(x);
               System.out.println(Y + ";\t"+ x);

                x += 0.1;
            }
        }

}
