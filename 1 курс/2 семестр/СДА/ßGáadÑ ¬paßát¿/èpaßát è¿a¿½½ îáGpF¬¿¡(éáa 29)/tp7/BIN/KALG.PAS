unit KAlg;

interface

uses
  KOsn;

procedure Alg782(var a: arr);
procedure Alg7812(var a: arr);
procedure Alg7815(var a: arr);
procedure Alg7823(var a: arr);

implementation

procedure Alg782(var a: arr);
var
  i, k, j, b: integer;
begin
{Головна діагональ}
  for k := 1 to p do
  begin    
    for i := 2 to n do
    begin
      b := A[k,i,i];
      j := i;
      while (j > 1) and (b < A[k,j-1,j-1]) do
      begin
        A[k,j,j] := A[k,j-1,j-1];
        j := j - 1;
      end;
      A[k,j,j] := b;
    end;  
  end;
  
{Побічна діагональ}  
for k := 1 to p do
  begin    
    for i := 2 to n do
    begin
      b := A[k,i,n-i+1];
      j := i;
      while (j > 1) and (b < A[k,j-1,n-j+2]) do
      begin
        A[k,j,n-j+1] := A[k,j-1,n-j+2];
        j := j - 1;
      end;
      A[k,j,n-j+1] := b;
    end;  
  end;
  
  
  
end;


procedure Alg7812(var a: arr);

var k, i, L, R, b,imin,imax:integer;

begin
{Головна діагональ}
for k:=1 to p do
  begin
   L:=1; R:=n;
   while L<R do
    begin
     imin:=L; imax:=L;
     for i:=L+1 to R do
      if A[k,i,i]<A[k,imin,imin] then imin:=i
       else 
         if A[k,i,i]>A[k,imax,imax] then imax:=i;
         
     if imin<>L then
      begin
       B:=A[k,imin,imin];
       A[k,imin,imin]:=A[k,L,L];
       A[k,L,L]:=B;
      end;
      
     if imax<>R then
      if imax=l then
       begin
        B:=A[k,imin,imin];
        A[k,imin,imin]:=A[k,R,R];
        A[k,R,R]:=B;
       end
      else
       begin
        B:=A[k,imax,imax];
        A[k,imax,imax]:=A[k,R,R];
        A[k,R,R]:=B;
       end;
     L:=L+1; R:=R-1;
    end;
  end;
  
{ Побічна діагональ} 
  for k:=1 to p do
  begin
   L:=1; R:=n;
   while L<R do
    begin
     imin:=L; imax:=L;
     for i:=L+1 to R do
      if A[k,i,n-i+1]<A[k,imin,n-imin+1] then imin:=i
       else 
         if A[k,i,n-i+1]>A[k,imax,n-imax+1] then imax:=i;
         
     if imin<>L then
      begin
       B:=A[k,imin,n-imin+1];
       A[k,imin,n-imin+1]:=A[k,L,n-L+1];
       A[k,L,n-L+1]:=B;
      end;
      
     if imax<>R then
      if imax=l then
       begin
        B:=A[k,imin,n-imin+1];
        A[k,imin,n-imin+1]:=A[k,R,n-R+1];
        A[k,R,n-R+1]:=B;
       end
      else
       begin
        B:=A[k,imax,n-imax+1];
        A[k,imax,n-imax+1]:=A[k,R,n-R+1];
        A[k,R,n-R+1]:=B;
       end;
     L:=L+1; R:=R-1;
    end;
  end;


end;




procedure Alg7815(var a: arr);
var
  r, i, j, b, k: integer;
begin
{Головна діагональ}  
  for k:=1 to p do
   begin
    r := n;
    while r > 1 do
     begin
      j := 1;
      for i := 1 to r - 1 do
        if A[k,i,i] > A[k,i+1,i+1] then
        begin
          b := A[k,i,i];
          A[k,i,i] := A[k,i+1,i+1];
          A[k,i+1,i+1] := b;
          j:= i;
        end;
       r := j;
     end;
    end;
    
  {Побічна діагональ}   
  for k:=1 to p do
   begin
    r := n;
    while r > 1 do
     begin
      j := 1;
      for i := 1 to r - 1 do
        if A[k,i,n-i+1] > A[k,i+1,n-i] then
        begin
          b := A[k,i,n-i+1];
          A[k,i,n-i+1] := A[k,i+1,n-i];
          A[k,i+1,n-i] := b;
          j:= i;
        end;
       r := j;
     end;
    end;    
    
end;

procedure Alg7823(var a: arr);
var
  k, kk, ii, j: integer;
  {Головна діагональ} 
  procedure QSort(k,l, r: integer);
  var
    b, tmp: integer;
    i, j: integer;
  begin
    b := A[k,((l + r) div 2),((l + r) div 2)];
    i := l;
    j := r;
    while i <= j do
    begin
      while A[k,i,i] < b do
        i := i + 1;
      while A[k,j,j] > b do
        j := j - 1;
      if i <= j then
      begin
        tmp := A[k,i,i];
        A[k,i,i] := A[k,j,j];
        A[k,j,j] := tmp;
        i := i + 1;
        j := j - 1;
      end;
    end;
    if l < j then Qsort(k,l, j);
    if i < r then Qsort(k, i, r);
  end;
  
{Побічна діагональ}   
procedure QSort_Pob(k,l, r: integer);
  var
    b, tmp: integer;
    i, j: integer;
  begin
    b := A[k,((l + r) div 2),n-((l + r) div 2)+1];
    i := l;
    j := r;
    while i <= j do
    begin
      while A[k,i,n-i+1] < b do
        i := i + 1;
      while A[k,j,n-j+1] > b do
        j := j - 1;
      if i <= j then
      begin
        tmp := A[k,i,n-i+1];
        A[k,i,n-i+1] := A[k,j,n-j+1];
        A[k,j,n-j+1] := tmp;
        i := i + 1;
        j := j - 1;
      end;
    end;
    if l < j then Qsort_Pob(k,l, j);
    if i < r then Qsort_Pob(k, i, r);
  end;  

begin 
 for k:=1 to p do
   begin
    Qsort(k,1, n);
    Qsort_Pob(k,1, n);
   end; 
end;    

end.