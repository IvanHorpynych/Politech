/**
 * Created by Eugenia on 05.12.2014.
 */
public class Main {
   public static void main(String[] args) throws Exception {
        CubicSplineInterpolation obj = new CubicSplineInterpolation();
        CubicSplineInterpolation.N = 100;
        obj.BuildSpline();
        obj.GetCsv();
       System.out.println("look for e:\\out.csv");
    }
    }

