unit Algoritm;

interface

uses goga;



procedure Alg2315(var a: arr);
procedure Alg2314(var a: arr);
procedure Alg2313(var a: arr);


implementation



procedure Alg2313(var a: arr);
var
  k, i, j, Inext, Jnext, R, s: integer;
  b: integer;
begin
  for k := 1 to p do
   for R:=m*n downto 2 do
    begin
      i := 1;
      j := 1;
      Inext := i+1;
      Jnext := j;
      for s := 1 to R - 1 do
       begin
        if a[k, i, j] > a[k, Inext, Jnext] then
         begin
          b := a[k, i, j];
          a[k, i, j] := a[k, Inext, Jnext];
          a[k, Inext, Jnext] := b;
         end;
        if i < m then inc(i) else begin
          inc(j);
          i := 1;
        end;
        Inext := i + 1;
        if Inext > m then
        begin
          inc(Jnext);
          Inext := 1;
        end;
      end;
    end;
end;



procedure Alg2314(var a: arr);
var
  k, i, j, Inext, Jnext, R, s: integer;
  b: integer;
  f:boolean;
begin
  for k := 1 to p do
  begin
    R := m * n;
    f:=true;
    while f=true do
    begin
      i := 1;
      j := 1;
      Inext := i+1;
      Jnext := j;
      f:=false;
      for s := 1 to R - 1 do
      begin
        if a[k, i, j] > a[k, Inext, Jnext] then
        begin
          b := a[k, i, j];
          a[k, i, j] := a[k, Inext, Jnext];
          a[k, Inext, Jnext] := b;
          f:=true;
        end;
        if i < m then inc(i) else begin
          inc(j);
          i := 1;
        end;
        Inext := i + 1;
        if Inext > m then
        begin
          inc(Jnext);
          Inext := 1;
        end;
      end;
      R:=R-1;
    end;
  end;
end;



procedure Alg2315(var a: arr);
var
  k, i, j, Inext, Jnext, R, s, x: integer;
  b: integer;
begin
  for k := 1 to p do
  begin
    R := m * n;
    while (R > 1) do
    begin
      x := 1;
      i := 1;
      j := 1;
      Inext := i+1;
      Jnext := j;
      for s := 1 to R - 1 do
      begin
        if a[k, i, j] > a[k, Inext, Jnext] then
        begin
          b := a[k, i, j];
          a[k, i, j] := a[k, Inext, Jnext];
          a[k, Inext, Jnext] := b;
          x := s;
        end;
        if i < m then inc(i) else begin
          inc(j);
          i := 1;
        end;
        Inext := i + 1;
        if Inext > m then
        begin
          inc(Jnext);
          Inext := 1;
        end;
      end;
      R := x;
    end;
  end;
end;

end.
