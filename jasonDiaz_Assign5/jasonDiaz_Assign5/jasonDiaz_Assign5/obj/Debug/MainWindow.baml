   M S B A M L   `   `   `  ����L  HjasonDiaz_Assign5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:jasonDiaz_Assign5jasonDiaz_Assign5  5   	   "    jasonDiaz_Assign5.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:jasonDiaz_Assign5     5   	   -   5          � Title$!  Country GUI v1 - Jason Diaz��5   	   $	��450��6-   $��700.1��6:   .��5	      � .+�5
   
   o� $��25��6   $	��Top=�6   .@�5      m� $��14(�6   $
��File��6%   .@�5      m� -   5        $��14(�5      $
��Exit��6)   67   5      m� 5      $��14(�6   $��Import��6%   .@�5      m� -   5        $��14(�5      $��Countries from JSON...��6)   6I   5      m� 5      $��14(�6   $
��Help��6%   .@�5      m� -   5        $��About...��5      6-   5      5      �� 5   
   $��Country��6   $
��Left=�6"   $��	10,50,0,0q�6=   $	��Top=�6P   $��14(�6h   �� 5   
   -   5         ��Name$ listBox��5      $
��Left=�6#   $��
11,84,0,10q�6>   $	��300��6R   6^   �� 5   
   $
��Name��6   $
��Left=�6   $��315,110,0,0q�6:   $	��Top=�6O   $��16(�6g   �� 5   
   -   5         ��Name$ textboxRegion��5      $��23��6)   $��418,183,10,0q�65    �� TextWrapping$
 Wrap=�6K   $	��Top=�6_   $��16(�6w    �� TextAlignment$ Justify=�6�    � ToolTip$ Selected Country's Region��6�    �� 
IsReadOnly . 6�   �� 5   
   $��Capital��6   $
��Left=�6"   $��315,144,0,0q�6=   $	��Top=�6R   $��16(�6j   �� 5   
   $��Region��6   $
��Left=�6!   $��315,177,0,0q�6<   $	��Top=�6Q   $��16(�6i   �� 5   
   -   5        $ textboxSubregion��5      $��23��6,   $��418,217,10,0q�68   $
 Wrap=�6N   $	��Top=�6b   $��16(�6z   $ Justify=�6�   $" Selected Country's Subregion��6�    . 6�   �� 5   
   $��	Subregion��6   $
��Left=�6$   $��315,213,0,0q�6?   $	��Top=�6T   $��16(�6l   �� 5   
   $��
Population��6   $
��Left=�6%   $��315,247,0,0q�6@   $	��Top=�6U   $��16(�6m   �� 5   
   -   5        $ textboxName��5      $��23��6'   $��418,114,10,0q�63   $
 Wrap=�6I   $	��Top=�6]   $��16(�6u   $ Justify=�6�   $ Selected Country's Name��6�    . 6�   �� 5   
   -	   5        $ textboxCapital��5      $��23��6*   $��418,148,10,0q�66   $
 Wrap=�6L   $	��Top=�6`   $��16(�6x   $ Justify=�6�    . $  Selected Country's Capital��6�   �� 5    
   -
   5        $ textboxPopulation��5       $��23��6-   $��418,251,10,0q�69   $
 Wrap=�6O   $	��Top=�6c   $��16(�6{   $ Justify=�6�    . $# Selected Country's Population��6�   5"      5#      