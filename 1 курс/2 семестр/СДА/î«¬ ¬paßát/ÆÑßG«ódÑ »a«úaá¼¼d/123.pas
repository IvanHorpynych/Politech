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
  TTime=record
      Hours,Min,Sec,HSec:word;
     end;

var A:arr; {Zminna typu masyv}
    C:vector;
    pr:proc; {Zminna protsedurnoho typu}
	 vec:vect;
   T: integer;

   procedure BackSortArray(var a: arr); {Protsedura zapovnennya oberneno vporyadkovannoho masyvu}
var
  i, j, k: word; {Zminni kordynat}
  l: word; {Zminna, za dopomohoyu yakoyi bude zapovnyuvatysya masyv}
begin
  l := p * m * n; {Zminniy l prysvoyuyet'sya znachennya kil'kosti elementiv masyvu}
  for k := 1 to p do {Lichyl'nyky prokhodu po koordynatam masyvu}
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l; {Prysvoyennya komirtsi masyvu znachennya l}
        dec(l); {Pry kozhnomu prokhodi lichyl'nyka, zminna l bude zmen'shuvatysya na 1}
      end;  
end;

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

function Time(pr:proc):longint;

var StartTime,FinishTime:TTime;

begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
 pr(A); {Zapusk obranoyi protsedury sortuvannya}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 Time:=ResTime(StartTime,FinishTime);
end;

procedure WORKAR1711(var a: arr);{Alhorytm "vstavka – obmin" z vykorystannyam dodatkovoho masyvu}

var ii, k, j, i, B:integer; {ii: koordynata odnovymirnoho masyvu; k,i,j: koordynaty 3-vymirnoho masyvu; B-dodatkova komirka}
    Z:vector; {Dodatkovyy odnovymirnyy masyv}

begin
 for k:=1 to p do {Lichyl'nyky prokhodu po koordynatam 3-vymirnoho masyvu}
  begin
   ii:=1;
   for i := 1 to m do
    for j := 1 to n do
    begin
      Z[ii] := a[k, i, j]; {Perepysuvannya elementiv dvovymirnoho masyvu do odnovymirnoho}
      inc(ii);{Pislya kozhnoho prokhodu lichyl'nyka, kordynata odnovymirnoho masyvu ii zbil'shuyet'sya na 1}
    end;

    for i:=2 to (n*m) do {Prokhid po odnovymirnomu masyvu}
      begin
        j:=i;
        while (j>1) and (Z[j]<Z[j-1]) do {Pochynayet'sya robota bezposeredn'o hibrydnoho alhorytmu "vstavka – obmin"}
          begin
            B:=Z[j]; {Zmina elementiv mistsyamy}
            Z[j]:=Z[j-1];
            Z[j-1]:=B;
            j:=j-1;
          end;
      end;
   ii := 1;
    for i := 1 to m do {Lichyl'nyky prokhodu po koordynatam 2-vymirnoho masyvu}
     for j := 1 to n do
      begin
       a[k, i, j]:= Z[ii]; {Perepysuvannya elementiv odnovymirnoho masyvu  do dvovymirnoho}
       inc(ii); {Pislya kozhnoho prokhodu lichyl'nyka, kordynata odnovymirnoho masyvu ii zbil'shuyet'sya na 1}
      end;   
  end;
end;
begin
  
  write('Workaround 1:');
        pr:=WORKAR1711;
        BackSortArray(A);
          T:=Time(pr);
          writeln(T);
end;