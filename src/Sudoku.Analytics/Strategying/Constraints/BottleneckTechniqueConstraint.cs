namespace Sudoku.Strategying.Constraints;

/// <summary>
/// Represents a constraint that determines whether bottleneck step represents the specified technique.
/// </summary>
[ConstraintOptions(AllowsMultiple = true, AllowsNegation = true)]
[TypeImpl(TypeImplFlag.Object_GetHashCode | TypeImplFlag.Object_ToString, ToStringBehavior = ToStringBehavior.Specified)]
public sealed partial class BottleneckTechniqueConstraint : Constraint
{
	/// <summary>
	/// Indicates the techniques selected.
	/// </summary>
	[HashCodeMember]
	public TechniqueSet Techniques { get; set; } = [];

	[StringMember]
	private string TechniquesString => Techniques.ToString();


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Constraint? other)
		=> other is BottleneckTechniqueConstraint comparer && Techniques == comparer.Techniques;

	/// <inheritdoc/>
	public override string ToString(IFormatProvider? formatProvider)
	{
		var culture = formatProvider as CultureInfo;
		return string.Format(SR.Get("BottleneckTechniqueConstraint", culture), Techniques.ToString(culture));
	}

	/// <inheritdoc/>
	public override BottleneckTechniqueConstraint Clone() => new() { IsNegated = IsNegated, Techniques = Techniques[..] };

	/// <inheritdoc/>
	protected override bool CheckCore(ConstraintCheckingContext context)
		=> !!(Techniques & [.. from step in context.AnalyzerResult.BottleneckSteps select step.Code]);
}
