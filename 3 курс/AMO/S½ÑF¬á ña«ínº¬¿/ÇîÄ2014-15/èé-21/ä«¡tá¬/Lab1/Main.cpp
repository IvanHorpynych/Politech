#include "AMO_1.h"
#pragma warning (disable:4996)
int main(){
	double a = -9.8; //��� ����
	double b = 13.9; //����� ����
	double eps = 0.01; //������ �������
	double krok = 0.001; //���� ���� �������
	double suma = 0.0; //���� ����
	double chlen = 0; //�������� ���� ����
	int n = 0; //������� ������� ����
	double x; //�����, � ��� ������������ �������� �������
	double TochnZn; //����� �������� ������� (� ������ ������� sh(x))
	int n2; //��������� ������� ����
	double h;
	int i;
	FILE* FTabl1;
	FTabl1 = fopen("Tabl1.txt", "w+t");

	//��������� ����������� ����� ���������� ������� ��� ����� ������� ������
	x = ZnahTochky(a, b);
	//��������� ���������� ������� �������� �������
	TochnZn = sinh(x);
	fprintf(FTabl1, "eps                ���������� ����    �������             �������\n");
	//�������� ���� ����� ������� ������
	while (eps > 1.0e-015){
		while (abs(suma - TochnZn) > eps){
			chlen = ZnahNastupnogo(chlen, x, n);
			suma += chlen;
			n++;
		}
		if (1.0e-008 == eps){
			n2 = n;
		}
		fprintf(FTabl1, "%.16f %.16f %.16f %d\n",eps, chlen, (suma - TochnZn), n);
		ZminaTochnosti(eps, krok);
		/*
		printf("ObchZn =\t %.16f\n", suma);
		printf("ZalChlen =\t %.16f\n", chlen);
		printf("Pohybka =\t %.16f\n", (suma - TochnZn));
		printf("DovzhRiadu =\t %d\n", n);
		*/
	}
	fprintf(FTabl1, "\n\n\nXi         ��������� �������      ���������� ����\n");
	/*��������� ������� �����*/
	eps = 1.0e-008;
	h = ObchH(a, b);
	suma = 0;
	for (int i = 0; i <= 10; i++){
		suma = 0;
		x = ObchXi(a,b, h, i);
		for (int k = 0; k <= n2; k++){
			chlen = ZnahNastupnogo(chlen, x, k);
			suma += chlen;
		}
		TochnZn = sinh(x);
		fprintf(FTabl1, "%.16f\t%.16f\t%.16f\n", x, (suma-TochnZn), chlen);
		//printf("AbsZn  =\t %.16f\n", TochnZn);
		//printf("ObchZn =\t %.16f\n", suma);
	}





	//getchar();

	fcloseall();
	return 0;
}