const
  sep = '|';

var
  i, m: integer;
  pz: real;
  px: string;

begin
  write('Input your value ');
  readln(px);
  //px := '0.01011011';
  m := px.Length;
  pz := 0;  
  
  writeln(sep, '':(m div 2), 'PX', '':(m div 2), sep, '':(m div 2), 'PZ', '':(m div 2), sep, ' LHK ', sep);  
  writeln(sep, ' ', px:m, ' ', sep, ' ', pz:0:m-2, ' ', sep, i:4, ' ', sep);  
  
  for i:=1 to px.Length-2 do
  begin    
    pz := StrToInt(px[px.Length]) + pz;
    writeln(sep, ' ':m + 2, sep, ' ', pz:0:m-2, ' ', sep, '':5, sep);  
    pz /= 2;           
    Delete(px, px.Length, 1);    
    writeln(sep, ' ', px:m, ' ', sep, ' ', pz:0:m-2, ' ', sep, i:4, ' ', sep);  
  end;
end.