unit TIMER;

interface

uses GLOBAL;

type proc=procedure(var A:arr);
	 vect=procedure(var C:vector);
var  pr:proc; {Змінна процедурного типу}
	 vec:vect; {Змінна процедурного типу}

function Time(pr:proc):longint; {Функція знаходження часу роботи алгоритму з масивом}
function TimeVector(vec:vect):longint;{Функція знаходження часу роботи алгоритму з вектором}

implementation

uses dos,crt;

type TTime=record {Користувацький тип "запис" для запам'ятовування часу}
      Hours,Min,Sec,HSec:word;
     end;


function ResTime(const STime,FTime:TTime):longint; {Функція знаходження різниці чачу}
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
 pr(A); {Запуск обраної процедури сортування масиву}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 Time:=ResTime(StartTime,FinishTime);
end;



function TimeVector(vec:vect):longint;

var StartTime,FinishTime:TTime;

begin
 with StartTime do
 GetTime(Hours,Min,Sec,HSec);
 vec(C); {Запуск обраної процедури сортування вектора}
 with FinishTime do
 GetTime(Hours,Min,Sec,HSec);
 TimeVector:=ResTime(StartTime,FinishTime);
end;

end.




                                                                         