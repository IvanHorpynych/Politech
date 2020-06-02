program Labo6SDA;
uses
  crt;
const
  m = 13;n = 40; t=5;
var
  i, j, p, k: integer;
begin
clrscr;
if (m mod 2)=0 then k:=0
else k:=1;
for p:=m downto ((m div 2)+1+k) do
begin
 for j:=((m+1)-p) to n-(m-p) do 
    begin
    gotoxy(j, p); write('='); delay(t); end;
 for i:=p-1 downto ((m+1)-p) do   
    begin
    gotoxy(n-(m-p),i); write(']'); delay(t);end; 
 for j:=(n-((m+1)-p)) downto ((m+1)-p) do
    begin
    gotoxy(j,((m+1)-p)); write ('-'); delay(t); end;
 for i:=((m+2)-p) to (m-((m+1)-p)) do
    begin
    gotoxy(((m+1)-p),i); write('|'); delay(t); end;  
end;
if k=1 then
for j:=((m div 2)+1) to (n-(m div 2)) do
begin
    gotoxy(j,((m div 2)+1)); write('='); delay(t);
end;  
gotoxy((m+1),1);
write('Vipolnil:Gorpinich-Radyzhenko Ivan');
gotoxy(1,(m+1));
    readln;      
end.   