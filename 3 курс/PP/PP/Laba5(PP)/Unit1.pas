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
    Timer1: TTimer;
    TrackBar1: TTrackBar;
    Label9: TLabel;
    Label10: TLabel;
    Image1: TImage;
    Panel4: TPanel;
    Image2: TImage;
    Button1: TButton;
    Button2: TButton;
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
    Label42: TLabel;
    Label43: TLabel;
    Label41: TLabel;
    Label47: TLabel;
    Label48: TLabel;
    Label49: TLabel;
    Label50: TLabel;
    Label51: TLabel;
    Label52: TLabel;
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
    Image15: TImage;
    Image16: TImage;
    Image17: TImage;
    Image18: TImage;
    Label39: TLabel;
    Label40: TLabel;
    Label44: TLabel;
    Edit1: TEdit;
    Label45: TLabel;
    Edit2: TEdit;
    Label46: TLabel;
    Image19: TImage;
    Image20: TImage;
    Image21: TImage;
    Timer2: TTimer;
    Label53: TLabel;
    Label54: TLabel;
    Label55: TLabel;
    Label56: TLabel;
    Label57: TLabel;
    Label58: TLabel;
    Label59: TLabel;
    Label60: TLabel;
    Label61: TLabel;
    Label62: TLabel;
    Label63: TLabel;
    Label64: TLabel;
    Label65: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  i:Integer=0;
  Form1 : TForm1;
implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
  Image15.Visible:=true;
  Application.ProcessMessages;
  sleep(1000);
  Image2.Visible:=true;
  Image18.Visible:=true;
  Timer1.Enabled:=true;
end;

procedure TForm1.Button2Click(Sender: TObject);
  begin
    Image2.Visible:=false;
    Image18.Visible:=false;
    Image15.Visible:=false;
    Timer1.Enabled:=false;
    Timer2.Enabled:=false;
    if Image5.Visible = true then Image5.Visible:=false;
    if Image7.Visible = true then Image7.Visible:=false;
    if Image19.Visible = true then Image19.Visible:=false;
    if Image9.Visible = true then Image9.Visible:=false;
    if Image13.Visible = true then Image13.Visible:=false;
    if Image20.Visible = true then Image20.Visible:=false;
    Edit1.Text:='100';
    Edit2.Text:='0';
  end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
  Timer1.Interval:=form1.TrackBar1.Position;
end;


procedure TForm1.Timer1Timer(Sender: TObject);
begin
  i:=(i+1) mod 6;
  if (i mod 2)=0 then
  begin
    Image5.Visible:=false;
    Image7.Visible:=false;
    Image19.Visible:=false;
  end;
 case i of
 1 : Begin
      Image5.Visible:=true;
      if 1<=strtoint(Edit1.text) then
        begin
          Edit1.text:=inttostr(strtoint(Edit1.text)-1);
          Edit2.text:=inttostr(strtoint(Edit2.text)+1);
        end
       else
        Begin
          Timer1.Enabled:=false;
          Image5.Visible:=false;
          Image21.Visible:=true;
          Application.ProcessMessages;
          sleep(1000);
          Image21.Visible:=false;
          Timer2.Enabled:=true;
        end;
     end;
 3 : Image19.Visible:=true;
 5 : Image7.Visible:=true;
end;

end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
i:=(i+1) mod 6;
  if (i mod 2)=0 then
  begin
    Image9.Visible:=false;
    Image13.Visible:=false;
    Image20.Visible:=false;
  end;
 case i of
 1 : Begin
      Application.ProcessMessages;
      Image9.Visible:=true;
      if 1<=strtoint(Edit2.text) then
        begin
          Edit2.text:=inttostr(strtoint(Edit2.text)-1);
          Edit1.text:=inttostr(strtoint(Edit1.text)+1);
        end
       else
        Begin
          Timer2.Enabled:=false;
          Image9.Visible:=false;
          Image21.Visible:=true;
          Application.ProcessMessages;
          sleep(1000);
          Image21.Visible:=false;
          Image15.Visible:=false;
          Image2.Visible:=false;
          Image18.Visible:=false;

        end;
     end;
 3 : Image20.Visible:=true;
 5 : Image13.Visible:=true;
end;
end;
end.
