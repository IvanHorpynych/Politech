#include <cstdlib>												
#include <iostream>												
#include <math.h>

using namespace std;

class Calculation
{


private:

	int mstep = 1000;

	int step = 0;

	double x = 0.0;

	double func(double x)
	{
		return sin(3 * x) + 2 * x * x - 1;
	}


	int direvative(double x)
	{
		if (x == 0)
			return 1e-14;

		double res = 3 * cos(3 * x) + 4 * x;

			if (res > 0)
				return 1;

			if (res < 0)
				return -1;
		
	}

	int direvative_2(double x)
	{
		if (x == 0)
			return 1e-14;

		double res = -9 * sin(3 * x) + 4;

		if (res > 0)
			return 1;

		if (res < 0)
			return -1;

	}

	double Iteration(double x0, double lambda, double eps, double q){
		x = x0;
		step = 1;


		double err = 0.0;

		double x1 = x - lambda * func(x) * direvative(x);

		while ((abs(x1 - x) > eps) && (step <= mstep)){
			++step;
			x = x1;
			x1 = x - lambda * func(x) * direvative(x);
		}

		err = (q / (1 - q) * (x1 - x));
		x = x1;


		return err;
	}

	double Chord(double a, double b, double eps){
		double err = 0;
		double x_i = 0;
		double x1 = 0;
		double x0 = 0;

		if (a > 0){
			x1 = a;
			x0 = b;
		}
		else{
			x1 = b;
			x0 = a;
		}

		step = 1;

		x_i = x1 - (func(x1) * (x1 - x0)) / (func(x1) - func(x0));
		err = eps + 1;
		x = x_i;

		while ((step < mstep) && (err >= eps)){
			x = x_i - (func(x_i) * (x_i - x0)) / (func(x_i) - func(x0));
			err = abs(x - x_i);
			x_i = x;
			step++;
		}

		return err;
	}

public:
	void RunIteration(double a, double b, double m, double M){
		double eps = 1E-2;
		double lambda = 1 / M;
		double q = 1 - m / M;
		step = 0;

		double err = 0.0;

		printf("\nIteration method:\n");
		printf("EPS		ROOT		ACCURANCY	STEPS	");
		printf("\n");


		for (int i = 0; i < 5; i++){
			x = 0.0;
			// Execution of the iteration method implementation
			err = Iteration(a, lambda, eps, q);
			// Output
			printf("%.0e %5s", eps, "	");
			printf("%2f %2s ", x, "	");
			printf("%f %4s", err, "	");
			printf("%.4d %3s ", step, "	");
			printf("\n");


			eps *= 1E-3;
		}

	}




	void RunChord(double a, double b){
		double eps = 1E-2, err = 0.0;
		step = 0;

		printf("\nChord method:\n");
		printf("EPS		ROOT		ACCURANCY	STEPS	");
		printf("\n");



		for (int i = 0; i < 5; i++){
			x = 0.0;
			// Execution of the chord method
			err = Chord(a, b, eps);
			// Output
			printf("%.0e %5s", eps, "	");
			printf("%2f %2s ", x, "	");
			printf("%f %4s", err, "	");
			printf("%.4d %3s ", step, "");
			printf("\n");

			eps *= 1E-3;
		}

	}

};