program dop;
uses crt;
const A: set of char=['b','c','d','f','g','h','j','k','l','m','n','p','q','r','s','t','v','x','z'];
var S: string;
    l: char;
    B: set of char;
procedure findsimbols(r:string; var P:set of char);
var i: byte;
begin
i:=1;
while (r[i]<>'.') and (i<>length(r)) do
    begin
    if r[i] in P then
    P:=P-[r[i]];
    i:=i+1;
    end; 
end;
begin
clrscr;
write('Введите текст:');
readln(S);
b:=a;
findsimbols(S,B);
write('Оставшиеся согласные:');
  for l:='a' to 'z' do
    if l in b then
      write(l:2);
readln;      
end.