function printline(val:integer):string; 
begin    
  writeln(val,' -> (', val mod 3,', ', val mod 5,', ', val mod 7,', ', val mod 11,')');
end;

procedure printall(val:string);
var a,b,c:integer;
    a3,b3,c3,
    a5,b5,c5,
    a7,b7,c7,
    a11,b11,c11:integer;
begin  
  a := System.Convert.ToInt32(val[1].ToString());
  b := System.Convert.ToInt32(val[2].ToString());
  c := System.Convert.ToInt32(val[3].ToString());
  
  a3 := a mod 3;
  b3 := b mod 3;
  c3 := c mod 3;
  
  a5 := a mod 5;
  b5 := b mod 5;
  c5 := c mod 5;
  
  a7 := a mod 7;
  b7 := b mod 7;
  c7 := c mod 7;
  
  a11 := a mod 11;
  b11 := b mod 11;
  c11 := c mod 11;
  
  printline(a);
  printline(b);
  printline(c);
  writeln();
  
  var tmp:integer:=(a3+b3+c3) mod 3;
  
  writeln(Format('a1 = r3(1*{0} + 1*{1} + 1*{2}) = {3} ',a3,b3,c3,tmp));
  
  tmp:= c5 mod 5;
  
  writeln(Format('a1 = r5(0*{0} + 0*{1} + 1*{2}) = {3} ',a5,b5,c5,tmp));
  
  tmp:= (2*a7+3*b7+c7) mod 7;
  
  writeln(Format('a1 = r7(2*{0} + 3*{1} + 1*{2}) = {3} ',a7,b7,c7,tmp));
  
  tmp:= (a11+10*b11+c11) mod 11;
  
  writeln(Format('a1 =r11(1*{0} +10*{1} + 1*{2}) = {3} ',a11,b11,c11,tmp));
  
end;

begin
  printall('208');
end.