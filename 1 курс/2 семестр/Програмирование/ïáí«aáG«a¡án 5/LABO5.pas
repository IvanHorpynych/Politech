program LABO5;

type
  func = function(x: real): real;

var
  a, b, eps, int2, int1: real;
  n: integer;
{$F+}
function q(x: real): real;
begin
  q := sin(1/(x*ln(x)*ln(x)));
end;
{$F-}
function integral(f: func; a, b: real; var n: integer): real;
var
  s, integ, h: real;
  i: integer;
begin
  if odd(n) then n := n + 1;
  h := (b - a) / n;
  s := (f(a) + f(b));
  for i := 1 to n - 1 do s := s + 2 * (i mod 2 + 1) * F(a + i * h); 
  integ := s * (h / 3);
  integral := integ;
end;

begin
  writeln('Integral sin(1/x*ln^2(x));');
  writeln('Vvedite granici:');
  readln(a, b);
  writeln('Vvedite tochnost:');
  readln(eps);
  writeln('Vvedite kolichestvo shagov:');
  readln(n);
  int1 := 0; 
  int2 := integral(q, a, b, n); 
  while abs(int2 - int1) > eps do 
  begin
    int1 := int2;
    n := 2 * n;
    int2 := integral(q, a, b, n);   
  end;
  writeln('Integral =', int2:5:5);
  readln;
end.