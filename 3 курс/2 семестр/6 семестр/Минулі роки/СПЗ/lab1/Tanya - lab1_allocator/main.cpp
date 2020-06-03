/* file name     : main.cpp
 * programmer    : Oleg Trishchuk
 * first written : 14/10/2010
 * last modified : 14/10/2010
 * related files : allocator.h
 * purpose       : allocator test-drive
 */


#include <iostream>

//#define NDEBUG
#include <assert.h>

#include "memory.h"


using namespace std;


#define LINE_BREAK cout<<endl<<"________________________________________________________________________________"\
	<<endl<<endl;


int main()
{
	memory m;
	void *f;
	cout<<"Start"<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_alloc(1024)"<<endl;
	f = m.mem_alloc(1024);
	cout<<f<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_free("<<f<<")"<<endl<<endl;
	m.mem_free(f);
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_alloc(256)"<<endl;
	f = m.mem_alloc(256);
	cout<<f<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_realloc("<<f<<", 512)"<<endl;
	f = m.mem_realloc(f, 512);
	cout<<f<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_alloc(10)"<<endl;
	void *f1 = m.mem_alloc(10);
	cout<<f1<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_alloc(512)"<<endl;
	void *f2 = m.mem_alloc(512);
	cout<<f2<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"> mem_free(0xDEADBEEF)"<<endl;
	m.mem_free((void*) 0xDEADBEEF);
	cout<<endl<<endl;
	m.mem_dump();
	LINE_BREAK

	cout<<"End. Goodbye."<<endl;
	system("PAUSE");
	return 0;
}
