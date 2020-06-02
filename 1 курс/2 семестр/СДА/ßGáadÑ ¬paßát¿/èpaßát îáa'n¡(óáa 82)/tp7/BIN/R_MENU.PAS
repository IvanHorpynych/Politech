Unit R_MENU;

interface

procedure Menu_1;

implementation

uses crt,UGLOB,UOBH,R_TIME;

procedure Menu_1;
var ch:char;
    T:integer;
begin
 repeat
  clrscr;
  gotoxy(35,12);
  Writeln('Pressed:');
  gotoxy(35,13);
  Writeln('1 - Sort array');
  gotoxy(35,14);
  Writeln('2 - Unsort array');
  gotoxy(35,15);
  Writeln('3 - Backsort array');
  gotoxy(35,16);
  Writeln('Esc - Exit');
  ch:=readkey;
  case ch of
   '1': SortArray(A);
   '2': UnSortArray(A);
   '3': BackSortArray(A);
   #27: halt;
   end;

 if (ch='1')or(ch='2')or(ch='3') then
 repeat
  clrscr;
  gotoxy(35,12);
  Writeln('Pressed:');
  gotoxy(35,13);
  Writeln('1 - Obhid 1');
  gotoxy(35,14);
  Writeln('2 - Obhid 2');
  gotoxy(35,15);
  Writeln('3 - Obhid 3');
  gotoxy(35,16);
  Writeln('Esc - Back');
  ch:=readkey;
  case ch of
   '1':pr:=Obh218;
   '2':pr:=Obh228;
   '3':pr:=Obh238;
   #27:break;
   end;
  if (ch='1')or(ch='2')or(ch='3')   then begin
  clrscr;
  gotoxy(35,14);
  Writeln('Please check...');
  T:=Time(pr);
  clrscr;
  gotoxy(35,14);
  Writeln('Algorithm time - ',T);
  gotoxy(35,15);
  Writeln('Pressd any key for continue');
  readkey;
  end;
 until (ch='1')or(ch='2')or(ch='3')or(ch=#27);

 until (ch='1')or(ch='2')or(ch='3')or(ch=#27);
end;


end.