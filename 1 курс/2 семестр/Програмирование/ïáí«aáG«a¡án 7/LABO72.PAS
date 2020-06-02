program LABO72;
uses graph;
type
     base=object
         point:record
         x:integer;
         y:integer;
         end;
         constructor init(x,y:integer);

    end;

     second=object(base)
       pline:record
         xl:integer;
         yl:integer;
         end;
       constructor initl(x1,y1,x2,y2:integer);
       procedure draw;
       procedure formul;
       end;

  constructor base.init(x,y:integer);
  begin
   point.x:=x;
   point.y:=y;
  end;

   constructor second.initl(x1,y1,x2,y2:integer);
   begin
   init(x1,y1);
   pline.xl:=x2;
   pline.yl:=y2;
  end;


  procedure second.draw;
  begin
  line(point.x,point.y,pline.xl,pline.yl);
  end;

  procedure second.formul;
  var a,b:real;
  begin
  a:=(pline.yl-point.y)/(pline.xl-point.x);
  b:=(-(point.x*pline.yl)+pline.xl*point.y)/(pline.xl-point.x);
  write('Uravnenye: y=(',a:4:2,')x+(',b:4:2,')');
  end;

  procedure InitGr;
var
   gd, gm : integer;
   ga     : string;
begin
     gd:=detect;
     ga:='';
     InitGraph(gd,gm,ga);
end;

  var line:second;mypoint:base;x1,y1,x2,y2:integer;
  begin
  writeln('Vvedite x1');read(x1);
  writeln('Vvedite y1');read(y1);
  writeln('Vvedite x2');read(x2);
  writeln('Vvedite y2');read(y2);
  line.initl(x1,y1,x2,y2);
  mypoint.init(x1,y1);

   initgr;
   line.draw; readln;
   line.formul;
   readln;
   closegraph;
   end.
