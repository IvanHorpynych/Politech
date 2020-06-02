Unit RBody;

Interface

const n=6;

type TColor=(white,red,green,yellow,notcolor);

procedure StartComponent;
procedure ClearCircle(ind:word);
procedure PaintCircle(ind:word;Col:TColor);
procedure ClearEllipse(ind:word);
procedure PaintEllipse(ind:word;Col:TColor);
procedure GameOver;
procedure PaintRect(col:byte;ind:byte);
procedure MainMenu;
procedure Zastavka;
procedure Help;
Implementation
uses graph;

const X_beg=52;
      Y_beg=230;
      Interv=90;
      Rad=40;

procedure StartComponent;
var i:byte;
begin
 ClearDevice;
 for i:=0 to n do
  begin
   if i<3 then
    begin
     SetColor(15);
     SetFillStyle(1,15);
    end
   else if i=3 then
    begin
     SetColor(15);
     SetFillStyle(1,9);
    end
   else
    begin
     SetColor(4);
     SetFillStyle(1,4);
    end;
   FillEllipse(X_beg+i*Interv,Y_beg,Rad,Rad);
  end;
 SetColor(14);
 for i:=1 to 3 do Circle(X_beg+3*Interv,Y_beg,Rad+i);

 SetTextStyle(0,0,2);
 SetColor(15);
 OutTextXY(30,350,'Use:');
 Rectangle(180,330,250,400);
 Line(185,365,245,365);
 MoveTo(185,365);
 LineTo(205,385);
 MoveTo(185,365);
 LineTo(205,345);
 Rectangle(280,330,350,400);
 SetTextStyle(0,0,1);
 OutTextXY(293,360,'Enter');
 Rectangle(380,330,450,400);
 Line(385,365,445,365);
 MoveTo(445,365);
 LineTo(425,345);
 MoveTo(445,365);
 LineTo(425,385);
 SetTextStyle(0,0,2);
 OutTextXY(193,425,'Press');
 OutTextXY(293,425,'ESC');
 OutTextXY(370,425,'for back');
 SetTextStyle(0,0,3);
 OutTextXY(190,80,'Seven holes')
end;

procedure PaintCircle;
var i:word;
begin
 if Col=green then
   begin
    SetColor(10);
    for i:=1 to 3 do Circle(X_beg+ind*Interv,Y_beg,Rad+i)
   end
  else
    begin
    SetColor(14);
    for i:=1 to 3 do Circle(X_beg+ind*Interv,Y_beg,Rad+i)
   end;
end;

procedure ClearCircle;
var i:word;
begin
 SetColor(9);
 for i:=1 to 3 do Circle(X_beg+ind*Interv,Y_beg,Rad+i);
end;

procedure ClearEllipse;
begin
 SetColor(15);
 SetFillStyle(1,9);
 FillEllipse(X_beg+ind*Interv,Y_beg,Rad,Rad);
end;

procedure PaintEllipse;
begin
 if Col=red then
  begin
   SetColor(4);
   SetFillStyle(1,4);
   FillEllipse(X_beg+ind*Interv,Y_beg,Rad,Rad);
  end
 else
  begin
   SetColor(15);
   SetFillStyle(1,15);
   FillEllipse(X_beg+ind*Interv,Y_beg,Rad,Rad);
  end;
end;

procedure GameOver;
begin
 ClearDevice;
 SetTextStyle(0,0,3);
 SetLineStyle(0,0,3);
 SetColor(15);
 OutTextXY(220,150,'You Win');
 SetTextStyle(0,0,2);
 OutTextXY(220,250,'Main Menu');
 OutTextXY(220,300,'Exit');
 SetColor(5);
 Rectangle(210,230,385,280);
end;

procedure PaintRect;
begin
 SetColor(col);
 Rectangle(210,230+ind*50,385,280+ind*50);
end;

procedure MainMenu;
begin
 ClearDevice;
 SetTextStyle(0,0,3);
 SetLineStyle(0,0,3);
 SetColor(15);
 OutTextXY(220,150,'Main Menu');
 SetTextStyle(0,0,2);
 OutTextXY(220,250,'Start game');
 OutTextXY(220,300,'Help');
 OutTextXY(220,350,'Exit');
 SetColor(5);
 Rectangle(210,230,385,280);
end;

procedure Zastavka;
var gd,gm:integer;
begin
 gd:=detect;
 InitGraph(gd,gm,'');
 SetBkColor(9);
 SetColor(4);
 ClearDevice;
 SetTextStyle(0,0,6);
 OutTextXY(50,200,'Seven Holes');
end;

procedure Help;
var f:text;
    s:string;
    ind:integer;

begin
 ClearDevice;
 SetColor(15);
 SetTextStyle(0,0,2);
 ind:=0;
 assign(f,'f.txt');
 reset(f);
 while eof(f)=false do
  begin
   readln(f,s);
   OutTextXY(20,20+ind,s);
   ind:=ind+20;
  end;
 OutTextXY(100,20+ind+100,'Press any key for back');
 Readln;
end;

end.