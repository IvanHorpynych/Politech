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

      for i:=2 to (m*n) do
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
 procedure Main_3(var C:vector);
var i,T:integer;
  begin
    vec:=SortVector;
  SortVect(C);
  for i:=1 to n*m do write(C[i]:5);
  writeln('Sort Vector:',TimeVector(vec)*p);
  readln;
   for i:=1 to n*m do write(C[i]:5);
   readln;
  UnSortVect(C);
  for i:=1 to n*m do write(C[i]:5);
  writeln('UnSort Vector:',TimeVector(vec)*p);
  readln;
   for i:=1 to n*m do write(C[i]:5);

  readln;
  BackSortVect(C);
  for i:=1 to n*m do write(C[i]:5);
  writeln('BackSort Vector:',TimeVector(vec)*p);
  readln;
   for i:=1 to n*m do write(C[i]:5);
   readln;
ch:=readkey;
if ch=#32 then exit;
  end;
end.
