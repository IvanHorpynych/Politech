/*!
* file: OOP_lab1_methods.cpp
* Definition of methods cArray
* written: 19/02/2015
* Copyright (c) 2015 Chernysh A.A.
*/
#include "OOP_lab1.h"
#include <conio.h>
#include <iostream>
using namespace std;

cArray::cArray(){
	//size = 0;
	cArr = new int[size];
}
cArray::~cArray(){
	size = 0;
	count = 0;
	//delete this->cArr;
	//free(cArr);
}
int cArray::Getsize() const{
	 return size;
}
int cArray::Getcount() const{
	return count;
}
int cArray::Getupperbound() const{
	return count-1;
}
bool cArray::IsEmpty() const{
	if (count == 0) return true;
	return false;
}
void cArray::SetSize(int n){
	size = n;
	if (size < count) count = size;
}
void cArray::FreeExtra(){
	this->count = this->Getupperbound();
}
void cArray::RemoveAll(){
	size = 0;
	count = 0;
}
void cArray::SetAt(int n, int idx){
	if ((idx > size) || (idx < 0)){
		cout << "Index out of range!!! Change index and repeat please" << endl;
		return;
	}
	else if (idx == size){
		size += GROWBY;
	}
	cArr[idx] = n;
	count++;
}
int cArray::GetAt(int idx) const{
	if ((idx > count) || (idx < 0)){
		cout << "Index out of range!!! Change index and repeat please" << endl;
		return NULL;
	}
	return cArr[idx];
}
void cArray::Add(int number){
	if (count == size){
		this->size += GROWBY;
	}
	this->cArr[count] = number;
	this->count++;
	return;
}
int& cArray::operator[](int idx){
	return cArr[idx];
}
void cArray::Append(cArray *ArrObj){
	for (int i = 0; i < ArrObj->Getcount(); i++)
		this->Add(ArrObj->GetAt(i));
}
void cArray::Copy(cArray *ArrObj){
	this->SetSize(ArrObj->Getsize());
	for (int i = 0; i < ArrObj->Getcount(); i++)
		this->SetAt(ArrObj->GetAt(i),i);
	
}
void cArray::InsertAt(int n, int idx){
	if (idx <= count-1){
		if (count + 1 > size) size += GROWBY;
		for (int i = count - 1; i>=idx; i--)
			cArr[i+1] = cArr[i];
		cArr[idx] = n;
		count++;
	}
	else if (idx > size) this->SetAt(n,idx);
}
void cArray::RemoveAt(int idx){
	for (int i = idx; i < this->count; i++)
		cArr[i] = cArr[i+1];
	count--;
}
