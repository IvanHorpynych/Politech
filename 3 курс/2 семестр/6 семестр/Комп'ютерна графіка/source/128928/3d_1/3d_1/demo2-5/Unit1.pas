unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  GL, GLU, StdCtrls, ExtCtrls,UrickGL, U3Dpolys, Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel3: TPanel;
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
    Button5: TButton;
    Button7: TButton;
    Button6: TButton;
    Button8: TButton;
    Button9: TButton;
    Image1: TImage;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);

    procedure FormCreate_partII;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure RadioGroup1Click(Sender: TObject);
    procedure RadioGroup2Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure ScrollBar1Change(Sender: TObject);
    procedure Button7Click(Sender: TObject);
    procedure SetTextOnScreenTo(st:string);
    procedure Button6Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure Button9Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    Scene:TsceneGL;
    mouse:T3dMouse;
    computer:Tentity;
    Procedure Place_all_the_objects;
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

Procedure Tform1.Place_all_the_objects;
var
  vertex:Tvertex;
begin
  computer:=Tentity.create;
  computer.LoadDXF('..\computer.dxf',true);
      computer.CreateTexture;
      with computer.texture do
      begin
        LoadTexture('texture.bmp');
        automatic:=false;
        envblendcolor[3]:=255;
      end;
  Computer.id:=1;
  Tface(computer.faces.items[0]).ApplyTexture:=True;
  Tface(computer.faces.items[1]).ApplyTexture:=True;
  with computer do
  begin
    vertex:=Tvertex(Tface(faces.items[1]).vertices.items[0]);
    vertex.tx:=1;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(faces.items[1]).vertices.items[1]);
    vertex.tx:=0;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(faces.items[1]).vertices.items[2]);
    vertex.tx:=0;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(faces.items[0]).vertices.items[0]);
    vertex.tx:=1;
    vertex.tz:=1;
    vertex:=Tvertex(Tface(faces.items[0]).vertices.items[1]);
    vertex.tx:=1;
    vertex.tz:=0;
    vertex:=Tvertex(Tface(faces.items[0]).vertices.items[2]);
    vertex.tx:=0;
    vertex.tz:=0;
  end;
  computer.setcolor(150,150,150);
  computer.Move(0.3, 0,-9.6);
  computer.Rotate(-79, -2, 50);
  computer.center;
  computer.CalcNormals;
  Scene.Entities.add(computer);
  Button4Click(nil);
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  light:Tlight;
begin
  image1.picture.loadFromFile('texture.bmp');
  Scene:=TsceneGL.create;
  Scene.InitRC(panel1.handle);
  Scene.Texturing:=true;

  Place_all_the_objects;

  light:=Tlight.create(1);
  Scene.Lights.add(light);

  Scene.UpdateArea(panel1.width,panel1.height);

  mouse:=T3dMouse.create(computer);
  mouse.scale(0.1,-0.1,0.1,1,1,1);

  FormCreate_partII;
end;

procedure TForm1.FormCreate_partII;
begin
  if computer.textured then
     computer.deletetexture; // удаляем старую текстуру
  // и создаем новую
  computer.CreateTexture;
  computer.texture.EnvironmentMode:=GL_decal;
  computer.texture.envblendcolor[3]:=scrollbar1.position;
  computer.texture.LoadTexture('texture.bmp');
  case RadioGroup1.itemindex of
    0:begin
        computer.Texture.MagFilter:=GL_linear;
      end;
    1:begin
        computer.Texture.MagFilter:=GL_nearest;
      end;
  end;
  case RadioGroup2.itemindex of
    0:begin
        computer.Texture.MinFilter:=GL_linear;
      end;
    1:begin
        computer.Texture.MinFilter:=GL_nearest;
      end;
  end;
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Scene.free;
end;

procedure TForm1.Panel1MouseMove(Sender: TObject; Shift: TShiftState; X,Y: Integer);
begin
  mouse.Move(x,y,shift);
  Scene.Redraw;
  application.processmessages;
end;

procedure TForm1.RadioGroup1Click(Sender: TObject);
begin
  case RadioGroup1.itemindex of
    0:begin
        computer.Texture.MagFilter:=GL_linear;
      end;
    1:begin
        computer.Texture.MagFilter:=GL_nearest;
      end;
  end;
  Scene.redraw;
end;

procedure TForm1.RadioGroup2Click(Sender: TObject);
begin
  case RadioGroup2.itemindex of
    0:begin
        computer.Texture.MinFilter:=GL_linear;
      end;
    1:begin
        computer.Texture.MinFilter:=GL_nearest;
      end;
  end;
  Scene.redraw;
end;

procedure TForm1.Button4Click(Sender: TObject);
var
  i:integer;
begin
// изменяем цвет на красный
  with computer.points do
  begin
    for i:=4 to 25 do
    begin
      with Tpoint(items[i]) do
      begin
        r:=210;
        g:=0;
        b:=0;
      end;
    end;
  end;
  scene.redraw;
end;

procedure TForm1.Button5Click(Sender: TObject);
var
  i:integer;
begin
// изменяем цвет на зеленый
  with computer.points do
  begin
    for i:=4 to 25 do
      with Tpoint(items[i]) do
      begin
        r:=0;
        g:=210;
        b:=0;
      end;
  end;
  scene.redraw;
end;

procedure TForm1.ScrollBar1Change(Sender: TObject);
begin
  FormCreate_partII;
  scene.redraw;
end;

procedure TForm1.SetTextOnScreenTo(st:string);
var
  dest, source, cuadro:trect;
begin
// Создаем BMP файл с цифрами полученными в st:string
  with image1.Canvas do
  begin
    font.name:='Arial';
    font.height:=38;
    font.style:=[fsBold];
    font.pixelsperinch:=72;
    TextOut(2,0,st);
    dest.left:=0;
    dest.right:=255;
    dest.top:=30;
    dest.bottom:=220;
    source.left:=0;
    source.right:=255;
    source.top:=0;
    source.bottom:=40;
    CopyRect(Dest, image1.canvas, Source);

    cuadro.left:=0;
    cuadro.right:=255;
    cuadro.top:=0;
    cuadro.bottom:=40;
    fillrect(cuadro);
  end;
  image1.picture.saveToFile('texture.bmp');
  FormCreate_partII;
  scene.redraw;
end;

// Передаем надпись на кнопке процедуре SetTextOnScreenTo
procedure TForm1.Button7Click(Sender: TObject);
begin
  SetTextOnScreenTo(Tbutton(sender).caption);
end;

procedure TForm1.Button6Click(Sender: TObject);
begin
  SetTextOnScreenTo(Tbutton(sender).caption);
end;

procedure TForm1.Button8Click(Sender: TObject);
begin
  SetTextOnScreenTo(Tbutton(sender).caption);
end;

procedure TForm1.Button9Click(Sender: TObject);
begin
  SetTextOnScreenTo(Tbutton(sender).caption);
end;

end.

