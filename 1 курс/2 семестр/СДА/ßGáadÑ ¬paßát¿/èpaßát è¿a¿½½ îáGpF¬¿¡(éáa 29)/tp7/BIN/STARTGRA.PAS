program Lab_7;
uses crt, graph;
var gd,gm,i:integer;
    v:char;
begin
 gd:=detect;
 InitGraph(gd,gm,'');
 SetColor(1);
 for i:=1 to 15 do
  begin
   SetBkColor(i);
   readkey;
  end;

 readkey;
end.