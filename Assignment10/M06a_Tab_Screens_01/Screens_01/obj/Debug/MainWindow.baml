   M S B A M L   `   `   `  ����E  AScreens_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Screens_01
Screens_01  5   	       Screens_01.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *localclr-namespace:Screens_01     5   	     � Title$  Person Manager��5   	   $	��700��6    $
��1000��6-    � DataContext 5         Screens_01.MainViewModel  5   
   .��5	      �  � RowDefinitions 5
   
   �� 5      $��2* �6   �� 5      $��3* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  $��0��6      AutoGenerateColumns .  $��10q�5      ��5      �� *People+ �� SelectedItem 5      �� *Person+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
First Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	FirstName+  5      $ 	Last Name��6%    69   �� *
LastName+  5      $ 
Occupation��6%    69   �� *SelectedOccupation+  5      $ Hobby��6%    69   �� *SelectedHobby+5      �� 5   
   $��1��6   $��10q�6L   	 �� SelectedIndex	 6"   �� *CurrentTabIndex+.@�5      �� $��
Basic Info��6   .��5         Screens_01.BasicInfoTab  5       �� 5!      $��Preferences��6   .��5"         Screens_01.PreferencesTab  5#      �� 5$      $��
Face Maker��6   .��5%         Screens_01.FaceMakerTab  5&      5'      �� 5)   
   $��2��6   
 �� Orientation$
 
Horizontal=�6"   $��Center=�6;   $��10q�6X   .+�5*      �� $	��New��6   $��5q�6R   ��6(   �� *
NewCommand+6R   �� 5+      $
��Save��6   $��5q�6R   ��6(   �� *SaveCommand+6R   �� 5,      $��Delete��6   $��5q�6R   ��6(   �� *DeleteCommand+6R   �� 5-      $��Previous��6   $��5q�6R   ��6(   �� *PreviousRecordCommand+6R   �� 5.      $
��Next��6   $��5q�6R   ��6(   �� *NextRecordCommand+6R   �� 5/      $	��150��6   $��5q�6C   ��6#   �� *SearchLastName+6C   �� 50      $��Search��6   $��5q�6R   ��6(   �� *SearchCommand+6R   51      52      53      