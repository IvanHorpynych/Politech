#pragma once


#include <list>

#include "block.h"


using namespace std;


#define BLOCK_SIZE 1024


class memory
{
	public:
		memory(void);
		~memory(void);
		void *mem_alloc(size_t size);
		void *mem_realloc(void *addr, size_t size);
		void mem_free(void *addr);
		void mem_dump(void);

	private:
		list<block> blocks;  // memory blocks
		typedef list<block>::iterator iterator;

		void *physical;  // physical memory block, used for real data

		void joinEmpty(iterator element); // join empty neighbors
};

