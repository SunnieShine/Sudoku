namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Double Exocet (Fish)</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="digitsMask"><inheritdoc/></param>
/// <param name="baseCells"><inheritdoc/></param>
/// <param name="targetCells"><inheritdoc/></param>
/// <param name="crosslineCells"><inheritdoc/></param>
/// <param name="baseCellsTheOther">Indicates the other side of the base cells.</param>
/// <param name="targetCellsTheOther">Indicates the other side of the target cells.</param>
public sealed partial class DoubleExocetGeneralizedFishStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	Mask digitsMask,
	ref readonly CellMap baseCells,
	ref readonly CellMap targetCells,
	ref readonly CellMap crosslineCells,
	[PrimaryConstructorParameter] ref readonly CellMap baseCellsTheOther,
	[PrimaryConstructorParameter] ref readonly CellMap targetCellsTheOther
) : ExocetStep(conclusions, views, options, digitsMask, in baseCells, in targetCells, [], in crosslineCells), IDoubleExocet
{
	/// <inheritdoc/>
	public override int BaseDifficulty => base.BaseDifficulty + 2;

	/// <inheritdoc/>
	public override Technique Code => Technique.DoubleExocetGeneralizedFish;
}
