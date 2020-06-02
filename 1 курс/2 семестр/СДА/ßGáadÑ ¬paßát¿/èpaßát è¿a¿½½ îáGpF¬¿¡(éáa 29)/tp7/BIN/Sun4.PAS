uses crt;
const a:set of char=['b'..'d','f'..'h','j'..'n','p'..'t','v'..'x','z',
                     'B'..'D','F'..'H','J'..'N','P'..'T','V'..'X','Z'];
      b:set of char=['a','e','i','o','u','y','A','E','I','O','U','Y'];
var f:text;
procedure main;
var s,st:string; i,gl,sgl:byte;
begin
clrscr;
assign(f,'lab4file.txt');
reset(f);
readln(f,st);
if st='' then writeln('file is empty') else writeln('file is filled');
close(f);
delay(1000); clrscr;
rewrite(f);
writeln('enter any text to fill the file');
repeat
readln(s);
writeln(f,s)
until length(s)=0;
close(f);
clrscr;
reset(f);
gl:=0; sgl:=0;
writeln('Contents of the file is as follows');
while not(eof(f)) do
  begin
       readln(f,s); writeln(s);
       for i:=1 to length(s) do
             if s[i] in b then inc(gl)
             else if s[i] in a then inc(sgl);
  end;
close(f);
writeln('the amount of vowel letters in the file is  ',gl);
writeln('the amount of consonant letters in the file is  ',sgl);
end;
procedure zastavka;
begin
clrscr;
gotoXY(40,8); writeln('NTUU KPI');
gotoXY(10,10); writeln('Department of system programming and specialized computer systems');
gotoXY(35,12); writeln('Laboratory #4');
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
clrscr;
zastavka; menu;
while true do
case readkey of
'1': begin
          main;
          gotoXY(10,25); write('press "enter" to return to the menu or "esc" to exit');
          case readkey of
          #13: menu;
          #27: halt;
          end;
     end;
#27: halt;
end;
end.