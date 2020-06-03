/**
 * Created by Eugenia on 23.11.2014.
 */


import static java.lang.Math.*;

class Solution {
public static double function(double x){
        return sqrt(2 * x + 1)/2;
}

public  static double initial(double x){
        return ((2*x+1)*(sqrt(2*x+1)))/6;
}

public static double derivative_2(double x){
        return 1/(2*sqrt(2*x+1));
}

public static double integral_newton(double a,double b){
        return initial(b)-initial(a);
}

public static double find_max_derivative_2(double a,double b,double step){
    double max = derivative_2(a);
    double tmp_max;
    for(double i = a + step; i <= b; i += step){
        if((tmp_max = abs(derivative_2(i))) > max)
            max = tmp_max;
    }
    return max;
}

public static double find_step_h(double a,double b,double eps){
    double step=1e-5;
    double max_derivative_2=find_max_derivative_2(a,b,step);
    double h=sqrt(12*eps/((b-a)*max_derivative_2));
    return h;
}

public static double integral_composite_trapezium_rule(double a,double b,double h,double n){
    double result = (function(a)+function(b))/2;
    for(int i = 1; i < n; i++)
        result += function(a+i*h);
    result *= h;
    return result;
}


public static void main(String[] args) {
        final double a = 0;
        final double b = 15;
        int count=5;
        double start_eps=0.01;
        double[] delta = new double[count];
        double eps = start_eps;
        System.out.println("Composite trapezium rule.\n");
        System.out.println("EPS\t\t\t\tStep\t\t\t\t\t    Exact value\t\t\t\t\t\tError");
        for(int i = 0; i < count; i++){
            double h = find_step_h(a,b,eps);
            double n = (b-a)/h;
            h = (b-a)/n;
            double res = integral_composite_trapezium_rule(a,b,h,n);
            delta[i]=abs(integral_newton(a,b)-res);
            System.out.format("%-6g%19.15f%25.15f%23.15f\n", eps, h, res, delta[i]);
            eps*=1e-1;
        }

        System.out.println();
        System.out.println("Error\t\t\t\t\t\tStep\t\t\t\t\t\tNew error");
        for(int i = 0; i < count; i++){
            eps=delta[i];
            //double R;
            double h1=0;
            double h2;
            double n1=1/pow(eps,1/3);
            double n2;
            double error;
            double res,res2n;
            do{
                n2 = 2*n1;
                h1=(b-a)/n1;
                h2=(b-a)/n2;
                n1=(b-a)/h1;
                n2=(b-a)/h2;
                res=integral_composite_trapezium_rule(a,b,h1,n1);
                res2n=integral_composite_trapezium_rule(a,b,h2,n2);
                n1*=2;
            }while(abs(res-res2n)/(double)3 > eps);
            error = abs(integral_newton(a,b)-res2n);
            System.out.format("%17.15f%24.15f%24.15f\n", delta[i], h2, error);
        }

    }
}