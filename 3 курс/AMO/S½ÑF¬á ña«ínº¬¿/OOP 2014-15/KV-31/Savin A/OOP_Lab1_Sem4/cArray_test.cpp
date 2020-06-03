#include "cArray.h"

int main()
{
	cArray arr;
	cArray arr2;
	printf("================================================================================");
	printf("Values after constructing an object:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	int ssize = 6;
	arr.SetSize(6);
	printf("Values after setting size to %d:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", ssize, arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr.Add(1);
	arr.Add(2);
	arr.Add(3);
	arr.Add(4);
	arr.Add(5);
	arr.Add(6);
	arr.Add(7);
	arr.print();
	printf("Values after adding 7 items:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr.SetAt(10, 2);
	arr.SetAt(20, 4);
	arr.print();
	printf("Values after setting items with indexes 2 and 4 to different values:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr2.SetAt(6, 0);
	arr2.SetAt(4, 1);
	arr2.SetAt(7, 2);
	arr2.SetAt(2, 3);
	arr2.SetAt(3, 4);
	arr2.Add(14);
	arr2.Add(18);
	arr.print();
	arr.Append(&arr2);
	arr2.print();
	arr.print();
	printf("Values after appending 2 arrays:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr.SetSize(5);
	arr.Copy(&arr2);
	arr.print();
	printf("Values after copying arr2 to arr:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr.InsertAt(9, 3);
	arr.print();
	printf("Values after inserting item at 3rd index:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr.RemoveAt(3);
	arr.print();
	printf("Values after deleting item at 3rd index:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	arr.print();
	printf("Value of item with index 2 that was recieved with redifinition - %d\n", arr[2]);
	printf("================================================================================");
	arr.~cArray();
	printf("Values after removing all existing items:\nAllowed size - %d\nMax allowed index - %d\nNumber of elements - %d\n", arr.Getsize(), arr.Getupperbound(), arr.Getcount());
	if (arr.IsEmpty() == true)
		printf("Emptiness - True\n"); else
		printf("Emptiness - False\n");
	printf("================================================================================");
	return 1;
}