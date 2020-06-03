#include "OOP_Lab4.h"

ListNode::ListNode(char *_data){
	data = (char *)malloc(strlen(_data) + 1);
	for (int i = 0; i < strlen(_data); i++)
		data[i] = _data[i];
	data[strlen(_data)] = '\0';
	prev = NULL;
	next = NULL;
};

ListNode::~ListNode(){
	delete data;
};

DoublyLinkedList::DoublyLinkedList(){
	head = NULL;
}

DoublyLinkedList::~DoublyLinkedList(){
	clear();
}

int DoublyLinkedList::size(){
	ListNode *tmp = head;
	int count = 0;
	if (head == NULL) return 0;
	while (tmp->next != NULL){
		tmp = tmp->next;
		++count;
	};
	return ++count;
}

bool DoublyLinkedList::empty(){
	return (head == NULL);
}

void DoublyLinkedList::clear(){
	while (head != NULL)
		this->pop_front();
}

void DoublyLinkedList::push_back(const ListNode &param){
	ListNode *nd = new ListNode(param.data);
	ListNode *tmp = head;
	if (tmp != NULL){
		while (tmp->next != NULL){
			tmp = tmp->next;
		};
		tmp->next = nd;
		nd->prev = tmp;
	}
	else head = nd;
}

void DoublyLinkedList::push_front(const ListNode &param){
	ListNode *nd = new ListNode(param.data);
	ListNode *tmp = head;
	if (head != NULL){
		nd->next = head;
		head->prev = nd;
		head = nd;
	}
	else head = nd;
}

void DoublyLinkedList::pop_front(){
	ListNode *tmp = head;
	if (head == NULL) return;
	if (tmp->next != NULL){
		head = tmp->next;
		head->prev = NULL;
	}
	else{
		head = NULL;
	}
}

void DoublyLinkedList::pop_back(){
	ListNode *tmp = head;
	if (head == NULL) return;
	while (tmp->next != NULL){
		tmp = tmp->next;
	};
	if (tmp->prev != NULL){
		tmp->prev->next = NULL;
	}
	else {
		head = NULL;
	}
}

void DoublyLinkedList::sort(){
	char *tmpstr = (char*)malloc(255);
	if (head == NULL) return;
	if (this->size() == 1) return;
	ListNode *tmp_first = NULL, *tmp_second = NULL;
	bool flag = 1;
	while (flag == 1){
		tmp_first = head;
		tmp_second = tmp_first->next;
		flag = 0;
		while (tmp_second){
			if (strcmp(tmp_first->data, tmp_second->data) > 0){
				strcpy(tmpstr, tmp_first->data);
				strcpy(tmp_first->data, tmp_second->data);
				strcpy(tmp_second->data, tmpstr);
				flag = 1;
			}
			tmp_first = tmp_first->next;
			tmp_second = tmp_second->next;
		}
	}
	free(tmpstr);
}

void DoublyLinkedList::insert_ord(const ListNode &param){
	ListNode *nd = new ListNode(param.data);
	ListNode *tmp = head;
	if (strcmp(nd->data, tmp->data) <= 0){
		this->push_front(*nd);
		return;
	}
	if (tmp != NULL){
		while ((tmp != NULL) && (strcmp(nd->data, tmp->data) > 0)){
			tmp = tmp->next;
		};
		if (!tmp){
			this->push_back(*nd);
			return;
		}
		nd->prev = tmp->prev;
		tmp->prev->next = nd;
		nd->next = tmp;
		tmp->prev = nd;
		tmp = NULL;
	}
	else this->push_front(*nd);
}

bool DoublyLinkedList::insert_after(char *dat, const ListNode &param){
	ListNode *nd = new ListNode(param.data);
	ListNode *tmp = head;
	if (tmp != NULL){
		while ((strcmp(dat, tmp->data) != 0) && (tmp->next != NULL)){
			tmp = tmp->next;
		};
		if ((tmp->next != NULL) && (strcmp(dat, tmp->data) == 0)){
			tmp = tmp->next;
			nd->prev = tmp->prev;
			tmp->prev->next = nd;
			nd->next = tmp;
			tmp->prev = nd;
			tmp = NULL;
			return 1;
		}
		else if (tmp->next == NULL && strcmp(dat, tmp->data) == 0){
			this->push_back(*nd);
			return 1;
		}
		else{
			return 0;
		}
	}
	else{
		return 0;
	}
}

DoublyLinkedList* DoublyLinkedList::operator=(const DoublyLinkedList &a){
	this->clear();
	ListNode *tmp = a.head;
	while (tmp != NULL){
		ListNode *nd = new ListNode(tmp->data);
		this->push_back(*nd);
		tmp = tmp->next;
		delete nd;
	}
	return this;
}

void DoublyLinkedList::merge(DoublyLinkedList &lst){
	if (lst.empty()) return;
	ListNode *tmp = lst.head;
	while (tmp){
		this->insert_ord(*tmp);
		tmp = tmp->next;
	}
	this->sort();
	lst.clear();
}

void DoublyLinkedList::erase(char *dat){
	ListNode *tmp = head, *tmphelp;
	while (tmp){
		if (strcmp(tmp->data, dat) == 0){
			if (tmp == head){
				head = tmp->next;
				head->prev = NULL;
			}
			else {
				tmphelp = tmp->next;
				tmp->prev->next = tmp->next;
				tmp->next->prev = tmp->prev;
				tmp = tmphelp;
				continue;
			}
		}
		tmp = tmp->next;
	}
}

void DoublyLinkedList::unique(){
	if (!head) return;
	this->sort();
	ListNode *tmp_first = head, *tmp_second = head->next;
	while (tmp_second){
		if (strcmp(tmp_first->data, tmp_second->data) == 0){
			if (tmp_second->next == NULL){
				this->pop_back();
				return;
			}
			tmp_second->next->prev = tmp_first;
			tmp_first->next = tmp_second->next;
			tmp_second = tmp_first->next;
			continue;
		}
		tmp_first = tmp_first->next;
		tmp_second = tmp_second->next;
	}
}

void DoublyLinkedList::assign(DoublyLinkedList &dl, int first, int last){
	ListNode *tmp_dl = dl.head, *tmphelp = NULL;
	int count = 0;
	while ((count <= last) && (tmp_dl != NULL)){
		if (count >= first){
			this->push_back(*tmp_dl);
			if (tmp_dl->next == NULL){
				dl.pop_back();
				return;
			}
			tmp_dl->prev->next = tmp_dl->next;
			tmp_dl->next->prev = tmp_dl->prev;
			tmphelp = tmp_dl->next;
			free(tmp_dl);
			++count;
			tmp_dl = tmphelp;
			continue;
		}
		tmp_dl = tmp_dl->next;
		++count;
	}

}

void DoublyLinkedList::splice(int where, const DoublyLinkedList &dl){
	if (where > this->size()) return;
	ListNode *tmp_dl = dl.head, *tmp_this = head, *tmphelp = NULL;
	int count = 0;
	ListNode *nd;

	while (tmp_this && count < where - 1){
		tmp_this = tmp_this->next;
		++count;
	}
	if (where == size()){
		while (tmp_dl){
			nd = new ListNode(tmp_dl->data);
			tmp_this->next = nd;
			nd->prev = tmp_this;
			tmp_dl = tmp_dl->next;
			tmp_this = nd;
		}
		return;
	}
	while (tmp_dl){
		nd = new ListNode(tmp_dl->data);
		tmphelp = tmp_this->next;
		tmp_this->next = nd;
		tmphelp->prev = nd;
		nd->next = tmphelp;
		nd->prev = tmp_this;
		tmp_dl = tmp_dl->next;
		tmp_this = tmp_this->next;
	}
}

void DoublyLinkedList::splice(int where, const DoublyLinkedList &dl, int first, int last){
	DoublyLinkedList *dll = new DoublyLinkedList();
	ListNode *tmp_dl = dl.head, *tmphelp = NULL;
	int count = 0;
	while ((count <= last) && (tmp_dl != NULL)){
		if (count >= first){
			dll->push_back(*tmp_dl);
		}
		tmp_dl = tmp_dl->next;
		++count;
	}
	this->splice(where, *dll);
}

void DoublyLinkedList::print(){
	ListNode *tmp = head;
	while (tmp){
		cout << tmp->data << " ";
		tmp = tmp->next;
	}
	cout << '\n';
}

void DoublyLinkedList::print_bkw(){
	ListNode *tmp = head;
	while (tmp->next){
		tmp = tmp->next;
	}
	while (tmp){
		cout << tmp->data << " ";
		tmp = tmp->prev;
	}
	cout << '\n';
}

