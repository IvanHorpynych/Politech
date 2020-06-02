program Laboratornaja2;
uses crt;
var x,y: real;
    i,n,check: integer;
    ch: char;
const a=-pi/3;
      b=2*pi;
function f1 (y:real):real;
begin
   f1:=1+exp(cos(x));
end;

function f2 (y:real):real;
begin
   f2:=9+cos(x);
end;

function f3 (y:real):real;
begin
   f3:=(sin(x)*sin(x))/(cos(x)*cos(x))+4*x;
end;

begin
repeat
clrscr;
repeat
  writeln;
  writeln ('Vvedite znachenye peremennoy "n":');
  write (' n=');
  {$I-}
  readln (n);
  check:=IOresult;
  if (check<>0)or(n<=0) then writeln('Vvedeno nevernoye znacheniye "n"!');
until (check=0)and(n>0);
  writeln (' ||=================================================================||');
  writeln (' ||','Funkcii odnoy peremennoy':45,'||':22);
  writeln (' ||=================================================================||');
  writeln (' || Argument X | 1+exp(cos(x))  |  9+cos(x) |   (sin(x)*sin(x))/    ||');
  writeln (' ||            |                |           |  (cos(x)*cos(x))+4*x  ||');
  writeln (' ||=================================================================||');
  for i:=0 to n do
  begin
      x:=a+i*((b-a)/n);
    write(' || ', x:9:4,'  | ',f1(x):11:7,'    | ',f2(x):8:5,'  |');
     if (cos(x)<>0) then
    begin
      writeln (f3(x):16:6,'       ||')
    end
     else writeln ('          --          ||');
    writeln (' ||-----------------------------------------------------------------||');

  end;
  writeln (' ||','Vipolnil: Gorpinich-Raduzhenko I.A.,KV-41 ||':67 );
  writeln (' ||=================================================================||');
  writeln ('Najmite "X" dlya vuxoda v menu ili druguyu knopky dlya povtora:');
  ch:= readkey
  until (ch='x');
end.
