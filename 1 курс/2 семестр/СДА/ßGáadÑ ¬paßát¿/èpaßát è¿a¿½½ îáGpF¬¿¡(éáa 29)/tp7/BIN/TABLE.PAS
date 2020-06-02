Program lb2;
uses crt;
const a=0;
      b=2;
      n=7;
var x: real;
    i,j: integer;
procedure Line(symbol: char; symbol2: char; symbol3: char);
     begin
      for j:=1 to 77 do
      if (j=20) or (j=35) or (j=58) then write(symbol)
      else if j=1 then write (symbol2)
        else if j=77 then writeln(symbol3)
          else write(char(205));
      end;
function f1(x:real):real;
      begin
      f1:=exp(2*x);
      end;
function f2(x:real):real;
      begin
      f2:=sqrt(exp(ln(x+9)*3));
      end;
function f3(x:real):real;
      begin
      f3:=ln(abs(24*x+2));
      end;
begin
clrscr;
line(char(205), char(201), char(187));
writeln(char(186), 'Function of one variable':50, char(186):26);
line(char(203), char(204), char(185));
writeln(char(186), 'argument X':14,
        char(186):5, 'exp(2*x)':12,
        char(186):3, 'sqrt(exp(ln(x+9)*3))':21,
        char(186):2, 'ln(abs(24*x+2))':17, char(186):2);
for i:=0 to n do
begin
x:=a+i*((b-a)/n);
line(char(206), char(204), char(185));
writeln (char(186), x:11:3,
         char(186):8, f1(x):12:3,
         char(186):3, f2(x):12:3,
         char(186):11, f3(x):12:3, char(186):7);
end;
line(char(202), char(204), char(185));
writeln(char(186), 'Made by Kindzer M.S. KV-31':74, char(186):2);
line(char(205), char(200), char(188));
readln;
end.
