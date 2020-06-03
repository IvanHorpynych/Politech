unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TForm1 = class(TForm)
    GroupBox1: TGroupBox;
    Button1: TButton;
    Edit1: TEdit;
    Label1: TLabel;
    Button2: TButton;
    GroupBox2: TGroupBox;
    Edit2: TEdit;
    Edit3: TEdit;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var i,t:longint;
begin
  t:=strtoint(edit1.Text);
  for i:=2 to t-1 do
  if t mod i=0 then label1.Caption:='Число не є простим';
  label1.Caption:=label1.Caption+'.Виконано';
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
label1.Caption:='Число не перевірене';
end;

end.
