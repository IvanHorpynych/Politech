unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OpenGL, ExtCtrls;

type
  TPoint3D = record//�������� ��������� ��� �������� ����� � ������������
  x,y,z : double;
  end;

  TForm1 = class(TForm)
    Timer1: TTimer;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormPaint(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure drawQuad(p1, p2, p3: TPoint3D);
  private
    { Private declarations }
    hrc: HGLRC;
  public
    { Public declarations }
  end;


var
  Form1: TForm1;
  DC : HDC;
  angel: real;
  ps : TPaintStruct;
  p1,p2,p3,p4: TPoint3D;

implementation

{$R *.dfm}

{ TForm1 }

procedure SetDCPixelFormat(hdc: HDC);
var
 pfd : TPIXELFORMATDESCRIPTOR; // ������ ������� ��������
 nPixelFormat : Integer;
Begin
With pfd do begin
  nSize := sizeof (TPIXELFORMATDESCRIPTOR); // ������ ���������
  nVersion := 1;                            // ����� ������
  dwFlags := PFD_DRAW_TO_WINDOW OR PFD_SUPPORT_OPENGL OR PFD_DOUBLEBUFFER;
// ��������� ������� ������, ������������ ���������� � ���������
  iPixelType := PFD_TYPE_RGBA; // ����� ��� ����������� ������
  cColorBits := 16;            // ����� ������� ���������� � ������ ������ �����
  cDepthBits := 32;            // ������ ������ ������� (��� z)
  iLayerType := PFD_MAIN_PLANE;// ��� ���������
  end;

  nPixelFormat := ChoosePixelFormat (hdc, @pfd); // ������ ������� - �������������� �� ��������� ������ ��������
  SetPixelFormat (hdc, nPixelFormat, @pfd);      // ������������� ������ �������� � ��������� ����������
End;



procedure TForm1.drawQuad(p1, p2, p3: TPoint3D);
begin
    glBegin(GL_QUADS);
    glVertex3d(p1.x,p1.y,p1.z);
    glVertex3d(p2.x,p2.y,p2.z);
    glVertex3d(p3.x,p3.y,p3.z);
    glVertex3d(p1.x+p3.x-p2.x,p1.y+p3.y-p2.y,p1.z+p3.z-p2.z);
   glEnd;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  SetDCPixelFormat(Canvas.Handle);// ������������� ������ ��������
  hrc := wglCreateContext(Canvas.Handle);//������ �������� ���������������
  wglMakeCurrent(Canvas.Handle, hrc);//������������� �������� ���������������
  DC := GetDC(Handle);
  angel := 0;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);   // ����������� �������� ���������������
  wglDeleteContext (hrc); // �������� ��������� OpenGL
end;

procedure TForm1.FormPaint(Sender: TObject);
begin
  p1.x := 1;  p1.y := 1;   p1.z := 1;
  p2.x := -1.25; p2.y := 0;   p2.z := 1;
  p3.x := -1.25; p3.y := 2.5; p3.z := 1;
  p4.x := -1.25; p4.y := 0;   p4.z := 2.5;

  BeginPaint(Handle, ps);
  // ������� ������ ����� � ������ �������
  glClearColor(1.0,1.0,1.0,1.0);
  glClear(GL_COLOR_BUFFER_BIT or GL_DEPTH_BUFFER_BIT);//������� ����� � ����� �������
  glColor(1.0,1.0,0.0);
  // ������������
  glLoadIdentity;
  glTranslatef(0 , 0, -15.0);//���������� ����� ���������� �� 15 ������ �����
  glRotatef(30,0,1,0);//������� ����� ������������ ����������� �� 30 �������� ������
//��� �.
glRotatef(angel,0,1,0);
//������� ����������� 3-� ���������
    glColor(1.0,0.0,0.0);
    drawQuad(p1,p2,p3);
    glColor(0.0,1.0,0.0);
    //drawQuad(p3,p2,p4);
    glColor(0.0,0.0,1.0);
    //drawQuad(p4,p3,p2);

  SwapBuffers(DC);   // ����� ������
  EndPaint(Handle, ps);

end;

procedure TForm1.FormResize(Sender: TObject);
begin
glMatrixMode(GL_PROJECTION);//������� �������� ��������������� ������� ��������
  glLoadIdentity;//������ ������� ������� ���������
  gluPerspective(30.0, Width/Height, 1.0, 15.0);//����������� ���������� ������
  //��������� � ������� ������� ���������. ������ 2 ��������� ������ ����
  //��������� ������������ ���� � � �, � ��������� 2 ��������� - ������� � �������
  //������� ���������
  glViewport(0, 0, Width, Height);//����������� ������� ���������
  glMatrixMode(GL_MODELVIEW);//������� �������� ��������������� ������� �������
  InvalidateRect(Handle, nil, False);//�������������� ����������� �����
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
 angel := round(angel + 1) mod 360;//��������� ���� �������� ����� �� 0 �� 360
 InvalidateRect(Handle,nil,False);//�������������� ����������� ����

end;

end.
