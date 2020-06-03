#include "stdafx.h"
#include "MultiString.h"


MultiString::MultiString(void)
{
	buf = new char*;
	*buf = new char[1];
	buf[0] = '\0';
	str_nmb = 1;
}

MultiString::MultiString(int n){
	str_nmb = n;
	buf = new char*[n];
	for(int i = 0; i<n; i++){
		buf[i] = new char[1];
		buf[i][0] = '\0';
	}
}

MultiString::MultiString(const MultiString &src ){
	str_nmb = src.GetLength();
	buf = new char*[str_nmb];
	for(int i = 0; i < str_nmb; i++){
		buf[i] = new char[strlen(src.buf[i]) + 1];
		strcpy(buf[i],src.buf[i]);
	}
}


int MultiString:: GetLength( ) const{
	return str_nmb;
}

void MultiString::SetAt( int nindex, const char* str ){
	if(nindex>= str_nmb) return;
	delete [] buf[nindex];
	buf[nindex] = new char[strlen(str) + 1];
	strcpy(buf[nindex],str);
}

int MultiString::Find(const char *pszSub )const{
	for(int i = 0; i<str_nmb; i++){
		if(!strcmp(buf[i],pszSub)) return i;
	}
	return -1;
}


void MultiString::PrintStr(int nIndex) const{
	if(IsEmpty()) return;
	cout << buf[nIndex];
	cout<<endl;
}

void MultiString::PrintAllStr() const{
	for(int i = 0; i<str_nmb; i++){
		PrintStr(i);
	}
}

void MultiString::PushBack(const char *str){
	if (!IsEmpty()){
		char **tmp = new char*[str_nmb + 1];
		for(int i = 0; i<str_nmb;i++){
			tmp[i] = new char[strlen(buf[i]) + 1];
			strcpy(tmp[i],buf[i]);
		}
		tmp[str_nmb] = new char[strlen(str) + 1];
		strcpy(tmp[str_nmb], str);
		for(int i = 0; i<str_nmb;i++){
			delete [] buf[i];
		}
		delete [] buf;
		str_nmb+=1;
		buf = new char*[str_nmb];
		buf = tmp;
	}else{
		buf = new char*;
		str_nmb = 1;
		buf[0] = new char[strlen(str) + 1];
		strcpy(buf[0], str);
	}

}

char* MultiString:: operator[](int nindex) const{
	if(IsEmpty()) return NULL;
	return buf[nindex];
}

MultiString* MultiString::MergeMultiStringexclusive(const MultiString &src){
	if(IsEmpty() || src.IsEmpty()) return this;
	MultiString *tmp = new MultiString();
	tmp->SetAt(0,buf[0]);
	for(int i = 1; i<str_nmb; i++){
		if(tmp->Find(buf[i]) == -1) tmp->PushBack(buf[i]);
	}

	for(int i = 0; i<src.GetLength(); i++){
		if(tmp->Find(src[i]) == -1) tmp->PushBack(src[i]);
	}
	return tmp;

}

void MultiString::Empty(){
	if(IsEmpty()) return;
	for(int i = 0; i < str_nmb; i++){
		delete buf[i];
	}
	delete [] buf;
	str_nmb = 0;
}


MultiString::~MultiString(void)
{
	for(int i = 0; i < str_nmb; i++){
		delete buf[i];
	}
	delete [] buf;
}

