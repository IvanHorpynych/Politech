program Lab_6;

uses crt, graph;

procedure Action;

const Num=10;

var gd,gm,i:integer;
    X_beg,Y_beg,X_end,Y_end,Mash:integer;
    x_max,x_min,y_max,y_min,dX:real;
    v:char;
    X,Y:array [1..num] of real;
    k:byte;
    s:string;
begin
 gd:=detect;
 InitGraph(gd,gm,'');
 SetColor(15);
 SetBkColor(0);
 X_beg:=50;
 Y_beg:=30;
 X_end:=500;
 Y_end:=440;

 repeat
  ClearDevice;
  Rectangle(X_beg,Y_beg,X_end,Y_end);
  OutTextXY(X_end+10,36,'Enter X min:');
  gotoxy(77,3);
  Readln(x_min);
  OutTextXY(X_end+10,53,'Enter X max:');
  gotoxy(77,4);
  Readln(x_max);
 until (x_min<=x_max)and((x_min>=0)and(x_max<=5));

 Mash:=9;
 SetColor(15);
 Line(60,235,490,235);
 Line(275,60,275,430);
 MoveTo(275,60);
 LineTo(270,65);
 MoveTo(275,60);
 LineTo(280,65);
 MoveTo(490,235);
 LineTo(485,230);
 MoveTo(490,235);
 LineTo(485,240);


 k:=Mash;
 for i:=1 to 18 do
  begin
   Line(272,235-k,278,235-k);
   if i<=9 then
    begin
     str(i,s);
     OutTextXY(260,231-k,s);
    end;
   k:=k+Mash;
  end;
 k:=Mash;
 for i:=1 to 22 do
  begin
   Line(275+k,232,275+k,238);
   if i<=9 then
    begin
     str(i,s);
     OutTextXY(273+k,242,s);
    end;
   k:=k+Mash;
  end;



 dX:=(x_max-x_min)/(num-1);
 X[1]:=x_min;
 Y[1]:=2*sqrt(x[1]*x[1]*x[1]);
 for i:=2 to num do
  begin
   X[i]:=X[i-1]+dX;
   Y[i]:=2*sqrt(x[i]*x[i]*x[i]);
  end;
 y_min:=Y[1]; y_max:=Y[1];
 for i:=2 to num do
  begin
   if Y[i]<y_min then y_min:=Y[i];
   if Y[i]>y_max then y_max:=Y[i];
  end;



 SetColor(4);
 MoveTo(275+round(x[1]*Mash),235-round(y[1]*Mash));
 PutPixel(275+round(x[i]*Mash),235-round(y[i]*Mash),4);
 for i:=2 to num do
  LineTo(275+round(x[i]*Mash),235-round(y[i]*Mash));

 OutTextXY(X_end+10,100,'y=2*X^3/2');
 SetColor(15);
 OutTextXY(X_end+10,150,'Made by');
 OutTextXY(X_end+10,170,'Anastasiev D.');
 OutTextXY(200,450,'Press any key for exit');


 readkey;
end;

Procedure Menu;
var i:integer;
    ch:char;
begin
 clrscr;
 for i:=1 to 10 do Writeln;
 Writeln('Lab#6':40);
 Writeln;
 Writeln('Construction graph of the function':63);
 Writeln;
 Writeln('Student group KV-31':48);
 Writeln;
 Writeln('Anastasiev D.':42);
 readkey;
 clrscr;
 Writeln('Pressed:');
 Writeln('  1 - Lab 6');
 Writeln('  2 - Exit');
 repeat
 ch:=readkey;
 case ch of
  '1': Action;
  '2': Exit;
  end;
  until (ch='1')or(ch='2');
end;


begin
 Menu;
end.