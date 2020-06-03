unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Buttons, ExtCtrls;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    Panel3: TPanel;
    Panel4: TPanel;
    Panel5: TPanel;
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Image4: TImage;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    Label16: TLabel;
    Label17: TLabel;
    Label18: TLabel;
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
    Label39: TLabel;
    Label40: TLabel;
    Label41: TLabel;
    Label42: TLabel;
    Label43: TLabel;
    Label44: TLabel;
    Label45: TLabel;
    Label58: TLabel;
    Label59: TLabel;
    Label60: TLabel;
    Label61: TLabel;
    Label62: TLabel;
    Label63: TLabel;
    Label64: TLabel;
    Label65: TLabel;
    Label66: TLabel;
    Label67: TLabel;
    Label68: TLabel;
    Label69: TLabel;
    Label70: TLabel;
    Label71: TLabel;
    Label72: TLabel;
    Label73: TLabel;
    Label74: TLabel;
    Label75: TLabel;
    Label76: TLabel;
    Label77: TLabel;
    Label78: TLabel;
    Image6: TImage;
    Image7: TImage;
    Image5: TImage;
    Image8: TImage;
    Image9: TImage;
    Image10: TImage;
    Image11: TImage;
    CheckBox1: TCheckBox;
    CheckBox2: TCheckBox;
    CheckBox3: TCheckBox;
    Image12: TImage;
    Image13: TImage;
    Image14: TImage;
    Image15: TImage;
    Image16: TImage;
    Timer1: TTimer;
    Timer2: TTimer;
    Timer3: TTimer;
    Image17: TImage;
    Image18: TImage;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Image19: TImage;
    Label46: TLabel;
    Label47: TLabel;
    Label48: TLabel;
    Label49: TLabel;
    Label50: TLabel;
    Label51: TLabel;
    Label52: TLabel;
    Label53: TLabel;
    Label54: TLabel;
    Label20: TLabel;
    Label19: TLabel;
    Label55: TLabel;
    procedure Zapros;
    procedure CheckBox1Click(Sender: TObject);
    procedure CheckBox2Click(Sender: TObject);
    procedure CheckBox3Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure Timer3Timer(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}


procedure TForm1.Zapros;
begin
  Image7.Visible:=true;
  Application.ProcessMessages;
  sleep(1500);
  Image7.Visible:=false;
end;

procedure TForm1.CheckBox1Click(Sender: TObject);
begin
  if CheckBox1.Checked = true then
    Begin
      Zapros;
      Image8.Visible:=true;
      Image2.Visible:=true;
      Timer1.Enabled:=true;
      Timer3.Enabled:=true;
    end
  else
    Begin
      Image8.Visible:=false;
      Image2.Visible:=false;
      Timer1.Enabled:=false;
      Timer2.Enabled:=false;
      Timer3.Enabled:=false;
      if Image13.Visible = true then Image13.Visible:=false;
      if Image15.Visible = true then Image15.Visible:=false;
      if Image16.Visible = true then Image16.Visible:=false;
      if Image4.Visible = true then Image4.Visible:=false;
      if Image18.Visible = true then Image18.Visible:=false;
    end;
end;

procedure TForm1.CheckBox2Click(Sender: TObject);
begin
  if CheckBox2.Checked = true then Image9.Visible:=true
  else Image9.Visible:=false;
  if (CheckBox2.Checked = true)and(CheckBox3.Checked = true) then
    Begin
      Image19.Visible:=true;
      Zapros;
      Image19.Visible:=false;
      Image11.Visible:=true;
      Image2.Visible:=true;
      Timer1.Enabled:=true;
      Timer3.Enabled:=true;
    end
  else
    Begin
      Image11.Visible:=false;
      Image2.Visible:=false;
      Timer1.Enabled:=false;
      Timer2.Enabled:=false;
      Timer3.Enabled:=false;
      if Image13.Visible = true then Image13.Visible:=false;
      if Image15.Visible = true then Image15.Visible:=false;
      if Image16.Visible = true then Image16.Visible:=false;
      if Image4.Visible = true then Image4.Visible:=false;
      if Image18.Visible = true then Image18.Visible:=false;
    end;
end;

procedure TForm1.CheckBox3Click(Sender: TObject);
begin
if CheckBox3.Checked = true then Image10.Visible:=true
  else Image10.Visible:=false;
  if (CheckBox2.Checked = true)and(CheckBox3.Checked = true) then
    Begin
      Image19.Visible:=true;
      Zapros;
      Image19.Visible:=false;
      Image11.Visible:=true;
      Image2.Visible:=true;
      Timer1.Enabled:=true;
      Timer3.Enabled:=true;
    end
  else
    Begin
      Image11.Visible:=false;
      Image2.Visible:=false;
      Timer1.Enabled:=false;
      Timer2.Enabled:=false;
      Timer3.Enabled:=false;
      if Image13.Visible = true then Image13.Visible:=false;
      if Image15.Visible = true then Image15.Visible:=false;
      if Image16.Visible = true then Image16.Visible:=false;
      if Image4.Visible = true then Image4.Visible:=false;
      if Image18.Visible = true then Image18.Visible:=false;
    end;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
 if Image16.Visible = true then Image16.Visible:=false
 else Image16.Visible:= true;
 Timer2.Enabled:=true;
end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
  if Image13.Visible = true then Image13.Visible:=false
  else Image13.Visible:=true;
  if Image15.Visible = true then Image15.Visible:=false
  else Image15.Visible:=true;

end;

procedure TForm1.Timer3Timer(Sender: TObject);
begin
  if Image4.Visible = true then Image4.Visible:=false
  else Image4.Visible:=true;
  if Image18.Visible = true then Image18.Visible:=false
  else Image18.Visible:=true;
end;

end.
