#include <iostream>
#include <vector>


class Seidel
{
public:
   	 Seidel();
	~Seidel();
private:
	double _beta_c(int i);

	void _xi_c(int i);

	void outM(std::vector<std::vector<double>> matrixO);

	void ForNormM();

	void solution();

	void _redM();

	double ret_Max_D();

	void addNr();
	double eps;
	std::vector<double> max_d;
	std::vector<double> xi;
	std::vector<std::vector<double>> xi_arr;
	std::vector<std::vector<double>> matrix;
};	