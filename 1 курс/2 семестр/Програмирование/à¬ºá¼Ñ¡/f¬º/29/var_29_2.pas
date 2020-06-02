program var_29_2;

var 
	f,g:text;
	i:integer;
	s:string;
	k,l:integer;
	ch:char;
	m:set of char;
	fl:Boolean;
	
begin
	assign(f,'29.txt');
	reset(f);
	writeln('File F : ');
	while not eof(f) do begin
		readln(f,s);
		writeln(s);
	end;
	writeln;
	reset(f);
	i:=0;
	m:=[];
	assign(g,'29_g.txt');
	rewrite(g);
	while not eof(f) do begin
		readln(f,s);
		i:=0;
		while i<=length(S)-1 do begin
		inc(i);
			if (s[i] in m) and (s[i]<>' ') then begin  delete(s,i,1); dec(i);end
			else include(m,s[i]);
		end;
		m:=[];
		writeln(g,s);
	end;
	close(f);
	close(g);
	assign(g,'29_g.txt');
	reset(g);
	writeln('File G : ');
	while not eof(g) do begin
	  readln(g,s);
	  writeln(s);
	end;
readln;
end.
