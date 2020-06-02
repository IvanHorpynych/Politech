{Sumvol probilu na pochatku ryadka ta na kinci ya ne vraxovuvav, 
oskilki vvazhayu ce za nepravulnishuy xid}
program Chernysh_variant9_zavd2;
var 
    f : text;
    ind, i : word;
    s : string;
    ch, ch1 : char;
begin
 assign(f,'2.txt');
 reset(f);
 ind:=0;
 writeln('File F : ');
 while not eof(f) do begin
  read(f,ch);
  write(ch);
 end;
 reset(f);
 writeln;
 writeln;
 i:=0;
 while not eof(F) do begin
  readln(f,s);
  inc(i);
  if (length(s) = 1) then begin writeln('String ',i,' : ',S,' Have 1 letter,its have not equal letters in the end and in the begin !!'); writeln;end else
  if length(s) = 0 then begin writeln ('String ',i,' is empty!!'); writeln;end
  else begin
  for i:=1 to length(s) do begin
    if s[i] <> ' ' then begin ch:=s[i]; break; end;
  end;
  for i:=length(s) downto 1 do begin
    if s[i] <> ' ' then begin ch1:=s[i]; break;end;
  end;'
  if ch = ch1 then 
    begin
      inc(ind);
      writeln('This string have equal letters in the end and in the begin : ');
      writeln(s);
      writeln; 
    end;
 end;
 end;
 writeln(ind,' Count strings, which have equal letters in the end and in the begin!!');
 close(f);
end.