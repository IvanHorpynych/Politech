/**
 * Created by Eugenia on 09.11.2014.
 */
public class Main {
    public static void main(String[] args) {
        System.out.println("Gauss and Seidel Method");
        System.out.println("Iterationable System");
        Solution s = new Solution(4, new double[][]{
                {22, -5, 6, 9},
                {13, 49, 20, 15},
                {20, 19, 50, 10},
                {-39, 12, 0, -58}
        }, new double[] {101, 353, 437, -274});

        GaussSeidel solver = new GaussSeidel();
        double[] solution = solver.solve(s);
        System.out.println(s.toString());

        for (int i = 0; i< solution.length; i++)
            System.out.println("x[" + Integer.toString(i+1) + "]= " + Double.toString(solution[i]));


        Solution s2 = new Solution(4, new double[][]{
                {3, 7, 6, 0},
                {13, 49, 20, 15},
                {20, 19, 50, 10},
                {17, 7, 17, 17}
        }, new double[]{63, 353, 437, 225});

        System.out.println(s2.toString());
        System.out.println("Gauss-Jordan Method");
        GaussJordan solver2 = new GaussJordan();
        Double[] solution2 = solver2.solve(s2);
        for (int i = 0; i < solution.length; i++)
            System.out.println("x[" + Integer.toString(i+1) + "]= " + Double.toString(solution2[i]));



    }
}
