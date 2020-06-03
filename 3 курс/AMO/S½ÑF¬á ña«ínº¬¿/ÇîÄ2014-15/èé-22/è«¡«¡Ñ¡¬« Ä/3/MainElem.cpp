#include "MainElem.h"

MainElem::MainElem()
{
	n = 4;
}

MainElem::~MainElem()
{

}

int *MainElem::Max_coef_cordinates(double **matrix, int step)
{
	int max = matrix[0][0];
	int toret[2];

	for(int i = 0; i < n - step; i++)
		for(int j = 0; j < n - step; j++)
			if(matrix[i][j] >= max)
			{
				max = matrix[i][j];
				toret[0] = i;
				toret[1] = j;
			}

	return toret;
}

double MainElem::Multiplier(double **matrix, int row_num, int step)
{
	//int matrix_size = sizeof(*matrix)/sizeof(**matrix);
	double toret;
	int max[2] = {this->Max_coef_cordinates(matrix, step)[0], this->Max_coef_cordinates(matrix, step)[1]};

	//for(int i = 0; i < matrix_size; i++)
		//if(i != max[0])
	toret = -matrix[row_num][max[1]] / matrix[max[0]][max[1]];
	
	return toret;
}

double MainElem::Matrix_mult(double **matrix, int posi, int posj, int step)
{
	//int matrix_size = sizeof(*matrix)/sizeof(**matrix);

	int max[2] = {this->Max_coef_cordinates(matrix, step)[0], this->Max_coef_cordinates(matrix, step)[1]};

	//for(int i = 0; i < matrix_size; i++)
		//for(int j = 0; j < matrix_size; j++)
	double toret = matrix[posi][posj] + matrix[max[0]][posj]*Multiplier(matrix, posi, step);
		
	return toret;
}

double *MainElem::Triangle(int matrix[4][5], double **ansmatrix)
{
	double *toret = new double[n];
	int *var = new int[n];

	double **newmatrix = new double*[n];
		for (int i = 0; i < n; i++)
			newmatrix[i] = new double[n+1];

	for(int i = 0; i < n; i++)
		for(int j = 0; j < n+1; j++)
			newmatrix[i][j] = matrix[i][j];

	for(int k = 0; k < n; k ++)
	{
		int max[2] = {this->Max_coef_cordinates(newmatrix, 0)[0], this->Max_coef_cordinates(newmatrix, 0)[1]};
		var[k] = max[1];

		for(int i = 0; i < n; i++)
		{
			newmatrix[max[0]][i] = 0;
			newmatrix[i][max[1]] = 0;
		}
	}

	/*for(int i = 0; i < n; i++)
	{
		toret[var[n-i-1]] = ansmatrix[n-i-1][i+1];
		for(int j = 0; j < i; j++)
			toret[var[n-i-1]] = toret[var[n-i-1]] - ansmatrix[n-i-1][var[j]]*toret[var[n-i]];

		toret[var[n-i-1]] = toret[var[n-i-1]]/ansmatrix[n-i-1][var[n-i-1] - 1];
	}*/
	
	/*toret[var[n-1]] = ansmatrix[n-1][1]/ansmatrix[n-1][0]; //x4
	toret[var[n-2]] = (ansmatrix[n-2][2] - ansmatrix[n-2][1]*toret[var[n-1]])/ansmatrix[n-2][0]; //x1
	toret[var[n-3]] = (ansmatrix[n-3][3] - ansmatrix[n-3][0]*toret[var[n-2]] - ansmatrix[n-3][2]*toret[var[n-1]])/ansmatrix[n-3][1]; //x3
	toret[var[n-4]] = (ansmatrix[n-4][4] - ansmatrix[n-4][2]*toret[2] - ansmatrix[n-4][0]*toret[0] - ansmatrix[n-4][3]*toret[3])/ansmatrix[n-4][1]; //x2
	*/
	
	toret[var[n-1]] = ansmatrix[n-1][1]/ansmatrix[n-1][0]; //x3
	toret[var[n-2]] = (ansmatrix[n-2][2] - ansmatrix[n-2][1]*toret[var[n-1]])/ansmatrix[n-2][0]; //x1
	toret[var[n-3]] = (ansmatrix[n-3][3] - ansmatrix[n-3][0]*toret[var[n-2]] - ansmatrix[n-3][2]*toret[var[n-1]])/ansmatrix[n-3][1]; //x3
	toret[var[n-4]] = (ansmatrix[n-4][4] - ansmatrix[n-4][2]*toret[2] - ansmatrix[n-4][0]*toret[0] - ansmatrix[n-4][3]*toret[3])/ansmatrix[n-4][1]; //x2
	
	

	std::cout << std::endl;
	for(int i = 0; i < n; i++)
		std::cout << i+1 << ' ' << RootCalculate(ansmatrix,i)[i] << ' '<< std::endl;
		
	return toret;
}

double **MainElem::Method(int matrix[4][5])
{
	double **newmatrix = new double*[n];
		for (int i = 0; i < n; i++)
			newmatrix[i] = new double[n+1];
	
	double **toretmatrix = new double*[n];
		for (int i = 0; i < n; i++)
			toretmatrix[i] = new double[n-i+1];

	int xoff[4];

	for(int k = 0; k < n - 1; k++)
	{
		double **resmatrix = new double*[n-k];
		for (int i = 0; i < n-k; i++)
			resmatrix[i] = new double[n-k];
		
		if(k == 0)
		{
			for(int i = 0; i < n; i++)
				for(int j = 0; j < n+1; j++)
					newmatrix[i][j] = matrix[i][j];
		}


		int newmatrix_size = sizeof(*newmatrix);
		//int newmatrix_size = sizeof(matrix)/sizeof(*newmatrix); 
		int max[2] = {this->Max_coef_cordinates(newmatrix, k)[0], this->Max_coef_cordinates(newmatrix, k)[1]};
		//xoff[k] = max[1];

		for(int i = 0; i < newmatrix_size-k; i++)
			for(int j = 0; j < newmatrix_size-k+1; j++)
			{
				if(i == max[0])
				{
					resmatrix[i][j] = newmatrix[i][j];
					toretmatrix[k][j] = newmatrix[i][j];
				}
				else
					resmatrix[i][j] = Matrix_mult(newmatrix,i,j,k);	
				
			}
		
			//xoff[k] = this->Max_coef_cordinates(resmatrix, k)[1];

		std::cout << std::endl;
		for(int i = 0; i < newmatrix_size-k; i++)
		{
			for(int j = 0; j < newmatrix_size-k+1; j++)
				std::cout << resmatrix[i][j] << ' ';
			std::cout << std::endl;
		}

		delete [] newmatrix;
		double **newmatrix = new double*[n-k];
		for (int i = 0; i < n-k; i++)
			newmatrix[i] = new double[n-k];

		int x = 0;
		int y = 0;
		for(int i = 0; i < newmatrix_size-k; i++)
			if (i != max[0])
			{
				for(int j = 0; j < newmatrix_size-k+1; j++)
					if(j != max[1])
					{
						newmatrix[x][y] = resmatrix[i][j];
						y++;
					}
				x++;
				y = 0;
			}
		
		delete [] resmatrix;

		std::cout << std::endl;
		std::cout << k << std::endl;
		for(int i = 0; i < newmatrix_size-k-1; i++)
		{
			for(int j = 0; j < newmatrix_size-k-1+1; j++)
				std::cout << newmatrix[i][j] << ' ';
			std::cout << std::endl;
		}

	}
	toretmatrix[3][0] = newmatrix[0][0];
	toretmatrix[3][1] = newmatrix[0][1];


		std::cout << std::endl;
		std::cout << "Triangle Matrix" << std::endl;
		for(int i = 0; i < n; i++)
		{
			for(int j = 0; j < n - i + 1; j++)
				std::cout << toretmatrix[i][j] << ' ';
			std::cout << std::endl;
		}

	Triangle(matrix, toretmatrix);

	return toretmatrix;
}











