namespace Sudoku.Analytics.Metadata;

/// <summary>
/// Represents an attribute type that will be applied to fields defined in <see cref="Technique"/>,
/// describing its detail which can be defined as fixed one, to be stored as metadata.
/// </summary>
/// <shared-comments>
/// <para>
/// <b>This property can only be applied to a <see cref="TechniqueGroup"/> field.</b>
/// </para>
/// </shared-comments>
[AttributeUsage(AttributeTargets.Field, Inherited = false)]
public sealed class TechniqueMetadataAttribute : ProgramMetadataAttribute<double, DifficultyLevel>
{
	/// <summary>
	/// Indicates the customized tag to be used. The value may be used in reflection to define your customized data
	/// that will be considered as <see langword="static"/> one.
	/// </summary>
	public object?[]? ExtraArguments { get; init; }

	/// <summary>
	/// Indicates whether the current technique supports for Siamese logic.
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="TechniqueMetadataAttribute" path="//shared-comments/para[1]"/>
	/// </remarks>
	public bool SupportsSiamese { get; init; }

	/// <summary>
	/// Indicates whether the current technique supports for Dual logic.
	/// </summary>
	/// <remarks><inheritdoc cref="TechniqueMetadataAttribute" path="//shared-comments/para[1]"/></remarks>
	public bool SupportsDual { get; init; }

	/// <summary>
	/// Indicates the rating value defined in direct mode.
	/// </summary>
	public double DirectRating { get; init; }

	/// <summary>
	/// Indicates the resource key that can fetch the corresponding resource string.
	/// </summary>
	[DisallowNull]
	public string? ResourceKey { get; init; }

	/// <summary>
	/// Indicates the abbreviation of the technique.
	/// </summary>
	[DisallowNull]
	public string? Abbreviation { get; init; }

	/// <summary>
	/// Indicates the reference links.
	/// </summary>
	[StringSyntax(StringSyntax.Uri)]
	[DisallowNull]
	public string[]? Links { get; init; }

	/// <summary>
	/// Indicates the extra difficulty factors.
	/// </summary>
	[DisallowNull]
	public string[]? ExtraFactors { get; init; }

	/// <summary>
	/// Indicates the containing techniuqe group that the current technique belongs to.
	/// </summary>
	public TechniqueGroup ContainingGroup { get; init; }

	/// <summary>
	/// Indicates the mode that the current technique can be used by solving a puzzle.
	/// By default the value is both <see cref="PencilmarkVisibility.Direct"/> and <see cref="PencilmarkVisibility.Indirect"/>.
	/// </summary>
	/// <seealso cref="PencilmarkVisibility.Direct"/>
	/// <seealso cref="PencilmarkVisibility.Indirect"/>
	public PencilmarkVisibility PencilmarkVisibility { get; init; } = PencilmarkVisibility.Direct | PencilmarkVisibility.Indirect;

	/// <summary>
	/// Indicates the customized related technique that the current technique is applied.
	/// </summary>
	public Technique RelatedTechnique { get; init; }

	/// <summary>
	/// Indicates the features of the technique.
	/// </summary>
	public TechniqueFeatures Features { get; init; }

	/// <summary>
	/// Indicates the special flags that the current technique will be applied in metadata.
	/// </summary>
	public TechniqueMetadataSpecialFlags SpecialFlags { get; init; }

	/// <summary>
	/// Indicates a that that can create a <see cref="Step"/> instance as the primary choice.
	/// </summary>
	[DisallowNull]
	public Type? PrimaryStepType { get; init; }

	/// <summary>
	/// Indicates a that that can create a <see cref="Step"/> instance as the secondary choice.
	/// </summary>
	[DisallowNull]
	public Type? SecondaryStepType { get; init; }

	/// <summary>
	/// Indicates a step searcher type that can produce steps that describes the current technique.
	/// </summary>
	[DisallowNull]
	public Type? StepSearcherType { get; init; }

	/// <summary>
	/// Indicates a type that can produce puzzles that always uses the current technique.
	/// </summary>
	[DisallowNull]
	public Type? GeneratorType { get; init; }
}
