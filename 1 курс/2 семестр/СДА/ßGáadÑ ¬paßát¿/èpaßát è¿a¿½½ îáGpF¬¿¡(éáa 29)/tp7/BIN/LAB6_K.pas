uses crt;

var
s:string;
i,n:integer;
c:char;

procedure Change (var s:string; l:byte);
begin
delete(s,l,1);
insert('-',s,l);
end;

begin
clrscr;
readln(s);
if pos('+',s)>0 then
  for c:='0' to '9' do
    begin
    n:=pos('+',s)+1;
    while n<=length(s) do
      begin
      if s[n]=c then Change(s,n);
      inc(n)
      end;
    end;
writeln(s);
readkey;
end.