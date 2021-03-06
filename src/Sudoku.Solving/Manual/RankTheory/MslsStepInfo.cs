﻿using System.Collections.Generic;
using Sudoku.Data;
using Sudoku.Data.Collections;
using Sudoku.Drawing;
using Sudoku.Techniques;
using static System.Math;

namespace Sudoku.Solving.Manual.RankTheory
{
	/// <summary>
	/// Provides a usage of <b>multi-sector locked sets</b> technique.
	/// </summary>
	/// <param name="Conclusions">All conclusions.</param>
	/// <param name="Views">All views.</param>
	/// <param name="Cells">Indicates the cells used.</param>
	public sealed record MslsStepInfo(
		IReadOnlyList<Conclusion> Conclusions, IReadOnlyList<View> Views, in Cells Cells)
		: RankTheoryStepInfo(Conclusions, Views)
	{
		/// <inheritdoc/>
		public override decimal Difficulty => 9.4M + (decimal)Floor((Sqrt(1 + 8 * Cells.Count) - 1) / 2) * .1M;

		/// <inheritdoc/>
		public override string? Acronym => "MSLS";

		/// <inheritdoc/>
		public override TechniqueTags TechniqueTags => base.TechniqueTags | TechniqueTags.Msls;

		/// <inheritdoc/>
		public override Technique TechniqueCode => Technique.Msls;

		/// <inheritdoc/>
		public override DifficultyLevel DifficultyLevel => DifficultyLevel.Nightmare;


		/// <inheritdoc/>
		public override string ToString()
		{
			string cellsStr = Cells.ToString();
			string elimStr = new ConclusionCollection(Conclusions).ToString();
			return $"{Name}: {Cells.Count.ToString()} cells {cellsStr} => {elimStr}";
		}
	}
}
