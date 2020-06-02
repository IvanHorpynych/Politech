program var_4_2;

var 
	f,g:text;
	s,s1:string;
	max:integer;
	begflag,fl:Boolean;
	l,k,i,j:integer;
begin
	assign(f,'4.txt');
	reset(f);
	
	while (not eof(F)) do
	begin
		readln(f,s);
		writeln(s);
	end;

	reset(f);
	writeln;
	begflag:=true;
	k:=0;
	i:=0;
	l:=0;
	assign(g,'5.txt');
	reset(g);
	repeat
	while not(eof(f)) do begin
		readln(f,s);
		inc(k);
		if begflag then begin
			begflag:=false;
			max:=Length(s);
		end;
		if (max <= length(s)) and (fl = true) then begin max:=length(s);i:=k end;
		writeln(i,' ',k);
	end;
	reset(f);
	for j:=1 to i do 
	readln(f,s);
	rewrite(g);
	fl:=true;
	while not eof(g) do begin
	  readln(g,s1);
	  if s1 = s then fl:=false;
	end;
	if fl = true then begin append(g);writeln(g,s);end;
	inc(l);
until l <= 3;
{	readln(s);
	while (not eof(f)) do
	begin
		readln(s1);
		if length()
	end;
} end.
