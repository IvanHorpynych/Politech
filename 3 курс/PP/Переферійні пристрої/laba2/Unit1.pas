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
    Image4: TImage;
    Button1: TButton;
    Button2: TButton;
    Panel1: TPanel;
    TrackBar1: TTrackBar;
    Timer1: TTimer;
    Label1: TLabel;
    procedure TrackBar1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
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

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
Timer1.Interval:=trackbar1.Position;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
image2.Visible:=true;
image1.Visible:=false;
timer1.Enabled:=true;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
if k1=true then begin
timer1.Enabled:=false;
image4.Visible:=false;
image1.Visible:=true;
panel1.Color:=clwindow;
k1:=false;
end else
begin
if image2.Visible=true then begin
image3.Visible:=true;
image2.Visible:=false;
panel1.Color:=clred;
end else begin
image2.Visible:=true;
image3.Visible:=false;
panel1.Color:=clwindow;
end;      end;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
if image2.Visible=true then begin
timer1.Enabled:=false;
image2.Visible:=false;
image1.Visible:=true;
end else begin
k1:=true;
image3.Visible:=false;
image4.Visible:=true;
panel1.Color:=clred;
end
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
k:=false;
k1:=false;
end;

end.
