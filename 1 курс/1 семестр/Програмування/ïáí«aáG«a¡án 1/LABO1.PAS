program aboratornaya1;
uses crt;
var a,b,x,y,z: real;
begin
clrscr;
 writeln ('Vvedite x,y,z:');
 write ('x=');
 readln (x);
 write ('y=');
 readln (y);
 write ('z=');
 readln (z);
 if ((abs(y-2)-3)=0) then
   writeln('V "A" proishodit delenie na "0"')
    else
      if ((x-y)=0) then
        writeln('V "A" proishodit delenie na "0"')
       else
        if (y<0) then
         writeln('"A" ne suchestvuet')
          else
    begin
    a:=exp((x+1)*ln(y))/exp(ln(abs(abs((y-2)-3)))/3)+(x+y/2)/2*abs(x-y);
    writeln('A=',a:10:4);
    end;

if (x<=-1) or (sin(z)=0) then writeln ('"B" ne suchestvuet')
  else
begin
b:=exp(1/sin(z)*ln(x+1));
writeln('B=',b:10:4);
end;
  writeln('Nagmite lubuy klavishy dlya zavercheniya...');
  readln;
end.