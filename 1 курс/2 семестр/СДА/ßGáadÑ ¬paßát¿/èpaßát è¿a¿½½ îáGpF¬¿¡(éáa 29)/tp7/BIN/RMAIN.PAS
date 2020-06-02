program RMain;
uses RBody,RConstru,crt;
var f:boolean;
begin
 Zastavka;
 delay(2000);
 repeat
  f:=false;
  MainMenu;
  case NMainMenu of
   0: begin
       StartComponent;
       StartInfOfEllipse;
       StartInfOfCircle;
       Left_Rigth(f);
       if f=true then
        begin
         GameOver;
         NGameOver;
        end;
      end;
   2:Halt;
  end;
 until false;
end.