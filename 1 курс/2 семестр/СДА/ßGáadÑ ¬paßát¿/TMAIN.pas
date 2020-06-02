uses
  TCOM, TALG,TTIME,crt;
var ch,ch1:char;
    T:integer;
    pr:proc;
procedure Tab;
var Tm, ind_var_sort, ind_var_alg,i:integer;
begin
 clrscr;
 Write('UnSort':13); Write('Sort':7); Writeln('BackSort':10);
 ind_var_alg:=1;
 repeat
  ind_var_sort:=1;
  case ind_var_alg of
   1: begin
       Write('A04');
       pr:=Alg454;
      end;
   2: begin
       Write('A09');
       pr:=Alg459;
      end;
   3: begin
       Write('A22');
       pr:=Alg4522;
      end;
   4: begin
       Write('A23');
       pr:=Alg4523;
      end;
  end;{case}

  repeat
   case ind_var_sort of
    1:  begin
          UnSort(A);
          Tm:=Time(pr);
        end;
    2:  begin
          Sort(A);
          Tm:=Time(pr);
        end;
    3:  begin
          BackSort(A);
          Tm:=Time(pr);
         end;
   end;{case}

   if ind_var_sort<>3 then Write(Tm:7)
    else Writeln(Tm:7);

   inc(ind_var_sort);
  until ind_var_sort>3;
  inc(ind_var_alg);
 until ind_var_alg>4;

 Writeln;
 Writeln('Press Enter for continue');
 readkey;
end;




begin
 repeat
  clrscr;
  Writeln('Press:');
  Writeln(' 1 - Sort Array');
  Writeln(' 2 - UnSort Array');
  Writeln(' 3 - BackSort Array');
  Writeln(' 4 - Tablica');
  Writeln(' Esc - Exit');
  ch:=readkey;
  case ch of
   '1': Sort(A);
   '2': UnSort(A);
   '3': BackSort(A);
   '4': Tab;
   #27:halt;
  end;
  if (ch='1') or (ch='2') or (ch='3') then
   begin
   repeat
    clrscr;
    Writeln('Press:');
    Writeln(' 1 - Algoritm 4');
    Writeln(' 2 - Algoritm 9');
    Writeln(' 3 - Algoritm 22');
    Writeln(' 4 - Algoritm 23');
    ch:=readkey;
   until (ch='1') or (ch='2') or (ch='3') or (ch='4');
   clrscr;
   Writeln('Show array before sort?  y/n');
   ch1:=readkey;
   if (ch1='y') or (ch1='Y')  then Output(A);

   Writeln;
   Writeln('Press Enter for sort');
   readkey;
   clrscr;
   Writeln('Please wait...');
   case ch of
    '1': T:=Time(Alg454);
    '2': T:=Time(Alg459);
    '3': T:=Time(Alg4522);
    '4': T:=Time(Alg4523);
   end;
   clrscr;
   Writeln('Show array after sort?  y/n');
   ch1:=readkey;
   if (ch1='y') or (ch1='Y')  then Output(A);
   Writeln;
   Writeln;
   Writeln('Algoritm time - ',T);
   Writeln;
   gotoxy(30,20);
   Writeln('Press Enter for continue');
   gotoxy(30,21);
   Writeln('Press Esc for Exit');
   ch:=readkey;
  end;


 until ch=#27;

end.