program LABO6;

type
  TPtr = ^TElem;
  TElem = record
    Inf: integer;
    Link: TPtr;
  end;

var
  BegL, EndL: Tptr;
  n, k: integer;

procedure input(n: integer; var BegL, EndL: Tptr);
var
  p: Tptr;
  i: integer;
begin
  BegL := nil;EndL := nil;
  new(p);
  p^.Inf := random(n);
  p^.Link := nil;
  BegL := p;
  EndL := p;
  for i := 2 to n do
  begin
    new(p);
    p^.Inf := random(n);
    p^.Link := nil;
    EndL^.Link := p;
    EndL := p;
  end;
end;

procedure output(var BegL, EndL: Tptr);
var
  p: Tptr;
begin
  p := BegL;
  while p <> nil do 
  begin
    write(p^.inf:5);
    p := p^.Link;
  end; 
end;

procedure sort(var BegL, EndL: Tptr);
var
  f: boolean;
  p: Tptr;
  value: integer;
begin
  f := true;
  while(f = true) do
  begin
    f := false;p := BegL;
    while p^.Link <> nil do
    begin
      if p^.Inf < p^.Link^.Inf then
      begin
        value := p^.Inf;
        p^.Inf := p^.Link^.Inf;
        p^.Link^.Inf := value;
        f := true;
      end;
      p := p^.Link;
    end;
  end;
end;

procedure clean(var BegL, EndL: Tptr);
var
  p: Tptr;
begin
  while BegL^.Link <> nil do 
  begin
    p := BegL;BegL := BegL^.Link;
    if BegL = nil then EndL := nil;
    dispose(p);
  end;
end;

begin
  write('Vvedite kolichestvo elementov v spiske:');
  readln(n);
  input(n, BegL, EndL);
  output(BegL, EndL);
  sort(BegL, EndL);
  writeln;
  output(BegL, EndL);
  clean(BegL, EndL);
end.