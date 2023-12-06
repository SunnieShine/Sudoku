#pragma warning disable

using Sudoku.Concepts;

namespace Sudoku.Algorithm.MinLex;

/// <summary>
/// Represents a finder object that checks for a sudoku grid, calculating for the minimal lexicographical-ordered value for that grid.
/// </summary>
/// <remarks>
/// <para>
/// This object can be used for checking for duplicate for grids. If two grids are considered to be equivalent,
/// two grids will contain a same minimal lexicographic value.
/// </para>
/// <para>
/// <inheritdoc cref="BestTriplet" path="/remarks"/>
/// </para>
/// </remarks>
public static class MinLexFinder
{
	/// <inheritdoc cref="Find(ref readonly Grid, bool)"/>
	public static extern string Find(string gridString, bool findForPattern = false);

	/// <summary>
	/// Find for the minimal lexicographic result for a grid.
	/// </summary>
	/// <param name="grid">The specified grid.</param>
	/// <param name="findForPattern">Indicates whether the grid only searches for its minimal pattern.</param>
	/// <returns>The minimal result.</returns>
	public static extern Grid Find(scoped ref readonly Grid grid, bool findForPattern = false);
}
