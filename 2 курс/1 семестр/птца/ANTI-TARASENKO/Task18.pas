const
  sep = '|';
  filename = 'Task18.csv';

var
  A, B, C, D, X, Y, V, W: string;

function rightBin(Val: string; l: integer): string;
var
  str: string;
begin
  for i: integer := 1 to l - Val.Length do
    Str += '0';
  Str += Val;
  result := Str;
end;

function AdditionalCode(Val: string): string;
begin
  if (val[1] = '1') then
  begin
    for i: integer := 1 to Val.Length do
    begin
      if (val[i] = '1') then 
        Val[i] := '0'
      else
        Val[i] := '1';
    end;
  end;
  
  if (val[Val.Length] = '1') then 
  begin
    Val[Val.Length] := '0';
    for i: integer := Val.Length - 1 downto 2 do
      if (val[i] = '1') then 
        Val[i] := '0'
      else
      begin
        Val[i] := '1';
        break;
      end;  
  end
  else
    Val[Val.Length] := '1';
  
  Result := Val;
end;

function InverseCode(Val: string): string;
begin
  if (val[1] = '1') then
  begin
    for i: integer := 1 to Val.Length do
    begin
      if (val[i] = '1') then 
        Val[i] := '0'
      else
        Val[i] := '1';
    end;
  end;
  
  Result := Val;
end;

function MinusVal(Val: string): string;
begin
  Val[1] := '1';
  Result := Val;
end;

function InverseFraction(Val: string): string;
begin
  if (Val[1] = '1') then
    Result := '1,' + InverseCode(Val.Substring(2))
  else
    Result := Val;
end;

function AdditionalFraction(Val: string): string;
begin
  if (Val[1] = '1') then
    Result := '1,' + AdditionalCode(Val.Substring(2))
  else
    Result := Val;
end;

begin
  
  A := '0,10000110';
  B := '0,11111010';
  C := '0,10010001';
  D := '0,11101111';
  
  X := '0,10000110';
  Y := '0,11111010';
  V := '0,10010000';
  W := '0,11101111';
  
  writeln('------------------------------------------------------------');
  writeln(' Числа |  Прямой код  |  Обратный код  | Дополнительный код ');
  writeln('------------------------------------------------------------');
  writeln('  +A   |', A:14, '|', InverseFraction(A):16, '|', AdditionalFraction(A):20);
  writeln('  -A   |', MinusVal(A):14, '|', InverseFraction(MinusVal(A)):16, '|', AdditionalFraction(MinusVal(A)):20);
  writeln('  +B   |', B:14, '|', InverseFraction(B):16, '|', AdditionalFraction(B):20);
  writeln('  -B   |', MinusVal(B):14, '|', InverseFraction(MinusVal(B)):16, '|', AdditionalFraction(MinusVal(B)):20);
  writeln('  +C   |', C:14, '|', InverseFraction(C):16, '|', AdditionalFraction(C):20);
  writeln('  -C   |', MinusVal(C):14, '|', InverseFraction(MinusVal(C)):16, '|', AdditionalFraction(MinusVal(C)):20);
  writeln('  +D   |', D:14, '|', InverseFraction(D):16, '|', AdditionalFraction(D):20);
  writeln('  -D   |', MinusVal(D):14, '|', InverseFraction(MinusVal(D)):16, '|', AdditionalFraction(MinusVal(D)):20);
  writeln('  +X   |', X:14, '|', InverseFraction(X):16, '|', AdditionalFraction(X):20);
  writeln('  -X   |', MinusVal(X):14, '|', InverseFraction(MinusVal(X)):16, '|', AdditionalFraction(MinusVal(X)):20); 
  writeln('  +Y   |', Y:14, '|', InverseFraction(Y):16, '|', AdditionalFraction(Y):20);
  writeln('  -Y   |', MinusVal(Y):14, '|', InverseFraction(MinusVal(Y)):16, '|', AdditionalFraction(MinusVal(Y)):20); 
  writeln('  +V   |', V:14, '|', InverseFraction(V):16, '|', AdditionalFraction(V):20);
  writeln('  -V   |', MinusVal(V):14, '|', InverseFraction(MinusVal(V)):16, '|', AdditionalFraction(MinusVal(V)):20); 
  writeln('  +W   |', W:14, '|', InverseFraction(W):16, '|', AdditionalFraction(W):20);
  writeln('  -W   |', MinusVal(W):14, '|', InverseFraction(MinusVal(W)):16, '|', AdditionalFraction(MinusVal(W)):20); 
  writeln('------------------------------------------------------------');
end.