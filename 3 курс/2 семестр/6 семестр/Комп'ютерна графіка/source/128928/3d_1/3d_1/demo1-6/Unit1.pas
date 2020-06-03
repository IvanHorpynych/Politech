unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons,printers;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton4: TSpeedButton;
    SpeedButton3: TSpeedButton;
    SpeedButton5: TSpeedButton;
    Button2: TButton;
    Label4: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,Y: Integer);
    procedure SpeedButton1Click(Sender: TObject);
    procedure SpeedButton2Click(Sender: TObject);
    procedure SpeedButton3Click(Sender: TObject);
    procedure SpeedButton4Click(Sender: TObject);
    procedure SpeedButton5Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Panel1MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Panel1MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TSceneGL;
    dolphin:TEntity;
    Mouse:T3DMouse;
    frames:integer; // Кол-во кадров в сек
    time1,time2:Tdatetime;
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
  frames:=0; // Обнуляем
  Scene:=TSceneGL.create;
  dolphin:=TEntity.create;
  dolphin.SetColor(120,170,210);
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
var
  i:integer;
begin
  Mouse.move(x, y, shift);
  Scene.Redraw;
  frames:=frames+1; // При перемещении добавляем кадр
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
  Scene.Redraw;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  printer.BeginDoc; // Начинаем печать
  printer.Canvas.CopyRect(panel1.clientrect, canvas, panel1.clientrect);
  printer.EndDoc; // и заканчиваем
end;

procedure TForm1.Panel1MouseDown(Sender: TObject; Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
begin
  time1:=time;// засекаем время
  frames:=0; // сбрасываем таймер
end;

procedure TForm1.Panel1MouseUp(Sender: TObject; Button: TMouseButton;Shift: TShiftState; X, Y: Integer);
var
  difference:Tdatetime;
  hour,minute,second,mseconds:word;
  miliseconds:longint;
  Frames_Per_Second,floFrames,FloMSec:double;
begin
  time2:=time;
  // Вычисление FPS
  difference:=time2-time1;
  decodetime(difference,hour,minute,second,mseconds);
  miliseconds:=mseconds+longint(second)*1000+longint(minute)*60000;
  floFrames:=frames;
  FloMSec:=miliseconds;
  Frames_Per_Second:=FloFrames/(FloMSec/1000);
  label4.caption:='FPS '+FloatToStrF(Frames_Per_Second, ffGeneral, 4,2);
end;






end.
