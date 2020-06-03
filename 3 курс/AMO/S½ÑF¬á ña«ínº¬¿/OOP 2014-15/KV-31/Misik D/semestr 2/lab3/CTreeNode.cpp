#include "CTreeNode.h"
#include <string>

TreeNode::TreeNode(const TreeNode &t) {
	*this = t;
}

TreeNode& TreeNode::operator=(const TreeNode &t) {
	this->m_BookId = t.m_BookId;
	if (t.m_Author != NULL) {
		this->m_Author = new char[strlen(t.m_Author) + 1];
		strcpy(this->m_Author, t.m_Author);
	}
	else this->m_Author = NULL;
	if (t.m_Title != NULL) {
		this->m_Title = new char[strlen(t.m_Title) + 1];
		strcpy(this->m_Title, t.m_Title);
	}
	else this->m_Title = NULL;
	this->m_Year = t.m_Year;
	this->m_Quantity = t.m_Quantity;
	return *this;
}