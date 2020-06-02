uses
  crt, TTIME,TCOM,TALG;
  

{$F-}

procedure Main;
const n=14;
var ind_var_sort,ind_var_alg,i:integer;
    Min,Max:longint;
    B:array[1..n]of longint;
    Sum:real;
begin

 ind_var_alg:=1;
 repeat
  ind_var_sort:=1;
  case ind_var_alg of
   1: pr:=Alg454;
   2: pr:=Alg459;
   3: pr:=Alg4522;
   4: pr:=Alg4523;
  end;{case} 
  
  repeat
   case ind_var_sort of
    1:  for i:=1 to n do
         begin
          UnSort(A);
          B[i]:=Time(pr);
         end;  
    2: for i:=1 to n do
         begin
          Sort(A);
          B[i]:=Time(pr);
         end; 
    3: for i:=1 to n do
         begin
          BackSort(A);
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
   if ind_var_sort<>3 then Write(Sum:7:1)
    else Writeln(Sum:7:1);
    
   inc(ind_var_sort);
  until ind_var_sort>3;
  inc(ind_var_alg);
 until ind_var_alg>4;

end;
{$F+}

begin
 clrscr;
 Main;
 readln;
end.