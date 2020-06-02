unit DemoUnit;
interface 
type
  filetype = file of real;
var
  g,f: filetype;
procedure vvod(var f: filetype; n: integer);
procedure vivod(f: filetype);
procedure sort(f: filetype; var g: filetype);
function mult(i: integer): longint;
implementation 

procedure vvod(var f: filetype; n: integer);
var
  i,j: integer; k: real;
begin
  rewrite(f);
  for i:= 1 to n do 
    begin
    k:=1;
    for j:=1 to i do
      k:=k*(mult(j)+ln(j)); 
      write(f, k);
    end;
end;

procedure vivod(f: filetype);
var
  k: real;
begin
  reset(f);
  while not (eof(f)) do begin read(f, k); write(k:1:2); writeln;end; end;

procedure sort(f: filetype; var g: filetype);
var
  m: real;
begin
  reset(g);
  rewrite(g);
  reset(f);
    while not (eof(f)) do 
    begin
    read(f, m);
      if (m<1000) then
      write(g, m);
    end; 
end;

function mult(i: integer): longint;
  var mul,j: longint;
  begin
    mul:=4;
    for j:=2 to i do
    mul:=mul*2;
    mult:=mul;
    end;
end.			
