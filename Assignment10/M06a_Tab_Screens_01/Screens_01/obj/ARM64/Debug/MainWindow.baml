   M S B A M L   `   `   `  ����E  AScreens_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Screens_01
Screens_01  5   	       Screens_01.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Screens_01     5   	     � Title$  Person Data Entry��5   	   $	��800��6#   $	��800��60    � DataContext 5	         Screens_01.MainViewModel  5
   
   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� SelectedIndex 6"   �� *CurrentTabIndex+.@�5      �� $��
Basic Info��6   .��5         Screens_01.BasicInfoTab   6%   �� 5      �� 5      $��Preferences��6   .��5         Screens_01.PreferencesTab   6'   �� 5      �� 5      $��
Face Maker��6   .��5         Screens_01.FaceMakerTab   6%   �� 5      �� 5      $��Review��6   .��5         Screens_01.ReviewTab   6"   �� 5       5!      �� 5#   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��10q�6X   .+�5$      �� $��Previous��6   $��5q�5%      $
��20,5q�6    ��5$   (   �� *PreviousCommand+{�5%   /   �� *	CanGoBack+�� 5&      $
��Next��6   $��5q�5'      $
��20,5q�6    ��5&   $   �� *NextCommand+{�5'   /   �� *CanGoForward+5(      5)      5*      