unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel3: TPanel;
    Image1: TImage;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    RadioGroup1: TRadioGroup;
    RadioGroup2: TRadioGroup;
    Label13: TLabel;
    ScrollBar1: TScrollBar;
    Label14: TLabel;
    Label15: TLabel;
    Label16: TLabel;
    Button4: TButton;
    Panel4: TPanel;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton4: TSpeedButton;
    SpeedButton3: TSpeedButton;
    SpeedButton5: TSpeedButton;
    SpeedButton6: TSpeedButton;
    Button2: TButton;
    procedure FormCreate(Sender: TObject);
    // Запускается после установки Transparency
    procedure FormCreate_partII;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Button2Click(Sender: TObject);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,Y: Integer);
    procedure SpeedButton1Click(Sender: TObject);
    procedure SpeedButton2Click(Sender: TObject);
    procedure SpeedButton3Click(Sender: TObject);
    procedure SpeedButton4Click(Sender: TObject);
    procedure SpeedButton5Click(Sender: TObject);
    procedure SpeedButton6Click(Sender: TObject);
    procedure RadioGroup1Click(Sender: TObject);
    procedure RadioGroup2Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TsceneGL;
    mouse:T3dMouse;
    thing:Tentity;
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
  image1.picture.loadFromFile('texture.bmp');
  Scene:=TsceneGL.create;
  Scene.InitRC(panel1.handle);
  Scene.Texturing:=true;
  light:=Tlight.create(1);
  Scene.Lights.add(light);
  thing:=Tentity.create;
  thing.CreateTexture;
end;

procedure TForm1.FormCreate_partII;
var
  vertex:Tvertex;
begin
  thing.texture.EnvironmentMode:=GL_decal; // Установка transparent  texturing
  thing.texture.envblendcolor[3]:=scrollbar1.position;

  thing.texture.LoadTexture('texture.bmp');
  thing.texture.automatic:=false;
  thing.Load('simplest.3dO');
  with thing do
  begin
    Move(0,0,-10);
    id:=1;
    Tface(thing.faces.items[2]).ApplyTexture:=false;
    Tface(thing.faces.items[3]).ApplyTexture:=false;
    vertex:=Tvertex(Tface(thing.faces.items[1]).vertices.items[0]);
    vertex.tx:=1;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(thing.faces.items[1]).vertices.items[1]);
    vertex.tx:=0;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(thing.faces.items[1]).vertices.items[2]);
    vertex.tx:=1;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[0]);
    vertex.tx:=1;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[1]);
    vertex.tx:=0;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[2]);
    vertex.tx:=0;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[3]);
    vertex.tx:=1;
    vertex.tz:=1;
  end;
  Scene.Entities.add(thing);
  Tentity(Scene.Entities.Items[0]).Rotate(-86,0,35);
  Scene.UpdateArea(panel1.width,panel1.height);

  mouse:=T3dMouse.create(thing);
  mouse.scale(0.1,-0.1,0.1,1,1,1);
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
  // после установки значения
  Panel1.Enabled:=True;
  button4.enabled:=False;
  FormCreate_partII;
  scene.redraw;
end;


procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  if mouse.mode=1 then
  begin
    mouse.mode:=3;
    button2.caption:='Поворот';
  end
    else
  begin
    mouse.mode:=1;
    button2.caption:='Перемещение';
  end
end;

procedure TForm1.Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
  mouse.Move(x,y,shift);
  Scene.Redraw;
end;

procedure TForm1.RadioGroup1Click(Sender: TObject);
begin
  case RadioGroup1.itemindex of
    0:thing.Texture.MagFilter:=GL_linear;
    1:thing.Texture.MagFilter:=GL_nearest;
  end;
  Scene.redraw;
end;

procedure TForm1.RadioGroup2Click(Sender: TObject);
begin
  case RadioGroup2.itemindex of
    0:thing.Texture.MinFilter:=GL_linear;
    1:thing.Texture.MinFilter:=GL_nearest;
  end;
  Scene.redraw;
end;

procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
  mouse.Block(4,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton2Click(Sender: TObject);
begin
  mouse.Block(5,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton3Click(Sender: TObject);
begin
  mouse.Block(6,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton4Click(Sender: TObject);
begin
  mouse.Block(3,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton5Click(Sender: TObject);
begin
  mouse.Block(1,TspeedButton(sender).down);
end;

procedure TForm1.SpeedButton6Click(Sender: TObject);
begin
  mouse.Block(2,TspeedButton(sender).down);
end;

end.

