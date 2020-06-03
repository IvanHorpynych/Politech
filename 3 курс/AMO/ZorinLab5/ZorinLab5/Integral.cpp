#include "main.h"

double integrate(double a, double b, double eps, function<double(double)> f) {
	int i;
	double h;
	double sig1 = 0;
	double sig2 = 0;
	int n = (int)(1.0 / sqrt(eps));
	double y0 = f(a), yn = f(b);
	h = (b - a) / n;
	for (i = 1; i < n; i++) {
		if (i % 2 == 0)
			sig1 += f(a + i*h);
		else
			sig2 += f(a + i*h);
	}
	double cur_int, prev_int = h / 3 * (4 * sig2 + 2 * sig1 + y0 + yn), cur_even = sig2 + sig1;
	int cur_n = 2 * n;
	do {

		h = (b - a) / cur_n;
		double cur_odd = 0, xi;
		for (i = 1, xi = a + h; i < cur_n; i += 2, xi += 2 * h) {
			cur_odd += f(xi);
		}
		cur_int = h / 3 * (4 * cur_odd + 2 * cur_even + y0 + yn);
		prev_int = cur_int;
		cur_even = cur_odd + cur_even;
		cur_n *= 2;
	} while (!(((abs((cur_int - prev_int) / cur_int) / 15) < eps) || ((abs(cur_int - prev_int) / 15) < eps)));
	return cur_int;
}