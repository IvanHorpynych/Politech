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
  Scene:=TSceneGL.create; // ������� �����
  dolphin:=TEntity.create; //  ������� ��������,
  dolphin.SetColor(60,100,250); // ������������� ����,
  dolphin.LoadDXF('..\dolphin.dxf',true); // ��������� �� �����,
  dolphin.CalcNormals; // ������������ �������,
  dolphin.Center; // �������� � �����
  with dolphin do
  begin
    move(0,0,-10); // ���������� �����
  end;
  Scene.Entities.add(dolphin); // ������� �������� � �����
  light:=Tlight.create(1); // ������� ��������� �� ���������
  Scene.lights.add(light); // � ��������� ��� �� �����
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);

  Mouse:=T3DMouse.create(dolphin); // ������� T3DMouse ��� ������ �������
  Mouse.scale(1,1,0.1,1,1,1); // ������ �������� ����������� � ��������
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

procedure TForm1.Panel1MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
begin
  Mouse.move(x,y,shift); // ��������� ������ - ������� � ����� �����������
  Scene.Redraw;
end;

procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
  Mouse.Block(4,TspeedButton(sender).down); // ���������� �������� rx
end;

procedure TForm1.SpeedButton2Click(Sender: TObject);
begin
  Mouse.Block(5,TspeedButton(sender).down); // ���������� �������� ry
end;

procedure TForm1.SpeedButton3Click(Sender: TObject);
begin
  Mouse.Block(6,TspeedButton(sender).down); // ���������� �������� rz
end;

procedure TForm1.SpeedButton4Click(Sender: TObject);
begin
  Mouse.Block(3,TspeedButton(sender).down); // ���������� ����������� �� z
end;


end.
