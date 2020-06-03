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
    Image11: TImage;
    Image12: TImage;
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Timer1: TTimer;
    TrackBar1: TTrackBar;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
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
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1 : TForm1;
  pres  : boolean;
implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
  Image2.Visible:=true;
  Image6.Visible:=true;
  Timer1.Enabled:=true;

  end;


procedure TForm1.Button2Click(Sender: TObject);
begin
Image2.Visible:=false;
Image6.Visible:=false;
Application.ProcessMessages;
sleep(form1.TrackBar1.Position);
  if image4.Visible = true then image4.Visible:=false;
  if image12.Visible = true  then image12.Visible:=false;
Timer1.Enabled:=false;

end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
 if Image4.Visible = false then Image4.Visible:=true
                          else Image4.Visible:=false;
 if Image12.Visible = false then Image12.Visible:=true
                          else Image12.Visible:=false;
end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
Timer1.Interval:=form1.TrackBar1.Position;
end;

end.
