import java.util.ArrayList;
import java.util.List;
import java.util.Arrays;

/**
 * Created by Eugenia on 09.11.2014.
 */
public class GaussJordan {
    public Double[] solve(Solution s) {
        int n = s.size();
        List<Double> result = new ArrayList<Double>();

        if (n == 1) result.add(s.getB(0) / s.getA(0, 0));
        else {
            for (int j = 1; j < n; j++)
                s.setA(0, j, s.getA(0, j) / s.getA(0, 0));

            s.setB(0, s.getB(0) / s.getA(0, 0));
            s.setA(0, 0, 1.0);

            double[][] A = new double[n - 1][n - 1];
            double[] B = new double[n - 1];

            for (int i = 0; i < n - 1; i++) {
                for (int j = 0; j < n-1; j++) {
                    A[i][j] = s.getA(i + 1, j + 1) - s.getA(0, j + 1) * s.getA(i + 1, 0);
                }
                B[i] = s.getB(i + 1) - s.getA(i + 1, 0) * s.getB(0);
            }

            Solution next = new Solution(n - 1, A, B);

            Double[] tmp_result = solve(next);
            result.add(s.getB(0));
            for (int j = 0; j < n - 1; j++)
                result.set(0, result.get(0) - (s.getA(0, j + 1) * tmp_result[j]));
            result.addAll(Arrays.asList(tmp_result));
        }

        return   result.toArray(new Double[result.size()]);
    }

}
