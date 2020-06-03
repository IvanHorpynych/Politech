unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    Panel4: TPanel;

    Label5: TLabel;
    Label6: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label17: TLabel;
    Label18: TLabel;
    Label19: TLabel;
    Label20: TLabel;
    Label21: TLabel;
    Label22: TLabel;
    Label23: TLabel;
    Label24: TLabel;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton3: TSpeedButton;
    SpeedButton4: TSpeedButton;
    SpeedButton5: TSpeedButton;
    SpeedButton6: TSpeedButton;
    Button2: TButton;
    Panel3: TPanel;
    Panel5: TPanel;
    Label4: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    Label25: TLabel;
    Label26: TLabel;
    Label27: TLabel;
    Label28: TLabel;
    Label29: TLabel;
    Label30: TLabel;
    Label31: TLabel;
    Label32: TLabel;
    Label33: TLabel;
    Label34: TLabel;
    Label35: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Button1: TButton;
    Button3: TButton;
    Procedure FormCreate(Sender: TObject);
    Procedure FormClose(Sender: TObject; var Action: TCloseAction);
    Procedure Button2Click(Sender: TObject);
    Procedure Panel1MouseMove(Sender: TObject; ShIft: TShIftState; X,
      Y: Integer);
    Procedure SpeedButton1Click(Sender: TObject);
    Procedure SpeedButton2Click(Sender: TObject);
    Procedure SpeedButton3Click(Sender: TObject);
    Procedure SpeedButton4Click(Sender: TObject);
    Procedure Panel1MouseUp(Sender: TObject; Button: TMouseButton;
      ShIft: TShIftState; X, Y: Integer);
    Procedure Label28Click(Sender: TObject);
    Procedure SpeedButton5Click(Sender: TObject);
    Procedure SpeedButton6Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TsceneGL;
    mouse:T3dMouse;
    Dolphin:Tentity;
    ColorActual:LongInt; // Цвет для закраски
  end;

var
  Form1: TForm1;
  rx,ry,rz:word;

implementation

{$R *.DFM}

Procedure TForm1.FormCreate(Sender: TObject);
var
  luz:Tlight;
begin
  Scene:=TsceneGL.create; // создаем сцену и дельфина ...

  Dolphin:=Tentity.create;
  Dolphin.SetColor(96,96,168);
  Dolphin.LoadDXF('..\dolphin.dxf',true);
  Dolphin.CalcNormals;
  Dolphin.Center;
  Dolphin.Center;
  with Dolphin do
  begin
    Move(0,0,-10);
    id:=1; // присваиваем объекту номер 1
  end;
  Scene.Entities.add(Dolphin);
  luz:=Tlight.create(1);

  Scene.Lights.add(luz);
  Scene.InitRC(panel1.handle);
  Scene.UpdateArea(panel1.width,panel1.height);
  // Создаем 3D Mouse и привязываем к нашему объекту
  mouse:=T3dMouse.create(Dolphin);
  mouse.scale(0.1,-0.1,0.1,1,1,1);
  ColorActual:=0;
end;

Procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

Procedure TForm1.Button2Click(Sender: TObject);
begin
// Перемещение или поворот
  If mouse.mode=1 then
  begin
    mouse.mode:=3;
    button2.caption:='Поворот';
  end
    else
  begin
    mouse.mode:=1;
    button2.caption:='Движение';
  end
end;


Procedure TForm1.Panel1MouseMove(Sender: TObject; ShIft: TShIftState; X,Y: Integer);
begin
  mouse.Move(x,y,shIft); // Перемещаем наш объект и
  Scene.Redraw;          // обновляем сцену
end;


Procedure TForm1.Panel1MouseUp(Sender: TObject; Button: TMouseButton;
  ShIft: TShIftState; X, Y: Integer);
var
  point:Tvertex;
  poly,vert,i:LongInt;
  iR,iG,iB:Byte;
begin
  mouse.FindVertex(x,panel1.height-y,Scene,point);
  for i:=1 to numFound do
  begin
    poly:=VertexHits[i] shr 16;
    vert:=VertexHits[i] mod 65536;
    If (poly<Dolphin.Faces.count) and (vert<Tface(Dolphin.Faces.Items[poly]).Vertices.count) then
      begin
        ir:=(ColorActual      ) mod 256;
        ig:=(ColorActual shr 8) mod 256;
        ib:=(ColorActual shr 16) mod 256;
        Tvertex(Tface(Dolphin.Faces.Items[poly]).Vertices.items[vert]).point.setcolor(ir,ig,ib);
      end;
  end;
  label17.caption:=intTostr(xxx);
  label18.caption:=intTostr(yyy);
  label5.caption:=intTostr(x);
  label6.caption:=intTostr(y);
  label21.caption:=intTostr(x-xxx);
  label22.caption:=intTostr(y-yyy);
  If numFound=0 then
  begin
    label17.caption:='  ';
    label18.caption:='  ';
    label21.caption:='  ';
    label22.caption:='  ';
  end;
  label11.caption:=intTostr(numfound);
end;


Procedure TForm1.Label28Click(Sender: TObject);
begin
  ColorActual:=Tlabel(sender).color; // Выбираем цвет закраски
  label29.color:=colorActual;
end;

// Запрещаем-разрешаем перемещение по отдельным осям

Procedure TForm1.SpeedButton5Click(Sender: TObject);
begin
  mouse.Block(1,TspeedButton(sender).down);
end;

Procedure TForm1.SpeedButton6Click(Sender: TObject);
begin
  mouse.Block(2,TspeedButton(sender).down);
end;

Procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
  mouse.Block(4,TspeedButton(sender).down);
end;

Procedure TForm1.SpeedButton2Click(Sender: TObject);
begin
  mouse.Block(5,TspeedButton(sender).down);
end;

Procedure TForm1.SpeedButton3Click(Sender: TObject);
begin
  mouse.Block(6,TspeedButton(sender).down);
end;

Procedure TForm1.SpeedButton4Click(Sender: TObject);
begin
  mouse.Block(3,TspeedButton(sender).down);
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
Dolphin.Save('dolphin.3do'); // Сохраняем объект
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
Dolphin.Load('dolphin.3do'); // Загружаем объект
scene.redraw;
end;

end.
