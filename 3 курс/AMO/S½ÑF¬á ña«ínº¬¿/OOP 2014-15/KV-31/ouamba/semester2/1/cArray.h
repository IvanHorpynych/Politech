#pragma once
#include <string.h>
#include <stdio.h>
#include <iostream>
#include <stdlib.h> // for rand()

 using namespace std ;

class cArray{

#define GROWBY 10
private:
	int mySize;
	int *vector;
	int maxIndex;

public:
	//створює порожній вектор.
	cArray(int size);
	~cArray(void);

		
	//повертає число элементів, які можуть бути розміщені у векторі..
	int GetSize() const;
	//повертає число элементів, розміщених у векторі.
	int GetCount() const;


	//повертає найбільший припустимий індекс вектора.
	int GetUpperBound();
	//перевірка вектора на порожнечу.
	bool IsEmpty()const;
	//зміна розміру вектора.
	void SetSize( int nNewSize );



		
	
	//звільняє пам’ять вище найбільшого припустимого індексу
	void FreeExtra();

	//видаляє всі елементи вектора (найбільший припустимий індекс - 0).
	void RemoveAll();


	//повертає елемент c індексом indx.
	int GetAt(int indx)const;
	//установлює значення елемента c індексом indx рівним n.
	void SetAt(int n, int indx);

	
	//додає елемент зі значенням n у кінець вектора (змінює значення найбільшого припустимого індексу).
	//Якщо вільних позицій у векторі немає – його розмір збільшується на GROWBY.
	void Add(int n);
	//додає елементи ar у кінець вектора. При необхідності розмір вектора збільшується на значення кратне GROWBY
	void Append(cArray *ar);
	//копіює ar у поточний вектор зі змінюючи належним чином його розмір на значення кратне GROWBY.
	void Copy(cArray *ar);

	
	//вставляє елемент n у позицію з індексом indx (змінює значення найбільшого припустимого індексу).
	void InsertAt(int n, int indx);
	//видаляє елемент у позиції з індексом indx (змінює значення найбільшого припустимого індексу).
	void RemoveAt(int indx);

	
	//втановлює/повертає значення елемента в позиції з індексом indx.
	int& operator [](int indx);

	//дозволяе порглядати вектор
	void Watch();

};
