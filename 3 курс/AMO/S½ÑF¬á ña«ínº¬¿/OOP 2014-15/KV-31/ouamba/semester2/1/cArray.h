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
	//������� ������� ������.
	cArray(int size);
	~cArray(void);

		
	//������� ����� ��������, �� ������ ���� ������� � ������..
	int GetSize() const;
	//������� ����� ��������, ��������� � ������.
	int GetCount() const;


	//������� ��������� ����������� ������ �������.
	int GetUpperBound();
	//�������� ������� �� ���������.
	bool IsEmpty()const;
	//���� ������ �������.
	void SetSize( int nNewSize );



		
	
	//������� ������ ���� ���������� ������������ �������
	void FreeExtra();

	//������� �� �������� ������� (��������� ����������� ������ - 0).
	void RemoveAll();


	//������� ������� c �������� indx.
	int GetAt(int indx)const;
	//���������� �������� �������� c �������� indx ����� n.
	void SetAt(int n, int indx);

	
	//���� ������� � ��������� n � ����� ������� (����� �������� ���������� ������������ �������).
	//���� ������ ������� � ������ ���� � ���� ����� ���������� �� GROWBY.
	void Add(int n);
	//���� �������� ar � ����� �������. ��� ����������� ����� ������� ���������� �� �������� ������ GROWBY
	void Append(cArray *ar);
	//����� ar � �������� ������ � ������� �������� ����� ���� ����� �� �������� ������ GROWBY.
	void Copy(cArray *ar);

	
	//�������� ������� n � ������� � �������� indx (����� �������� ���������� ������������ �������).
	void InsertAt(int n, int indx);
	//������� ������� � ������� � �������� indx (����� �������� ���������� ������������ �������).
	void RemoveAt(int indx);

	
	//���������/������� �������� �������� � ������� � �������� indx.
	int& operator [](int indx);

	//�������� ���������� ������
	void Watch();

};
