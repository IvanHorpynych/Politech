unit GLOBAL;

interface {Opysannya dostupnoyi informatsiyi dlya yinshykh moduliv}

const
  p = 2;
  m = 10;
  n = 10;

type
  arr = array[1..p, 1..m, 1..n] of integer; {Zadannya korystuvats'koho typu "masyv"}
  vector=array [1..n*m] of integer;
var A:arr; {Zminna typu masyv} 
    C:vector;

procedure ShowArray(const a: arr); {Protsedura vyvedennya masyvu na ekran}
procedure SortArray(var a: arr); {Protsedura vporyadkovannoho zapovnennya masyvu}
procedure UnSortArray(var a: arr); {Protsedura nevporyadkovannoho zapovnennya masyvu}
procedure BackSortArray(var a: arr); {Protsedura zapovnennya oberneno vporyadkovannoho masyvu}
procedure SortVect(var C: vector);
procedure UnSortVect(var C: vector);
procedure BackSortVect(var C: vector);
implementation

procedure ShowArray(const a: arr); {Protsedura vyvedennya masyvu na ekran}
var
  i, j, k: word;
begin
  for k := 1 to p do {Lichyl'nyky prokhodu po koordynatam masyvu}
  begin
    for i := 1 to m do
    begin
      for j := 1 to n do
        write(a[k, i, j]:8); {Vyvedennya elementu na ekran}
      writeln;
    end;
    writeln;
  end;
end;

procedure UnSortArray(var a: arr); {Protsedura nevporyadkovannoho zapovnennya masyvu}
var
  i, j, k: word; {Zminni kordynat}
begin
  randomize;
  for k := 1 to p do {Lichyl'nyky prokhodu po koordynatam masyvu}
    for i := 1 to m do
      for j := 1 to n do
        a[k, i, j] := random(p * m * n); {Prysvoyennya komirtsi masyvu randomnoho znachennya}
end;

procedure SortArray(var a: arr); {Protsedura vporyadkovannoho zapovnennya masyvu}
var
  i, j, k: word; {Zminni kordynat}
  l: word; {Zminna, za dopomohoyu yakoyi bude zapovnyuvatysya masyv}
begin
  l := 1;
  for k := 1 to p do {Lichyl'nyky prokhodu po koordynatam masyvu}
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l; {Prysvoyennya komirtsi masyvu znachennya l}
        inc(l); {Pry kozhnomu prokhodi lichyl'nyka, zminna l bude zbil'shuvatysya na 1}
      end;
end;

procedure BackSortArray(var a: arr); {Protsedura zapovnennya oberneno vporyadkovannoho masyvu}
var
  i, j, k: word; {Zminni kordynat}
  l: word; {Zminna, za dopomohoyu yakoyi bude zapovnyuvatysya masyv}
begin
  l := p * m * n; {Zminniy l prysvoyuyet'sya znachennya kil'kosti elementiv masyvu}
  for k := 1 to p do {Lichyl'nyky prokhodu po koordynatam masyvu}
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l; {Prysvoyennya komirtsi masyvu znachennya l}
        dec(l); {Pry kozhnomu prokhodi lichyl'nyka, zminna l bude zmen'shuvatysya na 1}
      end;  
end;

procedure SortVect(var C: vector);
var
  i,j:integer;
    begin
      for i:=1 to (n*m) do
      C[i]:=i;
    end; 

procedure UnSortVect(var C: vector);
var
  i,j:integer;
    begin
      j:=n*m;
      for i:=1 to (j) do
        C[i]:=random(j); 
    end;

procedure BackSortVect(var C: vector);
var
  i,j,g:integer;
    begin
      j:=n*m; g:=j;
      for i:=1 to j do
      begin
        C[i]:=g;
        dec(g);
      end;
    end;
end.