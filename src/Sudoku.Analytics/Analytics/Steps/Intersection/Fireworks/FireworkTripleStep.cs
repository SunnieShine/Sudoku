namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Firework Triple</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="cells">Indicates the cells used.</param>
/// <param name="digitsMask">Indicates the mask of digits used.</param>
public sealed partial class FireworkTripleStep(
	Conclusion[] conclusions,
	View[]? views,
	[PrimaryConstructorParameter] scoped in CellMap cells,
	[PrimaryConstructorParameter] Mask digitsMask
) : FireworkStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => base.BaseDifficulty + .1M;

	/// <inheritdoc/>
	public override Technique Code => Technique.FireworkTriple;

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, string[]?> FormatInterpolatedParts
		=> new Dictionary<string, string[]?> { { "en", new[] { CellsStr, DigitsStr } }, { "zh", new[] { CellsStr, DigitsStr } } };

	private string CellsStr => Cells.ToString();

	private string DigitsStr => DigitMaskFormatter.Format(DigitsMask, FormattingMode.Normal);
}
