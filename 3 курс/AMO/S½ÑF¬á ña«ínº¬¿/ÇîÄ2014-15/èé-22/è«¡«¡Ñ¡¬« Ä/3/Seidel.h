#include <iostream>
#include <vector>


class Seidel
{
public:
   	 Seidel();
	~Seidel();
private:
	//void PrintMatrix(std::vector<std::vector<double>> matrix);
	void Rootx(int i);
	void MatrixNorm();
	void Method();
	double Condition();
	void ZeroPrev();
	void InputMatrix();

	double eps;
	std::vector<double> MaxRoot;
	std::vector<double> xi;
	//std::vector<std::vector<double>> xi_arr;
	std::vector<std::vector<double>> matrix;
};	