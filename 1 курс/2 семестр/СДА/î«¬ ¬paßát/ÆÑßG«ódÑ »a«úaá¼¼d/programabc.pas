program abc;

uses crt,dos;

const
  p = 1;
  m = 40;
  n = 40;

  type
  arr = array[1..p, 1..m, 1..n] of integer; {Zadannya korystuvats'koho typu "masyv"}
  vector=array [1..n*m] of integer;
  proc=procedure(var A:arr);
	 vect=procedure(var C:vector);
var A:arr; {Zminna typu masyv}
    C:vector;
    pr:proc; {Zminna protsedurnoho typu}
	 vec:vect;
   T: integer;
    procedure SortVect(var C: vector);
var
  i,j:integer;
    begin
      for i:=1 to (n*m) do
      C[i]:=i;
    end;

procedure UnSortVect(var C: vector);
var
  i,j:integer;
    begin
      j:=n*m;
      for i:=1 to (j) do
        C[i]:=random(j);
    end;

procedure BackSortVect(var C: vector);
var
  i,j,g:integer;
    begin
      j:=n*m; g:=j;
      for i:=1 to j do
      begin
        C[i]:=g;
        dec(g);
      end;
    end;

function TimeVector(vec:vect):longint;
type TTime=record
      Hours,Min,Sec,HSec:word;
     end;

var StartTime,FinishTime:TTime;

function ResTime(const STime,FTime:TTime):longint;
begin
 ResTime:=360000*Longint(FTime.Hours)+
          6000*Longint(FTime.Min)+
          100*Longint(FTime.Sec)+
            Longint(FTime.HSec)-
          360000*Longint(STime.Hours)-
          6000*Longint(STime.Min)-
          100*Longint(STime.Sec)-
            Longint(STime.HSec);
end;

begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
 vec(C); {Zapusk obranoyi protsedury sortuvannya}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 TimeVector:=ResTime(StartTime,FinishTime);
end;
{$F+}
procedure SortVector(var C:vector);
var i,j,k,B: integer;
begin
  k:=m*n;
  for i:=2 to k do
  begin
    j:=i;
    while (j>1) and (C[j]<C[j-1]) do
    begin
      B:=C[j];
      C[j]:=C[j-1];
      C[j-1]:=B;
      j:=j-1;
    end;
  end;
end;
{$F-}
{$F+}
procedure SortVector1(var C:vector);
  begin
    end;
{$F-}

begin
clrscr;
vec:=SortVector;
  SortVect(C);
  T:=TimeVector(vec);
  writeln('Sort Vector:',T*p);
  UnSortVect(C);
  T:=TimeVector(vec);
  writeln('UnSort Vector:',T*p);
  BackSortVect(C);
  T:=TimeVector(vec);
  writeln('BackSort Vector:',T*p);
  readln;


  vec:=SortVector1;
  SortVect(C);
  T:=TimeVector(vec);
  writeln('Sort Vector:',T*p);
  UnSortVect(C);
  T:=TimeVector(vec);
  writeln('UnSort Vector:',T*p);
  BackSortVect(C);
  T:=TimeVector(vec);
  writeln('BackSort Vector:',T*p);
  readln;
  end.



