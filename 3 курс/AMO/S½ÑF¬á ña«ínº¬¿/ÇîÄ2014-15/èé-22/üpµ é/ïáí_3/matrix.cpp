#define _USE_MATH_DEFINES
#include <conio.h>
#include <iostream>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <algorithm>

using namespace std;

template <typename T>

void PrintMatrix(T &Matr,int row,int col)
{
	int i,j;
	for (i = 0; i < row; i++)
	{
		for (j=0; j < col;  j++)
		{
			cout<<*(Matr + i*col + j)<<"	";
		}
	cout<<endl;
	}
}
void GaussJordan (double *Matr, int row, int col){
	double *Matrix = new double [row*col];
	for (int i = 0; i < row*col; i++)
	{
		*(Matrix + i) = *(Matr + i);
	}


	for (int i = 0; i < row; i++)
	{
		double a1 = *(Matrix + i*col + i);
		for (int j = 0; j < col; j++)
		{
			*(Matrix + i*col + j) /= a1;
		}

		for (int j = 0; j < row; j++)
		{
			double a0 = *(Matrix + j*col + i);
			if (j != i)
			{
				for (int k=0; k < col; k++)
				{
					*(Matrix + j*col + k) -= (*(Matrix + i*col + k)) * a0; 
		 		}
			}
		}
	}

	cout<<"Gauss - Jordan method:"<<endl<<endl;;
    PrintMatrix(Matrix, row,col);
}
double mNorma(double *M,int row, int col)
{
	double *vect = new double [row];
	double max;
	for (int i = 0; i < row; i++)
	{
		for (int j =0; j < col - 1;j++)
		{
			*(vect + i) += *(M + i*col + j); 
		}
	}
	max = *(vect + 0);
	for (int i = 0; i < row ; i++)
	{
		if (*(vect + i) > max) max = *(vect + i);
	}
	return max;
}
void DirectIteration (double *Matr, int row, int col){
	double *Matrix = new double [row*col];
	double *x = new double [row];
	double *b = new double [row];
	double *x1 = new double [row];
	double *el = new double [col];

	cout<<"DirectIteration method:"<<endl<<endl;

	
	for (int i = 0; i < row*col; i++)
	{
		*(Matrix + i) = *(Matr + i);
	}
	int k = 0;
	for (int i = 0; i < row ; i++)
	{
		int max = 0;
		int imax = 0;

		for (int j = 0; j < col; j++)
		{
			*(el + j) = *(Matrix + i*col + j);
		}
		
		for (int j = i; j < row ; j++)
		{
			if ( (*(Matrix + j*col + i) > *(Matrix + (j+1)*col + i)) && (*(Matrix + j*col + i) > max) ) 
			{
				max = *(Matrix + j*col + i);
				imax = j;
			}
		}
		int k = 0;
		for (int j = 0; j < col; j++)
	 	{
			*(Matrix + i*col  + j) = *(Matrix + imax*col + j);
			*(Matrix + imax*col + j) = *(el + k);
			k++;
		}

	}
	PrintMatrix(Matrix, row,col);

for (int i = 0; i < row; i++)
	{ 		double d = *(Matrix + i*col + i);
		*(Matrix + i*col + 4) /= d;
		for (int j = 0; j < col - 1; j++)
		{
			*(Matrix + i*col + j) /= -d;	
		}
		*(Matrix + i*col + i) = 0;
	}
	do{
	for (int k = 0; k < row; k++)
	{
		*(x + k) = *(Matrix + k*col + 4);
	}
	*b = *x;
	for (int i = 0; i < row; i++)
	{
		for (int j = 0; j < row - 1; j++)
		{	
			*(x1 + j) = *(Matrix + i*col + 4) + (*(Matrix + i*col + j) * (*(x + j)));
		}
	}
	
	}while ( mNorma(x,4,1) - mNorma(Matrix,4,5) / 1 - mNorma(Matrix,4,5) < mNorma(b,4,1));

	for (int i = 0; i < row; i++)
	{
		if (*(x1 +i) < 0) *(x1 +i) = 0;
	}
	cout <<"x1 = "<< *(x1 + 1)<< endl<<"x2 = "<< *(x1 + 0)<< endl
	<<"x3 = "<< *(x1 + 3)<< endl<<"x4 = " << *(x1 + 2)<< endl;

}

int main()
{
	int i = 0, j = 0;
	int buf;
	int Counter = 0;
	FILE* f = fopen("file.txt","r");
	double *Matrix = new double;
	while (fscanf(f,"%d",&buf) != EOF)
	{
		Matrix = (double*)realloc(Matrix,sizeof(double)*(Counter + 1));
		Matrix[Counter] = buf;
		Counter++;
	}
	while((i*(i+1)) != Counter) i++;
	j = i + 1;
	cout<<"Matrix:"<<endl<<endl;
	PrintMatrix(Matrix,i,j); 
	cout<<endl;

	GaussJordan (Matrix,i,j);
	DirectIteration (Matrix, i, j);
}
