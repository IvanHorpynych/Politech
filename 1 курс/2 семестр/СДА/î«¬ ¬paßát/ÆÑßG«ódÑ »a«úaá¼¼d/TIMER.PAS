unit TIMER;

interface

uses GLOBAL;

type proc=procedure(var A:arr; var T:longint);
	   vect=procedure(var C:vector; var T:longint);
     TTime=record
      Hours,Min,Sec,HSec:word;
     end;
var  pr:proc; {Zminna protsedurnoho typu}
	   vec:vect;
     T:longint;
function Time(pr:proc):longint;
function TimeVector(vec:vect):longint;
function ResTime(const STime,FTime:TTime):longint;
implementation

uses dos,crt;




function ResTime(const STime,FTime:TTime):longint;
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
 pr(A,T); {Zapusk obranoyi protsedury sortuvannya}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 Time:=ResTime(StartTime,FinishTime);
end;



function TimeVector(vec:vect):longint;

var StartTime,FinishTime:TTime;
    T:integer;
begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
{ vec(C,T);} {Zapusk obranoyi protsedury sortuvannya}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 TimeVector:=ResTime(StartTime,FinishTime);
end;

end.




                                                                         