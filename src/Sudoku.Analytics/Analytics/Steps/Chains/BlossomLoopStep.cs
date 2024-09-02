namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Blossom Loop</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="pattern">Indicates the backing pattern.</param>
public sealed partial class BlossomLoopStep(
	Conclusion[] conclusions,
	View[]? views,
	StepGathererOptions options,
	[PrimaryConstructorParameter] BlossomLoop pattern
) : ChainStep(conclusions, views, options)
{
	/// <inheritdoc/>
	public override bool IsMultiple => true;

	/// <inheritdoc/>
	public override bool IsDynamic => false;

	/// <summary>
	/// Indicates whether the pattern uses grouped nodes.
	/// </summary>
	public bool IsGrouped => Pattern.Exists(static chain => chain.IsGrouped);

	/// <inheritdoc/>
	public override int Complexity => Pattern.Complexity;

	/// <inheritdoc/>
	public override int BaseDifficulty => 65;

	/// <inheritdoc/>
	public override Technique Code => Technique.BlossomLoop;

	/// <inheritdoc/>
	public override Mask DigitsUsed => Pattern.DigitsMask;

	/// <inheritdoc/>
	public override InterpolationArray Interpolations
		=> [new(SR.EnglishLanguage, [BurredLoopStr]), new(SR.ChineseLanguage, [BurredLoopStr])];

	/// <inheritdoc/>
	public override FactorArray Factors
		=> [new BlossomLoopGroupedFactor(), new BlossomLoopGroupedNodeFactor(), new BlossomLoopLengthFactor()];

	private string BurredLoopStr => Pattern.ToString("m", CoordinateConverter.GetInstance(Options.Converter));


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Step? other)
		=> other is BlossomLoopStep comparer && Pattern.Equals(comparer.Pattern);

	/// <inheritdoc/>
	public override int CompareTo(Step? other)
		=> other is BlossomLoopStep comparer ? Pattern.CompareTo(comparer.Pattern) : -1;
}
