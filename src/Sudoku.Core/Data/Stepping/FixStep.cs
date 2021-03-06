﻿using System;
using static Sudoku.Data.SudokuGrid;

namespace Sudoku.Data.Stepping
{
	/// <summary>
	/// Encapsulates a fix step.
	/// </summary>
	/// <param name="AllCells">Indicates all cells to fix.</param>
	[Obsolete("In the future, this type won't be used.", false)]
	public sealed record FixStep(in Cells AllCells) : IStep
	{
		/// <inheritdoc/>
		public unsafe void DoStepTo(UndoableGrid grid)
		{
			fixed (short* pGrid = grid)
			{
				foreach (int cell in AllCells)
				{
					pGrid[cell] = (short)(GivenMask | pGrid[cell] & MaxCandidatesMask);
				}
			}
		}

		/// <inheritdoc/>
		public unsafe void UndoStepTo(UndoableGrid grid)
		{
			fixed (short* pGrid = grid)
			{
				foreach (int cell in AllCells)
				{
					pGrid[cell] = (short)(ModifiableMask | pGrid[cell] & MaxCandidatesMask);
				}
			}
		}
	}
}
