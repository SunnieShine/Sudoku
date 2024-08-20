namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Death Blossom</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="pivot">Indicates the pivot cell.</param>
/// <param name="branches">Indicates the branches.</param>
/// <param name="zDigitsMask">Indicates the digits mask as eliminations.</param>
public sealed partial class DeathBlossomStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	[PrimaryConstructorParameter] Cell pivot,
	[PrimaryConstructorParameter] NormalBlossomBranchCollection branches,
	[PrimaryConstructorParameter] Mask zDigitsMask
) : DeathBlossomBaseStep(conclusions, views, options), IBranchTrait, IDeathBlossomCollection<NormalBlossomBranchCollection, Digit>
{
	/// <inheritdoc/>
	public override Technique Code => Technique.DeathBlossom;

	/// <inheritdoc/>
	public override Interpolation[] Interpolations
		=> [
			new(EnglishLanguage, [PivotStr, BranchesStr(EnglishLanguage)]),
			new(ChineseLanguage, [PivotStr, BranchesStr(ChineseLanguage)])
		];

	/// <inheritdoc/>
	public override FactorCollection Factors => [new BasicDeathBlossomPetalsCountFactor()];

	/// <inheritdoc/>
	int IBranchTrait.BranchesCount => Branches.Count;

	private string PivotStr => Options.Converter.CellConverter(in Pivot.AsCellMap());


	private string BranchesStr(string cultureName)
	{
		var culture = new CultureInfo(cultureName);
		return string.Join(
			SR.Get("Comma", culture),
			from branch in Branches
			select $"{Options.Converter.DigitConverter((Mask)(1 << branch.Key))} - {branch.Value}"
		);
	}
}
