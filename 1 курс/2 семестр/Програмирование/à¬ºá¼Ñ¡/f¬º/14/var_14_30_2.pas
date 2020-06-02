program Var_14_2;

var 	
	f : text;
	ch:char;
	s,s1,splus:string[255];
	fl:Boolean;
	i,j:integer;
	
begin
	assign(f,'4.txt');
	reset(f);
	while not(eof(f)) do begin
		readln(f,s);
		writeln(s);
	end;
	s1:='';
	writeln('Enter your string : ');
	readln(splus);
	reset(f);
	while (not eof(f)) do
	begin
		readln(f,s);
		fl:=false;
		for i := 1 to length(s) do begin
			for j := i to length(s) do begin
				s1:=s1+s[j];
				if s1 = splus then begin fl:=true; break; end;
				
			end;
			if fl then begin writeln('String : ', s, ' Have string : ', splus); break; end;
			s1:='';
		end;
	end;
end.
