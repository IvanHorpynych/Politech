/************************************************************************
*file: BinTree.h
*author: Gnedoj D.
*written: 11/04/2015
*last modified: 11/04/2015
*************************************************************************/

#pragma once
#include "TreeNode.h"

class BinTree{
public:
	BinTree();
	~BinTree();
	void initFromCsv(char*);
	void addBook(TreeNode**, TreeNode*);
	TreeNode* deleteById(TreeNode*, int);
	void booksFromAuthor(TreeNode*, vector<TreeNode*>*, string);
	void isBooksFromTitle(TreeNode*, int&, string);
//private:
	TreeNode* root;
	void del(TreeNode**);
	TreeNode* FindMin(TreeNode*);
};