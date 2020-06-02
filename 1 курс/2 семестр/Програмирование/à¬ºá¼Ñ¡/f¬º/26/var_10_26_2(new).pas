Program DZ_1;
uses crt;
var f,f1:text;
    ch:char;
    i,j,k:integer;
    s:string;
begin
    assign(f,'11.txt');
    assign(f1,'12.txt');
    rewrite(f1);
    reset(f);
    while not eof(f) do
      begin
        readln(f,s);
        k:=0; 
        for i:=2 to length(s) do 
          begin    
            if (s[i-1] = ' ') and (s[i] <> ' ') then inc(k);
            if k > 1 then begin writeln(f1,s); break end;
          end;
      end;
    close(f);
    close(f1);
    reset(f1);
    while not eof(f1) do begin
      readln(f1,s);
      writeln(s);
    end;
    close(f1);
readln;
end.        