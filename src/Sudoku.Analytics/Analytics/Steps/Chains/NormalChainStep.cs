namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>(Grouped) Chain</b> or <b>(Grouped) Loop</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="pattern">Indicates the chain or loop pattern.</param>
public sealed partial class NormalChainStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	[PrimaryConstructorParameter] ChainPattern pattern
) : ChainStep(conclusions, views, options)
{
	/// <summary>
	/// Indicates the sort key that can be used as chaining comparison.
	/// </summary>
	public int SortKey
		=> (IsLoop ? 2000 : 0) + (IsCannibalistic ? 1000 : 0) + (IsGrouped ? 500 : 0) + Pattern.GetSortKey(Conclusions.AsSet());

	/// <inheritdoc/>
	public override bool IsMultiple => false;

	/// <inheritdoc/>
	public override bool IsDynamic => false;

	/// <summary>
	/// Indicates whether the chain is cannibalistic,
	/// meaning at least one of a candidate inside a node used in the pattern is also a conclusion.
	/// </summary>
	public bool IsCannibalistic => Pattern.OverlapsWithConclusions(Conclusions.AsSet());

	/// <summary>
	/// Indicates whether the chain uses at least one grouped node.
	/// </summary>
	public bool IsGrouped => Pattern.IsGrouped;

	/// <summary>
	/// Indicates whether the chain pattern forms a Continuous Nice Loop or Grouped Continuous Nice Loop.
	/// </summary>
	public bool IsLoop => Pattern is Loop;

	/// <inheritdoc/>
	public override int BaseDifficulty => 60;

	/// <summary>
	/// Indicates the chain length.
	/// </summary>
	public int ChainLength => Pattern.Length;

	/// <inheritdoc/>
	public override Technique Code => Pattern.GetTechnique(Conclusions.AsSet());

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [new(EnglishLanguage, [ChainString]), new(ChineseLanguage, [ChainString])];

	/// <inheritdoc/>
	public override FactorCollection Factors => [new ChainLengthFactor(), new ChainGroupedFactor(), new ChainGroupedNodeFactor()];

	private string ChainString => Pattern.ToString("m", Options.Converter ?? CoordinateConverter.GetConverter(ResultCurrentCulture));


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Step? other)
		=> other is NormalChainStep comparer && Pattern.Equals(comparer.Pattern);

	/// <inheritdoc/>
	public override int CompareTo(Step? other)
		=> other is NormalChainStep comparer
			? SortKey.CompareTo(comparer.SortKey) is var result and not 0
				? result
				: Pattern.CompareTo(comparer.Pattern)
			: -1;
}

/// <include file='../../global-doc-comments.xml' path='g/csharp11/feature[@name="file-local"]/target[@name="class" and @when="extension"]'/>
file static class Extensions
{
	/// <summary>
	/// Indicates the sort key dictionary.
	/// </summary>
	private static readonly Dictionary<Technique, int> SortKeyDictionary = new()
	{
		// 3-chain
		{ Technique.Skyscraper, 1 },
		{ Technique.TwoStringKite, 2 },
		{ Technique.TurbotFish, 3 },
		{ Technique.GroupedSkyscraper, 4 },
		{ Technique.GroupedTwoStringKite, 5 },
		{ Technique.GroupedTurbotFish, 6 },

		// 5-chain
		{ Technique.WWing, 101 },
		{ Technique.MWing, 102 },
		{ Technique.SWing, 103 },
		{ Technique.LWing, 104 },
		{ Technique.HWing, 105 },
		{ Technique.PurpleCow, 106 },
		{ Technique.GroupedWWing, 107 },
		{ Technique.GroupedMWing, 108 },
		{ Technique.GroupedSWing, 109 },
		{ Technique.GroupedLWing, 110 },
		{ Technique.GroupedHWing, 111 },
		{ Technique.GroupedPurpleCow, 112 },

		// Others
		{ Technique.FishyCycle, 201 },
		{ Technique.XyCycle, 202 },
		{ Technique.ContinuousNiceLoop, 203 },
		{ Technique.XChain, 204 },
		{ Technique.XyChain, 205 },
		{ Technique.XyXChain, 206 },
		{ Technique.SelfConstraint, 207 },
		{ Technique.AlternatingInferenceChain, 208 },
		{ Technique.DiscontinuousNiceLoop, 209 },
		{ Technique.GroupedFishyCycle, 211 },
		{ Technique.GroupedXyCycle, 212 },
		{ Technique.GroupedContinuousNiceLoop, 213 },
		{ Technique.GroupedXChain, 214 },
		{ Technique.GroupedXyChain, 215 },
		{ Technique.GroupedXyXChain, 216 },
		{ Technique.GroupedSelfConstraint, 217 },
		{ Technique.NodeCollision, 218 },
		{ Technique.GroupedAlternatingInferenceChain, 219 },
		{ Technique.GroupedDiscontinuousNiceLoop, 220 },
	};


	/// <summary>
	/// Try to get sort key of the pattern.
	/// </summary>
	/// <param name="this">The pattern.</param>
	/// <param name="conclusions">Indicates conclusions to be used.</param>
	/// <returns>The pattern sort key.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetSortKey(this ChainPattern @this, ConclusionSet conclusions)
		=> SortKeyDictionary[@this.GetTechnique(conclusions)];
}