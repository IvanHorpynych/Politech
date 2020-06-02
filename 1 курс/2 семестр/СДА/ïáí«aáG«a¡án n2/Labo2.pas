program Labo2;
const n=8;
type matrix=array [1..n,1..n] of integer;
var a:matrix; i,j:integer;
procedure vivod(a:matrix);
var i,j:integer;
begin
for i:=1 to n do
 begin
  for j:=1 to n do
  write(a[i,j]:3);
  writeln;
 end;
end;
procedure sort(var a:matrix);
var i,j,s,imax,max:integer;
begin
for s:=1 to (n-1) do
begin
  if S mod 2=0 then
    begin
    max:=A[s,1]; imax:=s; 
    for i:=S+1 to n do
      begin
      if (i mod 2=0)then
        if A[i,1]>max then
        begin
        max:=A[i,1];
        imax:=i;
        end;
      end;
A[imax,1]:=A[S,1];
A[s,1]:=max;  
    end;    
end;
end;
begin 
randomize;
for i:=1 to n do 
 for j:=1 to n do 
  a[i,j]:=random(20);
writeln('Started matrix:');  
vivod(a);
sort(a);
writeln;
writeln('Sorted matrix:');
vivod(a);
readln;
end.

