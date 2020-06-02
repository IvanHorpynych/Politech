program Labo5;
uses crt;
const m=7; n=10;
var A: array [1..m,1..n] of integer;
    i,j,posi,posj: byte;
    min:integer;
begin
clrscr;
randomize;
for i:=1 to m do
    begin
    for j:=1 to n do
        begin
        A[i,j]:=random(200)-100;
        write(A[i,j]:5);
        end;
    writeln;
    end;
min:=A[m,n];
posi:=m;
posj:=n;
for j:=n downto 1 do
    begin
    for i:=m downto 1 do
        begin
        if A[i,j]<=min then
           begin
           min:=A[i,j];
           posi:=i;
           posj:=j;
           end;
        end;
    end;
write('Min element:',min,'; Koordynaty: i=',posi,', j=',posj);
readln;
end.