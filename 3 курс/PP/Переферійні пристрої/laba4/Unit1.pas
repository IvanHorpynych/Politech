unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Button1: TButton;
    Button2: TButton;
    Panel1: TPanel;
    TrackBar1: TTrackBar;
    Timer1: TTimer;
    Label1: TLabel;
    Timer2: TTimer;
    Image4: TImage;
    Memo1: TMemo;
    Button3: TButton;
    Label2: TLabel;
    Label3: TLabel;
    procedure TrackBar1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  k1:boolean;
  k:integer;

implementation

{$R *.dfm}

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
Timer1.Interval:=trackbar1.Position;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
image1.Visible:=false;  
image2.Visible:=true;
timer1.Enabled:=true;
timer2.Enabled:=true;
k:=0;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
if k=0 then begin
k:=1;
image4.Visible:=false;
memo1.Lines.Add(label2.Caption);
panel1.Color:=clwindow;
label2.Caption:='Вимкнення Control';
end else if k=1 then begin
k:=0;
image4.Visible:=true;
memo1.Lines.Add(label2.Caption);
panel1.Color:=clred;
label2.Caption:='Ввімкнення Control';
end else if k=2 then begin
timer1.Enabled:=false;
image4.Visible:=false;
memo1.Lines.Add(label2.Caption);
panel1.Color:=clwindow;
label2.Caption:='Вимкнення Control';
end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
k:=0;
k1:=false;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin                  
memo1.Lines.Add(label2.Caption);
label2.Caption:='Зупинка роботи схеми';
memo1.Lines.Add(label2.Caption);
label2.Caption:='Ініціалізація РРІ';
memo1.Lines.Add(label2.Caption);
label2.Caption:='Ініціалізація РТ';
memo1.Lines.Add(label2.Caption);
label2.Caption:='Запис Gate0';
image1.Visible:=true;
image2.Visible:=false;
image3.Visible:=false;
timer2.Enabled:=false;
if k=1 then timer1.Enabled:=false else k:=2;
end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
if k1=false then begin     
memo1.Lines.Add(label2.Caption);
label2.Caption:='Вимкнення лічильника';
image2.Visible:=true;
image3.Visible:=false;
k1:=true;
end else begin                       
memo1.Lines.Add(label2.Caption);
label2.Caption:='Ввімкнення лічильника';
image2.Visible:=false;
image3.Visible:=true;
k1:=false;
end;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
memo1.Clear;
end;

end.
