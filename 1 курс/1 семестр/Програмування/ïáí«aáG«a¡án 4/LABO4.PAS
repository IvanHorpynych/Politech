program Laboratornaya4;
uses crt;
type MATR=array [1..7,1..7] of real;
const n=4;
      m=7;
var
   c: real;
   i,j: integer;
   matrix_one, matrix_two: Matr;

procedure create_matrix (var A: Matr; x,y:integer);
begin
     for i:=1 to x do
         begin
         writeln;
         for j:=1 to y do
             begin
             A[i,j]:= random(100);
             if random(2)=1 then
             A[i,j]:=(A[i,j]*(-1));
             write (A[i,j]:6:2,'  ');
             end;
         end;
end;

procedure change_matrix (var A: Matr; x,y: integer);
begin
     for i:=1 to x do
         begin
         for j:=1 to Trunc(y/2) do
             begin
             C:= A[i,j];
             A[i,j]:= A[i,y-j+1];
             A[i,y-j+1]:=C;
             end;
         end;
end;

procedure write_matrix ( A: Matr; x,y:integer);
begin
     for i:=1 to x do
         begin
         writeln;
         for j:=1 to y do
             write(A[i,j]:6:2,'  ');
         end;
end;

begin
randomize;
clrscr;
writeln ('Matrix A');
        begin
        create_matrix(matrix_one,4,7);
        writeln;
        writeln ('|------------------------------------------------------|');
        end;
        readln;
writeln ('Matrix B');
        begin
        create_matrix(matrix_two,7,4);
        writeln;
        writeln ('|------------------------------------------------------|');
        end;
        readln;

change_matrix(matrix_one,n,m);

change_matrix(matrix_two,m,n);

writeln ('Transformed matrix A');
write_matrix(matrix_one,n,m);
writeln;
writeln ('|------------------------------------------------------|');
readln;

writeln ('Transformed matrix B');
write_matrix(matrix_two,m,n);
readln;
end.