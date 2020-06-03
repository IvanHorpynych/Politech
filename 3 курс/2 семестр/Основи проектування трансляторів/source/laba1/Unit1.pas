unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  kl=record
    s:string[40];
    n:string[3];
  end;
  TForm1 = class(TForm)
    Memo1: TMemo;
    Memo2: TMemo;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Memo3: TMemo;
    Memo4: TMemo;
    Memo5: TMemo;
    Memo6: TMemo;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Button1: TButton;
    Memo7: TMemo;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  a,chysla,sl:array[1..100]of kl;
  Form1: TForm1;
  i,t,t1,t2:integer;
  temp:string[20];

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
var f1:textfile;
begin
memo3.Lines.LoadFromFile('kl.pro');
memo1.Lines.LoadFromFile('input.pas');
assignfile(f1,'kl.pro');
reset(f1);
t:=8;
t1:=0;
t2:=0;
a[1].s:='PROGRAM';
a[1].n:='701';
a[2].s:='BEGIN';
a[2].n:='702';
a[3].s:='GOTO';
a[3].n:='703';
a[4].s:='IN';
a[4].n:='704';
a[5].s:='LINK';
a[5].n:='705';
a[6].s:='OUT';
a[6].n:='706';
a[7].s:='RETURN';
a[7].n:='707';
a[8].s:='END';
a[8].n:='708';
closefile(f1);
end;

procedure perevirka1(temp1:string);
var bool:boolean;
begin
temp:='';
bool:=false;
for i:=1 to t1 do begin
  if temp1=chysla[i].s then
    begin
    bool:=true;
    form1.memo2.lines.add(chysla[i].n);
    end;
    end;
if bool=false then
    begin
    inc(t1);
    chysla[t1].s:=temp1;
    chysla[t1].n:=inttostr(4*100+t1);
    form1.memo2.lines.add(chysla[t1].n);
end;
end;

procedure perevirka2(temp1:string);
var bool:boolean;
j,j1:integer;
begin
temp:='';
j:=0;
bool:=false;
for i:=1 to t do
for j:=1 to length(temp1) do
if temp1[j]=a[i].s[j] then
    inc(j1);
    if j1=length(a[i].s)-1 then begin
    form1.Memo2.Lines.Add(temp1);
    exit;
    end;
for i:=1 to t2 do
if temp1=sl[i].s then
    begin
    form1.memo2.lines.add(sl[i].n);
    bool:=true;
    end;
if bool=false then
    begin
    inc(t2);
    sl[t2].s:=temp1;
    sl[t2].n:=inttostr(8*100+t2);
    form1.memo2.lines.add(sl[t2].n);
    end;
end;

procedure TForm1.Button1Click(Sender: TObject);
var f2:textfile;
c,c1:char;
status:byte;
begin
assignfile(f2,'input.pas');
reset(f2);
status:=0;
while not (eof(f2)) do
           begin
c1:=c;
read(f2,c);

if status=2 then begin
  if (ord(c)>64) and (ord(c)<91) then temp:=temp+c  else
  if (c=' ') then begin status:=0; perevirka2(temp); end else
  if (c=';') then begin status:=4; perevirka2(temp); end else
  if (c=':') then begin status:=7; perevirka2(temp); end else
  if (c=',') then begin status:=3; perevirka2(temp); end else
  if (c=')') then begin status:=6; perevirka2(temp); end else
{  if (ord(c)>33) and (ord(c)<63) then
      begin
      memo7.Lines.Add('Помилка при введенні. Недопустимий символ у рядку');
      status:=9; end;}
end;

if status=1 then begin
  if (ord(c)>47) and (ord(c)<58) then temp:=temp+c else
  if (ord(c)>64) and (ord(c)<91) then begin temp:=temp+c; status:=2; end else
  if (c=' ') then begin status:=0; perevirka1(temp); end else
  if (c=';') then begin status:=4; perevirka1(temp); end else
  if (c=':') then begin status:=5; perevirka1(temp); end else{ce zavzhdy labelka}
{  if (ord(c)>33) and (ord(c)<63) then
      begin
      memo7.Lines.Add('Помилка при введенні. Недопустимий символ у рядку');
      status:=9; end;                                          }
  end;

if status=0 then begin
  if (ord(c)>47) and (ord(c)<58) then begin temp:=c; status:=1; end else
  if (ord(c)>64) and (ord(c)<91) then begin temp:=c; status:=2; end else
  if (c=',') then status:=3 else
  if (c=';') then status:=4 else
  if (c=':') then status:=5 else
  if (c='(') then status:=6 else
  if (c=' ') then status:=0 else
 { if (ord(c)>33) and (ord(c)<63) then                         
      begin
      memo7.Lines.Add('Помилка при введенні. Недопустимий символ у рядку');
      status:=9; end;                                        }
  end;

if status=7 then begin
  read(f2,c);
  if (c='=') then begin status:=0; form1.memo2.lines.add('301');end;
end;

if (status=3) or (status=4) or (status=5) then
   begin
   status:=0; form1.memo2.lines.add(inttostr(ord(c)));
   end;

if (status=6) then begin
  read(f2,c);
  if c='$' then begin read(f2,c);
  temp:=c;
  while c<>'$' do begin read(f2,c); temp:=temp+c; end;
  read(f2,c);
  if c=')' then begin
      form1.memo2.lines.add('302');                       
      inc(t2);
      sl[t2].s:=temp;
      sl[t2].n:=inttostr(8*100+t2);
      form1.memo2.lines.add(sl[t2].n);
      form1.memo2.lines.add('303');
      status:=0;
      temp:='';
      end;
  end;
  if c='*' then begin read(f2,c);
  temp:=c;
  while c<>'*' do read(f2,c);
  read(f2,c);
  status:=0;
  end else begin
  status:=0; form1.memo2.lines.add(inttostr(ord('(')));
  end;end;



end;
closefile(f2);
for i:=1 to t2 do memo4.Lines.Add(sl[i].s+' '+sl[i].n);
for i:=1 to t1 do memo5.Lines.Add(chysla[i].s+' '+chysla[i].n);
end;

end.
