#include <iostream>
#include <vector>

class Gaussian
{
public:
	Gaussian();
   ~Gaussian();
   void outM(std::vector<std::vector<double>> matrixO);
private:
	std::vector<double> _div_v(std::vector<double> vec,double fd);					//������� ������� �� ����������
	std::vector<double> _mul_v(std::vector<double> vec,double fd);					//��������� ������� �� ����������
	std::vector<double> _sub_v(std::vector<double> vec,std::vector<double> vec2);    //��������� ������� �� �������
	void solution();
	void getroot();

	

	std::vector<std::vector<double>> matrix;
	std::vector<std::vector<double>> matrix2;
	std::vector<double> root;
};