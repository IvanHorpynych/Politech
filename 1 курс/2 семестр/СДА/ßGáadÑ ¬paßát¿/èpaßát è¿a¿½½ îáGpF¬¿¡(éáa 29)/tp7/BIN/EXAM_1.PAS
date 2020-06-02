program sjjsdh;
uses crt;
var s:string;
procedure Action(s:string);
var i,l,ind_1,ind_2:integer;
    s1,s2:char;
begin
 l:=length(s);
 s1:='(';
 s2:=')';
 ind_1:=0;
 ind_2:=0;
 for i:=1 to l do
  begin
   if s[i]=s1 then inc(ind_1);
   if s[i]=s2 then inc(ind_2);
   if ind_2>ind_1 then
    begin
     Writeln('False');
     readkey;
     exit;
    end
  end;
 for i:=1 to l-1 do
  if (s[i]=s1) and (s[i+1]=s2) then
   begin
    Writeln('False');
    readkey;
    exit;
   end;
 if ind_1=ind_2 then Writeln('True')
  else writeLn('False');
 readkey;
end;
begin
 clrscr;
 Writeln('Enter S');
 Readln(s);
 Action(s);
end.