﻿#pragma warning disable IDE0079
#pragma warning disable 8618

using System.Collections.Generic;
using Sudoku.Data;

namespace Sudoku
{
	public static partial class Constants
	{
		/// <summary>
		/// The tables for grid processing. All fields will be initialized in
		/// the static constructor.
		/// </summary>
		public static partial class Tables
		{
			/// <summary>
			/// The table of all blocks to iterate for each blocks.
			/// </summary>
			public static readonly byte[][] IntersectionBlockTable;

			/// <summary>
			/// <para>Indicates a table for each cell's peers.</para>
			/// </summary>
			/// <example>
			/// '<c>Peers[0]</c>': the array of peers for the cell 0 (row 1 column 1).
			/// </example>
			public static readonly int[][] Peers;

			/// <summary>
			/// <para>
			/// The map of all cell offsets in its specified region.
			/// The indices is between 0 and 26, where <c>0..9</c> is for block 1 to 9,
			/// <c>9..18</c> is for row 1 to 9 and <c>18..27</c> is for column 1 to 9.
			/// </para>
			/// </summary>
			/// <example>
			/// '<c>RegionTable[0]</c>': all cell offsets in the region 0 (block 1).
			/// </example>
			public static readonly int[][] RegionCells;

			/// <summary>
			/// Indicates all grid maps that a grid contains.
			/// </summary>
			/// <example>
			/// '<c>RegionMaps[0]</c>': The map containing all cells in the block 1.
			/// </example>
			public static readonly Cells[] RegionMaps;

			/// <summary>
			/// Indicates the peer maps using <see cref="Peers"/> table.
			/// </summary>
			/// <seealso cref="Peers"/>
			public static readonly Cells[] PeerMaps;

			/// <summary>
			/// <para>
			/// Indicates all maps that forms the each intersection. The pattern will be like:
			/// <code>
			/// .-------.-------.-------.
			/// | C C C | A A A | A A A |
			/// | B B B | . . . | . . . |
			/// | B B B | . . . | . . . |
			/// '-------'-------'-------'
			/// </code>
			/// </para>
			/// <para>
			/// In addition, in this data structure, "<c>CoverSet</c>" is a block and "<c>BaseSet</c>" is a line.
			/// </para>
			/// </summary>
			public static readonly IReadOnlyDictionary<
				(byte Line, byte Block),
				(Cells LineMap, Cells BlockMap, Cells IntersectionMap, byte[] OtherBlocks)
			> IntersectionMaps;
		}
	}
}
