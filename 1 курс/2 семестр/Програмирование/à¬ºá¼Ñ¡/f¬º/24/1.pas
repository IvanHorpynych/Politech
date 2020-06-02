program _24;

var s:string;
    ind_d:word;

procedure Inp(var s:string);
begin
 Writeln('Enter text');
 Readln(s);
end;

procedure SearchWord(s:string);
var i:word;
begin
 ind_d:=0;
 if s[1]='n' then inc(ind_d);
 for i:=2 to length(s) do
   if (s[i]='n')and(s[i-1]=' ') then inc(ind_d); 
end;

begin
 Inp(s);
 SearchWord(s);
 if ind_d=0 then Writeln('Word not found') 
  else Writeln('Word with n: ',ind_d);
end.