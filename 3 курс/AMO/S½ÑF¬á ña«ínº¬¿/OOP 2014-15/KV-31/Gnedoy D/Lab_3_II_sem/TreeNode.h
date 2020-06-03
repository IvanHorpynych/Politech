/************************************************************************
*file: TreeNode.h
*author: Gnedoj D.
*written: 11/02/2015
*last modified: 11/02/2015
*************************************************************************/

#pragma once
#include <cstdlib>
#include <iostream>
#include <algorithm>
#include <vector>
#include <string>
#include <math.h>
#include <stdio.h>
#include <iostream>
#include <fstream>
#include <iomanip>
#include <stdlib.h>

using namespace std;

class TreeNode{
public:
	TreeNode(int, string, string, int, int);
	~TreeNode();
	void print();
	friend class BinTree;
private:
	int m_Bookid;
	string m_Author;
	string m_Title;
	int m_Year;
	int m_Quantity;
	TreeNode* left;
	TreeNode* right;
};