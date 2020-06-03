#include "DoublyLinkedList.h"


DoublyLinkedList::DoublyLinkedList(const DoublyLinkedList &item)
:head(new ListNode()),tail(0),_size(item._size)
{
	if (item.empty()){
		return;
	}
	ListNode *temp = item.head->next;
	while (temp){
		//create new element to push into list
		push_back(*temp);
		temp = temp->next;
	}
}


//Removes all elements from the list.
void DoublyLinkedList::clear(void){
	ListNode *temp = head->next;
	if (!temp) return;
	while( temp->next ){
		temp = temp->next;
		delete temp->prev;
	}
	delete temp;
	head->next = 0;
	tail = 0;
	_size = 0;
}
// Adds node to the front of the list.
void DoublyLinkedList::push_front(ListNode &item){
    ListNode *copy = new ListNode(item.data);
    
	if (head->next){
		head->next->prev = copy;
		copy->next = head->next;
	}else {
		copy->next = 0;
		tail = copy;
	}
	head->next = copy;
	copy->prev = head;
	_size++;
}


// Adds node to the end of the list.
void DoublyLinkedList::push_back(ListNode &item){
    ListNode *copy = new ListNode(item.data);

    //add item
	if (!head->next){
		copy->prev = head;
		head->next = copy;
	}else {
		copy->prev = tail;
		tail->next = copy;
	}
	copy->next = 0;
	tail = copy;
	_size++;
}

//Removes the first element of the list.
void DoublyLinkedList::pop_front(){
	if (!head->next) return;
	ListNode *temp = head->next;
	head->next = temp->next;
	temp->next->prev = head;
	delete temp;
	temp = 0;
	_size--;
}

//Removes the last element of the list.
void DoublyLinkedList::pop_back(){
	if (!head->next) return;

	tail = tail->prev;
	delete tail->next;
	tail->next = 0;
	_size--;
}

//Inserts node preserving list ordering
void DoublyLinkedList::insert_ord(ListNode &item){
	
	if (!head->next){
		push_front(item);
		return;
	}
	ListNode* temp = head->next;


	while(temp)
	{
		if (strcmp(temp->data,item.data) >= 0)
		{
			temp = temp->prev;
			ListNode *copy = new ListNode(item.data);

			copy->next = temp->next;
			copy->prev = temp;
			temp->next = copy;
			copy->next->prev = copy;
			_size++;
			return;
		}
		temp = temp->next;
	}
	push_back(item);


}

//Sorts list in nondescending order
void DoublyLinkedList::sort(){
	//bubblesort
	if (!head->next) return; //if list empty or has 1 element, return
	if (!head->next->next) return;

	ListNode *i;
	//iterator and temporary variables for swapping
	ListNode *right_border,*last_swap;
	
	do {
		last_swap = head->next;

		for (i = head->next->next;i; i = i->next){
			if (strcmp(i->prev->data,i->data) > 0){
				char *temp = new char[strlen(i->prev->data) + 1];
				strcpy(temp,i->prev->data);
				i->prev->data = (char*)realloc(i->prev->data,strlen(i->data)+1);
				
				strcpy(i->prev->data,i->data);
				i->data = (char*)realloc(i->data,strlen(temp)+1);
				
				strcpy(i->data,temp);
				last_swap = i;
				delete[] temp;
			}

		}
		right_border = last_swap;
	}while (right_border != head->next);
}

// Inserts nd after the the node with dat. Returns true on success	
bool DoublyLinkedList::insert_after(char *dat, ListNode &nd){
	if (!dat) return false;
	if (!head->next) {
		/*
		push_front(nd);
		return true;
		*/
		return false;
	}

	ListNode *temp = head->next;
	
	while (temp){
		if (!strcmp(dat,temp->data)){
			ListNode* copy = new ListNode(nd.data); 
			copy->next = temp->next;
			copy->prev = temp; 
			temp->next = copy; 
			if (copy->next){
				copy->next->prev = copy;				
			}else {
				tail = copy;
			}
			_size++;
			return true;
		}
		temp = temp->next;
	}
	return false;
}

// Overload of the assignment operator
DoublyLinkedList& DoublyLinkedList::operator=(const DoublyLinkedList &item){
	if (!empty()) {
		clear();
	}
	if (!item.head->next){
		return *this;
	}
	ListNode *temp = item.head->next;
	while (temp){
		//create new element to push into list
		push_back(*temp);
		temp = temp->next;
	}
	_size = item._size;
	return *this;
}

//Removes the elements from the argument 
//list, inserts them into the target list, and orders the new, combined set of 
//elements in nondescending order
void DoublyLinkedList::merge(DoublyLinkedList &list){
	if (!list._size) {
		return;
	}
	ListNode *temp = list.head->next;

	while (temp){
		push_front(*temp);
		temp = temp->next;
	}
	sort();
	list.clear();
}

// Removes all nodes with dat 
void DoublyLinkedList::erase(char *dat){
	ListNode *temp = head->next;
	if (!temp || !dat) return;
	while( temp ){
		if (!strcmp(dat,temp->data)){

			ListNode *del = temp; 
			temp = temp->next; 
			del->prev->next = temp; 
			if (temp){ 
				temp->prev = del->prev; 
			}else{
				tail = del->prev;
			}
			delete del;

			_size--;
		}else {
			temp = temp->next;
		}
	}
}

//Removes adjacent duplicate elements or adjacent elements
void DoublyLinkedList::unique(){
	//if less than 2 items, return
	if (_size < 2)return;
	sort();
	ListNode *i = head->next;
	while (i){
		if (!strcmp(i->data,i->next->data)){
			char *repeated_data = new char[strlen(i->data)+1];
			strcpy(repeated_data,i->data);
			i = i->prev;
			erase(repeated_data);
			delete[] repeated_data;
		}
		i = i->next;
	}
}

//deletes elements from argument list between first and last positions 
//and adds them to the end of target list
void DoublyLinkedList::assign(DoublyLinkedList &dl, int first, int last){
	if (!dl.head->next || last >= dl._size || first > last || first < 0 ){
		return;
	}
	//get the 'first' element of dl
	ListNode *temp = dl.head->next;
	for (int i = 0; i<first; i++){
		temp = temp->next;
	}
	for (int i = first; i<=last; i++){
		push_back(*temp);
		std::cout << "\n\n";
		print();
		std::cout << "\n\n";

		ListNode *del = temp;
		temp = temp->next;
		
		del->prev->next = temp;
		if (temp){
			temp->prev = del->prev;
		}
		else {
			dl.tail = dl.tail->prev;
		}
		dl._size--;
		delete del;
	}
}

//inserts elements of 
//argument list in target list starting from where position
void DoublyLinkedList::splice(int pos, const DoublyLinkedList &dl){
	if (!dl.head->next) return;

	ListNode *temp1 = head;
	ListNode *temp2 = dl.head->next;
	if (pos > _size || pos <= 0) {
		return;
	}
	//skip to the 'pos' element
	for (int i = 0;i < pos;i++){
		temp1 = temp1->next;
	}

	while (temp2){

		ListNode *push = new ListNode(temp2->data);
		//inserting item
		push->next = temp1->next;
		push->prev = temp1;
		temp1->next = push;
		if (push->next){
			push->next->prev = push;
		}else {
			tail = push;
		}
		//iteration
		temp1 = temp1->next;
		temp2 = temp2->next;
	};
}


// inserts elements of argument list from first to last positions 
//in target list starting from where position
void DoublyLinkedList::splice(int pos, const DoublyLinkedList &dl, int first, int last){

	if (first <= 0 || first > last || last > dl._size || pos > _size || pos <= 0){
		return;
	}

	ListNode *temp1 = head,*temp2 = dl.head;

	for(int i = 0; i<first; i++){
		temp2 = temp2->next;		
	}
	//skip until needed element
	for (int i = 0; i<pos; i++){
		temp1 = temp1->next;
	}
	//insert needed elements!
	for(int i = first; i<=last; i++){
		ListNode *push = new ListNode(temp2->data);
		//inserting item
		push->next = temp1->next;
		push->prev = temp1;
		temp1->next = push;
		if (push->next){
			push->next->prev = push;
		}else {
			tail = push;
		}
		//iteration
		temp1 = temp1->next;
		temp2 = temp2->next;
	}

}


//prints list
const void DoublyLinkedList::print() const{
	ListNode *temp = head->next;
	while (temp){
		std::cout << temp->data << std::endl;
		temp = temp->next;
	}
}

//prints list backward
const void DoublyLinkedList::print_bkw() const{
	ListNode *temp = tail;
	while (temp->prev){
		std::cout << temp->data << std::endl;
		temp = temp->prev;
	}
}
