unit TCOM;
{Golovnyy modul, mistyt'  ogoloshennia golovnogo masyvu,
masyvu sum elementiv, konstanty rozmirnostei cyh masyviv,
procedury zapovnennia ta vyvodu}

interface

const
  p = 4;
  m = 64;
  n = 64;

type
  arr = array[1..p, 1..m, 1..n] of word;
  s = array[1..n] of word;

var
  A:arr;
  Sum:s;

procedure NewSum(k:word);{procedura zapovnenia vektoru Sum, k-pererizy golovnogo masyvu}
procedure Output(const a: arr);
procedure Sort(var a: arr);
procedure UnSort(var a: arr);
procedure BackSort(var a: arr);


implementation

uses
  crt;

procedure NewSum(k:word);
var i,j:word;
begin
 for j:=1 to n do
  begin
   Sum[j]:=0;
   for i:= 1 to m do
    Sum[j]:=Sum[j]+A[k,i,j];
  end;
end;

procedure OutpSum(const sum: s);
var i:word;
begin
 for i:=1 to n do Write(Sum[i]:6);
end;


procedure Output(const a: arr);
var
  i, j, k: word;
begin
  for k := 1 to p do
  begin
    NewSum(k);
    Writeln;
    for i := 1 to m do
    begin
      for j := 1 to n do
        write(a[k, i, j]:6);
      writeln;
    end;
    writeln;
    OutpSum(Sum);
  end;
end;



procedure UnSort(var a: arr);
var
  i, j, k: word;
begin
  randomize;
  for k := 1 to p do
    for i := 1 to m do
      for j := 1 to n do
        a[k, i, j] := random(p * m * n);
end;

procedure Sort(var a: arr);
var
  i, j, k: word;
  l: word;
begin
  l := 1;
  for k := 1 to p do
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l;
        inc(l);
      end;
end;

procedure BackSort(var a: arr);
var
  i, j, k: word;
  l: word;
begin
  l := p * m * n;
  for k := 1 to p do
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l;
        dec(l);
      end;  
end;



end.