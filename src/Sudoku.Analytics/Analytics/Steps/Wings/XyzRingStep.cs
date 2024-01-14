namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>(Siamese) (Grouped) XYZ-Ring</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="intersectedDigit">Indicates the digit Z for XYZ-Wing pattern.</param>
/// <param name="pivot">Indicates the pivot cell.</param>
/// <param name="leafCell1">Indicates the leaf cell 1.</param>
/// <param name="leafCell2">Indicates the leaf cell 2.</param>
/// <param name="conjugateHousesMask">Indicates the conjugate houses used.</param>
/// <param name="isNice">Indicates whether the pattern is a nice loop.</param>
/// <param name="isGrouped">Indicates whether the conjugate pair is grouped one.</param>
/// <param name="isSiamese">Indicates whether the XYZ-loop is a Siamese one.</param>
public sealed partial class XyzRingStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	[RecordParameter] Digit intersectedDigit,
	[RecordParameter] Cell pivot,
	[RecordParameter] Cell leafCell1,
	[RecordParameter] Cell leafCell2,
	[RecordParameter] HouseMask conjugateHousesMask,
	[RecordParameter] bool isNice,
	[RecordParameter] bool isGrouped,
	[RecordParameter] bool isSiamese = false
) : WingStep(conclusions, views, options)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => IsNice ? 5.0M : 5.2M;

	/// <inheritdoc/>
	public override Technique Code
		=> (IsSiamese, IsGrouped, IsNice) switch
		{
			(true, true, true) => Technique.SiameseGroupedXyzNiceLoop,
			(_, true, true) => Technique.GroupedXyzNiceLoop,
			(true, true, _) => Technique.SiameseGroupedXyzLoop,
			(_, true, _) => Technique.GroupedXyzLoop,
			(true, _, true) => Technique.SiameseXyzNiceLoop,
			(_, _, true) => Technique.XyzNiceLoop,
			(true, _, _) => Technique.SiameseXyzLoop,
			_ => Technique.XyzLoop
		};
}
