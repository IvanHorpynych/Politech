unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    Timer1: TTimer;
    TrackBar1: TTrackBar;
    Label1: TLabel;
    Memo1: TMemo;
    Button1: TButton;
    Label2: TLabel;
    Button4: TButton;
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image8: TImage;
    Image9: TImage;
    Panel1: TPanel;
    Button2: TButton;
    Button3: TButton;
    Image10: TImage;
    procedure TrackBar1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  k : integer;

implementation

{$R *.dfm}

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
timer1.Interval:=trackbar1.Position;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
memo1.Clear;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
k:=0;
timer1.Enabled:=false;
image1.Visible:=true;
image2.Visible:=false;
image3.Visible:=false;
image4.Visible:=false;
image5.Visible:=false;
image6.Visible:=false;
image7.Visible:=false;
image8.Visible:=false;
image9.Visible:=false;
image10.Visible:=false;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var t1:integer;
begin
if k=0 then begin
    k:=1;
    image1.Visible:=false;
    image2.Visible:=true;
    panel1.Color:=clwhite;
    label2.Caption:='Ініціалізація PPI';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Ініціалізація PT СТ0';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Ініціалізація PT СТ1';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Запис константи в СТ0';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Запис константи в СТ1';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Очікування Ready';




end else if k=1 then begin
    k:=2;
    image2.Visible:=false;
    image3.Visible:=true;
    panel1.Color:=clwhite;
end else if k=2 then begin
    k:=3;
    image3.Visible:=false;
    image4.Visible:=true;
    panel1.Color:=clwhite;
end else if k=3 then begin
    k:=4;
    image4.Visible:=false;
    image5.Visible:=true;
    panel1.Color:=clwhite;
end else if k=4 then begin               
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Формування GateСТ0';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Очікування GateСТ0';
    k:=5;
    image5.Visible:=false;
    image9.Visible:=false;
    image6.Visible:=true;
    panel1.Color:=clwhite;
end else if k=5 then begin
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Перезапуск СТ0';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Очікування СТ0';
    k:=6;
    image6.Visible:=false;
    image7.Visible:=true;
    panel1.Color:=clwhite;
end else if k=6 then begin
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Перезапуск СТ1';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Очікування СТ1';
    k:=7;
    image7.Visible:=false;
    image8.Visible:=true;
    panel1.Color:=clred;
end else if k=7 then begin
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Перевірка Ready';
    k:=4;
    image8.Visible:=false;
    image9.Visible:=true;
    panel1.Color:=clred;
end else if k=10 then begin
image2.Visible:=false;
image3.Visible:=false;
image4.Visible:=false;
image5.Visible:=false;
image6.Visible:=false;
image7.Visible:=false;
image8.Visible:=false; 
image1.Visible:=true;
if panel1.Color=clred then begin
k:=11;
image9.Visible:=true;
end else k:=12;
end else if k=11 then begin
image10.Visible:=true;
image9.Visible:=false;
k:=12;
end else if k=12 then begin
image9.Visible:=false;
image10.Visible:=false;
panel1.Color:=clwhite;
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='Вимкнення програми';
k:=13;
end else if k=13 then begin
timer1.Enabled:=false;

end;
end;


procedure TForm1.FormCreate(Sender: TObject);
begin
trackbar1.Position:=timer1.Interval;
k:=0;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
timer1.Enabled:=true;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
k:=10;
end;

end.
