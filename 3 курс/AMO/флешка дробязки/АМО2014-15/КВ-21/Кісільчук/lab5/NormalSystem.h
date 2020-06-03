#pragma once
#include <stdio.h>
#include "PolynomialsLegendre.h"
#include "IntegralSimpson.h"
#include "Function.h"

long double** CreateNoralEquations(int m, long double a, long double b, long double(*Func)(long double, int));

void PrintNoralEquations(long double** matrix, int m);