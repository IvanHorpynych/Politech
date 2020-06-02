program main;
uses MENU,crt; {Виклик необхідних модулів}
begin
repeat {Повернення меню на головну сторінку, доки на головній сторінці меню не буде на тиснений Esc}
  clrscr;
  Menu_1; {Виклик процедури меню}
  clrscr;
 until keypressed;
end.