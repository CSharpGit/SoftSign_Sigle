   M S B A M L   `   `   `  ����  SoftSign_Sigle    SoftSign_Sigle.Appgame   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Appgame��5   	   $
��1024��6   $
��7200��6'    � 	Resources 5         EnemyTemplate �� 
TargetType J� RenderTransformOrigin��%�   &	        �� 5   
   " ��60   .R�5      � $
��Auto��6   $
��Auto��6!   $ 0.583,0.674&�6.   .+�5      A� $
��Auto��6   $��42��6(   ���	#FFE81D1D$��19,0,23,-40q�6D   5	      5
      5      .��5      � -   5         �Name$
 grid��5   >   
���   �$ 0.5,0.5&�6    � ColumnDefinitions 5   
   �� 5      $	��160 �6   �� 5      �� 5      $	��160 �6    � RowDefinitions 5   
   �� 5      $��55 �6   �� 5      �� 5      $��90 �6   .+�5   
   �� -   5         ��Name$ starttButton��5   "   $��Start!��6   $��Center=�68   $��61,33,37,26q�6U   $��2��6j   $��Center=�6w   $��31��6�   $��62��6�   6�   �� 5   
   $��2��6   $��	14,8,0,36q�6%    �� Orientation$ Vertical=�68   $��2��6O   .+�5      �� $
��Left=�6   $
��Wrap=�63   $��Avoid THese��6G   
�������$	��Top=�6m   $��16(�6�   �� 5      $��ContentControl��6   $
��Left=�66   $��0q�6Q   $	��Top=�6\   #���   6t   5      � 5   
   -   5        	 �Name$	 
processBar��5      $��1��6*   $��2��6:   $
��Left=�6G   $��	0,34,0,36q�6c   $	��316��6v   �� 5   
   -   5        
 ��Name$
 playArea��5      $��3��6#   $��1��67   $��Stretch=�6D   $��Stretch=�6`   $
��Auto��6~   $
��Auto��6�   ��5      �� 5        �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5!      �  � Color$ Black��6#    � Offset$ 0Q�61   � 5"      $ White��6#   $ 1Q�61   5#      .+�5%      �� -   5         ��Name$ gameOverText��5%      $��	Game Over��6.   $��Arial Black)�6?   $��60(�6X   $��Italic$�6f   
������� �� Left$	 145��6�   
 �� Top$ 45��6�   5'      �� 5(      -   5         ��Name$ target��5(      $��35��6(   $��38��64   $	 405��6?   $ 0.5,0.5&�6Q   $ 15��6q   z�5)      V� 5*      .~�5+      �� �� 5,      �� 5-       �� Angle$ 45Q�6*   T� 5.      5/      ��51      �� 52      $ 0.5,1&�6*   $ 0.5,0&�6;   .��53      � $ Black��6'   $ 0Q�65   � 54      $ 	#FFF55454��6'   $ 1Q�69   55      57      �� 58      -   5         ��Name$ human��58      $ Vertical=�6(   $
��Auto��6@   $
��Auto��6N   .+�59      A� 
�������$��10��6'   $��10��63   �� 5:      
�������$��25��6)   $��10��65   5;      5<      5>      5?      