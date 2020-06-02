unit TIMER;

interface

uses GLOBAL;

type proc=procedure(var A:arr);
	 vect=procedure(var C:vector);
var  pr:proc; {Zminna protsedurnoho typu}
	 vec:vect; {Zminna protsedurnoho typu}

function Time(pr:proc):longint; {Funktsiya znakhodzhennya chasu roboty alhorytmu z masyvom}
function TimeVector(vec:vect):longint; {Funktsiya znakhodzhennya chasu roboty alhorytmu z vektorom}

implementation

uses dos,crt;

type TTime=record {Korystuvats'kyy typ "zapys" dlya zapam`yatovuvannya chasu}
      Hours,Min,Sec,HSec:word;
     end;


function ResTime(const STime,FTime:TTime):longint; {Funktsiya znakhodzhennya riznytsi chachu}
begin
 ResTime:=360000*Longint(FTime.Hours)+
          6000*Longint(FTime.Min)+
          100*Longint(FTime.Sec)+
            Longint(FTime.HSec)-
          360000*Longint(STime.Hours)-
          6000*Longint(STime.Min)-
          100*Longint(STime.Sec)-
            Longint(STime.HSec);
end;

function Time(pr:proc):longint;

var StartTime,FinishTime:TTime;

begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
 pr(A); {Zapusk obranoyi protsedury sortuvannya masyvu}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 Time:=ResTime(StartTime,FinishTime);
end;



function TimeVector(vec:vect):longint;

var StartTime,FinishTime:TTime;

begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
 vec(C); {Zapusk obranoyi protsedury sortuvannya vektora}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 TimeVector:=ResTime(StartTime,FinishTime);
end;

end.




                                                                         