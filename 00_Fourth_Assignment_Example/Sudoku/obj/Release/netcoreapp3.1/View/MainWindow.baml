   M S B A M L   `   `   `  ���� 
  Sudoku!    ELTE.Sudoku.View.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Sudoku játék��$	��450��$	��400��$	��450��$	��400��.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�o� $��0��.@�m� $��Fájl��.@�m� $��Új játék�� m� Command �� *NewGameCommand+�� m� $��Játék betöltése...�� �� *LoadGameCommand+m� $��Játék mentése...�� �� *SaveGameCommand+�� m� $��	Kilépés�� �� *ExitCommand+m� $��Beállítások��.@�m� $��Könnyű játék�� m� IsCheckable$
 True�� m� 	IsChecked �� *
IsGameEasy+m� $��Közepes játék��$
 True�� �� *IsGameMedium+m� $��Nehéz játék��$
 True�� �� *
IsGameHard+�� $��1������ *Fields+���� .R�A�  A� Rows$ 9�� A� Columns$ 9������ .R��� $|�False�� J� RenderTransformOrigin$ 0.5, 0.5&�$��24(�$
��Bold"����� *StepCommand+���� *Number+���� *Text+z���  �� ScaleX$ 1Q�	 �� ScaleY$	 1Q����� 
 �� 
TargetType"
 �� �� Triggers ��  �� Binding �� *
IsLocked+ �� Value True.a���  �� Property � �� �� Value
 � �����  �� *
IsLocked+	 False.a���  � ��
 ����� � Triggers :�  :� RoutedEvent$ Button.Loaded��.[��� .���� .��W�  g� Duration$ 0:00:02D� W� From 0	 W� To 1 �� TargetProperty$ Opacity�:� $ Button.Click��.[��� .���� .���� $ 	0:00:00.2D�$* $(Foreground).(SolidColorBrush.Color)�g�p�  �� Value$	 Red�� �� KeyTime$ 0:00:00��p� $ Black��$ 	0:00:00.2��W� $ 	0:00:00.1D� 1 0.5 g� AutoReverse$
 True��$/ )(RenderTransform).(ScaleTransform.ScaleX)�W� $ 	0:00:00.1D� 1 0.5$
 True��$/ )(RenderTransform).(ScaleTransform.ScaleY)����� .���  � �� �� *X+��  � �� �� *Y+�� $��2��.@��� .���� $��Lépésszám: ���� .���� ���� *GameStepCount+�� �� .���� $��Játékidő: ���� .���� ���� *
GameTime+�� �� 