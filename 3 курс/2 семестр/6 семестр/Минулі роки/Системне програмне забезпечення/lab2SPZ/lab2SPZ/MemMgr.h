#ifndef _MEMMGR_H
#define _MEMMGR_H

#include <iostream>
#include <queue>
#include <vector>
#include <map>

#pragma warning(disable : 4018)

using namespace std;

typedef unsigned char uchar, *PUCHAR;
static uchar size_of_st = sizeof(size_t);
enum { defsize = 64*1024};

class MemMgr
{
private:

	vector<uchar*> m_begin;  // указатель на начало памяти
	MemMgr(const MemMgr&);
	MemMgr& operator = (const MemMgr&);
public:
	MemMgr();
	~MemMgr();
	void* alloc(size_t bytes);
	void* realloc( void*, size_t);
	void free(void*);
	void mem_dump();	
};
#endif
