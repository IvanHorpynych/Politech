program var1_1;
const 
	n = 3;
type
	matr = array[1..n,1..n] of integer;
var 
	a : matr;
	i, j, k : byte;
	d,dmax : integer;
begin

	for i := 1 to n do begin
		for j := 1 to n do begin
			read(a[i,j]);
		end;
	end;

	for i := 1 to n do begin
		for j := 1 to n do begin
			write(a[i,j]:4);
		end;
		writeln;
	end;

	dmax:=0;
	d:=1;
	for j := 1 to n do begin
		for i := 1 to n do begin
			d:=d*a[i,j];
		end;
		if dmax <= d then begin dmax:=d; k:=j; end;
		d:=1;
	end;
	writeln('Your row is : ');
		for i := 1 to n do begin
			writeln(a[i,k]);
		end;
	
readln;
end.
