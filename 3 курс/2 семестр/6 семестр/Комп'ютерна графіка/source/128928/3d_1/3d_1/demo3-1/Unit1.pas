unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel3: TPanel;
    Button4: TButton;
    TswingTimer: TTimer;
    Label37: TLabel;
    Label38: TLabel;
    Label39: TLabel;
    Label40: TLabel;
    Label41: TLabel;
    Label42: TLabel;
    Label43: TLabel;
    Label44: TLabel;
    SBcutAngle: TScrollBar;
    SBspotExp: TScrollBar;
    SBlightRed: TScrollBar;
    SBlightBlue: TScrollBar;
    SBlightGreen: TScrollBar;
    RBambient: TRadioButton;
    RBspecular: TRadioButton;
    RBdiffuse: TRadioButton;
    Procedure FormCreate(Sender: TObject);
    Procedure FormClose(Sender: TObject; var Action: TCloseAction);
    Procedure Panel1MouseMove(Sender: TObject; ShIft: TShIftState; X,Y: Integer);
    procedure SBcutAngleChange(Sender: TObject);
    procedure SBspotExpChange(Sender: TObject);
    procedure RBambientClick(Sender: TObject);
    procedure RBspecularClick(Sender: TObject);
    procedure RBdiffuseClick(Sender: TObject);
    procedure SBlightRedChange(Sender: TObject);
    procedure SBlightBlueChange(Sender: TObject);
    procedure SBlightGreenChange(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure TswingTimerTimer(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TsceneGL;
    mouse:T3dMouse;
    Dolphin:Tentity;
    Light:Tlight;
    SwingX,
    SwingY,
    SwingZ,
    PositionX, minx,maxx:integer;
  end;

var
  Form1: TForm1;
  rx,ry,rz:word;


implementation

{$R *.DFM}

Procedure TForm1.FormCreate(Sender: TObject);
begin
  Scene:=TsceneGL.create;
  Dolphin:=Tentity.create;
  with Dolphin do
  begin
    SetColor(96,96,168);
    LoadDXF('..\dolphin.dxf', true);
    CalcNormals;
    Center;
    Move(0,0,-20);
    id:=1;
  end;
  Scene.Entities.add(Dolphin);

  light:=Tlight.create(1);
  light.LightType:=CLspot;
  light.CutOffAngle:=5;
  Light.SpotExponent:=100;

  // Установка настроек освещения
  Light.SetOrientation(0,-1,0);
  Light.Source.SetPosition(0,5,-20);

  Scene.Lights.add(light);
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);

  mouse:=T3dMouse.create(Dolphin);
  mouse.scale(0.1,-0.1,0.1,1,1,1);
end;

Procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

Procedure TForm1.Panel1MouseMove(Sender: TObject; ShIft: TShIftState; X,Y: Integer);
begin
  mouse.Move(x,y,shIft);
  Scene.Redraw;
end;

procedure TForm1.SBcutAngleChange(Sender: TObject);
begin
  // Изменение угла освещения
  Light.CutOffAngle:=SBcutAngle.Position;
  Scene.Redraw;
end;

procedure TForm1.SBspotExpChange(Sender: TObject);
begin
  // Параметр затухания по EXP
  Light.SpotExponent:=SBspotExp.Position;
  Scene.Redraw;
end;

// Далее 3 процедуры для установки компонент
// освещения :
// 1 Ambient
// 2 Specular
// 3 diffuse
procedure TForm1.RBambientClick(Sender: TObject);
begin
  SBlightRed.position:=round(Light.Fambient[0] * 100);
  SBlightGreen.position:=round(Light.Fambient[1] * 100);
  SBlightBlue.position:=round(Light.Fambient[2] * 100);
end;

procedure TForm1.RBspecularClick(Sender: TObject);
begin
  SBlightRed.position:=round(Light.Fspecular[0] * 100);
  SBlightGreen.position:=round(Light.Fspecular[1] * 100);
  SBlightBlue.position:=round(Light.Fspecular[2] * 100);
end;

procedure TForm1.RBdiffuseClick(Sender: TObject);
begin
  SBlightRed.position:=round(Light.Fdiffuse[0] * 100);
  SBlightGreen.position:=round(Light.Fdiffuse[1] * 100);
  SBlightBlue.position:=round(Light.Fdiffuse[2] * 100);
end;

// 3 процедуры установки R G B параметров, для выбранной
// в предыдущей процедуры компоненты
procedure TForm1.SBlightRedChange(Sender: TObject);
begin
  if RBdiffuse.checked then
    Light.Fdiffuse[0]:=int(SBlightRed.position) / 100;
  if RBambient.checked then
    Light.Fambient[0]:=int(SBlightRed.position) / 100;
  if RBspecular.checked then
    Light.Fspecular[0]:=int(SBlightRed.position) / 100;
  Scene.Redraw;
end;

procedure TForm1.SBlightBlueChange(Sender: TObject);
begin
  if RBdiffuse.checked then
    Light.Fdiffuse[2]:=int(SBlightBlue.position) / 100;
  if RBambient.checked then
    Light.Fambient[2]:=int(SBlightBlue.position) / 100;
  if RBspecular.checked then
    Light.Fspecular[2]:=int(SBlightBlue.position) / 100;
  Scene.Redraw;
end;

procedure TForm1.SBlightGreenChange(Sender: TObject);
begin
  if RBdiffuse.checked then
    Light.Fdiffuse[1]:=int(SBlightGreen.position) / 100;
  if RBambient.checked then
    Light.Fambient[1]:=int(SBlightGreen.position) / 100;
  if RBspecular.checked then
    Light.Fspecular[1]:=int(SBlightGreen.position) / 100;
  Scene.Redraw;
end;

// Делаем переключающийся источник света
procedure TForm1.Button4Click(Sender: TObject);
begin
  TswingTimer.enabled:=not TswingTimer.enabled;
  if TswingTimer.enabled then
    begin
      button4.caption:='Статичное освещение';
      SwingX:=-31;
      PositionX:=1;
      minx:=1;
      maxx:=24;
      SwingZ:=0;
      SwingY:=0;
    end
  else
    button4.caption:='Переключать источники';
end;

procedure TForm1.TswingTimerTimer(Sender: TObject);
const
  Accel:array[1..24] of integer=
  (1,2,4,8,8,8,8,8,8,4,2,1,-1,-2,-4,-8,-8,-8,-8,-8,-8,-4,-2,-1);
begin
  SwingX:=SwingX+Accel[positionX];
  inc(positionX);
  if positionX > MaxX then
  begin
    MinX:=MinX+1;
    MaxX:=MaxX-1;
    PositionX:=MinX;
    if MinX=12 then
      Button4Click(nil);
  end;
  Light.SetOrientation(SwingX, SwingY, SwingZ);
  scene.redraw;
end;

end.
