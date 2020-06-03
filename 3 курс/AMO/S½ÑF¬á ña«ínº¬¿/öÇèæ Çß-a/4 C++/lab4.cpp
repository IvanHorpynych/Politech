//Program lab4(input,output)

#include <stdio.h>
#define N 255	//кількість байтів у надвеликому числі
typedef unsigned char byte;	//char використовується тому, що він займає 1 байт

extern "C" void BigShowN(byte* p1, int p2); //функція на мові ассемблера

int main(){
	byte x[N],y[N]; //надвеликі числа

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
