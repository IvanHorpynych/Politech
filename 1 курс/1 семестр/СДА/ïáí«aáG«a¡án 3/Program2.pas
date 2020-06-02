program Labo3;
uses crt;
const n=7;
type
    vector= array [1..n] of integer;
var P,a,b:integer;
    i:integer;
    Y: vector;
    Z: vector;
    R: real;
function Fy(K:integer):integer;
 begin
   if (K>(-20))and(K<10) then
      Fy:=1-a*K
   else
      Fy:=7-b*K;
 end;
 
procedure vector_Z(L:vector; var Z:vector);
var K,i:integer;
begin 
for i:=1 to n do
begin
Z[i]:=Fy(L[i]);
end;
end;

function Rm(Z:vector):real;
var i,j:integer;
begin
R:=0;
for i:=1 to n do
begin
P:=(-1);
  for j:=1 to i do
  P:=P*P;
   R:=R+P*(a*Z[i]-3*b);
   end;
   Rm:=r;
end;
begin
randomize;
write('Vvedite A:');
readln(a);
write('Vvedite B:');
readln(b);
write('Vector Y:');
for i:=1 to n do
  begin
  Y[i]:=random(50)-25;
  write(y[i]:5);
  end;
  writeln;
  write('Vector Z:');
  vector_z(Y,Z);
for i:=1 to n do
   write(Z[i]:5);
writeln;
R:=0;
for i:=1 to n do
  R:=Rm(z);
write('Rezultiruychee znachenie R:',R);
gotoxy(1,6);
readln;   
end.