program _2;

var f:text;
    ind,i:word;
    s:string;
    ch,ch1:char;
begin
 assign(f,'2.txt');
 reset(f);
 ind:=0;
 writeln('File F');
 while not eof(f) do begin
  read(f,ch);
  write(ch);
 end;
 reset(f);
 writeln;
 while not eof(F) do begin
  readln(f,s);
  if (length(s) = 1) and (s[1] <> ' ') then writeln('String : ',S,'Have 1 letter !!');
  for i:=1 to length(s) do begin
    if s[i]<>' ' then begin ch:=s[i]; break; end;
  end;
  for i:=length(s) downto 1 do begin
    if s[i]<>' ' then begin ch1:=s[i]; break;end;
  end;'
  if ch = ch1 then begin inc(ind); writeln(s); end;
 end;
 writeln(ind,' Strings have 1 letter!!');
end.