   M S B A M L   `   `   `  ����E  ACryptoInfo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CryptoInfo.ViewModelCryptoInfo.ViewModel  5   	   5clr-namespace:CryptoInfo.ModelsCryptoInfo.Models  5   	   'clr-namespace:CryptoInfo
CryptoInfo  5   	   W0clr-namespace:CryptoInfo.Infrastructure.Commands"CryptoInfo.Infrastructure.Commands  5	   	       CryptoInfo.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1vm"clr-namespace:CryptoInfo.ViewModel     5   	   0crypclr-namespace:CryptoInfo.Models     5   	   &lclr-namespace:CryptoInfo     5   	   @cmd0clr-namespace:CryptoInfo.Infrastructure.Commands     5	   	   $	��600��5   	   $	��920��6     � WindowStyle$
  None=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   .��5      �� ���#272537 �� CornerRadius$ 20��5       � DataContext 5   
   .  (CryptoInfo.ViewModel.MainWindowViewModel  5       � 	Resources 5   
   @  :CryptoInfo.Infrastructure.Commands.CloseApplicationCommand   CloseApplicationCommand��%   &	          5      6L   5       J� InputBindings 5   
   �� 5       �� 	Modifiers$ Controlf�6   
 �� Key$ Q��6-   	 �� Command#	 [  65   6j   ._�5   
   � 
 � ColumnDefinitions
 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5"      �� 5#      $��75 �6    �� 5$      .+�5'      �� $��Center=�5(      $��Center=�5)      
�������$��24(�5+      ��5'      �� *Title+5+      �� 5-      $��1��6   .+�5.      	� -   5        $
��Home��5.       d� 	IsChecked$
 TrueY�5/      $��50��50      
�������$��14(�52        MenuButtonTheme#��[ 54      ��53      �� *HomeViewCommand+54   G   	� 56      $��Cryptocurrency info��6   $��50��57      
�������$��14(�59      #��[ 5;      ��5:      �� *InfoViewCommand+5;      	� 5=      $
��Exit��6   $��50��5>      
�������$��14(�5@      $��Bottom=�5A      #��[ 5C      ��5B      �� *CloseApplicationCommand+5C      5D      �� 5F      $��1��6   $��0��5G      $��2��5H      $��10q�5I      ��5J      �� *CurrentView+ �� Mode$ TwoWay=�5K      5L      5M      