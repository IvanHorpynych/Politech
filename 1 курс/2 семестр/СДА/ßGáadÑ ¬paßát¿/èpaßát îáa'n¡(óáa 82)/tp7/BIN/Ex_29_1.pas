program Ex_29_1;
uses crt;
var i,dob,fact:integer;
    s:real;
begin
 S:=0; fact:=1; dob:=1;
 for i:=1 to 18 do
  begin
   if (i mod 3 <> 0 )
    then
      begin
       fact:=fact*(i+1);
       dob:=dob*i;
      end
    else 
     begin
      fact:=fact*(i+1);
      dob:=dob*i;
      S:=S+dob/fact;
      dob:=1;
     end; 
  end;
 writeln('S=',S);
end. 
      
