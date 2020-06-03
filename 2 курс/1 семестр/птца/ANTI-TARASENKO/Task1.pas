uses
  System;

type
  line = record
    val: integer;
    inBin: string;
  end;

var
  text: string := '4221';  
  arr: array of line;

function rightBin(Val: string; l: integer): string;
var
  str: string;
begin
  for i: integer := 1 to l - Val.Length do
    Str += '0';
  Str += Val;
  result := Str;
end;

function myConvert(base, what: string): integer;
var
  res: integer := 0;
begin
  for i: integer := 1 to what.Length do
    if (what[i] = '1') then
      res += StrToInt(base[i]);
  
  Result := res;
end;

procedure mySort;
var
  tmp: line;
  i, R: integer;
begin
  
  for R := arr.Length - 1 downto 2 do
    for i := 1 to R - 1 do
      if arr[i].val > arr[i + 1].val then
      begin
        tmp := arr[i];
        arr[i] := arr[i + 1];
        arr[i + 1] := tmp;
      end;
end;

procedure outArr;
var
  pre_val: integer := -1;
begin
  writeln('':3, text:(text.Length + 1));
  for i: integer := 0 to arr.Length - 1 do
    if (arr[i].val = pre_val) then
      writeln('':3, arr[i].inBin:(text.Length + 1))
    else begin
      writeln(arr[i].val:3, arr[i].inBin:(text.Length + 1));
      pre_val := arr[i].val;
    end;
end;

begin
  write('Your "DDK" :');
  readln(text);
  var tmp_str: string;
  for i: integer := 0 to Round(Power(2, text.Length)) - 1 do
  begin
    SetLength(arr, i + 1);
    tmp_str := rightBin(System.Convert.ToString(i, 2), text.Length);
    arr[i].inBin := tmp_str;
    arr[i].val   := myConvert(text, tmp_str);
  end;
  mySort;
  outArr;
end.