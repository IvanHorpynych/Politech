program Var_10_2;

var 	
	f,g : text;
	i:integer;
	s : string;
	fl,boolafter,Booluntil:Boolean;
begin
	assign(f,'1.txt');
	assign(g,'2.txt');
	reset(f);
	rewrite(g);
	while (not eof(f)) do
	begin
		readln(f,s);
		booluntil:=false;
		boolafter:=false;
		fl:=false;
		for i:=1 to Length(s) do begin
			if s[i] <> ' ' then booluntil:=true;
			if (s[i] = ' ') and booluntil then fl:=true;
			if (s[i] <> ' ') and fl then boolafter:=true;
		end;
		if boolafter then writeln(g,s);
		s:='';
	end;
	close(g);
	assign(g,'2.txt');
	
	reset(f);
	writeln('File F : ');
	while (not eof(f)) do
	begin
		readln(f,S);
		writeln(s);
	end;
	
  reset(g);
	writeln('File G : ');
	while (not eof(g)) do
	begin
		readln(g,S);
		writeln(s);
	end;
	close(f);
	close(g);
readln;
end.
