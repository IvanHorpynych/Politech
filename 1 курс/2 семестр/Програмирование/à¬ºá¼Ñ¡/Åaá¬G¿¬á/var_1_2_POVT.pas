program var1_2;
const 
	n = 10;
var 
	i, k,k1 : byte;
	dpoch, dkil,dmaxkil : integer;
	f:file of byte;

begin
	assign(f,'1.dat');
	rewrite(f);
	writeln('Enter file');
	for i:=1 to n do begin
		read(k);
		write(f,k);
	end;
	assign(f,'1.dat');
	reset(f);
	read(f,k);
	i:=0;
	dmaxkil:=0;
	while not eof(f) do begin
		read(f,k1);
		inc(i);
		if k1 > k then 
			begin 
			  if eof(f) then break;
				{dpoch:=i;}
				dkil:=1;
				k:=k1;
				inc(i);
				while true  do begin
					read(f,k1);
					
					if k1 > k then inc(dkil)
					else begin 
					        if dkil >= dmaxkil then
					          begin dmaxkil:=dkil;dpoch:=i-dkil-1;end; 
					        break;
					     end;
					if eof(f) then  begin 
					        if dkil >= dmaxkil then
					          begin dmaxkil:=dkil;dpoch:=i-dkil;end; 
					        break;
					     end;
					
					k:=k1;
					inc(i);
				end;
			end;
    
		k:=k1;
	end;

	reset(f);
	seek(f,dpoch);
	if dmaxkil = 0 then begin writeln('No!');exit;end;
	write('Your goal is : ');
	for i:=1 to dmaxkil+1 do
		begin
			read(f,k);
			write(k:4);
		end;
	close(f);
	writeln;
readln;
end.