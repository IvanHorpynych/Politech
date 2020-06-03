#include "CFile.h"

using namespace std;

int main(){
	cout << "Get from file" << endl;
	vector<CFile> v1 = GetFromCSV("test.csv");
	ListAll(v1);

	cout << "Add keyword \"aaa\"" << endl;
	AddKeyword(v1, "aaa");
	ListAll(v1);

	cout << "Delete keyword \"book\"" << endl;
	DelKeyword(v1, "book");
	ListAll(v1);

	cout << "Sort by keywords number:" << endl;
	SortByKeywordsNumber(v1);
	ListAll(v1);

	cout << "Delete all keywords which has more then 4 characters" << endl;
	DelKeywordByLen(v1, 4);
	ListAll(v1);

	cout << "Sort by names" << endl;
	SortByName(v1);
	ListAll(v1);

	cout << "Sort by size" << endl;
	SortBySize(v1);
	ListAll(v1);
	return 0;
}