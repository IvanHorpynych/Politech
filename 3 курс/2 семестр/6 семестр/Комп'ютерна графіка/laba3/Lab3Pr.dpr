program Lab3Pr;

uses
  Forms,
  Lab3 in 'Lab3.pas' {Form1};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
