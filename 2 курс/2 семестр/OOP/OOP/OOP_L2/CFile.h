#ifndef CFILE_H
#define CFILE_H

#include <string>
#include <vector>
#include <set>
#include <regex>
#include <iostream>
#include <algorithm>
#include <fstream>

class CFile{
public:
	CFile();
	CFile(const CFile &);
	~CFile();

	void SetDetails(const char*, int);
	void SetKeywords(const char*);
	void AddKeyword(const char*);
	void DelKeyword(const char*);
	void DelKeywordByLen(int);
	const CFile& operator= (const CFile &);
	bool operator< (const CFile &) const;
	int GetSize() const;
	int GetKeywordsNum();
	void SetSize(int);
	void Print() const;

private:
	std::string name;
	int size;
	std::set<std::string> keywords;
	int keywords_nmb;
};

void ListAll(const std::vector<CFile>&);
void AddKeyword(std::vector<CFile>&, char*);
void DelKeyword(std::vector<CFile>&, char*);
void DelKeywordByLen(std::vector<CFile>&, int);
void SortBySize(std::vector<CFile>&);
void SortByName(std::vector<CFile>&);
void SortByKeywordsNumber(std::vector<CFile>&);
std::vector<CFile> GetFromCSV(std::string);

#endif //CFILE_H