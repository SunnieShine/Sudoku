﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Data;

var grid = SudokuGrid.Parse("594000+82+70+7090016+40+600043+5+90+500000809060802+750800000+90009600+5+3+2+637002+9+1+802+5000746:821 223 325 326 831 233 134 135 241 344 345 346 261 364 365 366 171 475");
var dic = new Dictionary<int, IList<int>>();
foreach (int candidate in grid)
{
	if (dic.ContainsKey(candidate / 9))
	{
		dic[candidate / 9].Add(candidate);
	}
	else
	{
		dic.Add(candidate / 9, new List<int> { candidate });
	}
}

foreach (var candidates in from pair in dic orderby pair.Key ascending select pair.Value)
{
	Console.WriteLine(new Candidates(candidates).ToString());
}

#if FILE_COUNTER || false
using System;
using System.IO;
using Sudoku.Diagnostics;
using static System.Console;

string root = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName;

WriteLine(new FileCounter(root, "cs", withBinOrObjDirectory: false).CountUp());
#endif
