#pragma once
#include <stdio.h>
#include <math.h>

/*Виконує множення матриці Systema розміром m*n на стовпець Stovpets розміром m. Повертає стовпець.*/
double* MnozhenniaMatrytsiNaStovpets(double** Systema, int n, int Stovpets);

/*Виконує почленне додавання стовпця Stovpets2 до стовпця Stovpets1.*/
void DodavanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n);

/*Виконує почленне віднімання стовпця Stovpets2 від стовпця Stovpets1.*/
double* VidnimanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n);

/*Виконує приведення системи Systema із кількістю невідомих n до вигляду, коли кожен Xi виражений через інші невідомі.
Повертає матрицю заданого вигляду.*/
double** PryvedenniaSystemy(double** Systema, int n);

/*Знаходить суму модулів елементів вектора заданої довжини n.
Повертає суму.*/
double SumaModulivElementivRiadka(double* Riadok, int n);

/*Знаходить m-норму матриці Systema розмірності n*n. Повертає m-норму.*/
double ZnahodzhenniaNormy(double** Systema, int n);

/*Обчислює константу завершення ітерацій. Повертає константу завершення.*/
double ObchyslenniaKonstantyZavershennia(double q, double eps);

/*Виділяє із приведеної системи стовпець із номером m. Повертає стовпець.*/
double* VydilenniaStovptsia(double** Systema, int n, int m);

/*Обчислює чергове наближення за відомими матрицею Systema із розмірністю n*n, попереднім наближенням NablyzhenniaPoperednie.
Повертає вектор наближень.*/
double* ObchyslenniaNablyzhennia(double** Systema, int n, double* NablyzhenniaPoperednie);

/*Розв`язує СЛАР методом простої ітерації. Повертає вектор рішення.*/
double* RozvjazanniaMetodomIteratsiji(double** Systema, int n, double eps);