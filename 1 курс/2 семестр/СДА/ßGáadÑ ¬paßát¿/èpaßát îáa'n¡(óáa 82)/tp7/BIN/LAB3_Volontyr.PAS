program lab3proga;
uses crt;
var i,j,n:integer;
    u:real;
a:array[1..10,1..10] of real;
x:array[1..10] of real;
function f:real;
begin
f:=(3.7-j-2)*(i-4.3*j)-4.7*i;
end;
procedure matrix;
begin
for i:=1 to n do
    begin
         for j:=1 to n do
             begin
                  a[i,j]:=f;
                  write(a[i,j]:8:3);
             end;
         writeln;
    end;
end;
procedure vectorX;
var max,s,d:real;
    k:integer;
begin
max:=0; k:=1;
for j:=1 to n do
    max:=max+a[1,j];
for i:=1 to n do
    begin
         s:=0;
         for j:=1 to n do
             s:=s+a[i,j];
         if s>max then k:=i;
    end;
for j:=1 to n do
    x[j]:=a[k,j];
write('X={');
for i:=1 to n do
    begin
    for j:=i-1 to n-1 do
         if x[j]>x[j+1] then begin
                                  d:=x[i];
                                  x[i]:=x[i+1];
                                  x[i+1]:=d;
                             end;
         write(x[i]:9:3);
    end;
writeln('}');
end;
procedure maxU;
begin
u:=(abs(2*x[1]+1)-abs(x[1]));
for i:=1 to n do
    if (abs(2*x[i]+1)-abs(x[i]))>u then u:=(abs(2*x[i]+1)-abs(x[i]));
writeln('u=',u:6:3);
end;
begin
clrscr;
write('vvedite n ');
readln(n);
f;
matrix;
writeln;
vectorX;
writeln;
maxU;
end.