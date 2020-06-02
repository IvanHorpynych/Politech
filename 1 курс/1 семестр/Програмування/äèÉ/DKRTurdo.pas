program RGRFriday13;
{pervaya "pyatnitsa 13" v 21 veke: 13.10.2000; ot predyiduschey pyatnitsyi(13.08.1999) ih otdelyaet 427 dney}
const
  mpos: array [1..12] of byte = (31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
  n=2000; m=2099;

var
  month, year, days, day13, l: integer;

begin
  days := 414; {schetchik dney; kolichestvo dney s proshloy pÿtnitsyi (13.08.1999) 13 do mesyatsa sleduyuschey (01.10.2000)}
  day13 := 0; {schetchik pyatnits 13}
  month := 10; {podschet nachinaem s mesyatsa pervoy pyatnitsyi 13 v 21 veke, toest s 10}
  for year :=n to m do
  begin
    while month <= 12 do
    begin
      days :=days+13;{pribavlyaem chislo 13 dlya proverki na suschestvovanie pyatnitsyi 13 v etom mesyatse}
      l := days mod 7;
      if l= 0 then {proverka ostatka deleniya schetchika na chislo 7}
        day13 := day13 + 1; {esli schYotchik dney delitsya natselo na 7, to eto odna iz pyatnits 13}
        days := days + (mpos[month]-13); {pribavlyaem k schYotchiku kolichestvo ostavshihsya dney v mesyatse}
        if (month = 2) and (year mod 4 = 0) and ((year mod 100<>0)or (year mod 400=0)) then 
        inc(days); {esli mesyats fevral i god vyisokosnyiy, dobavlyaem eschYo odin den}
        month := month + 1 {perehod na sleduyuschiy mesyats}
    end;
    month := 1{vozvraschenie schetchika na nachalnuyu pozitsiyu goda}
  end;
  writeln('Kolichestvo "Pyatnits 13" v period s ',n,' goda po ',m,': ',day13);
  readln;
end.