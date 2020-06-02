program Labo3;
uses crt;
const n=7;
var R,a,b:integer;
    i:integer;
    Y: array [1..n] of integer;
    Z: array [1..n] of integer;
function sqrS: integer;
var j:integer;
    P:integer;
begin
P:=(-1);
  for j:=1 to i do
  P:=P*P;
sqrS:=P;
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
for i:=1 to n do
   begin
   if (Y[i]>(-20))and(Y[i]<10) then
   begin
      Z[i]:=1-a*Y[i];
      write(z[i]:5);
   end
   else
   begin
      Z[i]:=7-b*Y[i];
      write(z[i]:5);
   end;
   end;
writeln;
R:=0;
for i:=1 to n do
   R:=R+sqrS*(a*Z[i]-3*b);
write('Rezultiruychee znachenie R:',R);
gotoxy(1,6);
readln;   
end.