#include "cArray.h"


int main(){
	cArray *vec = new cArray;
	vec->SetSize(3);
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	vec->SetAt(10, 0);
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	vec->SetAt(11, 1);
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	vec->SetAt(12, 2);
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	vec->Add(13);
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	std::cout << "Vector contains : ";
	for (int i = 0; i < vec->GetCount(); ++i){
		std::cout << vec->GetAt(i) << " ";
	}

	std::cout << std::endl;
	std::cout << std::endl;

	vec->FreeExtra();
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	if (vec->IsEmpty() != 0){
		std::cout << "Vector is not empty ! " << std::endl;
	}
	else
	{
		std::cout << "Vector is empty ! " << std::endl;
	}

	std::cout << std::endl;
	std::cout <<"GetAt(0) = " << vec->GetAt(0) << std::endl;

	cArray *ar = new cArray;
	ar->SetSize(2);
	ar->SetAt(20, 0);
	ar->SetAt(35, 1);

	vec->Append(ar);
	std::cout << "Vector contains : ";
	for (int i = 0; i < vec->GetCount(); ++i){
		std::cout << vec->GetAt(i) << " ";
	}
	std::cout << std::endl;
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;

	vec->FreeExtra();
	std::cout << std::endl;
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;

	vec->RemoveAt(5);
	std::cout << "Vector contains : ";
	for (int i = 0; i < vec->GetCount(); ++i){
		std::cout << vec->GetAt(i) << " ";
	}
	std::cout << std::endl;
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;

	cArray *ar2 = new cArray;
	ar2->SetSize(2);
	ar2->SetAt(50, 0);
	ar2->SetAt(60, 1);

	std::cout << std::endl;
	vec->Copy(ar2);
	std::cout << "Vector contains : ";
	for (int i = 0; i < vec->GetCount(); ++i){
		std::cout << vec->GetAt(i) << " ";
	}
	std::cout << std::endl;
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;

	vec->FreeExtra();
	std::cout << std::endl;
	std::cout << "GetSize() returns " << vec->GetSize() << std::endl;
	std::cout << "GetCount()returns " << vec->GetCount() << std::endl;
	std::cout << "GetUpperBound() returns " << vec->GetUpperBound() << std::endl;
	std::cout << std::endl;
	return 0;

}