var
  a,b: string;
  res: double := 0;

function rightBin(Val: string; l: integer): string;
var
  str: string;
begin
  for i: integer := 1 to l - Val.Length do
    Str += '0';
  Str += Val;
  result := Str;
end;

begin
  readln(a);
  readln(b);
  
  var a_int:=System.Convert.ToInt32(a,2);
  var b_int:=System.Convert.ToInt32(b,2);
  
  writeln('--------');
  writeln(rightBin(System.Convert.ToString(a_int-b_int,2),a.Length),' -> ',a_int,'-',b_int,'=',a_int-b_int);
end.