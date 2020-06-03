/**
 * Created by Eugenia on 30.11.2014.
 */
public class Main {
    public static void main(String[] args) {
        LeastSquareApproximation.n = 5;

        while(LeastSquareApproximation.n < 50)
        {
            System.out.println("\n\n" + "N = "+ LeastSquareApproximation.n);
            LeastSquareApproximation lsa = new LeastSquareApproximation();
            lsa.GetNormalEquations();
            lsa.GaussianElimination();
            // obj.PrintMonomials();
            lsa.ErrorFix();
            lsa.ShowXY();
            lsa.n += 10;
        }
    }
}
