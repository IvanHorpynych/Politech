unit TALG;
{Modul z algorytmamy sortuvan'}

interface

uses
  TCOM;                          
                                 
  
procedure Alg454(var a: arr);    
procedure Alg459(var a: arr);    
procedure Alg4522(var a: arr);    
procedure Alg4523(var a: arr);   

implementation

type
  TArr=array[1..m] of word;
  
var
  Stolb:TArr;  

{Procedura zapysuvannia j-stovchyku, k-peperizu tryvymirnogo masuvu v dodatkovyy 
vektor Stovb }
procedure InclDod(k,j:word);
var i:word;
begin
 for i:=1 to m do
  Stolb[i]:=A[k,i,j];
end;

{Procedura povernennia elementiv z dodatkovogo
vektoru Stovb u pochatkovyy masyv}
procedure ExclDod(k,j:word);
var i:word;
begin
 for i:=1 to m do
  A[k,i,j]:=Stolb[i];
end;

{Prysvouvannia elementam stobchyka outp elementiv stovbchyka inp}
procedure Change(k,inp,outp:word);
var i:word;
begin
 for i:=1 to m do
  A[k,i,outp]:=A[k,i,inp];
end;


procedure Alg454(var a: arr);
var
 X:word;
 i,j,k,L,R,h:word;
begin
 for h:=1 to p do
  begin
   NewSum(h);
   for i:=2 to n do
    begin
     X:=Sum[i];
     InclDod(h,i);
     L:=1; R:=i;
     while l<R do
      begin
       j:=(L+R) div 2;
       if X>=Sum[j] then
        L:=j+1
       else
        R:=j;
      end;
     for k:=i-1 downto R do
      begin
       Sum[k+1]:=Sum[k];
       Change(h,k,k+1);
      end;
     Sum[R]:=X;
     ExclDod(h,R);
    end;
  end;
end; 

procedure Alg459(var a: arr);
var
 k,i,Min,s,Imin:word;
begin
 for k:=1 to p do
  begin
   NewSum(k);
   for s:=1 to n-1 do
    begin
     Min:=Sum[s];
     InclDod(k,s);
     Imin:=s;
     for i:=s+1 to n do
      if Sum[i]<Min then
       begin
        Min:=Sum[i];
        InclDod(k,i);
        Imin:=i        
       end;
     if Imin<>s then
      begin
       Sum[Imin]:=Sum[s];
       Change(k,s,Imin);
       Sum[s]:=Min;
       ExclDod(k,s);
      end;
    end;
  end;
end; 

procedure Alg4522(var a: arr); 
const
 max_t=(n-1) div 4 + 1;
var
 H:array[1..max_t] of word;
 B,i,j,k,r,v,t:word;
begin
 if n<4 then t:=1
  else t:=trunc(ln(n)/ln(2))-1;
 H[t]:=1;
 for i:= t-1 downto 1 do H[i]:=2*H[i+1]+1;
 for r:=1 to p do
  begin
   NewSum(r);
   for v:=1 to t do
    begin
     k:=H[v];
     for i:=k+1 to n do
      begin
       B:=Sum[i];
       InclDod(r,i);
       j:=i;
       while (j>k)and(B<Sum[j-k]) do
        begin
         Sum[j]:=Sum[j-k];
         Change(r,j-k,j);
         j:=j-k;
        end;
       Sum[j]:=B;
       ExclDod(r,j);
      end;
    end;
  end;
end;



procedure Alg4523(var a: arr);
var k:word;

procedure QSort(k,L,R:word);
var
 B,Tmp,i,j:word;
begin
 B:=Sum[(L+R) div 2];
 i:=L;
 j:=R;
 while i<=j do
  begin
   while Sum[i]<B do i:=i+1;
   while Sum[j]>B do j:=j-1;  
   if i<=j then
    begin
     Tmp:=Sum[i];
     InclDod(k,i);
     Sum[i]:=Sum[j];
     Change(k,j,i);
     Sum[j]:=Tmp;
     ExclDod(k,j);
     i:=i+1;
     j:=j-1;
    end;
  end;
 if L<j then QSort(k,L,j);
 if i<R then QSort(k,i,R);
end; 

begin
 for k:=1 to p do
  begin
   NewSum(k);
   QSort(k,1,n);
  end;
end;

end.