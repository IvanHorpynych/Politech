unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons,printers,
  ComCtrls, ToolWin;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton4: TSpeedButton;
    SpeedButton3: TSpeedButton;
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Button3Click(Sender: TObject);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,Y: Integer);
    procedure SpeedButton1Click(Sender: TObject);
    procedure SpeedButton2Click(Sender: TObject);
    procedure SpeedButton3Click(Sender: TObject);
    procedure SpeedButton4Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TSceneGL;
    Mouse:T3DMouse;
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
  Scene:=TSceneGL.create; // Создаем сцену
  dolphin:=TEntity.create; //  Создаем дельфина,
  dolphin.SetColor(60,100,250); // устанавливаем цвет,
  dolphin.LoadDXF('..\dolphin.dxf',true); // загружаем из файла,
  dolphin.CalcNormals; // корректируем нормали,
  dolphin.Center; // помещаем в центр
  with dolphin do
  begin
    move(0,0,-10); // перемещаяе назад
  end;
  Scene.Entities.add(dolphin); // Добавим дельфина в сцену
  light:=Tlight.create(1); // Создаем освещение по умолчанию
  Scene.lights.add(light); // и добавляем его на сцену
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);

  Mouse:=T3DMouse.create(dolphin); // Создаем T3DMouse для нашего объекта
  Mouse.scale(1,1,0.1,1,1,1); // задаем скорость перемещения и поворота
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

procedure TForm1.Panel1MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
begin
  Mouse.move(x,y,shift); // обнавляем объект - дальфин в новых координатах
  Scene.Redraw;
end;

procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
  Mouse.Block(4,TspeedButton(sender).down); // блокировка поворота rx
end;

procedure TForm1.SpeedButton2Click(Sender: TObject);
begin
  Mouse.Block(5,TspeedButton(sender).down); // блокировка поворота ry
end;

procedure TForm1.SpeedButton3Click(Sender: TObject);
begin
  Mouse.Block(6,TspeedButton(sender).down); // блокировка поворота rz
end;

procedure TForm1.SpeedButton4Click(Sender: TObject);
begin
  Mouse.Block(3,TspeedButton(sender).down); // блокировка перемещения по z
end;


end.
