#include <cstdio>
#include <iostream>
#include <math.h>
#include <vector>

using namespace std;

static double eps = 1E-14;

const int n = 4;
void GaussSeidel(double Matrix[n][n], double RightPart[n], double x[n]);
//void Gauss(vector<vector<double>> Vec, vector<double> RightPart, double x[n]);
void Gauss(double Matrix[n][n], double RightPart[n] , double x[n]);
void printMatrix(double Matrix[n][n], double RightPart[n], double x[n]);
void rewriteMatrix(double RightP[n], double RightPart[n], double tmpMatrix[n][n], double Matrix[n][n]);
