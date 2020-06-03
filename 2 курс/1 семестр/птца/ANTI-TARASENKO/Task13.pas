var
  s: string;
  res: double := 0;

function IntPart(s: string): real;
var
  res: real := 0;
begin
  for i: integer := 1 to s.Length do
    if (s[i] = '1') then
      res += Power(-2, s.Length - i);
  
  Result := res;
end;

function FractionPart(s: string): real;
var
  res: real := 0;
begin
  for i: integer := 1 to s.Length do
    if (s[i] = '1') then
      res += Power(-2, -i);
  
  Result := res;
end;

begin
  readln(s);
  
  if (s.Contains(',')) then
  begin
    res += IntPart(s.Substring(0,Pos(',',s)-1));
    res += FractionPart(s.Substring(Pos(',',s)));
  end
  else
    res += IntPart(s);
  
    writeln(res);
end.