unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls, UrickGL, U3Dpolys;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Timer1: TTimer;
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Timer1Timer(Sender: TObject);
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TSceneGL;
  end;

var
  Form1: TForm1;
  rx,ry,rz:word;

implementation

{$R *.DFM}

procedure TForm1.FormCreate(Sender: TObject);
var
  Dolphin:TEntity;
  Light:TLight;
begin
  Scene:=TSceneGL.create;
  // Создаем дельфина и загружаем его из файла
  Dolphin:=TEntity.create;
  Dolphin.SetColor(100,100,160);
  Dolphin.LoadDXF('..\Dolphin.dxf',true);

  Dolphin.CalcNormals; // высчитываем нормали векторов объекта
  Dolphin.Center;
  with Dolphin do
  begin
    move(0,0,-10);
    Rotate(-30,-30,-30);
  end;
  Scene.Entities.add(Dolphin);
  Light:=TLight.create(1);
  Scene.lights.add(Light);
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);
  Scene.Redraw;
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  inc(rx,1); if rx>360 then rx:=0;
  inc(ry,2); if ry>360 then ry:=0;
  dec(rz,1); if rz<0 then rz:=360;
  TEntity(Scene.Entities.Items[0]).Rotate(rx,ry,rz);
  Scene.Redraw;
end;

procedure TForm1.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
if key=Vk_Escape then close; // выход
end;



end.
