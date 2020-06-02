program _2;

var f:text;
    SMax:string;
    ind:word;
    
procedure SearchSMax(s:string);
var
    i:word;
    str:string;
begin
 for i:=1 to length(s) do
   if s[i]<>' ' then str:=str+s[i]
    else
     begin
      if length(str)>length(SMax) then SMax:=str;
      str:='';
     end;
 if length(str)>length(SMax) then SMax:=str;   
end;

procedure NumberMaxWord(s:string);
var i:word;
    str:string;
begin
 for i:=1 to length(s) do
   if s[i]<>' ' then str:=str+s[i]
    else
     begin
      if str=SMax then inc(ind);
      str:='';
     end;
 if str=SMax then inc(ind);    
end;

procedure Action;
var s:string;
begin
 assign(f,'2.txt');
 reset(f);
 SMax:='';
 while eof(f)=false do
  begin
   readln(f,s);
   SearchSMax(s);
  end;
 reset(f);
 ind:=0;
 while eof(f)=false do
  begin
   readln(f,s);
   NumberMaxWord(s);
  end;
Writeln('Text have ',ind,' ',SMax);   
end;

begin
 Action;
end.