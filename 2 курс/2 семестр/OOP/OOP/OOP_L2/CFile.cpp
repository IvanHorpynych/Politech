#include "CFile.h"

using namespace std;

CFile::CFile(){
	name = "";
	size = 0;
	keywords.clear();
	keywords_nmb = 0;
}

CFile::CFile(const CFile &arg){
	name = arg.name;
	size = arg.size;
	keywords = arg.keywords;
	keywords_nmb = arg.keywords_nmb;
}

CFile::~CFile(){}

void CFile::SetDetails(const char* name_, int size_){
	name = name_;
	size = size_;
}

void CFile::SetKeywords(const char* keywords_){
	regex div("[^;]+");
	smatch match;
	string keys_tmp = keywords_;
	keywords_nmb = 0;
	keywords.clear();
	while (regex_search(keys_tmp, match, div)) {
		AddKeyword(match[0].str().c_str());
		keys_tmp = match.suffix().str();
	}
}

void CFile::AddKeyword(const char *key){
	if (keywords.find(key) == keywords.end()){
		keywords.insert(string(key));
		keywords_nmb++;
	}
}

void CFile::DelKeyword(const char *key){
	if (keywords.find(key) != keywords.end()){
		keywords.erase(string(key));
		keywords_nmb--;
	}
}

void CFile::DelKeywordByLen(int len){
	for (set<string>::iterator i = keywords.begin(); i != keywords.end(); i++){
		if (i->length() > len){
			keywords.erase(i);
			keywords_nmb--;
		}
	}
}

const CFile& CFile::operator= (const CFile & arg){
	name = arg.name;
	size = arg.size;
	keywords = arg.keywords;
	keywords_nmb = arg.keywords_nmb;
	return *this;
}

bool CFile::operator< (const CFile &arg) const{
	return (name.compare(arg.name) < 0) ? true : false;
}

int CFile::GetSize() const{
	return size;
}

void CFile::SetSize(int newsize){
	size = newsize;
}

int CFile::GetKeywordsNum(){
	return keywords_nmb;
}

void CFile::Print() const{
	cout << name << endl;
	cout << size << endl;
	for(set<string>::iterator i = keywords.begin(); i != keywords.end(); i++)
		cout << *i << ";";
	cout << endl;
	cout << keywords_nmb << endl;
}

void ListAll(const vector<CFile>& vect){
	for (size_t i = 0; i < vect.size(); i++){
		vect[i].Print();
		cout << endl;
	}
}

void AddKeyword(vector<CFile>& vect, char* key){
	for (size_t i = 0; i < vect.size(); i++)
		vect[i].AddKeyword(key);
}

void DelKeyword(vector<CFile>& vect, char* key){
	for (size_t i = 0; i < vect.size(); i++)
		vect[i].DelKeyword(key);
}

void DelKeywordByLen(vector<CFile>& vect, int len){
	for (size_t i = 0; i < vect.size(); i++)
		vect[i].DelKeywordByLen(len);
}

bool compBySize(CFile& vect1, CFile& vect2){
	return (vect1.GetSize() > vect2.GetSize());
}

void SortBySize(vector<CFile>& vect){
	sort(vect.begin(), vect.end(), &compBySize);
}

void SortByName(vector<CFile>& vect){
	sort(vect.begin(), vect.end());
}

int compByKeyNmb(CFile& vect1, CFile& vect2){
	return (vect1.GetKeywordsNum() > vect2.GetKeywordsNum());
}

void SortByKeywordsNumber(vector<CFile>& vect){
	sort(vect.begin(), vect.end(), &compByKeyNmb);
}

vector<CFile> GetFromCSV(string FileName){
	ifstream file(FileName);
	vector<CFile> res;
	string line;
	file >> line;
	while (file){
		CFile tmp;

		regex div("[^;]+");
		smatch match;
		regex_search(line, match, div);
		string name = match[0].str();
		line = match.suffix().str();

		regex_search(line, match, div);
		int len = stoi(match[0].str());
		line = match.suffix().str();

		tmp.SetDetails(name.c_str(), len);
		tmp.SetKeywords(line.c_str());
		res.push_back(tmp);
		file >> line;
	}
	file.close();
	return res;
}