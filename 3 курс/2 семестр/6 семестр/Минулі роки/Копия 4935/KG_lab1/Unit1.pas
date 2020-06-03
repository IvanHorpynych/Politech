unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OpenGL;

type
  TForm1 = class(TForm)
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormPaint(Sender: TObject);
    procedure FormResize(Sender: TObject);
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
// множество битовых флагов, определяющих устройство и интерфейс
  iPixelType := PFD_TYPE_RGBA; // режим для изображения цветов
  cColorBits := 16;            // число битовых плоскостей в каждом буфере цвета
  cDepthBits := 32;            // размер буфера глубины (ось z)
  iLayerType := PFD_MAIN_PLANE;// тип плоскости
  end;

  nPixelFormat := ChoosePixelFormat (hdc, @pfd); // запрос системе - поддерживается ли выбранный формат пикселей
  SetPixelFormat (hdc, nPixelFormat, @pfd);      // устанавливаем формат пикселей в контексте устройства
End;


procedure TForm1.FormCreate(Sender: TObject);
begin
  SetDCPixelFormat(Canvas.Handle);// устанавливаем формат пикселей
  hrc := wglCreateContext(Canvas.Handle);//создаём контекст воспроизведения
  wglMakeCurrent(Canvas.Handle, hrc);//устанавливаем контекст воспроизведения
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);   // освобождаем контекст воспроизведения
  wglDeleteContext (hrc); // удаление контекста OpenGL
end;

procedure TForm1.FormPaint(Sender: TObject);
var ps: PAINTSTRUCT;
begin
BeginPaint(Self.Handle,ps);



glClearColor(0.0, 1.0, 0.7, 1.0); // определение цвета фона
glClear(GL_COLOR_BUFFER_BIT);// установка цвета фона

glLoadIdentity;
glTranslatef(0.0, 0.0, -10.0);

glColor(0.0,1.0,0.0,1.0);
glLineWidth(10.0);

   glBegin(GL_LINES);//Изображаем линию
    glVertex2d(0.0, 0.0);
    glVertex2d(0.0, -2.5);
   glEnd;

glColor(1.0,1.0,0.0,1.0);
glLineWidth(5.0);

   glBegin(GL_POLYGON); //Изображаем многоугольник
    glVertex2d(0.5, 1.0);
    glVertex2d(1.0, 0.5);
    glVertex2d(0.5, 0.0);
    glVertex2d(-0.5, 0.0);
    glVertex2d(-1.0, 0.5);
    glVertex2d(-0.5, 1.0);
   glEnd;

glColor(1.0,0.0,0.0,1.0);
glLineWidth(5.0);

   glBegin(GL_QUADS); //Изображаем четырехугольник
    glVertex2d(1.3, 1.7);
    glVertex2d(0.5, 1.0);
    glVertex2d(1.0, 0.5);
    glVertex2d(1.8, 1.2);
   glEnd;

   glBegin(GL_QUADS); //Изображаем четырехугольник
    glVertex2d(-1.3, 1.7);
    glVertex2d(-0.5, 1.0);
    glVertex2d(-1.0, 0.5);
    glVertex2d(-1.8, 1.2);
   glEnd;

   glBegin(GL_QUADS); //Изображаем четырехугольник
    glVertex2d(-1.3, -0.8);
    glVertex2d(-0.5, 0.0);
    glVertex2d(-1.0, 0.5);
    glVertex2d(-1.8, -0.3);
   glEnd;

   glBegin(GL_QUADS); //Изображаем четырехугольник
    glVertex2d(1.3, -0.8);
    glVertex2d(0.5, 0.0);
    glVertex2d(1.0, 0.5);
    glVertex2d(1.8, -0.3);
   glEnd;

   glBegin(GL_QUADS); //Изображаем четырехугольник
    glVertex2d(-0.5, 2.0);
    glVertex2d(-0.5, 1.0);
    glVertex2d(0.5, 1.0);
    glVertex2d(0.5, 2.0);
   glEnd;

   glBegin(GL_QUADS); //Изображаем четырехугольник
    glVertex2d(-0.5, -1.0);
    glVertex2d(-0.5, 0.0);
    glVertex2d(0.5, 0.0);
    glVertex2d(0.5, -1.0);
   glEnd;



SwapBuffers(Canvas.Handle);
EndPaint(Self.Handle,ps);
end;

procedure TForm1.FormResize(Sender: TObject);
begin
glMatrixMode(GL_PROJECTION);//Текущей матрицей устанавливается матрица проекций
  glLoadIdentity;//Замена текущей матрицы единичной
  gluPerspective(30.0, Width/Height, 1.0, 15.0);//Определение усеченного конуса
  //видимости в видовой системе координат. Первые 2 параметры задают углы
  //видимости относительно осей х и у, а последние 2 параметра - ближнюю и дальнюю
  //границы видимости
  glViewport(0, 0, Width, Height);//Определение области видимости
  glMatrixMode(GL_MODELVIEW);//Текущей матрицей устанавливается видовая матрица
  InvalidateRect(Handle, nil, False);//Принудительная перерисовка формы
end;

end.
