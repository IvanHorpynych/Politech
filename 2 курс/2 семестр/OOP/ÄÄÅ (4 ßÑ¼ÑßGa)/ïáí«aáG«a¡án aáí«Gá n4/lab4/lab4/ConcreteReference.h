#pragma once

#include <iostream>
#include "References.h"

class References;

class ConcreteReference
{
	private:
		char *word;
		int *pages;
		int _size;

		References &lst;

	public:
		
		ConcreteReference(char *_word, int *_pages, int __size);
		ConcreteReference(const ConcreteReference &ref);
		~ConcreteReference();
		
		char* GetWord();
		int* GetPages();
		int GetSize();

		bool operator==(const ConcreteReference &another_std) const;
		bool operator!=(const ConcreteReference &another_std) const;

		void delete_ref();
		void edit_word(char *_word);
		
		friend std::ostream& operator<<(std::ostream& os, const ConcreteReference &dt);
};
