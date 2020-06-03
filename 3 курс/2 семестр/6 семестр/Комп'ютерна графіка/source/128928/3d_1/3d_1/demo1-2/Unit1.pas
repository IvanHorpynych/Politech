unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys;

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
  dolphin:TEntity;
  light:Tlight;
begin
  Scene:=TSceneGL.create;           // Создаем новую сцену
  dolphin:=TEntity.create;          // и объект дельфин
  dolphin.setcolor(102,102,153);    // Устанавливаем цвет у дельфина
  dolphin.LoadDXF('..\dolphin.dxf',true); // Загружаем дельфина из DXF файла

  with dolphin do
  begin
    move(0,0,-15);  // переместим немного назад
    rotate(-30,-30,-30); // и повернем
  end;
  Scene.entities.add(dolphin); // добавим дельфина на сцену

  Light:=Tlight.create(1);

  Scene.lights.add(light);
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);
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
  // повернем дельфина и перересуем сцену
  TEntity(Scene.entities.Items[0]).rotate(rx,ry,rz);
  Scene.Redraw;
end;

procedure TForm1.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
if key=Vk_Escape then close; // выход
end;

end.
