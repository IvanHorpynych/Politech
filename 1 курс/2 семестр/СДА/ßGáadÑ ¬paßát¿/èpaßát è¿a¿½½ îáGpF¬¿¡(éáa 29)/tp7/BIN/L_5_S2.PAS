program Lab_7_Kirill;
uses crt, graph;
var gd, gm:integer;
    v:char;
procedure P_2D;
var i:integer;
begin
 ClearDevice;

 MoveTo(450,60);
 LineTo(450,400);
 Ellipse(270,200,180,270,30,30);
 Line(270,230,450,230);
 Line(240,200,240,60);

 readln;
end;
procedure P_3D;
var i:integer;
begin
 ClearDevice;
 for i:=0 to 10  do
 begin
 MoveTo(450-i,60+i);
 LineTo(450-i,400+i);
 Ellipse(270-i,200+i,180,270,30,30);
 Line(270-i,230+i,450-i,230+i);
 Line(240-i,200+i,240-i,60+i);
 end;
 readln;
end;




begin
 gd:=detect;
 InitGraph(gd, gm, '');
 SetColor(1);
 SetBkColor(15);
  repeat
 ClearDevice;
 OutTextXY(5,40,'Tremboveckiy K. KV-31');
 OutTextXY(5,60,'Pressed:');
 OutTextXY(5,70,'1 - 2D');
 OutTextXY(5,80,'2 - 3D');
 v:=readkey;
  case v  of
  '1': P_2D;
  '2': P_3D;
   else break;
 end;
 until false;
end.