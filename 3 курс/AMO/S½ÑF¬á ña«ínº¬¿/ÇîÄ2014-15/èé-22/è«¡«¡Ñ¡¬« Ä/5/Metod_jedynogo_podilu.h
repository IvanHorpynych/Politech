#pragma once
#include <stdio.h>
#include <malloc.h>
#include <string.h>

/*Перевіряє рядок, заданий масивом чисел на наявність головного елемента, заданого другим параметром-індексом, рівного нулю.
Повертає True в разі наявності такого елемента. Повертає False в разі відсутності такого елемента.*/
bool PerevirkaGolovnogo(double* Rivniannia, int Index);

/*Множить рядок, починаючи з позиції Index до позиції n на величину Mnozhnyk.*/
void MnozhenniaRiadka(double* Rivniannia, int Index, int n, double Mnozhnyk);

/*Почленно віднімає рядок Vidjemnyk від рядка Zmenshuvane. Довжина рядка задається n.*/
void VidnimanniaPochlenne(double* Zmenshuvane, double* Vidjemnyk, int n);

/*Виконує пошук першого рівняння з ненульовим головним елементом, заданим позицією Index,
починаючи з позиції Nomer, у системі рівнянь Systema з числом рівнянь n.
Повертає ноиер рівняння.*/
int PoshukRivnianniaZNenuliovymGolovnymElementom(int Index, int Nomer, double** Systema, int n);

/*Виконує почленний обмін вмістом двох рядків Riadok1 і Riadok2 з довжиною n.*/
void ObminVmistom(double* Riadok1, double* Riadok2, int n);

/*Виконує обнулення стовпця системи рівнянь Systema нижче головного елемента, що стоїть на позиції Index.*/
void ObnulenniaStovptsia(double** Systema, int Index, int KilkistRivnian, int n);

/*Виконує обнулення частини системи рівнянь під головною діагоналлю.*/
void ObnulenniaNyzhniojiChastyny(double** Systema, int KilkistRivnian, int n);

/*Виконує множення стовпця із номером Index системи рівнянь Systema на деякий елемент.*/
void MnozhenniaStovptsia(double** Systema, int Index, double Mnozhnyk);

/*Виконує віднімання від елемента рядка, що стоїть на позиції n, всіх елементів рядка, що стоять між елементом Index і елементом, що стоїть на позиції n.*/
void VidnimanniaVidOstanniogo(double* Rivniannia, int Index, int n);

/*Виконує зворотній хід метода Гаусса.*/
double* ZvorotnijHid(double** Systema, int KilkistRivnian, int n);