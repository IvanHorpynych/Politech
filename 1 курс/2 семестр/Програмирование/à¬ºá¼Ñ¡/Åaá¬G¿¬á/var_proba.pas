Program zavd;
uses crt;
var
   f:file of char;
   ch:char;

begin
    assign(f,'100.dat');
    rewrite(f);
    repeat
      read(ch);
      write(f,ch);
    Until ch = #13;
    close(f);
    assign(f,'100.dat');
    
    reset(f);
    while not eof(f) do begin
      read(f,ch);
      write(ch);
    end;
    
    reset(f);
    
    write(f,'1');
    reset(f);
    while not eof(f) do begin
      read(f,ch);
      write(ch);
    end;
    
readln;
end.