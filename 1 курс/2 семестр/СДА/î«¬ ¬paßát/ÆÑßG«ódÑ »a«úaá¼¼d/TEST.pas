unit TEST;

interface

uses
  GLOBAL,WORKAR,TIMER,crt,dos;

procedure Main_1(var a:arr; var C:vector);
procedure Main_2(var a:arr; var C:vector);

implementation

{type proc=procedure(var A:arr);}

var pr:proc;
    A:arr;
	ch:char;
   T:longint;
procedure Main_1(var a:arr; var C:vector);
const n=14;
var var_sort,var_alg,i,T:integer;
    Min,Max:longint;
    B:array[1..n]of longint;
    Sum:real;
begin

  
end;

procedure Main_2(var a:arr; var C:vector);
var var_sort,var_alg,B, i:integer;
begin
clrscr;
writeln('                    Sort    UnSort   BackSort');
 var_alg:=1;
 repeat
  var_sort:=1;
  case var_alg of
   1: begin
        write('Workaround 1:');
       { pr:=WORKAR1711;}
      end;
   2: begin
        write('Workaround 2:');
       { pr:=WORKAR1712;}
      end;

   3: begin
        write('Workaround 3:');
       { pr:=WORKAR1713;  }
      end;
  end;{case}

  repeat
   case var_sort of
    1:   begin
          SortArray(A);
          WORKAR1711(A,T);
          {B:=Time(pr);}
         end;
    2:   begin
          UnSortArray(A);
          WORKAR1711(A,T);
          {B:=Time(pr);}
         end;
    3:   begin
          BackSortArray(A);
          WORKAR1711(A,T);
          {B:=Time(pr);}
         end;
   end;{case}

   if var_sort<>3 then Write(T:10)
    else Writeln(T:10);
   inc(var_sort);
  until var_sort>3;
  inc(var_alg);
 until var_alg>3;
Writeln('Press Enter for continue');
readln;

{vec:=SortVector;}
  SortVect(C);
  SortVector(C,T);
  {T:=TimeVector(vec);}
  writeln('Sort Vector:',T*p);
  UnSortVect(C);
  SortVector(C,T);
  {T:=TimeVector(vec);}
  writeln('UnSort Vector:',T*p);
  BackSortVect(C);
  SortVector(C,T);
  {T:=TimeVector(vec);}
  writeln('BackSort Vector:',T*p); 

  Writeln('Press Enter for continue');
ch:=readkey;
if ch=#32 then exit;   
end;

end.