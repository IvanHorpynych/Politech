program var_7_1;

var 
	s,s1:string;
	i,k,min,k1:integer;
	ch:char;
	begflag:boolean;
begin
	readln(s);
	min:=1;
	i:=0;
	begflag:=true;
	while i<>length(s) do begin
		repeat
		  inc(i);
		  if s[i] <> ' ' then s1:=s1+s[i];
			if (i <> length(s)) then
			  if (s[i] = s[i+1]) then inc(k);
			
		until (s[i] = ' ' ) or (i = length(s));
    if begflag then begin min:=length(s1); begflag:=false;end;
	if (length(s1)<=min) and (length(s1) > 0) then min:=length(s1);
	s1:='';
	end;
	writeln('min = ',min);
	writeln('k = ',k);
end.
