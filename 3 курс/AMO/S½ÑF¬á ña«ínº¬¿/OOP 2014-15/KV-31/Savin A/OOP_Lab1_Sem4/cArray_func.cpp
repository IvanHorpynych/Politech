#include "cArray.h"

cArray::cArray() //конструктор пустого объекта с разрешенным размерром в 10 значений
{
	size = 0;
	allowed_size = 10;
	data_arr = new int[allowed_size];
}

cArray::~cArray() //деструктор, использует функцию удаления всех элементов
{
	RemoveAll();
}

int cArray::Getcount() const //получает текущий размер массива
{
	return size;
}

int cArray::Getsize() const //получает максимально возможный размер
{
	return allowed_size;
}

int cArray::Getupperbound() const //возвращает наибольший доступный индекс
{
	return size - 1;
}

bool cArray::IsEmpty() const //проверка на пустоту
{
	if (size == 0) return true;
	else return false;
}

void cArray::SetSize(int nnewsize) //задает размер массива
{
	if (nnewsize < size) { //если текущий размер массива превышает заданный
		data_arr = (int *)realloc(data_arr, nnewsize*sizeof(int)); //переопределяем выделенную память
		size = nnewsize; //и размер массива становится заданным размером
	}
	allowed_size = nnewsize;
}

void cArray::FreeExtra()
{
	data_arr = (int *)realloc(data_arr, (size + 1)*sizeof(int)); //освобождает всю неиспользуемую память выше максимального индекса
}

void cArray::RemoveAll() //удаляем все элементы
{
	data_arr = (int *)realloc(data_arr, 0);
	size = 0;
	allowed_size = 0;
}

int cArray::GetAt(int indx) const //получаем элемент с заданным индексом
{
	return data_arr[indx];
}

void cArray::print() //печать массива
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

void cArray::SetAt(int n, int indx) //установка значения элемента по индексу
{
	if (indx > size) return; //если индекс превышает максимально доступный (например у нас размер 5, максимальный индекс 4, а вставить хотим на 7) то возвращаемся
	if ((indx == size) && (size == allowed_size)) { //если индекс совпадает с максимально доступным и с максимально разрешенным
		return;
	}
	if (indx == size) {
		size++; //увеличиваем размер, если индекс совпал с максимально доступным
	}
	data_arr[indx] = n;
}

void cArray::Add(int n)
{
	if (size == allowed_size) { //если размер равен максимально разрешенному выделяем памяти
		allowed_size = allowed_size + GROWBY;
		data_arr = (int *)realloc(data_arr, allowed_size*sizeof(int));
	}
	data_arr[size] = n;
	size++;
}

void cArray::Append(cArray *ar)
{
	for (int i = 0; i < ar->size; i++) {
		Add(ar->data_arr[i]); //используем функцию добавления
	}
}

void cArray::Copy(cArray *ar)
{
	if (ar->size > allowed_size) {//если размер массива который копируется больше максимально разрешенного выделяем памяти
		allowed_size = allowed_size + GROWBY;
		data_arr = (int *)realloc(data_arr, allowed_size*sizeof(int));
	}
	for (int i = 0; i < ar->size; i++) {
		SetAt(ar->data_arr[i], i); //устанавливаем значения по индексам
	}
	size = ar->size; //изменяем размер
	FreeExtra(); //удаляем лишние элементы из конца
}

void cArray::InsertAt(int n, int indx) //вставка по индексу
{
	if (indx > size - 1) return;
	if ((size - allowed_size) == 0) { //если размер равен максимально разрешенному выделяем памяти
		allowed_size = allowed_size + GROWBY;
		data_arr = (int *)realloc(data_arr, allowed_size*sizeof(int));
	}
	size++; //увеличиваем размер
	for (int i = size - 2; i >= indx; i--) //сдвигаем все элементы после этого индекса
	{
		SetAt(data_arr[i], i + 1);
	}
	SetAt(n, indx); //устанавливаем значение по индексу
}

void cArray::RemoveAt(int indx) //удаляем по индексу
{
	if (indx > size - 1) return; 
	for (int i = indx; i < size - 1; i++) //сдвигаем всё влево
	{
		SetAt(data_arr[i + 1], i);
	}
	size--; //уменьшаем размер
	FreeExtra(); //удаляем лишнее
}

int& cArray::operator[](int indx) //переопределяем оператор скобочек
{
	int i = 0;
	if (indx > size - 1) return i;
	return data_arr[indx];
}