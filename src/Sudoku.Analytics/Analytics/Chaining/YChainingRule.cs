namespace Sudoku.Analytics.Chaining;

/// <summary>
/// Represents a chaining rule on Y rule (i.e. <see cref="LinkType.SingleCell"/>).
/// </summary>
/// <seealso cref="LinkType.SingleCell"/>
internal sealed class YChainingRule : ChainingRule
{
	/// <inheritdoc/>
	public override void CollectStrongLinks(ref readonly Grid grid, LinkDictionary linkDictionary)
	{
		foreach (var cell in BivalueCells)
		{
			var mask = grid.GetCandidates(cell);
			var digit1 = TrailingZeroCount(mask);
			var digit2 = mask.GetNextSet(digit1);
			linkDictionary.AddEntry(new(cell * 9 + digit1), new(cell * 9 + digit2));
		}
	}

	/// <inheritdoc/>
	public override void CollectWeakLinks(ref readonly Grid grid, LinkDictionary linkDictionary)
	{
		foreach (var cell in EmptyCells)
		{
			foreach (var combinationPair in grid.GetCandidates(cell).GetAllSets().GetSubsets(2))
			{
				linkDictionary.AddEntry(
					new(cell * 9 + combinationPair[0]),
					new(cell * 9 + combinationPair[1])
				);
			}
		}
	}
}
