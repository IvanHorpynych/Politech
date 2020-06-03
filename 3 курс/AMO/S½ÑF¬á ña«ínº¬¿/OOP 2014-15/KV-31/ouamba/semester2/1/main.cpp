#include "cArray.h"

 int main() 
 {
	 int size =0, elem =0;
	cArray a(11);   // створення обьектів класу
	cArray b(15);
	
	
	cout<<"size of array= "<<a.GetSize()<<"\n"; 
	cout<<"IS empty "<<a.IsEmpty()<<"\n" ;
		for (int i =0; i < a.GetSize(); i++) {   // заповнення вектору 
		   a.SetAt(i,i);
	    }

		// тестування методів классу cArray

	cout<<"IS empty "<<a.IsEmpty()<<"\n" ;
	cout<<"number of elements = "<<a.GetCount()<<"\n" ;
	cout<<"largest valid idnex = "<<a.GetUpperBound()<<"\n" ;
	a.Watch();
	a.SetSize(14);
	cout<<"size after change = "<<a.GetSize()<<"\n" ;
	cout<<"largest valid idnex = "<<a.GetUpperBound()<<"\n" ;
    a.FreeExtra();
	cout<<"size after free memory above the current upper bound = "<<a.GetSize()<<"\n" ;
	a.Watch();
	cout<<"what element do you whant to watch ? " ;
	cin>> elem ;
	cout<<"element "<<elem<<" = "<<a.GetAt(elem)<<"\n" ;
	a.SetAt(77,5);
	a.Watch();
	a.Add(11);
	a.Watch();
	cout<<"size of array= "<<a.GetSize()<<"\n"; 
	cout<<"largest valid idnex = "<<a.GetUpperBound()<<"\n" ;
	a.Append(&b);
	a.Watch();
	cout<<"size of array= "<<a.GetSize()<<"\n"; 
	    for (int i =0; i < b.GetSize(); i++) {   // заповнення вектору випадковими числами
			b.SetAt(rand()%20 +1,i);
		}
	a.Copy(&b);
	cout<<"size of array after copy b to a= "<<a.GetSize()<<"\n"; 
	a.Watch();
	a.RemoveAt(rand()%a.GetSize() +1);
	a.Watch();
	a.RemoveAll();
	a.Watch();

    return 0;
 }