#include <conio.h>
#include "functions.h"
#include <iostream>
using namespace std;


int main()
{
	//������ � �������������� �0..�n ��������
	double* Result;
	//������� ����������
	int n = 1;
	//���������� ����� ����������� �������
	int N = 50;
	//������� ��������������
	double A = 0;
	double B = 6;
	//������������� �������� ���������� ����������
	double epsIntegral = 1e-13;
	//����������� �������� ��������������������� ����������
	double epsLSD = 0.01;
	//�������������������� ���������� ���������
	double LSD = 10;
	//�������� �������������������� ���������� ���� �� ��������� ������ ��������
	do
	{
		cout<<"Stepen mnogochlena = "<<n-1<<endl;
		Result = BuildMatrix(n, A, B, epsIntegral);
		LSD = LestSquaresApproximation(Result, n, N, A, B);
		cout<<endl<<"DELTA["<<n<<"]="<<LSD<<endl<<endl;
		n+=5;
		_getch(); system("cls");
	}
	while (LSD >= epsLSD);

	_getch();
}