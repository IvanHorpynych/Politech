#pragma once
#include <iostream>
#include <fstream>
#include <string.h>
using namespace std;

// ������ ���� � ������� ����� 
const int len_name = 10;			// ����� ���� ��� ����� �������
const int len_code = 5;			// ����� ���� ��� ���� �������
const int len_price = 5;			// ����� ���� ��� ���� �������
const int len_n = 5;				// ����� ���� ��� ������� �������
const int len = len_name + len_code + len_price + len_n; // ����� ������ ������   

// ���� �����
class Toys {

// ��������� �����
public:
		Toys (int len_name = 10) { name = new char[len_name + 1]; code = 0; price = 0; n = 0; } // �����������
		~ Toys ( ) {delete [] name; } //����������

		void  SetName(const char*); 	// ����� ����� �������
		void  SetCode(const char*);  // ����� ���� �������
		void  SetPrice(const char*);  // ����� ���� �������
		void  SetN(const char*);       // ����� ������� �������

		// ��������� ����� ��� �������
		char*  GetName( ) const {return name; } 
		int  GetCode ( ) const {return code; } 
		float  GetPrice ( )const {return price; } 
		int  GetN ( ) const {return n; } 

		void  Print( ) const ; 		// ���� ����� ��� �������
		bool  FindName(const  char*) const;	 // ����������� ������� �� ������	
		bool  FindCode(const  int) const;    // ����������� ������� �� �����
		
// ��� �����

private:
	char *name;		// ����� �������
	int code;		// ���
	float price; 		// ����
	int n;			// ������� ������� � ����� ����
};
// ��������� ������� ������ �����
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

