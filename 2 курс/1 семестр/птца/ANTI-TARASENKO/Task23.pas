const
  sep = '|';
  filename = 'Task23.csv';

var
  px, py, pz: string;
  CTSh: integer;//Счетчик числа шагов умножения
  n :integer;

function rightBin(Val: string; l: integer): string;
var
  str: string;
begin
  for i: integer := 1 to l - Val.Length do
    Str += '0';
  Str += Val;
  result := Str;
end;  
  
function ShiftRight(s:string; count: integer):string;
begin
  for i:integer:=1 to count do
  begin
    s := s.Remove(s.Length-1,1);
    s := s.Insert(0,'0');
  end;
  
  result := s;
end;

function ShiftLeft(s:string; count: integer):string;
begin
  for i:integer:=1 to count do
  begin
    s := s.Remove(0,1);
    s := s.Insert(s.Length,'0');
  end;
  
  result := s;
end;

function appendNum(Val: string; len: integer): string;
var
  tmp_str: string;
begin
  for i: integer := 1 to len - Val.Length do
    tmp_str += '0';
  
  result := Val+tmp_str;
end;

function Sum(a,b:string):string;
begin
  var A_int,B_int:integer;
  A_int:=System.Convert.ToInt32(a,2);
  B_int:=System.Convert.ToInt32(b,2);
  
  result := System.Convert.ToString(A_int+B_int,2);
end;

procedure printline(px,py,pz,CTSh:string);
begin
  writeln(sep,px:(n+1),sep,py:(2*(n+1)),sep,pz:(2*(n+1)),sep,CTSh:(n div 2 + 1),sep);
  for i:integer:=1 to 6*(n+1)+1 do
    write('-');
  writeln();
end;

procedure f_printline(px,py,pz,CTSh:string); 
var
sep:string := ';,';
f:Text;
begin
  Assign(f,filename);
  Append(f);
  writeln(f,px:(n+1),sep,py:(2*(n+1)),sep,pz:(2*(n+1)),sep,CTSh:(n div 2 + 1),sep);  
  Close(f);
end;

begin
  var f:text;
  Assign(f,filename);
  Rewrite(f);
  Close(f);
  
  
  {write('Input your X');
  readln(px); //X -> PX
  write('Input your Y');
  readln(py); //Y -> PY}
  
  px := '11101111'; //X -> PX
  py := '11111010'; //Y -> PY}
      
  n := Length(px); //запоминаем n (число разрядов)
  
  py := rightBin(py,n+1);
  py := appendNum(py,2*n);
  
  pz := appendNum(pz,2*n); 
  
  CTSh := 1; //0 -> CTSh
  
  printline('PX    Xn','PY   ','PZ        ','СТШ ');
  f_printline('PX    Xn','PY   ','PZ        ','СТШ ');
  printline(px,py,pz,rightBin(System.Convert.ToString(CTSh,2),4));
  f_printline(px,py,pz,rightBin(System.Convert.ToString(CTSh,2),4));
  
  while (true) do
  begin    
    
    if (px[1] = '1') then
    begin      
      printline('','',appendNum(py,2*n),'');
      f_printline('','',appendNum(py,2*n),'');
      PZ := rightBin(Sum(PZ,PY),2*n);      
      printline('','',pz,'');
      f_printline('','',pz,'');
    end;
    
    if (CTSh = n) then break;
    
    py := ShiftRight(py,1);
    px := ShiftLeft(px,1);
    CTSh := CTSh + 1;
    
    printline(px,py,pz,rightBin(System.Convert.ToString(CTSh,2),4));
    f_printline(px,py,pz,rightBin(System.Convert.ToString(CTSh,2),4));
       
  end;  
end.