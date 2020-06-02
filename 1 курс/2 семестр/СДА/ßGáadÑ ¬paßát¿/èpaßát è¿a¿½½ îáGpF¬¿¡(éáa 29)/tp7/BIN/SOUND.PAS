program sound_1;
uses crt;
var x:real;
begin
 x:=1;
 repeat
  sound(trunc(1000*sin(x)/cos(x)));
  delay(100);
  sound(trunc(1000*cos(x)));
  delay(100);
  x:=x+0.1;
 until keypressed;
 nosound;

end.