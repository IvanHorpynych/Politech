unit UOBH;

interface

uses
  UGlob;


procedure Obh218(var a: arr);
procedure Obh228(var a: arr);
procedure Obh238(var A:arr);


implementation



procedure Obh218(var a: arr);

var ii,k,j, i, L, R, b,imin,imax:integer;
    Z:array[1..m*n] of integer;

begin
 ii := 1;                      
 for j := 1 to n do
  for i := 1 to m do
   begin
    Z[ii] := a[k, i, j];
    inc(ii);
   end;
   
   L:=1; R:=m*n;
   while L<R do
    begin
     imin:=L; imax:=L;
     for i:=L+1 to R do
      if Z[i]<Z[imin] then imin:=i
       else 
         if Z[i]>Z[imax] then imax:=i;
         
       B:=Z[imin];
       Z[imin]:=Z[L];
       Z[L]:=B;
      
      if imax=L then
       begin
        B:=Z[imin];
        Z[imin]:=Z[R];
        Z[R]:=B;
       end
      else
       begin
        B:=Z[imax];
        Z[imax]:=Z[R];
        Z[R]:=B;
       end;
     L:=L+1; R:=R-1;
    end;
    
 ii := 1;                      
 for j := 1 to n do
  for i := 1 to m do
   begin
    Z[ii] := a[k, i, j];
    inc(ii);
   end;  
    
end; 


procedure Obh228(var a: arr);

var k,j, i, L, R, b,imin,imax:integer;

begin
 for k:=1 to p do 
  begin
   L:=1; R:=m*n;
   while L<R do
    begin
     imin:=L; imax:=L;
     for i:=L+1 to R do
      if A[k,((i-1) mod m)+1,((i-1) div m)+1]<A[k,((imin-1) mod m)+1,((imin-1) div m)+1] then imin:=i
       else 
         if A[k,((i-1) mod m)+1,((i-1) div m)+1]>A[k,((imax-1) mod m)+1,((imax-1) div m)+1] then imax:=i;
         
       B:=A[k,((imin-1) mod m)+1,((imin-1) div m)+1];
       A[k,((imin-1) mod m)+1,((imin-1) div m)+1]:=A[k,((L-1) mod m)+1,((L-1) div m)+1];
       A[k,((L-1) mod m)+1,((L-1) div m)+1]:=B;
      
      if imax=L then
       begin
        B:=A[k,((imin-1) mod m)+1,((imin-1) div m)+1];
        A[k,((imin-1) mod m)+1,((imin-1) div m)+1]:=A[k,((R-1) mod m)+1,((R-1) div m)+1];
        A[k,((R-1) mod m)+1,((R-1) div m)+1]:=B;
       end
      else
       begin
        B:=A[k,((imax-1) mod m)+1,((imax-1) div m)+1];
        A[k,((imax-1) mod m)+1,((imax) div m)+1]:=A[k,((R-1) mod m)+1,((R-1) div m)+1];
        A[k,((R-1) mod m)+1,((R-1) div m)+1]:=B;
       end;
     L:=L+1; R:=R-1;
    end;
   end; 
    
 
    
end;

procedure Obh238(var A:arr);
var ind, L, R, B, i, j,k:integer;
    imin, imax, jmin, jmax:integer;
begin
 for k:=1 to p do
  begin
   ind:=1;
   L:=1; R:=m;
   while ind<=((n+1) div 2) do
    begin     
     imin:=L; jmin:=ind; imax:=L; jmax:=ind; 
   
     for i:=L to m do
      if A[k,i,ind]<A[k,imin,ind] then
       imin:=i
      else if A[k,i,ind]>A[k,imax,ind] then
       imax:=i;
       
     for j:=ind+1 to n-ind do
      for i:=1 to m do
       if A[k,i,j]<A[k,imin,jmin] then
        begin
         imin:=i;
         jmin:=j;
        end 
       else if A[k,i,j]>A[k,imax,jmax] then
        begin
         imax:=i;
         jmax:=j;
        end;   
        
      for i:=1 to R do
       if A[k,i,n-ind+1]<A[k,imin,jmin] then
        begin
         imin:=i;
         jmin:=n-ind+1;
        end 
       else if A[k,i,n-ind+1]>A[k,imax,jmax] then
        begin
         imax:=i;
         jmax:=n-ind+1;
        end;
        
      B:=A[k,imin,jmin];
      A[k,imin,jmin]:=A[k,L,ind];
      A[k,L,ind]:=B;
      
      if (imax=L)and(jmax=ind) then
       begin
        B:=A[k,imin,jmin];
        A[k,imin,jmin]:=A[k,R,n-ind+1];
        A[k,R,n-ind+1]:=B;
       end
      else
       begin
        B:=A[k,imax,jmax];
        A[k,imax,jmax]:=A[k,R,n-ind+1];
        A[k,R,n-ind+1]:=B;
       end;
     L:=L+1;
     R:=R-1;     
     
     if (L>m) then
      begin
       inc(ind);
       L:=1;
       R:=m;
      end;
      
     if (ind=(n+1)div 2)and((n mod 2) = 1)and(L=R) then inc(ind);
    end;
  end;
 
end;

 

  
  

end.