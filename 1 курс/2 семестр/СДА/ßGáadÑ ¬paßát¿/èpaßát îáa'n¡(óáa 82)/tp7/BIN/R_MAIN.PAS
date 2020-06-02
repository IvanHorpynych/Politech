program R_MAIN;
uses R_Menu,crt;
var ch:char;
begin
 repeat
  clrscr;
  Menu_1;
  clrscr;
 until keypressed;
end.