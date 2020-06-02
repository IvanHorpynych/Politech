uses crt;
const n=10;
type tfile=file of real;
var f:tfile; k:byte;
procedure main;
var a,b:real; flag:boolean;
    i:byte;
begin
clrscr;
flag:=false;
writeln('given file consist of the elements:');
reset(f);
while not(eof(f)) do
  begin
       read(f,a); write(a:5:2,'  ');
  end;
close(f);
writeln;
reset(f);
while not(eof(f)) do
  begin
       read(f,a); read(f,b);
       if b<a then begin
                        flag:=true;
                        break;
                   end;
  end;
close(f); reset(f); writeln('converted file is ');
if flag=true then begin
                     for i:=1 to n div 2 do
                      begin
                           seek(f,k-i); read(f,a);
                           seek(f,i-1); read(f,b);
                           seek(f,i-1); write(f,a);
                           seek(f,k-i); write(f,b);
                      end;
                     close(f); reset(f);
                     while not(eof(f)) do
                         begin
                              read(f,a); write(a:5:2,'  ');
                         end;
                end
else begin seek(f,0);
           while not(eof(f)) do
            begin
                 read(f,a);
                 write(a:4:2,' ');
            end;
     end;
close(f);
end;
procedure filling;
var i:byte; a:real;
begin
randomize;
assign(f,'lab3');
rewrite(f);
writeln('Do you want to order elemnts by nondecrease? (y/n)');
case readkey of
'n': for i:=1 to n do
      begin
           a:=random(100)+1/(random(100)+1);
           write(f,a);
      end;
'y': begin
          a:=random(50);
          for i:=1 to n do
            begin
                 a:=a+random(50)+1/(random(100)+1);
                 write(f,a);
            end;
     end;
end;
k:=filesize(f);
close(f);
end;
procedure zastavka;
begin
clrscr;
gotoXY(40,8); writeln('NTUU KPI');
gotoXY(10,10); writeln('Department of system programming and specialized computer systems');
gotoXY(35,12); writeln('Laboratory #3');
gotoXY(37,14); writeln('Variant #3');
gotoXY(17,24); writeln('The program is made by student of group KV-31 Volontyr Alexandr');
delay(3000); clrscr;
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
'1': begin clrscr; filling; main;
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
