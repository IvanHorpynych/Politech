#include "cArray.h"

 cArray::cArray(int  size) { // �����������  ������� ������� ������
	 mySize = size;
	 vector = new int[mySize];
	 maxIndex = -1;
	   for (int i =0; i< mySize; i++) {
	      vector[i] = 0;
	   }

 }

 cArray::~cArray() { //����������
	 delete [] vector;
 }

 int cArray::GetSize() const {//������� ����� ��������, �� ������ ���� ������� � ������
	 return mySize;
 }

 int cArray::GetCount() const {//������� ����� ��������, ��������� � ������.
	int j =0; 
	 for(int i =0;i < mySize; i++) {
		if (vector[i] != 0) {
		   j++;
		}
	 }
     return j;
 }

 int cArray::GetUpperBound ()  {//������� ��������� ����������� ������ �������.
	 
	 return maxIndex;
 }

 bool cArray::IsEmpty() const   // ���� ������ - true // ������ - false
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

 void cArray::SetSize(int nNewSize) { //���� ������ �������.
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

		 for (int i = mySize; i < nNewSize; i++ ) {   // ���� ��� ������� ���� ������� �������, ��� ��������� ������ (GetUpperBound())
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

 void cArray::FreeExtra()   //������� ������ ���� ���������� ������������ �������
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

 void cArray::RemoveAll() { //������� �� �������� ������� (��������� ����������� ������ - 0).
	 for (int i =0; i < mySize; i++) 
	    vector[i] = 0;	 

	 maxIndex =0;
 }

 int cArray::GetAt(int indx) const { //������� ������� c �������� indx.
    if(indx >= mySize) {
		cout<<"index out of range" ;
       return 0;
	}
	 return vector[indx];
 }

 void cArray::SetAt(int n, int indx) {  //���������� �������� �������� c �������� indx ����� n.	
      vector[indx] = n;
	  if (indx > maxIndex) {
	    maxIndex++;
	  }
 }

 void cArray::Add (int n) //���� ������� � ��������� n � ����� ������� (����� �������� ���������� ������������ �������).
							//���� ������ ������� � ������ ���� � ���� ����� ���������� �� GROWBY.
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

 void cArray::Append(cArray *ar)  //���� �������� ar � ����� �������. ��� ����������� ����� ������� ���������� �� �������� ������ GROWBY
 {
	 if ((mySize - maxIndex +1) >= ar->GetSize()) {
	    for( int i =0; i < ar->GetSize() ; i++ ) {
			vector[i + maxIndex +1] = ar->GetAt(i);  
		}
     }
	 else {
		 int residual = ar->GetSize() - (mySize - maxIndex);  // �������  (ar) ,  ��������� ���� � �������� �����
		 int *tmp = new int [mySize];
		      
		      for (int i =0; i < mySize; i++) {
		            tmp[i] = vector[i];
		       }

            delete [] vector;
		 int growbySum = (residual / GROWBY) +1;  // ����� GROWBY ���� ���� ��������

			vector = new int [mySize + (growbySum * GROWBY)];
			   for (int i =0; i < mySize; i++) {
			     vector[i] = tmp[i];
			   }
			   for (int i =mySize; i < (mySize + (growbySum * GROWBY)); i++) { // ��� ���������
			       vector[i] = 0;
			   } 
			   mySize = mySize + (growbySum * GROWBY);
			      for (int i =0; i < ar->GetSize(); i++) {
					  vector[ i + maxIndex] = ar->GetAt(i);       
				  }

	 }
	 maxIndex = maxIndex + ar->GetSize();
 }

 void cArray::Copy(cArray *ar) //����� ar � �������� ������ � ������� �������� ����� ���� ����� �� �������� ������ GROWBY.
 {
	 int newSize = ((ar->GetSize() / GROWBY )+1) * GROWBY;  // ����� ����� �������
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

 void cArray::InsertAt(int n,int indx) //�������� ������� n � ������� � �������� indx (����� �������� ���������� ������������ �������).
 {
	if ((mySize - maxIndex -1) > 0) {	   
	    for(int i =maxIndex; i >= 3; i++) {   // ���� ��� ������� ������
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

 void cArray::RemoveAt(int indx) { //������� ������� � ������� � �������� indx (����� �������� ���������� ������������ �������).
    vector[indx] = 0;
	maxIndex--;
	
 }

 int& cArray::operator[](int indx)// ���������� ��������� [] 
								//���������/������� �������� �������� � ������� � �������� indx.
 {
    return vector[indx];
 }

 void cArray::Watch() // ���� ������ �� �����
 {
    cout<<"your vector ";
	   for (int i =0; i < mySize; i++) {
			cout<<"  "<<vector[i] ;
		}
       cout<<"\n";
 }



