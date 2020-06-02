program l;
uses crt;
const n=8;
var A:array[1..n,1..n] of real;
    X: array[1..n] of real;
    f:integer;u:real;

  procedure vvod;
   var i:1..n; j:1..n;
  begin
   for i:=1 to n do
    begin
    for j:=1 to n do
    begin
    A[i,j]:=(exp(ln(0.2*i)*j)-11.2*i+9.2);
    end
   end;
  end;

  procedure vuvod;
   var i:1..n; j:1..n;
  begin
   for i:=1 to n do
    begin
     for j:=1 to n do
      write(A[i,j]:9:3);
      writeln;
    end;
  end;

  function NomerRyadkaIzMaxSumElementiv: integer;
  var S, S_max: real;B:array[1..2]of real;
      max_r,i,j,k: integer;
   begin
   writeln;
   S_max:=-1;
    for i:=1 to n do
    begin
     S:=0;
     for j:=1 to n do
      S:=S+abs(A[i,j]);
     if S>S_max then
      begin
      S_max:=S;
      max_r:=i;
     end;
    end;
    for j:=1 to n do
     X[j]:=A[max_r,j];
     B[1]:=X[1];
     B[2]:=X[2];
    for i:=3 to n do
     X[i-2]:=X[i];
     X[n-1]:=B[1];
     X[n]:=B[2];
     write('X=( ');
    for i:=1 to n do
     write(X[i]:3:3,' ');
     writeln(')');
     s:=0;
    for i:=1 to n do
    S:=S+X[i];
    u:=sqrt(abs(S));
    writeln('U=',u:3:3);
   end;

begin
clrscr;
vvod;
vuvod;
f:=NomerRyadkaIzMaxSumElementiv;
readln;
end.

