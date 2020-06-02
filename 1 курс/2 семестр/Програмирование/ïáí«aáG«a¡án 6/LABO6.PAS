program LABO6;

uses
  crt, graph;

const
  num = 3000;xb = 0;yb = 0;

type
  xy = record
    x: real;
    y: real;
    scrx: integer;
    scry: integer;
  end;

procedure InitGr;
var
  grMode: integer;
  grPath: string;
  grDriver: integer;
begin
  grDriver := VGA;
  grmode := 2;
  grPath := 'EGAVGA.BGI';
  initGraph(grDriver, grMode, grPath);
  end;

function funcx(t, a: real): real;
begin
  funcx := a*(t*t-1)/(t*t+1);
end;
function funcy(t, a: real): real;
begin
  funcy := (a*t)*(t*t-1)/(t*t+1);
end;


procedure graf(x0, y0: integer; mx, my: real);
var
  i: integer;s: string;
begin
  line(0, y0, getmaxx, y0);
  line(x0, 0, x0, getmaxy);

  for i := 1 to 20 do
  begin
    line(x0 - 3, y0 + round(i * my), x0 + 3, y0 + round(i * my));
    line(x0 - 3, y0 - round(i * my), x0 + 3, y0 - round(i * my));
    line(x0 + round(i * mx), y0 - 3, x0 + round(i * mx), y0 + 3);
    line(x0 - round(i * mx), y0 - 3, x0 - round(i * mx), y0 + 3);
    str(i, s);
    outtextXY(x0 - 25, y0 + round(i * my), '-' + s);
    outtextXY(x0 - 20, y0 - round(i * my), s);
    str(i, s);
    outtextXY(x0 + round(i * mx), y0 + 10, s);
    outtextXY(x0 - round(i * mx), y0 + 10, '-' + s);
  end;
  outtextxy(x0 + 5, y0 + 10, '0');
  outtextxy(getmaxx - 10, y0 - 10, 'X');
  outtextxy(x0 + 5, 10, 'Y');
  outtextxy(getmaxx - 150, 10, 'Strofoida');
  outtextxy(getmaxx - 200, 20, 'x=a(t^2-1)/(t^2+1)');
  outtextxy(getmaxx - 200, 30, 'y=at(t^2-1)/(t^2+1)');
  outtextxy(getmaxx - 270, 40, 'Vikonav: Horpynych-Raduzhenko I.O.');
end;

var
  arr: array[1..num]of xy;
  t, n, tn, te, dt, mx, my, xe, ye, minx, maxx, miny, maxy: real;
  a, b, d, i, x0, y0: integer;


begin
  clrscr;
  writeln('Vvedite chisla A');
  readln(a);
  InitGr;
  xe := getmaxx-400 ;ye := getmaxy+200;
  tn :=-2*pi;
  te :=2*pi;
  dt := (te - tn) / (num - 1);
  arr[1].x := funcx(tn, a);
  arr[1].y := funcy(tn, a);
  minx := arr[1].x;
  maxx := arr[1].x;
  miny := arr[1].y;
  maxy := arr[1].y;
  for i := 2 to num do
  begin
    tn := tn + dt;
    arr[i].x := funcx(tn, a);
    arr[i].y := funcy(tn, a);
    if arr[i].x < minx then minx := arr[i].x else
    if arr[i].x > maxx then maxx := arr[i].x;
    if arr[i].y < miny then miny := arr[i].y else
    if arr[i].y > maxy then maxy := arr[i].y;
  end;
  mx := (xe - xb) / (maxx - minx);
  my := (ye - yb) / (maxy - miny);
  x0 := getmaxx div 2;
  y0 := getmaxy div 2;
  for i := 1 to num do
  begin
    arr[i].scrx := x0 + round(arr[i].x * mx);
    arr[i].scry := y0 + round(arr[i].y * my);
  end;
  graf(x0, y0, mx, my);
  for i := 1 to num - 1 do
  begin
    line(arr[i].scrx, arr[i].scry, arr[i + 1].scrx, arr[i + 1].scry);
  end; readln;
  closegraph;
  readln;
end.
