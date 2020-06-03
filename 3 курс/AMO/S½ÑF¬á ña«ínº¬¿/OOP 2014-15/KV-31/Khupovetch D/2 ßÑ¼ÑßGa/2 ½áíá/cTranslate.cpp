#include "CTranslate.h"

CTranslate::CTranslate()
{
	eng = NULL;
	ita = NULL;
}

CTranslate::CTranslate(const CTranslate& elem)
{
	if (this != &elem)
	{
		eng = new char[strlen(elem.eng) + 1];
		ita = new char[strlen(elem.ita) + 1];
		strcpy(eng, elem.eng);
		strcpy(ita, elem.ita);
	}
}

CTranslate::~CTranslate()
{
	delete[] eng;
	delete[] ita;
	eng = NULL;
	ita = NULL;
}

bool CTranslate::operator<(const CTranslate& elem)
{
	return (strcmp(this->eng, elem.eng) < 0);
}

CTranslate& CTranslate::operator=(const CTranslate& elem)
{
	if (&elem != this)
	{
		delete[]eng;
		delete[]ita;
		if (elem.eng != NULL)
		{
			eng = new char[strlen(elem.eng) + 1];
			strcpy(eng, elem.eng);
		}
		else eng = NULL;
		if (elem.ita != NULL)
		{
			ita = new char[strlen(elem.ita) + 1];
			strcpy(ita, elem.ita);
		}
		else ita = NULL;
		return *this;
	}
	return *this;
}

void CTranslate::MakePair(char *en, char *it)
{
	if (en != eng)
	{
		delete[] eng;
		eng = NULL;
		eng = new char[strlen(en) + 1];
		strcpy(eng, en);
	}
	if (it != ita)
	{
		delete[] ita;
		ita = NULL;
		ita = new char[strlen(it) + 1];
		strcpy(ita, it);
	}
}

char* CTranslate::GetEng()const
{
	return eng;
}

char* CTranslate::GetIta()const
{
	return ita;
}

void CTranslate::Print()const
{
	if (eng != NULL)
	{
		cout << "English word: " << eng << endl;
	}
	else
	{
		cout << "This english word don`t found\n";
	}
	if (ita != NULL)
	{
		cout << "Italy word: " << ita << endl;
	}
	else
	{
		cout << "This italy word don`t found\n";
	}
	cout << "--------------------------------\n";
}

void FillList(list<CTranslate> *list_Trans, char* word)
{
	FILE *File_word;
	CTranslate *help;

	char *engl = new char[50];
	char *ital = new char[50];
	char *help_str = new char[50];

	if (list_Trans == NULL)
		list_Trans = new list<CTranslate>();

	File_word = fopen(word, "r");
	if (File_word == NULL) 
		return;

	while (!feof(File_word)) 
	{
		help = new CTranslate();
		fgets(help_str, 50, File_word);
		strcpy(ital, help_str);
		engl = strtok(help_str, ";");
		strcpy(ital, ital + strlen(engl) + 1);
		ital[strlen(ital)] = '\0';
		help->MakePair(engl, ital);
		list_Trans->push_back(*help);
		delete(help);
		help = NULL;
	}
}

void ListAll(list<CTranslate> *list_Trans)
{
	for (list<CTranslate>::iterator i = list_Trans->begin(); i != (list<CTranslate>::iterator) list_Trans->end(); i++)
	{
		i->Print();
	}
	return;
}

void DelByEng(list<CTranslate> *list_Trans, const char *en)
{
	list<CTranslate>::iterator i = list_Trans->begin();
	while (i != (list<CTranslate>::iterator) list_Trans->end())
	{
		if (strcmp(en, i->GetEng()) == 0)
		{
			list<CTranslate>::iterator del = i;
			i++;
			list_Trans->erase(del);
			continue;
		}
		i++;
	}
	return;
}

void DelByIta(list<CTranslate> *list_Trans, const char *ital)
{
	list<CTranslate>::iterator i = list_Trans->begin();
	while (i != (list<CTranslate>::iterator) list_Trans->end())
	{
		if (strcmp(ital, i->GetIta()) == 0)
		{
			list<CTranslate>::iterator del = i;
			i++;
			list_Trans->erase(del);
			continue;
		}
		i++;
	}
	return;
}

void SortByEng(list<CTranslate> *list_Trans)
{
	list_Trans->sort();
	return;
}

void SortByIta(list<CTranslate> *list_Trans)
{
	list_Trans->sort();
	return;
}

void TranslateEng(list<CTranslate> *list_Trans, const char *en)
{
	for (list<CTranslate>::iterator i = list_Trans->begin(); i != (list<CTranslate>::iterator) list_Trans->end(); i++)
	{
		if (strcmp(en, i->GetEng())== 0)
			cout << i->GetIta() << endl;
		return;
	}
}

void TranslateIta(list<CTranslate> *list_Trans, const char *ita)
{
	for (list<CTranslate>::iterator i = list_Trans->begin(); i != (list<CTranslate>::iterator) list_Trans->end(); i++)
	{
		if (strcmp(ita, i->GetIta())== 0)
			cout << i->GetEng() << endl;
		return;
	}
}
