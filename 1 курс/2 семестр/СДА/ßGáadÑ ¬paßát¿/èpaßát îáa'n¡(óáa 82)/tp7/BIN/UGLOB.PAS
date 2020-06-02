unit UGLOB;

interface

const
  p = 2;
  m = 20;
  n = 20;

type
  arr = array[1..p, 1..m, 1..n] of integer;
  
var A:arr;  
   

procedure ShowArray(const a: arr);
procedure SortArray(var a: arr);
procedure UnSortArray(var a: arr);
procedure BackSortArray(var a: arr);


implementation

procedure ShowArray(const a: arr);
var
  i, j, k: word;
begin
  for k := 1 to p do
  begin
    for i := 1 to m do
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
  i, j, k: word;
begin
  randomize;
  for k := 1 to p do
    for i := 1 to m do
      for j := 1 to n do
        a[k, i, j] := random(p * m * n);
end;

procedure SortArray(var a: arr);
var
  i, j, k: word;
  l: word;
begin
  l := 1;
  for k := 1 to p do
    for j := 1 to n do
      for i := 1 to m do
      begin
        a[k, i, j] := l;
        inc(l);
      end;
end;

procedure BackSortArray(var a: arr);
var
  i, j, k: word;
  l: word;
begin
  l := p * m * n;
  for k := 1 to p do
    for j := 1 to n do
      for i := 1 to m do
      begin
        a[k, i, j] := l;
        dec(l);
      end;  
end;


end.