namespace Sudoku.Solving.Backtracking;

/// <summary>
/// Defines a solver that can solve a sudoku puzzle, using backtracking algorithm.
/// </summary>
/// <remarks>
/// <para>
/// Please note that the current type has no optimization on solving. Therefore sometimes the puzzle will be
/// extremely slowly to be solved although it is not very hard by manually solved.
/// One of the examples satisfying the above condition is:
/// <code>
/// ..............3.85..1.2.......5.7.....4...1...9.......5......73..2.1........4...9
/// </code>
/// The current solver may spend about 4.5 min on solving this puzzle on my machine.
/// </para>
/// <para>
/// For more information, please visit
/// <see href="https://en.wikipedia.org/wiki/Sudoku_solving_algorithms#cite_note-difficult_17_clue-1">this link</see>.
/// </para>
/// </remarks>
public sealed class BacktrackingSolver : ISolver
{
	/// <inheritdoc/>
	public static string UriLink => "https://simple.wikipedia.org/wiki/Backtracking";


	/// <summary>
	/// To solve the specified grid.
	/// </summary>
	/// <param name="grid">The grid to be solved.</param>
	/// <param name="result">
	/// <para>The result of the grid.</para>
	/// <para>
	/// Different with other methods whose containing type is <see cref="ISolver"/>,
	/// this argument can be used no matter what the result value will be.
	/// </para>
	/// </param>
	/// <returns>
	/// A <see cref="bool"/>? value indicating whether the grid can be solved, i.e. has a unique solution.
	/// Please note that the method will return three possible values:
	/// <list type="table">
	/// <item>
	/// <term><see langword="true"/></term>
	/// <description>The puzzle has a unique solution.</description>
	/// </item>
	/// <item>
	/// <term><see langword="false"/></term>
	/// <description>The puzzle has multiple solutions.</description>
	/// </item>
	/// <item>
	/// <term><see langword="null"/></term>
	/// <description>The puzzle has no solution.</description>
	/// </item>
	/// </list>
	/// </returns>
	public bool? Solve(ref readonly Grid grid, out Grid result)
	{
		var resultArray = default(Digit[]);
		try
		{
			var solutionsCount = 0;
			var gridArray = grid.ToDigitsArray(); // Implicit plus one operation.
			solve(ref solutionsCount, ref resultArray, gridArray, 0);

			if (resultArray is null)
			{
				result = default;
				return null;
			}

			result = Grid.Create(resultArray, GridCreatingOption.MinusOne);
			return true;
		}
		catch (MultipleSolutionException)
		{
			result = Grid.Create(resultArray!, GridCreatingOption.MinusOne);
			return false;
		}


		static void solve(ref int solutionsCount, ref Digit[]? result, Digit[] gridValues, int finishedCellsCount)
		{
			if (finishedCellsCount == 81)
			{
				// Solution found.
				if (++solutionsCount > 1)
				{
					throw new MultipleSolutionException();
				}

				// We should catch the result.
				// If we use normal assignment, we well get the initial grid rather a solution,
				// because this is a recursive function.
				result = gridValues[..];
				return; // Exit the recursion.
			}

			if (gridValues[finishedCellsCount] != 0)
			{
				solve(ref solutionsCount, ref result, gridValues, finishedCellsCount + 1);
			}
			else
			{
				// Here may try 9 times.
				// Of course, you can add a new variable to save all candidates to let the algorithm run faster.
				var r = finishedCellsCount / 9;
				var c = finishedCellsCount % 9;
				for (var i = 0; i < 9; i++)
				{
					gridValues[finishedCellsCount]++;
					if (IsValid(gridValues, r, c))
					{
						solve(ref solutionsCount, ref result, gridValues, finishedCellsCount + 1);
					}
				}

				// All values are wrong, which means the value before we calculate is already wrong.
				// Backtracking the cell...
				gridValues[finishedCellsCount] = 0;
			}
		}
	}


	/// <summary>
	/// Determine whether the specified grid has confliction with the specified row and column.
	/// </summary>
	/// <param name="grid">The grid to be checked.</param>
	/// <param name="r">The row index.</param>
	/// <param name="c">The column index.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static bool IsValid(ref readonly Grid grid, RowIndex r, ColumnIndex c)
	{
		var number = grid.GetDigit(r * 9 + c);
		for (var i = 0; i < 9; i++)
		{
			if (i != r && grid.GetDigit(i * 9 + c) == number || i != c && grid.GetDigit(r * 9 + i) == number)
			{
				return false;
			}
		}
		for (RowIndex ii = r / 3 * 3, i = ii; i < ii + 3; i++)
		{
			for (ColumnIndex jj = c / 3 * 3, j = jj; j < jj + 3; j++)
			{
				if ((i != r || j != c) && grid.GetDigit(i * 9 + j) == number)
				{
					return false;
				}
			}
		}
		return true;
	}

	/// <inheritdoc cref="IsValid(ref readonly Grid, RowIndex, ColumnIndex)"/>
	public static bool IsValid(Digit[] grid, RowIndex r, ColumnIndex c)
	{
		var number = grid[r * 9 + c];
		for (var i = 0; i < 9; i++)
		{
			if (i != r && grid[i * 9 + c] == number || i != c && grid[r * 9 + i] == number)
			{
				return false;
			}
		}
		for (RowIndex ii = r / 3 * 3, i = ii; i < ii + 3; i++)
		{
			for (ColumnIndex jj = c / 3 * 3, j = jj; j < jj + 3; j++)
			{
				if ((i != r || j != c) && grid[i * 9 + j] == number)
				{
					return false;
				}
			}
		}
		return true;
	}
}
