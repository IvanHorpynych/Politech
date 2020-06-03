#include <iostream>
#include <vector>


class MainElem
{
public:
	MainElem();
	~MainElem();
	int *Max_coef_cordinates(double **matrix, int step);
	double Multiplier(double **matrix, int row_num, int step);
	double Matrix_mult(double **matrix, int posi, int posj, int step);
	double **Method(int matrix[4][5]);
	double *Triangle(int matrix[4][5], double **ansmatrix);

private:
	int n;
};	