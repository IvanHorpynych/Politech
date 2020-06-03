#include "ListNode.h"



ListNode::ListNode(char *_data){
	data = (char *)malloc(strlen(_data) + 1);
	for (int i = 0; i < strlen(_data); i++)
		data[i] = _data[i];
	data[strlen(_data)] = '\0';
	prev = NULL;
	next = NULL;
};

ListNode::~ListNode(){
	free(data);
};