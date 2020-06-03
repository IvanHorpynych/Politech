unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Timer1: TTimer;
    TrackBar1: TTrackBar;
    Label1: TLabel;
    Memo1: TMemo;
    Button1: TButton;
    Label2: TLabel;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Image2: TImage;
    Image3: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image8: TImage;
    Image9: TImage;
    Image10: TImage;
    Image11: TImage;
    Image12: TImage;
    Image13: TImage;
    Edit1: TEdit;
    Edit2: TEdit;
    Image14: TImage;
    Image15: TImage;
    Button5: TButton;
    procedure TrackBar1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button5Click(Sender: TObject);
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
image11.Visible:=false;
image12.Visible:=false;
image13.Visible:=false;
image14.Visible:=false;
image15.Visible:=false;
edit1.Text:='100';
edit2.Text:='100';
memo1.Lines.Add('Ініціалізація РРІ-1');
memo1.Lines.Add('Ініціалізація РРІ-2');
memo1.Lines.Add('Ініціалізація РT');
memo1.Lines.Add('Запис константи в лічильник');
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
memo1.Lines.Add('Ініціалізація РРІ-1');
memo1.Lines.Add('Ініціалізація РРІ-2');
memo1.Lines.Add('Ініціалізація РT');
memo1.Lines.Add('Запис константи в лічильник');
timer1.Enabled:=true;
k:=20;
image2.Visible:=true;
image5.Visible:=true;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
k:=0;
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
image11.Visible:=false;
image12.Visible:=false;
image13.Visible:=false;
image14.Visible:=false;
image15.Visible:=false;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var t1:integer;
begin
if k=0 then begin
timer1.Enabled:=false;
end else
if k=20 then begin
image14.Visible:=true;
memo1.Lines.Add('Формування Gate0');
label2.Caption:='Очікування готовності джерела для приймача';
k:=1;
end else
if k=1 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Формування сигналу готовності приймача';
k:=2;
image6.Visible:=true;
end else
if k=2 then begin
memo1.Lines.Add(label2.Caption);          
image15.Visible:=false;
label2.Caption:='Очікування запиту приймача';
memo1.Lines.Add(label2.Caption);
label2.Caption:='Видача байта з ОЗУ в al';
k:=3;
end else
if k=3 then begin
image7.Visible:=true;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Інкремент адреси';
t1:=strtoint(edit1.Text);
dec(t1);
edit1.Text:=inttostr(t1);
k:=4;
end else
if k=4 then begin
image11.Visible:=true;
image3.Visible:=true;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Видача байта в порт РА';
memo1.Lines.Add(label2.Caption);
label2.Caption:='Формування СТР-П';
k:=5;
end else
if k=5 then begin
image11.Visible:=false;
image13.Visible:=true;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Очікування скидання ЗП-П';
k:=6;
end else
if k=6 then begin         
image15.Visible:=true;
image7.Visible:=false;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Cкидання CTP-П';
k:=7;
end else
if k=7 then begin
image3.Visible:=false;
image13.Visible:=false;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Перевірка Out0';
if strtoint(edit1.text)>0 then k:=2 else
begin
image5.Visible:=false;
k:=8;
end;
end else
if k=8 then begin
image15.Visible:=false;
image6.Visible:=false;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Cкидання ГП-П';
k:=9;
end else
if k=9 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Перезапуск ЛЧК';
memo1.Lines.Add(label2.Caption);
label2.Caption:='Очікування ГИ-И';
k:=10;
end else
if k=10 then begin
image8.Visible:=true;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Формування ГП-И';
k:=11;
end else
if k=11 then begin
image9.Visible:=true;
k:=12;
end else
if k=12 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Формування ЗП-И';
k:=13;
end else
if k=13 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Очікування СТР-И';
image10.Visible:=true;
k:=14;
end else
if k=14 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Введення байта в порт РВ';
image4.Visible:=true;
k:=15;
end else
if k=15 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Ввід байта в ОЗУ';
image12.Visible:=true;
image13.Visible:=true;
k:=16;
end else
if k=16 then begin
memo1.Lines.Add(label2.Caption);
image15.Visible:=true;
label2.Caption:='Інкремент адреси';
t1:=strtoint(edit2.Text);
dec(t1);
edit2.Text:=inttostr(t1);
k:=17;
image12.Visible:=false;
end else
if k=17 then begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Cкидання запиту';
image10.Visible:=false;
image14.Visible:=false;
memo1.Lines.Add(label2.Caption);
label2.Caption:='Очікування скидання СТР-И';
k:=18;               
image4.Visible:=false;
image13.Visible:=false;
end else
if k=18 then begin
memo1.Lines.Add(label2.Caption);
image15.Visible:=false;
label2.Caption:='Перевірка out0';
if strtoint(edit2.text)>0 then k:=13 else
begin
image8.Visible:=false;
image9.Visible:=false;
k:=19;
end;
end else if k=19 then
begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='Закінчення виконання';
k:=0;
end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
trackbar1.Position:=timer1.Interval;
end;

procedure TForm1.Button5Click(Sender: TObject);
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
image11.Visible:=false;
image12.Visible:=false;
image13.Visible:=false;
image14.Visible:=false;
image15.Visible:=false;
edit1.Text:='5';
edit2.Text:='5';
memo1.Lines.Add('Ініціалізація РРІ-1');
memo1.Lines.Add('Ініціалізація РРІ-2');
memo1.Lines.Add('Ініціалізація РT');
memo1.Lines.Add('Запис константи в лічильник');
end;

end.
