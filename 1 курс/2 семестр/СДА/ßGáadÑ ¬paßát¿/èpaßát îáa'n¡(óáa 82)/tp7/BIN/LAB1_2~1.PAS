program Lab_1_2;
uses crt;
var x,y:real;
begin
clrscr;
write('x=');
readln(x);
if (x<-49)or(x>=-10)and(x<=0)or(x>10)and(x<=20)
  then
    writeln('rishennya nemaye')
  else
    if(x<-10)
      then
        begin
             y:=10*(-exp(ln(abs(x))*3))+(7*x)/5+2;
             writeln('y=',y:5:2);
        end
      else
         if(x>0)and(x<=10)
          then
             begin
              y:=10*exp(ln(x)*3)+(7*x)/5+2;
               writeln('y=',y:5:2);
             end
         else
            begin
             y:=-x+9;
             writeln('y=',y:5:2);
            end;
readln;
end.