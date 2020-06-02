program Ex_29;
uses crt;
var i,j,dob,fact:integer;
    s:real;
begin
 S:=0; fact:=1; dob:=1; j:=1; i:=3;
 while i<=18 do
  begin
   dob:=1;
     while j<=i do
      begin
       fact:=fact*(j+1);
       dob:=dob*j;
       j:=j+1;
      end;
    S:=S+dob/fact;
    i:=i+3;
  end;     
 writeln('S=',S);
end. 
      
