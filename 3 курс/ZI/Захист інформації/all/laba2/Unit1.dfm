object Form1: TForm1
  Left = 220
  Top = 141
  BorderStyle = bsToolWindow
  Caption = #1051#1072#1073#1086#1088#1072#1090#1086#1088#1085#1072' '#1088#1086#1073#1086#1090#1072' '#8470'2 '#1057#1090#1077#1087#1072#1085#1102#1082#1072' '#1052#1080#1093#1072#1081#1083#1072
  ClientHeight = 234
  ClientWidth = 312
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 136
    Top = 16
    Width = 63
    Height = 16
    Caption = 'Password:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Button1: TButton
    Left = 136
    Top = 0
    Width = 177
    Height = 17
    Caption = #1055#1110#1076#1110#1073#1088#1072#1090#1080' '#1087#1072#1088#1086#1083#1100
    TabOrder = 0
    OnClick = Button1Click
  end
  object RadioButton1: TRadioButton
    Left = 0
    Top = 0
    Width = 137
    Height = 25
    Caption = #1063#1080#1089#1083#1086#1074#1080#1081' '#1087#1072#1088#1086#1083#1100
    Checked = True
    TabOrder = 1
    TabStop = True
  end
  object RadioButton2: TRadioButton
    Left = 0
    Top = 24
    Width = 137
    Height = 25
    Caption = #1041#1091#1082#1074#1077#1085#1085#1080#1081' '#1087#1072#1088#1086#1083#1100
    TabOrder = 2
  end
  object RadioButton3: TRadioButton
    Left = 0
    Top = 48
    Width = 137
    Height = 25
    Caption = #1055#1072#1088#1086#1083#1100' '#1079#1072' '#1089#1083#1086#1074#1085#1080#1082#1086#1084
    TabOrder = 3
  end
  object Button2: TButton
    Left = 136
    Top = 32
    Width = 177
    Height = 17
    Caption = #1055#1086#1095#1072#1090#1080' '#1089#1087#1086#1095#1072#1090#1082#1091
    TabOrder = 4
    OnClick = Button2Click
  end
  object Memo1: TMemo
    Left = 0
    Top = 80
    Width = 313
    Height = 153
    ScrollBars = ssVertical
    TabOrder = 5
  end
  object Button3: TButton
    Left = 136
    Top = 48
    Width = 177
    Height = 17
    Caption = #1047#1072#1074#1072#1085#1090#1072#1078#1080#1090#1080' '#1074#1084#1110#1089#1090' '#1089#1083#1086#1074#1085#1080#1082#1072
    TabOrder = 6
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 136
    Top = 64
    Width = 177
    Height = 17
    Caption = #1047#1073#1077#1088#1077#1075#1090#1080' '#1074#1084#1110#1089#1090' '#1089#1083#1086#1074#1085#1080#1082#1072
    Enabled = False
    TabOrder = 7
    OnClick = Button4Click
  end
end
