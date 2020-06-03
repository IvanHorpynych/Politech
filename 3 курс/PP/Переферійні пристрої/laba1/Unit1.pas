unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, jpeg, ExtCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    Memo1: TMemo;
    Label1: TLabel;
    Image1: TImage;
    Button1: TButton;
    Button2: TButton;
    Panel1: TPanel;
    Label2: TLabel;
    TrackBar1: TTrackBar;
    Label3: TLabel;
    Image3: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image2: TImage;
    Image8: TImage;
    Image9: TImage;
    Image10: TImage;
    Image11: TImage;
    Image12: TImage;
    Button3: TButton;
    Timer1: TTimer;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  k,k1:boolean;

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
timer1.Interval:=trackbar1.Position;
k:=false;
k1:=false;
memo1.Lines.Add('Очікування статусу готовності "Ready"');
end;

procedure TForm1.Button1Click(Sender: TObject);
begin                     
if k1=true then k1:=false;
timer1.Enabled:=true;
image8.Visible:=true;
image9.Visible:=true;
image10.Visible:=true;
image12.Visible:=true;
memo1.Lines.Add('Кнопку "Пуск" натиснено');
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
if k1=true then k1:=false;
image8.Visible:=false;
image9.Visible:=false;
image10.Visible:=false;
image12.Visible:=false;
memo1.Lines.Add('Кнопку "Стоп" натиснено');
k:=true;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
if k1=true then k1:=false;
if (panel1.Color=clred) then begin
memo1.Lines.Add('Індикатор вимкнено ("Control" вимкнено)');
image11.Visible:=false;
panel1.Color:=clwhite;
if (k=true) then
begin
memo1.Lines.Add('Очікування статусу готовності "Ready"');
k:=false;
timer1.Enabled:=false;
end;
end else
if (k<>true) then begin
memo1.Lines.Add('Індикатор ввімкнено ("Control" ввімкнено)');
image11.Visible:=true;
panel1.Color:=clred;
end else
begin
memo1.Lines.Add('Очікування статусу готовності "Ready"');
k:=false;
timer1.Enabled:=false;
end;
end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
if k1=false then begin
memo1.Lines.Add('Інтервал змінено');
k1:=true;
end;
timer1.Interval:=trackbar1.Position;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
memo1.clear;
end;


end.
