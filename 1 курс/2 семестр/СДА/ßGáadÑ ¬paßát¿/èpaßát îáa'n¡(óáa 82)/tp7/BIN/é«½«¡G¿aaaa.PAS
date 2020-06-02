program laba4;
uses crt;
type tmas=array[1..13] of real;
var A,B,C:tmas;
procedure fillmatrix(var masiv:tmas;n:byte);
var i:byte;
begin
     for i:=1 to n do
         read(masiv[i]);
end;
procedure normmatrix(var masiv:tmas; n:byte);
var i:byte; max:real;
begin
     max:=masiv[1];
     for i:=1 to n do
         if masiv[i]>max then max:=masiv[i];
     if max<>0 then for i:=1 to n do
                        masiv[i]:=masiv[i]/max
     else writeln('devision by zero');
     writeln('max=',max:0:2);
end;
procedure showmatrix(masiv:tmas;n:byte);
var i:byte;
begin
     for i:=1 to n do
         write(masiv[i]:10:3);
     writeln;
     readkey;
end;
begin
clrscr;
write('A=');
fillmatrix(A,8);
normmatrix(A,8);
showmatrix(A,8);
write('B=');
fillmatrix(B,13);
normmatrix(B,13);
showmatrix(B,13);
write('C=');
fillmatrix(C,10);
normmatrix(C,10);
showmatrix(C,10);
end.