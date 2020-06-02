program DKR_Programming;

uses
  crt, Graph;

var
  arr: array[1..4, 1..4] of string;
  vect: array[1..16] of integer;
  men: array[1..5] of integer;
  i, j: integer;
  mline, mcolumn: integer;
  slc: integer;
  ch: char;
  chk: boolean;
  f: text;

procedure Output;

var
  lx, ly: integer;
  x, y: integer;
  j1, i1: integer;
  w1, h1: integer;
begin
  OutTextXY(210, 50, 'For leaving press ENTER');
  w1 := 30;
  h1 := 30;
  for i1 := 0 to 3 do
    for j1 := 0 to 3 do
    begin
      x := 235 + j1 * 35;
      y := 150 + i1 * 35;
      setFillStyle(1, 4);
      Bar(x, y, x + w1, y + h1);
    end;
  lx := 245;
  ly := 162;
  for i := 1 to 4 do
  begin
    for j := 1 to 4 do
    begin
      OutTextXY(lx, ly, arr[i, j]);
      lx := lx + 35;
    end;
    lx := 245;
    ly := ly + 35;
  end;
  line(220, 135, 220, 300);
  line(385, 135, 385, 300);
  line(220, 135, 385, 135);
  line(220, 300, 385, 300);
end;

procedure Tablo;

var
  b: integer;
  k, z: integer;
begin
  randomize;
  for z := 1 to 16 do
  begin
    b := random(15);
    k := 1;
    while k <> 17 do
    begin
      if vect[k] = b then
      begin
        b := random(17);
        k := 1;
      end
      else k := k + 1;
    end;
    vect[z] := b;
  end;
  z := 1;
  for i := 1 to 4 do
  begin
    for j := 1 to 4 do
    begin
      case vect[z] of
        1: arr[i, j] := '1 ';
        2: arr[i, j] := '2 ';
        3: arr[i, j] := '3 ';
        4: arr[i, j] := '4 ';
        5: arr[i, j] := '5 ';
        6: arr[i, j] := '6 ';
        7: arr[i, j] := '7 ';
        8: arr[i, j] := '8 ';
        9: arr[i, j] := '9 ';
        10: arr[i, j] := '10';
        11: arr[i, j] := '11';
        12: arr[i, j] := '12';
        13: arr[i, j] := '13';
        14: arr[i, j] := '14';
        15: arr[i, j] := '15';
        16: arr[i, j] := '  ';
      end;
      z := z + 1;
    end;
  end;
  Output;
end;

procedure Search;

begin
  for i := 1 to 4 do
  begin
    for j := 1 to 4 do
    begin
      if arr[i, j] = '  ' Then
      begin
        mline := i;
        mcolumn := J
      end;
    end;
  end;
end;

procedure cheat;

begin
  arr[1, 1] := '1 ';arr[1, 2] := '5 ';arr[1, 3] := '9 ';arr[1, 4] := '13 ';
  arr[2, 1] := '2 ';arr[2, 2] := '6 ';arr[2, 3] := '10 ';arr[2, 4] := '14 ';
  arr[3, 1] := '3 ';arr[3, 2] := '7';arr[3, 3] := '11';arr[3, 4] := '15';
  arr[4, 1] := '4';arr[4, 2] := '8';arr[4, 4] := '12';arr[4, 3] := '  ';
  mline := 4;mcolumn := 3;
end;

procedure direction;

begin
  ch := readkey;
end;

procedure repl;

begin
  direction;
  if ord(ch) = 79 then cheat;
  if ord(ch) = 75 then
  begin
    if mcolumn <> 4 then
    begin
      arr[mline, mcolumn] := arr[mline, mcolumn + 1];
      arr[mline, mcolumn + 1] := '  ';
      mcolumn := mcolumn + 1;
    end;
  end;
  if ord(ch) = 72 then
  begin
    if mline <> 4 then
    begin
      arr[mline, mcolumn] := arr[mline + 1, mcolumn];
      arr[mline + 1, mcolumn] := '  ';
      mline := mline + 1;
    end;
  end;
  if ord(ch) = 77 then
  begin
    if mcolumn <> 1 then
    begin
      arr[mline, mcolumn] := arr[mline, mcolumn - 1];
      arr[mline, mcolumn - 1] := '  ';
      mcolumn := mcolumn - 1;
    end;
  end;
  if ord(ch) = 80 then
  begin
    if mline <> 1 then
    begin
      arr[mline, mcolumn] := arr[mline - 1, mcolumn];
      arr[mline - 1, mcolumn] := '  ';
      mline := mline - 1;
    end;
  end;
  Output;
end;

procedure check;
var sd: integer;
begin
  chk := false;
  if (arr[1, 1] = '1 ') and (arr[1, 2] = '5 ') and (arr[1, 3] = '9 ') and (arr[1, 4] = '13 ')
  and (arr[2, 1] = '2 ') and (arr[2, 2] = '6 ') and (arr[2, 3] = '10 ') and (arr[2, 4] = '14 ')
  and (arr[3, 1] = '3 ') and (arr[3, 2] = '7') and (arr[3, 3] = '11') and (arr[3, 4] = '15')
  and (arr[4, 1] = '4') and (arr[4, 2] = '8') and (arr[4, 3] = '12') and (arr[4, 4] = '  ')
  then
  begin
    chk := true;
    OutTextXY(245, 100, 'Congratulate!!!');
    OutTextXY(245, 330, 'You have won =)');
      sd:=100;
      repeat
      sound(sd);
      delay(10);
      nosound;
      inc(sd,10);
      until (sd>1800);
    readln;
  end;
end;

procedure Game15;

var
  grMode: integer;
  grPath: string;
  grDriver: integer;
begin
  grDriver := VGA;
  grmode := 2;
  grPath := 'EGAVGA.BGI';
  initGraph(grDriver, grMode, grPath);
  Tablo;
  Search;
  repeat
    repl;
    check;
  until (ord(ch) = 13) or (chk = true);
  closeGraph;
end;

procedure help;

var
  f: text;
  g1: string;
begin
  clrscr;
 assign(f, 'fhelp.txt');
  reset(f);
  while not(eof(f)) do begin
  readln(f, g1); writeln(g1); end;
  writeln;
  writeln('For leaving press ENTER');
  readln;
  close(f);
end;

procedure autor;

var
  f: text;
  g1: string;
begin
  clrscr;
  assign(f, 'fhelp1.txt');
  reset(f);
  while not(eof(f)) do begin
  readln(f, g1); writeln(g1); end;
  writeln;
  writeln('For exit press ENTER');
  readln;
  close(f);
end;

begin
  men[1] := 15;
  men[2] := 12;
  men[3] := 12;
  men[4] := 12;
  repeat
    clrscr;
    mline := 1;
    slc := 1;

    GoToXY(32, 10);Textcolor(men[1]);writeln('Key');
    GoToXY(32, 11);Textcolor(men[2]);writeln('About program');
    GoToXY(32, 12);Textcolor(men[3]);writeln('Play');
    GoToXY(32, 13);Textcolor(men[4]);writeln('Exit');
    ch := readkey;
    if (ord(ch) = 80)  then
    begin
      for i := 1 to 4 do
      begin
        if (men[i] = 15) and (mline <> 4) then
        begin
          men[mline] := 12;
          men[mline + 1] := 15;
        end
        else mline := mline + 1;
      end;
    end;
    if ord(ch) = 72 then
    begin
      for i := 1 to 4 do
      begin
        if (men[i] = 15) and (mline <> 1) then
        begin
          men[mline] := 12;
          men[mline - 1] := 15;
        end
        else mline := mline + 1;
      end;
    end;
    if ord(ch) = 13 then
    begin
      for i := 1 to 4 do
      begin
        if men[i] = 15 then
        begin
          if slc = 1 then begin Help;break; end;
          if slc = 2 then begin autor;break; end;
          if slc = 3 then begin Game15;break; end;
        end
        else slc := slc + 1;
      end;
    end;
  until slc = 4
end.