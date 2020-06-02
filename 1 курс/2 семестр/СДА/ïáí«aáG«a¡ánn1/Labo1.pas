program Labo1;
const n=8; m=9;
var A: array [1..n,1..m] of integer;
    L,R,i,j,x: integer; 
begin
write('Введите искомое число:');

for i:=1 to n do
  for j:=1 to m do
A[i,j]:=(n+m+10)-i-j;
A[i,j]:=A[i-1,j];
A[1,2]:=A[1,1];
for i:=1 to n do
begin
writeln;
  for j:=1 to m do
  write(A[i,j]:3);
end;  
writeln;
readln(X);
L:=1; R:=m;
  while L<R do
  begin 
    j:=((L+R) div 2);
    if A[1,j]>x then
      L:=j+1
      else
        R:=j;
  end;
if A[1,R]=x then  
  writeln('Символ найден в первом рядке на позиции [1,',R,']')
  else
  writeln('Символ не найден в первом рядке!');
  
L:=1; R:=n;
  while L<R do
  begin 
    i:=((L+R) div 2);
    if A[i,m]>x then
      L:=i+1
        else
        R:=i;
  end;
if A[R,m]=x then  
  writeln('Символ найден в последнем солбце на позиции [',r,',',m,']')   
  else
  writeln('Символ не найден в последнем столбце!');
  readln;
end.  