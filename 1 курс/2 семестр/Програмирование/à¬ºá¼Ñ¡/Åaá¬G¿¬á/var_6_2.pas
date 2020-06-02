program var_6_2;
uses crt;
var
	f : file of char;
	i,k : integer;
	ch,ch1 : char;
	m:set of char;
	s:string;
begin
    assign(f,'3.dat');
	rewrite(f);
	repeat
		read(ch);
		write(f,ch);
	until (ch = #13);
	reset(f);
	{while not eof(f) do begin
		read(f,ch);
		write(ch);
	end;}
	reset(f);
	k:=0;
	s:='';
	m:=[];
	ch1:='f';
	writeln('Your letter is : ',ch1);
	while (not eof(f)) do
	begin
		repeat
			read(f,ch);
			include(m,ch);
			s:=s+ch;
			
		until (ch = ' ') or eof(f);
		if ch1 in m then begin
			inc(k);
			writeln(s);
		end;
		s:='';
		m:=[];
	end;
	reset(f);
	writeln('k = ',k);
end.
