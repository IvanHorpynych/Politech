#pragma once
#include <math.h>
#include <iostream>
#include <fstream>
#include <functional>
#include "methodMatrix.h"
#include "Integral.h"

using namespace std;


double lejandr(int n, double x);
double* constA(double a, double b, double eps, int N, function<double(double)> f);
double value(double*A, double x, int N);