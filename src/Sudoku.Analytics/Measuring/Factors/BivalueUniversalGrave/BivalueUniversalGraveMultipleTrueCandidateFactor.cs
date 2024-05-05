namespace Sudoku.Measuring.Factors;

/// <summary>
/// Represents a factor that describes the true candidates appeared in <see cref="BivalueUniversalGraveMultipleStep"/>.
/// </summary>
/// <seealso cref="BivalueUniversalGraveMultipleStep"/>
public sealed class BivalueUniversalGraveMultipleTrueCandidateFactor : Factor
{
	/// <inheritdoc/>
	public override string[] ParameterNames => [nameof(BivalueUniversalGraveMultipleStep.TrueCandidates)];

	/// <inheritdoc/>
	public override Type ReflectedStepType => typeof(BivalueUniversalGraveMultipleStep);

	/// <inheritdoc/>
	public override ParameterizedFormula Formula => static args => A002024(((CellMap)args![0]!).Count);
}
