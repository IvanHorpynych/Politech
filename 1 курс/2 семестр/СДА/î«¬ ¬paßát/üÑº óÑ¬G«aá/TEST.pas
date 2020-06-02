unit TEST;

interface

uses
  GLOBAL,WORKAR,TIMER,crt,dos;
  
procedure Main_1(var a:arr);
procedure Main_2(var a:arr);
implementation

type proc=procedure(var A:arr);

var pr:proc;
    A:arr;
	ch:char;

procedure Main_1(var a:arr);
const n=14;
var var_sort,var_alg,i:integer;
    Min,Max:longint;
    B:array[1..n]of longint;
    Sum:real;
begin
clrscr;
writeln('                    Sort      UnSort   BackSort');
 var_alg:=1;
 repeat
  var_sort:=1;
  case var_alg of
   1: begin
        write('Workaround 1:');
        pr:=WORKAR1711;
      end;
   2: begin
        write('Workaround 2:');
        pr:=WORKAR1712;
      end;

   3: begin
        write('Workaround 3:');
        pr:=WORKAR1713;
      end;
  end;{case}

  repeat
   case var_sort of
    1:  for i:=1 to n do
         begin
          SortArray(A);
          B[i]:=Time(pr);
         end;
    2: for i:=1 to n do
         begin
          UnSortArray(A);
          B[i]:=Time(pr);
         end;
    3: for i:=1 to n do
         begin
          BackSortArray(A);
          B[i]:=Time(pr);
         end;
   end;{case}

   Min:=B[3];
   Max:=B[3];
   for i:=4 to n do
    begin
     if B[i]>Max then Max:=B[i];
     if B[i]<Min then Min:=B[i];
    end;
   Sum:=0;
   for i:=3 to n do Sum:=Sum+B[i];
   Sum:=(Sum-Min-Max)/10;
   if var_sort<>3 then Write(Sum:10:1)
    else Writeln(Sum:10:1);

   inc(var_sort);
  until var_sort>3;
  inc(var_alg);
 until var_alg>3;
Writeln('Press Enter for continue');
ch:=readkey;
if ch=#32 then exit;	 
end;

procedure Main_2(var a:arr);
const n=14;
var var_sort,var_alg,B:integer;
begin
clrscr;
writeln('                    Sort      UnSort   BackSort');
 var_alg:=1;
 repeat
  var_sort:=1;
  case var_alg of
   1: begin
        write('Workaround 1:');
        pr:=WORKAR1711;
      end;
   2: begin
        write('Workaround 2:');
        pr:=WORKAR1712;
      end;

   3: begin
        write('Workaround 3:');
        pr:=WORKAR1713;
      end;
  end;{case}

  repeat
   case var_sort of
    1:   begin
          SortArray(A);
          B:=Time(pr);
         end;
    2:   begin
          UnSortArray(A);
          B:=Time(pr);
         end;
    3:   begin
          BackSortArray(A);
          B:=Time(pr);
         end;
   end;{case}

   if var_sort<>3 then Write(B:10)
    else Writeln(B:10);
   inc(var_sort);
  until var_sort>3;
  inc(var_alg);
 until var_alg>3;
Writeln('Press Enter for continue');
ch:=readkey;
if ch=#32 then exit;   
end;

end.


