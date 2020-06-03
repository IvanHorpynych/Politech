unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel3: TPanel;
    GroupBox1: TGroupBox;
    Label37: TLabel;
    SBcutAngle: TScrollBar;
    Label39: TLabel;
    SBspotExp: TScrollBar;
    Label41: TLabel;
    SBlightRed: TScrollBar;
    Label42: TLabel;
    Label43: TLabel;
    SBlightBlue: TScrollBar;
    Label44: TLabel;
    SBlightGreen: TScrollBar;
    Bevel3: TBevel;
    RBambient: TRadioButton;
    RBspecular: TRadioButton;
    RBdiffuse: TRadioButton;
    Button4: TButton;
    GroupBox2: TGroupBox;
    SBvectorUpX: TScrollBar;
    SBvectorUpY: TScrollBar;
    SBvectorUpZ: TScrollBar;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label13: TLabel;
    SBcenterX: TScrollBar;
    Label14: TLabel;
    SBcenterY: TScrollBar;
    Label15: TLabel;
    SBcenterZ: TScrollBar;
    Label16: TLabel;
    Button5: TButton;
    Panel4: TPanel;
    RBConstant: TRadioButton;
    RBlinear: TRadioButton;
    RBquad: TRadioButton;
    SBatenuation: TScrollBar;
    Label25: TLabel;
    CBlightType: TComboBox;
    Procedure Button1Click(Sender: TObject);
    Procedure FormCreate(Sender: TObject);
    Procedure FormClose(Sender: TObject; var Action: TCloseAction);
    Procedure Panel1MouseMove(Sender: TObject; ShIft: TShIftState; X,
      Y: Integer);
    procedure SBcutAngleChange(Sender: TObject);
    procedure SBspotExpChange(Sender: TObject);
    procedure RBambientClick(Sender: TObject);
    procedure RBspecularClick(Sender: TObject);
    procedure RBdiffuseClick(Sender: TObject);
    procedure SBlightRedChange(Sender: TObject);
    procedure SBlightBlueChange(Sender: TObject);
    procedure SBlightGreenChange(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure SBvectorUpXChange(Sender: TObject);
    procedure SBcenterXChange(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure SBatenuationChange(Sender: TObject);
    procedure RBConstantClick(Sender: TObject);
    procedure RBlinearClick(Sender: TObject);
    procedure RBquadClick(Sender: TObject);
    procedure CBlightTypeChange(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TsceneGL;
    mouse:T3dMouse;
    tunnel:Tentity;
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

Procedure TForm1.Button1Click(Sender: TObject);
begin
  close;
end;

Procedure TForm1.FormCreate(Sender: TObject);
begin
  Scene:=TsceneGL.create;
  // Создаем туннель
  tunnel:=Tentity.create;
  tunnel.SetColor(139,139,139);
  tunnel.LoadDXF('..\tunnel.dxf', true);
  tunnel.CalcNormals;
  tunnel.Center;
  with tunnel do
  begin
    Move(0,0,-10);
    id:=1;
  end;
  Scene.Entities.add(tunnel);
  // Зададим источник освещения
  light:=Tlight.create(1);
  light.LightType:=CLstar;
  light.CutOffAngle:=5;
  Light.SpotExponent:=100;

  Light.SetOrientation(0,0,-1);
  Light.Source.SetPosition(0,0,0);

  CBlightType.itemIndex:=1;  {select Positional light on the interface}

  Scene.Lights.add(light);
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);
  mouse:=T3dMouse.create(tunnel);
  mouse.scale(0.1,-0.1,0.1,1,1,1);
end;

Procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

Procedure TForm1.Panel1MouseMove(Sender: TObject; ShIft: TShIftState; X,
  Y: Integer);
begin
  mouse.Move(x,y,shIft);
  Scene.Redraw;
end;

procedure TForm1.SBcutAngleChange(Sender: TObject);
begin
  Light.CutOffAngle:=SBcutAngle.Position;
  Scene.Redraw;
end;

procedure TForm1.SBspotExpChange(Sender: TObject);
begin
  Light.SpotExponent:=SBspotExp.Position;
  Scene.Redraw;
end;

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

procedure TForm1.Button4Click(Sender: TObject);
begin
  light.source.point.z:=light.source.point.z-1;
  scene.redraw;
end;

procedure TForm1.SBvectorUpXChange(Sender: TObject);
begin
  Scene.ActiveCamera.SetPosition(
    SBvectorUpX.position,
    SBvectorUpY.position,
    SBvectorUpZ.position);
  Scene.Redraw;
end;


procedure TForm1.SBcenterXChange(Sender: TObject);
begin
  Scene.ActiveCamera.LookAt(
    SBcenterX.position,
    SBcenterY.position,
    SBcenterZ.position);
  Scene.Redraw;
end;


procedure TForm1.Button5Click(Sender: TObject);
begin
  light.source.point.z:=light.source.point.z+1;
  scene.redraw;
end;

procedure TForm1.SBatenuationChange(Sender: TObject);
begin
  Light.attenuation:=int(SBatenuation.position) / 100;
  scene.redraw;
end;

procedure TForm1.RBConstantClick(Sender: TObject);
begin
  light.attenuationType:=CLconstant;
  scene.redraw;
end;

procedure TForm1.RBlinearClick(Sender: TObject);
begin
  light.attenuationType:=CLlinear;
  scene.redraw;
end;

procedure TForm1.RBquadClick(Sender: TObject);
begin
  light.attenuationType:=CLquadratic;
  scene.redraw;
end;

procedure TForm1.CBlightTypeChange(Sender: TObject);
begin
  case CBlightType.itemIndex of
    0: Light.lightType:=CLambiental;
    1: Light.lightType:=CLstar;
    2: Light.lightType:=CLSpot;
  end; {case}
  Scene.Redraw;
end;

end.
