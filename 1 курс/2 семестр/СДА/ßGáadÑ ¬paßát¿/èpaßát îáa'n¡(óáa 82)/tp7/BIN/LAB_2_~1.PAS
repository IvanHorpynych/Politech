program Lab_2_2(SDA);
uses crt;
var S, Znam:double;
    i,n,Chis:integer;
begin
 clrscr;
 Writeln('Enter n');
 Readln(n);
 S:=0; Chis:=1; Znam:=1;
 for i:=1 to n do
  begin
   Chis:=Chis*4;
   Znam:=Znam*(i+cos(i)); 
   S:=S+(Chis-i)/Znam;   
  end;
 writeln('S=',S:5:7);
 readln;
end.