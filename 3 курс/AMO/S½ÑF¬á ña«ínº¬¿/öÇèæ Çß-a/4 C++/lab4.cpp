//Program lab4(input,output)

#include <stdio.h>
#define N 255	//������� ����� � ����������� ����
typedef unsigned char byte;	//char ��������������� ����, �� �� ����� 1 ����

extern "C" void BigShowN(byte* p1, int p2); //������� �� ��� ����������

int main(){
	byte x[N],y[N]; //�������� �����

	for (int i=0; i<N; i++){
		x[i]=i;
		y[i]=0;
	} //for
 
    printf("x=");
    BigShowN(x,N);
    printf("y=");
    BigShowN(y,N);
	return 0;
} //main
