unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OpenGL, Menus, StdCtrls;

type
  TForm1 = class(TForm)
    MainMenu1: TMainMenu;
    N1: TMenuItem;
    N2: TMenuItem;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormPaint(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure N2Click(Sender: TObject);
    procedure N1Click(Sender: TObject);
  private
    { Private declarations }
    hrc: HGLRC;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

//��������� ���������� ���� ��������� PIXELFORMATDESCRIPTOR
procedure SetDCPixelFormat (hdc : HDC);
var
 pfd : TPIXELFORMATDESCRIPTOR; // ���� ������� ������
 nPixelFormat : Integer;
Begin
With pfd do begin
  nSize := sizeof (TPIXELFORMATDESCRIPTOR); // ����� ���������
  nVersion := 1;                            // ����� ����
  dwFlags := PFD_DRAW_TO_WINDOW OR PFD_SUPPORT_OPENGL OR PFD_DOUBLEBUFFER;
// ������� ������ ���������, �� ���������� ������� �� ���������
  iPixelType := PFD_TYPE_RGBA; // ����� ��� ���������� �������
  cColorBits := 16;            // ����� ������ ������ � ������� ����� �������
  cDepthBits := 32;            // ����� ������ ������� (��� z)
  iLayerType := PFD_MAIN_PLANE;// ��� �������
  end;
  nPixelFormat := ChoosePixelFormat (hdc, @pfd);
  // ����� �� ������� - �� ����������� ������� ������ ������
  SetPixelFormat (hdc, nPixelFormat, @pfd);
  // ������������ ������ ������ � �������� ��������
End;

procedure TForm1.FormCreate(Sender: TObject);
begin
  SetDCPixelFormat(Canvas.Handle);// ������������ ������ ������
  hrc := wglCreateContext(Canvas.Handle);//��������� �������� ����������
  wglMakeCurrent(Canvas.Handle, hrc);//������������ �������� ����������
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);   // ��������� �������� ����������
  wglDeleteContext (hrc); // ��������� ��������� OpenGL
end;

procedure TForm1.FormPaint(Sender: TObject);
var ps: PAINTSTRUCT;
begin
BeginPaint(Self.Handle,ps);
glLoadIdentity;           //����� ������� ���������
glTranslatef(0.0, 0.0, -10.0);

{������ �������� ������� � OpenGL}
  glClearColor(0.1, 0.4, 0.0, 1.0); //���������� ������� ���� - �������
  glClear(GL_COLOR_BUFFER_BIT);     //������������ ������� ����
  glColor(0.0,0.0,0.7,1.0);         //������������ ���� ��������� - ����
  glPointSize(10);                  //������������ ����� �����
  glLineWidth(15.0);                //������������ ������ ��

   glBegin(GL_LINES);               //������ ���(��� ���)
    glVertex2d(-2.8, 1.5);
    glVertex2d(-1.9, 1.5);
   glEnd;

   glBegin(GL_LINES);               //������ ���(����� ���)
    glVertex2d(0.4, 1.5);
    glVertex2d(1.7, 1.5);
   glEnd;

  glColor(0.8,0.0,0.0,1.0);         //������������ ���� ��������� - ��������
   glBegin(GL_LINES);               //������ ��(�������)
    glVertex2d(-3.8, 1.6);
    glVertex2d(-2.3, -2.5);
   glEnd;

   glBegin(GL_LINES);               //������ ��(�������)
    glVertex2d(-2.3, -2.5);
    glVertex2d(2.0, -2.5);
   glEnd;

   glBegin(GL_LINES);               //������ ��(�������)
    glVertex2d(2.0, -2.5);
    glVertex2d(3.1, 1.6);
   glEnd;

  glColor(1.0,1.0,0.0,1.0);         //������������ ���� ��������� - ������
   glBegin(GL_TRIANGLES);           //������ ���������
    glVertex2d(-0.7, 1.6);
    glVertex2d(-1.8, -1.5);
    glVertex2d(1.3, -1.5);
   glEnd;



SwapBuffers(Canvas.Handle);
EndPaint(Self.Handle,ps);
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  glMatrixMode(GL_PROJECTION);  //����� �������� �������������� ������� ��������
  glLoadIdentity;               //����� ���� ������� ���������
  gluPerspective(30.0, Width/Height, 1.0, 15.0);
  //���������� �������� ������ �������� � ������ ������ ���������. ����� 2 ���������
  //������� ���� �������� ������� ���� � �� �, � ������ 2 ��������� - ������ ��
  //������ ������� ���������
  glViewport(0, 0, Width, Height);    //���������� ������ ��������
  glMatrixMode(GL_MODELVIEW);         //����� �������� �������������� ������ �������
  InvalidateRect(Handle, nil, False); //�������������� ����������� �����
end;

procedure TForm1.N2Click(Sender: TObject);
begin
close;
end;

procedure TForm1.N1Click(Sender: TObject);
begin
showmessage('����� ��������: ������� ����� ��-92 �������� ������� ���������');
end;

end.
