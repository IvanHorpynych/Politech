/**
 * Created by Eugenia on 08.11.2014.
 */
public class GaussSeidel {
    public GaussSeidel(){}

    public double[] solve(Solution s){
        int n = s.size();
        double[] x = new double[n];
        double[][] A = new double[n][n];
        double[] B = new double[n];

        for (int i = 0; i < n; i++){
            B[i] = s.getB(i)/s.getA(i,i);
            for (int j = 0; j < n; j++) {
                if (i == j)
                    A[i][j]=0;
                else
                    A[i][j] = -s.getA(i, j) / s.getA(i, i);
            }
            x[i] = B[i];
        }

        double[] prevX = new double[n];
        double q = M(A);

        if (q >= 1){
            System.out.println("Can`t be solved by GaussSeidel");
            return  new double[0];
        }

        while (M(sub(x,prevX)) > (1-q) * 1e-16/q){
            prevX = x.clone();
            for(int i = 0; i < n; i++){
                x[i] = B[i];
                for(int j = 0; j < n; j++){
                    x[i] += A[i][j]*x[j];
                }
            }
        }

        return x;

    }

    private double M (double[][] a){
        int n = (int)Math.sqrt(a.length);
        double max = 0;
        for (int j = 0; j < n; j++)
            max += Math.abs(a[0][j]);
        for (int i = 1; i < n; i++)
        {
            double current = 0;
            for (int j = 0; j < n; j++) current += Math.abs(a[i][j]);
            if (current > max) max = current;
        }

        return max;
    }

    private double M(double[] a)
    {
        int n = a.length;
        double max = Math.abs(a[0]);

        for (int i = 1; i < n; i++)
            if (Math.abs(a[i]) > max) max = Math.abs(a[i]);

        return max;
    }

    private double[] sub(double[] a, double[] b)
    {
        int n;
        if (a.length < b.length) n = a.length;
        else n = b.length;

        double[] result = new double[n];

        for (int i = 0; i < n; i++)
            result[i] = a[i] - b[i];

        return result;
    }

}




