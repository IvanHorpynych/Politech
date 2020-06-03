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

//Процедура заповнення полів структури PIXELFORMATDESCRIPTOR
procedure SetDCPixelFormat (hdc : HDC);
var
 pfd : TPIXELFORMATDESCRIPTOR; // данні формату пікселів
 nPixelFormat : Integer;
Begin
With pfd do begin
  nSize := sizeof (TPIXELFORMATDESCRIPTOR); // розмір структури
  nVersion := 1;                            // номер версії
  dwFlags := PFD_DRAW_TO_WINDOW OR PFD_SUPPORT_OPENGL OR PFD_DOUBLEBUFFER;
// множина бітових прапорців, що визначають пристрій та інтерфейс
  iPixelType := PFD_TYPE_RGBA; // режим для зображення кольорів
  cColorBits := 16;            // число бітових площин в кожному буфері кольору
  cDepthBits := 32;            // розмір буфера глибини (вісь z)
  iLayerType := PFD_MAIN_PLANE;// тип площини
  end;
  nPixelFormat := ChoosePixelFormat (hdc, @pfd);
  // запит до системи - чи підтримується обраний формат пікселів
  SetPixelFormat (hdc, nPixelFormat, @pfd);
  // встановлюємо формат пікселів в контексті пристрою
End;

procedure TForm1.FormCreate(Sender: TObject);
begin
  SetDCPixelFormat(Canvas.Handle);// встановлюємо формат пікселів
  hrc := wglCreateContext(Canvas.Handle);//створюємо контекст відтворення
  wglMakeCurrent(Canvas.Handle, hrc);//встановлюємо контекст відтворення
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);   // звільняємо контекст відтворення
  wglDeleteContext (hrc); // видалення контексту OpenGL
end;

procedure TForm1.FormPaint(Sender: TObject);
var ps: PAINTSTRUCT;
begin
BeginPaint(Self.Handle,ps);
glLoadIdentity;           //Заміна матриці одиничною
glTranslatef(0.0, 0.0, -10.0);

{Власне побудова малюнка в OpenGL}
  glClearColor(0.1, 0.4, 0.0, 1.0); //Визначення кольору фону - зелений
  glClear(GL_COLOR_BUFFER_BIT);     //Встановлення кольору фону
  glColor(0.0,0.0,0.7,1.0);         //Встановлюємо колір малювання - синій
  glPointSize(10);                  //Встановлюємо розмір точки
  glLineWidth(15.0);                //Встановлюємо ширину лінії

   glBegin(GL_LINES);               //Будуємо лінію(ліве око)
    glVertex2d(-2.8, 1.5);
    glVertex2d(-1.9, 1.5);
   glEnd;

   glBegin(GL_LINES);               //Будуємо лінію(праве око)
    glVertex2d(0.4, 1.5);
    glVertex2d(1.7, 1.5);
   glEnd;

  glColor(0.8,0.0,0.0,1.0);         //Встановлюємо колір малювання - червоний
   glBegin(GL_LINES);               //Будуємо лінії(обличчя)
    glVertex2d(-3.8, 1.6);
    glVertex2d(-2.3, -2.5);
   glEnd;

   glBegin(GL_LINES);               //Будуємо лінії(обличчя)
    glVertex2d(-2.3, -2.5);
    glVertex2d(2.0, -2.5);
   glEnd;

   glBegin(GL_LINES);               //Будуємо лінії(обличчя)
    glVertex2d(2.0, -2.5);
    glVertex2d(3.1, 1.6);
   glEnd;

  glColor(1.0,1.0,0.0,1.0);         //Встановлюємо колір малювання - жовтий
   glBegin(GL_TRIANGLES);           //Будуємо трикутник
    glVertex2d(-0.7, 1.6);
    glVertex2d(-1.8, -1.5);
    glVertex2d(1.3, -1.5);
   glEnd;



SwapBuffers(Canvas.Handle);
EndPaint(Self.Handle,ps);
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  glMatrixMode(GL_PROJECTION);  //Даною матрицею встановлюється матриця проекцій
  glLoadIdentity;               //Заміна даної матриці одиничною
  gluPerspective(30.0, Width/Height, 1.0, 15.0);
  //Визначення зрізаного конуса видимості у видовій системі координат. Перші 2 параметри
  //задають кути видимості відносно вісей х та у, а останні 2 параметри - ближню та
  //дальню границі видимости
  glViewport(0, 0, Width, Height);    //Визначення області видимості
  glMatrixMode(GL_MODELVIEW);         //Даною матрицею встановлюється видова матриця
  InvalidateRect(Handle, nil, False); //Принудительная перерисовка формы
end;

procedure TForm1.N2Click(Sender: TObject);
begin
close;
end;

procedure TForm1.N1Click(Sender: TObject);
begin
showmessage('Автор програми: студент групи КВ-92 Степанюк Михайло Федорович');
end;

end.
