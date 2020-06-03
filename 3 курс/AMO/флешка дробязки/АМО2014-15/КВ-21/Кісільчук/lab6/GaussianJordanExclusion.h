#pragma once
#include <stdio.h>

bool CheckMainElementsMatrix(double** matrix, int m);

double* GaussianJordanExclusion(double** matrix, int m);

void PrintMatrix(double** matrix, int n);