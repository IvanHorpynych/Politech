program E;
uses crt;
const n=4;
type Matrix=array[1..n,1..n] of integer;
var A:Matrix;

procedure Input_Output(var A:Matrix);
var i,j:integer;
begin
 for i:=1 to n do
  begin
   for j:=1 to n do
    begin
     A[i,j]:=random(5)+1;
     Write(A[i,j]);
    end;
   Writeln;
  end;
end;
procedure Action(A:Matrix);
var i,j,Min,min_j,Dob:integer;
begin
 Dob:=1;
 Min:=1;
 min_j:=1;
 for i:=1 to n do
  Min:=Min*A[i,1];
 for j:=2 to n do
  begin
   for i:=1 to n do
    Dob:=Dob*A[i,j];
   if Dob<Min then
    begin
     Min:=Dob;
     min_j:=j;
    end;
    Dob:=1;
   end;
 Writeln('Min=',Min,' at position ',min_j);
end;
begin
 clrscr;
 randomize;
 Input_Output(A);
 Action(A);
 readkey;
end.




