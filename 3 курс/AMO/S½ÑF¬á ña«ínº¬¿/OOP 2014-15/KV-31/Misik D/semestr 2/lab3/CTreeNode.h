//#pragma once

#include <stdio.h>

struct TreeNode {
	unsigned m_BookId;
	char* m_Author;
	char* m_Title;
	int m_Year;
	unsigned m_Quantity;

	TreeNode() { m_BookId = 0; m_Author = 0; m_Title = NULL; m_Year = 0; m_Quantity = 0; };
	TreeNode(const TreeNode &);
	~TreeNode() { if (m_Author != NULL) delete[] m_Author; if (m_Title != NULL) delete[] m_Title; };

	TreeNode& operator=(const TreeNode &);
};