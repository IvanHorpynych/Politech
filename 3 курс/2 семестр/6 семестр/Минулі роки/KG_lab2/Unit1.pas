unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OpenGL, ExtCtrls;

type
  TPoint3D = record//ќписание структуры дл€ хранени€ точки в пространстве
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
 pfd : TPIXELFORMATDESCRIPTOR; // данные формата пикселей
 nPixelFormat : Integer;
Begin
With pfd do begin
  nSize := sizeof (TPIXELFORMATDESCRIPTOR); // размер структуры
  nVersion := 1;                            // номер версии
  dwFlags := PFD_DRAW_TO_WINDOW OR PFD_SUPPORT_OPENGL OR PFD_DOUBLEBUFFER;
// множество битовых флагов, определ€ющих устройство и интерфейс
  iPixelType := PFD_TYPE_RGBA; // режим дл€ изображени€ цветов
  cColorBits := 16;            // число битовых плоскостей в каждом буфере цвета
  cDepthBits := 32;            // размер буфера глубины (ось z)
  iLayerType := PFD_MAIN_PLANE;// тип плоскости
  end;

  nPixelFormat := ChoosePixelFormat (hdc, @pfd); // запрос системе - поддерживаетс€ ли выбранный формат пикселей
  SetPixelFormat (hdc, nPixelFormat, @pfd);      // устанавливаем формат пикселей в контексте устройства
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
  SetDCPixelFormat(Canvas.Handle);// устанавливаем формат пикселей
  hrc := wglCreateContext(Canvas.Handle);//создаЄм контекст воспроизведени€
  wglMakeCurrent(Canvas.Handle, hrc);//устанавливаем контекст воспроизведени€
  DC := GetDC(Handle);
  angel := 0;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);   // освобождаем контекст воспроизведени€
  wglDeleteContext (hrc); // удаление контекста OpenGL
end;

procedure TForm1.FormPaint(Sender: TObject);
begin
  p1.x := 1;  p1.y := 1;   p1.z := 1;
  p2.x := -1.25; p2.y := 0;   p2.z := 1;
  p3.x := -1.25; p3.y := 2.5; p3.z := 1;
  p4.x := -1.25; p4.y := 0;   p4.z := 2.5;

  BeginPaint(Handle, ps);
  // очистка буфера цвета и буфера глубины
  glClearColor(1.0,1.0,1.0,1.0);
  glClear(GL_COLOR_BUFFER_BIT or GL_DEPTH_BUFFER_BIT);//ќчищаем также и буфер глубины
  glColor(1.0,1.0,0.0);
  // трехмерность
  glLoadIdentity;
  glTranslatef(0 , 0, -15.0);//ќтодвигаем точку наблюдени€ на 15 единиц назад
  glRotatef(30,0,1,0);//¬ращаем сцену относительно наблюдател€ на 30 градусов вокруг
//оси ”.
glRotatef(angel,0,1,0);
//¬ыводим изображени€ 3-х квадратов
    glColor(1.0,0.0,0.0);
    drawQuad(p1,p2,p3);
    glColor(0.0,1.0,0.0);
    //drawQuad(p3,p2,p4);
    glColor(0.0,0.0,1.0);
    //drawQuad(p4,p3,p2);

  SwapBuffers(DC);   // конец работы
  EndPaint(Handle, ps);

end;

procedure TForm1.FormResize(Sender: TObject);
begin
glMatrixMode(GL_PROJECTION);//“екущей матрицей устанавливаетс€ матрица проекций
  glLoadIdentity;//«амена текущей матрицы единичной
  gluPerspective(30.0, Width/Height, 1.0, 15.0);//ќпределение усеченного конуса
  //видимости в видовой системе координат. ѕервые 2 параметры задают углы
  //видимости относительно осей х и у, а последние 2 параметра - ближнюю и дальнюю
  //границы видимости
  glViewport(0, 0, Width, Height);//ќпределение области видимости
  glMatrixMode(GL_MODELVIEW);//“екущей матрицей устанавливаетс€ видова€ матрица
  InvalidateRect(Handle, nil, False);//ѕринудительна€ перерисовка формы
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
 angel := round(angel + 1) mod 360;//изменение угла поворота сцены от 0 до 360
 InvalidateRect(Handle,nil,False);//принудительна€ перерисовка окна

end;

end.
