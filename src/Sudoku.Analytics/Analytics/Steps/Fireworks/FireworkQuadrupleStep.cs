namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Firework Quadruple</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="cells">Indicates the cells used.</param>
/// <param name="digitsMask">Indicates the mask of digits used.</param>
public sealed partial class FireworkQuadrupleStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	[PrimaryConstructorParameter] ref readonly CellMap cells,
	[PrimaryConstructorParameter] Mask digitsMask
) : FireworkStep(conclusions, views, options)
{
	/// <inheritdoc/>
	public override int BaseDifficulty => base.BaseDifficulty + 4;

	/// <inheritdoc/>
	public override int Size => 4;

	/// <inheritdoc/>
	public override Technique Code => Technique.FireworkQuadruple;

	/// <inheritdoc/>
	public override Interpolation[] Interpolations
		=> [new(EnglishLanguage, [CellsStr, DigitsStr]), new(ChineseLanguage, [CellsStr, DigitsStr])];

	private string CellsStr => Options.Converter.CellConverter(Cells);

	private string DigitsStr => Options.Converter.DigitConverter(DigitsMask);
}
