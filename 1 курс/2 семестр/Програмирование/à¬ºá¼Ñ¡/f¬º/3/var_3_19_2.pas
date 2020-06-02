program var2_2;

var 
	f:file of char;
	s:string;
	i:integer;
	ch:char;
	fl:boolean;
begin
	assign(f,'1.dat');
	rewrite(f);
	writeln('Enter file');
	repeat
		read(ch);
		write(f,ch);
	until ch = #13;
  close(f);
  assign(f,'1.dat');
	reset(f);
	while not eof(f) do begin
		repeat
		  read(f,ch);
			s:=s+ch;
		until (ch = ' ') or eof(f); 
		fl:=true;
		for i:=1 to (length(s)-1) div 2 do begin
		  if s[i] <> s[(length(s)-1)-i+1] then fl:=false;
		end;
		if fl then writeln(s);
		s:='';
	end;
readln;
end.
