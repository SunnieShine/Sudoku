﻿using System.Collections.Generic;
using Sudoku.Data;

namespace Sudoku.Solving.Manual.Extensions
{
	/// <summary>
	/// Provides extension methods on <see cref="IEnumerable{T}"/>.
	/// </summary>
	/// <seealso cref="IEnumerable{T}"/>
	public static class EnumerableEx
	{
		/// <summary>
		/// Check whether the current loop is valid UL-shaped loop.
		/// </summary>
		/// <param name="loop">The loop.</param>
		/// <returns>A <see cref="bool"/> value indicating that.</returns>
		public static unsafe bool IsValidLoop(this IEnumerable<int> loop)
		{
			int visitedOddRegions = 0, visitedEvenRegions = 0;
			bool isOdd;
			foreach (int cell in loop)
			{
				for (var label = RegionLabel.Block; label <= RegionLabel.Column; label++)
				{
					int region = cell.ToRegion(label);
					if (*&isOdd)
					{
						if ((visitedOddRegions >> region & 1) != 0)
						{
							return false;
						}
						else
						{
							visitedOddRegions |= 1 << region;
						}
					}
					else
					{
						if ((visitedEvenRegions >> region & 1) != 0)
						{
							return false;
						}
						else
						{
							visitedEvenRegions |= 1 << region;
						}
					}
				}

				*&isOdd = !*&isOdd;
			}

			return visitedEvenRegions == visitedOddRegions;
		}
	}
}
