unit KOsn;

interface

const
  p = 2;
  n = 100;

type
  arr = array[1..p, 1..n, 1..n] of integer;
  
var A:arr;  
   

procedure ShowArray(const a: arr);
procedure SortArray(var a: arr);
procedure UnSortArray(var a: arr);
procedure BackSortArray(var a: arr);


implementation

procedure ShowArray(const a: arr);
var
  i, j, k: integer;
begin
  for k := 1 to p do
  begin
    for i := 1 to n do
    begin
      for j := 1 to n do
        write(a[k, i, j]:6);
      writeln;
    end;
    writeln;
  end;
end;

procedure UnSortArray(var a: arr);
var
  i, j, k: integer;
begin
  randomize;
  for k := 1 to p do
    for i := 1 to n do
      for j := 1 to n do
        a[k, i, j] := random(p * n * n);
end;

procedure SortArray(var a: arr);
var
  i, j, k: integer;
  b: integer;
begin
  b := 1;
  for k := 1 to p do
    for i := 1 to n do
      begin
        a[k, i, i] := b;
        a[k,i,n-i+1]:=b;
        inc(b);
      end;
end;

procedure BackSortArray(var a: arr);
var
  i, j, k: integer;
  b: integer;
begin
  b := n*n*p;
  for k := 1 to p do
    for i := 1 to n do
      begin
        a[k, i, i] := b;
        a[k,i,n-i+1]:=b;
        dec(b);
      end;
end;

end.