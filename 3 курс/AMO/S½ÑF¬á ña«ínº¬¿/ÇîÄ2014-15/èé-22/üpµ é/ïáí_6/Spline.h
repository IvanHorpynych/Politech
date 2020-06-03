#pragma once
//��������� ��� �������������� ����������� ������� �������
struct SplineKoefs
{
	double A, B, C, D ,X;
};
//�����, ���o��� ������ ���������� ������
class CubSpline
{
private:
	//  ������������ ������ � �������������� ��������
	SplineKoefs* Koefs;
	//  ���������� ��������
	int N;
public:
	//  ������� ���������� ����������� ������� ��� n �������� �����
	void BuildSpline(double *XVect, double *FxVect, int n);
	//  ���������� �������� ������� �� ����������� �������� �
	double ReturnSplineValue(double X);
	//  ������� �������� �������
	double CountValue(double X, SplineKoefs* Ptr);
};