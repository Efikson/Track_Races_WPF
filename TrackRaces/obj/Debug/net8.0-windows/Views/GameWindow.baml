   M S B A M L   `   `   `  ����E  ATrackRaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TrackRaces.ViewModelsTrackRaces.ViewModels  5   	   !    TrackRaces.Views.GameWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2vm#clr-namespace:TrackRaces.ViewModels     5   	   -   5          � Title$  
GameWindow��5   	    � SizeToContent$ WidthAndHeight=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $	��100 �6   �� 5      $��60 �6   �� 5      $	��100 �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��750 �6   �� 5      $	��150 �6   .+�5   
   �� -   5         ��Name$ 
gameCanvas��5      $��0��6%   $��0��62   $��8��6B   
�������$	��500��6f   .+�5      �� 
��� � �$��10��5!      ��5      �� *ActualWidth+ �� RelativeSource ��  �� AncestorType
 Canvas��5       �� *ActualHeight+ �� 
 Canvas5!      �� 5#      -   5        	 ��Name$	 CountdownValue��5#      $��40(�5$      $��Center=�5%      $��Center=�5&      $y�Visible=�5'      
 �� Left$	
 375��5)      
 �� Top$	 220��5*      ��5(      �� *CountdownValue+5*      5+      �� 5/   
   $��0��6   $��1��6"   $��center=�62   $��	0,10,0,10q�6O   .+�50      �� $	��Red��6   
���  ��$��Center=�63   $��18(�6P   �� 51      $��Center=�65   ��6   �� *Player1.Name+65   �� 52      $��Center=�6   	Score: �� 53      �6   �� *Player1.Score+54      �� 55      $��Jump collected��6   $��Center=�6\    d� 	IsChecked 60   �� *Player1.JumpCollected+6\   56      �� 59   
   $��1��6   $��1��6"   $��center=�62   $��	0,10,0,10q�6O   .+�5:      �� $
��Blue��6   
����  �$��Center=�65   $��18(�6R   �� 5;      $��Center=�65   ��6   �� *Player2.Name+65   �� 5<      $��Center=�6   	Score: �� 5=      �6   �� *Player2.Score+5>      �� 5?      $��Jump collected��6   $��Center=�6\    60   �� *Player2.JumpCollected+6\   5@      �� 5C   
   $��2��6   $��1��6"   $��center=�62   $��	0,10,0,10q�6O   .+�5D      �� -   5        $��	New Round��5D      $��Center=�6F   $��0,0,0,10q�6c   �� 5E      -   5        $��Return to Menu��5E      $��Center=�6I   5F      �� 5I   
   $��3��6   $��1��6"   $��center=�62   $��	0,10,0,10q�6O   .+�5J      �� $��Center=�6   First to reach: �� 5K   "   �6&   �� *GameSettings.TargetScore+5L      �� 5M      $��Time Until Bonus��6   $��Center=�6/   �� 5N      $��Center=�67   $��20(�6T   ��6   �� *TimeUntilBonus+6T   5O      5P      5R      