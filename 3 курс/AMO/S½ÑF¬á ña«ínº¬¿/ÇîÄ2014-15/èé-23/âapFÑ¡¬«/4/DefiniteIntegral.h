#ifndef DEFINITEINTEGRAL_H
#define DEFINITEINTEGRAL_H
class DefiniteIntegral {
public: DefiniteIntegral();
		~DefiniteIntegral();
		int SetBorder(long double _a, long double _b);
		long double SetEps(long double _eps);
		long double GetExactValueIntegral();
		long double GetAbsError();
		long double GetIntegrationStep();
		long double IntegrationSimpsonRule();
		long double IntegrationRefinedCalculation();
private:
	long double M4;
	long double eps;
	int n;
	long double h;
	long double result;
	long double a, b;
	bool ifcomput;
	long double integrand(long double x);
	long double ruleSimpson(int _n);
	long double antiderivative(long double border);
	long double derivative4(long double x);
};
#endif // DEFINITEINTEGRAL_H