
public class Main {
	
	static double func(double x)
	{
		return 0.3 * Math.exp(x) - x*x + 1;
	}
	
	static double derivative(double x)
	{
		return Math.abs(0.3 * Math.exp(x) - 2*x);
	}
	
	static double derivative1(double x)
	{
		return 0.3 * Math.exp(x) - 2*x;
	}
	
	double derivative2(double x)
	{
		return 0.3 * Math.exp(x) - 2;
	}
	
	static double fi_func(double x, double lamda)
	{
		return x - lamda*func(x);
	}
	
	static int check_iteration(double x_current, double x_next, double eps, double q)
	{
		if (Math.abs(x_next - x_current) <= (1 - q) / q*eps)
			return 1;
		return 0;
	}
	
	static int check_newton(double x_current, double eps, double m1)
	{
		if (Math.abs(func(x_current)) / m1 <= eps)
			return 1;
		return 0;
	}
	
	public static void main (String[] args){
		double[] m1 = new double[3];
		double[] M1 = new double[3];
		double[] q = new double[3];
		double[] lamda = new double[3];
		double[] x_beg  = new double[3];
		double[] a = new double[] { -1.0, 1.0, 3.0};
		double[] b = new double[] { 0.0, 2.0, 4.0 };
		double x_current, x_next, eps;
		int[][] numb = new int[2][4];

		for (int i = 0; i < 3; i++)
		{
			double tmp = 0;
			M1[i] = derivative(a[i]);
			tmp = derivative(b[i]);
			if (tmp > M1[i])
			{
				m1[i] = M1[i];
				M1[i] = tmp;
			}
			else
				m1[i] = tmp;
			q[i] = 1 - m1[i] / M1[i];
		}

		lamda[0] = 1 / M1[0];
		lamda[1] = -1 / M1[1];
		lamda[2] = 1 / M1[2];

		System.out.println("Method of Successive Approximations");
		System.out.println(" eps\t\t Equation root\t\t\t precision");
		eps = 1e-2;
		x_current = 0;
		for (int i = 1; i <= 4; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				int n = 0;
				double error = 0;
				x_next = (a[j] + b[j]) / 2;
				do
				{
					x_current = x_next;
					x_next = fi_func(x_current, lamda[j]);
					n++;
				} while (check_iteration(x_current, x_next, eps, q[j]) == 0);
				error = Math.abs(x_next - x_current)*q[j] / (1 - q[j]);
				if (j == 0)
				{
					numb[0][i - 1] = n;
					if (i == 4)
						System.out.println( "1.1E-11\t\t" + x_current+ "\t" + error);
					else
					System.out.println( eps +"\t\t" + x_current+ "\t" + error);
				}
				else
					System.out.println("\t\t" + x_current+ "\t" + error);
			}
			System.out.println();
			eps = eps*1e-3;
		}

		System.out.println("Newton-Raphson method");
		System.out.println(" eps\t\t Equation root\t\t\t precision");

		x_beg[0] = a[0];
		x_beg[1] = b[1];
		x_beg[2] = b[2];

		eps = 1e-2;
		for (int i = 1; i <= 4; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				int n = 0;
				double error = 0;
				x_next = x_beg[j];
				do
				{
					x_current = x_next;
					x_next = x_current - func(x_current) / derivative1(x_current);
					n++;
				} while (check_newton(x_current, eps, m1[j]) == 0);

				error = Math.abs(func(x_current)) / m1[j];
				if (j == 0)
				{
					numb[1][i - 1] = n;
					if (i == 4)
						System.out.println( "1.1E-11\t\t" + x_current+ "\t" + error);
					else
					System.out.println( eps +"\t\t" + x_current+ "\t" + error);
				}
				else
					System.out.println("\t\t" + x_current +"\t"+ error);
			}
			System.out.println();
			eps = eps*1e-3;
		}

		System.out.println(" eps\t\tMethod of Approximations\tNewton-Raphson method");
		eps = 1e-2;
		for (int i = 0; i < 4; i++)
		{
			if (i == 3)
					System.out.println( "1.1E-11\t\t\t"+ numb[0][i] +"\t\t\t"+ numb[1][i]);
				else
			System.out.println( eps +"\t\t\t"+ numb[0][i] +"\t\t\t"+ numb[1][i]);
			eps = eps*1e-3;
		}

	}

}
