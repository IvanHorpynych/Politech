Unit RConstru;

Interface



procedure Left_Rigth(var f:boolean);
procedure StartInfOfEllipse;
procedure StartInfOfCircle;
procedure NGameOver;
function NMainMenu:byte;

Implementation

uses crt,RBody;

type
     TEllipse=record
       Color:TColor;
     end;

     TCircle=record
      Color:TColor;
     end;

var
  ACircle:array[0..n] of TCircle;
  AEllipse:array[0..n] of TEllipse;

procedure StartInfOfEllipse;
var i:word;
begin
 for i:=0 to n do
   case i of
    0..2: AEllipse[i].Color:=white;

       3: AEllipse[i].Color:=notcolor;

    else  AEllipse[i].Color:=red;

   end;{case}
end;

procedure StartInfOfCircle;
var i:word;
begin
 for i:=0 to n do
  begin
   if (i=0) or (i=3) or (i=6) then ACircle[i].Color:=yellow
    else ACircle[i].Color:=green;
  end
end;

procedure Left_Rigth;

var ind,i:word;
    ch:char;
procedure Proverki;
begin
 if (ind>=2)and(ind<=4) then
   if (AEllipse[ind].Color<>notcolor)and((AEllipse[ind-1].Color=notcolor)or
      (AEllipse[ind-2].Color=notcolor)or(AEllipse[ind+1].Color=notcolor)or
      (AEllipse[ind+2].Color=notcolor))
     then
      begin
       PaintCircle(ind,green);
       ACircle[ind].Color:=green;
      end
     else
      begin
       PaintCircle(ind,yellow);
       ACircle[ind].Color:=yellow;
      end;

 if ind=0 then
   if (AEllipse[ind].Color<>notcolor)and((AEllipse[ind+1].Color=notcolor)or
      (AEllipse[ind+2].Color=notcolor))
     then
      begin
       PaintCircle(ind,green);
       ACircle[ind].Color:=green;
      end
     else
      begin
       PaintCircle(ind,yellow);
       ACircle[ind].Color:=yellow;
      end;

 if ind=6 then
   if (AEllipse[ind].Color<>notcolor)and((AEllipse[ind-1].Color=notcolor)or
      (AEllipse[ind-2].Color=notcolor))
     then
      begin
       PaintCircle(ind,green);
       ACircle[ind].Color:=green;
      end
     else
      begin
       PaintCircle(ind,yellow);
       ACircle[ind].Color:=yellow;
      end;

 if ind=1 then
    if (AEllipse[ind].Color<>notcolor)and((AEllipse[ind-1].Color=notcolor)or
       (AEllipse[ind+1].Color=notcolor)or(AEllipse[ind+2].Color=notcolor))
     then
      begin
       PaintCircle(ind,green);
       ACircle[ind].Color:=green;
      end
     else
      begin
       PaintCircle(ind,yellow);
       ACircle[ind].Color:=yellow;
      end;

  if ind=5 then
    if (AEllipse[ind].Color<>notcolor)and((AEllipse[ind+1].Color=notcolor)or
       (AEllipse[ind-1].Color=notcolor)or(AEllipse[ind-2].Color=notcolor))
     then
      begin
       PaintCircle(ind,green);
       ACircle[ind].Color:=green;
      end
     else
      begin
       PaintCircle(ind,yellow);
       ACircle[ind].Color:=yellow;
      end;
end;

begin
 ind:=3;
 repeat
  repeat
   ch:=readkey;
   case ch of
    #75: if ind>0 then
           begin
            ClearCircle(ind);
            dec(ind);
            Proverki;
           end;
    #77: if ind<6 then
           begin
            ClearCircle(ind);
            inc(ind);
            Proverki;
           end;
    #27: Exit;
   end;{case}
  until (ch=#13)and(ACircle[ind].Color=green);
  ClearEllipse(ind);
  for i:=0 to n do
   if AEllipse[i].Color=notcolor then
    begin
     PaintEllipse(i,AEllipse[ind].Color);
     AEllipse[i].Color:=AEllipse[ind].Color;
     AEllipse[ind].Color:=notcolor;
     ACircle[ind].Color:=yellow;
     PaintCircle(ind,yellow);
     break;
    end;

 until (AEllipse[0].Color=red)and(AEllipse[1].Color=red)and
       (AEllipse[2].Color=red)and(AEllipse[3].Color=notcolor)and
       (AEllipse[4].Color=white)and(AEllipse[5].Color=white)and
       (AEllipse[6].Color=white);
 f:=true;
end;


procedure NGameOver;
var ind:byte;
    ch:char;
begin
 ind:=0;
 repeat
  ch:=readkey;
  case ch of
   #72: if ind>0 then
         begin
          PaintRect(9,ind);
          dec(ind);
          PaintRect(5,ind);
         end;
   #80: if ind<1 then
         begin
          PaintRect(9,ind);
          inc(ind);
          PaintRect(5,ind);
         end;
  end; {case}
 until ch=#13;
 case ind of
  0: Exit;
  1: Halt;
 end;

end;

function NMainMenu;
var ind:byte;
    ch:char;
begin
 ind:=0;
 repeat
  ch:=readkey;
  case ch of
   #72: if ind>0 then
         begin
          PaintRect(9,ind);
          dec(ind);
          PaintRect(5,ind);
         end;
   #80: if ind<2 then
         begin
          PaintRect(9,ind);
          inc(ind);
          PaintRect(5,ind);
         end;
  end; {case}
 until ch=#13;
 NMainMenu:=ind;
end;


end.