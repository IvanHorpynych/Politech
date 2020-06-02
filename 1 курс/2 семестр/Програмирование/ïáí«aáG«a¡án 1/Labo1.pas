program labo1;
uses usermode,crt;
var  l: char;
begin 
clrscr;
{a:=[1..6]; b:=[2..6]; c:=[6..7];d:=[5..7];e:=[6..7];}{выполняется условие}
{a:=[1..4]; b:=[2..5]; c:=[1..7];d:=[5..7];e:=[6..7];}{не выполняется оба условия}
a:=[3..4]; b:=[1..5]; c:=[2..7];d:=[5..7];e:=[6..7];{не выполняется оба условия}
x:=[]; h:=e+a;
if (c=e) and (b <= h) then formyla_1(a,b,c,d,e,x) 
   else formyla_2(a,b,c,d,e,x);
write('Искомое множество X:'); vivod_mnojyny(x);
readln;

writeln;
write('Введите текст:');
readln(S);
Y:=K;
findsimbols(S,Y);
write('Оставшиеся согласные:');
  for l:='a' to 'z' do
    if l in Y then
      write(l:2);
readln;      
end.
