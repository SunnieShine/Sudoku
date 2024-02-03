namespace Sudoku.Analytics.Categorization;

/// <summary>
/// Provides with extension methods on <see cref="Technique"/>.
/// </summary>
/// <seealso cref="Technique"/>
public static class TechniqueExtensions
{
	/// <summary>
	/// Represents an error message that complex single field is not supported.
	/// </summary>
	private static readonly string Error_ComplexSingleNotSupportedToday = """
		The group of indirect technique field is neither locked candidates nor subset, which is not supported today. 
		In future, the field might be expanded to wider, and this value might be available.
		""".RemoveLineEndings();

	/// <summary>
	/// The internal <see cref="Type"/> instance to visit members for <see cref="Technique"/> via reflection.
	/// </summary>
	private static readonly Type TypeOfTechnique = typeof(Technique);


	/// <summary>
	/// Try to get the name of the current <see cref="Technique"/>.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <param name="culture">The culture information.</param>
	/// <returns>The name of the current technique.</returns>
	/// <exception cref="ResourceNotFoundException">Throws when the target name is not found in resource dictionary.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetName(this Technique @this, CultureInfo? culture = null)
		=> ResourceDictionary.GetOrNull(@this.ToString(), culture ?? CultureInfo.CurrentUICulture)
		?? ResourceDictionary.Get(@this.ToString(), ResourceDictionary.DefaultCulture);

	/// <summary>
	/// Try to get the English name of the current <see cref="Technique"/>.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>The name of the current technique.</returns>
	/// <exception cref="ResourceNotFoundException">Throws when the target name is not found in resource dictionary.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? GetEnglishName(this Technique @this) => ResourceDictionary.Get(@this.ToString(), ResourceDictionary.DefaultCulture);

	/// <summary>
	/// Try to get the abbreviation of the current <see cref="Technique"/>.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>The abbreviation of the current technique.</returns>
	/// <remarks>
	/// The routing rule can be described as below:
	/// <list type="number">
	/// <item>
	/// Check whether the field is marked attribute <see cref="AbbreviationAttribute"/>,
	/// and return property value <see cref="AbbreviationAttribute.Abbreviation"/> if marked.
	/// </item>
	/// <item>If 1) returns <see langword="null"/>, then search for resource dictionary, and return the target value if found.</item>
	/// <item>If 2) returns <see langword="null"/>, then check its <see cref="TechniqueGroup"/>, then return its abbreviation if worth.</item>
	/// <item>If 3) returns <see langword="null"/>, this method will return <see langword="null"/>; otherwise, a valid value.</item>
	/// </list>
	/// </remarks>
	/// <seealso cref="AbbreviationAttribute"/>
	/// <seealso cref="TechniqueGroup"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? GetAbbreviation(this Technique @this)
		=> TypeOfTechnique.GetField(@this.ToString())!.GetCustomAttribute<AbbreviationAttribute>()?.Abbreviation
		?? ResourceDictionary.GetOrNull($"TechniqueAbbr_{@this}", ResourceDictionary.DefaultCulture)
		?? @this.GetGroup().GetAbbreviation();

	/// <summary>
	/// Try to get all aliases of the current <see cref="Technique"/>.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <param name="cultureInfo">The culture information.</param>
	/// <returns>
	/// All possible aliases of the current technique.
	/// If the technique does not contain any aliases, the return value will be <see langword="null"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string[]? GetAliases(this Technique @this, CultureInfo? cultureInfo = null)
		=> ResourceDictionary.GetOrNull($"TechniqueAlias_{@this}", cultureInfo ?? CultureInfo.CurrentUICulture)?.SplitBy([';']);

	/// <summary>
	/// Try to get all configured links to EnjoySudoku forum describing the current technique.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>All configured links.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string[] GetIntroductionHyperlinks(this Technique @this)
		=> from attr in (ReferenceLinkAttribute[])TypeOfTechnique.GetField(@this.ToString())!.GetCustomAttributes<ReferenceLinkAttribute>() select attr.Link;

	/// <summary>
	/// Combine the technique with indirect technique, to produce a new <see cref="Technique"/> field, describing the complex single usage.
	/// For example, <see cref="Technique.CrosshatchingBlock"/> combining with <see cref="Technique.HiddenPair"/>
	/// will produce <see cref="Technique.HiddenPairCrosshatchingBlock"/>.
	/// </summary>
	/// <param name="this">
	/// The direct technique used. The value can be:
	/// <list type="bullet">
	/// <item><see cref="Technique.FullHouse"/></item>
	/// <item><see cref="Technique.CrosshatchingBlock"/></item>
	/// <item><see cref="Technique.CrosshatchingRow"/></item>
	/// <item><see cref="Technique.CrosshatchingColumn"/></item>
	/// <item><see cref="Technique.NakedSingle"/></item>
	/// </list>
	/// </param>
	/// <param name="indirect">
	/// The indirect technique used. The value can be ones of group <see cref="TechniqueGroup.LockedCandidates"/>
	/// and <see cref="TechniqueGroup.Subset"/>.
	/// </param>
	/// <returns>The combined result.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the argument <paramref name="this"/> is not a direct technique.</exception>
	/// <exception cref="NotSupportedException">
	/// Throws when the argument <paramref name="indirect"/> is neither locked candidates nor subset.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Technique ComplexSingleUsing(this Technique @this, Technique indirect)
		=> @this switch
		{
			Technique.FullHouse or >= Technique.CrosshatchingBlock and <= Technique.CrosshatchingColumn or Technique.NakedSingle
				=> indirect.GetGroup() switch
				{
					TechniqueGroup.LockedCandidates or TechniqueGroup.Subset => Enum.Parse<Technique>($"{indirect}{@this}"),
					_ when Enum.IsDefined(indirect) => throw new NotSupportedException(Error_ComplexSingleNotSupportedToday),
					_ => throw new ArgumentOutOfRangeException(nameof(indirect))
				},
			_ => throw new ArgumentOutOfRangeException(nameof(@this))
		};

	/// <summary>
	/// Try to get the group that the current <see cref="Technique"/> belongs to. If a technique doesn't contain a corresponding group,
	/// this method will return <see langword="null"/>. No exception will be thrown.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>The <see cref="TechniqueGroup"/> value that the current <see cref="Technique"/> belongs to.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TechniqueGroup? TryGetGroup(this Technique @this)
		=> TypeOfTechnique.GetField(@this.ToString())?.GetCustomAttribute<TechniqueGroupAttribute>()?.Group;

	/// <summary>
	/// Try to get the group that the current <see cref="Technique"/> belongs to.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>The <see cref="TechniqueGroup"/> value that the current <see cref="Technique"/> belongs to.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when the specified <see cref="Technique"/> does not belong to any <see cref="TechniqueGroup"/>.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TechniqueGroup GetGroup(this Technique @this)
		=> @this.TryGetGroup() ?? throw new MissingTechniqueGroupException(@this.ToString());

	/// <summary>
	/// Try to get its static difficulty level for the specified technique.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>
	/// The difficulty level that the current technique belongs to.
	/// If it doesn't contain a static difficulty level value, <see cref="DifficultyLevel.Unknown"/> will be returned.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DifficultyLevel GetDifficultyLevel(this Technique @this)
	{
		var fi = TypeOfTechnique.GetField(@this.ToString())!;
		return (fi.GetCustomAttribute<TechniqueFeatureAttribute>(), fi.GetCustomAttribute<DifficultyLevelAttribute>()) switch
		{
			({ Features: var feature }, _) when feature.HasFlag(TechniqueFeature.NotImplemented) => DifficultyLevel.Unknown,
			(_, { Level: var level }) => level,
			_ => throw new MissingDifficultyLevelException(@this.ToString())
		};
	}

	/// <summary>
	/// Try to get features for the current <see cref="Technique"/>.
	/// </summary>
	/// <param name="this">The <see cref="Technique"/> instance.</param>
	/// <returns>All found features for the current <see cref="Technique"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TechniqueFeature GetFeature(this Technique @this)
		=> TypeOfTechnique.GetField(@this.ToString())?.GetCustomAttribute<TechniqueFeatureAttribute>()?.Features ?? 0;

	/// <summary>
	/// Try to convert the current array instance into a <see cref="TechniqueSet"/> instance.
	/// </summary>
	/// <param name="this">The current instance.</param>
	/// <returns>The final result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TechniqueSet AsTechniqueSet(this Technique[] @this) => [.. @this];
}
