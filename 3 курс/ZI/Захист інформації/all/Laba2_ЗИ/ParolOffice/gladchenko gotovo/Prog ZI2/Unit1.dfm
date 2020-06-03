object Form1: TForm1
  Left = 610
  Top = 264
  Width = 291
  Height = 219
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
    Left = 8
    Top = 8
    Width = 49
    Height = 13
    Caption = 'Password:'
  end
  object Label2: TLabel
    Left = 8
    Top = 24
    Width = 43
    Height = 13
    Caption = 'Speed: 0'
  end
  object Button1: TButton
    Left = 24
    Top = 136
    Width = 81
    Height = 41
    Caption = 'Start'
    TabOrder = 0
    OnClick = Button1Click
  end
  object RadioButton1: TRadioButton
    Left = 64
    Top = 40
    Width = 113
    Height = 25
    Caption = 'Numerous'
    Checked = True
    TabOrder = 1
    TabStop = True
  end
  object RadioButton2: TRadioButton
    Left = 64
    Top = 64
    Width = 113
    Height = 25
    Caption = 'Letters'
    TabOrder = 2
  end
  object RadioButton3: TRadioButton
    Left = 64
    Top = 88
    Width = 113
    Height = 25
    Caption = 'By list'
    TabOrder = 3
  end
  object BitBtn1: TBitBtn
    Left = 176
    Top = 136
    Width = 89
    Height = 41
    TabOrder = 4
    OnClick = BitBtn1Click
    Kind = bkClose
  end
  object Timer1: TTimer
    Enabled = False
    OnTimer = Timer1Timer
    Left = 232
    Top = 40
  end
end
