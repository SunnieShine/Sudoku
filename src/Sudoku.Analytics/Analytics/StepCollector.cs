using System.Diagnostics.CodeAnalysis;
using System.SourceGeneration;
using Sudoku.Analytics.Metadata;

namespace Sudoku.Analytics;

/// <summary>
/// Represents an instance that can collect all possible <see cref="Step"/>s in a grid for one state.
/// </summary>
public sealed partial class StepCollector : AnalyzerOrCollector
{
	/// <summary>
	/// Indicates the error information that describes that the mode is undefined.
	/// </summary>
	private const string ErrorInfo_ModeIsUndefined = $"The property value '{nameof(DifficultyLevelMode)}' is undefined.";


	/// <summary>
	/// Indicates the maximum steps can be gathered.
	/// </summary>
	/// <remarks>
	/// The default value is 1000.
	/// </remarks>
	public int MaxStepsGathered { get; internal set; } = 1000;

	/// <summary>
	/// Indicates whether the solver only displays the techniques with the same displaying level.
	/// </summary>
	/// <remarks>
	/// The default value is <see cref="StepCollectorDifficultyLevelMode.OnlySame"/>.
	/// </remarks>
	public StepCollectorDifficultyLevelMode DifficultyLevelMode { get; internal set; } = StepCollectorDifficultyLevelMode.OnlySame;

	/// <inheritdoc/>
	[DisallowNull]
	[ImplicitField(RequiredReadOnlyModifier = false)]
	public override StepSearcher[]? StepSearchers
	{
		get => _stepSearchers;

		protected internal set => ResultStepSearchers = FilterStepSearchers(_stepSearchers = value, StepSearcherRunningArea.Gathering);
	}

	/// <inheritdoc/>
	public override StepSearcher[] ResultStepSearchers { get; protected internal set; } =
		from searcher in StepSearcherPool.Default()
		where searcher.RunningArea.Flags(StepSearcherRunningArea.Gathering)
		select searcher;


	/// <summary>
	/// Search for all possible steps in a grid.
	/// </summary>
	/// <param name="puzzle">The puzzle grid.</param>
	/// <param name="progress">The progress instance that is used for reporting the state.</param>
	/// <param name="cancellationToken">The cancellation token used for canceling an operation.</param>
	/// <returns>
	/// The result. If cancelled, the return value will be <see langword="null"/>; otherwise, a real list even though it may be empty.
	/// </returns>
	/// <exception cref="InvalidOperationException">Throws when property <see cref="DifficultyLevelMode"/> is not defined.</exception>
	public IEnumerable<Step>? Collect(scoped in Grid puzzle, IProgress<AnalyzerProgress>? progress = null, CancellationToken cancellationToken = default)
	{
		if (!Enum.IsDefined(DifficultyLevelMode))
		{
			throw new InvalidOperationException(ErrorInfo_ModeIsUndefined);
		}

		if (puzzle.IsSolved || !puzzle.ExactlyValidate(out _, out var isSukaku) || isSukaku is not { } sukaku)
		{
			return [];
		}

		try
		{
			return searchInternal(sukaku, progress, puzzle, cancellationToken);
		}
		catch (Exception ex)
		{
			if (ex is OperationCanceledException { CancellationToken: var c } && c == cancellationToken)
			{
				return null;
			}

			throw;
		}


		List<Step> searchInternal(bool sukaku, IProgress<AnalyzerProgress>? progress, scoped in Grid puzzle, CancellationToken cancellationToken)
		{
			const int defaultLevel = int.MaxValue;

			var possibleStepSearchers = ResultStepSearchers;
			var totalSearchersCount = possibleStepSearchers.Length;

			Initialize(puzzle, puzzle.SolutionGrid);

			var (lastLevel, bag, currentSearcherIndex) = (defaultLevel, new List<Step>(), 0);
			foreach (var searcher in possibleStepSearchers)
			{
				switch (searcher)
				{
					case { RunningArea: var runningArea } when !runningArea.Flags(StepSearcherRunningArea.Gathering):
					case { IsNotSupportedForSukaku: true } when sukaku:
					{
						goto ReportProgress;
					}
					case { Level: var currentLevel }:
					{
						// If a searcher contains the upper level, it will be skipped.
						switch (DifficultyLevelMode)
						{
							case StepCollectorDifficultyLevelMode.OnlySame
								when lastLevel != defaultLevel && currentLevel <= lastLevel || lastLevel == defaultLevel:
							case StepCollectorDifficultyLevelMode.OneLevelHarder
								when lastLevel != defaultLevel && currentLevel <= lastLevel + 1 || lastLevel == defaultLevel:
							case StepCollectorDifficultyLevelMode.All:
							{
								break;
							}
							default:
							{
								goto ReportProgress;
							}
						}

						cancellationToken.ThrowIfCancellationRequested();

						// Searching.
						var accumulator = new List<Step>();
						scoped var context = new AnalysisContext(accumulator, puzzle, false);
						searcher.Collect(ref context);

						if (accumulator.Count is not (var count and not 0))
						{
							goto ReportProgress;
						}

						lastLevel = currentLevel;
						bag.AddRange(count > MaxStepsGathered ? accumulator[..MaxStepsGathered] : accumulator);
						break;
					}
				}

			// Report the progress if worth.
			ReportProgress:
				progress?.Report(new(searcher.ToString(), ++currentSearcherIndex / (double)totalSearchersCount));
			}

			// Return the result.
			return bag;
		}
	}
}
