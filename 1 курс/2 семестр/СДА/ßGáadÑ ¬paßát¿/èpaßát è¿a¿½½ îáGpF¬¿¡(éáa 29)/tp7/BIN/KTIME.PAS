unit KTime;

interface

uses KOsn;

type proc=procedure(var A:arr);
var  pr:proc;
function Time(pr:proc):longint;

implementation

uses dos,crt;

function Time;
type TTime=record
      Hours,Min,Sec,HSec:word;
     end;
var StartTime,FinishTime:TTime;

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

begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
 pr(A);
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 Time:=ResTime(StartTime,FinishTime);
end;

end.




                                                                            360000*Longint(FTime.Hourse)+
