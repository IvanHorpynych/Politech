const
  sep = '|';

var
  INum2, ONum8, ONum16: string;
  NumValue:integer;

begin
  write('Input your value(Base 2): ');
  readln(INum2);
  
  if not (INum2.Contains(',')) then 
  begin
    NumValue := System.Convert.ToInt16(INum2,2);
    ONum8 := System.Convert.ToString(NumValue,8);
    ONum16 := System.Convert.ToString(NumValue,16).ToUpper();
    
    writeln('Base  2: ',INum2);
    writeln('Base  8: ',ONum8);
    writeln('Base 16: ',ONum16);
  end
  else // Если дробь
  begin
        
    INum2 := INum2.Remove(0,2);
    
    NumValue := System.Convert.ToInt16(INum2.Insert(INum2.Length,'0'),2);
    ONum8 := System.Convert.ToString(NumValue,8);
    
    NumValue := System.Convert.ToInt16(INum2,2);
    ONum16 := System.Convert.ToString(NumValue,16).ToUpper();
    
    writeln('Base  2: 0,',INum2);
    writeln('Base  8: 0,',ONum8);
    writeln('Base 16: 0,',ONum16);
  end;
  
end.