program Labo3;
var i:integer; s:real;
function Up(i: integer;var s:real): real;
var  f: real;
begin
  if (i = 1) then f := 1
  else
    f := 2*cos(Up(i-1,s))+i;
    s:=s+f;
    Up:=f;
end;
function Down(s,f:real; i,n:integer):real;
begin
if i>1 then f:=2*cos(f)+i; s:=s+f; 
if i=n then Down:=s else begin  
  Down:=Down(s,f,i+1,n);
end;
end; 
procedure Up_Down(f:real; i,n:integer; var s:real);
begin
if i>1 then f:=2*cos(f)+i;
if i=n then else Up_Down(f,i+1,n,s);
s:=s+f;
end;
function perevirka(n:integer):real;
var j:integer; s,f:real;
begin
f:=1; s:=f;
for j:=2 to n do begin
 f:=2*cos(f)+j; s:=s+f; end;
perevirka:=s; 
end; 
begin
write('Vvedite kolichestvo clenov posledovatelnisti: '); read(i); s:=0; Up(i,s);
writeln('Summa na vozvrasheniy pervih ', i,' chlenov ravna: ',s:15:2); s:=0;
writeln;
write('Vvedite kolichestvo clenov posledovatelnisti: '); read(i);
writeln('Summa na spuske pervih ', i,' chlenov ravna: ',Down(0,1,1,i):21:2);
writeln;
write('Vvedite kolichestvo clenov posledovatelnisti: '); read(i); Up_Down(1,1,i,s);
writeln('Summa na spuske i vozvrasheniy pervih ', i,' chlenov ravna: ',s:6:2);
writeln;
write('Vvedite kolichestvo clenov posledovatelnisti: '); read(i);
writeln('Proverochnaya summa pervih ', i,' chlenov ravna: ',perevirka(i):17:2);
writeln;
readln;
end.
