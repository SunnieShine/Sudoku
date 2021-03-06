﻿using System.Collections.Generic;
using Sudoku.Data;
using Sudoku.DocComments;
using Sudoku.Techniques;
using static Sudoku.Solving.Manual.FastProperties;

namespace Sudoku.Solving.Manual.Uniqueness.Polygons
{
	/// <summary>
	/// Encapsulates a <b>Borescoper's deadly pattern</b> technique searcher.
	/// </summary>
	public sealed partial class BdpStepSearcher : UniquenessStepSearcher
	{
		/// <summary>
		/// All different patterns.
		/// </summary>
		/// <remarks>
		/// All possible heptagons and octagons are in here.
		/// </remarks>
		private static readonly Pattern[] Patterns = new Pattern[14580];


		/// <inheritdoc cref="SearchingProperties"/>
		public static TechniqueProperties Properties { get; } = new(17, nameof(Technique.BdpType1))
		{
			DisplayLevel = 2
		};


		/// <inheritdoc/>
		public override void GetAll(IList<StepInfo> accumulator, in SudokuGrid grid)
		{
			if (EmptyMap.Count < 7)
			{
				return;
			}

			for (int i = 0, end = EmptyMap.Count == 7 ? 14580 : 11664; i < end; i++)
			{
				var pattern = Patterns[i];
				if ((EmptyMap & pattern.Map) != pattern.Map)
				{
					// The pattern contains non-empty cells.
					continue;
				}

				short cornerMask1 = 0, cornerMask2 = 0, centerMask =0;
				foreach (int cell in pattern.Pair1Map)
				{
					cornerMask1 |= grid.GetCandidates(cell);
				}
				foreach (int cell in pattern.Pair2Map)
				{
					cornerMask2 |= grid.GetCandidates(cell);
				}
				foreach (int cell in pattern.CenterCellsMap)
				{
					centerMask |= grid.GetCandidates(cell);
				}

				CheckType1(accumulator, grid, pattern, cornerMask1, cornerMask2, centerMask, pattern.Map);
				CheckType2(accumulator, grid, pattern, cornerMask1, cornerMask2, centerMask, pattern.Map);
				CheckType3(accumulator, grid, pattern, cornerMask1, cornerMask2, centerMask, pattern.Map);
				CheckType4(accumulator, grid, pattern, cornerMask1, cornerMask2, centerMask, pattern.Map);
			}
		}

		partial void CheckType1(IList<StepInfo> accumulator, in SudokuGrid grid, in Pattern pattern, short cornerMask1, short cornerMask2, short centerMask, in Cells map);
		partial void CheckType2(IList<StepInfo> accumulator, in SudokuGrid grid, in Pattern pattern, short cornerMask1, short cornerMask2, short centerMask, in Cells map);
		partial void CheckType3(IList<StepInfo> accumulator, in SudokuGrid grid, in Pattern pattern, short cornerMask1, short cornerMask2, short centerMask, in Cells map);
		partial void CheckType4(IList<StepInfo> accumulator, in SudokuGrid grid, in Pattern pattern, short cornerMask1, short cornerMask2, short centerMask, in Cells map);
	}
}
