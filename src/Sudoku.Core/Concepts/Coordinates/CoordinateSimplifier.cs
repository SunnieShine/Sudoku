namespace Sudoku.Concepts.Coordinates;

/// <summary>
/// Represents a simplifier type that can simplify the coordinates.
/// </summary>
public static class CoordinateSimplifier
{
	/// <summary>
	/// Try to simplify parts of cells, by combining same rows and columns.
	/// </summary>
	/// <param name="cells">The cells to be simplified.</param>
	/// <returns>A list of parts grouped by rows and its matched columns.</returns>
	public static ReadOnlySpan<CoordinateSplit> Simplify(ref readonly CellMap cells)
	{
		return (CoordinateSplit[])[
			..
			from pair in simplifyCoordinates([.. from cell in cells select (cell / 9, cell % 9)])
			let rows = pair.Item1
			let columns = pair.Item2
			select rows switch
			{
				RowIndex r => new CoordinateSplit([r], [.. columns]),
				SortedSet<RowIndex> r => new CoordinateSplit([.. r], [.. columns]),
				_ => throw new InvalidOperationException()
			}
		];


		static List<(object, SortedSet<ColumnIndex>)> simplifyCoordinates(List<(RowIndex, ColumnIndex)> coordinates)
		{
			var rowGroups = new Dictionary<RowIndex, SortedSet<ColumnIndex>>();
			var colGroups = new Dictionary<ColumnIndex, SortedSet<RowIndex>>();
			foreach (var (x, y) in coordinates)
			{
				if (!rowGroups.TryAdd(x, [y]))
				{
					rowGroups[x].Add(y);
				}
				if (!colGroups.TryAdd(y, [x]))
				{
					colGroups[y].Add(x);
				}
			}

			var (simplifiedRows, simplifiedCols) = (rowGroups.ToDictionary(), colGroups.ToDictionary());
			var finalSimplified = new List<(object, object)>();
			foreach (var (x, yList) in simplifiedRows)
			{
				foreach (var y in yList)
				{
					if (simplifiedCols.TryGetValue(y, out var xList))
					{
						if (xList.SequenceEqual([x]))
						{
							finalSimplified.Add((x, y));
						}
						else
						{
							finalSimplified.Add((xList, y));
						}
					}
					else
					{
						finalSimplified.Add((x, y));
					}
				}
			}

			var finalDict = new Dictionary<SortedSet<RowIndex>, SortedSet<ColumnIndex>>(SortedSet<RowIndex>.CreateSetComparer());
			foreach (var item in finalSimplified)
			{
				if (item.Item1 is SortedSet<RowIndex> xList)
				{
					if (!finalDict.TryAdd(xList, [(ColumnIndex)item.Item2]))
					{
						finalDict[xList].Add((ColumnIndex)item.Item2);
					}
				}
				else
				{
					var key = new SortedSet<RowIndex> { (RowIndex)item.Item1 };
					if (!finalDict.TryAdd(key, [(ColumnIndex)item.Item2]))
					{
						finalDict[key].Add((ColumnIndex)item.Item2);
					}
				}
			}

			return [
				..
				from kvp in finalDict.ToArray()
				let keySet = kvp.Key
				let valueSet = kvp.Value
				select (keySet.Count > 1 ? (keySet, valueSet) : ((object)keySet.Min, valueSet))
			];
		}
	}
}
