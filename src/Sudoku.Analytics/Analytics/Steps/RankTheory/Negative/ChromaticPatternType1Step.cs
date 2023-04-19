namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Chromatic Pattern Type 1</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="blocks"><inheritdoc/></param>
/// <param name="pattern"><inheritdoc/></param>
/// <param name="extraCell">Indicates the extra cell used.</param>
/// <param name="digitsMask"><inheritdoc/></param>
public sealed partial class ChromaticPatternType1Step(
	Conclusion[] conclusions,
	View[]? views,
	int[] blocks,
	scoped in CellMap pattern,
	[PrimaryConstructorParameter] int extraCell,
	Mask digitsMask
) : ChromaticPatternStep(conclusions, views, blocks, pattern, digitsMask)
{
	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, string[]?> FormatInterpolatedParts
		=> new Dictionary<string, string[]?>
		{
			{ "en", new[] { CellsStr, BlocksStr, DigitsStr } },
			{ "zh", new[] { BlocksStr, CellsStr, DigitsStr } }
		};

	/// <inheritdoc/>
	public override Technique Code => Technique.ChromaticPatternType1;
}
