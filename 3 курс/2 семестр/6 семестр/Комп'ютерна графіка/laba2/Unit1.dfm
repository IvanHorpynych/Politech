object Form1: TForm1
  Left = 536
  Top = 190
  BorderStyle = bsSingle
  Caption = #1051#1072#1073#1086#1088#1072#1090#1086#1088#1085#1072' '#1088#1086#1073#1086#1090#1072' '#8470'2 '#1079' '#1076#1080#1089#1094#1080#1087#1083#1110#1085#1080' "'#1050#1086#1084#1087#39#1102#1090#1077#1088#1085#1072' '#1075#1088#1072#1092#1110#1082#1072'"'
  ClientHeight = 440
  ClientWidth = 817
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  Menu = MainMenu1
  OldCreateOrder = False
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  OnPaint = FormPaint
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 1297
    Height = 81
    TabOrder = 0
    object Label1: TLabel
      Left = 64
      Top = 0
      Width = 193
      Height = 13
      Caption = #1054#1073#1077#1088#1110#1090#1100' '#1096#1074#1080#1076#1082#1110#1089#1090#1100' '#1086#1073#1077#1088#1090#1072#1085#1085#1103' '#1110#1085#1110#1094#1110#1072#1083#1110#1074
    end
    object Label2: TLabel
      Left = 64
      Top = 16
      Width = 108
      Height = 13
      Caption = #1042#1110#1076#1090#1110#1085#1082#1080':     '#1095#1077#1088#1074#1086#1085#1080#1081
    end
    object Label3: TLabel
      Left = 64
      Top = 32
      Width = 107
      Height = 13
      Caption = '                      '#1079#1077#1083#1077#1085#1080#1081
    end
    object Label4: TLabel
      Left = 64
      Top = 48
      Width = 106
      Height = 13
      Caption = '                           '#1089#1080#1085#1110#1081
    end
    object Label5: TLabel
      Left = 64
      Top = 64
      Width = 105
      Height = 13
      Caption = '                 '#1087#1088#1086#1079#1086#1088#1110#1089#1090#1100
    end
    object Button1: TButton
      Left = 0
      Top = 0
      Width = 57
      Height = 17
      Caption = #1057#1090#1072#1088#1090
      TabOrder = 0
      OnClick = Button1Click
    end
    object TrackBar1: TTrackBar
      Left = 256
      Top = 0
      Width = 1041
      Height = 17
      Max = 1000
      Min = 1
      Position = 1
      TabOrder = 1
      OnChange = TrackBar1Change
    end
    object TrackBar2: TTrackBar
      Left = 176
      Top = 16
      Width = 425
      Height = 17
      Max = 100
      Position = 100
      TabOrder = 2
      OnChange = TrackBar2Change
    end
    object TrackBar3: TTrackBar
      Left = 176
      Top = 32
      Width = 425
      Height = 25
      Max = 100
      TabOrder = 3
      OnChange = TrackBar3Change
    end
    object TrackBar4: TTrackBar
      Left = 176
      Top = 48
      Width = 425
      Height = 21
      Max = 100
      TabOrder = 4
      OnChange = TrackBar4Change
    end
    object TrackBar5: TTrackBar
      Left = 176
      Top = 64
      Width = 425
      Height = 21
      Max = 100
      Position = 100
      TabOrder = 5
      OnChange = TrackBar5Change
    end
    object Button2: TButton
      Left = 0
      Top = 16
      Width = 57
      Height = 17
      Caption = #1055#1072#1091#1079#1072
      TabOrder = 6
      OnClick = Button2Click
    end
    object Button3: TButton
      Left = 0
      Top = 32
      Width = 57
      Height = 17
      Caption = #1042#1080#1093#1110#1076
      TabOrder = 7
      OnClick = Button3Click
    end
    object Button4: TButton
      Left = 0
      Top = 48
      Width = 113
      Height = 17
      Caption = #1055#1088#1080#1093#1086#1074#1072#1090#1080' '#1087#1072#1085#1077#1083#1100
      TabOrder = 8
      OnClick = Button4Click
    end
    object Button5: TButton
      Left = 0
      Top = 64
      Width = 113
      Height = 17
      Caption = #1030#1085#1074#1077#1088#1089#1110#1103' '#1092#1086#1085#1091
      TabOrder = 9
      OnClick = Button5Click
    end
  end
  object MainMenu1: TMainMenu
    Left = 16
    Top = 240
    object N1: TMenuItem
      Caption = #1043#1086#1083#1086#1074#1085#1077' '#1084#1077#1085#1102' '#1083#1072#1073#1086#1088#1072#1090#1086#1088#1085#1086#1111' '#1088#1086#1073#1086#1090#1080
      object N2: TMenuItem
        Caption = #1055#1088#1086' '#1072#1074#1090#1086#1088#1072
        OnClick = N2Click
      end
      object N3: TMenuItem
        Caption = #1042#1080#1093#1110#1076' '#1079' '#1087#1088#1086#1075#1088#1072#1084#1080
        OnClick = N3Click
      end
    end
    object N4: TMenuItem
      Caption = #1060#1091#1085#1082#1094#1110#1111
      object N5: TMenuItem
        Caption = #1057#1090#1072#1088#1090
        OnClick = N5Click
      end
      object N7: TMenuItem
        Caption = #1055#1072#1091#1079#1072
        OnClick = N7Click
      end
      object N9: TMenuItem
        Caption = #1030#1085#1074#1077#1088#1089#1110#1103' '#1092#1086#1085#1091
        OnClick = N9Click
      end
      object N6: TMenuItem
        Caption = #1042#1074#1110#1084#1082#1085#1091#1090#1080'/'#1042#1080#1084#1082#1085#1091#1090#1080' '#1076#1086#1076#1072#1090#1082#1086#1074#1110' '#1092#1091#1085#1082#1094#1110#1111
        OnClick = N6Click
      end
    end
    object N8: TMenuItem
      Caption = #1056#1086#1079#1084#1072#1083#1102#1074#1072#1090#1080' '#1088#1110#1079#1085#1110' '#1075#1088#1072#1085#1110' '#1074#1080#1087#1072#1076#1082#1086#1074#1080#1084' '#1095#1080#1085#1086#1084
      OnClick = N8Click
    end
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 20
    OnTimer = Timer1Timer
    Left = 48
    Top = 240
  end
end
