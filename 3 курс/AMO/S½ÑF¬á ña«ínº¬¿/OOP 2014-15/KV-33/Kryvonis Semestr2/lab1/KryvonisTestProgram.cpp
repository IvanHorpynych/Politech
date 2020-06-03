/*!
* file: Lab_1.cpp
* test program for Multistring class
* written: 10/02/2015
* Copyright (c) 2012 by A. Kryvonis
*/
#include "MultiString.h"
#define SZ 5



int main()
{
	char *f1[] = {"Petya","Kolia", "Artem", "Kostia", "Ania"};
	char *f2[] = {"1","2", "3", "4", "5","Kostia"};
	char *f3[] = {"Dima","Sasha", "Kirill","Artem","Sasha"};



	MultiString *mults = new MultiString();

	MultiString *mt2 = new MultiString(SZ);
	for(int i = 0; i<SZ;i++){
		mt2->SetAt(i,f1[i]);
	}

	cout<<"Print mt3:"<<endl;
	MultiString *mt3 = new MultiString(*mt2);
	mt3->PrintAllStr();
	cout<<endl;

	MultiString *mt4 = new MultiString(6);
	for(int i = 0; i < mt4->GetLength();i++){
		mt4->SetAt(i,f2[i]);
	}
	cout<<"Print mt4:"<<endl;
	mt4->PrintAllStr();
	cout<<endl;


	cout<<"Print mt4 after MergeMultiStringExclusive(*mt3):"<<endl;
	mt4 = mt4->MergeMultiStringExclusive(*mt3);
	mt4->PrintAllStr();
	cout<<endl;

	MultiString *mt5 = new MultiString(SZ);
	for(int i = 0; i<SZ;i++){
		mt5->SetAt(i,f3[i]);
	}
	(*mt2)+=(*mt5);
	cout << "Print mt2 after mt2+=mt5):" << endl;
	mt2->PrintAllStr();

	

	

	return 0;
}

