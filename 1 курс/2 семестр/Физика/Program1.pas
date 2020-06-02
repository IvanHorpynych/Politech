program ABC;
uses crt;
var E1, E2, E3, y1,y2,y3:real;
i:word;
begin
for i:=1 to 4 do
begin
E1:=28248.6*(i*0.01);
y1:=84.4-(14124.3*(i*0.01)*(i*0.01));
writeln('Сантиметр ',i,':E1=',E1:8:2,';  y1=',y1:8:2);
end;
for i:=5 to 10 do
begin
E2:=70.62*(1/(0.01*i));
y2:=70.6*ln(1/(10*(i*0.01)));
writeln('Сантиметр ',i,':E2=',E2:8:2,';  y2=',y2:8:2);
end;
for i:=11 to 15 do
begin
E3:=36.72*(1/(i*0.01));
y3:=36.72*ln(1/(10*(i*0.01)));
writeln('Сантиметр ',i,':E3=',E3:8:2,';  y3=',y3:8:2);
end;
end.