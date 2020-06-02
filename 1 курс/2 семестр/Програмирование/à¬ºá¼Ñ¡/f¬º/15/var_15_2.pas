program Var_15_2;
uses crt;
const n = 10;

var 
	f:file of integer;
	i,j,k : integer;
	fl:Boolean;
	m:set of integer;
  a:array[1..3] of integer;
  
procedure prost(i:integer);
Var
	j,k:integer;
begin
	for j:=1 to i do begin
		fl:=true;
		for k:=2 to j do begin
			if (j mod k = 0) and (j <> k) then fl:=false;
		end;
		if fl then include(m,j);
	end;
	writeln;;
	writeln('Simple digits : ');
	writeln(m);
end;

begin
	assign(f,'15.dat');
	rewrite(f);
	randomize;
	for i:=1 to n do begin
		k:=random(1,100);
		write(f,k);
	end;
	reset(f);
	i:=0;
	writeln('Your File : ');
	while not eof(f) do begin
	  inc(i);
	  read(f,k);
	  if i<=3 then a[i]:=k;
	  write(k:4);
	end;
	prost(100);
	reset(f);
	k:=0;
	i:=0;
	while true do begin
	  
	  read(f,j);
	  if j in m then begin
	    
	    
	    seek(f,k);
	    write(f,j);
	    
	    inc(k);
	    seek(f,i);
	    write(f,a[k]);seek(f,i+1);
	  end;
	  if (k = 3) then break;
	  if eof(f) then begin  writeln('Dont have 3 simple digit!'); break; end;
	  inc(i);
	end;
	writeln('k = ',k);
	reset(f);
	writeln('New File : ');
	i:=0;
	while not eof(f) do begin
	  read(f,k);
	  write(k:4);
	  inc(i);
	  textcolor(white);
	  
	  
	end;
	close(f);
readln;
end.
