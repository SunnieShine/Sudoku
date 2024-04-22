#undef REMOVE_DUPLICATED_STEPS_IN_SINGLES_IF_RANDOM_ENABLED

namespace Sudoku.Analytics;

/// <summary>
/// Provides an analyzer that solves a sudoku puzzle using the human-friendly logics, and creates an <see cref="AnalyzerResult"/> instance
/// indicating the analytics data.
/// </summary>
/// <remarks>
/// Please note that this type has no accessible constructors,
/// you can just use type <see cref="Analyzers"/> to get <see cref="Analyzer"/>s you want to get.
/// In addition, you can also use <see cref="AnalyzerFactory"/> to create some extra configuration.
/// </remarks>
/// <seealso cref="AnalyzerResult"/>
/// <seealso cref="Analyzers"/>
/// <seealso cref="AnalyzerFactory"/>
/// <completionlist cref="Analyzers"/>
public sealed partial class Analyzer : AnalyzerOrCollector, IGlobalizedAnalyzer<Analyzer, AnalyzerResult>, IRandomizedAnalyzer<Analyzer, AnalyzerResult>
{
	/// <summary>
	/// Indicates the default steps capacity.
	/// </summary>
	private const int DefaultStepsCapacity = 54;


	/// <summary>
	/// The random number generator.
	/// </summary>
	private readonly Random _random = new();


	/// <inheritdoc/>
	public bool RandomizedChoosing { get; set; }

	/// <inheritdoc/>
	public bool IsFullApplying { get; set; }

	/// <summary>
	/// Indicates whether the solver will ignore slow step searchers being configured <see cref="StepSearcherRuntimeFlags.TimeComplexity"/>.
	/// </summary>
	/// <remarks>
	/// The default value is <see langword="false"/>.
	/// </remarks>
	/// <seealso cref="StepSearcherRuntimeFlags.TimeComplexity"/>
	public bool IgnoreSlowAlgorithms { get; set; }

	/// <summary>
	/// Indicates whether the solver will ignore slow step searchers being configured <see cref="StepSearcherRuntimeFlags.SpaceComplexity"/>.
	/// </summary>
	/// <remarks>
	/// The default value is <see langword="false"/>.
	/// </remarks>
	/// <seealso cref="StepSearcherRuntimeFlags.SpaceComplexity"/>
	public bool IgnoreHighAllocationAlgorithms { get; set; }

	/// <inheritdoc/>
	public CultureInfo? CurrentCulture { get; set; }

	/// <inheritdoc/>
	[DisallowNull]
	[ImplicitField(RequiredReadOnlyModifier = false)]
	public override StepSearcher[]? StepSearchers
	{
		get => _stepSearchers;

		set => ResultStepSearchers = FilterStepSearchers(_stepSearchers = value, StepSearcherRunningArea.Searching);
	}

	/// <inheritdoc/>
	public override StepSearcher[] ResultStepSearchers { get; protected internal set; } =
		from searcher in StepSearcherPool.BuiltInStepSearchersExpanded
		where searcher.RunningArea.HasFlag(StepSearcherRunningArea.Searching)
		select searcher;

	/// <inheritdoc/>
	public override StepSearcherOptions Options { get; set; } = StepSearcherOptions.Default;

	/// <summary>
	/// Indicates the conditional options to be set.
	/// </summary>
	internal StepSearcherConditionalOptions? ConditionalOptions { get; set; } = StepSearcherConditionalOptions.Default;

	/// <inheritdoc/>
	Random IRandomizedAnalyzer<Analyzer, AnalyzerResult>.RandomNumberGenerator => _random;

	/// <summary>
	/// Indicates the final <see cref="CultureInfo"/> instance to be used.
	/// </summary>
	private CultureInfo ResultCurrentCulture => CurrentCulture ?? CultureInfo.CurrentUICulture;


	/// <inheritdoc/>
	/// <exception cref="InvalidOperationException">Throws when the puzzle has already been solved.</exception>
	public AnalyzerResult Analyze(ref readonly Grid puzzle, IProgress<AnalyzerProgress>? progress = null, CancellationToken cancellationToken = default)
	{
		if (puzzle.IsSolved)
		{
			throw new InvalidOperationException(ResourceDictionary.ExceptionMessage("GridAlreadySolved"));
		}

		var result = new AnalyzerResult(in puzzle) { IsSolved = false };
		var solution = puzzle.SolutionGrid;
		if (puzzle.Uniqueness != Uniqueness.Bad)
		{
			// We should check whether the puzzle is a GSP firstly.
			// This method doesn't check for Sukaku puzzles, or ones containing multiple solutions.
			puzzle.InferSymmetricalPlacement(out var symmetricType, out var mappingDigits, out var selfPairedDigitsMask);

			try
			{
				// Here 'puzzle' may contains multiple solutions, so 'solution' may equal to 'Grid.Undefined'.
				// We will defer the checking inside this method stackframe.
				return analyzeInternal(
					in puzzle,
					in solution,
					result,
					symmetricType,
					mappingDigits,
					selfPairedDigitsMask,
					progress,
					cancellationToken
				);
			}
			catch (Exception ex)
			{
				return ex switch
				{
					RuntimeAnalyticsException e
						=> e switch
						{
							WrongStepException
								=> result with { IsSolved = false, FailedReason = FailedReason.WrongStep, UnhandledException = e },
							PuzzleInvalidException
								=> result with { IsSolved = false, FailedReason = FailedReason.PuzzleIsInvalid }
						},
					OperationCanceledException { CancellationToken: var c } when c == cancellationToken
						=> result with { IsSolved = false, FailedReason = FailedReason.UserCancelled },
					NotImplementedException or NotSupportedException
						=> result with { IsSolved = false, FailedReason = FailedReason.NotImplemented },
					_
						=> result with { IsSolved = false, FailedReason = FailedReason.ExceptionThrown, UnhandledException = ex }
				};
			}
		}
		return result with { IsSolved = false, FailedReason = FailedReason.PuzzleHasNoSolution };


		AnalyzerResult analyzeInternal(
			ref readonly Grid puzzle,
			ref readonly Grid solution,
			AnalyzerResult resultBase,
			SymmetricType symmetricType,
			ReadOnlySpan<Digit?> mappingDigits,
			Mask selfPairedDigitsMask,
			IProgress<AnalyzerProgress>? progress,
			CancellationToken cancellationToken
		)
		{
			var playground = puzzle;
			var totalCandidatesCount = playground.CandidatesCount;
			var (collectedSteps, stepGrids, stepSearchers) = (new List<Step>(DefaultStepsCapacity), new List<Grid>(DefaultStepsCapacity), ResultStepSearchers);
			var timestampOriginal = Stopwatch.GetTimestamp();
			var accumulator = IsFullApplying
				|| RandomizedChoosing
#if SINGLE_TECHNIQUE_LIMIT_FLAG
				|| ConditionalOptions?.LimitedSingle is not (null or 0)
#endif
				? []
				: default(List<Step>);
			var context = new AnalysisContext(in playground, in puzzle)
			{
				Accumulator = accumulator,
				IsSukaku = puzzle.PuzzleType == SudokuType.Sukaku,
				Options = Options,
				OnlyFindOne = !IsFullApplying
					&& !RandomizedChoosing
#if SINGLE_TECHNIQUE_LIMIT_FLAG
					&& ConditionalOptions?.LimitedSingle is null or 0
#endif
			};

			// Determine whether the grid is a GSP pattern. If so, check for eliminations.
			if ((symmetricType, selfPairedDigitsMask) is (not SymmetricType.None, not 0) && !mappingDigits.IsEmpty)
			{
				context.GspPatternInferred = symmetricType;
				context.MappingRelations = mappingDigits;

				if (SymmetricalPlacing.GetStep(in playground, Options) is { } step)
				{
					if (verifyConclusionValidity(in solution, step))
					{
						if (onCollectingSteps(
							collectedSteps, step, in context, ref playground, timestampOriginal,
							stepGrids, resultBase, cancellationToken, out var result))
						{
							return result;
						}
					}
					else
					{
						throw new WrongStepException(in playground, step);
					}
				}
			}

		FindNextStep:
			Initialize(in playground, in solution);
			string progressedStepSearcherName;
			foreach (var searcher in stepSearchers)
			{
				switch (playground, solution, searcher, this)
				{
					case ({ PuzzleType: SudokuType.Sukaku }, _, { Metadata.SupportsSukaku: false }, _):
					case (_, _, { RunningArea: StepSearcherRunningArea.None }, _):
					case (_, _, { Metadata.IsConfiguredSlow: true }, { IgnoreSlowAlgorithms: true }):
					case (_, _, { Metadata.IsConfiguredHighAllocation: true }, { IgnoreHighAllocationAlgorithms: true }):
					case (_, _, { Metadata.IsOnlyRunForDirectViews: true }, { Options: { DistinctDirectMode: true, IsDirectMode: false } }):
					case (_, _, { Metadata.IsOnlyRunForIndirectViews: true }, { Options: { DistinctDirectMode: true, IsDirectMode: true } }):
					case (_, { IsUndefined: true }, { Metadata.SupportAnalyzingMultipleSolutionsPuzzle: false }, _):
					{
						// Skips on those two cases:
						// 1. Sukaku puzzles can't use techniques that is marked as "not supported for sukaku".
						// 2. If the searcher is currently disabled.
						// 3. If the searcher is configured as slow.
						// 4. If the searcher is configured as high-allocation.
						// 5. If the searcher is only run for direct view, and the current is indirect view.
						// 6. If the searcher is only run for indirect view, and the current is direct view.
						// 7. If the searcher doesn't support for analyzing puzzles with multiple solutions, but we enable it.
						continue;
					}
#if SINGLE_TECHNIQUE_LIMIT_FLAG
					case (_, _, SingleStepSearcher, { ConditionalOptions: { AllowsHiddenSingleInLines: var allowLine, LimitedSingle: var limited and not 0 } }):
					{
						accumulator!.Clear();

						searcher.Collect(ref context);
						if (accumulator.Count == 0)
						{
							continue;
						}

						// Special case: consider the step is a full house, hidden single or naked single,
						// igonring steps not belonging to the technique set.
						var chosenSteps = new List<SingleStep>();
						foreach (var step in accumulator)
						{
							if (step is SingleStep { Code: var code } s)
							{
								switch (limited, code, allowLine)
								{
									case (SingleTechniqueFlag.FullHouse, not Technique.FullHouse, _):
									{
										break;
									}
									case (_, Technique.FullHouse, _):
									case (
										SingleTechniqueFlag.HiddenSingle,
										Technique.LastDigit or Technique.CrosshatchingBlock or Technique.HiddenSingleBlock,
										false
									):
									case (
										SingleTechniqueFlag.HiddenSingle,
										Technique.LastDigit
											or >= Technique.HiddenSingleBlock and <= Technique.HiddenSingleColumn
											or >= Technique.CrosshatchingBlock and <= Technique.CrosshatchingColumn,
										true
									):
									case (SingleTechniqueFlag.NakedSingle, Technique.NakedSingle, _):
									{
										chosenSteps.Add(s);
										break;
									}
								}
							}
						}
						if (chosenSteps.Count == 0)
						{
							continue;
						}

						if (IsFullApplying)
						{
							foreach (var step in chosenSteps)
							{
								if (!verifyConclusionValidity(in solution, step))
								{
									throw new WrongStepException(in playground, step);
								}

								if (onCollectingSteps(
									collectedSteps, step, in context, ref playground,
									timestampOriginal, stepGrids, resultBase, cancellationToken, out var result))
								{
									return result;
								}
							}
						}
						else
						{
							var chosenStep = RandomizedChoosing ? chosenSteps[_random.Next(0, chosenSteps.Count)] : chosenSteps[0];
							if (!verifyConclusionValidity(in solution, chosenStep))
							{
								throw new WrongStepException(in playground, chosenStep);
							}

							if (onCollectingSteps(
								collectedSteps, chosenStep, in context, ref playground,
								timestampOriginal, stepGrids, resultBase, cancellationToken, out var result))
							{
								return result;
							}
						}

						goto MakeProgress;
					}
#endif
					case (_, _, BruteForceStepSearcher, { RandomizedChoosing: true }):
					{
						accumulator!.Clear();

						searcher.Collect(ref context);
						if (accumulator.Count == 0)
						{
							continue;
						}

						// Here will fetch a correct step to be applied.
						var chosenStep = accumulator[_random.Next(0, accumulator.Count)];
						if (!verifyConclusionValidity(in solution, chosenStep))
						{
							throw new WrongStepException(in playground, chosenStep);
						}

						if (onCollectingSteps(
							collectedSteps, chosenStep, in context, ref playground,
							timestampOriginal, stepGrids, resultBase, cancellationToken, out var result))
						{
							return result;
						}

						goto MakeProgress;
					}
#if REMOVE_DUPLICATED_STEPS_IN_SINGLES_IF_RANDOM_ENABLED
					case (_, _, SingleStepSearcher, { RandomizedChoosing: true }):
					{
						// Randomly select a step won't take any effects on single steps.
						accumulator!.Clear();

						searcher.Collect(ref context);
						if (accumulator.Count == 0)
						{
							continue;
						}

						var temp = new List<Step>();
						if (accumulator.Count == 1)
						{
							temp.Add(accumulator[0]);
						}
						else
						{
							var distinctCandidatesKey = (CandidateMap)[];
							foreach (SingleStep step in accumulator)
							{
								if (!distinctCandidatesKey.Contains(step.Cell * 9 + step.Digit))
								{
									temp.Add(step);
									distinctCandidatesKey.Add(step.Cell * 9 + step.Digit);
								}
							}
						}

						// Here will fetch a correct step to be applied.
						var chosenStep = temp[_random.Next(0, temp.Count)];
						if (!verifyConclusionValidity(in solution, chosenStep))
						{
							throw new WrongStepException(in playground, chosenStep);
						}

						if (onCollectingSteps(
							collectedSteps, chosenStep, in context, ref playground,
							timestampOriginal, stepGrids, resultBase, cancellationToken, out var result))
						{
							return result;
						}
						break;
					}
#endif
					case (_, _, not BruteForceStepSearcher, { IsFullApplying: true } or { RandomizedChoosing: true }):
					{
						accumulator!.Clear();

						searcher.Collect(ref context);
						if (accumulator.Count == 0)
						{
							continue;
						}

						if (RandomizedChoosing)
						{
							// Here will fetch a correct step to be applied.
							var chosenStep = accumulator[_random.Next(0, accumulator.Count)];
							if (!verifyConclusionValidity(in solution, chosenStep))
							{
								throw new WrongStepException(in playground, chosenStep);
							}

							if (onCollectingSteps(
								collectedSteps, chosenStep, in context, ref playground,
								timestampOriginal, stepGrids, resultBase, cancellationToken, out var result))
							{
								return result;
							}
						}
						else
						{
							foreach (var foundStep in accumulator)
							{
								if (!verifyConclusionValidity(in solution, foundStep))
								{
									throw new WrongStepException(in playground, foundStep);
								}

								if (onCollectingSteps(
									collectedSteps, foundStep, in context, ref playground, timestampOriginal, stepGrids,
									resultBase, cancellationToken, out var result))
								{
									return result;
								}
							}
						}

						// The puzzle has not been finished, we should turn to the first step finder
						// to continue solving puzzle.
						goto MakeProgress;
					}
					default:
					{
						switch (searcher.Collect(ref context))
						{
							case null:
							{
								continue;
							}
							case var foundStep:
							{
								if (verifyConclusionValidity(in solution, foundStep))
								{
									if (onCollectingSteps(
										collectedSteps, foundStep, in context, ref playground, timestampOriginal, stepGrids,
										resultBase, cancellationToken, out var result))
									{
										return result;
									}
								}
								else
								{
									throw new WrongStepException(in playground, foundStep);
								}

								// The puzzle has not been finished, we should turn to the first step finder
								// to continue solving puzzle.
								goto MakeProgress;
							}
						}
					}
				}

			MakeProgress:
				progressedStepSearcherName = searcher.ToString(ResultCurrentCulture);
				goto ReportStateAndTryNextStep;
			}

			// All solver can't finish the puzzle... :(
			return resultBase with
			{
				FailedReason = FailedReason.AnalyzerGiveUp,
				ElapsedTime = Stopwatch.GetElapsedTime(timestampOriginal),
				InterimSteps = [.. collectedSteps],
				InterimGrids = [.. stepGrids]
			};

		ReportStateAndTryNextStep:
			progress?.Report(new(progressedStepSearcherName, (double)(totalCandidatesCount - playground.CandidatesCount) / totalCandidatesCount));
			goto FindNextStep;


			static bool verifyConclusionValidity(ref readonly Grid solution, Step step)
			{
				if (solution.IsUndefined)
				{
					// This will be triggered when the puzzle has multiple solutions.
					return true;
				}

				foreach (var (t, c, d) in step.Conclusions)
				{
					var digit = solution.GetDigit(c);
					if (t == Assignment && digit != d || t == Elimination && digit == d)
					{
						return false;
					}
				}

				return true;
			}

			static bool onCollectingSteps(
				List<Step> steps,
				Step step,
				ref readonly AnalysisContext context,
				ref Grid playground,
				long timestampOriginal,
				List<Grid> steppingGrids,
				AnalyzerResult resultBase,
				CancellationToken cancellationToken,
				[NotNullWhen(true)] out AnalyzerResult? result
			)
			{
				// Optimization: If the grid is inferred as a GSP pattern, we can directly add extra eliminations at symmetric positions.
				if (context is { GspPatternInferred: { } symmetricType } && step is not GurthSymmetricalPlacementStep)
				{
					var mappingRelations = context.MappingRelations;
					var originalConclusions = step.Conclusions;
					var newConclusions = new List<Conclusion>();
					foreach (var conclusion in originalConclusions)
					{
						var newConclusion = conclusion.GetSymmetricConclusion(symmetricType, mappingRelations[conclusion.Digit] ?? -1);
						if (newConclusion != conclusion && playground.Exists(newConclusion.Cell, newConclusion.Digit) is true)
						{
							newConclusions.Add(newConclusion);
						}
					}

					step.Conclusions = [.. originalConclusions, .. newConclusions];
				}

				var atLeastOneConclusionIsWorth = false;
				foreach (var (t, c, d) in step.Conclusions)
				{
					switch (t)
					{
						case Assignment when playground.GetState(c) == CellState.Empty:
						case Elimination when playground.Exists(c, d) is true:
						{
							atLeastOneConclusionIsWorth = true;

							goto FinalCheck;
						}
					}
				}

			FinalCheck:
				if (atLeastOneConclusionIsWorth)
				{
					steppingGrids.Add(playground);
					playground.Apply(step);
					steps.Add(step);

					if (playground.IsSolved)
					{
						result = resultBase with
						{
							IsSolved = true,
							Solution = playground,
							ElapsedTime = Stopwatch.GetElapsedTime(timestampOriginal),
							InterimSteps = [.. steps],
							InterimGrids = [.. steppingGrids]
						};
						return true;
					}
				}
				else
				{
					// No steps are available.
					goto ReturnFalse;
				}

				cancellationToken.ThrowIfCancellationRequested();

			ReturnFalse:
				result = null;
				return false;
			}
		}
	}
}
