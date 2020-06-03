#pragma once
#include <iostream>
#include <fstream>
#include <string.h>
using namespace std;

// розміри полів у записах файлу 
const int len_name = 10;			// розмір поля для назви іграшки
const int len_code = 5;			// розмір поля для коду іграшки
const int len_price = 5;			// розмір поля для ціни іграшки
const int len_n = 5;				// розмір поля для кількості іграшок
const int len = len_name + len_code + len_price + len_n; // розмір одного запису   

// опис класу
class Toys {

// інтерфейс класу
public:
		Toys (int len_name = 10) { name = new char[len_name + 1]; code = 0; price = 0; n = 0; } // конструктор
		~ Toys ( ) {delete [] name; } //деструктор

		void  SetName(const char*); 	// запис назви іграшки
		void  SetCode(const char*);  // запис коду іграшки
		void  SetPrice(const char*);  // запис ціни іграшки
		void  SetN(const char*);       // запис кількості іграшок

		// отримання даних про іграшку
		char*  GetName( ) const {return name; } 
		int  GetCode ( ) const {return code; } 
		float  GetPrice ( )const {return price; } 
		int  GetN ( ) const {return n; } 

		void  Print( ) const ; 		// друк даних про іграшку
		bool  FindName(const  char*) const;	 // знаходження іграшки за іменем	
		bool  FindCode(const  int) const;    // знаходження іграшки за кодом
		
// дані класу

private:
	char *name;		// назва іграшки
	int code;		// код
	float price; 		// ціна
	int n;			// кількість іграшок з даним ім’ям
};
// реалізація частини методів класу
	void  Toys::SetName(const char* buf) {
		strncpy(name, buf, len_name);
		name[len_name] = '\0';
	} 
	void  Toys::SetCode(const char* buf) {
		
	 	code  = atoi(buf+ len_name); 
	} 
	void  Toys::SetPrice(const char* buf) {
		price  = atof(buf +len_name + len_code);
	}
	void  Toys::SetN(const char* buf) {
		n  = atoi(buf+ len_name+ len_code+ len_price);
	} 
 
	void  Toys::Print( ) const{
		cout<< '\n' << GetName();
		cout<< " - " << GetCode();
		cout<< " - " << GetPrice();
		cout<< " - " << GetN();
		cout<< '\n';
	}
	bool  Toys::FindName (const  char* someName) const{
		if ((strstr(name, someName) && (name[strlen(someName)] == ' ')))
			return true;
		else 	return false;
	} 
bool  Toys::FindCode (const  int someCode) const{
		if (someCode == code)
			return true;
		else	return false;
	} 

