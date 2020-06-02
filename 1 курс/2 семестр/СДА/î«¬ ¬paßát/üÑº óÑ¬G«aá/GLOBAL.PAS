unit GLOBAL;

interface {Opysannya dostupnoyi informatsiyi dlya yinshykh moduliv}

const
  p = 4;
  m = 40;
  n = 40;

type
  arr = array[1..p, 1..m, 1..n] of integer; {Zadannya korystuvats'koho typu "masyv"}
  
var A:arr; {Zminna typu masyv} 
   

procedure ShowArray(const a: arr); {Protsedura vyvedennya masyvu na ekran}
procedure SortArray(var a: arr); {Protsedura vporyadkovannoho zapovnennya masyvu}
procedure UnSortArray(var a: arr); {Protsedura nevporyadkovannoho zapovnennya masyvu}
procedure BackSortArray(var a: arr); {Protsedura zapovnennya oberneno vporyadkovannoho masyvu}


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


end.