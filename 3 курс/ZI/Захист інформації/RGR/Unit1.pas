unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, MPlayer;

type
  TForm1 = class(TForm)
    MediaPlayer1: TMediaPlayer;
    MediaPlayer2: TMediaPlayer;
    MediaPlayer3: TMediaPlayer;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    Button6: TButton;
    Button7: TButton;
    Button8: TButton;
    Button9: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button7Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure Button9Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
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
begin
mediaplayer1.Play;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
mediaplayer1.Pause;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
mediaplayer1.Stop;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
mediaplayer2.Play;
end;

procedure TForm1.Button7Click(Sender: TObject);
begin
mediaplayer3.Play;
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
mediaplayer2.Pause;
end;

procedure TForm1.Button6Click(Sender: TObject);
begin
mediaplayer2.Stop;
end;

procedure TForm1.Button8Click(Sender: TObject);
begin
mediaplayer3.Pause;
end;

procedure TForm1.Button9Click(Sender: TObject);
begin
mediaplayer3.Stop;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
mediaplayer1.Open;
mediaplayer2.Open;
mediaplayer3.Open;
end;

end.
