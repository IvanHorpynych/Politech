unit M3;
interface
procedure S3;
procedure S6;
implementation
uses common, err, M2;
procedure S7;
begin
writeln(f,'S7 started');
writeln(f,'S7 finished');
end;
    procedure S8;
    begin
    writeln(f,'S8 started');
    writeln(f,'S8 finished');
    end;
procedure S6;
begin
writeln(f,'S6 started');
inc(vg3);
if (vg3<=vg4) then S2;
writeln(f,'S6 finished');
end;
    procedure S3;
    begin
    writeln(f,'S3 started');
    vg3:=6;
    S6; S7; S8;
    writeln(f,'S3 finished');
    end;
begin 
append(f);
writeln (f,'M3 started');
writeln(f,'M3 finished');
close(f);
end.