using System.Numerics;
using System.Runtime.CompilerServices;
using Sudoku.Analytics.Categorization;
using Sudoku.Analytics.Metadata;
using Sudoku.Analytics.Steps;
using Sudoku.Concepts;
using Sudoku.Linq;
using Sudoku.Rendering;
using Sudoku.Rendering.Nodes;
using Sudoku.Runtime.MaskServices;
using static System.Numerics.BitOperations;
using static Sudoku.Analytics.CachedFields;
using static Sudoku.Analytics.ConclusionType;
using static Sudoku.SolutionWideReadOnlyFields;

namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with an <b>Exocet</b> step searcher.
/// The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>Junior Exocet</item>
/// <item>Senior Exocet</item>
/// </list>
/// </summary>
[StepSearcher(
	Technique.JuniorExocet, Technique.JuniorExocetConjugatePair, Technique.JuniorExocetMirror, Technique.JuniorExocetSingleMirror,
	Technique.JuniorExocetIncompatiblePair, Technique.JuniorExocetTargetPair, Technique.JuniorExocetGeneralizedFish,
	Technique.JuniorExocetMirrorAlmostHiddenSet, Technique.SeniorExocet, Technique.SeniorExocetMirror, Technique.SeniorExocetSingleMirror)]
public sealed partial class ExocetStepSearcher : StepSearcher
{
	/// <summary>
	/// Indicates the mini-lines to be iterated, grouped by chute index.
	/// </summary>
	private static readonly CellMap[][] MinilinesGroupedByChuteIndex;


	/// <include file='../../global-doc-comments.xml' path='g/static-constructor' />
	static ExocetStepSearcher()
	{
		MinilinesGroupedByChuteIndex = new CellMap[6][];
		for (var i = 0; i < 6; i++)
		{
			MinilinesGroupedByChuteIndex[i] = [[], [], [], [], [], [], [], [], []];

			var (_, _, _, chuteHouses) = Chutes[i];
			var isRow = i is 0 or 1 or 2;
			var tempIndex = 0;
			foreach (var chuteHouse in chuteHouses)
			{
				for (var (houseCell, j) = (HouseFirst[chuteHouse], 0); j < 3; houseCell += isRow ? 3 : 27, j++)
				{
					scoped ref var current = ref MinilinesGroupedByChuteIndex[i][tempIndex++];
					current.Add(houseCell);
					current.Add(houseCell + (isRow ? 1 : 9));
					current.Add(houseCell + (isRow ? 2 : 18));
				}
			}
		}
	}


	/// <inheritdoc/>
	protected internal override Step? Collect(scoped ref AnalysisContext context)
	{
		if (CollectForAllRowColumnType(ref context) is { } baseTypeStep)
		{
			return baseTypeStep;
		}

		return null;
	}

	/// <summary>
	/// Try to fetch for all-row or all-column types.
	/// </summary>
	/// <param name="context"><inheritdoc cref="StepSearcher.Collect(ref AnalysisContext)" path="/param[@name='context']"/></param>
	/// <returns><inheritdoc cref="StepSearcher.Collect(ref AnalysisContext)" path="/returns"/></returns>
	private ExocetStep? CollectForAllRowColumnType(scoped ref AnalysisContext context)
	{
		scoped ref readonly var grid = ref context.Grid;

		// Iterate by size of houses to be iterated.
		scoped var chuteIndexBox = (stackalloc int[3]);
		foreach (var isRow in (true, false))
		{
			for (var size = 3; size <= 4; size++)
			{
				// Iterate on all possible rows and columns on size 3 or 4.
				foreach (var houses in (isRow ? AllRowsMask : AllColumnsMask).GetAllSets().GetSubsets(size))
				{
					var housesEmptyCells = CellMap.Empty;
					var housesCells = CellMap.Empty;
					var housesMask = MaskOperations.CreateHouse(houses);
					foreach (var house in houses)
					{
						housesEmptyCells |= HousesMap[house] & EmptyCells;
						housesCells |= HousesMap[house];
					}

					// We manually disable the case that 3 of 3 (or 4) houses in a same chute.
					chuteIndexBox.Clear();
					foreach (var house in houses)
					{
						chuteIndexBox[(isRow ? house - 9 : house - 18) / 3]++;
					}
					if (chuteIndexBox.Contains(3))
					{
						// This case is invalid.
						continue;
					}

					// Iterate on each chute (mega rows or columns) in order to check for each empty cell,
					// determining whether it can be used as a base.
					for (var (i, timesOfI) = (isRow ? 3 : 0, 0); timesOfI < 3; i++, timesOfI++)
					{
						var (_, chuteCells, _, chuteHouses) = Chutes[i];
						var chuteEmptyCells = chuteCells & EmptyCells;

						// Now iterate by size of base cells. The minimum value is 1, e.g.:
						//
						//   ..64.....1....39.7.5.............3..2....1.89....59....4......83....2....126...7.
						//
						// For digits 4 & 5 in houses r258, with base cell r9c7 and target cell r2c8 => r2c8 must be 4 or 5.
						for (var sizeOfBaseAndTarget = 1; sizeOfBaseAndTarget <= 2; sizeOfBaseAndTarget++)
						{
							// Iterate on each empty cells, or a cell group whose length is equal to iteration variable 'baseCellsSize'.
							scoped ref readonly var chuteHousesInCurrentChute = ref MinilinesGroupedByChuteIndex[i];
							for (var j = 0; j < 9; j++)
							{
								scoped ref readonly var minilineBaseCells = ref chuteHousesInCurrentChute[j];
								var baseEmptyCellsToBeIterated = minilineBaseCells & EmptyCells;
								if (!baseEmptyCellsToBeIterated)
								{
									// No cells can be iterated.
									continue;
								}

								// Iterate on each miniline, to get all possible cases.
								foreach (ref readonly var baseCells in baseEmptyCellsToBeIterated.GetSubsets(sizeOfBaseAndTarget).EnumerateRef())
								{
									if (housesEmptyCells & baseCells)
									{
										// Base cells shouldn't be located in the current list of houses being iterated.
										continue;
									}

									var baseCellsDigitsMask = grid[in baseCells];
									if (PopCount((uint)baseCellsDigitsMask) > baseCells.Count + 3)
									{
										// The base cells hold too much digits to be checked.
										continue;
									}

									// Now we should check for target cells.
									// The target cells must be located in houses being iterated, and intersects with the current chute.
									var targetCells = (chuteEmptyCells & housesEmptyCells) - baseCells.PeerIntersection;
									var generializedTargetCells = (housesCells & chuteCells) - baseCells.PeerIntersection;
									var targetCellsDigitsMask = grid[in targetCells];
									if ((targetCellsDigitsMask & baseCellsDigitsMask) == 0)
									{
										// They are out of relation.
										continue;
									}

									// Check whether all digits appeared in base cells can be filled in target empty cells.
									var allDigitsCanBeFilledInTargetEmptyCells = true;
									foreach (var digit in baseCellsDigitsMask)
									{
										if (!(targetCells & CandidatesMap[digit]))
										{
											allDigitsCanBeFilledInTargetEmptyCells = false;
											break;
										}
									}
									if (!allDigitsCanBeFilledInTargetEmptyCells)
									{
										continue;
									}

									// Check whether generalized target cells (non-empty) don't contain
									// any possible digits appeared in base cells.
									var targetUncoveredCellsContainDigitsAppearedInBaseCells = false;
									foreach (var cell in generializedTargetCells - EmptyCells)
									{
										if ((baseCellsDigitsMask >> grid.GetDigit(cell) & 1) != 0)
										{
											targetUncoveredCellsContainDigitsAppearedInBaseCells = true;
											break;
										}
									}
									if (targetUncoveredCellsContainDigitsAppearedInBaseCells)
									{
										continue;
									}

									// Get the count delta (target group by block.count - base.count). The result value must be -2, -1 or 0.
									// The details are mentioned below:
									//
									//   1) if < 0, the base contain more cells than the target, meaning the pattern may be a senior exocet;
									//   2) if > 0, the target contain more cells than the base,
									//      meaning the pattern contains more group than the number of base cells,
									//      which will lead to no conclusion;
									//   3) if == 0, the base has same number of cells with the target, a standard junior exocet will be formed.
									//
									// Therefore, I just check for the value on -2, -1 and 0.
									var delta = (from c in targetCells group c by c.ToHouseIndex(HouseType.Block)).Length - baseCells.Count;
									switch (delta)
									{
										case -1: // Note: Today we should only consider the cases on delta != -2.
										{
											// Try to fetch all possible endo-target cells if worth.
											// Check whether cross-line non-empty cells contains digits appeared in base cells.
											// If so, they will be endo-target cells.
											// The maximum possible number of appearing times is 2, corresponding to the real target cells count.
											var crossline = housesCells - chuteCells;
											var crosslineContainsDigitsAppearedInBaseCells = false;
											foreach (var cell in crossline)
											{
												if ((baseCellsDigitsMask >> grid.GetDigit(cell) & 1) != 0)
												{
													crosslineContainsDigitsAppearedInBaseCells = true;
													break;
												}
											}
											if (crosslineContainsDigitsAppearedInBaseCells)
											{
												continue;
											}

											var endoTargetCells = CellMap.Empty;

											// Here delta is strictly equal to -1 because I disable delta == -2 temporarily.
											foreach (var cell in crossline)
											{
												if (grid.GetState(cell) != CellState.Empty)
												{
													continue;
												}

												// Endo-target cells must contain at least one digit appeared in base cells.
												if ((grid.GetCandidates(cell) & baseCellsDigitsMask) == 0)
												{
													continue;
												}

												// Check if the current cell is filled with the digit not appeared in base cells,
												// then all base cell digits can only fill (size - 1) times at most in cross-line cells.
												// For example, if the size = 3, digits should only appear 2 times at most in cross-line cells.
												// If greater (times > size - 1), an exocet cannot be formed;
												// and if less (times < size - 1), we cannot conclude which digits are the target cells.
												var allDigitsCanBeFilledExactlySizeMinusOneTimes = true;
												foreach (var digit in baseCellsDigitsMask)
												{
													var mostTimes = MostTimesOf(digit, housesMask, chuteCells + cell);
													if (mostTimes != size - 1)
													{
														allDigitsCanBeFilledExactlySizeMinusOneTimes = false;
														break;
													}
												}
												if (!allDigitsCanBeFilledExactlySizeMinusOneTimes)
												{
													// All digits should strictly appear (size - 1) times at most in cross-line cells.
													continue;
												}

												endoTargetCells.Add(cell);
											}

											if (!endoTargetCells)
											{
												// No possible endo-target cells are found.
												continue;
											}

											foreach (var endoTargetCell in endoTargetCells)
											{
												if (CheckBaseJeOrSe(
													ref context, grid, in baseCells, in targetCells, endoTargetCell, in crossline,
													baseCellsDigitsMask, housesMask, out _
												) is { } baseTypeStep)
												{
													return baseTypeStep;
												}
											}
											break;
										}
										case 0:
										{
											// Check whether cross-line non-empty cells contains digits appeared in base cells.
											var crossline = housesCells - chuteCells;
											var crosslineContainsDigitsAppearedInBaseCells = false;
											foreach (var cell in crossline)
											{
												if ((baseCellsDigitsMask >> grid.GetDigit(cell) & 1) != 0)
												{
													crosslineContainsDigitsAppearedInBaseCells = true;
													break;
												}
											}
											if (crosslineContainsDigitsAppearedInBaseCells)
											{
												continue;
											}

											// Check for maximum times can be appeared in cross-line cells.
											var allDigitsCanBeFilledExactlySizeMinusOneTimes = true;
											foreach (var digit in baseCellsDigitsMask)
											{
												var mostTimes = MostTimesOf(digit, housesMask, in chuteCells);
												if (mostTimes != size - 1)
												{
													allDigitsCanBeFilledExactlySizeMinusOneTimes = false;
													break;
												}
											}
											if (!allDigitsCanBeFilledExactlySizeMinusOneTimes)
											{
												// All digits should strictly appear (size - 1) times at most in cross-line cells.
												// For example, if the size = 3, digits should only appear 2 times at most in cross-line cells.
												// If greater (times > size - 1), an exocet cannot be formed;
												// and if less (times < size - 1), we cannot conclude which digits are the target cells.
												continue;
											}

											if (CheckBaseJeOrSe(
												ref context, grid, in baseCells, in targetCells, -1, in crossline, baseCellsDigitsMask,
												housesMask, out var inferredTargetConjugatePairs
											) is { } baseTypeStep)
											{
												return baseTypeStep;
											}

											if (CheckMirror(
												ref context, grid, in baseCells, in targetCells, in crossline, baseCellsDigitsMask, isRow, i,
												housesMask
											) is { } mirrorTypeStep)
											{
												return mirrorTypeStep;
											}

											if (CheckSingleMirror(
												ref context, grid, in baseCells, in targetCells, in crossline, baseCellsDigitsMask, isRow, i,
												housesMask
											) is { } singleMirrorTypeStep)
											{
												return singleMirrorTypeStep;
											}

											if (CheckIncompatiblePair(
												ref context, grid, in baseCells, in targetCells, in crossline, baseCellsDigitsMask, delta,
												out var inferredTargetPairMask, housesMask
											) is { } incompatiblePairTypeStep)
											{
												return incompatiblePairTypeStep;
											}

											if (CheckTargetPair(
												ref context, grid, in baseCells, in targetCells, in crossline, baseCellsDigitsMask,
												inferredTargetPairMask, delta, housesMask, inferredTargetConjugatePairs
											) is { } targetPairTypeStep)
											{
												return targetPairTypeStep;
											}

											if (CheckGeneralizedFish(
												ref context, grid, in baseCells, in targetCells, in crossline, baseCellsDigitsMask,
												inferredTargetPairMask, delta, isRow, housesMask
											) is { } generalizedFishTypeStep)
											{
												return generalizedFishTypeStep;
											}

											if (CheckMirrorAlmostHiddenSet(
												ref context, grid, in baseCells, in targetCells, in crossline, baseCellsDigitsMask,
												delta, isRow, housesMask, i
											) is { } mirrorAhsTypeStep)
											{
												return mirrorAhsTypeStep;
											}

											break;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		return null;
	}


	private static ExocetBaseStep? CheckBaseJeOrSe(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		Cell endoTargetCell,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		HouseMask housesMask,
		out ReadOnlySpan<Conjugate> inferredTargetConjugatePairs
	)
	{
		var conclusions = new List<Conclusion>();
		var conjugatePairs = new List<Conjugate>(2);
		switch (targetCells.Count)
		{
			case 2:
			{
				foreach (var cell in endoTargetCell == -1 ? targetCells : targetCells + endoTargetCell)
				{
					foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~baseCellsDigitsMask))
					{
						conclusions.Add(new(Elimination, cell, digit));
					}
				}
				break;
			}
			case 3 or 4 when endoTargetCell == -1:
			{
				scoped var cellGroups = from cell in targetCells group cell by cell.ToHouseIndex(HouseType.Block);
				foreach (var cellGroup in cellGroups)
				{
					switch (cellGroup.Values.Count)
					{
						case 1:
						{
							foreach (var cell in cellGroup)
							{
								foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~baseCellsDigitsMask))
								{
									conclusions.Add(new(Elimination, cell, digit));
								}
							}
							break;
						}
						case 2:
						{
							var cellsInThisBlock = cellGroup.Values;
							var digitsMask = (Mask)(grid[in cellsInThisBlock, false, GridMaskMergingMethod.And] & ~baseCellsDigitsMask);
							if (digitsMask == 0)
							{
								break;
							}

							foreach (var coveredLine in cellsInThisBlock.CoveredHouses)
							{
								foreach (var conjugatePairDigit in digitsMask)
								{
									if ((HousesMap[coveredLine] & CandidatesMap[conjugatePairDigit]) != cellsInThisBlock)
									{
										continue;
									}

									// This digit is a conjugate pair.
									foreach (var cell in cellsInThisBlock)
									{
										foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~baseCellsDigitsMask & ~(1 << conjugatePairDigit)))
										{
											conclusions.Add(new(Elimination, cell, digit));
										}
									}

									conjugatePairs.Add(new(in cellsInThisBlock, conjugatePairDigit));
									break;
								}
							}
							break;
						}
						default:
						{
							throw new InvalidOperationException("The value is unsupported and invalid.");
						}
					}
				}
				break;
			}
		}

		// Try to get conjugate pairs in target cells.
		inferredTargetConjugatePairs = conjugatePairs.ToArray();

		if (conclusions.Count == 0)
		{
			// No eliminations found.
			return null;
		}

		var step = new ExocetBaseStep(
			[.. conclusions],
			[
				[
					.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
					.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
					.. endoTargetCell != -1 ? [new CellViewNode(WellKnownColorIdentifier.Auxiliary1, endoTargetCell)] : (ViewNode[])[],
					.. from cell in crossline - endoTargetCell select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
					.. conjugatePairs.Count != 0
						?
						from conjugatePair in conjugatePairs
						from cell in conjugatePair.Map
						select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary3, cell * 9 + conjugatePair.Digit)
						: [],
					..
					from cell in baseCells
					from digit in grid.GetCandidates(cell)
					select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + digit),
					..
					from cell in crossline - endoTargetCell
					where grid.GetState(cell) == CellState.Empty
					from digit in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
					select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary2, cell * 9 + digit),
					//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
				]
			],
			context.PredefinedOptions,
			baseCellsDigitsMask,
			in baseCells,
			in targetCells,
			endoTargetCell != -1 ? [endoTargetCell] : [],
			in crossline,
			[.. conjugatePairs]
		);
		if (context.OnlyFindOne)
		{
			return step;
		}

		context.Accumulator.Add(step);
		return null;
	}

	private static ExocetMirrorStep? CheckMirror(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		bool isRow,
		int chuteIndex,
		HouseMask housesMask
	)
	{
		var conclusions = new List<Conclusion>();
		var conjugatePairs = new List<Conjugate>(2);
		scoped var cellGroups = from cell in targetCells group cell by cell.ToHouseIndex(HouseType.Block);
		if (cellGroups.Length != 2)
		{
			return null;
		}

		foreach (ref readonly var cellGroup in cellGroups)
		{
			if (cellGroup.Values.Count == 2)
			{
				// If the number of target cells in one side is 2, we cannot determine which one is correct.
				continue;
			}

			foreach (var targetCell in cellGroup)
			{
				Unsafe.SkipInit(out CellMap miniline);
				foreach (ref readonly var temp in MinilinesGroupedByChuteIndex[chuteIndex].EnumerateRef())
				{
					if (temp.Contains(targetCell))
					{
						miniline = temp;
						break;
					}
				}

				var theOtherTwoCells = miniline - targetCell;
				var theOtherEmptyCells = theOtherTwoCells & EmptyCells;
				if (!theOtherEmptyCells)
				{
					// The current miniline cannot contain any eliminations.
					continue;
				}

				var otherCellsDigitsMask = grid[in theOtherEmptyCells];
				foreach (var house in theOtherEmptyCells.CoveredHouses)
				{
					// Check whether the current house has a conjugate pair in the current cells.
					foreach (var digit in otherCellsDigitsMask)
					{
						var cellsContainingDigit = (CandidatesMap[digit] & HousesMap[house]) - targetCell;
						if (cellsContainingDigit != theOtherEmptyCells)
						{
							continue;
						}

						// Here a conjugate pair will be formed.
						// Now check for eliminations.
						foreach (var elimCell in theOtherEmptyCells)
						{
							foreach (var elimDigit in (Mask)(grid.GetCandidates(elimCell) & ~baseCellsDigitsMask & ~(1 << digit)))
							{
								conclusions.Add(new(Elimination, elimCell, elimDigit));
							}
						}

						conjugatePairs.Add(new(in theOtherEmptyCells, digit));
					}
				}
			}
		}
		if (conclusions.Count == 0)
		{
			// No eliminations found.
			return null;
		}

		var step = new ExocetMirrorStep(
			[.. conclusions],
			[
				[
					.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
					.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
					.. from cell in crossline select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
					..
					from cell in baseCells
					from d in grid.GetCandidates(cell)
					select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + d),
					..
					from cell in crossline
					where grid.GetState(cell) == CellState.Empty
					from d in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
					select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary2, cell * 9 + d),
					..
					from conjugatePair in conjugatePairs
					from cell in conjugatePair.Map
					select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary3, cell * 9 + conjugatePair.Digit),
					//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
				]
			],
			context.PredefinedOptions,
			baseCellsDigitsMask,
			in baseCells,
			in targetCells,
			[],
			in crossline,
			[.. conjugatePairs]
		);
		if (context.OnlyFindOne)
		{
			return step;
		}

		context.Accumulator.Add(step);
		return null;
	}

	private static ExocetSingleMirrorStep? CheckSingleMirror(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		bool isRow,
		int chuteIndex,
		HouseMask housesMask
	)
	{
		var conclusions = new List<Conclusion>();
		var singleMirrors = CellMap.Empty;
		foreach (var cellGroup in from cell in targetCells group cell by cell.ToHouseIndex(HouseType.Block))
		{
			if (cellGroup.Values.Count == 2)
			{
				// This side contain 2 target empty cells. We cannot conclude for this case.
				continue;
			}

			foreach (var targetCell in cellGroup)
			{
				Unsafe.SkipInit(out CellMap miniline);
				foreach (ref readonly var temp in MinilinesGroupedByChuteIndex[chuteIndex].EnumerateRef())
				{
					if (temp.Contains(targetCell))
					{
						miniline = temp;
						break;
					}
				}

				if ((miniline - targetCell & EmptyCells) is not [var theOnlyMirrorCell])
				{
					// The mirror cells contain not 1 cell, it may not be included in this type.
					continue;
				}

				// Try to get the target cell that is not share with a same block with this mirror cell.
				var theOtherTargetCells = targetCells - targetCell;
				if (theOtherTargetCells is not [var theOtherTargetCell])
				{
					// The number of the other side of target cell is not 1, we cannot conclude for that.
					continue;
				}

				// Check for the only mirror cell, determining whether the cell contains an arbitrary extra digits.
				var digitsInMirrorCell = grid.GetCandidates(theOnlyMirrorCell);
				var elimDigitsFromTheOnlyMirrorCell = (Mask)(digitsInMirrorCell & ~baseCellsDigitsMask);

				// Check for the containing digits in mirror cells, and fetch which digits are appeared in base cells.
				// Such digits will be sync'ed with the other target cell.
				var containedDigitsAppearedInBaseCellsInMirror = (Mask)(digitsInMirrorCell & baseCellsDigitsMask);
				var elimDigitsFromTheOtherTargetCell = (Mask)(grid.GetCandidates(theOtherTargetCell) & ~containedDigitsAppearedInBaseCellsInMirror);

				// Try to fetch eliminations.
				if (elimDigitsFromTheOnlyMirrorCell != 0)
				{
					foreach (var elimDigit in elimDigitsFromTheOnlyMirrorCell)
					{
						conclusions.Add(new(Elimination, theOnlyMirrorCell, elimDigit));
					}
				}
				if (elimDigitsFromTheOtherTargetCell != 0)
				{
					foreach (var elimDigit in elimDigitsFromTheOtherTargetCell)
					{
						conclusions.Add(new(Elimination, theOtherTargetCell, elimDigit));
					}
				}

				singleMirrors.Add(theOnlyMirrorCell);
			}
			if (conclusions.Count == 0 || !singleMirrors)
			{
				// No eliminations found.
				return null;
			}

			var step = new ExocetSingleMirrorStep(
				[.. conclusions],
				[
					[
						.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
						.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
						.. from cell in crossline select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
						.. from cell in singleMirrors select new CellViewNode(WellKnownColorIdentifier.Auxiliary3, cell),
						..
						from cell in baseCells
						from d in grid.GetCandidates(cell)
						select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + d),
						..
						from cell in crossline
						where grid.GetState(cell) == CellState.Empty
						from d in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
						select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary2, cell * 9 + d),
						//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
					]
				],
				context.PredefinedOptions,
				baseCellsDigitsMask,
				in baseCells,
				in targetCells,
				[],
				in crossline,
				in singleMirrors
			);
			if (context.OnlyFindOne)
			{
				return step;
			}

			context.Accumulator.Add(step);
		}

		return null;
	}

	private static JuniorExocetIncompatiblePairStep? CheckIncompatiblePair(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		int delta,
		out Mask inferredTargetPairMask,
		HouseMask housesMask
	)
	{
		inferredTargetPairMask = 0;

		// This rule can only apply for the case on such conditions:
		//   1) The number of base cells must be 2.
		//   2) The delta value must be 0 (i.e. a standard JE).
		if (delta != 0)
		{
			return null;
		}

		if (baseCells is not [var base1, var base2])
		{
			return null;
		}

		// Now try to fetch the defining cells. First, try to get uncovered 4 blocks that the final cells should be located in.
		var cellsDoNotCover = CellMap.Empty;
		foreach (var (_, chuteCells, _, _) in Chutes)
		{
			if (chuteCells & baseCells)
			{
				cellsDoNotCover |= chuteCells;
			}
		}
		var lastFourBlocks = ~CellMap.Empty - cellsDoNotCover;
		var lastFourBlocksNotIntersectedWithCrossline = lastFourBlocks - crossline;
		var lastFourBlocksIntersectedWithCrossline = lastFourBlocks & crossline;

		// Try to check for cross-line cells, get all values and its containing cells.
		var valueCellsInLastFourBlocksIntersectedWithCrossline = lastFourBlocksIntersectedWithCrossline - EmptyCells;

		// Determine whether the value cells only span with 2 rows and columns, forming a square shape:
		//
		//   1 . . | 2 . .
		//   . . . | . . .
		//   . . . | . . .
		//   ------+------
		//   2 . . | 1 . .
		//   . . . | . . .
		//   . . . | . . .
		//
		// If not, the rule cannot be formed.
		if (valueCellsInLastFourBlocksIntersectedWithCrossline.Count != 4
			|| PopCount((uint)valueCellsInLastFourBlocksIntersectedWithCrossline.RowMask) != 2
			|| PopCount((uint)valueCellsInLastFourBlocksIntersectedWithCrossline.ColumnMask) != 2)
		{
			return null;
		}

		// Try to fetch the last 16 cells to be checked.
		var lastSixteenCells = lastFourBlocksNotIntersectedWithCrossline;
		foreach (var house in valueCellsInLastFourBlocksIntersectedWithCrossline.RowMask << 9)
		{
			lastSixteenCells -= HousesMap[house];
		}
		foreach (var house in valueCellsInLastFourBlocksIntersectedWithCrossline.ColumnMask << 18)
		{
			lastSixteenCells -= HousesMap[house];
		}

		// Now we have the 16 cells to be checked. Such cells are called "X Region".
		// We should check for value cells, determining which combinations of digits have spanned all 4 blocks the current 16 cells located in.
		scoped var valuesGroupedByBlock = (stackalloc Mask[2]);
		scoped var valueCellsBlocks = lastSixteenCells.BlockMask.GetAllSets();
		foreach (var blockIndex in (0, 1))
		{
			var valueCellsFromBlock1 = (lastSixteenCells & HousesMap[valueCellsBlocks[blockIndex]]) - EmptyCells;
			var valueCellsFromBlock2 = (lastSixteenCells & HousesMap[valueCellsBlocks[3 - blockIndex]]) - EmptyCells;
			var valuesFromBlock1 = MaskOperations.Create(from cell in valueCellsFromBlock1 select grid.GetDigit(cell));
			var valuesFromBlock2 = MaskOperations.Create(from cell in valueCellsFromBlock2 select grid.GetDigit(cell));
			var valuesFromBothBlocks = (Mask)(valuesFromBlock1 & valuesFromBlock2);
			if (valuesFromBothBlocks == 0)
			{
				// It seems no digits will be intersected with two blocks in diagonal direction...
				// The current "X Region" may not contain any possible conclusions. Just return.
				return null;
			}

			// Write down such digits.
			valuesGroupedByBlock[blockIndex] = valuesFromBothBlocks;
		}

		// Then we should check for combinations, determining which combinations are not correct.
		// The rule is, if two different digits from two groups of blocks in a diagonal direction are found,
		// they will not be the final pair of digits appeared in base cells.
		// For example, if the following diagram exists:
		//
		//   1 . . | 2 . .
		//   . . . | . . .
		//   . . . | . . .
		//   ------+------
		//   2 . . | 1 . .
		//   . . . | . . .
		//   . . . | . . .
		//
		// The pair of digits 1 and 2 cannot be the final pair in base cells,
		// meaning the base cells cannot be filled with 1 and 2 at the same time. They are incompatible.
		// Here is the prove (Chinese version):
		//
		//   https://tieba.baidu.com/p/5916787916
		//
		// Which tells us that, if so, the last pattern of exocet may form a deadly pattern.
		scoped var incompatibleCombinationsGroupedByDigit = (stackalloc Mask[9]);
		incompatibleCombinationsGroupedByDigit.Clear();
		foreach (var value1 in valuesGroupedByBlock[0])
		{
			foreach (var value2 in valuesGroupedByBlock[1])
			{
				incompatibleCombinationsGroupedByDigit[value1] |= (Mask)(1 << value2);
				incompatibleCombinationsGroupedByDigit[value2] |= (Mask)(1 << value1);
			}
		}

		// Now check for eliminations.
		var incompatibleCandidates = CandidateMap.Empty;
		var conclusions = new List<Conclusion>();
		var targetCellsDigitsMask = grid[in targetCells];
		foreach (var (elimCell, theOtherCell) in ((base1, base2), (base2, base1)))
		{
			var allDigits = grid.GetCandidates(theOtherCell);
			foreach (var elimDigit in grid.GetCandidates(elimCell))
			{
				if (incompatibleCombinationsGroupedByDigit[elimDigit] == (Mask)(allDigits & ~(1 << elimDigit)))
				{
					conclusions.Add(new(Elimination, elimCell, elimDigit));
					incompatibleCandidates.Add(elimCell * 9 + elimDigit);
				}
			}
		}
		if (conclusions.Count == 0)
		{
			return null;
		}

		// Temporarily remove the eliminated digits, and check whether the last digits in target cells form a pair.
		// Because we know a JE pattern will lead to a conclusion:
		//
		//   "The target cells should be a pair of different digits if a JE formed."
		//
		// we can conclude that the target cells will form a distributed disjointed pair (from concept "Distributed Disjointed Subset", DDS).
		// Then we can remove all digits from the cells that both target cells can see.
		var baseCellsLastDigitsMask = baseCellsDigitsMask;
		foreach (var digitCanBeRemoved in targetCellsDigitsMask)
		{
			if (baseCells == [.. from conclusion in conclusions where conclusion.Digit == digitCanBeRemoved select conclusion.Cell])
			{
				baseCellsLastDigitsMask &= (Mask)~(1 << digitCanBeRemoved);
			}
		}
		if (PopCount((uint)baseCellsLastDigitsMask) == 2)
		{
			// The JE has formed a distribution disjointed pair.
			inferredTargetPairMask = baseCellsLastDigitsMask;
		}

		var step = new JuniorExocetIncompatiblePairStep(
			[.. conclusions],
			[
				[
					.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
					.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
					.. from cell in crossline select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
					.. from cell in lastSixteenCells - EmptyCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary3, cell),
					..
					from cell in baseCells
					from d in grid.GetCandidates(cell)
					select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + d),
					..
					from cell in crossline
					where grid.GetState(cell) == CellState.Empty
					from d in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
					select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary2, cell * 9 + d),
					//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
				]
			],
			context.PredefinedOptions,
			baseCellsDigitsMask,
			in incompatibleCandidates,
			in baseCells,
			in targetCells,
			in crossline
		);
		if (context.OnlyFindOne)
		{
			return step;
		}

		context.Accumulator.Add(step);
		return null;
	}

	private static JuniorExocetTargetPairStep? CheckTargetPair(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		Mask inferredTargetPairMask,
		int delta,
		HouseMask housesMask,
		scoped ReadOnlySpan<Conjugate> inferredTargetConjugatePairs
	)
	{
		if (inferredTargetPairMask == 0)
		{
			return null;
		}

		if (delta != 0)
		{
			return null;
		}

		if (baseCells.Count != 2)
		{
			return null;
		}

		var conclusions = new List<Conclusion>();
		foreach (var cell in baseCells)
		{
			foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~inferredTargetPairMask))
			{
				conclusions.Add(new(Elimination, cell, digit));
			}
		}
		foreach (var (_, values) in from cell in targetCells group cell by cell.ToHouseIndex(HouseType.Block))
		{
			switch (values.Count)
			{
				case 1:
				{
					foreach (var cell in values)
					{
						foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~inferredTargetPairMask))
						{
							conclusions.Add(new(Elimination, cell, digit));
						}
					}
					break;
				}
				case 2 when inferredTargetConjugatePairs.First(conj => conj.Map == values) is { Digit: var conjDigit }:
				{
					foreach (var cell in values)
					{
						foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~inferredTargetPairMask & ~(1 << conjDigit)))
						{
							conclusions.Add(new(Elimination, cell, digit));
						}
					}
					break;
				}
			}
		}
		if (targetCells.Count == 2)
		{
			foreach (var cell in targetCells.PeerIntersection)
			{
				foreach (var digit in (Mask)(grid.GetCandidates(cell) & inferredTargetPairMask))
				{
					conclusions.Add(new(Elimination, cell, digit));
				}
			}
		}
		foreach (var cell in baseCells.PeerIntersection)
		{
			foreach (var digit in (Mask)(grid.GetCandidates(cell) & inferredTargetPairMask))
			{
				conclusions.Add(new(Elimination, cell, digit));
			}
		}
		if (conclusions.Count == 0)
		{
			return null;
		}

		var step = new JuniorExocetTargetPairStep(
			[.. conclusions],
			[
				[
					.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
					.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
					.. from cell in crossline select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
					..
					from cell in baseCells
					from d in grid.GetCandidates(cell)
					select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + d),
					..
					from cell in crossline
					where grid.GetState(cell) == CellState.Empty
					from d in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
					select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary2, cell * 9 + d),
					//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
				]
			],
			context.PredefinedOptions,
			baseCellsDigitsMask,
			inferredTargetPairMask,
			in baseCells,
			in targetCells,
			in crossline
		);
		if (context.OnlyFindOne)
		{
			return step;
		}

		context.Accumulator.Add(step);
		return null;
	}

	private static JuniorExocetGeneralizedFishStepConclusion? CheckGeneralizedFish(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		Mask inferredTargetPairMask,
		int delta,
		bool isRow,
		HouseMask housesMask
	)
	{
		if (inferredTargetPairMask == 0)
		{
			return null;
		}

		if (delta != 0)
		{
			return null;
		}

		if (baseCells.Count != 2)
		{
			return null;
		}

		var inferredTargetPairMaskDigit1 = TrailingZeroCount(inferredTargetPairMask);
		var inferredTargetPairMaskDigit2 = inferredTargetPairMask.GetNextSet(inferredTargetPairMaskDigit1 + 1);

		var conclusions = new List<Conclusion>();
		foreach (var line in isRow ? crossline.ColumnMask << 18 : crossline.RowMask << 9)
		{
			var crosslineCellsIntersectedWithLine = HousesMap[line] & crossline & (CandidatesMap[inferredTargetPairMaskDigit1] | CandidatesMap[inferredTargetPairMaskDigit2]);
			if (!crosslineCellsIntersectedWithLine)
			{
				// The current line does not contain any eliminations because all intersected cells in cross-line are not empty.
				continue;
			}

			var elimCells = (HousesMap[line] & EmptyCells) - crossline;
			foreach (var cell in elimCells)
			{
				foreach (var digit in (Mask)(grid.GetCandidates(cell) & inferredTargetPairMask))
				{
					conclusions.Add(new(Elimination, cell, digit));
				}
			}
		}
		if (conclusions.Count == 0)
		{
			return null;
		}

		var step = new JuniorExocetGeneralizedFishStepConclusion(
			[.. conclusions],
			[
				[
					.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
					.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
					.. from cell in crossline select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
					..
					from cell in baseCells
					from d in grid.GetCandidates(cell)
					select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + d),
					..
					from cell in targetCells
					from d in grid.GetCandidates(cell)
					where (inferredTargetPairMask >> d & 1) != 0
					select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary3, cell * 9 + d),
					..
					from cell in crossline
					where grid.GetState(cell) == CellState.Empty
					from d in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
					let isSwordfishDigit = (inferredTargetPairMask >> d & 1) != 0
					let colorIdentifier = isSwordfishDigit ? WellKnownColorIdentifier.Auxiliary3 : WellKnownColorIdentifier.Auxiliary2
					select new CandidateViewNode(colorIdentifier, cell * 9 + d),
					//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
				]
			],
			context.PredefinedOptions,
			baseCellsDigitsMask,
			inferredTargetPairMask,
			in baseCells,
			in targetCells,
			in crossline
		);
		if (context.OnlyFindOne)
		{
			return step;
		}

		context.Accumulator.Add(step);
		return null;
	}

	private static JuniorExocetMirrorAlmostHiddenSetStep? CheckMirrorAlmostHiddenSet(
		scoped ref AnalysisContext context,
		Grid grid,
		scoped ref readonly CellMap baseCells,
		scoped ref readonly CellMap targetCells,
		scoped ref readonly CellMap crossline,
		Mask baseCellsDigitsMask,
		int delta,
		bool isRow,
		HouseMask housesMask,
		int chuteIndex
	)
	{
		if (delta != 0)
		{
			return null;
		}

		foreach (var cellGroup in from cell in targetCells group cell by cell.ToHouseIndex(HouseType.Block))
		{
			if (cellGroup is not (_, [var targetCell]))
			{
				continue;
			}

			// Try to fetch the miniline of the current target cell located in.
			Unsafe.SkipInit(out CellMap miniline);
			foreach (ref readonly var temp in MinilinesGroupedByChuteIndex[chuteIndex].EnumerateRef())
			{
				if (temp.Contains(targetCell))
				{
					miniline = temp;
					break;
				}
			}

			var mirrorTwoCells = miniline - targetCell;
			var mirrorEmptyCells = mirrorTwoCells & EmptyCells;
			if (!mirrorEmptyCells)
			{
				// The current miniline cannot contain any eliminations.
				continue;
			}

			// Now check for empty cells in this house, removing all cells located in the miniline that the target cell located in.
			foreach (var coveredHouse in mirrorEmptyCells.CoveredHouses)
			{
				var otherCells = (HousesMap[coveredHouse] & EmptyCells) - miniline;
				if (otherCells.Count < 2)
				{
					// The target house does not contain enough cells to form an AHS.
					continue;
				}

				for (var size = 2; size <= otherCells.Count - 1; size++)
				{
					foreach (var extraCells in otherCells.GetSubsets(size - 1))
					{
						var ahsCells = extraCells | mirrorEmptyCells;
						foreach (var digitsMaskGroup in ((Mask)(grid[in ahsCells] & ~baseCellsDigitsMask)).GetAllSets().GetSubsets(size))
						{
							var extraDigitsMask = MaskOperations.Create(digitsMaskGroup);
							var lastHoldingMap = CellMap.Empty;
							foreach (var digit in digitsMaskGroup)
							{
								lastHoldingMap |= CandidatesMap[digit];
							}

							lastHoldingMap &= HousesMap[coveredHouse];
							if (lastHoldingMap - targetCell != ahsCells)
							{
								// Final map does not match.
								continue;
							}

							var conclusions = new List<Conclusion>();
							foreach (var cell in extraCells)
							{
								foreach (var digit in (Mask)(grid.GetCandidates(cell) & ~extraDigitsMask))
								{
									conclusions.Add(new(Elimination, cell, digit));
								}
							}
							if (conclusions.Count == 0)
							{
								// No valid conclusions exist.
								continue;
							}

							var step = new JuniorExocetMirrorAlmostHiddenSetStep(
								[.. conclusions],
								[
									[
										.. from cell in baseCells select new CellViewNode(WellKnownColorIdentifier.Normal, cell),
										.. from cell in targetCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary1, cell),
										.. from cell in crossline select new CellViewNode(WellKnownColorIdentifier.Auxiliary2, cell),
										.. from cell in extraCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary3, cell),
										.. from cell in mirrorEmptyCells select new CellViewNode(WellKnownColorIdentifier.Auxiliary3, cell),
										..
										from cell in baseCells
										from d in grid.GetCandidates(cell)
										select new CandidateViewNode(WellKnownColorIdentifier.Normal, cell * 9 + d),
										..
										from cell in mirrorEmptyCells
										from d in (Mask)(grid.GetCandidates(cell) & extraDigitsMask)
										select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary3, cell * 9 + d),
										..
										from cell in extraCells
										from d in (Mask)(grid.GetCandidates(cell) & extraDigitsMask)
										select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary3, cell * 9 + d),
										..
										from cell in crossline
										where grid.GetState(cell) == CellState.Empty
										from d in (Mask)(grid.GetCandidates(cell) & baseCellsDigitsMask)
										select new CandidateViewNode(WellKnownColorIdentifier.Auxiliary2, cell * 9 + d),
										//.. from house in housesMask select new HouseViewNode(WellKnownColorIdentifier.Auxiliary2, house)
									]
								],
								context.PredefinedOptions,
								baseCellsDigitsMask,
								in baseCells,
								in targetCells,
								in crossline,
								in extraCells,
								extraDigitsMask
							);
							if (context.OnlyFindOne)
							{
								return step;
							}

							context.Accumulator.Add(step);
						}
					}
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Try to get the maximum times that the specified digit, describing it can be filled with the specified houses in maximal case.
	/// </summary>
	/// <param name="digit">The digit to be checked.</param>
	/// <param name="houses">The houses that the digit can be filled with.</param>
	/// <param name="excludedCells">
	/// Indicates the cells the method doesn't cover them. If the value is not <see cref="CellMap.Empty"/>,
	/// all cells in the houses should be checked.
	/// </param>
	/// <returns>
	/// <para>The number of times that the digit can be filled with the specified houses, at most.</para>
	/// <para>
	/// If any one of the houses from argument <paramref name="houses"/> doesn't contain that digit,
	/// or the digit has already been filled with that house as a value, the value will be 0. No exception will be thrown.
	/// </para>
	/// </returns>
	private static int MostTimesOf(Digit digit, HouseMask houses, scoped ref readonly CellMap excludedCells)
	{
		var cells = CandidatesMap[digit];
		var cellsInHouses = CellMap.Empty;
		foreach (var house in houses)
		{
			cellsInHouses |= HousesMap[house] - excludedCells;
		}
		cells &= cellsInHouses;

		for (var size = Math.Min(9, PopCount((uint)houses)); size >= 1; size--)
		{
			foreach (var cellsChosen in cells.GetSubsets(size))
			{
				if (size >= 2)
				{
					var duplicated = false;
					foreach (var cellPair in cellsChosen.GetSubsets(2))
					{
						if (cellPair.InOneHouse(out _))
						{
							duplicated = true;
							break;
						}
					}
					if (duplicated)
					{
						continue;
					}
				}

				return size;
			}
		}

		return 0;
	}
}