namespace Sudoku;

/// <summary>
/// Provides with solution-wide read-only fields used.
/// </summary>
public static class SolutionWideReadOnlyFields
{
	/// <summary>
	/// Indicates the invalid fallback value
	/// of methods <see cref="TrailingZeroCount(int)"/> and <see cref="TrailingZeroCount(uint)"/>,
	/// which means that if the method returns an invalid value, that value must be equal to this.
	/// In other words, you can use this field to check whether the method invocation executes correctly.
	/// </summary>
	/// <remarks>
	/// For more details you want to learn about, please visit
	/// <see href="https://github.com/dotnet/runtime/blob/d4a59b36c679712b74eccf98deb1a362cdbaa6b1/src/libraries/System.Private.CoreLib/src/System/Numerics/BitOperations.cs#L586">this link</see>
	/// to get the inner code.
	/// </remarks>
	/// <seealso cref="TrailingZeroCount(int)"/>
	/// <seealso cref="TrailingZeroCount(uint)"/>
	public const int InvalidTrailingZeroCountMethodFallback = 32;

	/// <summary>
	/// Indicates the invalid fallback value
	/// of methods <see cref="TrailingZeroCount(long)"/> and <see cref="TrailingZeroCount(ulong)"/>,
	/// which means that if the method returns an invalid value, that value must be equal to this.
	/// In other words, you can use this field to check whether the method invocation executes correctly.
	/// </summary>
	/// <remarks>
	/// For more details you want to learn about, please visit
	/// <see href="https://github.com/dotnet/runtime/blob/d4a59b36c679712b74eccf98deb1a362cdbaa6b1/src/libraries/System.Private.CoreLib/src/System/Numerics/BitOperations.cs#L647">this link</see>
	/// to get the inner code.
	/// </remarks>
	/// <seealso cref="TrailingZeroCount(long)"/>
	/// <seealso cref="TrailingZeroCount(ulong)"/>
	public const int InvalidTrailingZeroCountMethodFallbackLong = 64;

	/// <summary>
	/// Indicates the mask that means all blocks.
	/// </summary>
	public const HouseMask AllBlocksMask = 0b000_000_000__000_000_000__111_111_111;

	/// <summary>
	/// Indicates the mask that means all rows.
	/// </summary>
	public const HouseMask AllRowsMask = 0b000_000_000__111_111_111__000_000_000;

	/// <summary>
	/// Indicates the mask that means all columns.
	/// </summary>
	public const HouseMask AllColumnsMask = 0b111_111_111__000_000_000__000_000_000;

	/// <summary>
	/// Indicates the mask that means all houses.
	/// </summary>
	public const HouseMask AllHousesMask = 0b111_111_111__111_111_111__111_111_111;

	/// <summary>
	/// Indicates the first cell offset for each house.
	/// </summary>
	public static readonly Cell[] HouseFirst = [0, 3, 6, 27, 30, 33, 54, 57, 60, 0, 9, 18, 27, 36, 45, 54, 63, 72, 0, 1, 2, 3, 4, 5, 6, 7, 8];

	/// <summary>
	/// <para>Indicates a table for each cell's peers.</para>
	/// </summary>
	/// <example>
	/// '<c>Peers[0]</c>': the array of peers for the cell 0 (row 1 column 1).
	/// </example>
	public static readonly Cell[][] Peers = [
		[1, 2, 9, 10, 11, 18, 19, 20, 3, 4, 5, 6, 7, 8, 27, 36, 45, 54, 63, 72],
		[2, 9, 10, 11, 18, 19, 20, 3, 4, 5, 6, 7, 8, 28, 37, 46, 55, 64, 73, 0],
		[1, 9, 10, 11, 18, 19, 20, 3, 4, 5, 6, 7, 8, 29, 38, 47, 56, 65, 74, 0],
		[4, 5, 12, 13, 14, 21, 22, 23, 1, 2, 6, 7, 8, 30, 39, 48, 57, 66, 75, 0],
		[3, 5, 12, 13, 14, 21, 22, 23, 1, 2, 6, 7, 8, 31, 40, 49, 58, 67, 76, 0],
		[3, 4, 12, 13, 14, 21, 22, 23, 1, 2, 6, 7, 8, 32, 41, 50, 59, 68, 77, 0],
		[7, 8, 15, 16, 17, 24, 25, 26, 1, 2, 3, 4, 5, 33, 42, 51, 60, 69, 78, 0],
		[6, 8, 15, 16, 17, 24, 25, 26, 1, 2, 3, 4, 5, 34, 43, 52, 61, 70, 79, 0],
		[6, 7, 15, 16, 17, 24, 25, 26, 1, 2, 3, 4, 5, 35, 44, 53, 62, 71, 80, 0],
		[1, 2, 10, 11, 18, 19, 20, 12, 13, 14, 15, 16, 17, 27, 36, 45, 54, 63, 72, 0],
		[1, 2, 9, 11, 18, 19, 20, 12, 13, 14, 15, 16, 17, 28, 37, 46, 55, 64, 73, 0],
		[1, 2, 9, 10, 18, 19, 20, 12, 13, 14, 15, 16, 17, 29, 38, 47, 56, 65, 74, 0],
		[3, 4, 5, 13, 14, 21, 22, 23, 9, 10, 11, 15, 16, 17, 30, 39, 48, 57, 66, 75],
		[3, 4, 5, 12, 14, 21, 22, 23, 9, 10, 11, 15, 16, 17, 31, 40, 49, 58, 67, 76],
		[3, 4, 5, 12, 13, 21, 22, 23, 9, 10, 11, 15, 16, 17, 32, 41, 50, 59, 68, 77],
		[6, 7, 8, 16, 17, 24, 25, 26, 9, 10, 11, 12, 13, 14, 33, 42, 51, 60, 69, 78],
		[6, 7, 8, 15, 17, 24, 25, 26, 9, 10, 11, 12, 13, 14, 34, 43, 52, 61, 70, 79],
		[6, 7, 8, 15, 16, 24, 25, 26, 9, 10, 11, 12, 13, 14, 35, 44, 53, 62, 71, 80],
		[1, 2, 9, 10, 11, 19, 20, 21, 22, 23, 24, 25, 26, 27, 36, 45, 54, 63, 72, 0],
		[1, 2, 9, 10, 11, 18, 20, 21, 22, 23, 24, 25, 26, 28, 37, 46, 55, 64, 73, 0],
		[1, 2, 9, 10, 11, 18, 19, 21, 22, 23, 24, 25, 26, 29, 38, 47, 56, 65, 74, 0],
		[3, 4, 5, 12, 13, 14, 22, 23, 18, 19, 20, 24, 25, 26, 30, 39, 48, 57, 66, 75],
		[3, 4, 5, 12, 13, 14, 21, 23, 18, 19, 20, 24, 25, 26, 31, 40, 49, 58, 67, 76],
		[3, 4, 5, 12, 13, 14, 21, 22, 18, 19, 20, 24, 25, 26, 32, 41, 50, 59, 68, 77],
		[6, 7, 8, 15, 16, 17, 25, 26, 18, 19, 20, 21, 22, 23, 33, 42, 51, 60, 69, 78],
		[6, 7, 8, 15, 16, 17, 24, 26, 18, 19, 20, 21, 22, 23, 34, 43, 52, 61, 70, 79],
		[6, 7, 8, 15, 16, 17, 24, 25, 18, 19, 20, 21, 22, 23, 35, 44, 53, 62, 71, 80],
		[28, 29, 36, 37, 38, 45, 46, 47, 30, 31, 32, 33, 34, 35, 9, 18, 54, 63, 72, 0],
		[27, 29, 36, 37, 38, 45, 46, 47, 30, 31, 32, 33, 34, 35, 1, 10, 19, 55, 64, 73],
		[27, 28, 36, 37, 38, 45, 46, 47, 30, 31, 32, 33, 34, 35, 2, 11, 20, 56, 65, 74],
		[31, 32, 39, 40, 41, 48, 49, 50, 27, 28, 29, 33, 34, 35, 3, 12, 21, 57, 66, 75],
		[30, 32, 39, 40, 41, 48, 49, 50, 27, 28, 29, 33, 34, 35, 4, 13, 22, 58, 67, 76],
		[30, 31, 39, 40, 41, 48, 49, 50, 27, 28, 29, 33, 34, 35, 5, 14, 23, 59, 68, 77],
		[34, 35, 42, 43, 44, 51, 52, 53, 27, 28, 29, 30, 31, 32, 6, 15, 24, 60, 69, 78],
		[33, 35, 42, 43, 44, 51, 52, 53, 27, 28, 29, 30, 31, 32, 7, 16, 25, 61, 70, 79],
		[33, 34, 42, 43, 44, 51, 52, 53, 27, 28, 29, 30, 31, 32, 8, 17, 26, 62, 71, 80],
		[27, 28, 29, 37, 38, 45, 46, 47, 39, 40, 41, 42, 43, 44, 9, 18, 54, 63, 72, 0],
		[27, 28, 29, 36, 38, 45, 46, 47, 39, 40, 41, 42, 43, 44, 1, 10, 19, 55, 64, 73],
		[27, 28, 29, 36, 37, 45, 46, 47, 39, 40, 41, 42, 43, 44, 2, 11, 20, 56, 65, 74],
		[30, 31, 32, 40, 41, 48, 49, 50, 36, 37, 38, 42, 43, 44, 3, 12, 21, 57, 66, 75],
		[30, 31, 32, 39, 41, 48, 49, 50, 36, 37, 38, 42, 43, 44, 4, 13, 22, 58, 67, 76],
		[30, 31, 32, 39, 40, 48, 49, 50, 36, 37, 38, 42, 43, 44, 5, 14, 23, 59, 68, 77],
		[33, 34, 35, 43, 44, 51, 52, 53, 36, 37, 38, 39, 40, 41, 6, 15, 24, 60, 69, 78],
		[33, 34, 35, 42, 44, 51, 52, 53, 36, 37, 38, 39, 40, 41, 7, 16, 25, 61, 70, 79],
		[33, 34, 35, 42, 43, 51, 52, 53, 36, 37, 38, 39, 40, 41, 8, 17, 26, 62, 71, 80],
		[27, 28, 29, 36, 37, 38, 46, 47, 48, 49, 50, 51, 52, 53, 9, 18, 54, 63, 72, 0],
		[27, 28, 29, 36, 37, 38, 45, 47, 48, 49, 50, 51, 52, 53, 1, 10, 19, 55, 64, 73],
		[27, 28, 29, 36, 37, 38, 45, 46, 48, 49, 50, 51, 52, 53, 2, 11, 20, 56, 65, 74],
		[30, 31, 32, 39, 40, 41, 49, 50, 45, 46, 47, 51, 52, 53, 3, 12, 21, 57, 66, 75],
		[30, 31, 32, 39, 40, 41, 48, 50, 45, 46, 47, 51, 52, 53, 4, 13, 22, 58, 67, 76],
		[30, 31, 32, 39, 40, 41, 48, 49, 45, 46, 47, 51, 52, 53, 5, 14, 23, 59, 68, 77],
		[33, 34, 35, 42, 43, 44, 52, 53, 45, 46, 47, 48, 49, 50, 6, 15, 24, 60, 69, 78],
		[33, 34, 35, 42, 43, 44, 51, 53, 45, 46, 47, 48, 49, 50, 7, 16, 25, 61, 70, 79],
		[33, 34, 35, 42, 43, 44, 51, 52, 45, 46, 47, 48, 49, 50, 8, 17, 26, 62, 71, 80],
		[55, 56, 63, 64, 65, 72, 73, 74, 57, 58, 59, 60, 61, 62, 9, 18, 27, 36, 45, 0],
		[54, 56, 63, 64, 65, 72, 73, 74, 57, 58, 59, 60, 61, 62, 1, 10, 19, 28, 37, 46],
		[54, 55, 63, 64, 65, 72, 73, 74, 57, 58, 59, 60, 61, 62, 2, 11, 20, 29, 38, 47],
		[58, 59, 66, 67, 68, 75, 76, 77, 54, 55, 56, 60, 61, 62, 3, 12, 21, 30, 39, 48],
		[57, 59, 66, 67, 68, 75, 76, 77, 54, 55, 56, 60, 61, 62, 4, 13, 22, 31, 40, 49],
		[57, 58, 66, 67, 68, 75, 76, 77, 54, 55, 56, 60, 61, 62, 5, 14, 23, 32, 41, 50],
		[61, 62, 69, 70, 71, 78, 79, 80, 54, 55, 56, 57, 58, 59, 6, 15, 24, 33, 42, 51],
		[60, 62, 69, 70, 71, 78, 79, 80, 54, 55, 56, 57, 58, 59, 7, 16, 25, 34, 43, 52],
		[60, 61, 69, 70, 71, 78, 79, 80, 54, 55, 56, 57, 58, 59, 8, 17, 26, 35, 44, 53],
		[54, 55, 56, 64, 65, 72, 73, 74, 66, 67, 68, 69, 70, 71, 9, 18, 27, 36, 45, 0],
		[54, 55, 56, 63, 65, 72, 73, 74, 66, 67, 68, 69, 70, 71, 1, 10, 19, 28, 37, 46],
		[54, 55, 56, 63, 64, 72, 73, 74, 66, 67, 68, 69, 70, 71, 2, 11, 20, 29, 38, 47],
		[57, 58, 59, 67, 68, 75, 76, 77, 63, 64, 65, 69, 70, 71, 3, 12, 21, 30, 39, 48],
		[57, 58, 59, 66, 68, 75, 76, 77, 63, 64, 65, 69, 70, 71, 4, 13, 22, 31, 40, 49],
		[57, 58, 59, 66, 67, 75, 76, 77, 63, 64, 65, 69, 70, 71, 5, 14, 23, 32, 41, 50],
		[60, 61, 62, 70, 71, 78, 79, 80, 63, 64, 65, 66, 67, 68, 6, 15, 24, 33, 42, 51],
		[60, 61, 62, 69, 71, 78, 79, 80, 63, 64, 65, 66, 67, 68, 7, 16, 25, 34, 43, 52],
		[60, 61, 62, 69, 70, 78, 79, 80, 63, 64, 65, 66, 67, 68, 8, 17, 26, 35, 44, 53],
		[54, 55, 56, 63, 64, 65, 73, 74, 75, 76, 77, 78, 79, 80, 9, 18, 27, 36, 45, 0],
		[54, 55, 56, 63, 64, 65, 72, 74, 75, 76, 77, 78, 79, 80, 1, 10, 19, 28, 37, 46],
		[54, 55, 56, 63, 64, 65, 72, 73, 75, 76, 77, 78, 79, 80, 2, 11, 20, 29, 38, 47],
		[57, 58, 59, 66, 67, 68, 76, 77, 72, 73, 74, 78, 79, 80, 3, 12, 21, 30, 39, 48],
		[57, 58, 59, 66, 67, 68, 75, 77, 72, 73, 74, 78, 79, 80, 4, 13, 22, 31, 40, 49],
		[57, 58, 59, 66, 67, 68, 75, 76, 72, 73, 74, 78, 79, 80, 5, 14, 23, 32, 41, 50],
		[60, 61, 62, 69, 70, 71, 79, 80, 72, 73, 74, 75, 76, 77, 6, 15, 24, 33, 42, 51],
		[60, 61, 62, 69, 70, 71, 78, 80, 72, 73, 74, 75, 76, 77, 7, 16, 25, 34, 43, 52],
		[60, 61, 62, 69, 70, 71, 78, 79, 72, 73, 74, 75, 76, 77, 8, 17, 26, 35, 44, 53]
	];

	/// <summary>
	/// <para>
	/// The map of all cell offsets in its specified house.
	/// The indices is between 0 and 26, where:
	/// <list type="table">
	/// <item>
	/// <term><c>0..9</c></term>
	/// <description>Block 1 to 9.</description>
	/// </item>
	/// <item>
	/// <term><c>9..18</c></term>
	/// <description>Row 1 to 9.</description>
	/// </item>
	/// <item>
	/// <term><c>18..27</c></term>
	/// <description>Column 1 to 9.</description>
	/// </item>
	/// </list>
	/// </para>
	/// </summary>
	/// <example>
	/// '<c>HouseCells[0]</c>': all cell offsets in the house 0 (block 1).
	/// </example>
	public static readonly Cell[][] HouseCells = [
		[0, 1, 2, 9, 10, 11, 18, 19, 20],
		[3, 4, 5, 12, 13, 14, 21, 22, 23],
		[6, 7, 8, 15, 16, 17, 24, 25, 26],
		[27, 28, 29, 36, 37, 38, 45, 46, 47],
		[30, 31, 32, 39, 40, 41, 48, 49, 50],
		[33, 34, 35, 42, 43, 44, 51, 52, 53],
		[54, 55, 56, 63, 64, 65, 72, 73, 74],
		[57, 58, 59, 66, 67, 68, 75, 76, 77],
		[60, 61, 62, 69, 70, 71, 78, 79, 80],
		[0, 1, 2, 3, 4, 5, 6, 7, 8],
		[9, 10, 11, 12, 13, 14, 15, 16, 17],
		[18, 19, 20, 21, 22, 23, 24, 25, 26],
		[27, 28, 29, 30, 31, 32, 33, 34, 35],
		[36, 37, 38, 39, 40, 41, 42, 43, 44],
		[45, 46, 47, 48, 49, 50, 51, 52, 53],
		[54, 55, 56, 57, 58, 59, 60, 61, 62],
		[63, 64, 65, 66, 67, 68, 69, 70, 71],
		[72, 73, 74, 75, 76, 77, 78, 79, 80],
		[0, 9, 18, 27, 36, 45, 54, 63, 72],
		[1, 10, 19, 28, 37, 46, 55, 64, 73],
		[2, 11, 20, 29, 38, 47, 56, 65, 74],
		[3, 12, 21, 30, 39, 48, 57, 66, 75],
		[4, 13, 22, 31, 40, 49, 58, 67, 76],
		[5, 14, 23, 32, 41, 50, 59, 68, 77],
		[6, 15, 24, 33, 42, 51, 60, 69, 78],
		[7, 16, 25, 34, 43, 52, 61, 70, 79],
		[8, 17, 26, 35, 44, 53, 62, 71, 80]
	];

	/// <summary>
	/// Indicates all grid maps that a grid contains.
	/// </summary>
	/// <example>
	/// '<c>HouseMaps[0]</c>': The map containing all cells in the block 1.
	/// </example>
	public static readonly CellMap[] HousesMap;

	/// <summary>
	/// Indicates the map of length 81, indicating the <see cref="CellMap"/> instances that only contain one cell.
	/// </summary>
	public static readonly CellMap[] CellsMap;

	/// <summary>
	/// Indicates the peer maps using <see cref="Peers"/> table.
	/// </summary>
	/// <seealso cref="Peers"/>
	public static readonly CellMap[] PeersMap;

	/// <summary>
	/// Indicates the chute maps.
	/// </summary>
	public static readonly Chute[] Chutes;

	/// <summary>
	/// Indicates the possible house types to iterate.
	/// </summary>
	public static readonly HouseType[] HouseTypes = [HouseType.Block, HouseType.Row, HouseType.Column];

	/// <summary>
	/// Indicates a block list that each cell belongs to.
	/// </summary>
	internal static readonly House[] BlockTable = [
		0, 0, 0, 1, 1, 1, 2, 2, 2,
		0, 0, 0, 1, 1, 1, 2, 2, 2,
		0, 0, 0, 1, 1, 1, 2, 2, 2,
		3, 3, 3, 4, 4, 4, 5, 5, 5,
		3, 3, 3, 4, 4, 4, 5, 5, 5,
		3, 3, 3, 4, 4, 4, 5, 5, 5,
		6, 6, 6, 7, 7, 7, 8, 8, 8,
		6, 6, 6, 7, 7, 7, 8, 8, 8,
		6, 6, 6, 7, 7, 7, 8, 8, 8
	];

	/// <summary>
	/// Indicates a row list that each cell belongs to.
	/// </summary>
	internal static readonly House[] RowTable = [
		9, 9, 9, 9, 9, 9, 9, 9, 9,
		10, 10, 10, 10, 10, 10, 10, 10, 10,
		11, 11, 11, 11, 11, 11, 11, 11, 11,
		12, 12, 12, 12, 12, 12, 12, 12, 12,
		13, 13, 13, 13, 13, 13, 13, 13, 13,
		14, 14, 14, 14, 14, 14, 14, 14, 14,
		15, 15, 15, 15, 15, 15, 15, 15, 15,
		16, 16, 16, 16, 16, 16, 16, 16, 16,
		17, 17, 17, 17, 17, 17, 17, 17, 17
	];

	/// <summary>
	/// Indicates a column list that each cell belongs to.
	/// </summary>
	internal static readonly House[] ColumnTable = [
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26,
		18, 19, 20, 21, 22, 23, 24, 25, 26
	];

	/// <summary>
	/// Indicates the combinatorial numbers from <c>C(1, 1)</c> to <c>C(30, 30)</c>.
	/// </summary>
	internal static readonly int[][] Combinatorial = [
		[1],
		[2, 1],
		[3, 3, 1],
		[4, 6, 4, 1],
		[5, 10, 10, 5, 1],
		[6, 15, 20, 15, 6, 1],
		[7, 21, 35, 35, 21, 7, 1],
		[8, 28, 56, 70, 56, 28, 8, 1],
		[9, 36, 84, 126, 126, 84, 36, 9, 1],
		[10, 45, 120, 210, 252, 210, 120, 45, 10, 1],
		[11, 55, 165, 330, 462, 462, 330, 165, 55, 11, 1],
		[12, 66, 220, 495, 792, 924, 792, 495, 220, 66, 12, 1],
		[13, 78, 286, 715, 1287, 1716, 1716, 1287, 715, 286, 78, 13, 1],
		[14, 91, 364, 1001, 2002, 3003, 3432, 3003, 2002, 1001, 364, 91, 14, 1],
		[15, 105, 455, 1365, 3003, 5005, 6435, 6435, 5005, 3003, 1365, 455, 105, 15, 1],
		[16, 120, 560, 1820, 4368, 8008, 11440, 12870, 11440, 8008, 4368, 1820, 560, 120, 16, 1],
		[17, 136, 680, 2380, 6188, 12376, 19448, 24310, 24310, 19448, 12376, 6188, 2380, 680, 136, 17, 1],
		[18, 153, 816, 3060, 8568, 18564, 31824, 43758, 48620, 43758, 31824, 18564, 8568, 3060, 816, 153, 18, 1],
		[19, 171, 969, 3876, 11628, 27132, 50388, 75582, 92378, 92378, 75582, 50388, 27132, 11628, 3876, 969, 171, 19, 1],
		[20, 190, 1140, 4845, 15504, 38760, 77520, 125970, 167960, 184756, 167960, 125970, 77520, 38760, 15504, 4845, 1140, 190, 20, 1],
		[21, 210, 1330, 5985, 20349, 54264, 116280, 203490, 293930, 352716, 352716, 293930, 203490, 116280, 54264, 20349, 5985, 1330, 210, 21, 1],
		[22, 231, 1540, 7315, 26334, 74613, 170544, 319770, 497420, 646646, 705432, 646646, 497420, 319770, 170544, 74613, 26334, 7315, 1540, 231, 22, 1],
		[23, 253, 1771, 8855, 33649, 100947, 245157, 490314, 817190, 1144066, 1352078, 1352078, 1144066, 817190, 490314, 245157, 100947, 33649, 8855, 1771, 253, 23, 1],
		[24, 276, 2024, 10626, 42504, 134596, 346104, 735471, 1307504, 1961256, 2496144, 2704156, 2496144, 1961256, 1307504, 735471, 346104, 134596, 42504, 10626, 2024, 276, 24, 1],
		[25, 300, 2300, 12650, 53130, 177100, 480700, 1081575, 2042975, 3268760, 4457400, 5200300, 5200300, 4457400, 3268760, 2042975, 1081575, 480700, 177100, 53130, 12650, 2300, 300, 25, 1],
		[26, 325, 2600, 14950, 65780, 230230, 657800, 1562275, 3124550, 5311735, 7726160, 9657700, 10400600, 9657700, 7726160, 5311735, 3124550, 1562275, 657800, 230230, 65780, 14950, 2600, 325, 26, 1],
		[27, 351, 2925, 17550, 80730, 296010, 888030, 2220075, 4686825, 8436285, 13037895, 17383860, 20058300, 20058300, 17383860, 13037895, 8436285, 4686825, 2220075, 888030, 296010, 80730, 17550, 2925, 351, 27, 1],
		[28, 378, 3276, 20475, 98280, 376740, 1184040, 3108105, 6906900, 13123110, 21474180, 30421755, 37442160, 40116600, 37442160, 30421755, 21474180, 13123110, 6906900, 3108105, 1184040, 376740, 98280, 20475, 3276, 378, 28, 1],
		[29, 406, 3654, 23751, 118755, 475020, 1560780, 4292145, 10015005, 20030010, 34597290, 51895935, 67863915, 77558760, 77558760, 67863915, 51895935, 34597290, 20030010, 10015005, 4292145, 1560780, 475020, 118755, 23751, 3654, 406, 29, 1],
		[30, 435, 4060, 27405, 142506, 593775, 2035800, 5852925, 14307150, 30045015, 54627300, 86493225, 119759850, 145422675, 155117520, 145422675, 119759850, 86493225, 54627300, 30045015, 14307150, 5852925, 2035800, 593775, 142506, 27405, 4060, 435, 30, 1]
	];

	/// <summary>
	/// Indicates the chute houses.
	/// </summary>
	private static readonly (House, House, House)[] ChuteHouses = [(9, 10, 11), (12, 13, 14), (15, 16, 17), (18, 19, 20), (21, 22, 23), (24, 25, 26)];


	/// <include file='../../global-doc-comments.xml' path='g/static-constructor' />
	static SolutionWideReadOnlyFields()
	{
		PeersMap = new CellMap[81];
		for (var i = 0; i < 81; i++)
		{
			PeersMap[i] = [.. Peers[i]];
		}

		CellsMap = new CellMap[81];
		for (var i = 0; i < 81; i++)
		{
			CellsMap[i] = [i];
		}

		HousesMap = new CellMap[27];
		for (var i = 0; i < 27; i++)
		{
			HousesMap[i] = [.. HouseCells[i]];
		}

		Chutes = new Chute[6];
		for (var i = 0; i < 3; i++)
		{
			var ((r1, r2, r3), (c1, c2, c3)) = (ChuteHouses[i], ChuteHouses[i + 3]);
			(Chutes[i], Chutes[i + 3]) = (
				new(HousesMap[r1] | HousesMap[r2] | HousesMap[r3], true, (Mask)(1 << r1 - 9 | 1 << r2 - 9 | i << r3 - 9)),
				new(HousesMap[c1] | HousesMap[c2] | HousesMap[c3], false, (Mask)(1 << c1 - 18 | 1 << c2 - 18 | i << c3 - 18))
			);
		}
	}
}
