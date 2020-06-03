#pragma once
//������� �������� ������������ � ���������� �������
double CountNormMatrixElement(int step1, int step2, double _X);
//������� �������� n-�� ����� ��������
double CountTn(int step, double _X);
//������� �������� ������� ������ � ���������� �������
double CountNormMatrixFreeTerm(int step, double _X);
//������� �������� �������� ������� 
double CountFx(double _X);
//������� ���������� ���������� ������� � �������� ������������ ��������,
//���������� ��������� �� ������ ������������� ��������
double* BuildMatrix(int n, double A, double B, double eps);
//�������� �������� �������� �� X
double CountPolynomValue(double* Result, int n, double X);
//������� �������� �������� ��������������� ����������
double LestSquaresApproximation(double* Result, int n, int N, double a, double b);
//������� �������� ��������� 
double CountIntegral(double A, double B, double eps, int step1, int step2, int Flag);
double*  SolveMatr ( double *mat, int razmer);