﻿using System.Drawing;
using System.Runtime.CompilerServices;
using Sudoku.Data;
using Sudoku.Drawing;
using Sudoku.Solving.Manual;

namespace Sudoku.Windows.Extensions
{
	/// <summary>
	/// Provides extension methods on <see cref="StepInfo"/>.
	/// </summary>
	/// <seealso cref="StepInfo"/>
	public static class StepInfoEx
	{
		/// <summary>
		/// Creates a new <see cref="Bitmap"/> instance that uses the .
		/// </summary>
		/// <param name="this">The information.</param>
		/// <param name="grid">The grid used.</param>
		/// <returns>The <see cref="Bitmap"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Bitmap CreateBitmap(this StepInfo @this, in SudokuGrid grid) =>
			new GridPainter(new(600, 600), new(), grid)
			{
				View = @this.Views[0],
				Conclusions = @this.Conclusions
			}.Draw();
	}
}
