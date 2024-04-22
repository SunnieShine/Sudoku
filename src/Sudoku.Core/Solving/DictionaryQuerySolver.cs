namespace Sudoku.Solving;

/// <summary>
/// Defines a <see cref="Dictionary{TKey, TValue}"/>-based LINQ solver that solves a sudoku grid,
/// using a different algorithm with the other one of type <see cref="EnumerableQuerySolver"/>.
/// </summary>
/// <remarks>
/// <para>
/// This algorithm is originally written by Python, posted from <see href="http://norvig.com/sudo.py">here</see>
/// by Richard Birkby, June 2007. For more information, please visit
/// <see href="http://norvig.com/sudoku.html">this link</see>.
/// </para>
/// <para>
/// Also, <see href="https://bugzilla.mozilla.org/attachment.cgi?id=266577">this link</see> is for the same algorithm
/// written by JavaScript 1.8+.
/// </para>
/// </remarks>
/// <seealso cref="EnumerableQuerySolver"/>
public sealed class DictionaryQuerySolver : ISolver
{
	/// <summary>
	/// Indicates the characters of all rows.
	/// </summary>
	private const string Rows = "ABCDEFGHI";

	/// <summary>
	/// Indicates the characters of all columns.
	/// </summary>
	private const string Columns = "123456789";

	/// <summary>
	/// Indicates the characters of all digits.
	/// </summary>
	private const string Digits = "123456789";


	/// <summary>
	/// Indicates all possible coordinates.
	/// </summary>
	private static readonly string[] Coordinates = [.. from r in Rows from c in Columns select $"{r}{c}"];

	/// <summary>
	/// Indicates the peers.
	/// </summary>
	private static readonly Dictionary<string, IEnumerable<string>> Peers;

	/// <summary>
	/// Indicates the houses.
	/// </summary>
	private static readonly Dictionary<string, IGrouping<string, string[]>> Houses;


	/// <include file='../../global-doc-comments.xml' path='g/static-constructor'/>
	static DictionaryQuerySolver()
	{
		var houseList = (IEnumerable<string[]>)[
			.. from c in Columns select (string[])[.. from r in Rows from p in c.ToString() select $"{r}{p}"],
			.. from r in Rows select (string[])[.. from p in r.ToString() from c in Columns select $"{p}{c}"],
			..
			from rs in (string[])["ABC", "DEF", "GHI"]
			from cs in (string[])["123", "456", "789"]
			select (string[])[.. from r in rs from c in cs select $"{r}{c}"]
		];

		Houses = (from s in Coordinates from u in houseList where u.Contains(s) group u by s).ToDictionary(static g => g.Key);
		Peers = (from s in Coordinates from u in Houses[s] from s2 in u where s2 != s group s2 by s)
			.ToDictionary(static g => g.Key, static g => g.Distinct());
	}


	/// <inheritdoc/>
	public static string? UriLink => "http://aspadvice.com/blogs/rbirkby/attachment/34077.ashx";


	/// <summary>
	/// To solve the puzzle.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="result">The result.</param>
	/// <returns>
	/// <para>
	/// The method will return <see langword="false"/> if the puzzle has more than one solution,
	/// or <see langword="null"/> if the puzzle cannot be solved.
	/// </para>
	/// <para>
	/// Please note that the method cannot return <see langword="true"/>
	/// due to not being aware of the uniqueness of the puzzle.
	/// </para>
	/// </returns>
	public bool? Solve(ref readonly Grid grid, out Grid result)
	{
		Unsafe.SkipInit(out result);

		var rawResult = Search(ParseGrid($"{grid:0}"));
		if (rawResult is null)
		{
			return null;
		}

		Debug.Assert(rawResult.Count == 81);
		var gridArray = new Digit[81];
		foreach (var (rawCell, rawDigit) in rawResult)
		{
			if (rawDigit is not [var digitChar and >= '1' and <= '9']
				|| rawCell is not [var rIndex and >= 'A' and <= 'I', var cIndex and >= '1' and <= '9'])
			{
				throw new("Unexpected error.");
			}

			var cell = (rIndex - 'A') * 9 + (cIndex - '1');
			var digit = digitChar - '1';
			gridArray[cell] = digit;
		}

		result = Grid.Create(gridArray);

		// Please note the algorithm doesn't perform well on the operation of validation on multiple solutions
		// due to the recursion algorithm (Depth-first searching). Therefore, the method
		// isn't aware of the solution count.
		return false;
	}

	/// <summary>
	/// Determines whether all elements in this collection are not <see langword="null"/>.
	/// </summary>
	/// <typeparam name="T">The type of the element in the sequence.</typeparam>
	/// <param name="sequence">The whole sequence.</param>
	/// <returns>A <see cref="bool"/> value.</returns>
	private bool AllNotNull<T>(ReadOnlySpan<T> sequence)
	{
		foreach (var e in sequence)
		{
			if (e is null)
			{
				return false;
			}
		}

		return true;
	}

	/// <inheritdoc cref="AllNotNull{T}(ReadOnlySpan{T})"/>
	private bool AllNotNull<T>(IEnumerable<T> sequence)
	{
		foreach (var e in sequence)
		{
			if (e is null)
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// To zip two lists of <see cref="string"/>s.
	/// </summary>
	/// <param name="a">The first array.</param>
	/// <param name="b">The second array.</param>
	/// <returns>The final zipped collection.</returns>
	private string[][] Zip(string[] a, string[] b)
	{
		var n = Math.Min(a.Length, b.Length);
		var sd = new string[n][];

		for (var i = 0; i < n; i++)
		{
			sd[i] = [a[i].ToString(), b[i].ToString()];
		}

		return sd;
	}

	/// <summary>
	/// Given a string of 81 digits (or <c>'.'</c>, <c>'0'</c> or <c>'-'</c>),
	/// and return a dictionary of a key-value pair of cell and the candidates.
	/// </summary>
	public Dictionary<string, string>? ParseGrid(string gridStr)
	{
		//var grid2 = from c in gridStr where "0.-123456789".Contains(c) select c;
		var values = Coordinates.ToDictionary(CommonMethods.Self, static _ => Digits);

		foreach (var sd in Zip(Coordinates, [.. from s in gridStr select s.ToString()]))
		{
			var (s, d) = (sd[0], sd[1]);
			if (Digits.Contains(d) && Assign(values, s, d) is null)
			{
				return null;
			}
		}

		return values;
	}

	/// <summary>
	/// Using depth-first search and propagation to try all possible values.
	/// </summary>
	/// <returns>A first found solution.</returns>
	/// <remarks>
	/// This algorithm is hard to determine whether the puzzle has multiple solutions, due to DFS.
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Dictionary<string, string>? Search(Dictionary<string, string>? values)
	{
		if (values is null)
		{
			return null;
		}

		if (values.All(static kvp => kvp.Value.Length == 1))
		{
			// Solved.
			return values;
		}

		// Choose the unfilled block s with the fewest possibilities.
		var s2 = (from s in Coordinates where values[s].Length > 1 orderby values[s].Length select s).First();
		return (
			from d in values[s2]
			let solution = Search(Assign(new(values), s2, d.ToString()))
			where solution is not null
			select solution
		)[0];
	}

	/// <summary>
	/// Eliminate all the other values (except d) from <c>values[s]</c> and propagate.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private Dictionary<string, string>? Assign(Dictionary<string, string> values, string s, string d)
		=> AllNotNull(from d2 in values[s] where d2.ToString() != d select Eliminate(values, s, d2.ToString())) ? values : null;

	/// <summary>
	/// Eliminate d from <c>values[s]</c>; propagate when values or places <![CDATA[<=]]> 2.
	/// </summary>
	private Dictionary<string, string>? Eliminate(Dictionary<string, string> values, string s, string d)
	{
		if (!values[s].Contains(d))
		{
			return values;
		}

		values[s] = values[s].Replace(d, string.Empty);
		switch (values[s].Length)
		{
			case 0:
			{
				// The last possible value has been removed, which means no longer possibilities
				// can be used for the next iteration. Just return.
				return null;
			}
			case 1:
			{
				// If there is only one value (d2) left in block, remove it from peers.
				var d2 = values[s];
				if (!AllNotNull(from s2 in Peers[s] select Eliminate(values, s2, d2)))
				{
					return null;
				}

				break;
			}
		}

		// Now check the places where d appears in the units of s.
		foreach (var u in Houses[s])
		{
			var dPlaces = from s2 in u where values[s2].Contains(d) select s2;
			if (dPlaces.Length == 0)
			{
				return null;
			}

			if (dPlaces.HasOnlyOneElement())
			{
				// d can only be in one place in unit; assign it there.
				if (Assign(values, dPlaces[0], d) is null)
				{
					return null;
				}
			}
		}

		return values;
	}
}
