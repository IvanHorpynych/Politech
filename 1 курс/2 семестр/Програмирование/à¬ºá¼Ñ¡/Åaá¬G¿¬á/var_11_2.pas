program var_11_2;

var 
	f : text;
	ch:char;
	s,s1,s2,s3:string;
	i,k,k1,p,j:integer;

begin
	assign(f,'3.txt');
	reset(f);
	i:=0;
	k:=0;
	k1:=0;
	while not eof(f) do begin
		readln(f,s);
		writeln(s);
		while i<>length(s) do begin
			repeat
			  inc(i);
				
				if s[i] <> ' ' then begin   s1:=s1+s[i];
				inc(k);end;
			until (s[i] = ' ') or (i = length(s));
			
			
			repeat
			  inc(i);
				
				if s[i] <> ' ' then begin s2:=s2+s[i];
				inc(k1);end;
			until (s[i] = ' ') or (i = length(s));
		
			if s2 < s1 then 
			begin
			  
				j:=1;
				for p:=i-k1-k-1 to i-k-2 do begin
					s[p]:=s2[j];
					inc(j);
					if p = i-k-2 then begin s[p+1]:=' ';end;
					{if j <> length(s2) then continue;}
				end; 
				j:=1;
				for p:=i-k to i-1 do begin
					s[p]:=s1[j];
					inc(j);
					{if (p = i-1) and (p<>length(s)) then begin s[p+1]:=' ';end;
			{if j = length(s1) then break;}
				end;
				
				
				
			end;
			if i <> length(s) then if s1>=s2 then i:=i-k-1
			                        else i:=i-k1-1;
			writeln(s);
			s1:='';
			s2:='';
			k:=0;
			k1:=0;
		end;
		i:=0;
		k1:=0;
		k:=0;
		end;
end.
