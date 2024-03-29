namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Complex Junior Exocet</b> or <b>Complex Senior Exocet</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="digitsMask"><inheritdoc/></param>
/// <param name="baseCells"><inheritdoc/></param>
/// <param name="targetCells"><inheritdoc/></param>
/// <param name="endoTargetCells"><inheritdoc/></param>
/// <param name="crosslineCells"><inheritdoc/></param>
/// <param name="crosslineHousesMask">Indicates the mask holding a list of houses spanned for cross-line cells.</param>
/// <param name="extraHousesMask">Indicates the mask holding a list of extra houses.</param>
public sealed partial class ComplexExocetBaseStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	Mask digitsMask,
	scoped ref readonly CellMap baseCells,
	scoped ref readonly CellMap targetCells,
	scoped ref readonly CellMap endoTargetCells,
	scoped ref readonly CellMap crosslineCells,
	[PrimaryConstructorParameter] HouseMask crosslineHousesMask,
	[PrimaryConstructorParameter] HouseMask extraHousesMask
) :
	ExocetStep(conclusions, views, options, digitsMask, in baseCells, in targetCells, in endoTargetCells, in crosslineCells),
	IComplexSeniorExocet
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty
		=> base.BaseDifficulty + this.GetShapeKind() switch { ExocetShapeKind.Franken => .4M, ExocetShapeKind.Mutant => .6M };

	/// <inheritdoc/>
	public override Technique Code
		=> (EndoTargetCells, this.GetShapeKind()) switch
		{
			([], ExocetShapeKind.Franken) => Technique.FrankenJuniorExocet,
			(_, ExocetShapeKind.Franken) => Technique.FrankenSeniorExocet,
			([], ExocetShapeKind.Mutant) => Technique.MutantJuniorExocet,
			(_, ExocetShapeKind.Mutant) => Technique.MutantSeniorExocet
		};
}
