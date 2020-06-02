program Var_5_2;
const
	n = 5;
var 
	f : file of integer;
	nul,i,k,p,j:integer;
begin
	assign(f,'2.dat');
	rewrite(f);
	for i:=1 to n do begin
		read(p);
		write(f,p);
	end;
	reset(f);
	for i := 1 to n do begin
		read(f,p);
		write(p:4);
	end;
	reset(f);
	writeln;
	nul:=1;
	for i := 0 to n-2 do begin
		seek(f,i);
		read(f,p);
		if p < 0 then 
		for j := i+1 to n-1 do begin
			read(f,k);
			if k > 0 then begin
				seek(f,j);
				write(f,p);
				seek(f,i);
				write(f,k);
				break;
			end; 
		end;
		if p = 0 then begin
		  seek(f,n-nul);
		  read(f,k);
		  seek(f,n-nul);
		  write(f,p);
		  seek(f,i);
		  write(f,k);
		  inc(nul);
		end;
	end;
	reset(f);
	for i := 1 to n do begin
		read(f,p);
		write(p:4);
	end;
	close(f);
	writeln;
readln;
end.
