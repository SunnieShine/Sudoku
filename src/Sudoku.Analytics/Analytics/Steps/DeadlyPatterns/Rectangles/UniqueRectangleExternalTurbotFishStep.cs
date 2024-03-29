namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Unique Rectangle External Turbot Fish</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="digit1"><inheritdoc/></param>
/// <param name="digit2"><inheritdoc/></param>
/// <param name="cells"><inheritdoc/></param>
/// <param name="guardianCells">Indicates the cells that the guardians lie in.</param>
/// <param name="isIncomplete">Indicates whether the pattern is incomplete.</param>
/// <param name="absoluteOffset"><inheritdoc/></param>
public sealed partial class UniqueRectangleExternalTurbotFishStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	Digit digit1,
	Digit digit2,
	scoped ref readonly CellMap cells,
	[PrimaryConstructorParameter] scoped ref readonly CellMap guardianCells,
	[PrimaryConstructorParameter] bool isIncomplete,
	int absoluteOffset
) : UniqueRectangleStep(
	conclusions,
	views,
	options,
	Technique.UniqueRectangleExternalTurbotFish,
	digit1,
	digit2,
	in cells,
	false,
	absoluteOffset
)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => base.BaseDifficulty + .1M;

	/// <inheritdoc/>
	public override ExtraDifficultyFactor[] ExtraDifficultyFactors
		=> [
			new(ExtraDifficultyFactorNames.Guardian, A004526(GuardianCells.Count) * .1M),
			new(ExtraDifficultyFactorNames.Incompleteness, IsIncomplete ? .1M : 0)
		];

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [
			new(EnglishLanguage, [D1Str, D2Str, CellsStr, GuardianCellsStr]),
			new(ChineseLanguage, [D1Str, D2Str, CellsStr, GuardianCellsStr])
		];

	private string GuardianCellsStr => Options.Converter.CellConverter(GuardianCells);
}
