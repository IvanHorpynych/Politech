program var_8_2;

var 
	f : file of char;
  k,k1,max:integer;
	ch:char;
	BegFlag:Boolean;

begin
	writeln('Enter text : ');
	assign(f,'8.dat');
	rewrite(f);
	repeat
		read(ch);
		write(f,ch);
	until (ch = #13);
	reset(f);

	k:=0;
	k1:=0;
	BegFlag:=true;
	while not eof(f) do begin
		repeat
			read(f,ch);
			if ch <> ' ' then inc(k);	
	  until (ch = ' ') or eof(f);
	  if eof(f) then k:=k-1;
		if Begflag then begin max:=k; BegFlag:=false; end;
		if k = max then inc(k1)  else
		  if k > max then begin max:=k; k1:=1; end;
		k:=0;
		writeln('max = ',max);
	end;
	writeln('Count words with max length are : ',k1);
readln;
end.
