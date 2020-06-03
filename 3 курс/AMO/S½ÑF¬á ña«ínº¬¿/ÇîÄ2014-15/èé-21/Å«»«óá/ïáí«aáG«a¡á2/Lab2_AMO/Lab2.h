#include <vector>
#include <iostream>
#include <math.h>

class inte
{
 public:
        double a;
        double b;
};

class Root
{
public:
	
	Root();
	~Root();
	void MofApproxim();  //��������
	void MofNewton();    //����� �������� 
	void Comp();         //���������;

private:

	double _eps();                                 //�������
	double _Q(double a, double b);                 //��������� Q
	double _der(double a);                         //�����������
	double _der2(double a);                        //����������� 2
	double _ConstForEnd(double q);                 //��������� ������� ����������
	double _ApproxCalc(double x,double a,double b);//��������������� ����������
	double _AlphCalc(double a,double b);           //���������� ����� �� ������� ���������
	double _FuncCalc(double x);                    //��������� �������� �������
	double _m1(double a);                 //��������� �1
	double _xn_B(double prev);                     //���������� ��� ��������

	double* root_App(double a, double b);
	double* root_New(double a, double b); 
	
	double eps;

	std::vector<inte> arr_v;
	std::vector<inte> arr_v2;
};