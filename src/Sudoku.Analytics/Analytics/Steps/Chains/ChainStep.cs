namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Chain</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
public abstract class ChainStep(Conclusion[] conclusions, View[]? views, StepSearcherOptions options) : IndirectStep(conclusions, views, options)
{
	/// <summary>
	/// Indicates whether the chain pattern consists of multiple sub-chains.
	/// </summary>
	public abstract bool IsMultiple { get; }

	/// <summary>
	/// Indicates whether the chain pattern is dynamic, which means it should be checked dynamically inside searching algorithm.
	/// </summary>
	public abstract bool IsDynamic { get; }
}