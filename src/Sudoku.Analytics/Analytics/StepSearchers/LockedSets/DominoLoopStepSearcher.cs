namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with a <b>Domino Loop</b> (i.e. SK Loop) step searcher.
/// The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>Domino Loop</item>
/// </list>
/// </summary>
[StepSearcher("StepSearcherName_DominoLoopStepSearcher", Technique.DominoLoop)]
public sealed partial class DominoLoopStepSearcher : StepSearcher
{
	/// <summary>
	/// The position table of all SK-loops.
	/// </summary>
	private static readonly Cell[][] SkLoopTable;


	/// <include file='../../global-doc-comments.xml' path='g/static-constructor' />
	static DominoLoopStepSearcher()
	{
		// Initialize for SK-loop table.
		SkLoopTable = new Cell[729][];

		var s = (stackalloc int[4]);
		for (var (a, n) = (9, 0); a < 18; a++)
		{
			for (var b = 9; b < 18; b++)
			{
				if (a / 3 == b / 3 || b < a)
				{
					continue;
				}

				for (var c = 18; c < 27; c++)
				{
					for (var d = 18; d < 27; d++)
					{
						if (c / 3 == d / 3 || d < c)
						{
							continue;
						}

						var all = HousesMap[a] | HousesMap[b] | HousesMap[c] | HousesMap[d];
						var overlap = (HousesMap[a] | HousesMap[b]) & (HousesMap[c] | HousesMap[d]);
						var blockMask = overlap.BlockMask;
						for (var (i, count) = (0, 0); count < 4 && i < 16; i++)
						{
							if ((blockMask >> i & 1) != 0)
							{
								s[count++] = i;
							}
						}

						all &= HousesMap[s[0]] | HousesMap[s[1]] | HousesMap[s[2]] | HousesMap[s[3]];
						all &= ~overlap;

						SkLoopTable[n] = new Cell[16];
						var pos = 0;
						foreach (var cell in all & HousesMap[a])
						{
							SkLoopTable[n][pos++] = cell;
						}
						foreach (var cell in all & HousesMap[d])
						{
							SkLoopTable[n][pos++] = cell;
						}
						var cells = (Cell[])[.. all & HousesMap[b]];
						SkLoopTable[n][pos++] = cells[2];
						SkLoopTable[n][pos++] = cells[3];
						SkLoopTable[n][pos++] = cells[0];
						SkLoopTable[n][pos++] = cells[1];
						cells = [.. all & HousesMap[c]];
						SkLoopTable[n][pos++] = cells[2];
						SkLoopTable[n][pos++] = cells[3];
						SkLoopTable[n][pos++] = cells[0];
						SkLoopTable[n++][pos++] = cells[1];
					}
				}
			}
		}
	}


	/// <inheritdoc/>
	protected internal override Step? Collect(ref AnalysisContext context)
	{
		var pairs = (stackalloc Mask[8]);
		var tempLink = (stackalloc Mask[8]);
		var linkHouse = (stackalloc House[8]);
		ref readonly var grid = ref context.Grid;
		foreach (var cells in SkLoopTable)
		{
			// Initialize the elements.
			var (n, i, candidateCount) = (0, 0, 0);
			pairs.Clear();
			linkHouse.Clear();

			// Get the values count ('n') and pairs list ('pairs').
			for (i = 0; i < 8; i++)
			{
				if (grid.GetState(cells[i << 1]) != CellState.Empty)
				{
					n++;
				}
				else
				{
					pairs[i] |= grid.GetCandidates(cells[i << 1]);
				}

				if (grid.GetState(cells[(i << 1) + 1]) != CellState.Empty)
				{
					n++;
				}
				else
				{
					pairs[i] |= grid.GetCandidates(cells[(i << 1) + 1]);
				}

				if (n > 4 || PopCount((uint)pairs[i]) > 5 || pairs[i] == 0)
				{
					break;
				}

				candidateCount += PopCount((uint)pairs[i]);
			}

			// Check validity: If the number of candidate appearing is lower than 32 - (n * 2), the state will become invalid.
			if (i < 8 || candidateCount > 32 - (n << 1))
			{
				continue;
			}

			if ((Mask)(pairs[0] & pairs[1]) is not (var candidateMask and not 0))
			{
				continue;
			}

			// Check all combinations.
			var masks = MaskOperations.GetMaskSubsets(candidateMask);
			for (var j = masks.Length - 1; j >= 0; j--)
			{
				var mask = masks[j];
				if (mask == 0)
				{
					continue;
				}

				tempLink.Clear();

				// Check the associativity:
				// Each pair should find the digits that can combine with the next pair.
				var linkCount = PopCount((uint)(tempLink[0] = mask));
				var k = 1;
				for (; k < 8; k++)
				{
					candidateMask = (Mask)(tempLink[k - 1] ^ pairs[k]);
					if ((candidateMask & pairs[(k + 1) % 8]) != candidateMask)
					{
						break;
					}

					linkCount += PopCount((uint)(tempLink[k] = candidateMask));
				}

				if (k < 8 || linkCount != 16 - n)
				{
					continue;
				}

				// Last check: Check the first and the last pair.
				candidateMask = (Mask)(tempLink[7] ^ pairs[0]);
				if ((candidateMask & pairs[(k + 1) % 8]) != candidateMask)
				{
					continue;
				}

				// Check elimination map.
				linkHouse[0] = cells[0].ToHouseIndex(HouseType.Row);
				linkHouse[1] = cells[2].ToHouseIndex(HouseType.Block);
				linkHouse[2] = cells[4].ToHouseIndex(HouseType.Column);
				linkHouse[3] = cells[6].ToHouseIndex(HouseType.Block);
				linkHouse[4] = cells[8].ToHouseIndex(HouseType.Row);
				linkHouse[5] = cells[10].ToHouseIndex(HouseType.Block);
				linkHouse[6] = cells[12].ToHouseIndex(HouseType.Column);
				linkHouse[7] = cells[14].ToHouseIndex(HouseType.Block);
				var conclusions = new List<Conclusion>();
				var map = [.. cells] & EmptyCells;
				for (k = 0; k < 8; k++)
				{
					if (((HousesMap[linkHouse[k]] & EmptyCells) & ~map) is not (var elimMap and not []))
					{
						continue;
					}

					foreach (var cell in elimMap)
					{
						if ((Mask)(grid.GetCandidates(cell) & tempLink[k]) is var digits and not 0)
						{
							foreach (var digit in digits)
							{
								conclusions.Add(new(Elimination, cell, digit));
							}
						}
					}
				}

				// Check the number of the available eliminations.
				if (conclusions.Count == 0)
				{
					continue;
				}

				// Highlight candidates.
				var candidateOffsets = new List<CandidateViewNode>();
				var link = new Mask[27];
				for (k = 0; k < 8; k++)
				{
					link[linkHouse[k]] = tempLink[k];
					foreach (var cell in map & HousesMap[linkHouse[k]])
					{
						var cands = (Mask)(grid.GetCandidates(cell) & tempLink[k]);
						if (cands == 0)
						{
							continue;
						}

						foreach (var digit in cands)
						{
							candidateOffsets.Add(
								new(
									(k & 3) switch
									{
										0 => ColorIdentifier.Auxiliary1,
										1 => ColorIdentifier.Auxiliary2,
										2 => ColorIdentifier.Auxiliary3,
										_ => ColorIdentifier.Normal
									},
									cell * 9 + digit
								)
							);
						}
					}
				}

				// Gather the result.
				var step = new DominoLoopStep([.. conclusions], [[.. candidateOffsets]], context.Options, [.. cells]);
				if (context.OnlyFindOne)
				{
					return step;
				}

				context.Accumulator.Add(step);
			}
		}

		return null;
	}
}
