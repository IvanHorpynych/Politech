/************************************************************************
*file: Test.cpp
*author: Gnedoj D.
*written: 11/02/2015
*last modified: 11/02/2015
*************************************************************************/
#include "lab_1_MultyString.h"

int main(){
	
	MultiString *ms1 = new MultiString();
	//MultiString *ms2 = new MultiString();
	//MultiString *ms3 = new MultiString(3);
	ms1->Add("1_0");
	ms1->Add("1_1");
	ms1->Add("1_2");
	
	MultiString *ms2 = new MultiString(*ms1);
	ms2->Add("test");
	for (int i = 0; i < ms2->GetLength(); i++)	ms2->PrintStr(i);
	printf("\n");
	for (int i = 0; i < ms1->GetLength(); i++)	ms1->PrintStr(i);
	/*
	ms2->Add("2_0");	
	ms2->Add("2_1");
	ms2->Add("2_2");
	ms2->Add("2_3");
	ms2->Add("2_4");
	ms2->Add("2_5");
	
	ms3->Add("3_0");
	ms3->Add("2_1");
	
	for (int i = 0; i < ms1->GetLength(); i++)	ms1->PrintStr(i);

	ms1->SetAt(1, "111111");
	ms1->SetAt(8, "888888");

	for (int i = 0; i < ms1->GetLength(); i++)
		ms1->PrintStr(i);

	MultiString *ms4 = new MultiString(*ms3);

	printf("OPERATOR [](1)  %s \n", (*ms1)[1]);
	printf("OPERATOR [](18) %s \n", (*ms1)[18]);

	printf("Find (111111) on position %d \n", (*ms1).Find("111111"));

	printf("ms4 \n");
	for (int i = 0; i < ms4->GetLength(); i++)
		ms4->PrintStr(i);
	
	*ms1 += *ms4;
	printf("ms1 after OPERATOR+=(ms4) \n");
	for (int i = 0; i < ms1->GetLength(); i++)
		ms1->PrintStr(i);

	*ms1 = *ms2;
	printf("ms1 after OPERATOR=(ms2) \n");
	for (int i = 0; i < ms1->GetLength(); i++)
		ms1->PrintStr(i);
	
	printf("MergeMultistringExclusive \n");
	MultiString ms5 = ms2->MergeMultiStringExclusive(*ms3);
	for (int i = 0; i < ms5.GetLength(); i++)
		ms5.PrintStr(i);
		*/
	return 0;
}