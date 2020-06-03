unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons,printers;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Panel1: TPanel;
    Panel2: TPanel;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton4: TSpeedButton;
    SpeedButton3: TSpeedButton;
    SpeedButton5: TSpeedButton;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,Y: Integer);
    procedure SpeedButton1Click(Sender: TObject);
    procedure SpeedButton2Click(Sender: TObject);
    procedure SpeedButton3Click(Sender: TObject);
    procedure SpeedButton4Click(Sender: TObject);
    procedure SpeedButton5Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TSceneGL;
    Dolphin:TEntity;
    Mouse:T3DMouse;
  end;

var
  Form1: TForm1;
  rx,ry,rz:word;

implementation

{$R *.DFM}

procedure TForm1.Button1Click(Sender: TObject);
begin
  close;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  light:Tlight;
begin
  Scene:=TSceneGL.create; // Создаем сцену

  dolphin:=TEntity.create; // Создаем и инициализируем объект
  dolphin.SetColor(200,250,250);
  dolphin.LoadDXF('..\dolphin.dxf',true);
  dolphin.CalcNormals;
  dolphin.Center;
  with dolphin do
  begin
    move(0,0,-10);
  end;
  Scene.Entities.add(dolphin);
  light:=Tlight.create(1);
  Scene.lights.add(light);
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);
  Mouse:=T3DMouse.create(dolphin);
  Mouse.scale(1,1,0.1,1,1,1);
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

procedure TForm1.Panel1MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
begin
  Mouse.move(x, y, shift);
  Scene.Redraw;
end;

procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
  Mouse.Block(4,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton2Click(Sender: TObject);
begin
  Mouse.Block(5,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton3Click(Sender: TObject);
begin
  Mouse.Block(6,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton4Click(Sender: TObject);
begin
  Mouse.Block(3,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton5Click(Sender: TObject);
begin
// Меняем способ отображения
  case dolphin.wireframe of
    0:begin
        speedbutton5.caption:='Линии';
        dolphin.wireframe:=1;
      end;
    1:begin
        speedbutton5.caption:='Заливка';
        dolphin.wireframe:=2;
      end;
    2:begin
        speedbutton5.caption:='Точки';
        dolphin.wireframe:=0;
      end;
  end;
  Scene.Redraw; // обновим сцену
end;

end.
