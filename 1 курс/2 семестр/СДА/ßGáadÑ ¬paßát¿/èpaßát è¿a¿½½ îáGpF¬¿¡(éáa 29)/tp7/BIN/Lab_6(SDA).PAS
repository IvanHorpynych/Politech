program Lab_6;
uses crt;
const m=24; n=80;
var i,j,p:byte;
procedure Output;
 begin
  write('*');
  delay(1);
 end;
begin
 clrscr;
 for p:=1 to trunc(m/2) do
  begin
   for j:=n-p+1 downto p+1 do
    begin
     gotoxy(j,m-p+1);
     Output;

    end;
   for i:=m-p+1 downto p+1 do
    begin
     gotoxy(p,i);
     Output;
    end;
   for j:=p to n-p do
    begin
     gotoxy(j,p);
     Output;
    end;
   for i:=p to m-p do
    begin
     gotoxy(n-p+1,i);
     Output;
    end;
  end;

 if odd(m)=true then
  for j:=n-trunc(m/2)+1 downto trunc(m/2)+1 do
   begin
    gotoxy(j,trunc((m+1)/2));
    Output;
   end;

 readln;
end.