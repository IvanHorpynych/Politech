#ifndef MEMALLOC_H
#define MEMALLOC_H

#include <stdlib.h>
#define MEM_SIZE 1024*1024//one megabyte

typedef struct _MemoryDescriptorListNode
{
    size_t blockSize;
    struct _MemoryDescriptorListNode * prevNode;
    struct _MemoryDescriptorListNode * nextNode;
} ListNode;

void * mem_alloc(size_t size);
void * mem_realloc(void * addr, size_t newSize);
void mem_free(void * addr);
void mem_dump();
#endif // MEMALLOC_H
