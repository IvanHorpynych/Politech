program var_6_1;

var
	s : string;
	i:integer;

begin
	readln(s);
	for i := 1 to length(s)-1 do begin
		if ((s[i] = ',') and (s[i+1] = '+')) or((s[i] = '+') and (s[i+1] = ',')) then inc(k);
	end;
	writeln('K = ',k);
readln;
end.
