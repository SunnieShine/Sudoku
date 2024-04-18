namespace Sudoku.Measuring.Factors;

/// <summary>
/// Represents a factor that describes whether the rectangle is an avoidable rectangle.
/// </summary>
public sealed class RectangleIsAvoidableFactor : Factor
{
	/// <inheritdoc/>
	public override string FormulaString => "{0} ? 1 : 0";

	/// <inheritdoc/>
	public override string[] ParameterNames => [nameof(UniqueRectangleStep.IsAvoidable)];

	/// <inheritdoc/>
	public override Type ReflectedStepType => typeof(UniqueRectangleStep);

	/// <inheritdoc/>
	public override Func<Step, int?> Formula
		=> static step => step switch
		{
			UniqueRectangleStep { IsAvoidable: var isAvoidable } => isAvoidable ? 1 : 0,
			_ => null
		};
}