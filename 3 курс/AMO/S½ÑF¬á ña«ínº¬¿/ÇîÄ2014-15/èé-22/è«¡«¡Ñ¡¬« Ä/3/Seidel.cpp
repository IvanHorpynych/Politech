#include "Seidel.h"

Seidel::Seidel()
{
	this->InputMatrix();
	this->Method();
}

Seidel::~Seidel()
{

}

void Seidel::InputMatrix()
{
 eps = 1E-3;
 std::vector<double> v;

 /*v.push_back(11);
 v.push_back(14);
 v.push_back(5);
 v.push_back(8);
 v.push_back(98);

 matrix.push_back(v);
 v.clear();

 v.push_back(3);
 v.push_back(38);
 v.push_back(18);
 v.push_back(16);
 v.push_back(108);

 matrix.push_back(v);
 v.clear();

 v.push_back(18);
 v.push_back(4);
 v.push_back(32);
 v.push_back(9);
 v.push_back(185);

 matrix.push_back(v);
 v.clear();

 v.push_back(12);
 v.push_back(9);
 v.push_back(13);
 v.push_back(4);
 v.push_back(111);

 matrix.push_back(v);
 v.clear();*/

 v.push_back(20);
 v.push_back(20);
 v.push_back(20);
 v.push_back(0);
 v.push_back(180);

 matrix.push_back(v);
 v.clear();

 v.push_back(16);
 v.push_back(37);
 v.push_back(14);
 v.push_back(6);
 v.push_back(163);

 matrix.push_back(v);
 v.clear();

 v.push_back(18);
 v.push_back(6);
 v.push_back(33);
 v.push_back(8);
 v.push_back(218);

 matrix.push_back(v);
 v.clear();

 v.push_back(16);
 v.push_back(9);
 v.push_back(14);
 v.push_back(13);
 v.push_back(142);

 matrix.push_back(v);
 v.clear();
}


/*void Seidel::PrintMatrix(std::vector<std::vector<double>> matrix)
{
	for (int i = 0; i < matrix.size(); i++)
	{
		for (int j = 0; j < matrix[i].size(); j++)
			std::cout<<matrix[i][j]<<"|";
		std::cout<<std::endl;
	}
}*/


void Seidel::Method()
{
	ZeroPrev(); 
	MatrixNorm();
	//xi_arr.push_back(xi);
	int n=0;

	printf("N    x1    x2     x3     x4    MaxRoot\n");
	double ifexit=0;
	do
	{
		std::cout << n << " ";
		for (int i = 0; i < matrix.size(); i++)
		{
			Rootx(i);
			printf("%.4f ", xi[i]);
		}
		
		//for (int i = 0; i < max_d.size(); i++)
			//printf("%.4f ", abs(max_d[i]));

		ifexit = Condition();
		printf("%.4f \n",ifexit);
		MaxRoot.clear();
		n++;
	}
  while(ifexit>eps);

}

/*void Seidel::_redM()
{
	
}*/

double Seidel::Condition()
{
	double max=abs(MaxRoot[0]);
	for (int i = 1; i < MaxRoot.size(); i++)
	{
		if (max<abs(MaxRoot[i])) max=abs(MaxRoot[i]);
	}
	return max;
}

void Seidel::MatrixNorm()
{
	for (int i = 0; i < matrix.size(); i++)
	{

		for (int j = 0; j < matrix[i].size()-1; j++)
		{
		  if(i!=j)
			if (matrix[i][j] > 0)
				matrix[i][j] = -matrix[i][j];
			else matrix[i][j] = abs(matrix[i][j]);
		}
		matrix[i].insert(matrix[i].begin(),matrix[i][matrix[i].size()-1]);
		matrix[i].erase(matrix[i].end()-1);
	} 
}

void Seidel::Rootx(int i)
{
	double sum = matrix[i][0];

		for (int j = 1; j < matrix[i].size(); j++)
			if (i+1 != j)
				sum += matrix[i][j]*xi[j-1];

		MaxRoot.push_back(abs(xi[i] - sum/matrix[i][i+1]));
		xi[i] = sum/matrix[i][i+1];
		//xi_arr.push_back(xi);
}

void Seidel::ZeroPrev()
{
	xi.push_back(0);
	xi.push_back(0);
	xi.push_back(0);
	xi.push_back(0);
}

/*double Seidel::_beta_c(int i)
{
	return matrix[i][i];
}*/


