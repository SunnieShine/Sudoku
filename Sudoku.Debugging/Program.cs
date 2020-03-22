﻿using Sudoku.Data;
using Sudoku.Solving.Manual;
using static System.Console;

namespace Sudoku.Debugging
{
	/// <summary>
	/// The class aiming to this console application.
	/// </summary>
	internal static class Program
	{
		/// <summary>
		/// The main function, which is the main entry point
		/// of this console application.
		/// </summary>
		private static void Main()
		{
			// Manual solver tester.
			var solver = new ManualSolver
			{
				CheckAlmostLockedQuadruple = true,
				OnlySaveShortestPathAic = true,
				UseCalculationPriority = false,
				FastSearch = true
			};
			var grid = Grid.Parse(
				"8..4...3...5..18...7.38...568...3.....91..6....42....3.2..7...4...6...5......49..");
			var analysisResult = solver.Solve(grid);
			WriteLine($"{analysisResult:-#!.}");
		}
	}
}
