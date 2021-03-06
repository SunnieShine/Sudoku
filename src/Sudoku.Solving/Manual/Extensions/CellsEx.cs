﻿using Sudoku.Data;
using static Sudoku.Constants.Tables;

namespace Sudoku.Solving.Manual.Extensions
{
	/// <summary>
	/// Provides extension methods on <see cref="Cells"/>.
	/// </summary>
	/// <seealso cref="Cells"/>
	public static class CellsEx
	{
		/// <summary>
		/// Check whether the cells form an empty cell.
		/// </summary>
		/// <param name="this">The empty cell grid map.</param>
		/// <param name="block">The block.</param>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <returns>A <see cref="bool"/> value indicating that.</returns>
		public static bool IsEmptyRectangle(this in Cells @this, int block, out int row, out int column)
		{
			int r = block / 3 * 3 + 9, c = block % 3 * 3 + 18;
			for (int i = r, count = 0; i < r + 3; i++)
			{
				if (!(@this & RegionMaps[i]).IsEmpty || ++count <= 1)
				{
					continue;
				}

				row = column = -1;
				return false;
			}

			for (int i = c, count = 0; i < c + 3; i++)
			{
				if (!(@this & RegionMaps[i]).IsEmpty || ++count <= 1)
				{
					continue;
				}

				row = column = -1;
				return false;
			}

			for (int i = r; i < r + 3; i++)
			{
				for (int j = c; j < c + 3; j++)
				{
					if (@this > (RegionMaps[i] | RegionMaps[j]))
					{
						continue;
					}

					row = i;
					column = j;
					return true;
				}
			}

			row = column = -1;
			return false;
		}
	}
}
