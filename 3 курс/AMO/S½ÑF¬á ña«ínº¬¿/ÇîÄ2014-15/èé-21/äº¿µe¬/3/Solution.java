/**
 * Created by Eugenia on 08.11.2014.
 */
public class Solution {
    private double[][] A;
    private double[] B;
    private int n;

    public Solution(int _n, double[][] _a, double[] _b){

            n = _n;
            A = _a;
            B = _b;

    }

    public double getA(int i, int j){return A[i][j];}
    public double getB(int i){return B[i];}
    public int size(){return n;}
    public void setA(int i, int j, double value){A[i][j] = value;}
    public void setB(int i, double value){B[i] = value;}

    @Override
    public String toString(){
        String result = "A = \n";
        for (int i = 0; i < n; i++){
            for (int j = 0; j < n; j++){
                if (getA(i,j) < 10.0 && getA(i,j) >= 0)
                    result += Double.toString(getA(i,j)) + "\t\t";
                else
                    result += Double.toString(getA(i,j)) + "\t";
            }
            result += "\n";
        }
        result += "\nB = ";
        for(int i = 0; i < n; i++)
            result += Double.toString(getB(i)) + "\t";
        result += "\n";

        return result;
    }
}
