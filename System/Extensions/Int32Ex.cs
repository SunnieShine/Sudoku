﻿using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Sudoku.DocComments;

namespace System.Extensions
{
	/// <summary>
	/// Provides extension methods on <see cref="int"/>.
	/// </summary>
	/// <seealso cref="int"/>
	public static class Int32Ex
	{
		/// <inheritdoc cref="Integer.IsPowerOfTwo(Integer)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPowerOfTwo(this int @this) => @this != 0 && (@this & (@this - 1)) == 0;

		/// <inheritdoc cref="Integer.FindFirstSet(Integer)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FindFirstSet(this int @this) => BitOperations.TrailingZeroCount(@this);

		/// <inheritdoc cref="Integer.PopCount(Integer)"/>
		public static int PopCount(this int @this) => BitOperations.PopCount((uint)@this);

		/// <inheritdoc cref="Integer.GetNextSet(Integer, int)"/>
		public static int GetNextSet(this int @this, int index)
		{
			for (int i = index + 1; i < 32; i++)
			{
				if ((@this & 1 << i) != 0)
				{
					return i;
				}
			}

			return -1;
		}

		/// <inheritdoc cref="Integer.SetAt(Integer, int)"/>
		public static int SetAt(this int @this, int order)
		{
			for (int i = 0, count = -1; i < 32; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}

			return -1;
		}

		/// <inheritdoc cref="Integer.GetAllSets(Integer)"/>
		public static IEnumerable<int> GetAllSets(this int @this)
		{
			if (@this == 0)
			{
				yield break;
			}

			for (int i = 0; i < 32; i++, @this >>= 1)
			{
				if ((@this & 1) != 0)
				{
					yield return i;
				}
			}
		}

		/// <inheritdoc cref="Integer.GetEnumerator(Integer)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerator<int> GetEnumerator(this int @this) => @this.GetAllSets().GetEnumerator();

		/// <inheritdoc cref="Integer.ReverseBits(ref Integer)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReverseBits(this ref int @this)
		{
			@this = @this >> 1 & 0x55555555 | (@this & 0x55555555) << 1;
			@this = @this >> 2 & 0x33333333 | (@this & 0x33333333) << 2;
			@this = @this >> 4 & 0x0F0F0F0F | (@this & 0x0F0F0F0F) << 4;
			@this = @this >> 8 & 0x00FF00FF | (@this & 0x00FF00FF) << 8;
			@this = @this >> 16 | @this << 16;
		}
	}
}
