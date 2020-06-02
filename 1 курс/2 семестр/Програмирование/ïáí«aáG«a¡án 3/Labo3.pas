program Labo3;
var f: file of longint; k,n,h,i:integer;
procedure zsuv(var f:file of longint);
var k,q,t,p:longint;
begin 
p:=filesize(f)-1;
seek(f,p); read(f,k);
for q:=filesize(f)-2 downto 0 do 
  begin
  seek(f,q); read(f,t);
  seek(f,q+1); write(f,t);
  end;
seek(f,0); write(f,k);
end;
procedure druk(f:file of longint);
var i,k:integer;
begin
for i:=0 to filesize(f)-1 do
  begin
  seek(f,i); read(f,k); write(k);
  end;
end;
begin
randomize;
assign(f,'f.dat');  rewrite(f);
write('Vvedite kolichestvo elementov v failye F:');
readln(n);
for i:=0 to n-1 do
  begin
  k:=random(9);
  seek(f,i);  write(f,k);
  end;
write('Ishodny fail F:'); druk(f);  writeln; 
write('Na skolyko poziciy nyzgno sovershit smesheniye?:');
read(h);
  for n:=1 to h do
    zsuv(f);
write('Fail "F" so smecheniem na ',h,' poziciy:');
druk(f);
close(f);
writeln;
end.
