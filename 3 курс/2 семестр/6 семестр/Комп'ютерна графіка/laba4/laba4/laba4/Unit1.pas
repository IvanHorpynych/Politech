unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, OpenGL, Math, ComCtrls;

type
  TForm1 = class(TForm)
    Image2: TImage;
    Panel1: TPanel;
    Panel2: TPanel;
    Button1: TButton;
    Timer1: TTimer;
    GroupBox1: TGroupBox;
    CheckBox1: TCheckBox;
    Label1: TLabel;
    Edit1: TEdit;
    GroupBox2: TGroupBox;
    GroupBox3: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    Edit2: TEdit;
    Edit3: TEdit;
    GroupBox4: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    Edit4: TEdit;
    Edit5: TEdit;
    Label6: TLabel;
    Edit6: TEdit;
    GroupBox5: TGroupBox;
    Label7: TLabel;
    GroupBox6: TGroupBox;
    Label8: TLabel;
    Label9: TLabel;
    Edit7: TEdit;
    Edit8: TEdit;
    GroupBox7: TGroupBox;
    Label10: TLabel;
    Label11: TLabel;
    Edit9: TEdit;
    Edit10: TEdit;
    Edit11: TEdit;
    Button2: TButton;
    Label12: TLabel;
    TrackBar1: TTrackBar;
    Label13: TLabel;
    TrackBar2: TTrackBar;
    GroupBox10: TGroupBox;
    Label14: TLabel;
    Edit13: TEdit;
    CheckBox6: TCheckBox;
    CheckBox5: TCheckBox;
    CheckBox7: TCheckBox;
    GroupBox8: TGroupBox;
    Edit12: TEdit;
    CheckBox2: TCheckBox;
    CheckBox3: TCheckBox;
    CheckBox4: TCheckBox;
    procedure FormMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image2Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Panel2MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Panel2MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Panel2MouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure CheckBox1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  DC:HDC;
  hrc:HGLRC;
  xmousedown:Boolean;
  rX,rY,rZ:Single;
  preX,preY:Integer;
  g:Extended;
  IsRot:Boolean;//Чи є обертання
  Inter:Integer;//Інтервал таймера в секундах
  VRot:Integer;//Обертання (градуси за секунду)
  VX1,//Швидкість точки 1 по вісі X
  VZ1,//Швидкість точки 1 по вісі Z
  VY1,//Швидкість точки 1 по вісі Y
  OX1,//Відхилення точки 1 по вісі X
  OZ1,//Відхилення точки 1 по вісі Z
  L1,//Довжина нитки підвісу точки 1
  VX2,//Швидкість точки 2 по вісі X
  VZ2,//Швидкість точки 2 по вісі Z
  VY2,//Швидкість точки 2 по вісі Y
  OX2,//Відхилення точки 2 по вісі X
  OZ2,//Відхилення точки 2 по вісі Z
  L2,//Довжина нитки підвісу точки 2
  X1,//к x1
  X2,//к x2
  Y1,//к y1
  Y2,//к y2
  Z1,//к z1
  Z2//к z2
  :Extended;

implementation

{$R *.dfm}

procedure SetDCPixelFormat;
var
  pfd:PixelFormatDescriptor;
  nPixelFormat:Integer;
begin
  FillChar(pfd,SizeOf(pfd),0);
  pfd.dwFlags:=PFD_DRAW_TO_WINDOW or
               PFD_SUPPORT_OPENGL or
               PFD_DOUBLEBUFFER;
  nPixelFormat:=ChoosePixelFormat(DC,@pfd);
  SetPixelFormat(DC,nPixelFormat,@pfd);
end;

procedure TForm1.FormMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
const
  SC_DRAGMOVE = $F012;
begin
  ReleaseCapture;
  Form1.Perform(WM_SYSCOMMAND, SC_DRAGMOVE, 0);
end;

procedure TForm1.Image2Click(Sender: TObject);
begin
  Form1.Close;
end;

procedure AllDisable;
begin
  Form1.Edit1.Enabled:=False;
  Form1.Edit2.Enabled:=False;
  Form1.Edit3.Enabled:=False;
  Form1.Edit4.Enabled:=False;
  Form1.Edit5.Enabled:=False;
  Form1.Edit6.Enabled:=False;
  Form1.Edit7.Enabled:=False;
  Form1.Edit8.Enabled:=False;
  Form1.Edit9.Enabled:=False;
  Form1.Edit10.Enabled:=False;
  Form1.Edit11.Enabled:=False;
  Form1.Edit12.Enabled:=False;
  Form1.Edit13.Enabled:=False;
end;

procedure AllEnable;
begin
  Form1.Edit1.Enabled:=True;
  Form1.Edit2.Enabled:=True;
  Form1.Edit3.Enabled:=True;
  Form1.Edit4.Enabled:=True;
  Form1.Edit5.Enabled:=True;
  Form1.Edit6.Enabled:=True;
  Form1.Edit7.Enabled:=True;
  Form1.Edit8.Enabled:=True;
  Form1.Edit9.Enabled:=True;
  Form1.Edit10.Enabled:=True;
  Form1.Edit11.Enabled:=True;
  Form1.Edit12.Enabled:=True;
  Form1.Edit13.Enabled:=True;
end;

procedure LoadParams;
begin
  VX1:=StrToFloat(Form1.Edit2.Text);
  VZ1:=StrToFloat(Form1.Edit3.Text);
  OX1:=StrToFloat(Form1.Edit4.Text);
  OZ1:=StrToFloat(Form1.Edit5.Text);
  L1:=StrToFloat(Form1.Edit6.Text);
  VX2:=StrToFloat(Form1.Edit7.Text);
  VZ2:=StrToFloat(Form1.Edit8.Text);
  OX2:=StrToFloat(Form1.Edit9.Text);
  OZ2:=StrToFloat(Form1.Edit10.Text);
  L2:=StrToFloat(Form1.Edit11.Text);
  Inter:=StrToInt(Form1.Edit12.Text);
  VRot:=StrToInt(Form1.Edit1.Text);
  g:=StrToInt(Form1.Edit13.Text)/10;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  if Button1.Caption='Старт' then
  begin
    AllDisable;
    LoadParams;
    Button1.Caption:='Стоп';
    DC:=GetDC(Panel2.Handle);
    SetDCPixelFormat;
    hrc:=wglCreateContext(DC);
    wglMakeCurrent(DC,hrc);
    glClearColor(0,0,0.3,0);
    Timer1.Enabled:=True;
    Timer1.Interval:=Inter;
    xmousedown:=False;
    glEnable(GL_LINE_SMOOTH);
    glEnable(GL_DEPTH_TEST);
    glLineWidth (1);
    glPointSize(10);
    glEnable(GL_POINT_SMOOTH);
    VY1:=0;
    VY2:=0;
  end else begin
    AllEnable;
    Timer1.Enabled:=False;
    Button1.Caption:='Старт';
    wglMakeCurrent(0,0);
    wglDeleteContext(hrc);
    ReleaseDC(Panel2.Handle,DC);
    DeleteDC(DC);

  end;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0,0);
  wglDeleteContext(hrc);
  ReleaseDC(Panel2.Handle,DC);
  DeleteDC(DC);
end;

function Lowly:Extended;
begin
  Result:=(100-Form1.TrackBar2.Position)/100;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
const
  U=2;
  A=20;
var
  ps:TPaintStruct;
  i:Integer;
  t:Extended;
begin
  if CheckBox1.Checked=True then IsRot:=True else IsRot:=False;
  if IsRot=True then rY:=rY+VRot*Inter/1000;
  t:=Inter/1000;
  BeginPaint(Panel2.Handle,ps);

  glClear(GL_COLOR_BUFFER_BIT or
          GL_DEPTH_BUFFER_BIT);
  glViewPort(0,0,Panel2.ClientWidth,Panel2.ClientHeight);
  glLoadIdentity;
  gluPerspective(30,Panel2.Width/Panel2.Height,1,1000);

  glTranslatef (0.0, -5.0, -TrackBar1.Position); // перенос-вісь Z
  glRotatef (rX, 1.0, 0.0, 0.0); // поворот-вісь X
  glRotatef (rY, 0.0, 1.0, 0.0); // поворот-вісь Y
  glRotatef (rZ, 0.0, 0.0, 1.0); // поворот-вісь Z

  //Розрахунок нового положення точок
  VX2:=VX2-VX1;
  VZ2:=VZ2-VZ1;

  VX1:=(VX1-Sin(DegToRad(OX1))*g*t);
  VZ1:=(VZ1-Sin(DegToRad(OZ1))*g*t);
  VX2:=(VX2-Sin(DegToRad(OX2))*g*t+VX1);
  VZ2:=(VZ2-Sin(DegToRad(OZ2))*g*t+VZ1);

  OX1:=(OX1+(VX1*t-Sin(DegToRad(OX1))*g*t*t/2)*Lowly);
  OZ1:=(OZ1+(VZ1*t-Sin(DegToRad(OZ1))*g*t*t/2)*Lowly);
  OX2:=(OX2+(VX2*t-Sin(DegToRad(OX2))*g*t*t/2)*Lowly);
  OZ2:=(OZ2+(VZ2*t-Sin(DegToRad(OZ2))*g*t*t/2)*Lowly);

  //Розрахунок декартових координат точок за полярними
  X1:=L1*Cos(DegToRad(OX1-90));
  Z1:=L1*Cos(DegToRad(OZ1-90));
  Y1:=10+L1*Sin(DegToRad(OX1-90)){+L1*Sin(DegToRad(OZ1-90))};
  X2:=X1+L2*Cos(DegToRad(OX2-90));
  Z2:=Z1+L2*Cos(DegToRad(OZ2-90));
  Y2:=Y1+L2*Sin(DegToRad(OX2-90)){*X2/Sqrt(X2*X2+Z2*Z2)+L2*Sin(DegToRad(OZ2-90))*Z2/Sqrt(X2*X2+Z2*Z2)};

  //Малюємо координатні вісі
  if CheckBox5.Checked=True then
  begin
    glColor3f(0.5,0.5,0.5);
    glBegin(GL_LINES);
    for i:=-Round(A/U) to Round(A/U) do
    begin
      glVertex3f(-A,0,i*U);
      glVertex3f(A,0,i*U);
      glVertex3f(i*U,0,-A);
      glVertex3f(i*U,0,A);
    end;
    glEnd;
  end;

  if CheckBox7.Checked=True then
  begin
    glColor3f(0.5,0.5,0.5);
    glBegin(GL_LINES);
    for i:=-Round(A/U) to Round(A/U) do
    begin
      glVertex3f(0,-A,i*U);
      glVertex3f(0,A,i*U);
      glVertex3f(0,i*U,-A);
      glVertex3f(0,i*U,A);
    end;
    glEnd;
  end;

  if CheckBox6.Checked=True then
  begin
    glColor3f(0.5,0.5,0.5);
    glBegin(GL_LINES);
    for i:=-Round(A/U) to Round(A/U) do
    begin
      glVertex3f(-A,i*U,0);
      glVertex3f(A,i*U,0);
      glVertex3f(i*U,-A,0);
      glVertex3f(i*U,A,0);
    end;
    glEnd;
  end;

  if CheckBox3.Checked=True then
  begin
    glColor3f(0,1,0);
    glBegin(GL_LINES);
      glVertex3f(0,-A,0);
      glVertex3f(0,A,0);
    glEnd;
  end;

  if CheckBox4.Checked=True then
  begin
    glColor3f(0,0,1);
    glBegin(GL_LINES);
      glVertex3f(0,0,-A);
      glVertex3f(0,0,A);
    glEnd;
  end;

  if CheckBox2.Checked=True then
  begin
    glColor3f(1,0,0);
    glBegin(GL_LINES);
      glVertex3f(-A,0,0);
      glVertex3f(A,0,0);
    glEnd;
  end;

  //Підвіси маятників
  glColor3f(1,0,1);
  glBegin(GL_LINES);
    glVertex3f(0,10,0);
    glVertex3f(X1,Y1,Z1);
  glEnd;
  glBegin(GL_LINES);
    glVertex3f(X1,Y1,Z1);
    glVertex3f(X2,Y2,Z2);
  glEnd;
  //Тіла на підвісах
  glColor3f(0,1,1);
  glBegin(GL_POINTS);
    glVertex3f(0,10,0);
    glVertex3f(X1,Y1,Z1);
    glVertex3f(X2,Y2,Z2);
  glEnd;

  SwapBuffers(DC);
  EndPaint(Panel2.Handle,ps);
end;

procedure TForm1.Panel2MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  xmousedown:=True;
  preX:=X;
  preY:=Y;
end;

procedure TForm1.Panel2MouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  xmousedown:=False;
end;

procedure TForm1.Panel2MouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
  if xmousedown=true then
  begin
    if preX<X then
    begin
      rY:=rY+2;
      preX:=X;
    end;
    if preX>X then
    begin
      rY:=rY-2;
      preX:=X;
    end;
  end;
end;

procedure TForm1.CheckBox1Click(Sender: TObject);
begin
  If IsRot=True then IsRot:=False else IsRot:=True;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  If Form1.Button1.Caption='Стоп' then
    if Timer1.Enabled=True then Timer1.Enabled:=False
    else Timer1.Enabled:=True;
end;

end.
