program _2;


type
  TPtr=^Elem;
  Elem=record
   len:word;
   inf:string;
   link:TPtr;
  end;
  
var f:text;
    Top:TPtr;
    
procedure Pop(value:string);
var P:TPtr;
begin
 new(P);
 P^.inf:=value;
 P^.len:=length(value);
 p^.link:=Top;
 Top:=P;
end;   

procedure Push;
var P:TPtr;
begin
 new(P);
 P:=Top;
 Top:=Top^.link;
 Dispose(P);
end; 

procedure CreateList(var f:text);
var s:string;
begin
 assign(f,'2.txt');
 reset(f);
 while eof(f)=false do
  begin
   readln(f,s);
   Pop(s);
  end;
 close(f); 
end; 

procedure SortList;
var PS,I:TPtr;
    ind,Imax:word;
    Max:word;
    SMax:string;
begin
 new(PS);
 PS:=Top;
 while PS^.link<>nil do
  begin
   Max:=PS^.len;
   SMax:=PS^.inf;
   ind:=0;
   Imax:=0;
   I:=PS^.link;
   while I<>nil do
    begin
     inc(ind);
     if I^.len>Max then
      begin
       Max:=I^.len;
       SMax:=I^.inf;
       Imax:=ind;
      end;
     I:=I^.link;
    end;
   I:=PS;
   for ind:=1 to IMax do I:=I^.link;
   I^.len:=PS^.len;
   I^.inf:=PS^.inf;
   PS^.len:=Max;
   PS^.inf:=SMax;
   PS:=PS^.link;
  end;
end;

procedure CreateNewFile;
var g:text;
    P:TPtr;
begin
 assign(g,'newfile.txt');
 rewrite(g);
 P:=Top;
 while P<>nil do
  begin
   writeln(g,P^.inf);
   P:=P^.link;
  end;
 close(g); 
end;


begin
 CreateList(f);
 SortList;
 CreateNewFile;
 while Top<>nil do Push;
end.
