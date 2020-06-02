program Laboratornaja3;
uses crt;
const n=7;
type
  matrix= array [1..n, 1..n] of real;
var
  x: array [1..n] of real;
  A: matrix;
  i,j: integer;
  f,u,max,min: real;
  ch: char;
  procedure create_matrix(var A:matrix; i,j:integer);
  begin
   for i:=1 to n do
     begin
     writeln ('|-------------------------------------------------------|');
     write ('|');
         for j:=1 to n do
         begin
             f:=2.5*i+8.75*j-j-3;
             A[ i, j ]:=f;
         write(A[ i, j ]:6:2,'|':2);
          end;
     writeln;
     end;
  end;   
begin
clrscr;
repeat
writeln ('Vipolnil: Gorpinich-Raduzhenko I.A., KV-41');
i:=1; j:=1;
 begin
    create_matrix(a,i,j);
 writeln ('|-------------------------------------------------------|');
 end;
 begin
     for i:=1 to n do
         begin
         j:=1;
         max:=A[ i, j ]; min:=A[ i, j ];
             for j:=2 to n do
             begin
             if (A[ i, j ]>=max) then
                max:=A[ i, j ];
             if (A[ i, j ]<=min) then
                min:=A[ i, j ];
             end;
         x[i]:=abs(max-min);
         end;
 end;
begin
 max:=abs(x[1]);
     for i:=2 to n do
         begin
             if (abs(x[i])>=max) then
                max:=abs(x[i]);
         end;
 end;
 writeln;
 write ('X=');
         for i:=1 to n do
         begin
             write(' ',x[i]:5:1);
         end;
 writeln;
 writeln ('U=',max:5:2);
 writeln;
 write ('Nagmite "Y" dlya vixoda ili ENTER dlya povtora:');
 read;
 writeln;
 ch:=readkey;
 until (ch='y');
 end.