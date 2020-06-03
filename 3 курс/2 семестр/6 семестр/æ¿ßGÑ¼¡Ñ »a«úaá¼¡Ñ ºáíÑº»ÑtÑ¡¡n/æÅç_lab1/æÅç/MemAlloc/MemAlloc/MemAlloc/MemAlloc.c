#include "MemAlloc.h"

ListNode * head = NULL;
static char memory[MEM_SIZE];
int aa = 5;

void * mem_alloc(size_t size)
{
    ListNode * tmp;
    ListNode * properBlock = NULL;
    ListNode * buffer;
    size_t properBlockSize = MEM_SIZE;
    size_t currentBlockSize;
    if (!size || size >= MEM_SIZE - sizeof(ListNode))
        return NULL;

    if (!head)//if NULL
    {
        head = memory;
        properBlock = head;
        properBlock->blockSize = size;
        properBlock->nextNode = NULL;
        properBlock->prevNode = NULL;
        currentBlockSize = (size_t)properBlock + sizeof(ListNode);
        return (size_t)properBlock + sizeof(ListNode);
    }
    else
    {
        if (head != memory)//перевірка, чи є вільний простір на початку масиву
        {
            if (((size_t)head - (size_t)memory) >= size + sizeof(ListNode))//якщо є, то взнаємо його розмір
            {
                properBlock = memory;
                properBlockSize = ((size_t)head - (size_t)memory);
            }
        }
        //далі також пошук підходящого місця для вставки
        //Для цього пробіжимо весь список, і знайдемо мінімальний, але більший за size вільний простір
        tmp = head;
        while (tmp->nextNode)
        {
            currentBlockSize = (size_t)tmp->nextNode - ((size_t)tmp + sizeof(ListNode) + tmp->blockSize);//отримуємо розмір вільного простору
            //якщо поточний розмір вільного простору більший за запрошений розмір плюс розмір дескриптора і він менший за розмір попереднього підходящого сегменту
            if ((currentBlockSize >= size + sizeof(ListNode)) && (currentBlockSize < properBlockSize))
            {
                properBlockSize = currentBlockSize;//то робимо переприсвоювання підходящого розміру
                properBlock = (size_t)tmp + sizeof(ListNode) + tmp->blockSize;//та вказівника на початок даної ділянки
                buffer = tmp;
            }
            tmp = tmp->nextNode;
        }

        //Виділення памяті, якщо знайдений підходящий блок
        if (properBlock)
        {
            //якщо пишеться на початок області
            if (properBlock == memory)
            {
                properBlock->blockSize = size;
                properBlock->prevNode = NULL;
                properBlock->nextNode = head;
                head->prevNode = properBlock;
                head = properBlock;
            }else//якщо десь в середину
            {                
                properBlock->blockSize = size;
                properBlock->nextNode = buffer->nextNode;
                properBlock->prevNode = buffer;
                buffer->nextNode->prevNode = properBlock;
                buffer->nextNode = properBlock;
            }
            return ((size_t)properBlock + sizeof(ListNode));
        }else

        //якщо память ще не була виділена, то виділимо після останньої ноди
        //tmp зараз на неї вказує
        if (size <= ((size_t)memory + MEM_SIZE) - ((size_t)tmp + tmp->blockSize + 2*sizeof(ListNode)))
        {
            properBlock = (size_t)tmp + tmp->blockSize + sizeof(ListNode);
            properBlock->blockSize = size;
            properBlock->nextNode = NULL;
            properBlock->prevNode = tmp;
            properBlock->prevNode->nextNode = properBlock;
            return ((size_t)properBlock + sizeof(ListNode));
        }
    }
    return NULL;
}
void mem_free(void * addr)
{
    ListNode * tmp;
    if (!head)
        return;
    tmp = head;

    while (tmp)
    {
        if ((size_t)tmp + sizeof(ListNode) == addr)
        {
            if (tmp == head)
                head = head->nextNode;

            if (tmp->prevNode)
                tmp->prevNode->nextNode = tmp->nextNode;

            if (tmp->nextNode)
                tmp->nextNode->prevNode = tmp->prevNode;

            return;
        }
        tmp = tmp->nextNode;
    }

}

void * mem_realloc(void * addr, size_t newSize)
{
    ListNode * nodePtr = NULL;
    ListNode * tmp;
    char * currentBlock = addr;
    char * newBlock;
    size_t i;
    //пошук дескриптора даного блоку
    tmp = head;
    while (tmp)
    {
        if ((size_t)tmp + sizeof(ListNode) == addr)
        {
            nodePtr = tmp;
            break;
        }
        tmp = tmp->nextNode;
    }

    //якщо дескриптор по даному адресу знадений
    if (nodePtr)
    {
        //якщо новий розмір == старому, то вертаємо старий адрес
        if (nodePtr->blockSize == newSize)
            return addr;

        //якщо запит на зменшення розміру блока
        if (nodePtr->blockSize > newSize)
        {
            nodePtr->blockSize = newSize;
            return addr;
        }

        //якщо запит на БІЛЬШИЙ блок
        if (nodePtr->blockSize < newSize)
        {
            newBlock = mem_alloc(newSize);

            //помилка, память не виділилася
            if (!newBlock)
                return NULL;

            //А якщо все ОК, то копіюємо дані із старого в новий блок
            for (i = 0; i < nodePtr->blockSize; ++i)
                newBlock[i] = currentBlock[i];

            //видаляємо старий блок
            mem_free(addr);

            //та вертаємо адрес нового блоку
            return newBlock;
        }
    }
    //вертається той самий адрес, якщо дескриптор не знайдений
    return addr;
}

void mem_dump()
{
	ListNode * tmp = head;
	int i = 1;
	size_t usefulOccupiedMemory = 0, wholeOccupiedMemory = 0;
	printf("************************************************\n");
    if (!head)
	{
		printf("It's empty\n");
        return;
	}
    
     while (tmp)
     {
         printf("Block %d;\tSize: %d;\tAddress %d;\n",i++, tmp->blockSize, ((size_t)tmp + sizeof(ListNode)));
		 usefulOccupiedMemory += tmp->blockSize;
		 wholeOccupiedMemory += (tmp->blockSize + sizeof(ListNode));
         tmp = tmp->nextNode;
     }
	 printf("Useful occupied memory in bytes: %d\n",usefulOccupiedMemory);
	 printf("Whole occupied memory in bytes: %d\n",wholeOccupiedMemory);
}
