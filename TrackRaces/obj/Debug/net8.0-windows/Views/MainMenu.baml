   M S B A M L   `   `   `  ����E  ATrackRaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TrackRaces.ViewModelsTrackRaces.ViewModels  5   	       TrackRaces.Views.MainMenu   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2vm#clr-namespace:TrackRaces.ViewModels     5   	   -   5          � Title$  MainMenu��5   	   $	��300��6   $	��440��6'    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   .��5      � .+�5   
   �� $��Player One Name:��6   $
��Left=�6+   $	��Top=�6F   $��	20,20,0,0q�6^   �� 5   
    �� 	MaxLength$ 20��60   $
��Left=�6?   $	��Top=�6Z   $	��200��6r   $��
150,20,0,0q�6~   ��6   �� *Player1.Name+6~   �� 5   
   $��Q | A D��6   $
��Left=�6#   $	��Top=�6>   $��
360,20,0,0q�6V   �� 5   
   $��Player Two Name:��6   $
��Left=�6+   $	��Top=�6F   $��	20,60,0,0q�6^   �� 5   
   $ 20��60   $
��Left=�6?   $	��Top=�6Z   $	��200��6r   $��
150,60,0,0q�6~   ��6   �� *Player2.Name+6~   �� 5   
   $��0 | ← →��6   $
��Left=�6#   $	��Top=�6>   $��
360,60,0,0q�6V   �� 5   
   $��Line Thickness:��6   $
��Left=�6*   $	��Top=�6E   $��
20,100,0,0q�6]   �� 5   
    � Minimum$ 5Q�6    � Maximum$ 20Q�6   $
��Left=�6W   $	��Top=�6r   $	��200��6�   $��150,100,0,0q�6�    � Value 6*   �� *GameSettings.LineThickness+6�   �� 5   
   $
��Left=�6@   $	��Top=�6[   $��360,100,0,0q�6s   ��6   �� *GameSettings.LineThickness+6s   �� 5   
   $��Line Speed:��6   $
��Left=�6&   $	��Top=�6A   $��
20,140,0,0q�6Y   �� 5   
   $ 1Q�6   $ 10Q�6   $
��Left=�6S   $	��Top=�6n   $	��200��6�   $��150,140,0,0q�6�    6*   �� *GameSettings.LineSpeed+6�   �� 5    
   $
��Left=�6<   $	��Top=�6W   $��360,140,0,0q�6o   ��6   �� *GameSettings.LineSpeed+6o   �� 5#   
   $��First to reach:��6   $
��Left=�6*   $	��Top=�6E   $��
20,180,0,0q�6]   �� 5$   
   $ 1Q�6   $ 15Q�6   $
��Left=�6U   $	��Top=�6p   $	��200��6�   $��150,180,0,0q�6�    6*   �� *GameSettings.TargetScore+6�   �� 5%   
   $
��Left=�6?   $	��Top=�6Z   $��360,180,0,0q�6r   ��6   �� *GameSettings.TargetScore+6r   �� 5(   
   -   5        $��
Start Game��5(      $��15(�6D   $��Center=�6R   $��Bottom=�6o   $	��150��6�   $��0,0,0,20q�6�   5)      5*      