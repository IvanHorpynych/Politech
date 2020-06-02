unit WORKAR; {Protsedura Workaround, u yakiy pryvedenyy alhorytm z neobkhidnymy metodamy obkhoda}

interface

uses GLOBAL; {Opysannya dostupnoyi informatsiyi dlya yinshykh moduliv}


procedure WORKAR1711(var A: arr); {Alhorytm "vstavka – obmin" z vykorystannyam dodatkovoho masyvu}
procedure WORKAR1712(var A: arr);  {Alhorytm "vstavka-obmin" z vykorystannyam elementiv "uyavnoho" vektora}
procedure WORKAR1713(var A: arr); {Alhorytm "vstavka-obmin", zdiysnyuyuchy obkhid bezposeredn'o po elementakh zadanoho masyvu}
procedure SortVector(var C: vector); {Alhorytm "vstavka – obmin" sortuvannya vektora}

implementation


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

procedure WORKAR1712(var a: arr); {Alhorytm "vstavka-obmin" z vykorystannyam elementiv "uyavnoho" vektora}

var k,j, i, B:integer; {k,i,j: koordynaty 3-vymirnoho masyvu; B-dodatkova komirka pam"yati}
begin
 for k:=1 to p do {Lichyl'nyky prokhodu po pererizam 3-vymirnoho masyvu}
   for i:=2 to (n*m) do {Lichyl'nyky prokhodu po uyavnomu vektoru}
    begin
      j:=i;
      
      while (j>1) and (A[k,((j-1) div n)+1,((j-1) mod n)+1]<A[k,((j-2) div n)+1,((j-2) mod n)+1]) do {Formula perevedennya}
        begin
          B:=A[k,((j-1) div n)+1,((j-1) mod n)+1]; {Zmina elementiv mistsyamy}
          A[k,((j-1) div n)+1,((j-1) mod n)+1]:=A[k,((j-2) div n)+1,((j-2) mod n)+1];
          A[k,((j-2) div n)+1,((j-2) mod n)+1]:=B;
          j:=j-1;
        end;
    end;      
end;

procedure WORKAR1713(var a: arr); {Alhorytm "vstavka-obmin", zdiysnyuyuchy obkhid bezposeredn'o po elementakh zadanoho masyvu}
var 
    i, j, k, Z, H, f,g, B: integer; {k,i,j: koordynaty masyvu; B-dodatkova komirka pam"yati; Z, H, f, g: dodatkovi zminni}
begin
 for k:=1 to p do {Lichyl'nyk prokhodu po pererizam 3-vymirnoho masyvu}
  for i:=1 to m do {Lichyl'nyky prokhodu po koordynatam pererizu}
    for j:=1 to n do 
    begin
        Z:=i; H:=j; {Kopiyuvannya koordynat elementa, dlya mozhlyvosti yikh zminy}
        if H<>1 then {perevirka na pershist' elementa u ryadku}
          begin
            f:=Z; g:=H-1; {Vidbuvayet'sya perekhid elementiv koordynat g,f na susidniy element zliva}
          end
        else
          begin
            f:=Z-1; g:=n;{Vidbuvayet'sya perekhid koordynat g,f na ostanniy element poperedn'oho ryadka}
          end;
      
    while (H>=1) and (f>=1) and (A[k,Z,H]<A[k,f,g]) do {Perevirka za umovoyu alhorytmu; H,f- perevirka vykhodu za ramky }
      begin
        B:=A[k,Z,H]; {Zmina elementiv mistsyamy}
        A[k,Z,H]:=A[k,f,g];
        A[k,f,g]:=B;

        dec(H);   dec(g); {Zmen'shennya koordynat, vidpovidayuchykh za prokhid po stovptsyam elementa}
        
        if H=1 then {Perevirka na pershchist' koordynaty elementu}
          begin
            g:=n; f:=Z-1; {Vidbuvayet'sya perekhid dodatkovykh koordynat g,f na ostanniy element poperedn'oho ryadka}
          end
        else 
          if H=0 then {Perevirka na zakin'chennya ryadka}
            begin
              H:=n; Z:=Z-1; {Dodatkovi koordynaty H,Z perekhodyat' na ostanniy element poperedn'oho ryadka}
            end;
      end;
    end;
end;

procedure SortVector(var C:vector); {Alhorytm "vstavka – obmin" sortuvannya vektora}
var i,j,k,B: integer; {i,j: koordynaty prokhodu  po vektoru; B-dodatkova komirka pam"yati;}
begin

  for i:=2 to (m*n) do {Lichyl'nyk prokhodu po vektoru}
  begin
    j:=i;
    while (j>1) and (C[j]<C[j-1]) do {Pochynayet'sya robota bezposeredn'o hibrydnoho alhorytmu "vstavka – obmin"}
    begin
      B:=C[j]; {Zmina elementiv mistsyamy}
      C[j]:=C[j-1];
      C[j-1]:=B;
      j:=j-1; {Zmishchennya koordynaty vlivo}
    end;
  end;
end;

end.