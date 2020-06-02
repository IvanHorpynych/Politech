{2. Есть файл текстового типа. Отсортировать его по неувеличению длинны строки.}

program variant20_2;

uses
  crt;
const n = 100;
type Tfile=text;
Var FF : Tfile;
	
    A: array[1..n] of string;
   


procedure OutFile(var dop: Tfile; name: char);
var
  str: string;
begin
 	reset(ff);
	while not(eof(ff)) do
		begin
			readln(ff,str);
			writeln(str);
		end;
	readln;
close(ff);
end;


procedure zapus(var dop: Tfile);
var
 str: string;i, j: integer;
begin
  I:=1;
  reset(ff);
  while not eof(ff) do
    Begin
    readln(ff,str);
    A[i]:=str;
    I:=i+1;
    end;
    close(ff);
  for i:=1 to n-1 do
  for j:=i+1 to n  do
  begin
    if (length(a[i]) < length(a[j])) then
      begin
        str:=a[i];
        a[i]:=a[j];
        a[j]:=str;
      end;
  end;
  readln;
end;
procedure newFile(var dop: Tfile);
var
 i:byte;
begin
 	rewrite(ff);
  for I:=1 to n do
    if a[i]<>'' then writeln(ff,a[i]);
 
close(ff);
end;
procedure nwFile(var dop: Tfile);
var
  str: string;
begin
 	 reset(ff);
  while not eof(ff) do
  begin
    readln(ff,str);
    writeln(str);
  end;
  writeln;
close(ff);
end;
begin
  clrscr;
  assign(ff, '4.txt');
 
  OutFile(ff, 'F');
  
  zapus(ff);
  newFile(ff);
  nwFile(ff);
  
end.