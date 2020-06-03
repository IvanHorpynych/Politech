#include "MemMgr.h"
MemMgr::MemMgr()
{
	uchar* new_block =  new uchar[defsize + 3*size_of_st];
	m_begin.push_back( new_block );

	size_t* tmp = (size_t *) *(m_begin.end()-1);
	
	*tmp = defsize; //curr_len
	tmp += size_of_st;
	*tmp = 0; //prev_bloc_len
	tmp += size_of_st;
	*tmp = 0; // 0 - free; 
}



void* MemMgr::alloc(size_t bytes)
{
	void *memory = 0;
	if(bytes == 0)
		return memory;
	bytes = (bytes + 3) & 0xFFFFFFFC;
	if(bytes > defsize)
		return NULL;
	vector<uchar*>::iterator it;
	for(it = m_begin.begin(); it < m_begin.end(); it++)
	{
		size_t* tmp = (size_t *) *it;
		size_t nch_size = 0;
		while(nch_size < defsize)
		{
			size_t block_len = *tmp;
			tmp += 2*size_of_st;

			if(*tmp == 0 && block_len >= bytes)
			{
				*tmp = 1;		//set to filled
				tmp += size_of_st;
				memory = (void *)tmp; 
				tmp-= 3*size_of_st; //set number of bytes that filled
				*tmp = bytes;

				//create free block
				if(block_len - bytes > 3*size_of_st)
				{
					tmp += 3*size_of_st + bytes/4;
					*tmp = block_len - bytes - 3*size_of_st;
					tmp += size_of_st;
					*tmp = bytes;
					tmp += size_of_st;
					*tmp = 0;
					tmp += size_of_st;
				}
				break;

			}
			tmp += size_of_st + block_len/4;
			nch_size += 3*sizeof(size_t) + block_len;
		}
	}
	if(memory == 0)
	{
		try
		{
			m_begin.push_back( new uchar[defsize + 3*size_of_st] );
			size_t* tmp = (size_t *) *(m_begin.end() - 1);
			*tmp = bytes;
			tmp += size_of_st;
			*tmp = 0;
			tmp += size_of_st;
			*tmp = 1;		// 0 - free;
			tmp += size_of_st;
			memory = tmp;
			if(defsize - bytes > 3*size_of_st)
			{
				tmp += size_of_st + bytes/4;

				//free space
				*tmp = defsize - bytes - 3*size_of_st;
				tmp += size_of_st;
				*tmp = bytes;
				tmp += size_of_st;
				*tmp = 0;
			}
		}
		catch(...)
		{	memory = 0;	}
	}
	return memory;
}
void* MemMgr::realloc(void* spase, size_t new_size)
{
	if(new_size == 0)
		return NULL;
	new_size = (new_size + 3) & 0xFFFFFFFC;
	void* memory = 0;
	if(spase == 0)
		return alloc(new_size);
	size_t* tmp = (size_t *) spase;
	tmp -= 3*size_of_st;
	size_t curr_size = *tmp;
	if(curr_size == new_size)
		return spase;
	else if(curr_size > new_size )
	{
		memory = spase;
		if(curr_size - new_size >= 3*size_of_st )
		{
			tmp = (size_t *)spase - 2*size_of_st;
			size_t prev_size = *tmp;
			size_t left_b = prev_size;
			size_t total_len = curr_size + 3*size_of_st;
			while(left_b != 0)
			{
				total_len += left_b + 3*size_of_st;
				tmp -= (left_b/4 + 3*size_of_st);
				left_b = *tmp;
			}

			if((defsize + 3*size_of_st) - total_len > 3*size_of_st)			{
				//check right block
				tmp = (size_t *)spase + curr_size/4;
				size_t* alien_block = tmp;
				tmp += 2*size_of_st;
				if(*tmp == 0) // free memory + right_block
				{
					tmp -= 2*size_of_st;
					size_t right_len = *tmp;

				if((defsize + 3*size_of_st) - total_len - right_len > 6*size_of_st )
						alien_block = tmp + right_len/4 + 3*size_of_st;
					else
						alien_block = NULL;

					tmp = (size_t *)spase - 3*size_of_st;
					*tmp = new_size;
					tmp += new_size/4 + 3*size_of_st;
					*tmp = curr_size - new_size + right_len;
					tmp += size_of_st;
					*tmp = new_size;
					tmp += size_of_st;
					*tmp = 0;

					if(alien_block)
					{
						alien_block += size_of_st;
						*alien_block = curr_size - new_size + right_len;	
					}
				}
				else
				{
					tmp = (size_t *)spase;
					tmp -= 3*size_of_st;
					*tmp = new_size;
					alien_block += size_of_st;
					*alien_block = new_size;
				}

			}
			else
			{
				tmp = (size_t *)spase;
			
				tmp -= 3*size_of_st;
				*tmp = new_size;
				tmp += new_size/4 + 3*size_of_st;
				*tmp = curr_size - new_size - 3*size_of_st;
				tmp += size_of_st;
				*tmp = new_size;
				tmp += size_of_st;
				*tmp = 0;
				tmp += size_of_st;
			}
		}
		return memory;
	}
	else
	{
		//check next block (is_free ??)
		tmp += size_of_st;
		size_t prev_size = *tmp;
		size_t left_b = prev_size;
		size_t total_len = curr_size + 3*size_of_st;
		while(left_b != 0)
		{
			total_len += left_b + 3*size_of_st;
			tmp -= (left_b/4 + 3*size_of_st);
			left_b = *tmp;
		}

		if((defsize + 3*size_of_st) - total_len > 3*size_of_st) //right block is real !
		{
				//check right block
			tmp = (size_t *)spase + curr_size/4;
			size_t right_len = *tmp;
			tmp += 2*size_of_st;
			if(*tmp == 0 &&  new_size <= (curr_size + right_len + 3*size_of_st))
			{
				memory = spase;
				tmp = (size_t *)spase - 3*size_of_st;
				*tmp = new_size;
				//new free block
				if(curr_size + right_len > new_size)
				{
					tmp += new_size/4 + 3*size_of_st;
					*tmp = curr_size + right_len - new_size;
					tmp += size_of_st;
					*tmp = new_size;
					tmp += size_of_st;
					*tmp = 0;
					tmp += size_of_st;
				}
			}
		}

		if(memory == 0)
		{
			//find new block
			memory = alloc(new_size);
			if(memory != NULL)
			{
				uchar* old_mem = (uchar *)spase;
				uchar* new_mem = (uchar *)spase;
				//copy data from old block to new;
				for(int i = 0; i < curr_size; i++ )
				{
					*new_mem = *old_mem;
					new_mem += sizeof(uchar);
					old_mem += sizeof(uchar);
				}
				//clear old memory
				free(spase);
			}
		}
		return memory;
	}
}

void MemMgr::free(void* space)
{
	size_t* alien_block = NULL;

	size_t* tmp = (size_t *)space - size_of_st;
	*tmp = 0;	//set to free
	tmp -= size_of_st;
	size_t prev_size = *tmp;
	tmp -= size_of_st;
	size_t curr_size = *tmp;
	tmp += size_of_st;

	////////////////
	size_t left_b = prev_size;

	size_t total_len = curr_size + 3*size_of_st;

	while(left_b != 0)
	{
		total_len += left_b + 3*size_of_st;
		tmp -= (left_b/4 + 3*size_of_st);
		left_b = *tmp;
	}
	
	if((defsize + 3*size_of_st) - total_len > 3*size_of_st) // right block is real !
	{
		//check right block
		tmp = (size_t *)space + curr_size/4;
		alien_block = tmp;
		tmp += 2*size_of_st;
		if(*tmp == 0)
		{
			//right + current
			tmp -= 2*size_of_st;
			size_t right_len = *tmp;

			if((defsize + 3*size_of_st) - total_len - right_len > 6*size_of_st )
				alien_block = tmp + right_len/4 + 3*size_of_st;
			else
				alien_block = NULL;
			tmp = (size_t *)space - 3*size_of_st;
			*tmp += right_len + 3*size_of_st;
			curr_size += right_len + 3*size_of_st;

		}
	}
	////////////////
	if(prev_size != 0)
	{
		tmp = (size_t *)space - prev_size/4 - 4*size_of_st;
		if(*tmp == 0) //prev block is free!
		{
			tmp -= 2*size_of_st;
			*tmp += curr_size + 3*size_of_st;
			curr_size = *tmp;
		}
	}

	if(alien_block)
	{
		alien_block += size_of_st;
		*alien_block = curr_size;
	}
	
	
}

MemMgr::~MemMgr(void)
{
	for(size_t i = 0; i < m_begin.size(); i++)
	{
		delete[] *(m_begin.end()-1);
		m_begin.pop_back();
	}	}
void MemMgr::mem_dump()
{
	vector<uchar*>::iterator it;
	int i = 1;
	cout<<endl<<endl;
	for(it = m_begin.begin(); it < m_begin.end(); it++)
	{
		cout<<"Page #"<<i<<" attributes:"<<endl;
		i++;
		
		size_t* tmp = (size_t *) *it;
		
		size_t len = 0;
		while(len < defsize)
		{
			size_t block_len = *tmp;
			tmp += size_of_st;
			size_t prev_len = *tmp;
			tmp += size_of_st;
			size_t is_free = *tmp;
			len += block_len + 3*size_of_st;
			tmp += block_len/4 + size_of_st;
			
			cout<<"len = "<<block_len<<"; prev = "<<prev_len;
			if(is_free == 0)
				cout<<";free = TRUE"<<endl;
			else
				cout<<";free = FALSE"<<endl;
		}
	}
}
