#include "Gaussian.h"

Gaussian::Gaussian()
{
 std::vector<double> v;

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

  solution();

}

Gaussian::~Gaussian()
{
}

void Gaussian::solution()
{
	std::vector<double> res1;
	std::vector<double> res2;
	for (int i = 0; i < matrix.size()+1; i++)
	{
		res1=_div_v(matrix[0],matrix[0][0]);    //дел на а11
		matrix2.push_back(res1);
		for (int j = 1; j < matrix.size(); j++)
		{
			res2=_mul_v(res1,matrix[j][0]);
			res2=_sub_v(res2,matrix[j]);
			res2.erase(res2.begin());
			matrix[j-1]=res2;
		}
		matrix.erase(matrix.end()-1);
		std::cout<<std::endl;
	}
	matrix2.push_back(_div_v(matrix[0],matrix[0][0])); 

	getroot();
}

void Gaussian::getroot()
{
	root.push_back(matrix2[matrix2.size()-1][1]);

	for (int i = 0; i < matrix2.size()-1; i++)
		{
			for (int j = 1; j < matrix2[i].size()-1; j++)
			{
             if (matrix2[i][j]>0)
				 matrix2[i][j]=0-matrix2[i][j];
			 else 
				matrix2[i][j]=abs(matrix2[i][j]);
			}
		}

	matrix2[2][1]=matrix2[2][1]*root[0];
	root.push_back(matrix2[2][1]+matrix2[2][2]);

	matrix2[1][1]=matrix2[1][1]*root[1];
	matrix2[1][2]=matrix2[1][2]*root[0];
	root.push_back(matrix2[1][1]+matrix2[1][2]+matrix2[1][3]);

	matrix2[0][1]=matrix2[0][1]*root[2];
	matrix2[0][2]=matrix2[0][2]*root[1];
	matrix2[0][3]=matrix2[0][3]*root[0];
	root.push_back(matrix2[0][1]+matrix2[0][2]+matrix2[0][3]+matrix2[0][4]);

		for (int i = root.size()-1; i >=0; i--)
		{
			std::cout<<root[i]<<" ";
		}
		std::cout<<std::endl;
}


void Gaussian::outM(std::vector<std::vector<double>> matrixO)
{
	for (int i = 0; i < matrixO.size(); i++)
	{
		for (int j = 0; j < matrixO[i].size(); j++)
			std::cout<<matrixO[i][j]<<" ";
		std::cout<<std::endl;
	}
}

std::vector<double> Gaussian::_div_v(std::vector<double> vec,double fd)
{
	std::vector<double> fret;

	for (int i = 0; i < vec.size(); i++)
			fret.push_back(vec[i]/fd);

 return fret;
}

std::vector<double> Gaussian::_mul_v(std::vector<double> vec,double fd)
{
	std::vector<double> fret;

	for (int i = 0; i < vec.size(); i++)
			fret.push_back(vec[i]*fd);

 return fret;
}

std::vector<double> Gaussian::_sub_v(std::vector<double> vec,std::vector<double> vec1)
{
	std::vector<double> fret;

	for (int i = 0; i < vec.size(); i++)
			fret.push_back(vec1[i]-vec[i]);

 return fret;
}