unit M1;
interface 
procedure S1;
implementation
uses common, err;
procedure S4;
begin
writeln(f,'S4 started');
writeln(f,'S4 finished');
end;
  procedure S1;
  begin
  writeln(f,'S1 started');
  S4;
  writeln(f,'S1 finished');
  end;
begin 
append(f);
writeln (f,'M1 started');
writeln(f,'M1 finished');
close(f);
end.
  