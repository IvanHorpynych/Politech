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
    Image11: TImage;
    Image12: TImage;
    Timer1: TTimer;
    TrackBar1: TTrackBar;
    Label9: TLabel;
    Label10: TLabel;
    Image1: TImage;
    Panel4: TPanel;
    Image2: TImage;
    Image3: TImage;
    Button1: TButton;
    Button2: TButton;
    Timer2: TTimer;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    Label16: TLabel;
    Label17: TLabel;
    Label18: TLabel;
    Label19: TLabel;
    Label20: TLabel;
    Label21: TLabel;
    Label22: TLabel;
    Label23: TLabel;
    Label24: TLabel;
    Label25: TLabel;
    Label26: TLabel;
    Label27: TLabel;
    Label28: TLabel;
    Label29: TLabel;
    Label30: TLabel;
    Label31: TLabel;
    Label32: TLabel;
    Label33: TLabel;
    Label34: TLabel;
    Label35: TLabel;
    Label36: TLabel;
    Label37: TLabel;
    Label38: TLabel;
    Label40: TLabel;
    Label42: TLabel;
    Label43: TLabel;
    Image7: TImage;
    Label39: TLabel;
    Label41: TLabel;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Label44: TLabel;
    Label45: TLabel;
    Label46: TLabel;
    Label47: TLabel;
    Label48: TLabel;
    Label49: TLabel;
    Label50: TLabel;
    Label51: TLabel;
    Label52: TLabel;
    Shape1: TShape;
    Shape2: TShape;
    Shape3: TShape;
    Shape4: TShape;
    Label53: TLabel;
    Label54: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
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
  Application.ProcessMessages;
  sleep(1000);
  Image2.Visible:=true;
  Timer1.Enabled:=true;
end;


procedure TForm1.Button2Click(Sender: TObject);
  begin
    if image5.Visible = true then image5.Visible:=false;
    if image6.Visible = true then image6.Visible:=false;
    if image12.Visible = true  then image12.Visible:=false;
    Timer1.Enabled:=false;
    Timer2.Enabled:=false;
    Image2.Visible:=false;
    Image4.Visible:=false;
  end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  if Image5.Visible = false then Image5.Visible:=true
                            else Image5.Visible:=false;
  Timer2.Enabled:=true;
end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
  Timer1.Interval:=form1.TrackBar1.Position;
  Timer2.Interval:=form1.TrackBar1.Position;
end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
  if Image6.Visible = false then Image6.Visible:=true
                            else Image6.Visible:=false;
  if Image12.Visible = false then Image12.Visible:=true
                             else Image12.Visible:=false;
end;

end.
