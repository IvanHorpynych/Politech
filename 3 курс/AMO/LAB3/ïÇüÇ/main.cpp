#include "methods.h"
using namespace std;
int main()
{
	int index, sub_index, si = 0, sj = 0;
	double eps = 0.001;
	double MATRIX[M][N] = {{6.0,12.0,20.0,15.0,115.0 }, 
				  	  {15.0,42.0,17.0,9.0,275.0}, 
					  {1.0,16.0,22.0,4.0,158.0 }, 
					  {10.0,2.0,17.0,7.0,103.0}};

	double **S_MATRIX = new double*[M]; 
	for (index = 0; index < N; index++) S_MATRIX[index] = new double[N];
	for(index = 0; index < M; index++) 
		for(sub_index = 0; sub_index < N; sub_index++) 
			S_MATRIX[index][sub_index] = MATRIX[index][sub_index];

	double *x=new double[M];
	for(index = 0; index < M; index++) x[index] = 0.0;

	cout<<"         Gaussian Elimination Method        "<<endl;
	cout << "       x1       " << "       x2       " << "       x3       " << "       x4       " << endl;
	cout<<"-------------------------------------------------------------------"	<<endl;
	gaussian_elimination(S_MATRIX, x, si, sj);
	for (index = 0; index < M; index++)	cout<<setw(14)<<x[index]<<" | ";
	cout<<endl<<"-------------------------------------------------------------------"<<endl<<endl;


	cout<<"         Direct Method        "<<endl;
	cout << "       x1       " << "       x2       " << "       x3      " << "         x4       " << endl;
	cout<<"-------------------------------------------------------------------"<<endl;
		direct(MATRIX, x, eps);
	for (index = 0; index < M; index++)
		cout<<setw(14)<<x[index]<<" | ";
	cout<<endl<<"-------------------------------------------------------------------"<<endl;

	system("PAUSE");
	return 0;
}