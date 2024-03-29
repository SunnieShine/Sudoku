namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Brute Force</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
public sealed class BruteForceStep(Conclusion[] conclusions, View[]? views, StepSearcherOptions options) :
	LastResortStep(conclusions, views, options)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => AnalyzerResult.MaximumRatingValueTheory;

	/// <inheritdoc/>
	public override Technique Code => Technique.BruteForce;

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [new(EnglishLanguage, [AssignmentStr]), new(ChineseLanguage, [AssignmentStr])];

	private string AssignmentStr => Options.Converter.ConclusionConverter(Conclusions);
}
