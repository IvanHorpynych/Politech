// toys.cpp
// Основна частина програми

#include "toys.h"
int main()
{
const char filename[] = "1.txt";
const int n_record = 3;
Toys M[n_record];
char buf[len+1];
char someName[len_name+1];

ifstream fin(filename);
if (!fin) { cout<< "File error\n"; return 1;}

// читання файлу запис за записом у буфер buf  з наступним їх збереженням у масиві M  
int i=0;
while (fin.getline(buf, len)) {
	if(i>= n_record) { cout<< "Too long File \n"; return 1;}
	//buf[len]='\0';
	//cout << "\n" << buf;
	M[i].SetName(buf);
	M[i].SetCode(buf);
	M[i].SetPrice(buf);
	M[i].SetN(buf);
	M[i].Print( );
	i++;
}

int k=i,  n_toys=0;
float average_price=0;
while (1) {
	cout<<"\nEnter name or word end: ";
cin>>someName;
if(strcmp(someName, "end")==0) break;
	int found=0;
   for (i=0; i<k; i++)
   { //cout<< "\n" << M[i].GetPrice();
	if (M[i].FindName(someName))
	{
	      M[i].Print( );
	      n_toys++;    average_price+= M[i].GetPrice();
	      found=1;  
	}
   }
  if(!found) cout<< "\nNo toy\n";
}
if(n_toys) cout<< "\nAverage price = " << average_price/n_toys << "\n";

//system("pause"); 
return 0;

}

