   M S B A M L   `   `   `  ����
  Sudoku!    ELTE.Sudoku.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Sudoku játék��5   	   $	��450��6   $	��400��6+   $	��450��67   $	��400��6G   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   o� $��0��6   .@�5      m� $��Fájl��6   .@�5      m� $��Új játék��6    m� Command 6-   �� *NewGameCommand+�� 5      m� 5      $��Játék betöltése...��6    67   �� *LoadGameCommand+m� 5      $��Játék mentése...��6    65   �� *SaveGameCommand+�� 5      m� 5      $��	Kilépés��6    6,   �� *ExitCommand+5      m� 5      $��Beállítások��6   .@�5      m� $��Könnyű játék��6    m� IsCheckable$
 True��61    m� 	IsChecked 6D   �� *
IsGameEasy+m� 5      $��Közepes játék��6   $
 True��62    6E   �� *IsGameMedium+m� 5      $��Nehéz játék��6   $
 True��60    6C   �� *
IsGameHard+5      5      �� 5   
   $��1��6   ��6$   �� *Fields+��5      �� 5      .R�5       A�  A� Rows$ 9��6"    A� Columns$ 9��6+   5!      ��5#      �� 5%      .R�5&      �� $|�False��6z    J� RenderTransformOrigin$ 0.5, 0.5&�6�   $��24(�6�   $
��Bold"�6�   ��6   �� *StepCommand+��6=   �� *Number+��6a   �� *Text+z�5)      �� 5*       �� ScaleX$ 1Q�6-   	 �� ScaleY$	 1Q�68   ��5,      �� 5.      
 �� 
TargetType"
 ��6$    �� Triggers 5/   "   �� 51   &    �� Binding 62   �� *
IsLocked+ �� Value True6O   .a�52   *   ��  �� Property � �� �� Value
 � ���6G   53   '   �� 54   &    62   �� *
IsLocked+	 False6O   .a�55   *   ��  � ��
 �����6G   56   '   58       � Triggers 5:      :� 5<       :� RoutedEvent$ Button.Loaded��6+   .[�5=   "   �� .��5>   &   �� .��5?   *   W�  g� Duration$ 0:00:02D�6:    W� From 06M   	 W� To 16V    �� TargetProperty$ Opacity�6]   5@   '   5A   #   5B      :� 5C      $ Button.Click��6+   .[�5D   "   �� .��5E   &   �� .��5F   *   �� $ 	0:00:00.2D�6G   $* $(Foreground).(SolidColorBrush.Color)�6\   g�5H   2   p�  �� Value$	 Red��6H    �� KeyTime$ 0:00:00��6T   p� 5I   2   $ Black��6H   $ 	0:00:00.2��6V   5K   +   W� 5L   *   $ 	0:00:00.1D�6:    16O    0.56X    g� AutoReverse$
 True��6a   $/ )(RenderTransform).(ScaleTransform.ScaleX)�6t   W� 5M   *   $ 	0:00:00.1D�6:    16O    0.56X   $
 True��6a   $/ )(RenderTransform).(ScaleTransform.ScaleY)�6t   5N   '   5O   #   5P      5R      5S      ��5U      �� 5W      .�5X      ��  � �� 61   �� *X+�� 5Y       � �� 64   �� *Y+5Z      5\      �� 5]   
   $��2��6   .@�5_      �� .��5`      �� $��Lépésszám: ��6   5a      �� 5b      .��5c      �� ��6   �� *GameStepCount+5d      �� 5e      �� 5f      .��5g      �� $��Játékidő: ��6   5h      �� 5i      .��5j      �� ��6   �� *
GameTime+5k      �� 5l      �� 5m      5n      5o      5p      