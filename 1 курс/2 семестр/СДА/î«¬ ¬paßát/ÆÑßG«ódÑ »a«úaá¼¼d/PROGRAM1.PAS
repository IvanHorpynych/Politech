program zbs;

const
  p = 1;
  m = 20;
  n = 10;

type
  arr = array[1..p, 1..m, 1..n] of integer;

var
  A: arr;

procedure ShowArray(const a: arr);
var
  i, j, k: word;
begin
  for k := 1 to p do
  begin
    for i := 1 to m do
    begin
      for j := 1 to n do
        write(a[k, i, j]:6);
      writeln;
    end;
    writeln;
  end;
end;

procedure UnSortArray(var a: arr);
var
  i, j, k: word;
begin
  randomize;
  for k := 1 to p do
    for i := 1 to m do
      for j := 1 to n do
        a[k, i, j] := random(p * m * n);
end;

procedure Obh1713(var a: arr);
var
  i, j, k, Z, H, f, g, B: integer;
begin
  for k := 1 to p do
  begin
    for i := 1 to m do
      for j := 1 to n do 
      begin
        Z := i;H := j;
        if H <> 1 then 
        begin
          f := Z;g := H - 1; 
        end
        else
        begin
          f := Z - 1;g := n;
        end;
        
        while (H >= 1) and (f >= 1) and (A[k, Z, H] < A[k, f, g]) do
        begin
          B := A[k, Z, H];
          A[k, Z, H] := A[k, f, g];
          A[k, f, g] := B;
          
          dec(H);dec(g);
          if H = 1 then
          begin
            g := n;f := Z - 1; 
          end
          else 
          if H = 0 then
          begin
            H := n;Z := Z - 1;
          end;
        end;  
      end;
  end;
end;

begin
  UnSortArray(A);
  ShowArray(A);    
  ABC(A);  
  ShowArray(A);
end.