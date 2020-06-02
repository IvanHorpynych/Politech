program LABO22;
uses crt;
var
p,s:double;
i,n:integer;
begin
clrscr;
writeln ('Vvedite naturalnoe n:');
write ('n=');
readln (n);
	s:=0;
	p:=1;
	for i:=1 to n do
	begin
	  s:=s+(4/3*i-1);
		p:=p*(i+sqrt(i))/s;
	end;
	writeln (p:10:7);
	readln;
end.
