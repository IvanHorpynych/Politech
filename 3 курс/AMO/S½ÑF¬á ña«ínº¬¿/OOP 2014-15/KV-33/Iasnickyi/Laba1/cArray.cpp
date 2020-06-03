#include "cArray.h"


cArray::cArray(){
	size = data.size();
}


cArray::~cArray(){
	
}

int cArray::GetSize(){
	return data.capacity();
}


int cArray::GetCount(){
	return data.size();
}


bool cArray::IsEmpty()const{
	return data.empty();
}


int cArray::GetUpperBound()const{
	return data.size() - 1;
}


void cArray::SetSize(int nNewSize){
	 data.reserve(nNewSize);
}


int cArray::GetAt(int indx)const{
	if (indx < 0 || indx < size){
		std::cout << "Out of range !" << std::endl;
	}
	else
		return data[indx];
}


void cArray::SetAt(int n, int indx){
	if (indx < 0 || indx < size){
		std::cout << "Out of range !" << std::endl;
	}
	else
	{
		it = data.begin();
		data.insert(it + indx, n);
	}
}


void cArray::FreeExtra(){
	std::vector<int> (data).swap(data);
}


void cArray::RemoveAll(){
	data.erase(data.begin(), data.end());
}


void cArray::Add(int n){
	if (cArray::GetSize() == cArray::GetCount()){
		cArray::SetSize((cArray::GetSize() + GROWBY));
		data.push_back(n);
	}
	else
		data.push_back(n);
		
}


void cArray::Append(cArray *ar){
	if (cArray::GetSize() == cArray::GetCount()){
		cArray::SetSize((cArray::GetSize() + GROWBY));
		for (int i = 0; i < ar->GetSize(); ++i){
			data.push_back(ar->GetAt(i));
		}
	}
	else
	{
		for (int i = 0; i < ar->GetSize(); ++i)
			data.push_back(ar->GetAt(i));
	}
}


void cArray::Copy(cArray *ar){
	cArray::SetSize((cArray::GetSize() + GROWBY));
	for (int i = 0; i < ar->GetSize(); ++i)
		data.push_back(ar->GetAt(i));
}


void cArray::InsertAt(int n, int indx){
	if (indx < 0 || indx < size){
		std::cout << "Out of range !" << std::endl;
	}
	else
		cArray::SetAt(n, indx);
}


void cArray::RemoveAt(int indx){
	if (indx < 0 || indx < size){
		std::cout << "Out of range !" << std::endl;
	}
	else
		data.erase(data.begin() + indx);
}


int& cArray::operator[] (int indx){
	if (indx < 0 || indx < size){
		std::cout << "Out of range !" << std::endl;
	}
	else
		return data[indx];
}


