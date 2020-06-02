Program LABO21;
uses crt;
var
i,j,n:integer;
p,s:double;
begin
clrscr;
writeln ('Vvedite naturalnoe n:');
write ('n=');
readln (n);
p:=1;
for i:=1 to n do
	begin
		s:=0;
		for j:=1 to i do
			s:=s+(4/3)*j-1;
			p:=p*(i+sqrt(i))/s;
	end;
writeln (p:10:7);
readln;
end.
