### 代码简述

顶层文件：`EventController.cs`

parse文件:  `Parser.cs` , `FOLD.cs`

与折纸相关的类：`Beam.cs`, `Face.cs`, `Node.cs`

Debug类：`Debug.cs`

画面片修改的类：`MethodExtensionForUnity.cs`, `Triangulator.cs`



其中力的计算相关代码在 `EventController.cs`, `Beam.cs`, `Face.cs`, `Node.cs` 中



### 调试

使用VS+unity可以比较方便的调试（尤其是这个项目涉及到的数学计算比较多）

1. 在VS中`附加unity调试程序`

   ![1560234955625](.\img\1560234955625.png)

2. 选择unity端口

   ![1560235020858](.\img\1560235020858.png)

3. 在unity中运行项目
4. 代码会在VS中打的断点停下来，然后就可以去找自己想看的变量值了