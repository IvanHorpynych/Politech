unit lab3;

interface

uses
  Windows, Messages, Forms, OpenGL, Graphics, Classes, ExtCtrls, SysUtils;

type
  TForm1 = class(TForm)
    Timer1: TTimer;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);

  private
    hrc: HGLRC;
    DC: HDC;
    Position : Array [0..3] of GLfloat;
    Angel: double;
    Bitmap: TBitmap;
    Bits: Array [0..63, 0..63, 0..2] of GLubyte;
    procedure SetDCPixelFormat;
    procedure BmpTexture;
  protected
    procedure WMPaint(var Msg: TWMPaint); message WM_PAINT;
  end;

var
  Form1: TForm1;

implementation

{$R *.DFM}

procedure TForm1.WMPaint(var Msg: TWMPaint);
var ps: TPaintStruct;
begin
  BeginPaint(Handle, ps);
  glClearColor(0.0,0.0,0.0,1.0);
  glClear(GL_COLOR_BUFFER_BIT or GL_DEPTH_BUFFER_BIT);

  glColor(0.5,0.5,0.5);
  glLoadIdentity;
  gluLookAt(10,10,10,0,0,0,0,0,1);

  glRotatef(Angel,1,0,0);

  
  glBegin(GL_QUADS);
    glNormal3f(1.5, 0.0, 0.0);

    glTexCoord2d(0.0, 1.0);
    glVertex3d(1.5,1.5,1.5);
    glTexCoord2d(1.0, 1.0);
    glVertex3d(1.5,1.5,-1.5);
    glTexCoord2d(1.0, 0.0);
    glVertex3d(1.5,-1.5,-1.5);
    glTexCoord2d(0.0, 0.0);
    glVertex3d(1.5,-1.5,1.5);
  glEnd;

  glBegin(GL_QUADS);
    glNormal3f(0.0, 1.5, 0.0);
    glTexCoord2d(0.0, 1.0);
    glVertex3d(1.5,1.5,1.5);
    glTexCoord2d(1.0, 1.0);
    glVertex3d(1.5,1.5,-1.5);
    glTexCoord2d(1.0, 0.0);
    glVertex3d(-1.5,1.5,-1.5);
    glTexCoord2d(0.0, 0.0);
    glVertex3d(-1.5,1.5,1.5);
  glEnd;

  glBegin(GL_QUADS);
    glNormal3f(0.0, 0.0, 1.5);
    glTexCoord2d(0.0, 1.0);
    glVertex3d(1.5,1.5,1.5);
    glTexCoord2d(1.0, 1.0);
    glVertex3d(-1.5,1.5,1.5);
    glTexCoord2d(1.0, 0.0);
    glVertex3d(-1.5,-1.5,1.5);
    glTexCoord2d(0.0, 0.0);
    glVertex3d(1.5,-1.5,1.5);
  glEnd;

  

  Position[1] := sin(Angel/180*pi)*10;
  Position[2] := cos(Angel/180*pi)*10;
  Caption := 'X=' + FloatToStrF(Position[0],fffixed, 6,1)+
             '  Y=' + FloatToStrF(Position[1],fffixed, 6,1)+
             '  Z=' + FloatToStrF(Position[2],fffixed, 6,1);;
  glLightfv(GL_LIGHT0, GL_POSITION, @Position);

  SwapBuffers(DC);   // конец работы
  EndPaint(Handle, ps);
end;
{Установка формата пикселей}
procedure TForm1.SetDCPixelFormat;
var
  nPixelFormat: Integer;
  pfd: TPixelFormatDescriptor;

begin
  FillChar(pfd, SizeOf(pfd), 0);

  with pfd do begin
    nSize     := sizeof(pfd);
    nVersion  := 1;
    dwFlags   := PFD_DRAW_TO_WINDOW or
                 PFD_SUPPORT_OPENGL or
                 PFD_DOUBLEBUFFER;
    iPixelType:= PFD_TYPE_RGBA;
    cColorBits:= 24;
    cDepthBits:= 32;
    iLayerType:= PFD_MAIN_PLANE;
  end;

  nPixelFormat := ChoosePixelFormat(DC, @pfd);
  SetPixelFormat(DC, nPixelFormat, @pfd);
                     glRotatef(Angel,1,0,0);

end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  DC := GetDC(Handle);
  SetDCPixelFormat;
  hrc := wglCreateContext(DC);
  wglMakeCurrent(DC, hrc);

  Angel := 0;

  glEnable(GL_LIGHTING); // включаем источник света
  glEnable(GL_LIGHT0);   // активируем источник света №0
  glEnable(GL_DEPTH_TEST);
  glEnable(GL_COLOR_MATERIAL);
  Position[0] := 3.0;
  Position[1] := 10.0;
  Position[2] := 10.0;
  Position[3] := 1.0;
  BmpTexture;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);
  wglDeleteContext(hrc);
  ReleaseDC(Handle, DC);
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity;
  gluPerspective(30.0, Width / Height, 1.0, 25.0);
  glViewport(0, 0, Width, Height);
  glMatrixMode(GL_MODELVIEW);
  InvalidateRect(Handle, nil, False);
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
 if angel > 360 then angel := 0
                else angel := angel + 2;
 InvalidateRect(Handle,nil,false);
end;

procedure TForm1.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
 Timer1.Enabled := not Timer1.Enabled;
end;

procedure TForm1.BmpTexture;
var
  i, j: Integer;
begin
   bitmap := TBitmap.Create;
   bitmap.LoadFromFile('image1.bmp');
   
    For i := 0 to 63 do
      For j := 0 to 63 do begin
        bits [i, j, 0] := GetRValue(bitmap.Canvas.Pixels[i,j]);
        bits [i, j, 1] := GetGValue(bitmap.Canvas.Pixels[i,j]);
        bits [i, j, 2] := GetBValue(bitmap.Canvas.Pixels[i,j]);
    end;

    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA,
                 64, 64,     // здесь задается размер текстуры
                 0, GL_RGB, GL_UNSIGNED_BYTE, @Bits);
    glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);
    glEnable(GL_TEXTURE_2D);
end;

end.
