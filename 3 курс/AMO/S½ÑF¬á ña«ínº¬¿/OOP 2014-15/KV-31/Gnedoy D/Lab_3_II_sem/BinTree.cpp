/************************************************************************
*file: BinTree.cpp
*author: Gnedoj D.
*written: 11/04/2015
*last modified: 11/04/2015
*************************************************************************/

#define _CRT_SECURE_NO_WARNINGS
#include "BinTree.h"

BinTree::BinTree(){
	root = NULL;
}

BinTree::~BinTree(){
	del(&root);
	root = NULL;
}

void BinTree::initFromCsv(char* fileName){
	fstream File;
	string m_Bookid;
	string m_Author;
	string m_Title;
	string m_Year;
	string m_Quantity;
	
	File.open(fileName);
	int counter = 0;
	char tmp;
	if (File){
		while (!File.eof()){
			File.get(tmp);
			if (counter == 0 && tmp != ';'){
				m_Bookid.push_back(tmp);

				continue;
			}else if (tmp == ';'){
				++counter;
				if (counter == 5){
					counter = 0;
					addBook(&this->root, new TreeNode(stoi(m_Bookid), m_Author, m_Title, stoi(m_Year), stoi(m_Quantity)));
					m_Bookid.clear();
					m_Author.clear();
					m_Title.clear();
					m_Year.clear();
					m_Quantity.clear();
					continue;
				}
				continue;
			}
			if (counter == 1 && tmp != ';'){
				m_Author.push_back(tmp);
				continue;
			}else if (tmp == ';'){
				++counter;
				continue;
			}
			if (counter == 2 && tmp != ';'){
				m_Title.push_back(tmp);
				continue;
			}else if (tmp == ';'){
				++counter;
				continue;
			}
			if (counter == 3 && tmp != ';'){
				m_Year.push_back(tmp);
				continue;
			}else if (tmp == ';'){
				++counter;
				continue;
			}
			if (counter == 4 && tmp != ';'){
				m_Quantity.push_back(tmp);
				continue;
			}else if (tmp == ';'){
				++counter;

				continue;
			}
			
		}

		File.close();
	}
}

void BinTree::addBook(TreeNode** currentRoot, TreeNode* newNode){
	if (*currentRoot == NULL)
		*currentRoot = newNode;
	else if (newNode->m_Bookid < (*currentRoot)->m_Bookid){
		TreeNode *left = (*currentRoot)->left;
		addBook(&left, newNode);
		(*currentRoot)->left = left;
	}else{
		TreeNode *right = (*currentRoot)->right;
		addBook(&right, newNode);
		(*currentRoot)->right = right;
	}
}

TreeNode* BinTree::deleteById(TreeNode* root, int data){
	if (root == NULL) return root;
	else if (data < root->m_Bookid) root->left = deleteById(root->left, data);
	else if (data > root->m_Bookid) root->right = deleteById(root->right, data);
	// Wohoo... I found you, Get ready to be deleted	
	else {
		// Case 1:  No child
		if (root->left == NULL && root->right == NULL) {
			delete root;
			root = NULL;
		}
		//Case 2: One child 
		else if (root->left == NULL) {
			TreeNode *temp = root;
			root = root->right;
			delete temp;
		}
		else if (root->right == NULL) {
			TreeNode *temp = root;
			root = root->left;
			delete temp;
		}
		// case 3: 2 children
		else {
			TreeNode *temp = FindMin(root->right);
			root->m_Bookid = temp->m_Bookid;
			root->right = deleteById(root->right, temp->m_Bookid);
		}
	}
	return root;
}

void BinTree::booksFromAuthor(TreeNode* root, vector<TreeNode*> *mass, string author){
	if (root == NULL) return;
	booksFromAuthor(root->left, mass, author);       //Visit left subtree
	if (root->m_Author == author){
		mass->push_back(root);
	}
	booksFromAuthor(root->right, mass, author);      // Visit right subtree
}

void BinTree::isBooksFromTitle(TreeNode* root, int& quantity, string title){
	if (root == NULL) return;
	isBooksFromTitle(root->left, quantity, title);       //Visit left subtree
	if (root->m_Title == title){
		quantity = root->m_Quantity;
	}
	isBooksFromTitle(root->right, quantity, title);      // Visit right subtree
}

void BinTree::del(TreeNode** root){
	if ((*root) == NULL) return;
	if ((*root)->left != NULL) del(&((*root)->left));
	if ((*root)->right != NULL) del(&((*root)->right));
	delete *root;
}

TreeNode* BinTree::FindMin(TreeNode* root){
	while (root->left != NULL) root = root->left;
	return root;
}