program prog_1;
uses crt;
var i,j,k:integer;
    s:string;

begin
clrscr;
i:=12; j:=40;
gotoxy(j,i);
k:=ord('d');


repeat
if k=ord('s') then

 repeat
  if i=25 then
   begin
    gotoxy(j,i);
    write('*');
    delay(50);
    gotoxy(j,i);
    write(' ');
    i:=1;
    gotoxy(j,i);
   end
  else
   begin
    gotoxy(j,i);
    textcolor(15);
    write('*');
    delay(50);
    gotoxy(j,i);
    write(' ');
    inc(i);
   end;
  if keypressed then k:=ord(readkey);
  if (k=ord('a'))  or (k=ord('d')) then break;
 until false;


if k=ord('w') then
 repeat
  if i=1 then
   begin
    gotoxy(j,i);
    write('*');
    delay(50);
    gotoxy(j,i);
    write(' ');
    i:=25;
    gotoxy(j,i);
   end
  else
   begin
    gotoxy(j,i);
    textcolor(15);
    write('*');
    delay(50);
    gotoxy(j,i);
    write(' ');
    dec(i);
   end;


  if keypressed then k:=ord(readkey);
  if (k=ord('a')) or (k=ord('d')) then break;
 until false;

if k=ord('a') then
 repeat
  if j=1 then
 begin
   gotoxy(j,i);
  write('*');
  delay(50);
  gotoxy(j,i);
  write(' ');
  j:=80;
  gotoxy(j,i);
 end

 else
  begin
  gotoxy(j,i);
  textcolor(15);
  write('*');
  delay(50);
  gotoxy(j,i);
  write(' ');
  dec(j);
  end;

  if keypressed then k:=ord(readkey);
  if k=ord('z') then exit;
  if (k=ord('w')) or (k=ord('s')) then break;
 until false;


if k=ord('d') then
 repeat
  if j=80 then
  begin
    gotoxy(j,i);
  write('*');
  delay(50);
  gotoxy(j,i);
  write(' ');
  j:=1;
  gotoxy(j,i);

  end
  else
   begin
    asm
     mov ah,01;
     mov cx,$2000;
     int $10;

     end;
    gotoxy(j,i);
    textcolor(15);
    write('*');
    delay(50);
    gotoxy(j,i);
    write(' ');
    inc(j);
   end;
  if keypressed then k:=ord(readkey);
  if (k=ord('w')) or (k=ord('s')) then break;
 until false;

until false;
end.