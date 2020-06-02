program Exam_1;
uses crt;
var s:string;
function Action(var s:string):string;
var s_beg,s_en,b:string;
    ind_beg,ind_en,i,l:integer;
begin
 s_beg:='begin';
 s_en:='end';
 ind_beg:=0;
 ind_en:=0;
 s:=s+' ';
 l:=length(s);
 for i:=1 to l do
   if s[i]<>' ' then b:=b+s[i]
   else
    begin
     if b=s_beg then inc(ind_beg);
     if b=s_en then inc(ind_en);
     if ind_en>ind_beg then
      begin
       Action:='Ne virno';
       exit;
      end;
     b:='';
    end;
 if ind_beg=ind_en then Action:='Virno'
  else Action:='Ne virno';

end;
begin
 clrscr;
 Writeln('Enter text');
 Readln(s);
 Writeln(Action(s));
 readkey;
end.
