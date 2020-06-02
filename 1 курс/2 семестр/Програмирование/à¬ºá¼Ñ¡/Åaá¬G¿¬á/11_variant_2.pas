var
  s: string;
  i, j, r, x: integer;
  f: text;
  A: array[1..100] of string;
  B: array[1..100] of string;

begin
  assign(f, '3.txt');
  reset(f);
  x := 1;
  while not eof(f) do
  begin
    readln(f, s);
    j := 1;
    i := 1;
    while i <= length(s) do
    begin
    A[j] := '';
      while (i <= length(s)) and (s[i] <> ' ') do 
      begin
        A[j] := A[j] + s[i];
        inc(i);
      end;
      inc(j);
      inc(i);
    end;
    
    for R := j-1 downto 2 do   
      for i := 1 to R - 1 do        
        if A[i] > A[i + 1] then         
        begin
          s := A[i];            
          A[i] := A[i + 1];            
          A[i + 1] := s;         
        end;
    
    s := '';
    for i := 1 to j-1 do
      s := s+A[i]+' ';
    B[x] := s;
    writeln(s);
    inc(x);
  end;
  close(f);
  rewrite(f);
  for i := 1 to x do begin
      //writeln(B[i]);
      writeln(f,B[i]);
  end;
  close(f);
end.