program Lab3_2;
uses crt;
var    Fi:real;
       n:integer;
function Rekurs(i:integer;var Fi:real;sum:real):real;
begin
 if i>1 then 
  begin
    sum:=Rekurs(n-1,Fi,sum);
    Fi:=cos(Fi)*sqrt(i)/5;
    sum:=sum+Fi;
    Rekurs:=Sum;  
  end
  else 
   begin
    Fi:=cos(fi)/5;
    Rekurs:=cos(1)/5;
   end;
   
end;
begin
 clrscr;
 Writeln('Enter n');
 Write('n=');
 Readln(n);
 Fi:=1;
 Writeln('Summa n komponentiv:');
 Writeln('Sum=',Rekurs(n,Fi,0):3:5);
 readln;
end.