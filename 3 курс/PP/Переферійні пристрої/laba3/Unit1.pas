unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Button1: TButton;
    Button2: TButton;
    Panel1: TPanel;
    Panel2: TPanel;
    TrackBar1: TTrackBar;
    Label1: TLabel;
    Timer1: TTimer;
    Image2: TImage;
    Button3: TButton;
    Image3: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image8: TImage;
    Timer2: TTimer;
    Label2: TLabel;
    Memo1: TMemo;
    Button4: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure TrackBar1Change(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure Button4Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  writing: boolean;
  t,i: integer;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
t:=1;
image2.Visible:=true;
timer1.Enabled:=true;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
memo1.Lines.Add(label2.Caption);
label2.Caption:='������������ ������ �������';
memo1.Lines.Add(label2.Caption);
label2.Caption:='���������� �������� �� �����';
timer2.Enabled:=true;
image2.Visible:=false;
t:=0;
end;

procedure TForm1.TrackBar1Change(Sender: TObject);
begin
timer2.Interval:=timer1.Interval div 10;
timer1.Interval:=trackbar1.Position;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
    image2.Visible:=false;
    image3.Visible:=false;
    image4.Visible:=false;
    image5.Visible:=false;
    image6.Visible:=false;
    image7.Visible:=false;
    image8.Visible:=false;
    timer1.Enabled:=false;
    panel1.Caption:='100';
    panel2.Caption:='0';
    writing:=true;           
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='���������� �� ��������� �����';  
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='������������ �в (����� 1)';
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='����������"Ready" (��4=1)';
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
timer2.Interval:=timer1.Interval div 10;
writing:=true;
i:=100;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
if t=-1 then
  begin
    image2.Visible:=false;
    image3.Visible:=false;
    image4.Visible:=false;
    image5.Visible:=false;
    image6.Visible:=false;
    image7.Visible:=false;
    image8.Visible:=false;
    timer1.Enabled:=false;
    memo1.Lines.Add(label2.Caption);
    label2.Caption:='������������ ������ � obj';
  end;
if t=0 then t:=-1;
if writing=true then
    begin
      if t=3 then
        begin
          memo1.Lines.Add(label2.Caption);
          label2.Caption:='����� ���';
          image3.Visible:=false;
          image5.Visible:=false;
          image7.Visible:=true;
          t:=1;
        end else begin
      if t=2 then
        begin
          if i=0 then
            begin
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='�������� �� �� ����� ��������';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='�� ����� ��������';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='��������� ���������';
              writing:=false;
              t:=3;
              image3.Visible:=false;
              image5.Visible:=false;
            end
        else begin
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='��������� ������ ������ ���';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='��������� ���������';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='�������� �� �� ����� ��������';
              dec(i);
              panel1.Caption:=inttostr(i);
              t:=3;
            end;
        end;
      if t=1 then
        begin
          memo1.Lines.Add(label2.Caption);
          label2.Caption:='���� ����� � ����������';
          image3.Visible:=true;
          image5.Visible:=true;
          image7.Visible:=false;
          t:=2;
        end;
        end;
end
  else
    begin
      if t=3 then
        begin
          if i=100 then
            begin
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='�������� �� �� ����� �������';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='�� ����� �������';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='��������� ��������';
              writing:=true;
              t:=0;
              timer1.Enabled:=false;
              image4.Visible:=false;
              image8.Visible:=false;
              image2.Visible:=false;
            end
        else begin                                               
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='��������� ������ ������ ���';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='��������� ���������';
              memo1.Lines.Add(label2.Caption);
              label2.Caption:='�������� �� �� ����� ��������';
              inc(i);
              panel2.Caption:=inttostr(i);
              t:=1;
            end;
        end;
      if t=2 then
        begin                   
          memo1.Lines.Add(label2.Caption);
          label2.Caption:='����� ���';
          image6.Visible:=false;
          image4.Visible:=true;
          image8.Visible:=true;
          t:=3;
        end;
      if t=1 then
        begin
          memo1.Lines.Add(label2.Caption);
          label2.Caption:='��� ����� � �����';
          image6.Visible:=true;
          image4.Visible:=false;
          image8.Visible:=false;
          t:=2;
        end;
  end;



end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
timer2.Interval:=timer1.Interval div 10;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
memo1.Lines.Clear;
end;

end.
