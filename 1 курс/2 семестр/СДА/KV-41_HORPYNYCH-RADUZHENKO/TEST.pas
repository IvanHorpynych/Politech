unit TEST;

interface

 uses
  GLOBAL,WORKAR,TIMER,crt,dos; 
  
procedure Main_1(var a,v:arr; var C:vector); {Paketnyy vyvid serednikh znachen' chasu roboty alhorytmiv}
procedure Main_2(var a,v:arr; var C:vector);{Paketnyy vyvid znachen' chasu roboty alhorytmiv}

implementation


var ch:char;

procedure Main_1(var a,v:arr; var C:vector); {Paketnyy vyvid serednikh znachen' chasu roboty alhorytmiv}
const n=14;
var var_sort,var_alg,i:integer;
    Min,Max:longint;
    B:array[1..n]of longint; {Vektor dlya zapam"yatovuvannya chasu roboty alhorytmiv}
    Sum:real;
    T:longint; {Komirka zapam"yatovuvannya chasu}
begin
clrscr;
writeln('                    Sort    UnSort   BackSort');

UnSortArray(V); {Zapovnyuye dodatkovyy masyv randomnymy znachennyamy}

 var_alg:=1; {Pochatkovi znachennya vyboru alhorytmu}
 repeat
  var_sort:=1;{Pochatkovi znachennya vyboru metodu zapovnennya masyvu}
  case var_alg of {Vybir alhorytmu}
   1: begin
        write('Workaround 1:');
        pr:=WORKAR1711;{Protsedurnomu vkazivnyku prysvoyuyet'sya adresa alhorytmu z vykorystannyam dodatkovoho masyvu}
      end;
   2: begin
        write('Workaround 2:');
        pr:=WORKAR1712; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa z vykorystannyam elementiv "uyavnoho" vektora}
      end;

   3: begin
        write('Workaround 3:');
        pr:=WORKAR1713; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa alhorytmu zdiysnyuyuchy obkhid po elementakh  masyvu}
      end;
  end;{case}

  repeat
   case var_sort of{Vybir metodu zapovnennya masyvu}
    1:  for i:=1 to n do {Lichyl'nyk kil'kosti vykonannya alhorytmu}
         begin
          SortArray(A);{Protsedura vporyadkovannoho zapovnennya masyvu}
          B[i]:=Time(pr); {Prysvoyennya komirtsi vektora chasu roboty alhorytma}
         end;
    2: for i:=1 to n do {Lichyl'nyk kil'kosti vykonannya alhorytmu}
         begin
          UnSortCopy(A); {Protsedura kopiyuvannnya z dodatkovoho randomnoho masyvu u neobkhidnyy}
          B[i]:=Time(pr);{Prysvoyennya komirtsi vektora chasu roboty alhorytma}
         end;
    3: for i:=1 to n do {Lichyl'nyk kil'kosti vykonannya alhorytmu}
         begin
          BackSortArray(A); {Protsedura zapovnennya oberneno vporyadkovannoho masyvu}
          B[i]:=Time(pr); {Prysvoyennya komirtsi vektora chasu roboty alhorytma}
         end;
   end;{case}

   Min:=B[3];
   Max:=B[3];
   for i:=4 to n do {Poshuk minimal'noho ta maksymal'noho znachennya chasu roboty}
    begin
     if B[i]>Max then Max:=B[i];
     if B[i]<Min then Min:=B[i];
    end;
   Sum:=0;
   for i:=3 to n do Sum:=Sum+B[i];
   Sum:=(Sum-Min-Max)/10; {Znakhodzhennya seredn'oho znachennya roboty alhorytmu}
   if var_sort<>3 then Write(Sum:10:1)
    else Writeln(Sum:10:1);

   inc(var_sort); {Zmina vyboru metodu zapovnennya masyvu}
  until var_sort>3;
  inc(var_alg); {Zmina vyboru alhorytmu}
 until var_alg>3;
Writeln('Press Enter for continue');
readln;

vec:=SortVector; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa alhorytmu "vstavka-obmin" sortuvannya vektora}
  SortVect(C);{Protsedura vporyadkovannoho zapovnennya vektora}
  T:=TimeVector(vec); {Komirtsi zapam`yatovuvannya chasu prysvoyuyet'sya znachennya chasu roboty alhorytmu}
  writeln('Sort Vector:',T*p); {Vyvid na ekran teoretychnoho znachennya roboty alhorytmu z vektorom}
  UnSortVect(C); {Protsedura nevporyadkovannoho zapovnennya vektora}
  T:=TimeVector(vec); {Komirtsi zapam`yatovuvannya chasu prysvoyuyet'sya znachennya chasu roboty alhorytmu}
  writeln('UnSort Vector:',T*p); {Vyvid na ekran teoretychnoho znachennya roboty alhorytmu z vektorom}
  BackSortVect(C); {Protsedura zapovnennya oberneno vporyadkovannoho vektora}
  T:=TimeVector(vec); {Komirtsi zapam`yatovuvannya chasu prysvoyuyet'sya znachennya chasu roboty alhorytmu}
  writeln('BackSort Vector:',T*p); {Vyvid na ekran teoretychnoho znachennya roboty alhorytmu z vektorom}

  Writeln('Press Enter for continue');
ch:=readkey;
if ch=#32 then exit;{Vykhid z roboty protsedury}
end;

procedure Main_2(var a,v:arr; var C:vector); {Paketnyy vyvid znachen' chasu roboty alhorytmiv}
var var_sort,var_alg,i:integer;
T,B:longint; {Komirky zapam"yatovuvannya chasu}
begin
clrscr;
writeln('                    Sort    UnSort   BackSort');

UnSortArray(V);{Zapovnyuye dodatkovyy masyv randomnymy znachennyamy}

 var_alg:=1; {Pochatkovi znachennya vyboru alhorytmu}
 repeat
  var_sort:=1; {Pochatkovi znachennya vyboru metodu zapovnennya masyvu}
  case var_alg of {Vybir alhorytmu}
   1: begin
        write('Workaround 1:');
        pr:=WORKAR1711; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa alhorytmu z vykorystannyam dodatkovoho masyvu}
      end;
   2: begin
        write('Workaround 2:');
        pr:=WORKAR1712; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa z vykorystannyam elementiv "uyavnoho" vektora}
      end;

   3: begin
        write('Workaround 3:');
        pr:=WORKAR1713; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa alhorytmu zdiysnyuyuchy obkhid po elementakh masyvu}
      end;
  end;{case}

  repeat
   case var_sort of {Vybir metodu zapovnennya masyvu}
    1:   begin
          SortArray(A);{Protsedura vporyadkovannoho zapovnennya masyvu}
          B:=Time(pr);{Prysvoyennya komirtsi znachennya chasu roboty alhorytma}
         end;
    2:   begin
          UnSortCopy(A); {Protsedura kopiyuvannnya z dodatkovoho randomnoho masyvu u neobkhidnyy}
          B:=Time(pr);{Prysvoyennya komirtsi znachennya chasu roboty alhorytma}
         end;
    3:   begin
          BackSortArray(A); {Protsedura zapovnennya oberneno vporyadkovannoho masyvu}
          B:=Time(pr); {Prysvoyennya komirtsi vektora chasu roboty alhorytma}
         end;
   end;{case}

   if var_sort<>3 then Write(B:10)
    else Writeln(B:10);
   inc(var_sort);
  until var_sort>3;
  inc(var_alg);
 until var_alg>3;
Writeln('Press Enter for continue');
readln;

vec:=SortVector; {Protsedurnomu vkazivnyku prysvoyuyet'sya adresa alhorytmu "vstavka-obmin" sortuvannya vektora}
  SortVect(C); {Protsedura vporyadkovannoho zapovnennya vektora}
  T:=TimeVector(vec);{Komirtsi zapam`yatovuvannya chasu prysvoyuyet'sya znachennya chasu roboty alhorytmu}
  writeln('Sort Vector:',T*p); {Vyvid na ekran teoretychnoho znachennya roboty alhorytmu z vektorom}
  UnSortVect(C);{Protsedura nevporyadkovannoho zapovnennya vektora}
  T:=TimeVector(vec);{Komirtsi zapam`yatovuvannya chasu prysvoyuyet'sya znachennya chasu roboty alhorytmu}
  writeln('UnSort Vector:',T*p);{Vyvid na ekran teoretychnoho znachennya roboty alhorytmu z vektorom}
  BackSortVect(C);{Protsedura zapovnennya oberneno vporyadkovannoho vektora}
  T:=TimeVector(vec);{Komirtsi zapam`yatovuvannya chasu prysvoyuyet'sya znachennya chasu roboty alhorytmu}
  writeln('BackSort Vector:',T*p); {Vyvid na ekran teoretychnoho znachennya roboty alhorytmu z vektorom}

  Writeln('Press Enter for continue');
ch:=readkey;
if ch=#32 then exit;  {Vykhid z roboty protsedury}
end;

end.