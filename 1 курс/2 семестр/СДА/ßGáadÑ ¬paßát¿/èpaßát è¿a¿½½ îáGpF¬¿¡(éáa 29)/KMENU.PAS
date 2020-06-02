Unit KMenu;

interface

procedure Menu1;

implementation

uses crt,KOsn,KAlg,KTime;

procedure Menu1;
var ch:char;
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
  Writeln('1 - Algorithm 2');
  gotoxy(35,14);
  Writeln('2 - Algorithm 12');
  gotoxy(35,15);
  Writeln('3 - Algorithm 15');
  gotoxy(35,16);
  Writeln('4 - Algorithm 23');
  gotoxy(35,17);
  Writeln('Esc - Back');
  ch:=readkey;
  case ch of
   '1':pr:=Alg782;
   '2':pr:=Alg7812;
   '3':pr:=Alg7815;
   '4':pr:=Alg7823;
   #27:break;
   end;
  if (ch='1')or(ch='2')or(ch='3')or(ch='4')   then begin
  clrscr;
  gotoxy(35,14);
  Writeln('Algorithm time - ',Time(pr));
  gotoxy(35,15);
  Writeln('Pressd any key for continue');
  readkey;
  end;
 until (ch='1')or(ch='2')or(ch='3')or(ch='4')or(ch=#27);

 until (ch='1')or(ch='2')or(ch='3')or(ch=#27);
end;


end.