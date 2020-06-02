program LABO5;
uses crt;
const n=4;m=7; g=9;l=3;

type
 Tmas1=array [1..n,1..m] of byte;
 Tmas2=array [1..g,1..l] of byte;
 Pmas1=^Tmas1;
 Pmas2=^Tmas2;

var
 A:Pmas1; B:Pmas2;

procedure input(P:pointer; k:byte);
var i,j:integer;
begin
case k of
 1: begin
    for i:=1 to n do
       for j:=1 to m do
         Pmas1(P)^[i,j]:=random(25);
    end;
  2: begin
    for i:=1 to g do
       for j:=1 to l do
         Pmas2(P)^[i,j]:=random(25);
    end;
  end;
end;
procedure output(P:pointer; k:byte);
var i,j:integer;
begin
case k of
 1: begin
  writeln('Massiv 1');
    for i:=1 to n do begin
       for j:=1 to m do
         write(Pmas1(P)^[i,j]:5);
        writeln;
    end;
    end;
  2: begin
    writeln('Massiv 2');
    for i:=1 to g do begin
       for j:=1 to l do
         write(Pmas2(P)^[i,j]:5);
        writeln;
     end;
   end;
  end;
 end;
procedure exchange(P:pointer; k:byte);
var i,j,F:byte; max:integer; posi,posj:byte;
begin
case k of
 1: begin
 for j:=1 to (m div 2) do
     for i:=1 to n do
         begin
         F:=Pmas1(P)^[i,m-j+1];
         Pmas1(P)^[i,m-j+1]:=Pmas1(P)^[i,1+j-1];
         Pmas1(P)^[i,1+j-1]:=F;
         end;
    end;

2: begin
   for j:=1 to (l div 2) do
     for i:=1 to g do
         begin
            F:=Pmas2(P)^[i,l-j+1];
            Pmas2(P)^[i,l-j+1]:=Pmas2(P)^[i,1+j-1];
            Pmas2(P)^[i,1+j-1]:=F;
         end;
   end;
end;
end;
begin
clrscr;
randomize;
new(A);
input(A,1);
output(A,1);
exchange(A,1);
output(A,1);
dispose(A);
readln;
new(B);
input(B,2);
output(B,2);
exchange(B,2);
output(B,2);
dispose(B);
readln;
end.
