#include "CBinTree.h"
#include <string>
#include <windows.h>
#include <conio.h>

void gotoxy(const int x, const int y) {												//set cursor in console on {X,Y}
	COORD c = { x, y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), c);
}

void write_on_y(int y);

int menu() {
	int KEY;
	do {
		gotoxy(35, 8);
		printf("1. Read from file");
		gotoxy(35, 9);
		printf("2. Add element");
		gotoxy(35, 10);
		printf("3. Delete by ID");
		gotoxy(35, 11);
		printf("4. Print info about book");
		gotoxy(35, 12);
		printf("5. Print info about author");
		gotoxy(35, 13);
		printf("6. Clear");
		gotoxy(35, 14);
		printf("7. Print");
		gotoxy(35, 15);
		printf("0. Exit");
		KEY = _getch();
		if (KEY <= '7' && KEY >= '0') break;
	} while (true);
	system("cls");
	return KEY - '0';
}

int main() {
	BinTree *tree = NULL;
	int ret_menu = 0;
	while (ret_menu = menu()) {
		if (ret_menu == 1) {
			char *name = new char[30];
			printf("Enter name of file\n");
			scanf("%s", name);
			if (ReadFromFile(&tree, name)) printf("\nAll OK");
			else printf("\nBad");
			_getch();
			system("cls");
		} else if (ret_menu == 2) {
			TreeNode *node = new TreeNode;
			printf("Author\n");
			node->m_Author = new char[40];
			scanf("%s", node->m_Author);
			printf("Book ID\n");
			scanf("%d", &node->m_BookId);
			printf("Quanity\n");
			scanf("%d", &node->m_Quantity);
			printf("Title\n");
			node->m_Title = new char[40];
			scanf("%s", node->m_Title);
			printf("Year\n");
			scanf("%d", &node->m_Year);
			AddElement(&tree, *node);
			printf("\nAll OK");
			_getch();
			system("cls");
		} else if (ret_menu == 3) {
			int book_id;
			printf("Book ID\n");
			scanf("%d", &book_id);
			if (DeleteById(&tree, book_id)) printf("\nAll OK");
			else printf("\nBad");
			_getch();
			system("cls");
		} else if (ret_menu == 4) {
			char *book_name = new char[40];
			printf("Title\n");
			scanf("%s", book_name);
			PrintInfoAboutBook(&tree, book_name);
			_getch();
			system("cls");
		} else if (ret_menu == 5) {
			char *book_author = new char[40];
			printf("Author\n");
			scanf("%s", book_author);
			PrintInfoByAuthor(&tree, book_author);
			_getch();
			system("cls");
		}
		else if (ret_menu == 6) {
			Clear(&tree);
		} else if (ret_menu == 7) {
			Print(&tree);
			_getch();
			system("cls");
		}
	}
	return 0;
}