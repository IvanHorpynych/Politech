#include "stdio.h"
#include "windows.h"
#include "string.h"

#define NALLOC 1024
typedef long align;

struct header {
	struct header *ptr;
	size_t size;
};

typedef struct header Header;

Header base;              // Empty list to get started.
Header *free_ptr = NULL;  // Start of the free list.  

void mem_free(void *addr) {
	Header *bp;
	Header *p;

	bp = (Header *)addr - 1;  // Pointer on header.

	for (p = free_ptr; !(bp > p && bp < p->ptr); p = p->ptr) {  // Freed block at start or end of arena.
		if (p >= p->ptr && (bp > p || bp < p->ptr))
			break;
	}

	if ((unsigned long *)bp + bp->size == p->ptr) {  // To high adjacent block.
		bp->size += p->ptr->size;
		bp->ptr = p->ptr->ptr;
	} else {
		bp->ptr = p->ptr;
	}

	if ((unsigned long *)p + p->size == bp) {  // To down adjacent block.
		p->size += bp->size;
		p->ptr = bp->ptr;
	} else {
		p->ptr = bp;
	}

	free_ptr = p;
}

Header *call_memory(size_t n_blocks) {
	HANDLE handle;
    unsigned char *ram_ptr; 	// Memory storage.
    Header *new_memory;
	
	handle = GetProcessHeap();

	if (handle == NULL) {
		printf("Invalid handle.\n");
		exit(1);
	}
	
	if (n_blocks < NALLOC) {
		n_blocks = NALLOC;
	}

	ram_ptr = (unsigned char *)HeapAlloc(handle, HEAP_ZERO_MEMORY, n_blocks * sizeof(unsigned long));

	new_memory = (Header *)ram_ptr;
	new_memory->size = n_blocks;
	mem_free((void *)(new_memory + 1));

	return free_ptr;
}

void *mem_alloc(size_t size) {
	Header *p, *prev_ptr;
	size_t n_blocks;

    n_blocks = (size + sizeof(unsigned long) - 1) / sizeof(unsigned long) + 2;

	if ((prev_ptr = free_ptr) == NULL ) {  // List is empty.
		prev_ptr = &base;
		free_ptr = prev_ptr;
		base.ptr = free_ptr;
		base.size = 0;
	}

	for (p = prev_ptr->ptr; ;prev_ptr = p, p = p->ptr) {
		if (p->size >= n_blocks) {
			if (p->size == n_blocks) { 
				prev_ptr->ptr = p->ptr; 
			} else {
				p->size -= n_blocks;
				(unsigned long *)p += p->size;
				p->size = n_blocks;
			}
			free_ptr = prev_ptr;
			return (void *)(p + 1);
		}
		
		if (p == free_ptr) {
			if ((p = call_memory(n_blocks)) == NULL) {
				return NULL;
			}
		}
	}
}

void *mem_realloc(void *addr, size_t size) {
	unsigned char *new_addr;
	size_t old_n_blocks = 0;
	size_t new_n_blocks = 0;
	Header *bp;
	
	if (addr == NULL) { return mem_alloc(size); }
	if (size == old_n_blocks* sizeof(unsigned long)) { return addr; }
	
	new_addr = mem_alloc(size);

	bp = (Header *)addr - 1;  // Pointer on header.
	old_n_blocks = bp->size;
    new_n_blocks = (size + sizeof(unsigned long) - 1) / sizeof(unsigned long) + 2;  // +2 blocks by 4 bytes for header.

	if (old_n_blocks > new_n_blocks) 
		memcpy(new_addr, addr, (new_n_blocks - 2) * sizeof(unsigned long));
	else
		memcpy(new_addr, addr, (old_n_blocks - 2) * sizeof(unsigned long));

	
	mem_free(addr);
	return (void *)new_addr;
}


void mem_dump() {
	Header *p = &base;

	do {
		printf("[ addr=%p] [size=%4.1d] [next=%p]\n", p, p->size, p->ptr);
		p = p->ptr;
	} while (p != &base);
}

int main() {
	void *ptr1, *ptr2, *ptr3, *ptr4;

	ptr1 = mem_alloc(2000);
    ptr2 = mem_alloc(2000);
	ptr3 = mem_alloc(4000);
	ptr4 = mem_alloc(105);
    mem_dump();

    printf("MEM_ALLOC\n");
	printf("[ptr=%p] [size=%d]\n", ptr1, ((Header *)ptr1 - 1)->size);
    printf("[ptr=%p] [size=%d]\n", ptr2, ((Header *)ptr2 - 1)->size);
    printf("[ptr=%p] [size=%d]\n", ptr3, ((Header *)ptr3 - 1)->size);
    printf("[ptr=%p] [size=%d]\n", ptr4, ((Header *)ptr4 - 1)->size);

	
	printf("MEM_REALLOC\n");
	ptr1 = mem_realloc(ptr1, 100);
	ptr2 = mem_realloc(ptr2, 1000);
	ptr3 = mem_realloc(ptr3, 8000);
	ptr4 = mem_realloc(ptr4, 5);

	printf("[ptr=%p] [size=%d]\n", ptr1, ((Header *)ptr1 - 1)->size);
    printf("[ptr=%p] [size=%d]\n", ptr2, ((Header *)ptr2 - 1)->size);
    printf("[ptr=%p] [size=%d]\n", ptr3, ((Header *)ptr3 - 1)->size);
    printf("[ptr=%p] [size=%d]\n", ptr4, ((Header *)ptr4 - 1)->size);

	printf("MEM_FREE\n");
	mem_free(ptr4);
	mem_free(ptr3);
    mem_free(ptr2);
    mem_free(ptr1);

	mem_dump();	

	return 0;
}
