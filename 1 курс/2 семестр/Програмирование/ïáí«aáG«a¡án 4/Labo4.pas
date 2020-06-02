program Labo4;
var x,y: char;  
    f,g,h:text;
    T:boolean;
begin 
assign(f,'f.txt');  assign(g,'g.txt');   assign(h,'h.txt');
reset(f); reset(g); rewrite(h);
if eof(f) then write('Fail "F" pustoy!')
            else
            if eof(g) then write('Fail "G" pustoy!')
                else
T:=false;  
writeln('Opareciya vypolnena uspechno!');                  
while (T=false) and not(eof(f)) and not(eof(g)) do 
begin
  read(f,x);  read(g,y);
  if x=y then write(h,x)
         else T:=true;
end;
close(f);    close(g);     close(h); 
readln;
end. 