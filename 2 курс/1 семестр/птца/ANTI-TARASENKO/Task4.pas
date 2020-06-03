const
  sep = '|';

var
  px, py, pr, i, x: integer;
  pz: string;
  pz_l: integer;

begin
  write('Input your value ');
  readln(x);
  //x := 165;
  pz_l := System.Convert.ToString(x, 2).Length;
  px := x;
  py := x div 2;
  pz := '';
  i := 0;
  
  writeln(sep, ' i ', sep, '  PX  ', sep, '  PY  ', sep, '  PR  ', sep, '':(pz_l div 2-1),'PZ','':(pz_l div 2 -1), sep, ' LHK ', sep);
  
  while (px > 0) do
  begin
    PR := PX - PY * 2;
    PZ := PR.ToString() + PZ;
    writeln(sep, i:2, ' ', sep, px:4, '  ', sep, py:4, '  ', sep, pr:4, '  ', sep, pz:pz_l, sep, i:4, ' ', sep);
    PX := PY;
    PY := PX div 2;
    Inc(i);
  end;
end.