program Labo2;
uses DemoUnit;
var y:integer;
begin
assign(f,'f.dat'); 
assign(g,'g.dat');
write('Vvedite kolichestvo chisel:');
repeat
read(y);
if y<=0 then begin 
    write ('Vi vveli nepravilnoe kolichestvo. Vvedite novoe');
    writeln; end;
until y>0;
vvod(f,y);
writeln('Dannye fayla "F":');
vivod(f);
sort(f,g);
writeln('Otsortirovannie dannye fayla "f":');
vivod(g); 
close(f);
close(g);
readln;
end.				
