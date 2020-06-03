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
	void MofApproxim();  //итераций
	void MofNewton();    //метод дотичних 
	void Comp();         //порівняння;

private:

	double _eps();                                 //похибка
	double _Q(double a, double b);                 //вычисляет Q
	double _der(double a);                         //Производная
	double _der2(double a);                        //Производная 2
	double _ConstForEnd(double q);                 //Константа условия завершения
	double _ApproxCalc(double x,double a,double b);//Приблизительное вычисления
	double _AlphCalc(double a,double b);           //Вычисление Альфа на заданом интервале
	double _FuncCalc(double x);                    //вычисляем значение функции
	double _m1(double a);                 //вычисляем м1
	double _xn_B(double prev);                     //вычисление для бисекции

	double* root_App(double a, double b);
	double* root_New(double a, double b); 
	
	double eps;

	std::vector<inte> arr_v;
	std::vector<inte> arr_v2;
};