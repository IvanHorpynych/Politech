program _1;

const Bukvy=['a'..'z','A'..'Z'];

procedure Action;
var Set1:Set of char;
    s:string;
    i:word;
    
begin
 Writeln('Enter string:');
 readln(s);
 i:=1;
 Set1:=[];
 while i<=length(s) do
  begin
   if s[i] in Bukvy then
    if s[i] in Set1 then delete(s,i,1)
     else
      begin
       Set1:=Set1+[s[i]];
       inc(i);
      end
    else inc(i);  
  end;
  
 Writeln('New string:',s);
 
end;   

begin
 Action;
end. 