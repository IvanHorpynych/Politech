unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComObj, StdCtrls, ExtCtrls, ActiveX, Buttons;

type
  TMyThread = class(TThread)
    protected
    procedure Execute; override;
    end;
  TForm1 = class(TForm)
    Button1: TButton;
    Label1: TLabel;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    RadioButton3: TRadioButton;
    Button2: TButton;
    Memo1: TMemo;
    Button3: TButton;
    Button4: TButton;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
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
    Form1.Label1.Caption:='Password: '+pass;
    try
      if getlast(fpath,3)='doc' then MsWord.Documents.Open(fpath,0,0,0,pass)
      else MsExcel.Workbooks.Open[fpath,0,0,5,pass];
    except
      inc(a);
    end;
  end;
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
  form1.Button2.Enabled:=true;
  Form1.Button1.Caption:='Виконано';
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  input:=TStringList.Create;
  openDialog:=TOpenDialog.Create(self);
  openDialog.InitialDir:=ExtractFilePath(Application.ExeName);
  openDialog.Filter:='Файли Microsoft Word|*.doc|Файли Microsoft Excel|*.xls';
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

procedure TForm1.FormCreate(Sender: TObject);
begin
button2.Enabled:=false;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  Form1.Button1.Caption:='Підібрати пароль';
  radiobutton1.Enabled:=true;
  radiobutton2.Enabled:=true;
  radiobutton3.Enabled:=true;
  button2.Enabled:=false;
  button1.Enabled:=true;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
memo1.Lines.LoadFromFile('passwords.txt');
button4.Enabled:=true;
button3.Enabled:=false;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
memo1.Lines.savetoFile('passwords.txt');
button3.Enabled:=true;
button4.Enabled:=false;
memo1.Lines.Clear;
end;



end.
