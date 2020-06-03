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
    Panel4: TPanel;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton4: TSpeedButton;
    SpeedButton3: TSpeedButton;
    SpeedButton5: TSpeedButton;
    SpeedButton6: TSpeedButton;
    Button2: TButton;
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Button2Click(Sender: TObject);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure SpeedButton1Click(Sender: TObject);
    procedure SpeedButton2Click(Sender: TObject);
    procedure SpeedButton3Click(Sender: TObject);
    procedure SpeedButton4Click(Sender: TObject);
    procedure SpeedButton5Click(Sender: TObject);
    procedure SpeedButton6Click(Sender: TObject);
    procedure RadioGroup1Click(Sender: TObject);
    procedure RadioGroup2Click(Sender: TObject);
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
  vertex:Tvertex;
begin
  image1.picture.loadFromFile('texture.bmp');
  Scene:=TsceneGL.create;
  Scene.InitRC(panel1.handle);
  Scene.Texturing:=true; // включаем поддержку текстур на сцене
  thing:=Tentity.create; // создаем пустой объект
  thing.CreateTexture;   // добавим текстуры на объект
  thing.texture.LoadTexture('texture.bmp');
  thing.texture.automatic:=False;

  thing.Load('simplest.3dO');   {load a triangle and a rectangle, this is no thing}
  thing.Move(0,0,-10);
  thing.id:=1;
  with thing do
  begin
    Tface(thing.faces.items[2]).ApplyTexture:=false; // Внутренние поверхности не
    Tface(thing.faces.items[3]).ApplyTexture:=false; // текстурированы

    // Треугольник
    vertex:=Tvertex(Tface(thing.faces.items[1]).vertices.items[0]);// x,y,z =0,0,0
    vertex.tx:=1;
    vertex.tz:=0;

    vertex:=Tvertex(Tface(thing.faces.items[1]).vertices.items[1]);// x,y,z =0,2,0
    vertex.tx:=0;
    vertex.tz:=1;

    vertex:=Tvertex(Tface(thing.faces.items[1]).vertices.items[2]);// x,y,z =0,0,2
    vertex.tx:=1;
    vertex.tz:=1;
    // Квадрат
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[0]);// x,y,z =2,0,0
    vertex.tx:=1;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[1]);// x,y,z =0,0,0
    vertex.tx:=0;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[2]);// x,y,z =0,0,2
    vertex.tx:=0;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(thing.faces.items[0]).vertices.items[3]);// x,y,z =2,0,2
    vertex.tx:=1;
    vertex.tz:=1;
  end;

  Scene.Entities.add(thing);
  light:=Tlight.create(1);
  Scene.Lights.add(light);
  Scene.UpdateArea(panel1.width,panel1.height);

  mouse:=T3dMouse.create(thing);
  mouse.scale(0.1,-0.1,0.1,1,1,1);
end;

procedure TForm1.RadioGroup1Click(Sender: TObject);
begin
  case RadioGroup1.itemindex of
    0:thing.Texture.MagFilter:=GL_linear; //  текстурные фильтры
    1:thing.Texture.MagFilter:=GL_nearest;//  использование  GL_linear без Hardware поддержки OpenGL очень медленно
  end;
  Scene.redraw;
end;

procedure TForm1.RadioGroup2Click(Sender: TObject);
begin
  case RadioGroup2.itemindex of
    0:thing.Texture.MinFilter:=GL_linear; //  текстурные фильтры
    1:thing.Texture.MinFilter:=GL_nearest;//  использование  GL_linear без Hardware поддержки OpenGL очень медленно
  end;
  Scene.redraw;
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

