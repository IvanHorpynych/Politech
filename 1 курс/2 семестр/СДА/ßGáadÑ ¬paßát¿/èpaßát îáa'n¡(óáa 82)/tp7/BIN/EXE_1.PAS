program Lab_7;
uses crt, graph;

var gd,gm:integer;
    v:char;
procedure P_2D;
var i,k,x,y:integer;
begin
 ClearDevice;
 x:=300;
 y:=50;
 k:=20;
 SetColor(1);
for i:= 1 to 10 do
begin
 Pieslice(x,y,0,360,20);
 y:=y+10;
 delay(0);
 ClearDevice;
end;
readkey;

end;

procedure P_3D;
var i,k:integer;
begin
 ClearDevice;
 k:=0;
 for i:=1 to 25 do
  begin
   Line(500-k,20+k,500-k,400+k);
   Ellipse(500-k,115+k,90,270,150,95);
   MoveTo(500-k,210+k);
   LineTo(350-k,400+k);
   inc(k);
  end;

 OutTextXY(5,400,'Continue? y/n');
 v:=readkey;
 ClearDevice;
end;
begin
 gd:=detect;
 InitGraph(gd,gm,'');
 SetBkColor(15);
 SetColor(8);
 repeat
 OutTextXY(5,40,'Anastasiev D. KV-31');
 OutTextXY(5,60,'Pressed:');
 OutTextXY(5,70,'1 - 2D');
 OutTextXY(5,80,'2 - 3D');
 v:=readkey;
  case v  of
  '1': P_2D;
  '2': P_3D;
   else break;
 end;
 until v='n';



 OutTextXY(5,100,'Press any key for exit');
 readln;
 CloseGraph;
end.