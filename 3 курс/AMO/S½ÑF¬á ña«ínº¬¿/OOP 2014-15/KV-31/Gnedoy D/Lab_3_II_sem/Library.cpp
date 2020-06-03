/************************************************************************
*file: Library.cpp
*author: Gnedoj D.
*written: 11/02/2015
*last modified: 11/02/2015
*************************************************************************/

#include "BinTree.h"

int main(){
	BinTree* newTree = new BinTree();
	newTree->initFromCsv("input.csv");
	newTree->root = newTree->deleteById(newTree->root, 2);
	vector<TreeNode*> array;
	newTree->booksFromAuthor(newTree->root, &array, "Pavel Gnedoy");
	for (int i = 0; i < array.size(); ++i){
		array[i]->print();
	}
	int quantityTest = 0;
	newTree->isBooksFromTitle(newTree->root, quantityTest, "c++ Best practics");
	cout << quantityTest << endl;
	delete newTree;
	return 0;
}