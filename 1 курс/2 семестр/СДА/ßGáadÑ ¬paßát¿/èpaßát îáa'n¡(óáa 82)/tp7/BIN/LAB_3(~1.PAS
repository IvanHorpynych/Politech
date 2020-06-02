program Lab_3;
uses crt;
const n=20;
var Y,Z:array[1..n] of real;
    a,b,R:real;
    i:integer;
begin
 clrscr;
 a:=2.0;
 b:=3.0;
 randomize;
 Writeln('Array Y: ');
 for i:=1 to n do
  begin
   Y[i]:=random(40);
   Write(Y[i]:3:3,' ');
  end;
  
 Writeln; 
 Writeln('Array Z: '); 
 for i:=1 to n do
  begin
   if (Y[i]>=11) and (Y[i]<=33)
    then 
     Z[i]:=Y[i]-10*a
    else
     Z[i]:=Y[i]+5*b;
   Write(Z[i]:3:3,' ');
  end;
 
 for i:=1 to n do            
  begin
   if odd(i)=true
    then
     Z[i]:=-a*Z[i]
    else                          
     Z[i]:=a*Z[i];
  end;
  
 R:=Z[1];  
 for i:=2 to n do
   if Z[i]<R then R:=Z[i];
 Writeln;  
 Writeln('R=',R:3:3);
 readln;  
end. 
    