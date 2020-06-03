#include "MultiString.h"

int main()
{
	MultiString *ms1 = new MultiString;
	MultiString *ms2 = new MultiString(3);

	ms1->Adding_str("Shylov");
	ms1->Adding_str("Denis");
	ms2->Adding_str("Alexandrovich");

	MultiString ms3;// = ms1;

	printf("1 ");
	ms1->PrintStr(0);
	ms1->PrintStr(1);
	ms2->PrintStr(3);

	printf("\n2 ");
	ms3.PrintStr(1);

	printf("\n3 ");
	*ms1 += *ms2;
	ms1->PrintStr(0);
	ms1->PrintStr(1);
	ms1->PrintStr(5);

	printf("\n ");
	ms2 = ms1;
	ms2->PrintStr(0);

	printf("\n ");
	if ((ms3)[1] != NULL)	printf((ms3)[1]);

	printf("\n ");
	printf("%d", ms1->Find("Alexandrovich"));

	printf("\n ");
	ms1->SetAt(3, "Yo");
	ms1->PrintStr(3);

	printf("\n ");
	printf("%d", ms1->GetLength());

	printf("\n ");
	ms1->MergeMultiStringExclusive(*ms2);
	for (int i = 0; i < ms1->GetLength(); i++)
		ms1->PrintStr(i);
	printf("\n");
}