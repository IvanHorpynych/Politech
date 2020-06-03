const
  sep = '|';
  filename = 'Task27.csv';

var
  px, py, pz, pfi, Zo, shiftDelta: string; 
  CTSh: integer;//Счетчик числа шагов умножения
  n: integer;

procedure printline(px, py, pz, CTSh, pfi, comment: string);
begin
  Zo := '00';
  if (pz.Length > 2 * n + 1) then
  begin
    if (pz.Substring(0, 2) = '11') then
      Zo := '11';    
    pz := pz.Substring(pz.Length - 2 * n);    
  end; 
  writeln(sep, px:(n + 1), sep, py:(n + 1), sep, Zo, sep, pz:(2 * (n + 1)), sep, CTSh:(n div 2 + 1), sep, pfi:3, sep, comment:22, sep);
  for i: integer := 1 to 10 * n - 4 do
    write('-');
  writeln();
end;

procedure f_printline(px, py, pz, CTSh, pfi, comment: string);
var
  sep: string := ';,';
  f: Text;
begin
  Assign(f, filename);
  Append(f);
  
  Zo := '00';
  if (pz.Length > 2 * n + 1) then
  begin
    if (pz.Substring(0, 2) = '11') then
      Zo := '11';    
    pz := pz.Substring(pz.Length - 2 * n);    
  end; 
  writeln(f, px:(n + 1), sep, py:(n + 1), sep, Zo, sep, pz:(2 * (n + 1)), sep, CTSh:(n div 2 + 1), sep, pfi:3, sep, comment:22);
  
  Close(f);
end;

function rightBin(Val: string; l: integer): string;
var
  str: string;
begin
  for i: integer := 1 to l - Val.Length do
    Str += '0';
  Str += Val;
  result := Str;
end;

function ShiftRight(s: string; count: integer): string;
begin
  for i: integer := 1 to count do
  begin
    s := s.Remove(s.Length - 1, 1);        
    if (s.Length > 2 * n + 1) then
      s := s.Insert(0, '1')
    else
      s := s.Insert(0, '0');
  end;  
  result := s;
end;

function appendNum(Val: string; len: integer): string;
var
  tmp_str: string;
begin
  for i: integer := 1 to len - Val.Length do
    tmp_str += '0';
  
  result := Val + tmp_str;
end;

function Sum(a, b: string): string;
begin
  printline('', '', b, '', '', '+Y');
  f_printline('', '', b, '', '', '+Y');
  
  var A_int, B_int: integer;
  A_int := System.Convert.ToInt32(a, 2);
  B_int := System.Convert.ToInt32(b, 2);
  
  printline('', '', rightBin(System.Convert.ToString(A_int + B_int, 2), 2 * n), '', '', 'Результат суммирования');
  f_printline('', '', rightBin(System.Convert.ToString(A_int + B_int, 2), 2 * n), '', '', 'Результат суммирования');
  
  result := System.Convert.ToString(A_int + B_int, 2);
end;

function Minus(a, b: string): string;
begin
  
  var A_int, B_int: integer;
  A_int := System.Convert.ToInt32(a, 2);
  B_int := System.Convert.ToInt32(b, 2);
  
  printline('', '', System.Convert.ToString(-B_int, 2), '', '', '-Y');
  f_printline('', '', System.Convert.ToString(-B_int, 2), '', '', '-Y');
  
  printline('', '', System.Convert.ToString(A_int - B_int, 2), '', '', 'Результат суммирования');
  f_printline('', '', System.Convert.ToString(A_int - B_int, 2), '', '', 'Результат суммирования');
  
  result := System.Convert.ToString(A_int - B_int, 2);
end;

begin
  var f: text;
  Assign(f, filename);
  Rewrite(f);
  Close(f);
  
  
  {write('Input your X');
  readln(px); //X -> PX
  write('Input your Y');
  readln(py); //Y -> PY}
  
  px := '11101111'; //X -> PX
  py := '11111010'; //Y -> PY}
  pfi := '0';
  
  n := Length(px); //запоминаем n (число разрядов)
  
  pz := appendNum(pz, 2 * n); 
  
  CTSh := 0; //0 -> CTSh    
  
  while (true) do
  begin
    if (CTSh = 0) then
    begin
      printline('PX    Xn', 'PY   ', 'PZ        ', 'СТШ ', 'pf', 'Пояснения');
      f_printline('PX    Xn', 'PY   ', 'PZ        ', 'СТШ ', 'pf', 'Пояснения');  
      printline(px, py, pz, rightBin(System.Convert.ToString(CTSh, 2), 4), '0', 'Исходное состояние');
      f_printline(px, py, pz, rightBin(System.Convert.ToString(CTSh, 2), 4), '0', 'Исходное состояние');
    end
    else
    begin
      printline(px, '', pz, rightBin(System.Convert.ToString(CTSh, 2), 4), pfi, 'Cдвиг ' + shiftDelta);
      f_printline(px, '', pz, rightBin(System.Convert.ToString(CTSh, 2), 4), pfi, 'Cдвиг ' + shiftDelta);
    end;
    
    if (pfi = '1') then
    begin
      if (px[px.Length] = '0') and (px[px.Length - 1] = '0') then
      begin
        PZ := rightBin(Sum(PZ, appendNum(PY, 2 * n)), 2 * n);
        if (CTSh = n) then break;    
        px := ShiftRight(px, 2);
        pz := ShiftRight(pz, 2);
        pfi := '0';
        CTSh := CTSh + 2;     
        shiftDelta := '2';
        continue;
      end
      else
      if (px[px.Length] = '1') and (px[px.Length - 1] = '0') then
      begin
        if (CTSh = n) then break;    
        px := ShiftRight(px, 1);
        pz := ShiftRight(pz, 1);
        pfi := '1';
        CTSh := CTSh + 1;              
        shiftDelta := '1';
        continue;
      end
      else
      if (px[px.Length] = '0') and (px[px.Length - 1] = '1') then
      begin
        PZ := rightBin(Minus(PZ, appendNum(PY, 2 * n)), 2 * n);
        if (CTSh = n) then break;    
        px := ShiftRight(px, 2);
        pz := ShiftRight(pz, 2);
        pfi := '1';
        CTSh := CTSh + 2;         
        shiftDelta := '2';
        continue;
      end
      else
      begin
        if (CTSh = n) then break;    
        px := ShiftRight(px, 2);
        pz := ShiftRight(pz, 2);
        pfi := '1';
        CTSh := CTSh + 2;      
        shiftDelta := '2';
        continue;
      end;
    end
    else //fi = 0
    begin
      if (px[px.Length] = '0') and (px[px.Length - 1] = '0') then
      begin
        if (CTSh = n) then break;    
        px := ShiftRight(px, 2);
        pz := ShiftRight(pz, 2);
        pfi := '0';
        CTSh := CTSh + 2;       
        shiftDelta := '2';
        continue;
      end
      else
      if (px[px.Length] = '1') and (px[px.Length - 1] = '0') then
      begin
        PZ := rightBin(Sum(PZ, appendNum(PY, 2 * n)), 2 * n);
        if (CTSh = n) then break;    
        px := ShiftRight(px, 2);
        pz := ShiftRight(pz, 2);
        pfi := '0';
        CTSh := CTSh + 2;     
        shiftDelta := '2';
        continue;
      end
      else
      if (px[px.Length] = '0') and (px[px.Length - 1] = '1') then
      begin
        if (CTSh = n) then break;    
        px := ShiftRight(px, 1);
        pz := ShiftRight(pz, 1);
        pfi := '0';
        CTSh := CTSh + 1;    
        shiftDelta := '1';
        continue;
      end
      else
      begin
        PZ := rightBin(Minus(PZ, appendNum(PY, 2 * n)), 2 * n);
        if (CTSh = n) then break;    
        px := ShiftRight(px, 2);
        pz := ShiftRight(pz, 2);
        pfi := '1';
        CTSh := CTSh + 2;       
        shiftDelta := '2';
        continue;
      end;
    end;           
  end;    
  
end.