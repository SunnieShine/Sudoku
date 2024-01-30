namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with a <b>Single</b> step searcher. The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>
/// Common techniques:
/// <list type="bullet">
/// <item>Full House (If the property <see cref="EnableFullHouse"/> is <see langword="true"/>)</item>
/// <item>Last Digit (If the property <see cref="EnableLastDigit"/> is <see langword="true"/>)</item>
/// <item>Naked Single</item>
/// </list>
/// </item>
/// <item>
/// Direct techniques:
/// <list type="bullet">
/// <item>Crosshatching</item>
/// </list>
/// </item>
/// <item>
/// Indirect techniques:
/// <list type="bullet">
/// <item>Hidden Single</item>
/// </list>
/// </item>
/// </list>
/// </summary>
[StepSearcher(
	Technique.CrosshatchingBlock, Technique.CrosshatchingRow, Technique.CrosshatchingColumn, Technique.LastDigit,
	Technique.FullHouse, Technique.HiddenSingleBlock, Technique.HiddenSingleRow, Technique.HiddenSingleColumn, Technique.NakedSingle,
	IsPure = true, IsFixed = true)]
[StepSearcherRuntimeName("StepSearcherName_SingleStepSearcher")]
public sealed partial class SingleStepSearcher : StepSearcher
{
	/// <summary>
	/// Indicates whether the solver enables the technique full house.
	/// </summary>
	[RuntimeIdentifier(RuntimeIdentifier.EnableFullHouse)]
	public bool EnableFullHouse { get; set; }

	/// <summary>
	/// Indicates whether the solver enables the technique last digit.
	/// </summary>
	[RuntimeIdentifier(RuntimeIdentifier.EnableLastDigit)]
	public bool EnableLastDigit { get; set; }

	/// <summary>
	/// Indicates whether the solver checks for hidden single in block firstly.
	/// </summary>
	[RuntimeIdentifier(RuntimeIdentifier.HiddenSinglesInBlockFirst)]
	public bool HiddenSinglesInBlockFirst { get; set; }

	/// <summary>
	/// Indicates whether the solver uses ittoryu mode to solve a puzzle.
	/// </summary>
	[RuntimeIdentifier(RuntimeIdentifier.AnalyzerUseIttoryuMode)]
	public bool UseIttoryuMode { get; set; }


	/// <inheritdoc/>
	protected internal override Step? Collect(scoped ref AnalysisContext context)
		=> UseIttoryuMode ? Collect_IttoryuMode(ref context) : Collect_NonIttoryuMode(ref context);

	/// <summary>
	/// Checks for single steps using ittoryu mode.
	/// </summary>
	/// <param name="context"><inheritdoc cref="Collect(ref AnalysisContext)" path="/param[@name='context']"/></param>
	/// <returns><inheritdoc cref="Collect(ref AnalysisContext)" path="/returns"/></returns>
	private Step? Collect_IttoryuMode(scoped ref AnalysisContext context)
	{
		scoped ref readonly var grid = ref context.Grid;
		for (var (i, digit) = (0, context.PreviousSetDigit); i < 9; i++, digit = (digit + 1) % 9)
		{
			if (!EnableFullHouse)
			{
				goto CheckForHiddenSingle;
			}

			for (var house = 0; house < 27; house++)
			{
				var (count, resultCell, flag) = (0, -1, true);
				foreach (var cell in HousesMap[house])
				{
					if (grid.GetState(cell) == CellState.Empty && (resultCell = cell) is var _ && ++count > 1)
					{
						flag = false;
						break;
					}
				}
				if (!flag || count == 0)
				{
					continue;
				}

				if (TrailingZeroCount(grid.GetCandidates(resultCell)) != digit)
				{
					continue;
				}

				var emptyCellsCountFromAllPeerHouses = 0;
				foreach (var houseType in HouseTypes)
				{
					var peerHouse = resultCell.ToHouseIndex(houseType);
					foreach (var cell in HouseCells[peerHouse])
					{
						if (grid.GetState(cell) == CellState.Empty)
						{
							emptyCellsCountFromAllPeerHouses++;
						}
					}
				}

				var step = new FullHouseStep(
					[new(Assignment, resultCell, digit)],
					[[new HouseViewNode(ColorIdentifier.Normal, house)]],
					context.PredefinedOptions,
					house,
					resultCell,
					digit
				);

				if (context.OnlyFindOne)
				{
					context.PreviousSetDigit = digit;
					return step;
				}

				context.Accumulator.Add(step);
			}

		CheckForHiddenSingle:
			for (var house = 0; house < 27; house++)
			{
				if (CheckForHiddenSingleAndLastDigit(this, in grid, ref context, digit, house) is not { } step)
				{
					continue;
				}

				if (context.OnlyFindOne)
				{
					context.PreviousSetDigit = digit;
					return step;
				}

				context.Accumulator.Add(step);
			}

			for (var cell = 0; cell < 81; cell++)
			{
				if (grid.GetState(cell) != CellState.Empty)
				{
					continue;
				}

				if (grid.GetCandidates(cell) is var mask && !IsPow2(mask))
				{
					continue;
				}

				if (TrailingZeroCount(mask) != digit)
				{
					continue;
				}

				if (GetNakedSingleSubtype(in grid, cell) is var subtype && subtype.IsUnnecessary())
				{
					continue;
				}

				var cellOffsets = GetNakedSingleExcluders(in grid, cell, digit, out _);
				var step = new NakedSingleStep(
					[new(Assignment, cell, digit)],
					[[.. cellOffsets]],
					context.PredefinedOptions,
					cell,
					digit,
					subtype
				);
				if (context.OnlyFindOne)
				{
					context.PreviousSetDigit = digit;
					return step;
				}

				context.Accumulator.Add(step);
			}
		}

		return null;
	}

	/// <summary>
	/// Checks for single steps using non-ittoryu mode.
	/// </summary>
	/// <param name="context"><inheritdoc cref="Collect(ref AnalysisContext)" path="/param[@name='context']"/></param>
	/// <returns><inheritdoc cref="Collect(ref AnalysisContext)" path="/returns"/></returns>
	private unsafe Step? Collect_NonIttoryuMode(scoped ref AnalysisContext context)
	{
		scoped ref readonly var grid = ref context.Grid;
		var isFullyMarkedMode = !context.PredefinedOptions.DistinctDirectMode || !context.PredefinedOptions.IsDirectMode;

		// Please note that, by default we should start with hidden singles. However, if a user has set the option
		// that a step searcher should distinct with direct mode and in-direct mode (i.e. all candidates are displayed),
		// we should start with a naked single because they are "direct" in such mode.
		var p = stackalloc SingleModuleSearcherFunc[] { &CheckFullHouse, &CheckNakedSingle, &CheckHiddenSingle };
		var q = stackalloc SingleModuleSearcherFunc[] { &CheckFullHouse, &CheckHiddenSingle, &CheckNakedSingle };
		var r = stackalloc SingleModuleSearcherFunc[] { &CheckNakedSingle, &CheckHiddenSingle };
		var s = stackalloc SingleModuleSearcherFunc[] { &CheckHiddenSingle, &CheckNakedSingle };
		var searchers = (EnableFullHouse, isFullyMarkedMode) switch { (true, true) => p, (true, _) => q, (_, true) => r, _ => s };
		for (var i = 0; i < (searchers == p || searchers == q ? 3 : 2); i++)
		{
			if (searchers[i](this, ref context, in grid) is { } step)
			{
				return step;
			}
		}

		return null;
	}


	/// <summary>
	/// Get subtype of the hidden single.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="cell">The cell.</param>
	/// <param name="house">Indicates the house.</param>
	/// <param name="chosenCells">The chosen cells.</param>
	/// <returns>The subtype of the hidden single.</returns>
	private static SingleSubtype GetHiddenSingleSubtype(scoped ref readonly Grid grid, Cell cell, House house, scoped ref readonly CellMap chosenCells)
	{
		scoped ref readonly var houseCells = ref HousesMap[house];
		var (b, r, c) = (0, 0, 0);
		foreach (var chosenCell in chosenCells)
		{
			foreach (var houseType in HouseTypes)
			{
				if (HousesMap[chosenCell.ToHouseIndex(houseType)] & houseCells)
				{
					(houseType == HouseType.Block ? ref b : ref houseType == HouseType.Row ? ref r : ref c)++;
					break;
				}
			}
		}

		return Enum.Parse<SingleSubtype>(
			house switch
			{
				>= 0 and < 9 => $"{HouseType.Block}HiddenSingle0{r}{c}",
				>= 9 and < 18 => $"{HouseType.Row}HiddenSingle{b}0{c}",
				>= 18 and < 27 => $"{HouseType.Column}HiddenSingle{b}{r}0"
			}
		);
	}

	/// <summary>
	/// Get subtype of the naked single.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="cell">The cell.</param>
	/// <returns>The subtype of the naked single.</returns>
	private static SingleSubtype GetNakedSingleSubtype(scoped ref readonly Grid grid, Cell cell)
	{
		var valuesCountInBlock = 0;
		foreach (var c in HousesMap[cell.ToHouseIndex(HouseType.Block)])
		{
			if (grid.GetState(c) != CellState.Empty)
			{
				valuesCountInBlock++;
			}
		}
		return valuesCountInBlock switch
		{
			0 => SingleSubtype.NakedSingle0,
			1 => SingleSubtype.NakedSingle1,
			2 => SingleSubtype.NakedSingle2,
			3 => SingleSubtype.NakedSingle3,
			4 => SingleSubtype.NakedSingle4,
			5 => SingleSubtype.NakedSingle5,
			6 => SingleSubtype.NakedSingle6,
			7 => SingleSubtype.NakedSingle7,
			_ => SingleSubtype.NakedSingle8
		};
	}

	/// <summary>
	/// Check for full houses.
	/// </summary>
	private static FullHouseStep? CheckFullHouse(SingleStepSearcher @this, scoped ref AnalysisContext context, scoped ref readonly Grid grid)
	{
		for (var house = 0; house < 27; house++)
		{
			var (count, resultCell, flag) = (0, -1, true);
			foreach (var cell in HousesMap[house])
			{
				if (grid.GetState(cell) == CellState.Empty)
				{
					resultCell = cell;
					if (++count > 1)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag || count == 0)
			{
				continue;
			}

			var digit = TrailingZeroCount(grid.GetCandidates(resultCell));
			var step = new FullHouseStep(
				[new(Assignment, resultCell, digit)],
				[[new HouseViewNode(ColorIdentifier.Normal, house)]],
				context.PredefinedOptions,
				house,
				resultCell,
				digit
			);
			if (context.OnlyFindOne)
			{
				return step;
			}

			context.Accumulator.Add(step);
		}

		return null;
	}

	/// <summary>
	/// Check for hidden singles.
	/// </summary>
	private static HiddenSingleStep? CheckHiddenSingle(SingleStepSearcher @this, scoped ref AnalysisContext context, scoped ref readonly Grid grid)
	{
		if (@this.HiddenSinglesInBlockFirst)
		{
			// If block first, we'll extract all blocks and iterate on them firstly.
			for (var house = 0; house < 9; house++)
			{
				for (var digit = 0; digit < 9; digit++)
				{
					if (CheckForHiddenSingleAndLastDigit(@this, in grid, ref context, digit, house) is not { } step)
					{
						continue;
					}

					if (context.OnlyFindOne)
					{
						return step;
					}

					context.Accumulator.Add(step);
				}
			}

			// Then secondly rows and columns.
			for (var house = 9; house < 27; house++)
			{
				for (var digit = 0; digit < 9; digit++)
				{
					if (CheckForHiddenSingleAndLastDigit(@this, in grid, ref context, digit, house) is not { } step)
					{
						continue;
					}

					if (context.OnlyFindOne)
					{
						return step;
					}

					context.Accumulator.Add(step);
				}
			}
		}
		else
		{
			// We'll directly iterate on each house.
			// Theoretically, this iteration should be faster than above one, but in practice,
			// we may found hidden singles in block much more times than in row or column.
			for (var digit = 0; digit < 9; digit++)
			{
				for (var house = 0; house < 27; house++)
				{
					if (CheckForHiddenSingleAndLastDigit(@this, in grid, ref context, digit, house) is not { } step)
					{
						continue;
					}

					if (context.OnlyFindOne)
					{
						return step;
					}

					context.Accumulator.Add(step);
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Check for naked singles.
	/// </summary>
	private static NakedSingleStep? CheckNakedSingle(SingleStepSearcher @this, scoped ref AnalysisContext context, scoped ref readonly Grid grid)
	{
		for (var cell = 0; cell < 81; cell++)
		{
			if (grid.GetState(cell) != CellState.Empty)
			{
				continue;
			}

			var mask = grid.GetCandidates(cell);
			if (!IsPow2(mask))
			{
				continue;
			}

			var digit = TrailingZeroCount(mask);
			var cellOffsets = GetNakedSingleExcluders(in grid, cell, digit, out _);
			var step = new NakedSingleStep(
				[new(Assignment, cell, digit)],
				[[.. cellOffsets]],
				context.PredefinedOptions,
				cell,
				digit,
				GetNakedSingleSubtype(in grid, cell)
			);
			if (context.OnlyFindOne)
			{
				return step;
			}

			context.Accumulator.Add(step);
		}

		return null;
	}

	/// <summary>
	/// Checks for existence of hidden single and last digit conclusion in the specified house.
	/// </summary>
	/// <param name="this">The current instance.</param>
	/// <param name="grid">The grid.</param>
	/// <param name="context">The context.</param>
	/// <param name="digit">The digit used.</param>
	/// <param name="house">The house used.</param>
	/// <returns>Not <see langword="null"/> if conclusion can be found.</returns>
	/// <remarks>
	/// <para><include file="../../global-doc-comments.xml" path="/g/developer-notes"/></para>
	/// <para>
	/// The main idea of hidden single is to search for a digit can only appear once in a house,
	/// so we should check all possibilities in a house to found whether the house exists a digit
	/// that only appears once indeed.
	/// </para>
	/// </remarks>
	private static HiddenSingleStep? CheckForHiddenSingleAndLastDigit(
		SingleStepSearcher @this,
		scoped ref readonly Grid grid,
		scoped ref AnalysisContext context,
		Digit digit,
		House house
	)
	{
		var (count, resultCell, flag) = (0, -1, true);
		foreach (var cell in HousesMap[house])
		{
			if (grid.Exists(cell, digit) is true)
			{
				resultCell = cell;
				if (++count > 1)
				{
					flag = false;
					break;
				}
			}
		}
		if (!flag || count == 0)
		{
			// The digit has been filled into the house, or the digit appears more than once,
			// it will be invalid for a hidden single. Just skip it.
			return null;
		}

		// The digit is a hidden single.
		// Now collect information (especially for rendering & text) from the current found step.
		var (enableAndIsLastDigit, cellOffsets) = (false, new List<CellViewNode>());
		if (@this.EnableLastDigit)
		{
			// Sum up the number of appearing in the grid of 'digit'.
			var digitCount = 0;
			for (var cell = 0; cell < 81; cell++)
			{
				if (grid.GetDigit(cell) == digit)
				{
					digitCount++;
					cellOffsets.Add(new(ColorIdentifier.Normal, cell) { RenderingMode = BothDirectAndPencilmark });
				}
			}

			enableAndIsLastDigit = digitCount == 8;
		}

		return (enableAndIsLastDigit, house) switch
		{
			(true, >= 9) => null,
			(true, _) => new LastDigitStep([new(Assignment, resultCell, digit)], [[.. cellOffsets]], context.PredefinedOptions, resultCell, digit, house),
			_ when GetHiddenSingleExcluders(in grid, digit, house, resultCell, out var chosenCells) is var cellOffsets2
				=> GetHiddenSingleSubtype(in grid, resultCell, house, in chosenCells) switch
				{
					var subtype when subtype.IsUnnecessary() => null,
					var subtype => new HiddenSingleStep(
						[new(Assignment, resultCell, digit)],
						[[.. cellOffsets2, new HouseViewNode(ColorIdentifier.Normal, house)]],
						context.PredefinedOptions,
						resultCell,
						digit,
						house,
						enableAndIsLastDigit,
						subtype
					)
				}
		};
	}

	/// <summary>
	/// Try to create a list of <see cref="CellViewNode"/>s indicating the crosshatching base cells.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="digit">The digit.</param>
	/// <param name="house">The house.</param>
	/// <param name="cell">The cell.</param>
	/// <param name="chosenCells">The chosen cells.</param>
	/// <returns>A list of <see cref="CellViewNode"/> instances.</returns>
	private static CellViewNode[] GetHiddenSingleExcluders(
		scoped ref readonly Grid grid,
		Digit digit,
		House house,
		Cell cell,
		out CellMap chosenCells
	)
	{
		if (Crosshatching.GetCrosshatchingInfo(in grid, digit, house, in CellsMap[cell]) is { } info)
		{
			(chosenCells, var covered, var excluded) = info;
			return [
				.. from c in chosenCells select new CellViewNode(ColorIdentifier.Normal, c) { RenderingMode = DirectModeOnly },
				..
				from c in covered
				let p = excluded.Contains(c) ? ColorIdentifier.Auxiliary2 : ColorIdentifier.Auxiliary1
				select new CellViewNode(p, c) { RenderingMode = DirectModeOnly }
			];
		}

		chosenCells = [];
		return [];
	}

	/// <summary>
	/// Get all <see cref="CellViewNode"/>s that represents as excluders.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="cell">The cell.</param>
	/// <param name="digit">The digit.</param>
	/// <param name="excluderHouses">The excluder houses.</param>
	/// <returns>A list of <see cref="CellViewNode"/> instances.</returns>
	private static CellViewNode[] GetNakedSingleExcluders(scoped ref readonly Grid grid, Cell cell, Digit digit, out House[] excluderHouses)
	{
		(var result, var i, excluderHouses) = (new CellViewNode[8], 0, new House[8]);
		foreach (var otherDigit in (Mask)(Grid.MaxCandidatesMask & (Mask)~(1 << digit)))
		{
			foreach (var otherCell in Peers[cell])
			{
				if (grid.GetDigit(otherCell) == otherDigit)
				{
					result[i] = new(ColorIdentifier.Normal, otherCell) { RenderingMode = DirectModeOnly };
					(CellsMap[cell] + otherCell).InOneHouse(out excluderHouses[i]);

					i++;
					break;
				}
			}
		}

		return result;
	}
}
