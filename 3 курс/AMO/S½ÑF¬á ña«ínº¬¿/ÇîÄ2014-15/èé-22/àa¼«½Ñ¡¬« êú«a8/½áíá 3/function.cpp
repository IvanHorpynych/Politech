#include "function.h"

void GaussSeidel(double Matrix[n][n], double RightPart[n], double x[n]){
     double tmpMatrix[n][n];
	 double RightP[n];
	 double d,s;
	 rewriteMatrix(RightP, RightPart, tmpMatrix, Matrix);
     do{
		d = 0;
        for (int i = 0; i < n; i++){
			s = 0;
            for (int j = 0; j < n; j++)
				if (i != j)
					s += tmpMatrix[i][j] * (x[j]);
            double temp = x[i];
            x[i] = (RightP[i] - s) / tmpMatrix[i][i];
            d = abs(temp - x[i]);
         }
     } while (eps < d);
     printMatrix(tmpMatrix, RightP, x);
}
void Gauss(double Matrix[n][n], double RightPart[n] , double x[n]){
	double tmpMatrix[n][n];
	double tmp = 0,g = 0;
	double RightP[n];
	rewriteMatrix(RightP, RightPart, tmpMatrix, Matrix);
	for (int i = 0; i < n; i++){
		for (int j = i + 1; j < n; j++){
			tmp = tmpMatrix[j][i]/tmpMatrix[i][i];
			for (int k = i; k < n; k++)
				tmpMatrix[j][k] = tmpMatrix[j][k] - tmp*tmpMatrix[i][k];
			RightP[j] = RightP[j] -tmp*RightP[i];
		}
	}
	for (int k = n - 1; k>=0; k--){
		tmp = 0;
		for (int j = k + 1; j<n; j++){
			g = tmpMatrix[k][j]*x[j];
			tmp = tmp + g;
		}
		x[k] = (RightP[k] - tmp)/tmpMatrix[k][k];
	}
	printMatrix(tmpMatrix, RightP, x);
}
void printMatrix(double Matrix[n][n], double RightPart[n], double x[n]){
	for (int i=0; i<n;i++){
		for (int j = 0; j < n; j++){
			cout.precision(4);
			cout << Matrix[i][j] << "\t";
		}
		cout << RightPart[i] << "\t";
		cout << "x" << i+1 << "=" << x[i] << endl;
	}
}
void rewriteMatrix(double RightP[n], double RightPart[n], double tmpMatrix[n][n], double Matrix[n][n]){
	for (int i = 0; i < n; i++)
		RightP[i] = RightPart[i];
	for (int i = 0; i< n; i++)
		for (int j = 0; j < n; j++)
			tmpMatrix[i][j] = Matrix[i][j];
}