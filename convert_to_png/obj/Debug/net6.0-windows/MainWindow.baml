   M S B A M L   `   `   `  ????I  Econvert_to_png, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:convert_to_pngconvert_to_png  5   	       convert_to_png.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:convert_to_png     5   	   -   5          ? Title$  资源转换器??5   	   $	??800??6   $
??1000??6$   .??5	      ? .+?5
   
   ?? -   5        $??
Background??5
      .+?5      ?? $	??787??6   $??/Image/背景.png??6!   $
??Fill=?68   $
??1000??6G   $??Center=?6T   $??Center=?6q    J? RenderTransformOrigin$ 0.5,0&?6?   z?5      V? 5      .~?5      ?? ?? 5      ?? 5      T? 5      5      5      5      ?? 5   
   -   5         ??Name$ btnStart??5      $??Right=?6#   $??31,-60,757,550q?6^   $ 0.839,0.512&?6v   ??5      ?? 5      .R?5      ?  ? ColumnDefinitions 5      ?? 5      ?? 5      .+?5      ?? $??90??6!   $??90??6,   $??/Image/开始.png??68   5       5!      5#      ? 5$   
   -   5        $??taskProgress??5$   P   $??20??6   $
??Left=?6"   $??	9,161,0,0q?6=   $	??Top=?6d   $	??243??6|   ?? 5%   
   $
??Left=?6   $	??Top=?6/   $??112,165,0,0q?6?   ??6G   ??  ?? ElementName$ taskProgress?? ?? Path$ Value? ?? StringFormat$ {0:0}%??6?   5&      5'      