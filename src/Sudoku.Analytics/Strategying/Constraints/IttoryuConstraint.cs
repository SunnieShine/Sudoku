namespace Sudoku.Strategying.Constraints;

/// <summary>
/// Represents a constraint that checks whether a puzzle can be finished by ittoryu rules.
/// </summary>
[GetHashCode]
[ToString]
public sealed partial class IttoryuConstraint : Constraint, IComparisonOperatorConstraint, ILimitCountConstraint<int>
{
	/// <summary>
	/// Indicates the step searcher used.
	/// </summary>
	private static readonly Analyzer LocalAnalyzer = Analyzers.Default
		.WithStepSearchers(
			new SingleStepSearcher
			{
				EnableFullHouse = true,
				EnableLastDigit = true,
				HiddenSinglesInBlockFirst = true,
				UseIttoryuMode = true
			}
		)
		.WithUserDefinedOptions(new() { DistinctDirectMode = true, IsDirectMode = true });


	/// <summary>
	/// Indicates the rounds used.
	/// </summary>
	[HashCodeMember]
	[StringMember]
	public int Rounds { get; set; }

	/// <summary>
	/// Indicates the single technique that can be used in the checking.
	/// </summary>
	[HashCodeMember]
	[StringMember]
	public SingleTechniqueFlag LimitedSingle { get; set; }

	/// <inheritdoc/>
	[HashCodeMember]
	[StringMember]
	public ComparisonOperator Operator { get; set; }

	/// <inheritdoc/>
	int ILimitCountConstraint<int>.LimitCount { get => Rounds; set => Rounds = value; }


	/// <inheritdoc/>
	public static int Minimum => 1;

	/// <inheritdoc/>
	public static int Maximum => 2;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Constraint? other)
		=> other is IttoryuConstraint comparer
		&& (Rounds, Operator, LimitedSingle) == (comparer.Rounds, comparer.Operator, comparer.LimitedSingle);

	/// <inheritdoc/>
	public override string ToString(CultureInfo? culture = null)
		=> string.Format(ResourceDictionary.Get("IttoryuConstraint", culture), Operator.GetOperatorString(), Rounds);

	/// <inheritdoc/>
	public override IttoryuConstraint Clone()
		=> new() { IsNegated = IsNegated, Operator = Operator, LimitedSingle = LimitedSingle, Rounds = Rounds };

	/// <inheritdoc/>
	protected override bool CheckCore(ConstraintCheckingContext context)
	{
		if (context.AnalyzerResult is not { IsSolved: true, DifficultyLevel: DifficultyLevel.Easy })
		{
			// Bug fix: We won't check for steps if the grid is hard than 'DifficultyLevel.Easy'.
			// For example if a moderate puzzle is found, the expression '(SingleStep)steps[i]' will throw an InvalidCastException
			// because the step 'steps[i]' may not be a 'SingleStep'.
			return false;
		}

		if (LocalAnalyzer.Analyze(context.Grid) is not
			{
				IsSolved: true,
				DifficultyLevel: DifficultyLevel.Easy,
				InterimSteps: { Length: var stepsCount } steps,
				InterimGrids: var stepGrids
			})
		{
			return false;
		}

		var maximum = new SortedSet<SingleTechniqueFlag>(from step in steps select step.Code.GetSingleTechnique()).Max;
		if (maximum > LimitedSingle)
		{
			// The puzzle will use advanced techniques.
			return false;
		}

		var roundsCount = 1;
		for (var i = 0; i < stepsCount - 1; i++)
		{
			var previousDigit = ((SingleStep)steps[i]).Digit;
			var currentDigit = ((SingleStep)steps[i + 1]).Digit;
			if ((previousDigit, currentDigit) is (8, 0))
			{
				roundsCount++;
				continue;
			}

			if (currentDigit - previousDigit is 0 or 1)
			{
				continue;
			}

			// Check whether the current digit is already completed.
			// If the digit is already completed, we should consider this case as "consecutive" also.
			scoped ref readonly var currentGrid = ref stepGrids[i + 1];
			if (currentGrid.ValuesMap[previousDigit].Count == 9)
			{
				continue;
			}

			roundsCount = -1;
			break;
		}
		if (roundsCount == -1)
		{
			return false;
		}

		return Operator.GetOperator<int>()(roundsCount, Rounds);
	}
}
