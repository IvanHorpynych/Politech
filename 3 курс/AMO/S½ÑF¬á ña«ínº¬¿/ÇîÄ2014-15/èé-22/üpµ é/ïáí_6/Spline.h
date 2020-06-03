#pragma once
//Структура для коэффициентови кубического сплайна сплайна
struct SplineKoefs
{
	double A, B, C, D ,X;
};
//Класс, котoрый строит кубический сплайн
class CubSpline
{
private:
	//  Динамический массив с коэффициентами сплайнов
	SplineKoefs* Koefs;
	//  Количества сплайнов
	int N;
public:
	//  Функция построения кубического сплайна для n заданных точек
	void BuildSpline(double *XVect, double *FxVect, int n);
	//  Возвращает значение сплайна по переданному значению Х
	double ReturnSplineValue(double X);
	//  Считает значение сплайна
	double CountValue(double X, SplineKoefs* Ptr);
};