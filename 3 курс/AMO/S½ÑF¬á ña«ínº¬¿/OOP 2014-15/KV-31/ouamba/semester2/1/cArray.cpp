#include "cArray.h"

 cArray::cArray(int  size) { // конструктор  створює порожній вектор
	 mySize = size;
	 vector = new int[mySize];
	 maxIndex = -1;
	   for (int i =0; i< mySize; i++) {
	      vector[i] = 0;
	   }

 }

 cArray::~cArray() { //деструктор
	 delete [] vector;
 }

 int cArray::GetSize() const {//повертає число элементів, які можуть бути розміщені у векторі
	 return mySize;
 }

 int cArray::GetCount() const {//повертає число элементів, розміщених у векторі.
	int j =0; 
	 for(int i =0;i < mySize; i++) {
		if (vector[i] != 0) {
		   j++;
		}
	 }
     return j;
 }

 int cArray::GetUpperBound ()  {//повертає найбільший припустимий індекс вектора.
	 
	 return maxIndex;
 }

 bool cArray::IsEmpty() const   // якщо пустий - true // повний - false
 {
    int j =0;
	if (vector == NULL) 
		return true;
    
	for (int i =0; i < mySize; i++) {
		if (vector[i]) 
			j =1;
	}

	if (j)
		return false;
	else  return true;
 }

 void cArray::SetSize(int nNewSize) { //зміна розміру вектора.
	 int *tmp = new int[mySize];
	     for (int i =0; i < mySize; i++) {
	       tmp[i] = vector[i];
	     }
	   delete [] vector;
	   vector = new int[nNewSize];
	  
	   if ( mySize < nNewSize) {	 
		 for (int i =0; i < mySize; i++) {
	       vector[i] = tmp[i]; 
		 }

		 for (int i = mySize; i < nNewSize; i++ ) {   // цикл для нульової нової частини вектора, для корректної роботи (GetUpperBound())
			 vector[i] = 0;							
		 }
	   }
	   else {
		 for (int i =0; i < nNewSize; i++) {
	       vector[i] = tmp[i]; 
		 }
	   }
		 mySize = nNewSize;
 }

 void cArray::FreeExtra()   //звільняє пам’ять вище найбільшого припустимого індексу
 {
    int *tmp = new int[mySize];
	   for(int i =0; i< mySize; i++) {
		   tmp[i] = vector[i];
	   }
	   delete [] vector;
	   vector = new int [maxIndex +1];
	      for(int i =0; i < maxIndex +1; i++) 
			  vector[i] = tmp[i];

	mySize = maxIndex +1;
 }

 void cArray::RemoveAll() { //видаляє всі елементи вектора (найбільший припустимий індекс - 0).
	 for (int i =0; i < mySize; i++) 
	    vector[i] = 0;	 

	 maxIndex =0;
 }

 int cArray::GetAt(int indx) const { //повертає елемент c індексом indx.
    if(indx >= mySize) {
		cout<<"index out of range" ;
       return 0;
	}
	 return vector[indx];
 }

 void cArray::SetAt(int n, int indx) {  //установлює значення елемента c індексом indx рівним n.	
      vector[indx] = n;
	  if (indx > maxIndex) {
	    maxIndex++;
	  }
 }

 void cArray::Add (int n) //додає елемент зі значенням n у кінець вектора (змінює значення найбільшого припустимого індексу).
							//Якщо вільних позицій у векторі немає – його розмір збільшується на GROWBY.
 {
	 if (maxIndex == (mySize -1)) {
		 int *tmp = new int[mySize];
	         for(int i =0; i < mySize; i++){
			      tmp[i] = vector[i];
			 }
		   delete [] vector;
		 
		   vector = new int[mySize + GROWBY];
	         for (int i =0; i < mySize; i++) {
			     vector[i] = tmp[i];
			 }
			 for (int i =mySize; i < mySize + GROWBY; i++) {
			     vector[i] = 0;
			 }
			 
			 mySize = mySize + GROWBY;
			 maxIndex++;
			 vector[maxIndex] = n;
	 }
	 else {
	    vector[maxIndex +1] = n;
	 }
	 
	 maxIndex++;
 }

 void cArray::Append(cArray *ar)  //додає елементи ar у кінець вектора. При необхідності розмір вектора збільшується на значення кратне GROWBY
 {
	 if ((mySize - maxIndex +1) >= ar->GetSize()) {
	    for( int i =0; i < ar->GetSize() ; i++ ) {
			vector[i + maxIndex +1] = ar->GetAt(i);  
		}
     }
	 else {
		 int residual = ar->GetSize() - (mySize - maxIndex);  // частина  (ar) ,  побивинна бути в доданому масиві
		 int *tmp = new int [mySize];
		      
		      for (int i =0; i < mySize; i++) {
		            tmp[i] = vector[i];
		       }

            delete [] vector;
		 int growbySum = (residual / GROWBY) +1;  // номер GROWBY який буде потрібний

			vector = new int [mySize + (growbySum * GROWBY)];
			   for (int i =0; i < mySize; i++) {
			     vector[i] = tmp[i];
			   }
			   for (int i =mySize; i < (mySize + (growbySum * GROWBY)); i++) { // для обнулення
			       vector[i] = 0;
			   } 
			   mySize = mySize + (growbySum * GROWBY);
			      for (int i =0; i < ar->GetSize(); i++) {
					  vector[ i + maxIndex] = ar->GetAt(i);       
				  }

	 }
	 maxIndex = maxIndex + ar->GetSize();
 }

 void cArray::Copy(cArray *ar) //копіює ar у поточний вектор зі змінюючи належним чином його розмір на значення кратне GROWBY.
 {
	 int newSize = ((ar->GetSize() / GROWBY )+1) * GROWBY;  // новий розмір вектора
	    delete [] vector;

		vector = new int [newSize];
		  for (int i =0; i < ar->GetSize(); i++) { 
			  vector[i] = ar->GetAt(i);
		  }
        
		if (newSize > ar->GetSize() ) {
		    for (int i =ar->GetSize(); i < newSize; i++) {
			    vector[i] = 0;
			}
		}
        mySize = newSize;
		maxIndex = ar->GetSize() -1;
 }

 void cArray::InsertAt(int n,int indx) //вставляє елемент n у позицію з індексом indx (змінює значення найбільшого припустимого індексу).
 {
	if ((mySize - maxIndex -1) > 0) {	   
	    for(int i =maxIndex; i >= 3; i++) {   // цикл для правого сдвигу
	       vector[i +1] = vector[i];
	    }

        vector[indx] = n;
	}
	else {     
	   int *tmp = new int[mySize];
	      for(int i =0; i< mySize; i++) {
	        tmp[i] = vector[i];
	      }
        delete [] vector;
	   
	  vector = new int[mySize + GROWBY];
	    for(int i =maxIndex; i >= 3; i++) { 
	       tmp[i +1] = tmp[i];
	    }
	  tmp[indx] = n;
	     for (int i =0; i < mySize; i++) {
		     vector[i] = tmp[i];
		 }
      
		 mySize = mySize + GROWBY; 
	}
	maxIndex++;
 }

 void cArray::RemoveAt(int indx) { //видаляє елемент у позиції з індексом indx (змінює значення найбільшого припустимого індексу).
    vector[indx] = 0;
	maxIndex--;
	
 }

 int& cArray::operator[](int indx)// перегрузка оператора [] 
								//втановлює/повертає значення елемента в позиції з індексом indx.
 {
    return vector[indx];
 }

 void cArray::Watch() // вивід масиву на екран
 {
    cout<<"your vector ";
	   for (int i =0; i < mySize; i++) {
			cout<<"  "<<vector[i] ;
		}
       cout<<"\n";
 }



