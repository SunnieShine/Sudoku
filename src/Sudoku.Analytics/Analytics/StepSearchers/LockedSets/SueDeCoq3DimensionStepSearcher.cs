namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with a <b>3-dimensional Sue de Coq</b> step searcher.
/// The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>3-dimensional Sue de Coq</item>
/// </list>
/// </summary>
[StepSearcher(Technique.SueDeCoq3Dimension)]
[StepSearcherRuntimeName("StepSearcherName_SueDeCoq3DimensionStepSearcher")]
public sealed partial class SueDeCoq3DimensionStepSearcher : StepSearcher
{
	/// <inheritdoc/>
	protected internal override Step? Collect(scoped ref AnalysisContext context)
	{
		scoped ref readonly var grid = ref context.Grid;
		var rbList = new List<CellMap>(3);
		var cbList = new List<CellMap>(3);
		foreach (var pivot in EmptyCells)
		{
			var r = pivot.ToHouseIndex(HouseType.Row);
			var c = pivot.ToHouseIndex(HouseType.Column);
			var b = pivot.ToHouseIndex(HouseType.Block);
			var rbMap = HousesMap[r] & HousesMap[b];
			var cbMap = HousesMap[c] & HousesMap[b];
			var rbEmptyMap = rbMap & EmptyCells;
			var cbEmptyMap = cbMap & EmptyCells;
			if (rbEmptyMap.Count < 2 || cbEmptyMap.Count < 2)
			{
				// The intersection needs at least two cells.
				continue;
			}

			reinitializeList(rbList, in rbEmptyMap);
			reinitializeList(cbList, in cbEmptyMap);

			foreach (ref readonly var rbCurrentMap in rbList.AsReadOnlySpan())
			{
				var rbSelectedInterMask = grid[in rbCurrentMap];
				if (PopCount((uint)rbSelectedInterMask) <= rbCurrentMap.Count + 1)
				{
					continue;
				}

				foreach (ref readonly var cbCurrentMap in cbList.AsReadOnlySpan())
				{
					var cbSelectedInterMask = grid[in cbCurrentMap];
					if (PopCount((uint)cbSelectedInterMask) <= cbCurrentMap.Count + 1)
					{
						continue;
					}

					if ((cbCurrentMap & rbCurrentMap).Count != 1)
					{
						continue;
					}

					// Get all maps to use later.
					var blockMap = HousesMap[b] - rbCurrentMap - cbCurrentMap & EmptyCells;
					var rowMap = HousesMap[r] - HousesMap[b] & EmptyCells;
					var columnMap = HousesMap[c] - HousesMap[b] & EmptyCells;

					// Iterate on the number of the cells that should be selected in block.
					for (var i = 1; i < blockMap.Count; i++)
					{
						foreach (ref readonly var selectedBlockCells in blockMap.GetSubsets(i))
						{
							var blockMask = grid[in selectedBlockCells];
							var elimMapBlock = (CellMap)[];

							// Get the elimination map in the block.
							foreach (var digit in blockMask)
							{
								elimMapBlock |= CandidatesMap[digit];
							}
							elimMapBlock &= blockMap - selectedBlockCells;

							for (var j = 1; j < MathExtensions.Min(9 - i - selectedBlockCells.Count, rowMap.Count, columnMap.Count); j++)
							{
								foreach (ref readonly var selectedRowCells in rowMap.GetSubsets(j))
								{
									var rowMask = grid[in selectedRowCells];
									var elimMapRow = (CellMap)[];

									foreach (var digit in rowMask)
									{
										elimMapRow |= CandidatesMap[digit];
									}
									elimMapRow &= HousesMap[r] - rbCurrentMap - selectedRowCells;

									for (var k = 1; k <= MathExtensions.Min(9 - i - j - selectedBlockCells.Count - selectedRowCells.Count, rowMap.Count, columnMap.Count); k++)
									{
										foreach (ref readonly var selectedColumnCells in columnMap.GetSubsets(k))
										{
											var columnMask = grid[in selectedColumnCells];
											var elimMapColumn = (CellMap)[];

											foreach (var digit in columnMask)
											{
												elimMapColumn |= CandidatesMap[digit];
											}
											elimMapColumn &= HousesMap[c] - cbCurrentMap - selectedColumnCells;

											if ((blockMask & rowMask) != 0 && (rowMask & columnMask) != 0 && (columnMask & blockMask) != 0)
											{
												continue;
											}

											var fullMap = rbCurrentMap | cbCurrentMap | selectedRowCells | selectedColumnCells | selectedBlockCells;
											var otherMap_row = fullMap - (selectedRowCells | rbCurrentMap);
											var otherMap_column = fullMap - (selectedColumnCells | cbCurrentMap);
											var mask = grid[in otherMap_row];
											if ((mask & rowMask) != 0)
											{
												// At least one digit spanned two houses.
												continue;
											}

											mask = grid[in otherMap_column];
											if ((mask & columnMask) != 0)
											{
												continue;
											}

											mask = (Mask)((Mask)(blockMask | rowMask) | columnMask);
											var rbMaskOnlyInInter = (Mask)(rbSelectedInterMask & ~mask);
											var cbMaskOnlyInInter = (Mask)(cbSelectedInterMask & ~mask);

											var bCount = PopCount((uint)blockMask);
											var rCount = PopCount((uint)rowMask);
											var cCount = PopCount((uint)columnMask);
											var rbCount = PopCount((uint)rbMaskOnlyInInter);
											var cbCount = PopCount((uint)cbMaskOnlyInInter);
											if (cbCurrentMap.Count + rbCurrentMap.Count + i + j + k - 1 == bCount + rCount + cCount + rbCount + cbCount
												&& !!(elimMapRow | elimMapColumn | elimMapBlock))
											{
												// Check eliminations.
												var conclusions = new List<Conclusion>();
												foreach (var digit in blockMask)
												{
													foreach (var cell in elimMapBlock & CandidatesMap[digit])
													{
														conclusions.Add(new(Elimination, cell, digit));
													}
												}
												foreach (var digit in rowMask)
												{
													foreach (var cell in elimMapRow & CandidatesMap[digit])
													{
														conclusions.Add(new(Elimination, cell, digit));
													}
												}
												foreach (var digit in columnMask)
												{
													foreach (var cell in elimMapColumn & CandidatesMap[digit])
													{
														conclusions.Add(new(Elimination, cell, digit));
													}
												}
												if (conclusions.Count == 0)
												{
													continue;
												}

												var candidateOffsets = new List<CandidateViewNode>();
												foreach (var digit in rowMask)
												{
													foreach (var cell in (selectedRowCells | rbCurrentMap) & CandidatesMap[digit])
													{
														candidateOffsets.Add(new(ColorIdentifier.Normal, cell * 9 + digit));
													}
												}
												foreach (var digit in columnMask)
												{
													foreach (var cell in (selectedColumnCells | cbCurrentMap) & CandidatesMap[digit])
													{
														candidateOffsets.Add(new(ColorIdentifier.Auxiliary1, cell * 9 + digit));
													}
												}
												foreach (var digit in blockMask)
												{
													foreach (var cell in (selectedBlockCells | rbCurrentMap | cbCurrentMap) & CandidatesMap[digit])
													{
														candidateOffsets.Add(new(ColorIdentifier.Auxiliary2, cell * 9 + digit));
													}
												}

												var step = new SueDeCoq3DimensionStep(
													[.. conclusions],
													[
														[
															.. candidateOffsets,
															new HouseViewNode(ColorIdentifier.Normal, r),
															new HouseViewNode(ColorIdentifier.Auxiliary2, c),
															new HouseViewNode(ColorIdentifier.Auxiliary3, b)
														]
													],
													context.PredefinedOptions,
													rowMask,
													columnMask,
													blockMask,
													selectedRowCells | rbCurrentMap,
													selectedColumnCells | cbCurrentMap,
													selectedBlockCells | rbCurrentMap | cbCurrentMap
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
						}
					}
				}
			}
		}

		return null;


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void reinitializeList(List<CellMap> list, scoped ref readonly CellMap emptyMap)
		{
			list.Clear();
			switch (emptyMap)
			{
				case { Count: 2 }:
				{
					list.AddRef(in emptyMap);
					break;
				}
				case [var i, var j, var k]:
				{
					list.AddRef([i, j]);
					list.AddRef([i, k]);
					list.AddRef([j, k]);
					break;
				}
			}
		}
	}
}
