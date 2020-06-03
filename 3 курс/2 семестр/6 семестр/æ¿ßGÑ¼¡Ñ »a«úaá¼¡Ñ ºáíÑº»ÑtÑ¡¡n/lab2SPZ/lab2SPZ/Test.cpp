#include "MemMgr.h"
//MemMgr test
static MemMgr myMemMgr;
int main()
{
	size_t i = 7;
	//cout<<defsize<<endl;
	cout<<"alloc j (28 bytes)"<<endl;
	int* j = (int *) myMemMgr.alloc( i*sizeof(int) );
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"realloc j (84 bytes)"<<endl;
	j = (int *) myMemMgr.realloc( j, (i+i+i)*sizeof(int) );
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"alloc l (84 bytes)"<<endl;
	int* l = (int *) myMemMgr.alloc((i+i+i)*sizeof(int) );
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"free l (84 bytes)"<<endl;
	myMemMgr.free(l);
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"realloc j (84 bytes)"<<endl;
	j = (int *) myMemMgr.realloc( j, (i)*sizeof(int) );
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"alloc defsize (65536 bytes+100)"<<endl;
	uchar* p = (uchar *) myMemMgr.alloc(defsize+1);
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"realloc p = defsize - 500 (64536 bytes)"<<endl;
	p = (uchar *) myMemMgr.realloc( p, defsize - 500);
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"realloc p (65536 bytes)"<<endl;
	p = (uchar *) myMemMgr.realloc( p, defsize );
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"free p (65536 bytes)"<<endl;
	myMemMgr.free( (void *)p );
	myMemMgr.mem_dump();
	cout<<"\n"<<endl;
	cout<<"free j (84 bytes)"<<endl;
	myMemMgr.free( (void *)j );
	myMemMgr.mem_dump();
	cin.get();
	return 0;
}
