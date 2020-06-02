program main;
uses MENU,crt; {Vyklyk neobkhidnykh moduliv}
begin
repeat {Povernennya menyu na holovnu storinku, doky na holovniy storintsi menyu ne bude natysnenyy Esc}
  clrscr;
  Menu_1; {Vyklyk protsedury menyu}
  clrscr;
 until keypressed;
end.