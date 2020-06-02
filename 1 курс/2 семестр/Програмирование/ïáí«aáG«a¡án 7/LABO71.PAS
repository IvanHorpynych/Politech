program laba7yakovenko1;
uses crt;
const num=100;
type
    mas=array[1..num]of integer;
    vect=object
     n:integer;
     max:integer;
     a:mas;
     ch:char;
     procedure invect;
     procedure outvect;
     function maxvect:integer;
     end;

procedure vect.invect;
var i:integer;
begin
randomize;
ch:='A';
repeat
write('Vedite kolichestvo elementov vectora ',ch,' (max= ',num,'): n=');
readln(n);
write('Vvedite maximalnoe znacheniye:');
readln(max);
until n in [1..num];
for i:=1 to n do
 a[i]:=random(100);
clrscr;
end;

procedure vect.outvect;
var i:integer;
begin
for i:=1 to n do
 write(a[i]:4);
end;


function vect.maxvect;
var i,j:integer;
begin
j:=0;
for i := 1 to n do
  if a[i] > max then j:=j+1;
maxvect:=j;
end;

var v:vect;
begin
clrscr;
v.invect;
writeln('Vector ', v.ch);
v.outvect;
writeln;
writeln('Kolichextvo elementov prevyshayshie dannyu= ',v.maxvect);
readln;
end.
