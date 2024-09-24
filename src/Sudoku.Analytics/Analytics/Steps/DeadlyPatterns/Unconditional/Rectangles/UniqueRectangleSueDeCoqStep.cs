namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Unique Rectangle Sue de Coq</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="digit1"><inheritdoc/></param>
/// <param name="digit2"><inheritdoc/></param>
/// <param name="cells"><inheritdoc/></param>
/// <param name="isAvoidable"><inheritdoc/></param>
/// <param name="block">Indicates the block index that the Sue de Coq pattern used.</param>
/// <param name="line">Indicates the line (row or column) index that the Sue de Coq pattern used.</param>
/// <param name="blockMask">Indicates the mask that contains all digits from the block of the Sue de Coq pattern.</param>
/// <param name="lineMask">Indicates the cells in the line of the Sue de Coq pattern.</param>
/// <param name="intersectionMask">
/// Indicates the mask that contains all digits from the intersection of houses <see cref="Block"/> and <see cref="Line"/>.
/// </param>
/// <param name="isCannibalistic">Indicates whether the Sue de Coq pattern is a cannibalism.</param>
/// <param name="isolatedDigitsMask">Indicates the mask that contains all isolated digits.</param>
/// <param name="blockCells">Indicates the cells in the block of the Sue de Coq pattern.</param>
/// <param name="lineCells">Indicates the cells in the line (row or column) of the Sue de Coq pattern.</param>
/// <param name="intersectionCells">Indicates the cells in the intersection from houses <see cref="Block"/> and <see cref="Line"/>.</param>
/// <param name="absoluteOffset"><inheritdoc/></param>
public sealed partial class UniqueRectangleSueDeCoqStep(
	Conclusion[] conclusions,
	View[]? views,
	StepGathererOptions options,
	Digit digit1,
	Digit digit2,
	ref readonly CellMap cells,
	bool isAvoidable,
	[PrimaryConstructorParameter] House block,
	[PrimaryConstructorParameter] House line,
	[PrimaryConstructorParameter] Mask blockMask,
	[PrimaryConstructorParameter] Mask lineMask,
	[PrimaryConstructorParameter] Mask intersectionMask,
	[PrimaryConstructorParameter] bool isCannibalistic,
	[PrimaryConstructorParameter] Mask isolatedDigitsMask,
	[PrimaryConstructorParameter] ref readonly CellMap blockCells,
	[PrimaryConstructorParameter] ref readonly CellMap lineCells,
	[PrimaryConstructorParameter] ref readonly CellMap intersectionCells,
	int absoluteOffset
) :
	UniqueRectangleStep(
		conclusions,
		views,
		options,
		isAvoidable ? Technique.AvoidableRectangleSueDeCoq : Technique.UniqueRectangleSueDeCoq,
		digit1,
		digit2,
		in cells,
		isAvoidable,
		absoluteOffset
	),
	IIsolatedDigitTrait
{
	/// <inheritdoc/>
	public override int BaseDifficulty => base.BaseDifficulty + 5;

	/// <inheritdoc/>
	public override Mask DigitsUsed
		=> (Mask)(base.DigitsUsed | (Mask)((Mask)((Mask)(BlockMask | LineMask) | IntersectionMask) | IsolatedDigitsMask));

	/// <inheritdoc/>
	public override InterpolationArray Interpolations
		=> [
			new(SR.EnglishLanguage, [D1Str, D2Str, CellsStr, MergedCellsStr, SueDeCoqDigitsMask]),
			new(SR.ChineseLanguage, [D1Str, D2Str, CellsStr, MergedCellsStr, SueDeCoqDigitsMask])
		];

	/// <inheritdoc/>
	public override FactorArray Factors
		=> [
			Factor.Create(
				"Factor_RectangleSueDeCoqIsolatedFactor",
				[nameof(IsCannibalistic), nameof(IIsolatedDigitTrait.ContainsIsolatedDigits)],
				GetType(),
				static args => !(bool)args![0]! && (bool)args![1]! ? 1 : 0
			),
			Factor.Create(
				"Factor_RectangleSueDeCoqCannibalismFactor",
				[nameof(IsCannibalistic)],
				GetType(),
				static args => (bool)args![0]! ? 1 : 0
			),
			Factor.Create(
				"Factor_RectangleIsAvoidableFactor",
				[nameof(IsAvoidable)],
				GetType(),
				static args => (bool)args![0]! ? 1 : 0
			)
		];

	/// <inheritdoc/>
	bool IIsolatedDigitTrait.ContainsIsolatedDigits => IsolatedDigitsMask != 0;

	/// <inheritdoc/>
	int IIsolatedDigitTrait.IsolatedDigitsCount => IsolatedDigitsMask == 0 ? 0 : Mask.PopCount(IsolatedDigitsMask);

	private string MergedCellsStr => Options.Converter.CellConverter(LineCells | BlockCells);

	private string SueDeCoqDigitsMask => Options.Converter.DigitConverter((Mask)(LineMask | BlockMask));
}