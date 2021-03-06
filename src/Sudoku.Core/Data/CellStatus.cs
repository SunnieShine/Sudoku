﻿using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Sudoku.Data
{
	/// <summary>
	/// Represents a cell status.
	/// </summary>
	[Flags, Closed]
	public enum CellStatus : byte
	{
		/// <summary>
		/// Indicates the cell status is invalid.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		Undefined = 0,

		/// <summary>
		/// Indicates that the cell is empty.
		/// </summary>
		Empty = 1,

		/// <summary>
		/// Indicates that the cell has already filled a value,
		/// but the value doesn't exist when the puzzle begins.
		/// </summary>
		Modifiable = 2,

		/// <summary>
		/// Indicates that the cell has already filled a value,
		/// and the value does exist when the puzzle begins. In
		/// other words, the value is a given (or a hint, clue, etc.).
		/// </summary>
		Given = 4,

		/// <summary>
		/// Indicates all cases of cell statuses.
		/// </summary>
		/// <remarks>
		/// You had better use this field on cases when processing
		/// the bitwise operations in a <see cref="SudokuGrid"/>.
		/// This field should not be used to process cell status comparison.
		/// </remarks>
		/// <seealso cref="SudokuGrid.GetStatus(int)"/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		All = Empty | Modifiable | Given
	}
}
