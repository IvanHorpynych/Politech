program Lab_2;
uses crt;
type func=function(x:real):real;
const a=0;
      b=2*pi;
      n=7;
var i,j:integer;
    x:real;

procedure Tablica(ch1:char; ch2:char; ch3:char);
 var i:integer;
  begin
     for i:=1 to 78 do
      begin
       if (i=19)or(i=38)or(i=57)
        then
          Write(ch2)
        else
          if i=1
            then
             Write(ch1)
            else
             if i=78
              then
                Writeln(ch3)
              else
                Write(char(205));
     end;
  end;
{$F+}
function f1(x:real):real;
  begin
    f1:=sin(x)*sin(x)*sin(x);
  end;
function f2(x:real):real;
  begin
    f2:=2-cos(x);
  end;
function f3(x:real):real;
  begin
    f3:=1+exp(cos(x));
  end;
{$F-}
procedure OutputF(x:real;F:func);
begin
 Write(F(x):12:3);
end;



begin
clrscr;
Tablica(char(201),char(205),char(187));
Writeln(char(186),'Function of one variable':51,char(186):26);
Tablica(char(204),char(203),char(185));
Writeln(char(186),'Argument X':14,
        char(186):4,'(sin(x))^3':15,

        char(186):4,'2-cos(x)':14,
        char(186):5,'1+exp(cos(x))':17,
        char(186):4);
for j:=0 to n do
 begin
  x:=a+j*(b-a)/n;
  Tablica(char(204),char(206),char(185));
  Write(char(186),x:10:3);
    Write(char(186):8); OutputF(x,f1);
    Write(char(186):7); OutputF(x,f2);
    Write(char(186):7); OutputF(x,f3);
    Writeln(char(186):9);
 end;
Tablica(char(204),char(208),char(185));
Writeln(char(186), 'Made by Anastasiev D. V. KV-31':74,char(186):3);
Tablica(char(200),char(205),char(188));
readln;
end.


