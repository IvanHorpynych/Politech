program Chernysh_variant9_zavd1;
uses 
  crt;
var 
  f:file of char;
  s:string;
  i:integer;
  ch:char;

procedure Action;
var  i:word;
begin
 i:=1;
 writeln('Enter S : ');
 readln(s);
 clrscr;
 writeln('Your string is :',s);
 while i<=length(s) do 
   if s[i]='#' then
    if i=1 then delete(s,1,1)
     else begin
      delete(s,i-1,2);
      dec(i);
     end 
   else inc(i); 
 writeln('New string is : ',s);
end;

begin
  action;
readln;
end.

