unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OpenGL, Menus, ExtCtrls, StdCtrls, ComCtrls;

type
  TPoint3D = record
  x,y,z : double;
  end;

  TForm1 = class(TForm)
    MainMenu1: TMainMenu;
    N1: TMenuItem;
    N3: TMenuItem;
    N2: TMenuItem;
    Timer1: TTimer;
    Panel1: TPanel;
    Button1: TButton;
    TrackBar1: TTrackBar;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    TrackBar2: TTrackBar;
    TrackBar3: TTrackBar;
    TrackBar4: TTrackBar;
    Label5: TLabel;
    TrackBar5: TTrackBar;
    N4: TMenuItem;
    N5: TMenuItem;
    N6: TMenuItem;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    N7: TMenuItem;
    N8: TMenuItem;
    Button5: TButton;
    N9: TMenuItem;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormPaint(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure N3Click(Sender: TObject);
    procedure N2Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
    procedure TrackBar2Change(Sender: TObject);
    procedure TrackBar3Change(Sender: TObject);
    procedure TrackBar4Change(Sender: TObject);
    procedure TrackBar5Change(Sender: TObject);
    procedure N5Click(Sender: TObject);
    procedure N6Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure N7Click(Sender: TObject);
    procedure N8Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure N9Click(Sender: TObject);
  private
    { Private declarations }
    hrc: HGLRC;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  DC : HDC;
  ps : TPaintStruct;
  p:array[1..12] of TPoint3D;
  a1,a2:integer;
  b,cc,t:boolean;
  t1,t2,t3,t4:real;

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
  DC := GetDC(Handle);
  glEnable(GL_BLEND);//разрешаем смешивание
   glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);//указываем способ смешивания
  a1:=1;
  t:=true;
  cc:=false;
  t1:=1;
  t2:=0;
  t3:=0;
  t4:=1;
  b:=false;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  wglMakeCurrent(0, 0);   // звільняємо контекст відтворення
  wglDeleteContext (hrc); // видалення контексту OpenGL
end;

procedure TForm1.FormPaint(Sender: TObject);
var ps: PAINTSTRUCT;
i:integer;
k:real;
begin
  BeginPaint(Handle, ps);
  randomize;
  // очищення буфера кольору та буфера глибини

  if cc then  glClearColor(0.0,0.0,0.0,0.0) else   glClearColor(1.0,1.0,1.0,1.0);
  glClear(GL_COLOR_BUFFER_BIT or GL_DEPTH_BUFFER_BIT);
  glColor4d(t1,t2,t3,t4);
  // тривимірність
  glLoadIdentity;
  glTranslatef(-4 , 0, -15.0); //Зміщуємо точку перегляду на 15 одиниць назад
//  glRotatef(10,0,1,0);        //Обертаємо сцену відносно спостерігача на 30
                              //градусів навколо вісі У.
  glRotatef(a1,1,0,0);

//Виводимо зображення букви С - передній план
  glColor4d(t1,t2,t3,t4);
  p[1].x:=1.25; p[1].y:=1.75;  p[1].z:=0;
  p[2].x:=1.25; p[2].y:=1.5;   p[2].z:=0;
  p[3].x:=0.75; p[3].y:=1.5;   p[3].z:=0;
  p[4].x:=0.5;  p[4].y:=1.25;  p[4].z:=0;
  p[5].x:=0.5;  p[5].y:=1;     p[5].z:=0;
  p[6].x:=0.75; p[6].y:=0.75;  p[6].z:=0;
  p[7].x:=1.25; p[7].y:=0.75;  p[7].z:=0;
  p[8].x:=1.25; p[8].y:=0.5;   p[8].z:=0;
  p[9].x:=0.5;  p[9].y:=0.5;   p[9].z:=0;
  p[10].x:=0.25;p[10].y:=0.75; p[10].z:=0;
  p[11].x:=0.25;p[11].y:=1.5;  p[11].z:=0;
  p[12].x:=0.5; p[12].y:=1.75; p[12].z:=0;
  glBegin(GL_POLYGON);
    glVertex3d(p[3].x,p[3].y,p[3].z);
    glVertex3d(p[2].x,p[2].y,p[2].z);
    glVertex3d(p[1].x,p[1].y,p[1].z);
    glVertex3d(p[12].x,p[12].y,p[12].z);
    glVertex3d(p[4].x,p[4].y,p[4].z);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(p[4].x,p[4].y,p[4].z);
    glVertex3d(p[5].x,p[5].y,p[5].z);
    glVertex3d(p[9].x,p[9].y,p[9].z);
    glVertex3d(p[10].x,p[10].y,p[10].z);
    glVertex3d(p[11].x,p[11].y,p[11].z);
    glVertex3d(p[12].x,p[12].y,p[12].z);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(p[6].x,p[6].y,p[6].z);
    glVertex3d(p[7].x,p[7].y,p[7].z);
    glVertex3d(p[8].x,p[8].y,p[8].z);
    glVertex3d(p[9].x,p[9].y,p[9].z);
    glVertex3d(p[5].x,p[5].y,p[5].z);
  glEnd;
//Виводимо зображення букви С - задній план
glColor4d(t1,t2,t3,t4);
  for i:=1 to 12 do p[i].z:=0.2;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(p[3].x,p[3].y,p[3].z);
    glVertex3d(p[2].x,p[2].y,p[2].z);
    glVertex3d(p[1].x,p[1].y,p[1].z);
    glVertex3d(p[12].x,p[12].y,p[12].z);
    glVertex3d(p[4].x,p[4].y,p[4].z);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(p[4].x,p[4].y,p[4].z);
    glVertex3d(p[5].x,p[5].y,p[5].z);
    glVertex3d(p[9].x,p[9].y,p[9].z);
    glVertex3d(p[10].x,p[10].y,p[10].z);
    glVertex3d(p[11].x,p[11].y,p[11].z);
    glVertex3d(p[12].x,p[12].y,p[12].z);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(p[6].x,p[6].y,p[6].z);
    glVertex3d(p[7].x,p[7].y,p[7].z);
    glVertex3d(p[8].x,p[8].y,p[8].z);
    glVertex3d(p[9].x,p[9].y,p[9].z);
    glVertex3d(p[5].x,p[5].y,p[5].z);
  glEnd;
//Виводимо зображення букви С - бокові квадрати
for i:=1 to 11 do begin
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_QUADS);
    glVertex3d(p[i].x,p[i].y,p[i].z);
    glVertex3d(p[i].x,p[i].y,0);
    glVertex3d(p[i+1].x,p[i+1].y,0);
    glVertex3d(p[i+1].x,p[i+1].y,p[i+1].z);
  glEnd; end;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_QUADS);
    glVertex3d(p[12].x,p[12].y,p[12].z);
    glVertex3d(p[12].x,p[12].y,0);
    glVertex3d(p[1].x,p[1].y,0);
    glVertex3d(p[1].x,p[1].y,p[1].z);
  glEnd;

//Виводимо зображення букви М
glColor4d(t1,t2,t3,t4);
  k:=0;
  glBegin(GL_POLYGON);
    glVertex3d(2, 0.5, k);
    glVertex3d(2, 1.75, k);
    glVertex3d(1.7, 1.75, k);
    glVertex3d(1.7, 0.5, k);
    glVertex3d(2, 0.5, k);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2, 1.75, k);
    glVertex3d(1.7, 1.75, k);
    glVertex3d(2.2, 1, k);
    glVertex3d(2.5, 1, k);
    glVertex3d(2, 1.75, k);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(3.0, 1.75, k);
    glVertex3d(2.7, 1.75, k);
    glVertex3d(2.2, 1, k);
    glVertex3d(2.5, 1, k);
    glVertex3d(3.0, 1.75, k);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.7, 0.5, k);
    glVertex3d(2.7, 1.75, k);
    glVertex3d(3.0, 1.75, k);
    glVertex3d(3.0, 0.5, k);
    glVertex3d(2.7, 0.5, k);
  glEnd;
  { Передній план букви М}
  k:=0.2;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2, 0.5, k);
    glVertex3d(2, 1.75, k);
    glVertex3d(1.7, 1.75, k);
    glVertex3d(1.7, 0.5, k);
    glVertex3d(2, 0.5, k);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2, 1.75, k);
    glVertex3d(1.7, 1.75, k);
    glVertex3d(2.2, 1, k);
    glVertex3d(2.5, 1, k);
    glVertex3d(2, 1.75, k);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(3.0, 1.75, k);
    glVertex3d(2.7, 1.75, k);
    glVertex3d(2.2, 1, k);
    glVertex3d(2.5, 1, k);
    glVertex3d(3.0, 1.75, k);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.7, 0.5, k);
    glVertex3d(2.7, 1.75, k);
    glVertex3d(3.0, 1.75, k);
    glVertex3d(3.0, 0.5, k);
    glVertex3d(2.7, 0.5, k);
  glEnd;
  { Бокові стінки}
  glBegin(GL_POLYGON);
    glVertex3d(2, 0.5, 0);
    glVertex3d(2, 1.75, 0);
    glVertex3d(2, 1.75, 0.2);
    glVertex3d(2, 0.5, 0.2);
    glVertex3d(2, 0.5, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(1.7, 1.75, 0);
    glVertex3d(1.7, 0.5, 0);
    glVertex3d(1.7, 0.5, 0.2);
    glVertex3d(1.7, 1.75, 0.2);
    glVertex3d(1.7, 1.75, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(3, 0.5, 0);
    glVertex3d(3, 1.75, 0);
    glVertex3d(3, 1.75, 0.2);
    glVertex3d(3, 0.5, 0.2);
    glVertex3d(3, 0.5, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.7, 1.75, 0);
    glVertex3d(2.7, 0.5, 0);
    glVertex3d(2.7, 0.5, 0.2);
    glVertex3d(2.7, 1.75, 0.2);
    glVertex3d(2.7, 1.75, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2, 1.75, 0);
    glVertex3d(1.7, 1.75, 0);
    glVertex3d(1.7, 1.75, 0.2);
    glVertex3d(2, 1.75, 0.2);
    glVertex3d(2, 1.75, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(3, 1.75, 0);
    glVertex3d(2.7, 1.75, 0);
    glVertex3d(2.7, 1.75, 0.2);
    glVertex3d(3, 1.75, 0.2);
    glVertex3d(3, 1.75, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2, 0.5, 0);
    glVertex3d(1.7, 0.5, 0);
    glVertex3d(1.7, 0.5, 0.2);
    glVertex3d(2, 0.5, 0.2);
    glVertex3d(2, 0.5, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(3, 0.5, 0);
    glVertex3d(2.7, 0.5, 0);
    glVertex3d(2.7, 0.5, 0.2);
    glVertex3d(3, 0.5, 0.2);
    glVertex3d(3, 0.5, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.2, 1, 0);
    glVertex3d(2.5, 1, 0);
    glVertex3d(2.5, 1, 0.2);
    glVertex3d(2.2, 1, 0.2);
    glVertex3d(2.2, 1, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.7, 1.75, 0);
    glVertex3d(2.2, 1, 0);
    glVertex3d(2.2, 1, 0.2);
    glVertex3d(2.7, 1.75, 0.2);
    glVertex3d(2.7, 1.75, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.5, 1, 0);
    glVertex3d(3.0, 1.75, 0);
    glVertex3d(3.0, 1.75, 0.2);
    glVertex3d(2.5, 1, 0.2);
    glVertex3d(2.5, 1, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(1.7, 1.75, 0);
    glVertex3d(2.2, 1, 0);
    glVertex3d(2.2, 1, 0.2);
    glVertex3d(1.7, 1.75, 0.2);
    glVertex3d(1.7, 1.75, 0);
  glEnd;
  if b then glColor4d(random(100)/3,random(100)/3,random(100)/3,random(100)/3);
  glBegin(GL_POLYGON);
    glVertex3d(2.5, 1, 0);
    glVertex3d(2, 1.75, 0.2);
    glVertex3d(2, 1.75, 0.2);
    glVertex3d(2.5, 1, 0.2);
    glVertex3d(2.5, 1, 0);
  glEnd;





  SwapBuffers(DC);   // кінець роботи
  EndPaint(Handle, ps);
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

procedure TForm1.N3Click(Sender: TObject);
begin
Close;
end;

procedure TForm1.N2Click(Sender: TObject);
begin
showmessage('Автор програми: студент групи КВ-92 Степанюк Михайло Федорович');
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
 if t=true then
 a1 := round(a1 + 1) mod 180;//изменение угла поворота сцены от 0 до 360
 if t=false then
 a1 := round(a1 - 1) mod 180;//изменение угла поворота сцены от 0 до 360
 if a1=0 then t:=true;
 if a1=179 then t:=false;
 InvalidateRect(Handle,nil,False);//принудительная перерисовка окна
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
timer1.Enabled:=true;
end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
timer1.Interval:=trackbar1.Position;
end;

procedure TForm1.TrackBar2Change(Sender: TObject);
begin
t1:=trackbar2.Position/100;
end;

procedure TForm1.TrackBar3Change(Sender: TObject);
begin
t2:=trackbar3.Position/100;
end;

procedure TForm1.TrackBar4Change(Sender: TObject);
begin
t3:=trackbar4.Position/100;
end;

procedure TForm1.TrackBar5Change(Sender: TObject);
begin
t4:=trackbar5.Position/100;
end;

procedure TForm1.N5Click(Sender: TObject);
begin
timer1.Enabled:=true;
end;

procedure TForm1.N6Click(Sender: TObject);
begin
panel1.Visible:=not(panel1.Visible);
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
timer1.Enabled:=false;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
close;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
panel1.Visible:=false;
end;

procedure TForm1.N7Click(Sender: TObject);
begin
timer1.Enabled:=false;
end;

procedure TForm1.N8Click(Sender: TObject);
begin
b:=not(b);
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
cc:=not(cc);
end;

procedure TForm1.N9Click(Sender: TObject);
begin
cc:=not(cc);
end;

end.
