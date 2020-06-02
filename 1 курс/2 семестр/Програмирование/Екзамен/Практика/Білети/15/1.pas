program _1;

const n=50;

type Vector=array[1..n] of integer;

var A:Vector;

procedure Inp(var A:Vector);
var i:word;
begin
 for i:=1 to n do A[i]:=random(-10,30);
end;

procedure Outp(const A:Vector);
var i:word;
begin
 for i:=1 to n do Write(A[i]:5);
end;

procedure Sort(var A:Vector);
var i,j:word;
    B:integer;
begin
 for i:=n downto 2 do
  for j:=1 to n-1 do
   if A[j]<A[j+1] then
    begin
     B:=A[j];
     A[j]:=A[j+1];
     A[j+1]:=b
    end;    
end;

function Dod(const A:Vector):word;
var i,ind:word;
begin
 ind:=0;
 for i:=1 to n do
  if A[i]>0 then inc(ind);
 Dod:=ind; 
end;

begin
 Inp(A);
 Outp(A);
 if Dod(A)>10 then begin
   Sort(A);
   Writeln;
   Outp(A);
  end; 
end.