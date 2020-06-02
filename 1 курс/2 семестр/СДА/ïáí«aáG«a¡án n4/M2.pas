unit M2;
interface
procedure S2;
implementation
uses common, err, M3;
procedure S5;
begin
writeln(f,'S5 started');
writeln(f,'S5 finished');
end;
procedure S2;
begin
writeln(f,'S2 started');
S5; S6;
writeln(f,'S2 finished');
end;
begin 
append(f);
writeln (f,'M2 started');
writeln(f,'M2 finished');
close(f);
end.