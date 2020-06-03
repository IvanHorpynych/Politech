/* file name     : allocator.cpp
 * programmer    : Oleg Trishchuk
 * first written : 14/10/2010
 * last modified : 14/10/2010
 * related files : allocator.h, memory.h
 * purpose       : allocator realization
 */


#include "allocator.h"


using namespace std;


myAllocator::myAllocator(void)
{
}


myAllocator::~myAllocator(void)
{
}

void* myAllocator::mem_alloc(size_t size)
{
	// FIXME
	return (void *)NULL;
}

void* myAllocator::mem_realloc(void *addr, size_t size)
{
	// FIXME
	return (void *)NULL;
}

void myAllocator::mem_free(void *addr)
{
}
