unit common;
interface
 type Tg1=integer; Tg2='a'..'z'; Tg3=0..2; Tg4=4..6; Tg5=7..9;
 const Cg1=1; Cg2=3; Cg3='a';
 var Vg1:Tg1; Vg2:Tg2; Vg3:Tg3; Vg4:Tg4; f:text;
implementation
begin
assign(f, 'f.txt'); rewrite(f);
  writeln(f, 'Common started');
  vg1:=0; vg2:='x'; vg3:=5; vg4:=9; 
  writeln(f, 'Vg1=', vg1, '; Vg2=', vg2, '; Vg3=', vg3,'; Vg4=', vg4);
  writeln(f, 'Cg1=', cg1, '; Cg2=', cg2, '; Cg3=', cg3);
  writeln(f, 'Common finished');
  close(f);
end.
