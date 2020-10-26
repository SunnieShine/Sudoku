﻿using System.Collections.Generic;
using Sudoku.Data;
using Sudoku.Data.Collections;
using Sudoku.Drawing;

namespace Sudoku.Solving.Manual.Uniqueness.Qiu
{
	/// <summary>
	/// Provides a usage of <b>Qiu's deadly pattern type 1</b> (QDP) technique.
	/// </summary>
	/// <param name="Conclusions">All conclusions.</param>
	/// <param name="Views">All views.</param>
	/// <param name="Pattern">The pattern.</param>
	/// <param name="Candidate">The candidate.</param>
	public sealed record QdpType1TechniqueInfo(
		IReadOnlyList<Conclusion> Conclusions, IReadOnlyList<View> Views, in Pattern Pattern, int Candidate)
		: QdpTechniqueInfo(Conclusions, Views, Pattern)
	{
		/// <inheritdoc/>
		public override TechniqueCode TechniqueCode => TechniqueCode.QdpType1;


		/// <inheritdoc/>
		public override string ToString()
		{
			string patternStr = Pattern.FullMap.ToString();
			string candStr = new SudokuMap(Candidate).ToString();
			using var elims = new ConclusionCollection(Conclusions);
			string elimStr = elims.ToString();
			return
				$"{Name}: Cells {patternStr} will be a deadly pattern " +
				$"if {candStr} is false => {elimStr}";
		}
	}
}
