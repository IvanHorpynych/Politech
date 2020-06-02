program Laboratornaya41;
uses crt;
type vector= array [1..30] of real;
const n=5;
var i,j,k: integer;
    A,B: vector;
procedure transponade_vector( x,y: integer; var M,P: vector);
   begin
          k:=n;
          for i:=x to y do
          begin
          P[k]:=M[i];
          k:=k-1;
          end;
               k:=1;
               for i:=x to y do
               begin
               M[i]:=P[k];
               k:=k+1;
               write(M[i]:6:1);
               end;
   writeln;
   end;
procedure write_vector(M: vector; x,y:integer);
begin
for i:=x to y do
write (M[i]:6:1);
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
k:=1;
for i:=((3*n)+1) to (4*n) do
begin
     B[k]:=A[i];
     k:=k+1;
end;
for i:=1 to n do
     A[(3*n)+i]:=A[i];

for i:=1 to n do
    begin
    A[i]:=B[i];
    write(A[i]:6:1);
    end;
    writeln;
transponade_vector(n+1,2*n,A,B);
transponade_vector(((2*n)+1),3*n,A,B);
transponade_vector(((3*n)+1),4*n,A,B);
readln;
end.