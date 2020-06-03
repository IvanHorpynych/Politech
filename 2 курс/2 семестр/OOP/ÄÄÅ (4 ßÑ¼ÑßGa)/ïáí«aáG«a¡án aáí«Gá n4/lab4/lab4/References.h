#pragma once
#include "ConcreteReference.h"
#include "List.h"

class ConcreteReference;

class References
{
	private:
		List<ConcreteReference> ref_list;
		List<ConcreteReference> deleted;

		References();	
		References(const References&) = delete;
		References& operator=(References&) = delete;;
	
	public:

		static References& Instance();
		
	    void load(const char *filename);

		List<ConcreteReference>::Iterator begin();
		List<ConcreteReference>::Iterator end();

		List<ConcreteReference> GetRefList();
		List<ConcreteReference> GetDeleted();

		void delete_ref(List<ConcreteReference>::Iterator &itr);
		void delete_less(int count);
		void delete_all();

		void reestablish(char *_word);
		void reestablish_all();

		int size();
		bool empty();

		List<ConcreteReference>::Iterator find(char *_word);
};
