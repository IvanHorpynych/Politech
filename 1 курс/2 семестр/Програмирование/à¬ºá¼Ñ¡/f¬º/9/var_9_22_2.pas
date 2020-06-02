program var_9_22_2;

var
	f:file of char;
	ch:char;
  i,j,k:integer;
begin
	assign(f,'9.dat');
	rewrite(f);
	repeat
		read(ch);
		write(f,ch);
	until (ch = #13);

	close(f);
	assign(f,'9.dat');
	writeln;
	{reset(f);
	writeln;
	while (not eof(f)) do
	begin
		read(f,ch);
		write(ch);
	end;}
	reset(f);
	i:=0;
	while (not eof(F)) do
	begin
	  if i>=filesize(f) then break;
		inc(i);
		read(f,ch);
		if (ch = '#') and not(eof(f) )then begin
			k:=1; 
			read(f,ch);
			{inc(i);}
			while (ch = '#') and not eof(f) do
			begin
				inc(k);
				inc(i);
				read(f,ch);
			end;
			if i<>filesize(f) then begin
			j:=0;
			while not(eof(f)) do begin
				seek(f,i+j);
				read(f,ch);
				seek(f,i-(2*k)+j);
				write(f,ch);
				inc(j);
				seek(f,i+j);
			end;
			end;
		seek(f,filesize(f)-(2*k)+1);
		truncate(f);
		seek(f,i);
		end;
	end;
	reset(f);
writeln;
writeln('New : ');
	while not(eof(f)) do begin
		read(f,ch);
		write(ch);
	end;
writeln;
readln;
end.
