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
  cube:Tentity;
  Face:TFace;
  light:Tlight;
begin
  Scene:=TSceneGL.create; // Создаем новую сцену
  Cube:=TEntity.create; // Создаем пустой объект TEntity
  Cube.SetColor(90,200,150); // Инициируем цвета R,G,B
  Face:=cube.addFace;
  // Создаем 1-й face в исходном кубе  всего 6 граней ( в кубе )
    Face.AddVertex(1.0, 1.0, 1.0,0.0, 0.0, 1.0);  // добавляем 1-й  vertex
    Face.AddVertex(-1.0, 1.0, 1.0,0.0, 0.0, 1.0); // добавляем 2-й  vertex
    Face.AddVertex(-1.0, -1.0, 1.0,0.0, 0.0, 1.0);// добавляем 3-й  vertex
    Face.AddVertex(1.0, -1.0, 1.0,0.0, 0.0, 1.0);// добавляем 4-й  vertex

  Face:=cube.addFace;
    Face.AddVertex(1.0, 1.0, -1.0,0.0, 0.0, -1.0);
    Face.AddVertex(1.0, -1.0, -1.0,0.0, 0.0, -1.0);
    Face.AddVertex(-1.0, -1.0, -1.0,0.0, 0.0, -1.0);
    Face.AddVertex(-1.0, 1.0, -1.0,0.0, 0.0, -1.0);

  Face:=cube.addFace;
    Face.AddVertex(-1.0, 1.0, 1.0,-1.0, 0.0, 0.0);
    Face.AddVertex(-1.0, 1.0, -1.0,-1.0, 0.0, 0.0);
    Face.AddVertex(-1.0, -1.0, -1.0,-1.0, 0.0, 0.0);
    Face.AddVertex(-1.0, -1.0, 1.0,-1.0, 0.0, 0.0);

  Face:=cube.addFace;
    Face.AddVertex(1.0, 1.0, 1.0,1.0, 0.0, 0.0);
    Face.AddVertex(1.0, -1.0, 1.0,1.0, 0.0, 0.0);
    Face.AddVertex(1.0, -1.0, -1.0,1.0, 0.0, 0.0);
    Face.AddVertex(1.0, 1.0, -1.0,1.0, 0.0, 0.0);

  Face:=cube.addFace;
    Face.AddVertex(-1.0, 1.0, -1.0,0.0, 1.0, 0.0);
    Face.AddVertex(-1.0, 1.0, 1.0,0.0, 1.0, 0.0);
    Face.AddVertex(1.0, 1.0, 1.0,0.0, 1.0, 0.0);
    Face.AddVertex(1.0, 1.0, -1.0,0.0, 1.0, 0.0);

  Face:=cube.addFace;
    Face.AddVertex(-1.0, -1.0, -1.0,0.0, -1.0, 0.0);
    Face.AddVertex(1.0, -1.0, -1.0,0.0, -1.0, 0.0);
    Face.AddVertex(1.0, -1.0, 1.0,0.0, -1.0, 0.0);
    Face.AddVertex(-1.0, -1.0, 1.0,0.0, -1.0, 0.0);

  with cube do
  begin
    move(0,0,-15); // Перемещаем куб в координаты x, y, z
    Rotate(-30,-30,-30); // и поворачиваем на угол
  end;

  Scene.Entities.add(cube);// добавим куб на сцену

  light:=Tlight.create(1); // создадим источник света и
  Scene.lights.add(light); // добавим его на сцену

  Scene.InitRC(panel1.handle); // передадим Handle Panel1 нашей сцене,
                               // на ней будет происходить рендеринг
  Scene.UpdateArea(panel1.width,panel1.height);
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free; // очищаем сцену
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  inc(rx,1); if rx>360 then rx:=0;
  inc(ry,2); if ry>360 then ry:=0;
  dec(rz,1); if rz<0 then rz:=360;
  Tentity(Scene.Entities.Items[0]).Rotate(rx,ry,rz); // повернем куб
  Scene.Redraw;                                 // ... и обновим сцену
end;

procedure TForm1.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
if key=Vk_Escape then close; // выход
end;

end.
