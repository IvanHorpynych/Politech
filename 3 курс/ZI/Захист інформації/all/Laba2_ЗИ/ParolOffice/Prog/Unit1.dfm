object Form1: TForm1
  Left = 206
  Top = 221
  Width = 423
  Height = 407
  Caption = 'Office Password'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 32
    Top = 80
    Width = 130
    Height = 20
    Caption = 'Current Password:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 32
    Top = 120
    Width = 48
    Height = 20
    Caption = 'PPS: 0'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Panel1: TPanel
    Left = 24
    Top = 160
    Width = 369
    Height = 41
    TabOrder = 4
  end
  object Button1: TButton
    Left = 184
    Top = 304
    Width = 209
    Height = 41
    Caption = 'Start'
    TabOrder = 0
    OnClick = Button1Click
  end
  object RadioButton1: TRadioButton
    Left = 32
    Top = 168
    Width = 113
    Height = 25
    Caption = '0..9'
    Checked = True
    TabOrder = 1
    TabStop = True
  end
  object RadioButton2: TRadioButton
    Left = 88
    Top = 168
    Width = 113
    Height = 25
    Caption = 'A..Z'
    TabOrder = 2
  end
  object RadioButton3: TRadioButton
    Left = 144
    Top = 168
    Width = 113
    Height = 25
    Caption = #1047#1072' '#1089#1087#1080#1089#1082#1086#1084
    TabOrder = 3
  end
  object Timer1: TTimer
    Enabled = False
    OnTimer = Timer1Timer
    Left = 16
    Top = 8
  end
end
