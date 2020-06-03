const
  sep = '|';

var
  i, m: integer;
  pz: string;
  px, x: real;

begin
  write('Input your value ');
  readln(x);
  //x := 0.644531;
  m := 8;
  px := x;
  pz := '0.';  
  
  writeln(sep, '    PX    ', sep, '      PZ    ', sep, ' LHK ', sep);
  writeln(sep, ' ', px, ' ', sep, ' 0.00000000 ', sep, '   0 ', sep);
  
  for i := 1 to m do
  begin
    px := px * 2;
    pz := pz+trunc(px).ToString();
    writeln(sep, ' ', px:0:6, ' ', sep, pz:m + 3, ' ', sep, i:4, ' ', sep);
    px := Frac(px);
    writeln(sep, ' ', px:0:6, ' ', sep, '            ', sep, '     ', sep);
    
  end;
end.