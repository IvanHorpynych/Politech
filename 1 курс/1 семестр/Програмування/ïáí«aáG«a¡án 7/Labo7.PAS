program labo7;
uses Graph, crt;
Const n=3;
	q_name: array [1..n] of string=('1. 2D',
 '2. 3D',
 '3. Exit');
Var i,j: byte;  gd,gm:integer;

 procedure frame (x1, y1, x2, y2:integer);
 var i:integer;
 begin
 gotoxy (x1, y1);
 write (#201);
 for i:=(x1+1) to (x2-1) do write (#205);
 write (#187);
 for i:=(y1+1) to (y2-1) do
  begin
  gotoxy (x1, i);
  write (#186);
  gotoxy (x2, i);
  write (#186);
  end;
  gotoxy (x1, y2);
  write (#200);
 for i:=(x1+1) to (x2-1) do write (#205);
 write (#188);
 end;

 procedure zastavka;
 var ch:char;
 begin
 clrscr;
 frame (10,5,70,18);
 gotoxy (25,6); write ('Laboratorna robota po programuvannyu');
 gotoxy (35,7); write ('#7');
 gotoxy (32, 8); write ('Variant 6');
 gotoxy (38,12); write('Vikonav: student 1 kursy');
 gotoxy (38,13); write('grupy KV-41');
 gotoxy (38,14); write('Gotpynych Ivan');
 gotoxy (35,17); write ('Kyiv 2014');
 ch:=readkey;
 clrscr;
 end;


procedure dvad;
begin
gd:=detect;
InitGraph(gd,gm,'');
moveto(319,99);
LineTo(300,99);
LineTo(290,120);
LineTo(275,120);
LineTo(290,50);
LineTo(335,50);
LineTo(335,120);
LineTo(319,120);
LineTo(319,99);
MoveTo(319,85);
LineTo(300,85);
LineTo(305,65);
LineTo(319,65);
LineTo(319,85);
readln;
CloseGraph;
end;
procedure trid;
begin
gd:=detect;
InitGraph(gd,gm,'');
moveto(319,99);
LineTo(300,99);
LineTo(290,120);
LineTo(275,120);
LineTo(290,50);
LineTo(335,50);
LineTo(335,120);
LineTo(319,120);
LineTo(319,99);
MoveTo(319,85);
LineTo(300,85);
LineTo(305,65);
LineTo(319,65);
LineTo(319,85);
MoveTo(300,85);
LineTo(308,80);
LineTo(308,65);
MoveTo(308,80);
LineTo(319,80);
MoveTo(290,120);
Lineto(302,115);
LineTo(308,99);
MoveTo(335,120);
LineTo(340,115);
LineTo(340,45);
LineTo(295,45);
LineTo(290,50);
MoveTo(335,50);
LineTo(340,45);
readln;
CloseGraph;
end;
begin
zastavka;
While true do
Begin
clrscr;
gotoxy (24,4);
Write ('Glavnoe menyu');
For j:=1 to 3 do
Begin gotoxy (25, 7+j);write (q_name[j]); end;
Gotoxy (20,15); write ('Vvedite nomer i najmite enter:');
Readln (i);
Case i of
1: dvad;
2: trid;
3: begin clrscr; Halt (1); end;
else begin
	gotoxy (20,17);write (' Neverniy nomer ');
delay (1000);
end;
end;
end;
end.

