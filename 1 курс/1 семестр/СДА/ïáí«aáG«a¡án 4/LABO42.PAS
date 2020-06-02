program Laboratornaya42;
uses crt;
type vector= array [1..30] of integer;
const n=5;
var i,j: integer;
    P: integer;
    A: vector;
procedure transponade_vector( x,y: integer; var M: vector);
   begin
          for i:=x*n+1 to (x*n+(n div 2)) do
          begin
          P:=M[i];
          M[i]:=M[y*n-i+1];
          M[y*n-i+1]:=P;
          end;
   begin
   for i:=((x*n)+1) to ((x+1)*n) do
   write (M[i]:5);
   writeln;
   end;
   end;
procedure write_vector(M: vector; x,y:integer);
begin
for i:=x to y do
write (M[i]:5);
writeln;
end;

begin
clrscr;
randomize;
for i:=1 to (4*n) do
begin
    A[i]:=random(100);
    if random(2)=1 then
    A[i]:=(A[i]*(-1));
end;
writeln('Nachalniy vector A:');
write_vector (A,1,n);
write_vector (A,(n+1),2*n);
write_vector (A,(2*n)+1,3*n);
write_vector (A,(3*n+1),4*n);
writeln;
writeln('Preobrazovanniy vector A:');
for i:=1 to n do
begin
    P:=A[i];
    A[i]:=A[4*n-i+1];
    A[4*n-i+1]:=P;
    end;
for i:=1 to (n div 2) do
begin
    P:=A[i];
    A[i]:=A[n-i+1];
    A[n-i+1]:=P;
end;
for i:=1 to n do
    write (A[i]:5);
    writeln;
transponade_vector(1,3,A);
transponade_vector(2,5,A);

for i:=3*n+1 to 4*n do
write (A[i]:5);
readln;
end.