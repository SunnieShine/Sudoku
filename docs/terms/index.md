# 术语词列表

本页面列举常见的数独相关术语词汇。按术语词描述的功能进行排序。

* **数值取值相关**
  * **[提示数](given)、已知数（Given/Known）**：表示一个题目里的提示信息，作为初始情况下存在的数字，用于逻辑推理得到剩下的数字，是基础情况下就存在的数字信息。
  * **[填入数](modifiable)（Modifiable）**：表示一个题目经过逻辑推理得到了的填入的数字。在最开始不包含它们，是后面得到的数字信息，它们也可用于逻辑推理得到后面的结果。
  * **[确定值](value)、符号、数字（Value/Symbol/Digit）**：表示提示数和填入数的统称。
  * **[候选数](candidate)（Candidate）**：表示一个空格里可以填入的数字的情况可能性。一个空格最多可以包含 9 个候选数。
* **基本元素**
  * **[行](row)（Row）**：表示盘面按横排分布的 9 个单元格。
  * **[列](column)（Column）**：表示盘面按竖列分布的 9 个单元格。
  * **[宫](block)（Block/Box）**：表示盘面按粗线分隔起来的 9 组单元格，每一组都有 9 个单元格，分布为 3×3 的正方形单元格序列。
  * **[行列](line)（Line）**：表示一个行和一个列。为行和列的单位量的统称。
  * **[单元格](cell)（Cell）**：表示一个在题目里可以填入数字的基本单位。
  * **[区域](house)、家庭（Region/House/Family）**：行、列、宫的统称。
  * **[大行](floor)（Floor）**：表示横着并排的三个宫的所有单元格构成的集合。
  * **[大列](tower)（Tower）**：表示竖着并排的三个宫的所有单元格构成的集合。
  * **[大行列](chute)（Chute）**：表示并排的三个宫的所有单元格集合。
  * **[小行](boxrow)、宫行（Boxrow/Minirow）**：表示一个盘面里某一个宫和某一个行相交的三个单元格。
  * **[小列](boxcolumn)、宫列（Boxcolumn/Minicolumn）**：表示一个盘面里某一个宫和某一个列相交的三个单元格。
  * **[相关单元格](peer)（Peer/Buddy）**：表示某个单元格所在的行、列、宫里的别的单元格构成的一组单元格序列。
* **结构性术语**
  * **[共轭对](conjugate-pair)（Conjugate Pair）**：同一个区域下，只有两处可以填入同一个数字的单元格。
  * **[严格共享候选数](restricted-common-candidate)（Restricted Common Candidate）**：用于约束 ALS 链式技巧之中的数值间的弱关系。
  * [节点](node)、结点（Node/Potential）：用于链的连接，是链的重要构成元素。
* **题目性质术语**
  * **[最小题](minimal-puzzle)、精简题（Minimal Puzzle）**：题目之中的任何一个提示数都不可缺少。
  * **[一刀流题](ittouryu-puzzle)（Ittouryu Puzzle）**：题目可按照数字 1 到 9 次序完成，不跳数字。
  * **[后门](backdoor)（Backdoor）**：可以大幅度降低题目难度的出数或删数结论。
  * **魔术格（Magic Cell）**：魔术格就是出数结论的后门，所用到的单元格。

