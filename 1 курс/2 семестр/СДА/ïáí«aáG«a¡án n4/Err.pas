unit err;
interface
  procedure error;
implementation
  uses common;
  procedure error;
  begin
    writeln(f, 'Error!');
  end;
begin
  append(f);
  writeln(f, 'Err started');
  writeln(f, 'Err finished');
  close(f);
end.
