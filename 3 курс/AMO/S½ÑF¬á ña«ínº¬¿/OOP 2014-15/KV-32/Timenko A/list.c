#include "list.h"

#include <stdlib.h>
#include <stdio.h>

LNode *createList(int size)
{
    LNode *head;
    LNode *tail;
    int i;
    
    if (size < 1)
        return NULL;
    
    head = NULL;
    for (i = 0; i < size; ++i)
    {
        LNode *newNode;
        
        newNode = (LNode *)malloc(sizeof(LNode));
        newNode->id = i;
        newNode->prev = head;
        newNode->next = NULL;
        
        if (head)
            head->next = newNode;
        head = newNode;
    }
    
    tail = head;
    while (tail->prev)
        tail = tail->prev;
    
    tail->prev = head;
    head->next = tail;
    
    return head;
}

LNode *getListNode(const LNode *head, int id)
{
    const LNode *nodeIterator;
    
    nodeIterator = head;
    
    while (true)
    {
        nodeIterator = nodeIterator->next;
        if (nodeIterator->id == id)
            return (LNode *)nodeIterator;
        if (nodeIterator == head)
            break;
    }
    
    return NULL;
}

void appendToList(LNode **head, const LNode *entry)
{
    LNode *newNode;
    
    newNode = (LNode *)malloc(sizeof(LNode));
    *newNode = *entry;
    
    newNode->next = (*head)->next;
    newNode->next->prev = newNode;
    newNode->prev = *head;
    (*head)->next = newNode;
    
    *head = newNode;
}

void deleteFromList(LNode **head, int id)
{
    LNode *nodeIterator = *head;
    
    while (true)
    {
        nodeIterator = nodeIterator->next;
        
        if (nodeIterator->id == id)
        {
            LNode *tmp;
            
            if (nodeIterator == *head)
                *head = nodeIterator->prev;
            tmp = nodeIterator;
            nodeIterator->prev->next = nodeIterator->next;
            nodeIterator->next->prev = nodeIterator->prev;
            nodeIterator = nodeIterator->prev;
            free((void *)tmp);
        }
        else
            if (nodeIterator == *head)
                break;
    }
}

void insertToList(LNode *head, const LNode *entry, int id)
{
    LNode *nodeIterator;
    
    nodeIterator = head;
    
    while (true)
    {
        nodeIterator = nodeIterator->next;
        
        if (nodeIterator->id == id)
        {
            LNode *newNode;
            
            newNode = (LNode *)malloc(sizeof(LNode));
            *newNode = *entry;
            
            newNode->next = nodeIterator;
            newNode->prev = nodeIterator->prev;
            nodeIterator->prev->next = newNode;
            nodeIterator->prev = newNode;
            
            break;
        }
        
        if (nodeIterator == head)
            break;
    }
}

void revertList(LNode *head)
{
    LNode *nodeIterator = head;
    
    while (true)
    {
        LNode *tmp = nodeIterator->next;
        nodeIterator->next = nodeIterator->prev;
        nodeIterator->prev = tmp;
        
        nodeIterator = nodeIterator->prev;
        
        if (nodeIterator == head)
            break;
    }
}

LNode *mergeLists(const LNode *head1, const LNode *head2)
{
    const LNode *nodeIterator;
    LNode *newHead;
    LNode *newTail;
    
    nodeIterator = head1;
    
    newHead = NULL;
    while (true)
    {
        LNode *newNode;
        
        nodeIterator = nodeIterator->next;
        
        newNode = (LNode *)malloc(sizeof(LNode));
        newNode->id = nodeIterator->id;
        newNode->prev = newHead;
        newNode->next = NULL;
        
        if (newHead)
            newHead->next = newNode;
        newHead = newNode;
        
        if (nodeIterator == head1)
            break;
    }
    
    newTail = newHead;
    while (newTail->prev)
        newTail = newTail->prev;
    
    newTail->prev = newHead;
    newHead->next = newTail;
    
    nodeIterator = head2;
    
    while (true)
    {
        nodeIterator = nodeIterator->next;
        
        if (!getListNode(newHead, nodeIterator->id))
            appendToList(&newHead, nodeIterator);
        
        if (nodeIterator == head2)
            break;
    }
    
    return newHead;
}

void cleanListNearbyEqualNodes(LNode **head)
{
    LNode *nodeIterator;
    bool isElemDeleted;
    
    nodeIterator = *head;
    
    while (true)
    {
        nodeIterator = nodeIterator->next;
        
        while (true)
        {
            isElemDeleted = false;
            
            if (nodeIterator->id == nodeIterator->next->id)
            {
                LNode *tmp;
                
                tmp = nodeIterator->next;
                tmp->next->prev = nodeIterator;
                nodeIterator->next = tmp->next;
                if (tmp == *head)
                    *head = tmp->next;
                free((void *)tmp);
                isElemDeleted = true;
            }
            
            if (!isElemDeleted)
                break;
        }
        
        if (nodeIterator == *head)
            break;
    }
}

void printList(LNode *head)
{
    LNode *nodeIterator;
    
    nodeIterator = head;
    
    while (true)
    {
        nodeIterator = nodeIterator->next;
        
        printf("%i ", nodeIterator->id);
        if (nodeIterator == head)
            break;
    }
}

void destroyList(LNode **head)
{
    LNode *nodeIterator;
    
    nodeIterator = *head;
    
    while (true)
    {
        LNode *tmp;
        
        tmp = nodeIterator;
        nodeIterator = nodeIterator->next;
        free((void *)tmp);
        if (nodeIterator == *head)
            break;
    }
    
    *head = NULL;
}
