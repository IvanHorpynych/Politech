{ заданий символьний фаіл прямого доступу .серед символів тексту
 є символ "#",який видаляє попередній символ .Відповідно,К 
символів # підряд видаляють К попередніх символів тексту а також
 самі символи #. }

Program ProstoProga2;
Uses crt;

Var F: file of char;
	ch: char;
	i,j,k,r,d: integer;
  b: boolean;
BEGIN
  {filemode:=2;}
	Clrscr;
	Assign(f,'3.txt');
	reset(f);
	while not eof(f) do
		begin
			read(f,ch);
			write(ch);
		end;
	b:=false;
  reset(f);
	while b = false do
	begin
    reset(f);
    k:=filesize(F);
    b:=true;
    for i:=0 to k-1 do
    begin
      read(f,ch);
      if ch='#' then begin r:=i; b:=false; break; end;
    end;
    if b=false then
    for j:=1 to 2 do begin
    begin
     if j=2 then r:=r-1;
     for d:=r to filesize(f)-2 do
     begin
       seek(f,d+1);
       read(f,ch);
       seek(f,d);
       write(f,ch);
     end;
     seek(f,filesize(f)-1);
     truncate(F);
    end;
   end;
	end;
  close(f);
  reset(f);
  while not eof(f) do
  begin
    read(f,ch);
    write(ch);
  end;
  readln;
close(f);
END.