program LABO1_2;
uses crt;
var y,x: real;
 begin
 clrscr;
 writeln ('Vvedite x:');
 write ('x=');
 read (x);
  begin
   if (x>-13) and (x<=-3) or (x>14) then
    begin
    y:=(-4)*x*x*x+3*x-7;
    writeln ('y=',y:10:4);
    end
   else
    if (x>=2) and (x<=7) then
     begin
     y:=x*3+14;
     writeln ('y=',y:10:4);
     end
    else
     writeln ('Funkcija ne sushhestvet');
     readln;
  end;
  readln;
 end.