const
  sep = '|';

var
  i, m: integer;
  pz: integer;
  px: string;

begin
  write('Input your value ');
  readln(px);
  //px := '10111001';
  m := px.Length;
  pz := 0;  
  
  writeln(sep, '':(m div 2),'PX','':(m div 2), sep, '':(m div 2),'PZ','':(m div 2), sep, ' LHK ', sep);  
  writeln(sep, ' ',px:m,' ', sep, ' ',pz:m,' ', sep, i:4,' ', sep);  
  
  for i := 1 to m do
  begin        
    pz := StrToInt(px[1])+pz;
    writeln(sep, ' ':m+2, sep, ' ',pz:m,' ', sep, i:4,' ', sep);  
    Delete(px,1,1);
    
    if (px.Length = 0) then break;
    
    pz*=2;
    writeln(sep, ' ',px:m,' ', sep, ' ',pz:m,' ', sep, '':5, sep);  
  end;
end.