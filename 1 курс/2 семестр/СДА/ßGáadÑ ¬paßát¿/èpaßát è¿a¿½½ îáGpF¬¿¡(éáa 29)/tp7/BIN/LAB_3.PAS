program Lab_3;
uses crt;
const n=6;
var A:array[1..n,1..n]of double;
    X:array[1..n]of double;
    i,j,mIndex:integer;
    U,Dob:double;
begin
 clrscr;
 Writeln('Array A:');
 Writeln; 
 for i:=1 to n do
  begin
    for j:=1 to n do
      begin
       A[i,j]:=(abs(j-3)-1.3)*(6.5-j)+2*abs(33-i)-1.5;   {Заповнення матриці А}
       Write(A[i,j]:3:2,' ');
      end;
    Writeln;  
  end;
 Writeln;  
  
 U:=0; 
 Writeln('Array X:');                              {Заповнення вектора Х}
 for i:=1 to n do
  begin
   mIndex:=1;
   for j:=1 to n do
   if A[i,mIndex]<A[i,j] then mIndex:=j;  {Знаходження максимального елемента рядка}    
   Dob:=0;
   for j:=1 to n do
    Dob:=Dob+A[i,j]*A[j,mIndex];            {Знаходження скалярного добутку}
   X[i]:=Dob;
   Write(X[i]:8:3,' ');
   U:= U + X[i]*X[n-i+1];                     {Знаходження U}
  end;  
  Writeln;
  Writeln('U=', U:10:3);
  Readln;
end.  