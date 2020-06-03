/* file name     : allocator.h
 * programmer    : Oleg Trishchuk
 * first written : 14/10/2010
 * last modified : 14/10/2010
 * related files : allocator.cpp, memory.h
 * purpose       : allocator definition
 */


#pragma once


#include "memory.h"


class myAllocator
{
	public:
		myAllocator(void);
		~myAllocator(void);

		void* mem_alloc(size_t size);
		void* mem_realloc(void *addr, size_t size);
		void mem_free(void *addr);
	private:
};

