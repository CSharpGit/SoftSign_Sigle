   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 !  MyScrollViewerControlTemplate  �� 
TargetType �Name � ColumnDefinitions � RowDefinitions ��Name ��Name �� CanContentScroll �� CanHorizontallyScroll �� CanVerticallyScroll	 ��Name4   .System.Windows.Automation.AutomationProperties
    AutomationId � Cursor �� ScrollableHeight � Maximum � Minimum* �� #ComputedVerticalScrollBarVisibility �� ViewportHeight �� ViewportSize  MyScrollBarStyle � Value �� Mode �� RelativeSource �� ScrollableWidth �� Orientation, �� %ComputedHorizontalScrollBarVisibility �� ViewportWidth  ScrollBarDisabledBackground �� Color  VerticalScrollBarPageButton �� 
TargetType �� Property �� Value  ScrollBarThumb �� RadiusX �� RadiusY �� Triggers  P� Property! P� Value" �� 
TargetName ! HorizontalScrollBarPageButton# J� SnapsToDevicePixels$ ]�Name% J� IsMouseOver& ]� DecreaseRepeatButton' �� PageUpCommand( ]� IncreaseRepeatButton) �� PageDownCommand* ]� Thumb+ �� Triggers, �� PageLeftCommand- �� PageRightCommand�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %  &	        &	 �    &	 �    &	 G    &	 �    &	     7  7  7  7  7  7  7  7  �� 5      "  ��6<   .R�5   
   � $
 Grid��6   #��z��6    5      �� 5      $��* �6#   �� 5      $
��Auto �6#    5
      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $ Corner��6   $��1��6(   
�������$��1��6E   �� 5      $! PART_ScrollContentPresenter��6%   # z��6J    .   .  #��z��6�   #��z��6�   $��0��6  #��z��6"  $��0��6E  �� 5      $	 PART_VerticalScrollBar��6   $
 VerticalScrollBar��68   $ Arrow��6n   $��1��6}   # z 6�   $ 0Q�6�   $��0��6�   #y�z 6�   # z 6u  #���  6�   6  �� *VerticalOffset+$ OneWay=� �� *TemplatedParent+6�  �� 5      $	 PART_HorizontalScrollBar��6   $
 HorizontalScrollBar��6:   $ Arrow��6r   $��0��6�   # z 6�   $ 0Q�6�   $ 
Horizontal=�6�   $��1��6�   #y�z 6�   # z 6�  #���  6�   64  �� *HorizontalOffset+$ OneWay=� �� *TemplatedParent+6�  5      5      �� 5      $ #F4F4F4��6:   �� 5      " ��60   .�5   
   ��  �@�OverridesDefaultStyle . 62   �� 5   
    � ��
 ���� 6'   �� 5   
    � |� .  6&   �� 5   
    � �� .  6&   �� 5   
    � �� 5      �� 5      "  ��6"   .R�5      �� #��z��6    #��z��6D   #��z��6f   5      5!      5"      �� 5#      " n�6#   .�5$   
   ��  �@�OverridesDefaultStyle . 62   �� 5%   
    � �� .  6&   �� 5&   
    � �� 5'      �� 5(      "  n�6"   .R�5)      �� $��	thumbRect��6    
����d�$ 3��6G   $ 3��6S    5*      P� 5+        �@J�IsMouseOver! . . �5,      �� $" 	thumbRect��6B    � ��
 ��  �5-      5/      51      52      �� 53      " ��62   .�54   
   ��  �@�OverridesDefaultStyle . 62   �� 55   
    � ��
 ���� 6'   �� 56   
    � |� .  6&   �� 57   
    � �� .  6&   �� 58   
    � �� 59      �� 5:      "  ��6"   .R�5;      �� #��z��6    #��z��6D   #��z��6f   5<      5>      5?      �� 5@      " ��6%   .�5A   
   ��  � ��
 �����6'   �� 5B   
    �@��IsPressAndHoldEnabled .  69   �� 5C   
    �@��IsFlicksEnabled .  63   �� 5D   
    � ��$ 6��6"   �� 5E   
    � ��$ 6��6%   �� 5F   
    � �� 5G      �� 5H      "  ��6"   .R�5I      � $ Bg��6   
�������# . $��6��6Y    5J      �� 5K      .+�5M      ]� $$ 
PART_Track��6    ��. #{�z% 6O   & 5N      �� 5O   "   #��Z' 6/   8��  6\   ( 5Q      �� 5R   "   #��Z) 6/   8�� 6^   * 5T      n� 5U   "   8�� 6(   5W      5X       5Y      P� 5Z        � {�! .  . �5[      �� $" Bg��6;   8   � ��6K   5\      5^      5`      + 5a   
   P� 5b        �@��Orientation$! 
Horizontal=�6-   . �5c      ��  � ��$
 Auto��6*   �� 5d       � ��$ 0��6-   �� 5e       � ��$ 6��6+   �� 5f       � ��$ 6��6.   �� 5g       � ��
 �����6/   �� 5h       � �� 5i      �� 5j      "  ��6*   .R�5k      � $ Bg��6#   
�������# .  5l   "   �� 5m   &   .+�5o   "   ]� $$ 
PART_Track��6(   #{�z% 6=   & 5p   &   �� 5q   *   #��Z, 67   8�� 6f   ( 5s   &   �� 5t   *   #��Z- 67   8�� 6g   * 5v   &   n� 5w   *   8�� 60   5y   #   5z       5{      P� 5|   "     � {�! .  . �5}   &   �� $" Bg��6C   8   � ��6S   5~   #   5�      5�      5�      5�      5�      