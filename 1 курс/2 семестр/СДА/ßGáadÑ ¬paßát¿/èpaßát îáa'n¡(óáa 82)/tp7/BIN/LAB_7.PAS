program Lab_7;
uses crt, graph;
var gd,gm:integer;
    v:char;
procedure P_2D;
var i,k:integer;
begin
 ClearDevice;
 k:=0;
 for i:=1 to 1 do
  begin

   Ellipse(400-k,115,81,285,120,95);

   Line(432-k,208,432-k,150);
   Line(432-k,150-k,405-k,150-k);


   inc(k);
  end;
 OutTextXY(5,400,'Continue? y/n');
 v:=readkey;
 ClearDevice;
end;

procedure P_3D;
var i,k:integer;
begin
 ClearDevice;
 k:=0;
 for i:=1 to 20 do
  begin
   Ellipse(400-k,115+k,81,285,120,95);

   Line(432-k,208+k,432-k,150+k);
   Line(432-k,150+k,405-k,150+k);
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
  OutTextXY(250,100,'Christman. KV-31');
  OuttextXY(250,120,'Laboratorna robota 7');
  delay(3000);
  ClearDevice;
  repeat

 OutTextXY(5,60,'Pressed:');
 OutTextXY(5,70,'1 - 2D');
 OutTextXY(5,80,'2 - 3D');
 OutTextXY(5,100,'3 - Exit');
 v:=readkey;
  case v  of
  '1': P_2D;
  '2': P_3D;
   else break;
 end;
 until v='n';
 OutTextXY(5,150,'Press any key for exit');
 readln;
 CloseGraph;
end.