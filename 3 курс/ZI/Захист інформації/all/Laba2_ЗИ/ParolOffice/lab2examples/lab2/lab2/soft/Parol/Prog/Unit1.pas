unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Va, Classes, Graphics, Controls, Forms,
  Dialogs, ComObj, StdCtrls, ExtCtrls, ActiveX;

type
  TMyThread = class(TThread)
    protected
    procedure Execute; override;
    end;
  TForm1 = class(TForm)
    Button1: TButton;
    Label1: TLabel;
    Label2: TLabel;
    Timer1: TTimer;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    RadioButton3: TRadioButton;
    Panel1: TPanel;
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
  private
    { Private declarations }
    PT:TMyThread;
  public
    { Public declarations }
  end;

var
  Form1:TForm1;
  openDialog:TOpenDialog;
  MsExcel,MsWord:Variant;
  i,a,s:integer;
  fpath,pass:string;
  input:TStringList;

implementation

{$R *.dfm}

function getlast(line:string;len:integer):string;
begin
  getlast:=copy(line,length(line)-len+1,len);
end;

function getpass(i:integer):string;
var
  pass:string;
begin
  pass:='';
  { div - chilochislenne dilenna mod - ostatok } 
  if (i div (27*27*27*27*27))>0 then pass:=pass+char((i div (27*27*27*27*27))+96);
  if (i div (27*27*27*27))>0 then pass:=pass+char((i div (27*27*27*27))+96);
  if (i div (27*27*27))>0 then pass:=pass+char((i div (27*27*27))+96);
  if (i div (27*27))>0 then pass:=pass+char((i div (27*27))+96);
  if (i div 27)>0 then pass:=pass+char((i div 27)+96);
  if (i mod 27)>0 then pass:=pass+char((i mod 27)+96)
  else pass:=pass+char((i mod 26)+96);
  getpass:=pass;
end;

procedure TMyThread.Execute;
begin
  a:=0;
  s:=0;
  i:=0;
  CoInitialize(nil);
  if getlast(fpath,3)='doc' then MsWord := CreateOleObject('Word.Application')
  else MsExcel := CreateOleObject('Excel.Application');
  Form1.Timer1.Enabled:=True;
  if Form1.RadioButton3.Checked then input.LoadFromFile('passwords.txt');
  while a=i do
  begin
    inc(i);
    if Form1.RadioButton2.Checked then pass:=getpass(i)
    else
    if Form1.RadioButton3.Checked
    then
    begin
      if i<=input.Count then pass:=input.Strings[i-1]
      else break;
    end
    else pass:=IntToStr(i);
    Form1.Label1.Caption:='Current Password: '+pass;
    try
      if getlast(fpath,3)='doc' then MsWord.Documents.Open(fpath,0,0,0,pass)
      else MsExcel.Workbooks.Open[fpath,0,0,5,pass];
    except
      inc(a);
    end;
  end;
  Form1.Timer1.Enabled:=False;
  if getlast(fpath,3)='doc' then
  begin
    MsWord.ActiveDocument.Close;
    MsWord.Application.Quit;
  end
  else
  begin
    MsExcel.ActiveWorkbook.Close;
    MsExcel.Application.Quit;
  end;
  Form1.Button1.Caption:='Done';
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  input:=TStringList.Create;
  openDialog:=TOpenDialog.Create(self);
  openDialog.InitialDir:=ExtractFilePath(Application.ExeName);
  openDialog.Filter:='Microsoft Word Files|*.doc|Microsoft Excel Files|*.xls';
  if openDialog.Execute then
  begin
    fpath:=openDialog.FileName;
    Button1.Enabled:=False;
    RadioButton1.Enabled:=False;
    RadioButton2.Enabled:=False;
    RadioButton3.Enabled:=False;
    PT:=TMyThread.Create(False);
  end;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  Form1.Label2.Caption:='PPS: '+IntToStr(i-s);
  s:=i;
end;

end.
