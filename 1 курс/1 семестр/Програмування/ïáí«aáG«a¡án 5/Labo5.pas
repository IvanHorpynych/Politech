program labo5;
uses crt;
type
 student = record
 prz,im,pb:string;
 progr,sda,mat,lin,D_sum:integer;
 day,month,year:integer;
end;
const
 Size_of_month: array [1..12] of integer =(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
var
 anketa: array [1..25] of student;
 a:array [1..10] of integer;
 f:text;
 z,i:integer;s1:string;
procedure input;
var
 n,err,k,j,ch,dday,mmonth,yyear:integer;
 s:string;
begin
 i:=1;
 while not eof(f) do begin
  readln(f,s);
  n:=length(s);
  k:=2;{?}
   for j:=1 to n do
    if s[j]=' ' then
     begin
      a[k]:=j;
      inc(k);;
     end;
   with anketa[i] do begin
    prz:=copy(s,a[1]+1,a[2]-a[1]-1);
    im:=copy(s,a[2]+1,a[3]-a[2]-1);
    pb:=copy(s,a[3]+1,a[4]-a[3]-1);
    val(s[a[4]+1],ch,err);
    progr:=ch;
    val(s[a[5]+1],ch,err);
    sda:=ch;
    val(s[a[6]+1],ch,err);
    mat:=ch;
    val(s[a[7]+1],ch,err);
    lin:=ch;
    s1:=copy(s,a[8]+1,2);
    val(s1,ch,err);
    day:=ch;
    s1:=copy(s,a[9]+1,2);
    val(s1,ch,err);
    month:=ch;
    s1:=copy(s,a[10]+1,4);
    val(s1,ch,err);
    year:=ch;
  yyear:=1990;
  mmonth:=1;D_sum:=0; 
  while yyear<year do 
 begin
  D_sum:=D_sum + 365;
  if (yyear mod 4=0) and ((yyear mod 100<>0)or (yyear mod 400=0)) then
  inc (D_sum); 
  inc (yyear);
 end;

mmonth:=1 ;
while mmonth<month do 
 begin
  D_sum:=D_sum+Size_of_Month[mmonth] ; 
if (year mod 4=0) and (mmonth=2) then
inc (D_sum) ;
inc (mmonth) 
 end;
 D_sum:=D_sum+day;
   end;
  inc(i); end;
end;
procedure output;
begin
  with anketa[z] do
   writeln(' ',prz,' ',im,' ',pb,' ',progr,' ',sda,' ',mat,' ',lin,'; ',day,'.',month,'.',year);
 writeln;
      end;
procedure sort_born(i:integer);
var
  o: integer;
  n: string;
  h,j: byte;
begin
 for h:=1 to i-1 do
    for j:=h+1 to i do
      if anketa[h].D_sum>anketa[j].D_sum then
          begin
            n:=anketa[h].prz;
            anketa[h].prz:=anketa[j].prz;
            anketa[j].prz:=n;
            n:=anketa[h].im;
            anketa[h].im:=anketa[j].im;
            anketa[j].im:=n;
            n:=anketa[h].pb;
            anketa[h].pb:=anketa[j].pb;
            anketa[j].pb:=n;
            o:=anketa[h].mat;
            anketa[h].mat:=anketa[j].mat;
            anketa[j].mat:=o;
            o:=anketa[h].lin;
            anketa[h].lin:=anketa[j].lin;
            anketa[j].lin:=o;
            o:=anketa[h].progr;
            anketa[h].progr:=anketa[j].progr;
            anketa[j].progr:=o;
            o:=anketa[h].sda;
            anketa[h].sda:=anketa[j].sda;
            anketa[j].sda:=o;
            o:=anketa[h].day;
            anketa[h].day:=anketa[j].day;
            anketa[j].day:=o;
            o:=anketa[h].month;
            anketa[h].month:=anketa[j].month;
            anketa[j].month:=o;
            o:=anketa[h].year;
            anketa[h].year:=anketa[j].year;
            anketa[j].year:=o;
            o:=anketa[h].D_sum;
            anketa[h].D_sum:=anketa[j].D_sum;
            anketa[j].D_sum:=o;
          end; end;
begin
clrscr;
assign(f,'LAB5.DAT');
reset(f);
input;
close(f);
writeln('Studenty:');
for z:=1 to i-1{&} do
begin
writeln('==========================================================='); 
 output;
 end;
writeln('===========================================================');
sort_born(i);
writeln;
writeln('Studenty po date rozhdeniya(v poryadke vozrastaniya):');
writeln;
for z:=2 to i{?} do
begin
writeln('-----------------------------------------------------------');
output;
end;
writeln('-----------------------------------------------------------'); 
readln; 
end.