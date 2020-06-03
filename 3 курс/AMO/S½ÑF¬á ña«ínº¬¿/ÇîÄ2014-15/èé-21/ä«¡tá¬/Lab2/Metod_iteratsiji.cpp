#include "Metod_iteratsiji.h"


/*���������� q �� �������� ��������.*/
double ObchQ(double a, double b){
	double al = ObchPohidn(a) / ObchPohidn(b);
	if (1 < al){
		return 1 - 1 / al;
	}
	return 1 - al;
}

/*���������� Alpha �� �������� ��������*/
double ObchAlpha(double a, double b){
	double ZnA = ObchPohidn(a);
	double ZnB = ObchPohidn(b);
	if (ZnA > ZnB){
		return 1 / ZnA;
	}
	return 1 / ZnB;
}



/*���������� ����������.*/
double ObchNabl(double x, double a, double b){
	double alpha = ObchAlpha(a, b);
	double f = ObchFunc(x);
	return x - alpha*f;
}

/*���������� ��������� ����� ����������.*/
double ObchConstZav(double q, double e){
	return (1 - q)*e / q;
}

/*���������� ������� ���������� �� �������� [a, b].*/
double ObchPershNabl(double a, double b){
	return (a + b) / 2;
}

/*���������� ����������� �������� ������ �� �������� [a, b] �� ������� e.*/
double* ObchKoren_it(double a, double b, double e){
	double Xnast = ObchPershNabl(a, b);//�������� X �� �������� (K+1)
	double Xpoper;//�������� X �� �������� K
	double q = ObchQ(a, b);//���������� ���
	double Czav = ObchConstZav(q, e);//��������� ����� ����������
	int i = 0;//������� �������� �������
	do{
		Xpoper = Xnast;
		Xnast = ObchNabl(Xpoper, a, b);
		i++;
	} while (abs(Xnast - Xpoper) > Czav);
	double* fff = new double[2];
	fff[0] = Xnast;
	fff[1] =i;
	return fff;
}

/*��������� ����� ������� ������*/
/*
void VykPershChast(double eps, double KrZminy, int KilkZmin){
	double MasKoren[3][2] = { { 0.01, 0.1 }, { 0.3, 0.4 }, { 3.3, 3.4 } };
	FILE* f = fopen("AMO2_iteratsija.txt", "wt");
	fprintf(f, "eps\t\t\t��������\t\t������ �������\n");
	for (int i = 1; i <= KilkZmin; i++){
		eps *= KrZminy;
		for (int j = 0; j < 3; j++){
			double xi = ObchKoren(MasKoren[j][0], MasKoren[j][1], eps);
			double OcTochn = ObchConstZav(ObchQ(MasKoren[j][0], MasKoren[j][1]), eps);
			fprintf(f, "%.16f\t%.16f\t%.16f\n", eps, xi, OcTochn);
		}
	}
	fcloseall();
}
*/