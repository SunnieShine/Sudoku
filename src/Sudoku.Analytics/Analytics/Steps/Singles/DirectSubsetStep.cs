namespace Sudoku.Analytics.Steps;

/// <summary>
/// Represents a data structure that describes for a technique of <b>Direct Subset</b>.
/// </summary>
/// <param name="conclusions"><inheritdoc cref="Step.Conclusions" path="/summary"/></param>
/// <param name="views"><inheritdoc cref="Step.Views" path="/summary"/></param>
/// <param name="options"><inheritdoc cref="Step.Options" path="/summary"/></param>
/// <param name="cell"><inheritdoc cref="SingleStep.Cell" path="/summary"/></param>
/// <param name="digit"><inheritdoc cref="SingleStep.Digit" path="/summary"/></param>
/// <param name="subsetCells">Indicates the subset cells used.</param>
/// <param name="subsetDigitsMask">Indicates the digits that the subset used.</param>
/// <param name="subsetHouse">Indicates the subset house.</param>
/// <param name="interim">Indicates the interim cells used.</param>
/// <param name="interimDigitsMask">Indicates the digits produced in interim.</param>
/// <param name="subtype"><inheritdoc cref="SingleStep.Subtype" path="/summary"/></param>
/// <param name="basedOn"><inheritdoc cref="ComplexSingleBaseStep.BasedOn" path="/summary"/></param>
/// <param name="subsetTechnique">Indicates the subset technique used.</param>
public sealed partial class DirectSubsetStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	Cell cell,
	Digit digit,
	[PrimaryConstructorParameter] ref readonly CellMap subsetCells,
	[PrimaryConstructorParameter] Mask subsetDigitsMask,
	[PrimaryConstructorParameter] House subsetHouse,
	[PrimaryConstructorParameter] ref readonly CellMap interim,
	[PrimaryConstructorParameter] Mask interimDigitsMask,
	SingleSubtype subtype,
	Technique basedOn,
	[PrimaryConstructorParameter] Technique subsetTechnique
) :
	ComplexSingleBaseStep(conclusions, views, options, cell, digit, subtype, basedOn, [[subsetTechnique]]),
	ISizeTrait,
	ICellListTrait
{
	/// <summary>
	/// Indicates whether the used subset is a naked subset.
	/// </summary>
	public bool IsNaked
		=> SubsetTechnique is Technique.NakedPair or Technique.NakedPairPlus or Technique.LockedPair
		or Technique.NakedTriple or Technique.NakedTriplePlus or Technique.LockedTriple
		or Technique.NakedQuadruple or Technique.NakedQuadruplePlus;

	/// <summary>
	/// <inheritdoc
	///     cref="NakedSubsetStep(Conclusion[], View[], StepSearcherOptions, int, ref readonly CellMap, short, bool?)"
	///     path="/param[@name='isLocked']"/>
	/// </summary>
	public bool? IsLocked
		=> SubsetTechnique switch
		{
			Technique.NakedPair or Technique.NakedTriple or Technique.NakedQuadruple => null,
			Technique.NakedPairPlus or Technique.NakedTriplePlus or Technique.NakedQuadruplePlus => false,
			Technique.LockedPair or Technique.LockedTriple => true,
			_ => null
		};

	/// <inheritdoc/>
	public int Size => SubsetCells.Count;

	/// <inheritdoc/>
	public override int BaseDifficulty => IsNaked ? 33 : 37;

	/// <inheritdoc/>
	public override Technique Code => BasedOn.ComplexSingleUsing(SubsetTechnique);

	/// <inheritdoc/>
	public override Interpolation[] Interpolations
		=> [
			new(EnglishLanguage, [CellsStr, HouseStr, InterimCellStr, InterimDigitStr, TechniqueNameStr, DigitsStr, SubsetNameStr]),
			new(ChineseLanguage, [CellsStr, HouseStr, InterimCellStr, InterimDigitStr, TechniqueNameStr, DigitsStr, SubsetNameStr])
		];

	/// <inheritdoc/>
	public override FactorCollection Factors => [new DirectSubsetSizeFactor(), new DirectSubsetIsLockedFactor()];

	/// <inheritdoc/>
	int ICellListTrait.CellSize => SubsetCells.Count;

	private string CellsStr => Options.Converter.CellConverter(SubsetCells);

	private string HouseStr => Options.Converter.HouseConverter(1 << SubsetHouse);

	private string InterimCellStr => Options.Converter.CellConverter(Interim);

	private string InterimDigitStr => Options.Converter.DigitConverter(InterimDigitsMask);

	private string TechniqueNameStr => BasedOn.GetName(GetCulture(null));

	private string DigitsStr => Options.Converter.DigitConverter(SubsetDigitsMask);

	private string SubsetNameStr => SubsetTechnique.GetName(GetCulture(null));


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Step? other)
		=> other is DirectSubsetStep comparer
		&& SubsetCells == comparer.SubsetCells && SubsetDigitsMask == comparer.SubsetDigitsMask
		&& Interim == comparer.Interim && InterimDigitsMask == comparer.InterimDigitsMask
		&& Subtype == comparer.Subtype && SubsetTechnique == comparer.SubsetTechnique;
}
