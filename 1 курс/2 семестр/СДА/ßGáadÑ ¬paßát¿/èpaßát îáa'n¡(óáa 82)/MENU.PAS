unit Menu;

interface

uses crt, Goga,Algoritm, MTIME;

procedure menu1;

implementation



procedure menu1;
var ch, ch1:char;
    T:integer;
begin
 repeat
  repeat
   clrscr;
   Writeln('1 - Sort');
   Writeln('2 - BackSort');
   Writeln('3 - UnSort');
   Writeln('4 - Exit');
   ch:=readkey;
   case ch of
    '1': SortArray(A);
    '2': BackSortArray(A);
    '3': UnSortArray(A);
    '4': halt;
   end; {case}
  until (ch='1')or(ch='2')or(ch='3')or(ch='4');
  clrscr;
  gotoxy(23,60);
  Writeln('Esc - Exit');
  Writeln('1 - Alg 13');
  Writeln('2 - Alg 14');
  Writeln('3 - Alg 15');
  ch:=readkey;
  case ch of
    '1': begin
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then 
           begin
            ShowArray(A);
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:= Alg2313;
          Writeln('Please wait...');
          T:=Time(pr);
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then
           begin
            ShowArray(A);
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    '2':  begin
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then
           begin
            ShowArray(A);
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:= Alg2314;
          Writeln('Please wait...');
          T:=Time(pr);
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then
           begin
            ShowArray(A);
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    '3': begin
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then
           begin
            ShowArray(A);
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:= Alg2315;
          Writeln('Please wait...');
          T:=Time(pr);
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then
           begin
            ShowArray(A);
            Writeln('Press Enter for continue');
            readkey;
           end;

         end;
   end;{case}
   if ch<>#27 then
    begin

     clrscr;
     gotoxy(40,12);
     Writeln('Algorithm time - ',T);
     gotoxy(40,13);
     Writeln('Press Enter for continue');
     readkey;
    end;
 until (ch=#27);
end;
end.