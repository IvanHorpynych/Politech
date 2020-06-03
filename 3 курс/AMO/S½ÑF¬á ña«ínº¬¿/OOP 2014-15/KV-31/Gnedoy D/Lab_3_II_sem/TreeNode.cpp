/************************************************************************
*file: TreeNode.cpp
*author: Gnedoj D.
*written: 11/04/2015
*last modified: 11/04/2015
*************************************************************************/

#define _CRT_SECURE_NO_WARNINGS
#include "TreeNode.h"

TreeNode::TreeNode(int m_Bookid, string m_Author, string m_Title, int m_Year, int m_Quantity){
	this->m_Bookid = m_Bookid;
	this->m_Author = m_Author;
	this->m_Title = m_Title;
	this->m_Year = m_Year;
	this->m_Quantity = m_Quantity;
	this->left = NULL;
	this->right = NULL;
}

TreeNode::~TreeNode(){
	m_Author.clear();
	m_Title.clear();
	left = NULL;
	right = NULL;
}

void TreeNode::print(){
	cout << "\tId: " << m_Bookid << endl;
	cout << "\tAuthor: " << m_Author << endl;
	cout << "\tTitle: " << m_Title << endl;
	cout << "\tYear: " << m_Year << endl;
	cout << "\tQuantity: " << m_Quantity << endl;
}