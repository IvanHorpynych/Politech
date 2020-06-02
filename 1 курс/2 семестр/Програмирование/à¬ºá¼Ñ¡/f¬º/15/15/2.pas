program _2;

const n=20;

var f:file of integer;
    ProstSet:set of integer;
 {Процедура заполнения множества простых чисел до 100}   
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
{Процедура заполнения случайными числами файла}
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
    {В это множество я буду записывать первые 3 простые числа 
     из тех случайных которые в файле }
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
   {Фишка в том что простых чисел может быть меньше чем 3
    для этого тут пару дополнительных условий}
   if k in ProstSet then
     if ind<3 then
      begin
       Set1:=Set1+[k];
       write(g,k);//записываю эти первые простые числа в новый файл
       inc(ind)
      end
     else break;
  end;
 {Дальше снова прохожу по первому файлу и переписываю все из него 
  в новый, если попадаю на те первые числа которые я уже туда записал,
  игнорирую их. Тут возникает еще одна сложность, те простые числа,
  которые я уже записал в начало могут повторятся дальше, по этому когда я 
  первый раз на них попадаю, я удаляю их из подолнительного множеста}
 reset(f);
 while eof(f)=false do
  begin
   read(f,k);   
   if not(k in Set1) then write(g,k)
    else Set1:=Set1-[k];
  end;
  
 {переписываю все в начальный файл, а дополнительный удаляю} 
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
