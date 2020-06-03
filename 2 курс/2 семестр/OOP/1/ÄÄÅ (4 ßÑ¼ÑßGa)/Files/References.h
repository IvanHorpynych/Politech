#pragma once
#include <iostream>
#include "List.h"


class _references
{
	private:
		char *word;
		int *pages;
		int _size;

		static List<_references> ref_list;
		static List<_references> deleted;

		_references(const _references &ref);
		_references(char *_word, int *_pages, int __size);
	

	public:
		friend List<_references>;

		~_references();
		

		void delete_ref();

		void edit_word(char *_word);

		bool operator==(const _references &another_std) const;
		bool operator!=(const _references &another_std) const;

		static List<_references>::Iterator Creat();

		static void load(const char *filename);

		static List<_references>::Iterator begin();
		static List<_references>::Iterator end();

		static void delete_ref(List<_references>::Iterator &itr);
		static void delete_less(int count);
		static void delete_all();

		static void reestablish(char *_word);
		static void reestablish_all();

		static int size();
		static bool empty();

		static List<_references>::Iterator find(char *_word);

		friend List<_references>::Iterator create(char *_word, int *_pages, int __size);

		friend std::ostream& operator<<(std::ostream& os, const _references &dt);
};
