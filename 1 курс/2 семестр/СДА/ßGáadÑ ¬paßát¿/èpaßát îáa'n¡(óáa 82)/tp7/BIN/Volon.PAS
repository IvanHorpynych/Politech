program laba5_prog;
uses crt;
const n=7;
type student=record
     surname:string[20];
     name:string[20];
     fathersname:string[20];
     subject1:string[20];
     subject2:string[20];
     subject3:string[20];
     subject4:string[20];
     end;
     masiv=array[1..n] of student;
var a:masiv;
    b:string;
    f:text;
    i,x:byte;
    s:string;

procedure fillmasiv;
begin
assign(f,'lab5.txt');
reset(f);
i:=1;
while not eof(f) do
      while not eoln(f) do
            begin
                 readln(f,b);
                 a[i].surname:=copy(b,1,pos(' ',b)-1);
                 delete(b,1,pos(' ',b));
                 a[i].name:=copy(b,1,pos(' ',b)-1);
                 delete(b,1,pos(' ',b));
                 a[i].fathersname:=copy(b,1,pos(' ',b)-1);
                 delete(b,1,pos(' ',b));
                 a[i].subject1:=copy(b,1,pos(' ',b)-1);
                 delete(b,1,pos(' ',b));
                 a[i].subject2:=copy(b,1,pos(' ',b)-1);
                 delete(b,1,pos(' ',b));
                 a[i].subject3:=copy(b,1,pos(' ',b)-1);
                 delete(b,1,pos(' ',b));
                 a[i].subject4:=copy(b,1,length(b));
                 inc(i);
            end;
close(f);
end;
procedure showmasiv;
begin
for i:=1 to n do
writeln(a[i].surname,a[i].name:10,a[i].fathersname:13,a[i].subject1:12,
a[i].subject2:10,a[i].subject3:12,a[i].subject4:12);
end;
procedure SeekStudent;
begin
for i:=2 to n do
    begin
          s:=s+a[i].subject1+a[i].subject2+a[i].subject3+a[i].subject4;
          x:=pos('good',s);
          if x<>0 then begin
                            delete(s,x,4);
                            x:=pos('perfect',s);
                            if x<>0 then begin
                                              delete(s,x,7);
                                              x:=pos('perfect',s);
                                              if x<>0 then begin
                                                                delete(s,x,7);
                                                                x:=pos('perfect',s);
                                                                if x<>0 then
                                                                writeln(a[i].surname,a[i].name:9,a[i].fathersname:13);

                                                           end;
                                         end;
                        end;
          s:='';
    end;
end;
begin
clrscr;
fillmasiv;
showmasiv;
writeln;
writeln('Students claiming the title of excellent ones:');
SeekStudent;
readkey;
end.

