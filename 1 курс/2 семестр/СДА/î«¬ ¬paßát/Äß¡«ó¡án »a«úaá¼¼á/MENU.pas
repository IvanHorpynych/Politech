Unit MENU;

interface {Opysannya dostupnoyi informatsiyi dlya yinshykh moduliv}

procedure Menu_1;

implementation {Opysannya prykhovanoyi informatsiyi vid inshykh moduliv}

uses crt,GLOBAL,WORKAR,TIMER,TEST;

procedure Menu_1;
var ch, ch1:char; {Zminni, neobkhidni dlya roboty protsedury Menu_1}
    T:integer;  {Zminna, yaka peredaye chas vykonannya obranoho metoda obkhodu}
begin
 repeat {Tsykl povernennya holovnoho menyu na pochatkovu storinku}
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
  Writeln('4 - Packed-mode launch');
  gotoxy(35,17);
  Writeln('5 - Average packed-mode launch');
  gotoxy(35,18);
  Writeln('Esc - Exit');
  ch:=readkey;
  case ch of {Vybir metodu zapovnennya masyvu}
   '1': SortArray(A);
   '2': UnSortArray(A);
   '3': BackSortArray(A);
   '4': begin{Vybir metodu paketnoho zapusku}
		Main_2(A,V,C);{Prokhid po alhorytmakh po odnomu razu}
		exit;
		end;
    '5': begin
    Main_1(A,V,C);{Prokhid po alhorytmakh z poshukom seredn'oho znachennya}
    exit;
    end;
   #27: halt; {Pry natyskanni klavishi Esc prohramma zavershyt'sya}
   end;

 if (ch='1')or(ch='2')or(ch='3') then {Vybir metodu obkhodu}
  clrscr;
  gotoxy(35,12);
  Writeln('Pressed:');
  gotoxy(35,13);
  Writeln('1 - Bypass 1 (With additional array)'); {Obkhid z vykorystannyam dodatkovoho masyvu}
  gotoxy(35,14);
  Writeln('2 - Bypass 2 (With indexes imaginary vector)'); {Obkhid z vykorystannyam indeksiv uyavnoho masyvu}
  gotoxy(35,15);
  Writeln('3 - Bypass 3 (Around it elements of array)'); {Obkhid bezposeredn'o po elementakh masyvu}
  gotoxy(35,16);
  Writeln('Esc - Back');
  ch:=readkey;
  case ch of
    '1': begin {Bezposerdniy vyklyk vykonannya obranoho obkhodu}
          clrscr;
          Writeln('Press Space if you want to show array before sorting'); 
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Vyvid pervozdannoho zadanoho masyvu(na vybir)}
           begin
            ShowArray(A); {Bezposeredniy vyvid pervozdannoho zadanoho masyvu}
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:=WORKAR1711; {Zminniy protsedurnoho typu prysvoyuyet'sya znachennya protsedury obranoho metodu obkhodu}
          Writeln('Please wait...');
          T:=Time(pr); {Prysvoyennya chasu vykonannya alhorytmu za zadanym vydom okhodu}
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Vyvid vidsortovanoho masyvu(na vybir)}
           begin
            ShowArray(A);{Bezposeredniy vyvid vidsortovanoho masyvu}
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    '2':  begin {Bezposerdniy vyklyk vykonannya obranoho obkhodu}
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Vyvid pervozdannoho zadanoho masyvu(na vybir)}
           begin
            ShowArray(A); {Bezposeredniy vyvid pervozdannoho zadanoho masyvu}
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:=WORKAR1712; {Zminniy protsedurnoho typu prysvoyuyet'sya znachennya protsedury obranoho metodu obkhodu}
          Writeln('Please wait...');
          T:=Time(pr); {Prysvoyennya chasu vykonannya alhorytmu za zadanym vydom okhodu}
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Vyvid vidsortovanoho masyvu(na vybir)}
           begin
            ShowArray(A); {Bezposeredniy vyvid vidsortovanoho masyvu}
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    '3': begin {Bezposerdniy vyklyk vykonannya obranoho obkhodu}
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Vyvid pervozdannoho zadanoho masyvu(na vybir)}
           begin
            ShowArray(A); {Bezposeredniy vyvid pervozdannoho zadanoho masyvu}
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:=WORKAR1713; {Zminniy protsedurnoho typu prysvoyuyet'sya znachennya protsedury obranoho metodu obkhodu}
          Writeln('Please wait...');
          T:=Time(pr); {Prysvoyennya chasu vykonannya alhorytmu za zadanym vydom okhodu}
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Vyvid vidsortovanoho masyvu(na vybir)}
           begin
            ShowArray(A); {Bezposeredniy vyvid vidsortovanoho masyvu}
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    #27: exit; {Vykhid z protsedury Menu_1}
   end;{case}
   if ch<>#27 then
    begin
     clrscr;
     gotoxy(40,12);
     Writeln('Algorithm time - ',T); {Vyvid chasu vykonannya zadanoho alhorytmu}
     gotoxy(40,13);
     Writeln('Press Enter for continue');
     readkey;
    end;
 until (ch=#27); {Tsykl povernennya holovnoho menyu na pochatkovu storinku, doky ne bude natysnuta Esc}
end;
end.