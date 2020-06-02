Unit UnitMenu;

{//////////////////////////////////////////}
Interface
{//////////////////////////////////////////}
type TProc=procedure;

Procedure Menu(n:integer; Name:string; Proc:TProc);


{//////////////////////////////////////////}
Implementation
{//////////////////////////////////////////}

uses crt;

Procedure Menu;
var i:integer;
    ch:char;
begin
 for i:=1 to 10 do Writeln;
 Writeln('Лабораторна робота № ':50,n);
 Writeln;
 Writeln(Name:42);
 Writeln;
 Writeln('Студента групи КВ-31':48);
 Writeln;
 Writeln('Анастасьєва Д.':42);
 readkey;
 clrscr;
 Writeln('Pressed:');
 Writeln('  1 - Lab',n);
 Writeln('  2 - Exit');
 repeat
 ch:=readkey;
 case ch of
  '1': Proc;
  '2': Exit;
  end;
  until (ch='1')or(ch='2');
end; 

end.
