#include "CBinTree.h"
#include <string>

char *OneWord(char **str) {
	char *temp = *str;
	char *hlp = strstr(*str, ";");
	hlp[0] = '\0';
	*str = hlp + 1;
	return temp;
}

bool ReadFromFile(BinTree** bt, const char *name) {
	FILE *f = fopen(name, "r");
	if (f == NULL) return false;
	Clear(bt);
	while (!feof(f)) {
		char *str = new char[255];
		TreeNode t;
		fscanf(f, "%s\n", str);
		strcat(str, ";");
		char *end = str;
		char *temp = new char[80];

		t.m_BookId = atoi(OneWord(&str));

		temp = OneWord(&str);
		t.m_Author = new char[strlen(temp) + 1];
		strcpy(t.m_Author, temp);

		temp = OneWord(&str);
		t.m_Title = new char[strlen(temp) + 1];
		strcpy(t.m_Title, temp);

		t.m_Year = atoi(OneWord(&str));

		t.m_Quantity = atoi(OneWord(&str));

		AddElement(bt, t);

		delete[] end;
	}

	return true;
}

void Clear(BinTree **bt) {
	if ((*bt) == NULL) return;
	else {
		if ((*bt)->left != NULL) Clear(&(*bt)->left);
		if ((*bt)->right != NULL) Clear(&(*bt)->right);
		if ((*bt)->root != NULL) delete (*bt)->root;

		(*bt)->root = NULL;
	}
}

void AddElement(BinTree **bt, const TreeNode &t) {
	if ((*bt) == NULL) (*bt) = new BinTree(t);
	else if ((*bt)->root->m_BookId < t.m_BookId) AddElement(&(*bt)->right, t);
	else if ((*bt)->root->m_BookId > t.m_BookId) AddElement(&(*bt)->left, t);
	else (*bt)->root->m_Quantity += t.m_Quantity;
}

bool DeleteById(BinTree** bt, const int num) {
	if ((*bt) == NULL || (*bt)->root == NULL) return false;

	if ((*bt)->root->m_BookId < num) return DeleteById(&(*bt)->right, num);
	else if ((*bt)->root->m_BookId > num) return DeleteById(&(*bt)->left, num);

	BinTree *left = (*bt)->left;
	BinTree *right = (*bt)->right;
	BinTree *to_push;
	if (right != NULL) to_push = right;
	else to_push = NULL;

	delete (*bt);
	if (right != NULL) {
		(*bt) = right;

		while (to_push->left != NULL) to_push = to_push->left;
		to_push->left = left;
	} else (*bt) = left;
	return true;
}

void PrintInfoAboutBook(BinTree** bt, const char *book_name) {
	if ((*bt) == NULL || (*bt)->root == NULL) return;
	if (strcmp((*bt)->root->m_Title, book_name) == 0) {
		printf("Title of book - %s\n", book_name);
		if ((*bt)->root->m_Quantity != 0) printf("Book is in library\n");
		else printf("Book isn't in library\n");
	}
	PrintInfoAboutBook(&(*bt)->left, book_name);
	PrintInfoAboutBook(&(*bt)->right, book_name);
}

void PrintInfoByAuthor(BinTree** bt, const char *author_name) {
	if ((*bt) == NULL || (*bt)->root == NULL) return;
	if (strcmp((*bt)->root->m_Author, author_name) == 0) {
		printf("Title of book - %s ", (*bt)->root->m_Title);
		printf("by author %s\n", author_name);
	}
	PrintInfoByAuthor(&(*bt)->left, author_name);
	PrintInfoByAuthor(&(*bt)->right, author_name);
}

void Print(BinTree **bt) {
	if ((*bt) == NULL || (*bt)->root == NULL) return;
	printf("Title of book - %s ", (*bt)->root->m_Title);
	printf("by author %s\n", (*bt)->root->m_Title);
	printf("ID - %d\n", (*bt)->root->m_BookId);
	Print(&(*bt)->left);
	Print(&(*bt)->right);
}