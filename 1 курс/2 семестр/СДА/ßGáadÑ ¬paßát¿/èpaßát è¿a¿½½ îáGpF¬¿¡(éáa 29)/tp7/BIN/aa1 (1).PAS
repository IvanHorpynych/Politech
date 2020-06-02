uses crt;
type tfile=file of longint;
var f:tfile;
    i,kol:integer; a,p:longint;
procedure run;
begin
clrscr;
assign(f,'lab2');
rewrite(f);
p:=4;
for i:=1 to 17 do
    begin
         a:=p-3*i;
         write(f,a);
         p:=p*2;
    end;
close(f);
end;
function amount(var g:tfile):byte;
var b:longint; k:byte;
begin
k:=0;
reset(f);
while not(eof(f)) do
      begin
           read(f,b);
           if (b mod 2=0) and (b<=32767) then inc(k);
           write(b,' ');
      end;
writeln;
close(f);
amount:=k;
end;
procedure zastavka;
begin
clrscr;
gotoXY(40,8); writeln('NTUU KPI');
gotoXY(10,10); writeln('Department of system programming and specialized computer systems');
gotoXY(35,12); writeln('Laboratory #2');
gotoXY(37,14); writeln('Variant #3');
gotoXY(17,24); writeln('The program is made by student of group KV-31 Volontyr Alexandr');
delay(5000); clrscr;
end;
procedure menu;
begin
clrscr;
gotoXY(40,10); writeln('Menu');
gotoXY(25,12); writeln('1:press "1" to run the programm');
gotoXY(25,14); writeln('exit: press "esc" to exit');
end;
begin
zastavka; menu;
while true do
case readkey of
'1': begin
          clrscr;
          run;
          kol:=amount(f);
          writeln('amount of even numbers among the components of the file is ',kol);
          gotoXY(10,25); write('press "enter" to return to the menu or "esc" to exit');
          case readkey of
          #13: menu;
          #27: halt;
          end;
      end;
#27: halt;
end;
readkey;
end.