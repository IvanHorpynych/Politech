program Project1;

uses
  Forms,
  U3Dpolys in '..\..\U3Dpolys.pas',
  UrickGL in '..\..\UrickGL.pas',
  Unit1 in 'Unit1.pas' {Form1};

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
