namespace Sudoku.Runtime.MinimalLexicographicOrderServices;

/// <summary>
/// Provides with extension methods on <see cref="Grid"/>, by checking min-lex-related properties.
/// </summary>
public static class GridMinLexExtensions
{
	/// <summary>
	/// Checks whether the current grid is the minimal lexicographical form, which means the corresponding string text code
	/// is the minimum value in all equivalent transforming cases in lexicographical order.
	/// </summary>
	/// <param name="this">The grid to be checked.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsMinLexGrid(this scoped ref readonly Grid @this)
		=> @this.PuzzleType != SudokuType.Sukaku && @this.GetUniqueness() == Uniqueness.Unique && @this.ToString("0") is var s
			? new MinLexFinder().Find(s) == s
			: throw new InvalidOperationException(SR.ExceptionMessage("MinLexShouldBeUniqueAndNotSukaku"));

	/// <summary>
	/// Checks the minimal lexicographical grid form.
	/// </summary>
	/// <param name="this">The grid to be checked.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Grid GetMinLexGrid(this scoped ref readonly Grid @this) => new MinLexFinder().Find(in @this);

	/// <summary>
	/// Adjust the grid to minimal lexicographical form.
	/// </summary>
	/// <param name="this">The grid to be changed.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void MakeMinLex(this scoped ref Grid @this) => @this = @this.GetMinLexGrid();
}