// Lab_1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "MultiString.h"
#define SZ 5



int _tmain(int argc, _TCHAR* argv[])
{
	char *data[] = {"Dima","Sasha", "Artem", "Kostia", "Ania"};
	char *data_add[] = {"AAA","BBB", "CCC", "DDD", "EEE"};
	char *data2[] = {"Dima","Sasha", "Kirill"};



	MultiString *test = new MultiString();

	MultiString *test2 = new MultiString(SZ);
	for(int i = 0; i<SZ;i++){
		test2->SetAt(i,data[i]);
	}

	cout<<"Print test3:"<<endl;
	MultiString *test3 = new MultiString(*test2);
	test3->PrintAllStr();
	cout<<endl;

	MultiString *test4 = new MultiString(3);
	for(int i = 0; i < test4->GetLength();i++){
		test4->SetAt(i,data2[i]);
	}
	cout<<"Print test4:"<<endl;
	test4->PrintAllStr();
	cout<<endl;


	cout<<"Print test4 befor MergeMultiStringexclusive(*test3):"<<endl;
	test4 = test4->MergeMultiStringexclusive(*test3);
	test4->PrintAllStr();
	cout<<endl;

	MultiString *test5 = new MultiString(SZ);
	for(int i = 0; i<SZ;i++){
		test5->SetAt(i,data_add[i]);
	}
	(*test2)+=(*test5);
	test2->PrintAllStr();	

	return 0;
}

