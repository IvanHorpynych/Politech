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
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image8: TImage;
    Image9: TImage;
    Image10: TImage;
    Image13: TImage;
    Image14: TImage;
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
    Label42: TLabel;
    Label43: TLabel;
    Label44: TLabel;
    Label45: TLabel;
    Label46: TLabel;
    Label47: TLabel;
    Label48: TLabel;
    Label49: TLabel;
    Image15: TImage;
    Image16: TImage;
    Image17: TImage;
    Image18: TImage;
    Image19: TImage;
    Image20: TImage;
    Image21: TImage;
    Image22: TImage;
    Image23: TImage;
    Image24: TImage;
    Label41: TLabel;
    Label50: TLabel;
    Label15: TLabel;
    Label16: TLabel;
    Label17: TLabel;
    Label51: TLabel;
    Label52: TLabel;
    Label53: TLabel;
    CheckBox1: TCheckBox;
    CheckBox2: TCheckBox;
    CheckBox3: TCheckBox;
    CheckBox4: TCheckBox;
    CheckBox5: TCheckBox;
    CheckBox6: TCheckBox;
    Label54: TLabel;
    Label55: TLabel;
    Label56: TLabel;
    Label57: TLabel;
    Label58: TLabel;
    Label59: TLabel;
    Label60: TLabel;
    Label61: TLabel;
    Shape1: TShape;
    Shape2: TShape;
    Shape4: TShape;
    Label29: TLabel;
    procedure Pochatok;
    procedure Exit;
    procedure Timer1Timer(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure CheckBox4Click(Sender: TObject);
    procedure CheckBox5Click(Sender: TObject);
    procedure CheckBox6Click(Sender: TObject);
    procedure CheckBox1Click(Sender: TObject);
    procedure CheckBox2Click(Sender: TObject);
    procedure CheckBox3Click(Sender: TObject);
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


procedure TForm1.Pochatok;
begin
  Image9.Visible:=true;
  Image14.Visible:=true;
  Timer1.Enabled:=true;
  Timer2.Enabled:=true;
end;

procedure Tform1.Exit;
  begin
    Image2.Visible:=false;
    Image9.Visible:=false;
    Image14.Visible:=false;
    Timer2.Enabled:=false;
    if image10.Visible = true then image10.Visible:=false;
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

procedure TForm1.Timer2Timer(Sender: TObject);
begin
if Image10.Visible = false then Image10.Visible:=true
                           else Image10.Visible:=false;
end;

procedure TForm1.CheckBox4Click(Sender: TObject);
begin
if (CheckBox4.Checked = true) then
  begin
    Image22.Visible:=true;
    Image2.Visible:=true;
    Image9.Visible:=true;
    Application.ProcessMessages;
    sleep(200);
    if ((CheckBox1.Checked = true)and(CheckBox2.Checked = true))or
        ((CheckBox1.Checked = true)and(CheckBox3.Checked = true))or
        ((CheckBox2.Checked = true)and(CheckBox3.Checked = true)) then
        Pochatok
        else CheckBox4.Checked:= false;

  end
else
  Begin
    Image22.Visible:=false;
    Exit;
  end;


end;

procedure TForm1.CheckBox5Click(Sender: TObject);
begin
  if (CheckBox5.Checked = true)then Image23.Visible:=true
  else Image23.Visible:=false;
  if (CheckBox5.Checked = true)and(CheckBox6.Checked = true) then
    Begin
      Image2.Visible:=true;
      Image9.Visible:=true;
      Application.ProcessMessages;
      sleep(200);
      if ((CheckBox1.Checked = true)and(CheckBox2.Checked = true))or
          ((CheckBox1.Checked = true)and(CheckBox3.Checked = true))or
          ((CheckBox2.Checked = true)and(CheckBox3.Checked = true)) then
          Pochatok
        else
          Begin
            CheckBox5.Checked:=false;
            CheckBox6.Checked:=false;
          end;
    end
  else Exit;
end;

procedure TForm1.CheckBox6Click(Sender: TObject);
begin
  if (CheckBox6.Checked = true)then Image24.Visible:=true
  else Image24.Visible:=false;
  if (CheckBox5.Checked = true)and(CheckBox6.Checked = true) then
    Begin
      Image2.Visible:=true;
      Image9.Visible:=true;
      Application.ProcessMessages;
      sleep(200);
      if ((CheckBox1.Checked = true)and(CheckBox2.Checked = true))or
          ((CheckBox1.Checked = true)and(CheckBox3.Checked = true))or
          ((CheckBox2.Checked = true)and(CheckBox3.Checked = true)) then
          Pochatok
        else
          Begin
            CheckBox5.Checked:=false;
            CheckBox6.Checked:=false;
          end;
    end
  else Exit;
end;

procedure TForm1.CheckBox1Click(Sender: TObject);
begin
  if (CheckBox1.Checked = true)then Image6.Visible:=true
  else
    Begin
      Image6.Visible:=false;
      Exit;
    end;
end;

procedure TForm1.CheckBox2Click(Sender: TObject);
begin
  if (CheckBox2.Checked = true)then Image20.Visible:=true
  else
    Begin
      Image20.Visible:=false;
      Exit;
    end
end;

procedure TForm1.CheckBox3Click(Sender: TObject);
begin
  if (CheckBox3.Checked = true)then Image21.Visible:=true
  else
    Begin
      Image21.Visible:=false;
      Exit;
    end;
end;
end.
