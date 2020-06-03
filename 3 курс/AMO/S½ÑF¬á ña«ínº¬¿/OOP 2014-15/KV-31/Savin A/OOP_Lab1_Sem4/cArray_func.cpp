#include "cArray.h"

cArray::cArray() //����������� ������� ������� � ����������� ��������� � 10 ��������
{
	size = 0;
	allowed_size = 10;
	data_arr = new int[allowed_size];
}

cArray::~cArray() //����������, ���������� ������� �������� ���� ���������
{
	RemoveAll();
}

int cArray::Getcount() const //�������� ������� ������ �������
{
	return size;
}

int cArray::Getsize() const //�������� ����������� ��������� ������
{
	return allowed_size;
}

int cArray::Getupperbound() const //���������� ���������� ��������� ������
{
	return size - 1;
}

bool cArray::IsEmpty() const //�������� �� �������
{
	if (size == 0) return true;
	else return false;
}

void cArray::SetSize(int nnewsize) //������ ������ �������
{
	if (nnewsize < size) { //���� ������� ������ ������� ��������� ��������
		data_arr = (int *)realloc(data_arr, nnewsize*sizeof(int)); //�������������� ���������� ������
		size = nnewsize; //� ������ ������� ���������� �������� ��������
	}
	allowed_size = nnewsize;
}

void cArray::FreeExtra()
{
	data_arr = (int *)realloc(data_arr, (size + 1)*sizeof(int)); //����������� ��� �������������� ������ ���� ������������� �������
}

void cArray::RemoveAll() //������� ��� ��������
{
	data_arr = (int *)realloc(data_arr, 0);
	size = 0;
	allowed_size = 0;
}

int cArray::GetAt(int indx) const //�������� ������� � �������� ��������
{
	return data_arr[indx];
}

void cArray::print() //������ �������
{
	if (size == 0) {
		printf("Array is empty\n");
		return;
	}
	for (int i = 0; i < size; i++){
		printf("%d ", data_arr[i]);
	}
	printf("\n");
}

void cArray::SetAt(int n, int indx) //��������� �������� �������� �� �������
{
	if (indx > size) return; //���� ������ ��������� ����������� ��������� (�������� � ��� ������ 5, ������������ ������ 4, � �������� ����� �� 7) �� ������������
	if ((indx == size) && (size == allowed_size)) { //���� ������ ��������� � ����������� ��������� � � ����������� �����������
		return;
	}
	if (indx == size) {
		size++; //����������� ������, ���� ������ ������ � ����������� ���������
	}
	data_arr[indx] = n;
}

void cArray::Add(int n)
{
	if (size == allowed_size) { //���� ������ ����� ����������� ������������ �������� ������
		allowed_size = allowed_size + GROWBY;
		data_arr = (int *)realloc(data_arr, allowed_size*sizeof(int));
	}
	data_arr[size] = n;
	size++;
}

void cArray::Append(cArray *ar)
{
	for (int i = 0; i < ar->size; i++) {
		Add(ar->data_arr[i]); //���������� ������� ����������
	}
}

void cArray::Copy(cArray *ar)
{
	if (ar->size > allowed_size) {//���� ������ ������� ������� ���������� ������ ����������� ������������ �������� ������
		allowed_size = allowed_size + GROWBY;
		data_arr = (int *)realloc(data_arr, allowed_size*sizeof(int));
	}
	for (int i = 0; i < ar->size; i++) {
		SetAt(ar->data_arr[i], i); //������������� �������� �� ��������
	}
	size = ar->size; //�������� ������
	FreeExtra(); //������� ������ �������� �� �����
}

void cArray::InsertAt(int n, int indx) //������� �� �������
{
	if (indx > size - 1) return;
	if ((size - allowed_size) == 0) { //���� ������ ����� ����������� ������������ �������� ������
		allowed_size = allowed_size + GROWBY;
		data_arr = (int *)realloc(data_arr, allowed_size*sizeof(int));
	}
	size++; //����������� ������
	for (int i = size - 2; i >= indx; i--) //�������� ��� �������� ����� ����� �������
	{
		SetAt(data_arr[i], i + 1);
	}
	SetAt(n, indx); //������������� �������� �� �������
}

void cArray::RemoveAt(int indx) //������� �� �������
{
	if (indx > size - 1) return; 
	for (int i = indx; i < size - 1; i++) //�������� �� �����
	{
		SetAt(data_arr[i + 1], i);
	}
	size--; //��������� ������
	FreeExtra(); //������� ������
}

int& cArray::operator[](int indx) //�������������� �������� ��������
{
	int i = 0;
	if (indx > size - 1) return i;
	return data_arr[indx];
}