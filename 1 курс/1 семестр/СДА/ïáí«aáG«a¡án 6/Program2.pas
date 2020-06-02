Program q123;
uses crt;
const
  m = 20; n = 40; t=5;
var
  o, i, j, p, k: integer;
procedure up;
begin
gotoxy(o*(m div 2)+i-k,i+k);  write('*');delay(50);
end;
procedure down;
begin 
gotoxy((((j*n-j*m) div 2)-i-k+p),(m div 2)+i-k); write('*');delay(50);
end;
begin
if (m mod 2)=0 then p:=0
else p:=1;
for j:=1 to 4 do
begin
if j=1 then o:=0
else o:=(j-1);
for k:=0 to (m div 4) do
begin
for i:=1+K to ((m div 2)-k) do
up;
for i:=1+k to ((m div 2)-(k+1)) do
down;
end;
end;
gotoxy(1,25);
end.
    
