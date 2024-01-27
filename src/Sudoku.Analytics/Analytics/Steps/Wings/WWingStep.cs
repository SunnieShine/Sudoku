namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>(Grouped) W-Wing</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="startCell">Indicates the start cell.</param>
/// <param name="endCell">Indicates the end cell.</param>
/// <param name="bridge">Indicates the bridge cells connecting with cells <see cref="StartCell"/> and <see cref="EndCell"/>.</param>
public sealed partial class WWingStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	[RecordParameter] Cell startCell,
	[RecordParameter] Cell endCell,
	[RecordParameter] scoped ref readonly CellMap bridge
) : IrregularWingStep(conclusions, views, options)
{
	/// <summary>
	/// Indicates whether the pattern is grouped.
	/// </summary>
	public bool IsGrouped => Bridge.Count >= 3;

	/// <inheritdoc/>
	public override decimal BaseDifficulty => 4.4M;

	/// <inheritdoc/>
	public override Technique Code => IsGrouped ? Technique.GroupedWWing : Technique.WWing;

	/// <inheritdoc/>
	public override ExtraDifficultyFactor[] ExtraDifficultyFactors => [new(ExtraDifficultyFactorNames.IsGrouped, IsGrouped ? .1M : 0)];

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [new(EnglishLanguage, [StartCellStr, EndCellStr, BridgeStr]), new(ChineseLanguage, [StartCellStr, EndCellStr, BridgeStr])];

	private string StartCellStr => Options.Converter.CellConverter([StartCell]);

	private string EndCellStr => Options.Converter.CellConverter([EndCell]);

	private string BridgeStr => Options.Converter.CellConverter(Bridge);
}