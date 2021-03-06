﻿using System;
using System.Extensions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sudoku.Data.Extensions
{
	/// <summary>
	/// Provides extension methods for <see cref="SymmetryType"/>.
	/// </summary>
	/// <seealso cref="SymmetryType"/>
	public static class SymmetryTypeEx
	{
		/// <summary>
		/// Get the name of the current symmetry type.
		/// </summary>
		/// <param name="this">The type.</param>
		/// <returns>The name.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetName(this SymmetryType @this)
		{
			return @this switch
			{
				SymmetryType.None => "No symmetry",
				SymmetryType.Central => "Central symmetry type",
				SymmetryType.Diagonal => "Diagonal symmetry type",
				SymmetryType.AntiDiagonal => "Anti-diagonal symmetry type",
				SymmetryType.XAxis => "X-axis symmetry type",
				SymmetryType.YAxis => "Y-axis symmetry type",
				SymmetryType.AxisBoth => "Both X-axis and Y-axis",
				SymmetryType.DiagonalBoth => "Both diagonal and anti-diagonal",
				SymmetryType.All => "All symmetry type",
				_ => getAllPossibleNames(@this)
			};

			static string getAllPossibleNames(SymmetryType type)
			{
				const string separator = ", ";
				var sb = new ValueStringBuilder(stackalloc char[210]);
				var flags = Enum.GetValues<SymmetryType>();
				for (int i = 1, length = flags.Length; i < length; i++)
				{
					var flag = flags[i];
					if (type.Flags(flag))
					{
						sb.Append(flag.ToString());
						sb.Append(separator);
					}
				}

				if (sb.Length != 0)
				{
					sb.RemoveFromEnd(separator.Length);
					return sb.ToString();
				}

				return string.Empty;
			}
		}
	}
}
