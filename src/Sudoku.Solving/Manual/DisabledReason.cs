﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Sudoku.Solving.Manual
{
	/// <summary>
	/// Indicates a reason why the searcher is disabled.
	/// </summary>
	[Flags, Closed]
	public enum DisabledReason : byte
	{
		/// <summary>
		/// Indicates the searcher is normal.
		/// </summary>
		None,

		/// <summary>
		/// Indicates the searcher searches for last resorts, which don't need to show.
		/// </summary>
		LastResort = 1,

		/// <summary>
		/// Indicates the searcher has bugs while searching.
		/// </summary>
		HasBugs = 2,

		/// <summary>
		/// Indicates the searcher runs so slowly that the author himself can't stand to use it.
		/// </summary>
		TooSlow = 4,

		/// <summary>
		/// Indicates the searcher can get correct <see cref="StepInfo"/>s, but the difference
		/// of the difficulty among them are too big.
		/// </summary>
		Unstable = 8
	}
}
