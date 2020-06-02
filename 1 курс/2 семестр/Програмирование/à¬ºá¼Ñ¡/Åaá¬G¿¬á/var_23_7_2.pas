program Var_7_2;
uses crt;

var 
	f : file of char;
	p,i,k,j:integer;
	ch,ch1:char;
begin
	assign(f,'4.dat');
	rewrite(f);
	repeat
		read(ch);
		write(f,ch);
	until (ch = #13);
	reset(f);
	writeln;
	while not eof(f) do begin
		read(f,ch);
		write(ch);
	end;
	reset(f);
	k:=0;
	i:=0;
	j:=0;
	while not eof(f) do begin
		repeat
			read(f,ch);
			inc(k);
			inc(j);
		until (ch = ' ') or eof(f);
		if k mod 2 = 0 then begin
			for i:=1  to k do begin
				seek(f,j-k+i-1);
				read(f,ch1);
				p:=0;
				while not eof(f) do begin
					inc(p);
					read(f,ch);
					seek(f,j-k+p-1);
					write(f,ch);
					seek(f,j-k+p);
				end;
				seek(f,eof(f)-1);
				write(f,ch1);
			end;
			end;
	end;
end.
