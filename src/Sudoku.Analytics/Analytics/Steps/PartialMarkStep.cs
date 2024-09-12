namespace Sudoku.Analytics.Steps;

/// <summary>
/// Represents a solving step that can be found without any candidates, except few of candidates marked.
/// Logically, the technique can be treated as a single step with locked candidates or subset patterns.
/// </summary>
/// <param name="conclusions"><inheritdoc cref="Step.Conclusions" path="/summary"/></param>
/// <param name="views"><inheritdoc cref="Step.Views" path="/summary"/></param>
/// <param name="options"><inheritdoc cref="Step.Options" path="/summary"/></param>
public abstract class PartialMarkStep(Conclusion[] conclusions, View[]? views, StepGathererOptions options) :
	Step(conclusions, views, options)
{
	/// <inheritdoc/>
	public sealed override PencilmarkVisibility PencilmarkType => PencilmarkVisibility.PartialMark;
}