#include <iostream>
#include <iomanip>

#include "memory.h"


using namespace std;


memory::memory(void)
{
	physical = new char[BLOCK_SIZE];  // allocate physical block
	// this whole block is free

	block *newBlock = new block;
	newBlock->setUsed(false);
	newBlock->setStart((size_t) physical);
	newBlock->setSize(BLOCK_SIZE);
	blocks.insert(blocks.begin(), *newBlock);
}


memory::~memory(void)
{
	delete[] physical;  // delete physical block
}


void* memory::mem_alloc(size_t size)
{
	iterator it = blocks.begin();
	size_t retval = 0;

	while (it != blocks.end())  // searching for suitable block
	{
		if (it->getUsed() || (it->getSize() < size))
		{
			++it;
			continue;
		}

		if (it->getSize() > size)  // block is bigger, split it
		{
			block *newBlock = new block;
			newBlock->setUsed(true);
			newBlock->setStart(it->getStart());
			newBlock->setSize(size);
			blocks.insert(it, *newBlock);

			it->setStart(it->getStart() + size);
			it->setSize(it->getSize() - size);

			retval = newBlock->getStart();
		}
		else  // block is suitable
		{
			it->setUsed(true);
			retval = it->getStart();
		}

		break;
	}

	if (retval)
	{
		return (void*) retval;
	}

	return (void*) NULL;  // no free space
}


void* memory::mem_realloc(void *addr, size_t size)
{
	iterator it = blocks.begin();

	while (it != blocks.end())  // searching for block to reallocate
	{
		if (!it->getUsed() || (it->getStart() != (size_t) addr))
		{
			++it;
			continue;
		}

		break;
	}
	
	if (it == blocks.end())  // block not found, do nothing
	{
		return (void*) NULL;
	}

	if (size < it->getSize())
	{
		iterator temporary = it;
		++temporary;

		block *newBlock = new block;
		newBlock->setUsed(false);
		newBlock->setStart(it->getStart() + size);
		newBlock->setSize(it->getSize() - size);
		blocks.insert(temporary, *newBlock);

		return (void*) 
	}

	size_t retval = (size_t) mem_alloc(size);  // allocate new block

	if (retval)
	{
		memcpy((void*) retval, (void*) addr, min(it->getSize(), size));  // copy old data
		mem_free(addr);  // free old block

		return (void*) retval;  // return new block
	}

	return (void*)NULL;
}


void memory::mem_free(void *addr)
{
	iterator it = blocks.begin();

	while (it != blocks.end())  // find a block,
	{
		if (!it->getUsed() || (it->getStart() != (size_t) addr))
		{
			++it;
			continue;
		}

		it->setUsed(false);  // switch it off,
		joinEmpty(it);  // and join with empty neighbors

		break;
	}
}


void memory::joinEmpty(iterator element)
{
	iterator temporary;

	if (element != blocks.begin())  // not first - try to join with previous
	{
		temporary = element;
		temporary--;  // get previous

		if (!temporary->getUsed() && (temporary->getStart() + temporary->getSize() == element->getStart()))
		{
			temporary->setSize(temporary->getSize() + element->getSize());  // join
			blocks.erase(element);
			element = temporary;
		}
	}

	if (element != --blocks.end())  // not last - try to join with next
	{
		temporary = element;
		element++; // get next

		if (!element->getUsed() && (temporary->getStart() + temporary->getSize() == element->getStart()))
		{
			temporary->setSize(temporary->getSize() + element->getSize());  // join
			blocks.erase(element);
			element = temporary;
		}
	}
}


void memory::mem_dump(void)  // dump blocks status
{
	iterator it = blocks.begin();
	int count = 0;

	cout<<"         ********************************"<<endl;
	cout<<"         * Start      * Size     * Used *"<<endl;
	cout<<"*****************************************"<<endl;

	while (it != blocks.end())
	{
		cout<<"*"<<setw(6)<<"#"<<count<<" * 0x"<<hex<<setw(8)<<setfill('0')<<it->getStart();
		cout<<setfill(' ')<<dec<<" * "<<setw(8)<<it->getSize()<<" * ";
		if (it->getUsed())
		{
			cout<<"used ";
		}
		else
		{
			cout<<"     ";
		}
		cout<<"*"<<endl;
		++count;
		++it;
	}

	cout<<"*****************************************"<<endl;
}
