program var2_2;

var 
	f:text;
	s:string;
	i:integer;
	ch:char;
	fl:boolean;
begin
	assign(f,'1.dat');
	rewrite(f);
	writeln('Enter file');
	repeat
		read(ch);
		write(f,ch);
	until ch = 'n';
  close(f);
  assign(f,'1.dat');
	reset(f);
	readln(f,s);
	writeln(s);
readln;
end.
