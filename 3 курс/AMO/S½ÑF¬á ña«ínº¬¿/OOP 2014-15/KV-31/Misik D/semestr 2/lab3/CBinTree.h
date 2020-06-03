//#pragma once

#include "CTreeNode.h"

struct BinTree {
	TreeNode* root;
	BinTree* left;
	BinTree* right;

	BinTree() { root = new TreeNode(); left = NULL; right = NULL; };
	BinTree(const TreeNode &t) { root = new TreeNode(t); left = NULL; right = NULL; };
	~BinTree() { delete root; };

};

bool ReadFromFile(BinTree **, const char *);
void Clear(BinTree **);
void AddElement(BinTree **, const TreeNode &);
bool DeleteById(BinTree **, const int num);
void PrintInfoAboutBook(BinTree**, const char *);
void PrintInfoByAuthor(BinTree** , const char *);
void Print(BinTree **);