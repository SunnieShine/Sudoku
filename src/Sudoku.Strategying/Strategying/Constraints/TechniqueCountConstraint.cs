namespace Sudoku.Strategying.Constraints;

/// <summary>
/// Represents a rule that checks whether the specified analyzer result after analyzed by a grid
/// contains the specified techniques.
/// </summary>
[GetHashCode]
[ToString]
public sealed partial class TechniqueCountConstraint : Constraint, IComparisonOperatorConstraint
{
	/// <inheritdoc/>
	public override bool AllowDuplicate => true;

	/// <summary>
	/// Indicates the appearing times.
	/// </summary>
	[HashCodeMember]
	[StringMember]
	public int LimitCount { get; set; }

	/// <inheritdoc/>
	[HashCodeMember]
	[StringMember]
	public ComparisonOperator Operator { get; set; }

	/// <summary>
	/// Indicates the technique used.
	/// </summary>
	[HashCodeMember]
	[StringMember]
	public Technique Technique { get; set; }


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Constraint? other)
		=> other is TechniqueCountConstraint comparer
		&& (LimitCount, Operator, Technique) == (comparer.LimitCount, comparer.Operator, comparer.Technique);

	/// <inheritdoc/>
	public override bool Check(scoped ConstraintCheckingContext context)
	{
		if (!context.RequiresAnalyzer)
		{
			return false;
		}

		var times = 0;
		foreach (var step in context.AnalyzerResult)
		{
			if (Technique == step.Code)
			{
				times++;
			}
		}

		return Operator.GetOperator<int>()(times, LimitCount);
	}

	/// <inheritdoc/>
	public override string ToString(CultureInfo? culture = null)
		=> string.Format(
			ResourceDictionary.Get("TechniqueCountConstraint", culture),
			Technique.GetName(culture),
			Operator.GetOperatorString(),
			LimitCount
		);
}