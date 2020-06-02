uses crt;
var
f:text;
a:array[1..100]of string;
i,j,k:integer;
st,t:string;
begin
assign(f,'input.txt');
reset(f);
read(f,st);
close(f);
writeln(st);
st:=' '+st;
for i:=1 to length(st)do
if st[i]=' 'then inc(k) else a[k]:=a[k]+st[i];
for i:=1 to k do
 for j:=2 to k do
 if a[j]<a[j-1] then
 begin
 t:=a[j];
 a[j]:=a[j-1];
 a[j-1]:=t;
 end;
 for i:=1 to k do
 writeln(a[i]);
readln;
end.