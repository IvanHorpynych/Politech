program _2;

const n=20;

var f:file of integer;
    ProstSet:set of integer;
 {��������� ���������� ��������� ������� ����� �� 100}   
procedure Prost;
const n=100;
var i,j:word;
    b:boolean;
begin
 ProstSet:=ProstSet+[2];
 b:=true;
 for i:=3 to 100 do
  begin
   for j:=2 to i-1 do
    if i mod j = 0 then
     begin
      b:=false;
      break;
     end;
    if b=true then  ProstSet:=ProstSet+[i];
    b:=true;
   end; 
  Writeln('Prosti chisla do 100:'); 
  for i:=1 to 100 do
   if i in ProstSet then Write(i:5);   
end;    
{��������� ���������� ���������� ������� �����}
procedure InpFile(f:file of integer);
var i:word;
    k:integer;
begin
 assign(f,'f.dat');
 rewrite(f);
 for i:=1 to n do
  begin
   k:=random(100);
   write(f,k);
  end;
end;

procedure OutpFile(f:file of integer);
var k:integer;
begin
 reset(f);
 while eof(f)=false do
  begin
   read(f,k);
   write(k:5);
  end; 
end; 

procedure Change(f:file of integer);
var g:file of integer;
    {� ��� ��������� � ���� ���������� ������ 3 ������� ����� 
     �� ��� ��������� ������� � ����� }
    Set1:set of integer;
    k,ind,i:integer;
begin
 assign(g,'g.dat');
 rewrite(g);
 reset(f);
 ind:=0;
 for i:=1 to n do
  begin 
   read(f,k);
   {����� � ��� ��� ������� ����� ����� ���� ������ ��� 3
    ��� ����� ��� ���� �������������� �������}
   if k in ProstSet then
     if ind<3 then
      begin
       Set1:=Set1+[k];
       write(g,k);//��������� ��� ������ ������� ����� � ����� ����
       inc(ind)
      end
     else break;
  end;
 {������ ����� ������� �� ������� ����� � ����������� ��� �� ���� 
  � �����, ���� ������� �� �� ������ ����� ������� � ��� ���� �������,
  ��������� ��. ��� ��������� ��� ���� ���������, �� ������� �����,
  ������� � ��� ������� � ������ ����� ���������� ������, �� ����� ����� � 
  ������ ��� �� ��� �������, � ������ �� �� ��������������� ��������}
 reset(f);
 while eof(f)=false do
  begin
   read(f,k);   
   if not(k in Set1) then write(g,k)
    else Set1:=Set1-[k];
  end;
  
 {����������� ��� � ��������� ����, � �������������� ������} 
 reset(g);
 rewrite(f);
 while eof(g)=false do
  begin
   read(g,k);
   write(f,k);
  end;
 close(g);
 erase(g);
 reset(f);
 writeln;
 Writeln('Element of new file: ');
 OutpFile(f);
 close(f)
  
end;

begin
 Prost;
 writeln;
 InpFile(f);
 writeln('Elements of file:');
 OutpFile(f);
 Change(f);
end.
