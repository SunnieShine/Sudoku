namespace Sudoku.Measuring.Factors;

/// <summary>
/// Represents a factor that describes the size of subset appeared in <see cref="UniqueRectangleExternalType3Step"/>.
/// </summary>
/// <seealso cref="UniqueRectangleExternalType3Step"/>
public sealed partial class UniqueRectangleExternalSubsetSizeFactor : Factor
{
	/// <inheritdoc/>
	public override string[] ParameterNames => [nameof(IPatternType3StepTrait<UniqueRectangleExternalType3Step>.SubsetSize)];

	/// <inheritdoc/>
	public override Type ReflectedStepType => typeof(UniqueRectangleExternalType3Step);

	/// <inheritdoc/>
	public override ParameterizedFormula Formula => static args => (int)args![0]!;
}