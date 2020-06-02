program Var_4_1;
uses
    crt;
const
    m = 3;
    n = 4;
var
    a : array[1..m,1..n] of integer;
    i,j,k:integer;
procedure Qsort(L,R:integer);
var 
    f,tmp,i,j,b:integer;
begin
    for f:=1 to m do begin
    b:=a[f,L+R div 2];
    i:=L;
    j:=R;
    while i<=j do begin
      while a[f,i] < b do inc(i);
      while a[f,j] > b do dec(j);
      if i <= j then begin
        tmp:=a[f,i];
        a[f,i]:=a[f,j];
        a[f,j]:=tmp;
        inc(i);
        dec(j);
      end;
    end;
    if L < j then Qsort(l,j);
    if i < R then Qsort(r,i);
    end;
end;

BEGIN
  for i:=1 to m do 
    for j:=1 to n do
      read(a[i,j]);
  k:=0;
  for i:=1 to m do begin
    for j:=1 to n do begin
      write(a[i,j]:4);
      if a[i,j] = 0 then inc(k);
    end;
    writeln;
  end;
  if k mod 2 = 0 then 
  Qsort(1,n);
  
  for i:=1 to m do begin
    for j:=1 to n do begin
      write(a[i,j]:4);
    end;
    writeln;
  end;
END.    