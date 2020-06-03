unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, DBCtrls, ExtCtrls;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    Panel3: TPanel;
    Button1: TButton;
    Button2: TButton;
    Image12: TImage;
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image8: TImage;
    Image9: TImage;
    Image10: TImage;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Clock: TTimer;
    TrackBar1: TTrackBar;
    Label8: TLabel;
    Image11: TImage;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure ClockTimer(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1 : TForm1;
implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
  Image4.Visible:=true;
  Image6.Visible:=true;
  Image8.Visible:=true;
  Image10.Visible:=true;
  Application.ProcessMessages;
  Clock.Enabled:=true;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  Image4.Visible:=false;
  Image6.Visible:=false;
  Image8.Visible:=false;
  Image10.Visible:=false;
  Application.ProcessMessages;
  sleep(form1.TrackBar1.Position);
  if image2.Visible = true then image2.Visible:=false;
  if image12.Visible = true  then image12.Visible:=false;
  Clock.Enabled:=false;
end;

procedure TForm1.ClockTimer(Sender: TObject);
begin
  if image2.Visible = false then image2.Visible:=true
                            else image2.Visible:= false;
  if image12.Visible = false then image12.Visible:=true
                             else image12.Visible:= false;
end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
Clock.Interval:=form1.TrackBar1.Position;
end;

end.
