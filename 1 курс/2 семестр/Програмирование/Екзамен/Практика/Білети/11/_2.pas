program _2;

var f,g:text;

function Probel(s:string):boolean;
var i:word;
begin
 if s='' then begin
  Probel:=true;
  exit;
 end; 
 for i:=1 to length(s) do
  begin
   if s[i]<>' ' then 
    begin
     Probel:=false;
     exit;
    end;   
  end;
 Probel:=true; 
end;

procedure Action;
var s,s1,s2:string;
    i,len:byte;
begin
 assign(f,'f.txt');
 assign(g,'newfile.txt');
 reset(f);
 rewrite(g);
 while eof(f)=false do
  begin
   readln(f,s);
   s:=s+' ';
   s2:=#255;
   while Probel(s)=false do begin
   len:=length(s);
   for i:=1 to len do
    begin
     if s[i]<>' ' then s1:=s1+s[i]
      else
       begin
        if (s1<s2)and(s1<>'') then s2:=s1;
        s1:='';
       end; 
    end; 
   s2:=s2+' ' ;
   write(g,s2);
   delete(s2,length(s2),1);
   delete(s,pos(s2,s),length(s2)+1);  
   s2:=#255;
  end;
  writeln(g,''); 
 end;
 
close(f); close(g); 
end;

{procedure Change;
var s:string;
begin
rewrite(f);
reset(g);
while eof(g)=false do
 begin
  readln(g,s);
  writeln(f,s);
 end;
close(f);
close(g); 
end;}

begin
 Action;
 {Change;}
end.
    