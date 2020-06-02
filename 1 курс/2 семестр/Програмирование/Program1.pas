uses
  crt;
var
  f: text;
  s: string;
  i,k: byte;
  boo: boolean;
  ch: char;
begin
  gotoxy(38,3); write('_ _ _ _ _ _ _');
  gotoxy(0,0);
  assign(f,'1.txt');
  reset(f);
  read(f,s);
  close(f);
  boo:=true;
  repeat
    if s='' then
    begin
      gotoxy(15,20);
      writeln('YOU ARE WIN! :)');
      exit;
    end;
    ch:=readkey;
    if pos(ch,s)=0 then begin
        inc(k);
        case k of
          1: begin
               for i:=1 to 6 do
               begin
                 gotoxy(10,10+i);
                 writeln('|');
               end;
             end;
          2: begin
               for i:=1 to 6 do
               begin
                 gotoxy(10+i,10);
                 writeln('_');
               end;
             end;
          3: begin
               gotoxy(16,11);
               writeln('|');
             end;
          4: begin
               gotoxy(16,12);
               writeln('O');
             end;
          5: begin
               gotoxy(15,13);
               writeln('/|\');
             end;
          6: begin
               gotoxy(15,14);
               writeln('/ \');
               gotoxy(15,20);
               writeln('YOU ARE FAILED');
               exit;
             end;
        end;
      end else
    begin
      delete(s,pos(ch,s),1);
      case upCase(ch) of
        'Ì': begin gotoxy(38,3); write('Ì');
             end;
        'Î': begin
               if boo then begin
                 gotoxy(40,3); write('Î'); boo:=false
               end else begin
                 gotoxy(46,3); write('Î');
               end;
             end;
        'Ğ': begin gotoxy(42,3); write('Ğ');
             end;
        'Ê': begin gotoxy(44,3); write('Ê');
             end;
        'Â': begin gotoxy(48,3); write('Â');
             end;
        'Ü': begin gotoxy(50,3); write('Ü');
             end;
      end;
    end;
  until k>=6;
  readln;
end.