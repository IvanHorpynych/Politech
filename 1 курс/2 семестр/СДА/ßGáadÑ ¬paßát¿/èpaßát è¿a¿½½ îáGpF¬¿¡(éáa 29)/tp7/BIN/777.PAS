program laba7;
uses crt,graph;

const
     norm=$17;
     sel=$70;
     p=3;
var gd,gm,i,j,n,x,y:integer;

 menu: array[1..p] of string[10];
   punkt: integer;
   ch: char;

   procedure punkt1;
   begin
   clrscr;
   n:=1;
   ch:=chr(27);
   end;

   procedure punkt2;
   begin
   clrscr;
   n:=10;
   ch:=chr(27);
   end;

   procedure punkt3;
   begin
   halt(1);
   end;

   procedure menutoscr;
   var j: integer;
   begin
        clrscr;
        for j:=1 to p do
        begin
             gotoxy(x,y+j-1);
             write(menu[j]);
        end;
        textattr:=sel;
        gotoxy(x,y+punkt-1);
        write(menu[punkt]);
        textattr:=norm;
   end;


procedure Rock;
begin
 for i:=1 to n do
  begin
   Setcolor(15-i+1);
   line(330+i,400+i,350+i,400+i);
   bar(200+i,100+i,250+i,400+i);
   arc(250+i,200+i,270,90,80);
   arc(250+i,200+i,270,90,100);
   arc(250+i,400+i,0,90,100);
   floodfill(340+i,380+i,getmaxcolor );
   floodfill(251+i,101+i,getmaxcolor);
   delay (2000);
  end;
 readln;
end;

begin
clrscr;
gotoxy(12,8);
textcolor(15);
writeln('                 Laboratorna 7                 ');
gotoxy (12,10);
writeln('                 Student: Chukhlib Y.V.           ');
gotoxy(12,12);
writeln('                 Group KV-31                       ');
gotoxy(12,14);
writeln('                 Task number 17                             ');

readkey;
clrscr;
menu[1]:=' 2D';
menu[2]:=' 3D';
menu[3]:=' Exit';
repeat
punkt:=1;
x:=5;
y:=5;
textattr:=norm;
menutoscr;
repeat
 ch:=ReadKey;
          if ch=char(0) then begin
               ch:=readkey;
               case ch of
                    chr(80):
                         if punkt<p then begin
                              gotoxy(x,y+punkt-1);
                              write(menu[punkt]);
                              punkt:=punkt+1;
                              textattr:=SEl;
                              gotoxy(x,y+punkt-1);
                              write(menu[punkt]);
                              textattr:=NORM;
                         end;
                    chr(72):
                         if punkt>1 then begin
                              gotoxy(x,y+punkt-1);
                              write(menu[punkt]);
                              punkt:=punkt-1;
                              textattr:=SEl;
                              gotoxy(x,y+punkt-1);
                              write(menu[punkt]);
                              textattr:=NORM;
                         end;
                 end;
              end
          else
               if ch=chr(13) then begin
                    case punkt of
                         1:punkt1;
                         2:punkt2;
                         3:halt(1);
                    end;
                    MenuToScr;
               end;
     until ch=chr(27);{Esc }




 gd:=detect;
 InitGraph(gd,gm,'');



 Rock;


 readln;
 closegraph;
 until punkt=3;

end.
