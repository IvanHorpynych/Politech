unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    GroupBox1: TGroupBox;
    Label11: TLabel;
    Label12: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    RadioGroup1: TRadioGroup;
    CheckBox1: TCheckBox;
    GroupBox2: TGroupBox;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    ScrollBar1: TScrollBar;
    ScrollBar2: TScrollBar;
    ScrollBar3: TScrollBar;
    ScrollBar7: TScrollBar;
    ScrollBar8: TScrollBar;
    ScrollBar9: TScrollBar;
    CheckBox2: TCheckBox;
    GroupBox3: TGroupBox;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    ScrollBar4: TScrollBar;
    ScrollBar5: TScrollBar;
    ScrollBar6: TScrollBar;
    Button2: TButton;
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,Y: Integer);
    procedure ScrollBar4Change(Sender: TObject);
    procedure ScrollBar1Change(Sender: TObject);
    procedure ScrollBar7Change(Sender: TObject);
    procedure ScrollBar8Change(Sender: TObject);
    procedure CheckBox1Click(Sender: TObject);
    procedure RadioGroup1Click(Sender: TObject);
    procedure ScrollBar9Change(Sender: TObject);
    procedure CheckBox2Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TSceneGL;
    dolphin:TEntity;
    Mouse:T3DMouse;
    ColorsLocked:boolean;
  end;

var
  Form1: TForm1;
  rx,ry,rz:word;

implementation

{$R *.DFM}

procedure TForm1.FormCreate(Sender: TObject);
var
  light:Tlight;
begin
  Scene:=TSceneGL.create;
  dolphin:=TEntity.create;
  dolphin.SetColor(70,100,100);
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

procedure TForm1.ScrollBar4Change(Sender: TObject);
begin
// Выставляем значения background'а сцены
  scene.backR:=scrollbar4.position / 100;
  scene.backG:=scrollbar5.position / 100;
  scene.backB:=scrollbar6.position / 100;
  if ColorsLocked then
  begin
    scrollbar1.position:=scrollbar4.position;
    scrollbar2.position:=scrollbar5.position;
    scrollbar3.position:=scrollbar6.position;
  end;
  scene.redraw;
end;

procedure TForm1.ScrollBar1Change(Sender: TObject);
begin
// Устанавливаем значения компонент цвета тумана
  scene.fogcolor[0]:=scrollbar1.position/100;
  scene.fogcolor[1]:=scrollbar2.position/100;
  scene.fogcolor[2]:=scrollbar3.position/100;
  scene.fogcolor[3]:=1;
  scene.redraw;
end;

procedure TForm1.ScrollBar7Change(Sender: TObject);
begin
// Насыщенность тумана
  scene.fogDensity:=scrollbar7.position/100;
  scene.redraw;
end;

procedure TForm1.ScrollBar8Change(Sender: TObject);
begin
// Минимальная дистанция тумана
  scene.fogMinDist:=scrollbar8.position/100;
  scene.redraw;
end;

procedure TForm1.ScrollBar9Change(Sender: TObject);
begin
// Максимальная дистанция тумана
  scene.fogMaxDist:=scrollbar9.position/100;
  scene.redraw;
end;


procedure TForm1.CheckBox1Click(Sender: TObject);
begin
// Включаем - выключаем туман
  scene.fogEnabled:=checkbox1.checked;
end;

procedure TForm1.RadioGroup1Click(Sender: TObject);
begin
// Тип визуализации тумана
  case radioGroup1.itemindex of
    0:scene.fogType:=Gl_Linear;
    1:scene.fogType:=GL_exp;
    2:scene.fogType:=GL_exp2;
  end;
  scene.redraw;
end;

procedure TForm1.CheckBox2Click(Sender: TObject);
begin
  ColorsLocked:=checkbox2.checked;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
// Параметры тумана для дельфина в Амазонке :)
  checkbox1.checked:=true;
  checkbox2.checked:=true;
  dolphin.rotate(-112, 3, 74);
  dolphin.move(0, 0, -8.4);
  scrollbar4.position:=49;
  scrollbar5.position:=31;
  scrollbar6.position:=16;
  scrollbar7.position:=27;
  scene.redraw;
end;

end.
